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
        IEnumerable<DO.Product> listpro = dal.Product.GetAll();
        if (!listpro.Any())
            throw new BO.NoItemsException();
        List<BO.ProductForList> listproducts = new List<BO.ProductForList> ();
        foreach (DO.Product product in listpro)
        {    
            try
            {
                BO.ProductForList p=new BO.ProductForList();
                listproducts.Add(BO.Tools.CopyFields(product,p));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }       
        return listproducts;
    }

    public IEnumerable<BO.Product> GetProducts()
    //עבור מנהל ועבור קטלוג קונה .בקשת רשימת מוצרים
    {
        IEnumerable<DO.Product> listpro = dal.Product.GetAll();
        if (!listpro.Any())
            throw new BO.NoItemsException();
        List<BO.Product> listproducts = new List<BO.Product>();
        foreach (DO.Product product in listpro)
        {
            try
            {
                BO.Product p = new BO.Product();
                listproducts.Add(BO.Tools.CopyFields(product,p));
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
            IEnumerable<DO.Product> listpro = dal.Product.GetAllWithDeleted();
            if (!listpro.Any())
                throw new BO.NoItemsException();
            List<BO.ProductForList> listproducts = new List<BO.ProductForList>();
            foreach (DO.Product product in listpro)
            {
                BO.ProductForList p = new BO.ProductForList();
                listproducts.Add(BO.Tools.CopyFields(product,p));
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
            IEnumerable<DO.Product> listpro = dal.Product.GetAllDeleted();
            if (!listpro.Any())
                throw new BO.NoItemsException();
            List<BO.ProductForList> listproducts = new List<BO.ProductForList>();
            foreach (DO.Product product in listpro)
            {
                BO.ProductForList p = new BO.ProductForList();
                listproducts.Add(BO.Tools.CopyFields(product,p));
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
                DO.Product pro = dal.Product.GetByID(id);
               BO.Product product = new BO.Product();
               return BO.Tools.CopyFields(pro,product);
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
                DO.Product pro = dal.Product.GetDeletedById(id);
                BO.Product product = new BO.Product();
                return BO.Tools.CopyFields(pro,product);
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
        if(id<=0)
            throw new BO.NotExistException();
        try
        {
            DO.Product p = dal.Product.GetDeletedById(id);
            dal.Product.Restore(p);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }

    public BO.ProductItem GetProduct(int id,BO.Cart cart)
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
                    IsInStock = (counter > 0) 
                    ////path???????????
                };
                return prod;
            }
            throw new BO.NotExistException();
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
                DO.Product p = new DO.Product();
                dal.Product.Add(BO.Tools.CopyFields(product, p));
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
            if(id<0)
                throw new BO.NotExistException();
            IEnumerable<DO.Order> lst = dal.Order.GetAll();
            foreach (DO.Order order in lst)
            {
                if(dal.OrderItem.GetByOrderAndId(order.ID, id)!=null)
                    throw new BO.InAnOrderException();
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

      if(!((newproduct.ID > 0) && (newproduct.Name != null) && (newproduct.Price > 0) && (newproduct.InStock >= 0)))
           throw new BO.NotExistException();
        try
        {
            DO.Product p =new DO.Product();
            dal.Product.Add(BO.Tools.CopyFields(newproduct,p));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

}