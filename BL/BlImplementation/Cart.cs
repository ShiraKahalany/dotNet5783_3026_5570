using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
namespace BlImplementation;

//מימוש המתודות של סל קניות
internal class Cart:ICart
{
    DalApi.IDal dal = DalApi.DalFactory.GetDal() ?? throw new NullReferenceException("Missing Dal");  //מופע הנתונים
    
    public BO.Cart? AddProductToCart(BO.Cart? cart, int id, int amountToAdd)
        //מתודה המקבלת עגלה,מספר מזהה של מוצר, וכמות להוספה, ומוסיפה את המוצר לעגלה
    {
        if (cart == null)
            throw new ArgumentNullException();
        try
        {
            DO.Product product = dal.Product.GetByID(id);
            if (product.InStock < amountToAdd)
                throw new BO.NotInStockException();
            if(cart.Items!=null)
            {
                foreach (BO.OrderItem? item in cart.Items)
                {
                    if (item == null)
                        break;
                    if (item.ProductID == id) //אם המוצר קיים בסל קניות
                    {
                        item.Amount+=amountToAdd;
                        cart.TotalPrice = (cart.TotalPrice??0) + (product.Price*amountToAdd);
                        cart.TotalPrice = Math.Round(cart.TotalPrice??0, 2);
                        return cart;
                    }
                }
            }
            //אם המוצר עוד לא קיים בסל הקניות
            BO.OrderItem temp = new BO.OrderItem
            {
                ID = cart.Items.Count+1,
                ProductID = id,
                Price = product.Price,
                IsDeleted = false,
                Amount = amountToAdd
            };

            if(cart.Items!=null)
                cart.Items.Add(temp);
            else
            {
                cart.Items=new List<BO.OrderItem>();
                cart.Items.Add(temp);
            }
            cart.TotalPrice = (cart.TotalPrice??0)+ (product.Price*amountToAdd);
            cart.TotalPrice = Math.Round(cart.TotalPrice ?? 0, 2);
            return cart;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public BO.Cart UpdateAmountOfProductInCart(BO.Cart cart, int id, int amount)
        //מתודה המקבלת סל קניות, מזהה מוצר, וכמות רצויה - ומעדכנת את הכמות של המורצ בסל לכמות הרצויה
    {
        try
        {
            DO.Product product = dal.Product.GetByID(id);
            if (cart.Items == null)
                throw new BO.NotExistException();

            foreach (BO.OrderItem item in cart.Items)
            {
                if (item!=null && item.ProductID == id)
                {
                   if(amount ==0)
                    {
                        cart.Items.Remove(item);
                        cart.TotalPrice = cart.TotalPrice??0 - (item.Price * item.Amount);
                        cart.TotalPrice = Math.Round(cart.TotalPrice ?? 0, 2);
                        return cart;
                    }
                    int? difference = amount - item.Amount;
                    if (item.Amount < amount)
                    {
                        if (!(product.InStock >= difference))
                            throw new BO.NotInStockException();
                        item.Amount = amount;
                        cart.TotalPrice = (cart.TotalPrice??0) + (item.Price *difference);
                        cart.TotalPrice = Math.Round(cart.TotalPrice ?? 0, 2);
                        return cart;
                    }
                    if (item.Amount > amount)
                    {
                        item.Amount = amount;
                        cart.TotalPrice = (cart.TotalPrice??0 )+ (item.Price * difference);
                        cart.TotalPrice = Math.Round(cart.TotalPrice ?? 0, 2);
                        return cart;
                    }
                }
            }
            return cart;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public int MakeAnOrder(BO.Cart? cart)
        //מתודה המקבלת סל קניות ויוצרת ממנו הזמנה
    {
        if (cart == null)
            throw new BO.NotExistException();
        try
        {
            if (cart.CustomerName == null)
                throw new BO.NoNameException();
            if (cart.CustomerAddress == null)
                throw new BO.NoAddressException();
            if ((cart.CustomerEmail == null)||(!cart.CustomerEmail.Contains('@')))
                throw new BO.IllegalEmailException();
            List <BO.OrderItem> newlist = new List<BO.OrderItem>();
            if (cart.Items==null||cart.Items.Count==0)
                throw new BO.NotItemsInCartException();

            //בדיקת זמינות המוצרים במלאי
            foreach (BO.OrderItem item in cart.Items)
            {
                DO.Product product = dal.Product.GetByID(item?.ProductID??0);
                if (item == null)
                    break;
                if(product.InStock<=item.Amount)
                    throw new BO.NotInStockException();
                if (item.Amount <= 0)
                    throw new BO.AmountNotPossitiveException();
                newlist.Add(item);
            }
            if (newlist.Count < 0)
                throw new BO.NotItemsInCartException();

            //יצירת ההזמנה
            DO.Order neworder = new DO.Order
            {
                IsDeleted = false,
                ID = 0,
                CustomerName = cart.CustomerName,
                CustomerEmail = cart.CustomerEmail,
                CustomerAddress = cart.CustomerAddress,
                OrderDate = DateTime.Now,
                ShipDate = null, 
                DeliveryDate = null
            };
            
            int newId=dal.Order.Add(neworder);  //הוספת ההזמנה למאגר ההזמנות

           //עידכון מלאי המוצרים
            foreach (BO.OrderItem item in newlist)
            {
                DO.Product product = dal.Product.GetByID(item?.ProductID ?? 0);
                DO.OrderItem temp=new DO.OrderItem();
                temp.ProductID=product.ID;
                temp = (DO.OrderItem)Tools.CopyPropToStruct(item, typeof(DO.OrderItem));
                temp.OrderID = newId;
                dal.OrderItem.Add(temp);
                product.InStock-=item.Amount;
                dal.Product.Update(product);
            }
            return newId;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }




    //public void Restore(int id)
    //{
    //    if (id <= 0)
    //        throw new BO.NotExistException();
    //    try
    //    {

    //        BO.Cart cart= new BO.Cart();
    //        cart.IsDeleted


    //        DO.Order c = dal.Order.GetDeletedById(id);
    //        dal.Cart.Restore(c);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw new Exception(ex.Message);
    //    }
    //}
}
