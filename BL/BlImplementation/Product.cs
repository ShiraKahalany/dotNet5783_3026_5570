using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
namespace BlImplementation;


//מימוש ממשק מוצר
internal class Product : IProduct
{
    DalApi.IDal dal = DalApi.DalFactory.GetDal() ?? throw new NullReferenceException("Missing Dal");  //מופע הנתונים

    public IEnumerable<BO.ProductForList> GetListedProducts()
    //מתודה לקבלת רשימת כל המוצרים התקפים
    {
        try
        {
            IEnumerable<DO.Product?> listpro = dal.Product.GetAll(null);
            if (!listpro.Any())
                throw new BO.NoItemsException();
            List<BO.ProductForList> listproducts = new List<BO.ProductForList>();

            //  BO ממיר רשימה שלDO  לרשימה של  
            var listproduct = from DO.Product product in listpro
                              let p = new BO.ProductForList()
                              select product.CopyFields(p);
            return listproduct;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public IEnumerable<BO.Product> GetProducts()
    //עבור מנהל ועבור קטלוג קונה .בקשת רשימת מוצרים
    {
        IEnumerable<DO.Product?> listpro = dal.Product.GetAll((DO.Product? product) => product.GetValueOrDefault().IsDeleted == false);
        if (!listpro.Any())
            throw new BO.NoItemsException();
        List<BO.Product> listproducts = new List<BO.Product>();
        foreach (DO.Product product in listpro)
        {
            try
            {
                BO.Product p = new BO.Product();
                listproducts.Add(product.CopyFields(p));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        return listproducts;
    }
    public IEnumerable<BO.ProductForList> GetListedProductsWithDeleted()
    // בקשת רשימת מוצרים -כולל מחוקים- עבור המנהל 
    {
        try
        {
            IEnumerable<DO.Product?> listpro = dal.Product.GetAll(null);
            if (!listpro.Any())
                throw new BO.NoItemsException();
            List<BO.ProductForList> listproducts = new List<BO.ProductForList>();
            foreach (DO.Product product in listpro)
            {
                BO.ProductForList p = new BO.ProductForList();
                listproducts.Add(product.CopyFields(p));
            }
            return listproducts;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    public IEnumerable<BO.ProductForList> GetListedDeletedProducts()
    {
        try
        {
            IEnumerable<DO.Product?> listpro = dal.Product.GetAll((DO.Product? product) => product.GetValueOrDefault().IsDeleted);
            if (!listpro.Any())
                throw new BO.NoItemsException();
            List<BO.ProductForList> listproducts = new List<BO.ProductForList>();
            foreach (DO.Product product in listpro)
            {
                BO.ProductForList p = new BO.ProductForList();
                listproducts.Add(product.CopyFields(p));
            }
            return listproducts;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    public BO.Product GetProduct(int id)
    //בקשת פרטי מוצר עבור מנהל
    {
        try
        {
            if (id > 0)
            {
                DO.Product? pro = dal.Product.GetTByFilter((DO.Product? product) => product.GetValueOrDefault().IsDeleted == false && (product.GetValueOrDefault().ID == id));
                BO.Product product = new BO.Product();
                return pro.CopyFields(product);
            }
            throw new BO.NotExistException();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    public BO.Product GetDeletedById(int id)
    {
        try
        {
            if (id > 0)
            {
                DO.Product? pro = dal.Product.GetTByFilter((DO.Product? product) => product.GetValueOrDefault().IsDeleted && (product.GetValueOrDefault().ID == id));
                BO.Product product = new BO.Product();
                return pro.CopyFields(product);
            }
            throw new BO.NotExistException();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
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
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }

    public BO.ProductItem GetProduct(int id, BO.Cart cart)
    // בקשת פרטי מוצר עבור הקונה
    {
        try
        {
            if (id > 0)
            {
                DO.Product? pro = dal.Product.GetTByFilter((DO.Product? product) => product.GetValueOrDefault().IsDeleted == false && (product.GetValueOrDefault().ID == id)); ;
                int? counter = 0;
                foreach (BO.OrderItem? item in cart.Items) { if (item?.ProductID == id) counter = item?.Amount; }
                BO.ProductItem prod = new BO.ProductItem
                {
                    IsDeleted = pro.GetValueOrDefault().IsDeleted,
                    ID = pro.GetValueOrDefault().ID,
                    Name = pro.GetValueOrDefault().Name,
                    Price = pro?.Price,
                    Category = (BO.Category)pro?.Category,
                    Amount = counter,
                    IsInStock = (counter > 0)
                    ////path???????????
                };
                return prod;
            }
            throw new BO.NotExistException();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public void AddProduct(BO.Product product)
    //הוספת מוצר עבור מנהל
    {
        try
        {
            if ((product.ID > 0) && (product.Name != null) && (product.Price > 0) && (product.InStock >= 0))
            {
                //DO.Product p = new DO.Product();
                dal.Product.Add((DO.Product)Tools.CopyPropToStruct(product, typeof(DO.Product)));
            }
            else
                throw new BO.WrongDetailsException();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
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
            foreach (DO.Order order in lst)
            {
                if (dal.OrderItem.GetByOrderAndId(order.ID, id) != null)
                    throw new BO.InAnOrderException();
            }
            dal.Product.Delete(id);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public void UpdateProduct(BO.Product newproduct)
    //עידכון נתוני מוצר עבור מנהל
    {

        if (!((newproduct.ID > 0) && (newproduct.Name != null) && (newproduct.Price > 0) && (newproduct.InStock >= 0)))
            throw new BO.NotExistException();
        try
        {
            //DO.Product p =new DO.Product();
            dal.Product.Update((DO.Product)Tools.CopyPropToStruct(newproduct, typeof(DO.Product)));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public IEnumerable<BO.ProductForList> GetProductList(BO.Filters enumFilter = BO.Filters.None, Object? filterValue = null)
    {
        IEnumerable<DO.Product?> doProductList =
        enumFilter switch
        {
            BO.Filters.filterByCategory =>
            dal!.Product.GetAll(dp => (dp?.Category == (filterValue != null ? (DO.Category)filterValue : DO.Category.All))&& dp?.IsDeleted==false),

            BO.Filters.filterByName =>
             dal!.Product.GetAll(dp => (dp?.Name == (string?)(filterValue)) && dp?.IsDeleted == false),

            BO.Filters.None =>
            dal!.Product.GetAll((DO.Product? order) => order.GetValueOrDefault().IsDeleted == false),
            _ => dal!.Product.GetAll((DO.Product? order) => order.GetValueOrDefault().IsDeleted == false),
        };

        return (from DO.Product doProduct in doProductList
                select BlApi.Tools.CopyFields(doProduct, new BO.ProductForList()))
               .ToList();
    }

    //public IEnumerable<BO.ProductForList> GetByCategory(BO.Category ct)
    //{
    //    try
    //    {
    //        IEnumerable<DO.Product?> listpro;
    //        listpro = dal.Product.GetAll((DO.Product? product) => product.GetValueOrDefault().IsDeleted == false && ((int)product.GetValueOrDefault().Category == (int)ct));
    //        var listproduct = from DO.Product product in listpro
    //                          let p = new BO.ProductForList()
    //                          select product.CopyFields(p);
    //        return listproduct;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw new Exception(ex.Message);
    //    }


    //}
}