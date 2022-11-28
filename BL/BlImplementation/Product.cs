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
        IEnumerable<DO.Product> listpro = dal.Product.GetAll();
        if (!listpro.Any())
            throw new NoItemsException();
        List<BO.ProductForList> listproducts = new List<BO.ProductForList> ();
        foreach (DO.Product product in listpro)
        {
            try
            {
                ProductForList p=new ProductForList();
                listproducts.Add(product.CopyFields(p));
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
            throw new NoItemsException();
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
    // בקשת רשימת מוצרים -כולל מחוקים- עבור המנהל ועבור קטלוג קונה
    {
        try
        {
            IEnumerable<DO.Product> listpro = dal.Product.GetAllWithDeleted();
            if (!listpro.Any())
                throw new NoItemsException();
            List<BO.ProductForList> listproducts = new List<BO.ProductForList>();
            foreach (DO.Product product in listpro)
            {
                ProductForList p = new ProductForList();
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
                DO.Product pro = dal.Product.GetByID(id);
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
    public ProductItem GetProduct(int id,BO.Cart cart)
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
                dal.Product.Add(product.CopyFields(p));
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
            dal.Product.Add(newproduct.CopyFields(p));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }

}