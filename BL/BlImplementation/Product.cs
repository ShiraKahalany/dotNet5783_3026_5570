using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using DalApi;


namespace BlImplementation;
using BO;
using DO;

internal class Product : IProduct 
{
    DalApi.IDal dal = DalApi.DalFactory.GetDal() ?? throw new NullReferenceException("Missing Dal");
   
    public IEnumerable<BO.ProductForList> GetListedProducts() 
     //עבור מנהל ועבור קטלוג קונה .בקשת רשימת מוצרים
    {
        try
        {
        IEnumerable<DO.Product> listpro = dal.Product.GetAll();
            if (!listpro.Any()) throw new MyExceptionNoItems();
        List<BO.ProductForList> listproducts = new List<BO.ProductForList> ();
        foreach (DO.Product product in listpro)
        {
            listproducts.Add(new ProductForList
            {
                IsDeleted = false,
                ID = product.ID,
                Name = product.Name,
                Price = product.Price,
                Category = (BO.Category?)product.Category
            }) ;
        }       
        return listproducts;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }
    public IEnumerable<BO.ProductForList> GetListedProductsWithDeleted()
    // בקשת רשימת מוצרים -כולל מחוקים- עבור המנהל ועבור קטלוג קונה
    {
        try
        {
            IEnumerable<DO.Product> listpro = dal.Product.GetAllWithDeleted();
            if (!listpro.Any()) throw new MyExceptionNoItems();
            List<BO.ProductForList> listproducts = new List<BO.ProductForList>();
            foreach (DO.Product product in listpro)
            {
                listproducts.Add(new ProductForList
                {
                    IsDeleted = false,
                    ID = product.ID,
                    Name = product.Name,
                    Price = product.Price,
                    Category = (BO.Category?)product.Category
                });
            }
            return listproducts;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public BO.Product GetProducts(int id)
    //בקשת פרטי מוצר עבור מנהל
    {
        try
        {
            if (id > 0)
            {
                DO.Product pro = dal.Product.GetByID(id);
                BO.Product prod = new BO.Product
                {
                    ID = pro.ID,
                    IsDeleted = pro.IsDeleted,
                    Name = pro.Name,
                    Price = pro.Price,
                    Category = (BO.Category?)pro.Category,
                    InStock = pro.InStock
                    ////path???????????
                };
                    return prod;
            }
            throw new BO.MyExceptionNotExist();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public ProductItem GetProducts(int id,BO.Cart cart)
    // בקשת פרטי מוצר עבור הקונה
    {
        try
        {
            if (id > 0)
            {
                DO.Product pro = dal.Product.GetByID(id);
                int counter = 0;
                foreach(BO.OrderItem? item in cart.Items) { if(item?.ProductID==id) counter++; }
                BO.ProductItem prod = new BO.ProductItem
                {
                    IsDeleted = pro.IsDeleted,
                    ID = pro.ID,
                    Name = pro.Name,
                    Price = pro.Price,
                    Category = (BO.Category?)pro.Category,
                    Amount = counter,
                    InStock = (counter > 0) 
                    ////path???????????
                };
                return prod;
            }
            throw new BO.MyExceptionNotExist();
        }
        catch(Exception ex) 
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
                dal.Product.Add(Cloning.Clone(product));
            }
            else
                throw new BO.MyException();
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
            if(id<0)
                throw new BO.MyExceptionNotExist();
            DO.Product prod = dal.Product.GetByID(id); //?אמור לזרוק חרידה מה די-או אם המוצר לא קיים - האם זה מספיק
            IEnumerable<DO.Order> lst = dal.Order.GetAll();
            foreach (DO.Order order in lst)
            {
                if(dal.OrderItem.GetByOrderAndId(order.ID, id)!=null)
                    throw new BO.MyExceptionInAnOrder();
            }
            dal.Product.Delete(id);
        }
        catch(Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public void UpdateProduct(BO.Product newproduct)
    //עידכון נתוני מוצר עבור מנהל
    {

    }



    
    //public IEnumerable<ProductItem> GetProducts()

    //{

    //    //implementaiton needed

    //    throw new NotImplementedException();
    //}
}