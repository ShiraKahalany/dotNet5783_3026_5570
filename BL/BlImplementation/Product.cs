using BlApi;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;

namespace BlImplementation;


//מימוש ממשק מוצר
internal class Product : IProduct
{
    DalApi.IDal dal = DalApi.DalFactory.GetDal() ?? throw new NullReferenceException("Missing Dal");  //מופע הנתונים

    //public IEnumerable<BO.ProductForList> GetListedProducts()
    ////מתודה לקבלת רשימת כל המוצרים התקפים
    //{
    //    IEnumerable<DO.Product?> listpro = dal.Product.GetAll(null);
    //    if (!listpro.Any())
    //        throw new BO.NoItemsException();
    //    List<BO.ProductForList> listproducts = new List<BO.ProductForList>();

    //    //  BO ממיר רשימה שלDO  לרשימה של  
    //    var listproduct = from DO.Product product in listpro
    //                      let p = new BO.ProductForList()
    //                      select product.CopyFields(p);
    //    return listproduct;
    //}

    //public BO.Product GetProduct(int id)
    ////בקשת פרטי מוצר עבור מנהל
    //{
    //    if (id > 0)
    //    {
    //        DO.Product? pro;
    //        try
    //        {
    //            pro = dal.Product.GetTByFilter((DO.Product? product) => product.GetValueOrDefault().IsDeleted == false && (product.GetValueOrDefault().ID == id));
    //        }
    //        catch (DO.NotExistException ex)
    //        {
    //            throw new BO.NotExistException(ex.Message);
    //        }
    //        BO.Product product = new BO.Product();
    //        return pro.CopyFields(product);
    //    }
    //    throw new BO.NotExistException("Invalid ID");
    //}
    public IEnumerable<BO.Product> GetProducts(BO.Filters enumFilter = BO.Filters.None, Object? filterValue = null)
    //עבור מנהל ועבור קטלוג קונה .בקשת רשימת מוצרים
    {
        IEnumerable<DO.Product?> doProductList =
               enumFilter switch
               {
                   BO.Filters.filterByCategory =>
                   //dal!.Product.GetAll(dp => (dp?.Category == (filterValue != null ? (DO.Category)filterValue : DO.Category.All)) && dp?.IsDeleted == false),
                   dal!.Product.GetAll(dp => ((filterValue != null ? (dp?.Category == (DO.Category)filterValue && dp?.IsDeleted == false) : (dp?.IsDeleted == false)))),
                   BO.Filters.filterByIsDeleted =>
                    dal!.Product.GetAll(dp => dp?.IsDeleted == true),

                   BO.Filters.filterByName =>
                    dal!.Product.GetAll(dp => (dp?.Name == (string?)(filterValue)) && dp?.IsDeleted == false),

                   BO.Filters.None =>
                   dal!.Product.GetAll((DO.Product? order) => order.GetValueOrDefault().IsDeleted == false),
                   _ => dal!.Product.GetAll((DO.Product? order) => order.GetValueOrDefault().IsDeleted == false),
               };

        //return (from DO.Product doProduct in doProductList
        //        select BlApi.Tools.CopyFields(doProduct, new BO.ProductForList()))
        //       .ToList();
        return (from DO.Product doProduct in doProductList
                select BlApi.Tools.CopyFields(doProduct, new BO.Product()));
    }


    public void Restore(int id)
    {
        if (id <= 0)
            throw new BO.NotExistException();
        try
        {
            DO.Product? p = dal.Product.GetTByFilter((DO.Product? product) => product.GetValueOrDefault().IsDeleted && (product.GetValueOrDefault().ID == id));
            dal.Product.Restore((DO.Product)p);
        }
        catch (DO.NotExistException ex)
        {
            throw new BO.NotExistException(ex.Message);
        }

    }

    public BO.ProductItem GetProductItem(int id, BO.Cart cart)
    // בקשת פרטי מוצר עבור הקונה
    {
        try
        {
            if (id > 0)
            {
                DO.Product? pro = dal.Product.GetTByFilter((DO.Product? product) => product.GetValueOrDefault().IsDeleted == false && (product.GetValueOrDefault().ID == id));
                if (cart==null || cart.Items == null)
                {
                    BO.ProductItem p =new BO.ProductItem { Amount = 0, IsInStock = (pro?.InStock > 0) };
                    return pro.CopyFields(p);
                }
                // new BO.NotExistException("There Are No Items In The Cart");
                var LIST = (IEnumerable<BO.OrderItem>)cart.Items;
                int counter = LIST.Sum((BO.OrderItem item) => item.Amount ?? 0);
                BO.ProductItem prod = new BO.ProductItem
                {
                    IsDeleted = pro.GetValueOrDefault().IsDeleted,
                    ID = pro.GetValueOrDefault().ID,
                    Name = pro.GetValueOrDefault().Name,
                    Price = pro?.Price,
                    Category = (BO.Category)pro?.Category!,
                    Amount = counter,
                    IsInStock = (pro?.InStock > 0),
                    Path = pro.GetValueOrDefault().Path
                };
                return prod;
            }
            throw new BO.NotExistException();
        }
        catch (DO.NotExistException ex)
        {
            throw new BO.NotExistException(ex.Message);
        }
    }
    public void AddProduct(BO.Product product)
    //הוספת מוצר עבור מנהל
    {
        try
        {
            if ((product.ID > 0) && (product.Name != null) && (product.Price > 0) && (product.InStock >= 0))
            {
                dal.Product.Add((DO.Product)Tools.CopyPropToStruct(product, typeof(DO.Product)));
            }
            else
                throw new BO.WrongDetailsException();
        }
        catch (DO.AlreadyExistException)
        {
            throw new BO.AlreadyExistException();
        }
    }

    public void DeleteProduct(int id)
    //מחיקת מוצר עבור מנהל
    {
        try
        {
            if (id < 0)
                throw new BO.NotExistException();
            IEnumerable<DO.Order?> lst = dal.Order.GetAll((DO.Order? order) => order.GetValueOrDefault().IsDeleted == false);
            List<DO.Order?> lst2 = lst.ToList();
            if (lst2.Find((DO.Order? order) => dal.OrderItem.GetTByFilter((DO.OrderItem? product) => product.GetValueOrDefault().IsDeleted == false && (product.GetValueOrDefault().OrderID == order?.ID) && (product.GetValueOrDefault().ProductID == id)) != null) != null)
                throw new BO.InAnOrderException();
        }
        catch (DO.NotExistException)
        {
            try
            {
                dal.Product.Delete(id);  //אם המוצר לא מופיע באף הזמנה אפשר למחוק אותו
            }
            catch (DO.NotExistException ex)
            {
                throw new BO.NotExistException(ex.Message);
            }

        }

    }
    public void UpdateProduct(BO.Product newproduct)
    //עידכון נתוני מוצר עבור מנהל
    {

        if (!((newproduct.ID > 0) && (newproduct.Name != null) && (newproduct.Price > 0) && (newproduct.InStock >= 0)))
            throw new BO.NotExistException();
        try
        {
            dal.Product.Update((DO.Product)Tools.CopyPropToStruct(newproduct, typeof(DO.Product)));
        }
        catch (DO.NotExistException ex)
        {
            throw new BO.NotExistException(ex.Message);
        }
    }

    public IEnumerable<BO.ProductForList> GetProductList(BO.Filters enumFilter = BO.Filters.None, Object? filterValue = null)
    {
        IEnumerable<DO.Product?> doProductList =
        enumFilter switch
        {
            BO.Filters.filterByCategory =>
            //dal!.Product.GetAll(dp => (dp?.Category == (filterValue != null ? (DO.Category)filterValue : DO.Category.All)) && dp?.IsDeleted == false),
            dal!.Product.GetAll(dp => ((filterValue != null ? (dp?.Category == (DO.Category)filterValue && dp?.IsDeleted == false) : (dp?.IsDeleted == false)))),



            BO.Filters.filterByName =>
             dal!.Product.GetAll(dp => (dp?.Name == (string?)(filterValue)) && dp?.IsDeleted == false),

            BO.Filters.None =>
            dal!.Product.GetAll((DO.Product? order) => order.GetValueOrDefault().IsDeleted == false),
            _ => dal!.Product.GetAll((DO.Product? order) => order.GetValueOrDefault().IsDeleted == false),
        };

        //return (from DO.Product doProduct in doProductList
        //        select BlApi.Tools.CopyFields(doProduct, new BO.ProductForList()))
        //       .ToList();
        return (from DO.Product doProduct in doProductList
                select BlApi.Tools.CopyFields(doProduct, new BO.ProductForList()));
    }

    public IEnumerable<BO.ProductItem> GetProductItemsList(BO.Cart cart, BO.Filters enumFilter = BO.Filters.None, Object? filterValue = null)
    {

        IEnumerable<DO.Product?> doProductList =
        enumFilter switch
        {
            BO.Filters.filterByCategory =>
            //dal!.Product.GetAll(dp => (dp?.Category == (filterValue != null ? (DO.Category)filterValue : DO.Category.All)) && dp?.IsDeleted == false),
            dal!.Product.GetAll(dp => ((filterValue != null ? (dp?.Category == (DO.Category)filterValue && dp?.IsDeleted == false) : (dp?.IsDeleted == false)))),



            BO.Filters.filterByName =>
             dal!.Product.GetAll(dp => (dp?.Name == (string?)(filterValue)) && dp?.IsDeleted == false),

            BO.Filters.None =>
            dal!.Product.GetAll((DO.Product? order) => order.GetValueOrDefault().IsDeleted == false),
            _ => dal!.Product.GetAll((DO.Product? order) => order.GetValueOrDefault().IsDeleted == false),
        };

        //return (from DO.Product doProduct in doProductList
        //        select BlApi.Tools.CopyFields(doProduct, new BO.ProductForList()))
        //       .ToList();
         var x= from DO.Product doProduct in doProductList
                select GetProductItem(doProduct.ID, cart);
        return x;
               // select BlApi.Tools.CopyFields(doProduct, new BO.ProductItem { IsInStock = (doProduct.InStock > 0), Amount = 0 }));
    }

}