﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;
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
                throw new NotInStockException();

            if(cart.Items!=null)
            {
                foreach (BO.OrderItem? item in cart.Items)
                {
                    if (item == null)
                        break;
                    if (item.ProductID == id) //אם המוצר קיים בסל קניות
                    {
                        item.Amount+=amountToAdd;
                        cart.TotalPrice += product.Price*amountToAdd;
                        cart.TotalPrice = Math.Round(cart.TotalPrice??0, 2);
                        return cart;
                    }
                }
            }
            //אם המוצר עוד לא קיים בסל הקניות
            BO.OrderItem temp = new BO.OrderItem
            {
                ID = 0,
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
            cart.TotalPrice += product.Price;
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
                throw new NotExistException();

            foreach (BO.OrderItem item in cart.Items)
            {
                if (item!=null && item.ProductID == id)
                {
                   if(amount ==0)
                    {
                        cart.Items.Remove(item);
                        cart.TotalPrice -= item.Price * item.Amount;
                        return cart;
                    }
                    int? difference = amount - item.Amount;
                    if (item.Amount < amount)
                    {
                        if (!(product.InStock >= difference))
                            throw new NotInStockException();
                        item.Amount = amount;
                        cart.TotalPrice += item.Price *difference;
                        return cart;
                    }
                    if (item.Amount > amount)
                    {
                        item.Amount = amount;
                        cart.TotalPrice += item.Price * difference;
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
            throw new NotExistException();
        try
        {
            if (cart.CustomerName == null)
                throw new NoNameException();
            if (cart.CustomerAddress == null)
                throw new NoAddressException();
            if ((cart.CustomerEmail == null)||(!cart.CustomerEmail.Contains('@')))
                throw new IllegalEmailException();
            List <OrderItem> newlist = new List<OrderItem>();
            if (cart.Items == null)
                throw new NotItemsInCartException();

            //בדיקת זמינות המוצרים במלאי
            foreach (OrderItem item in cart.Items)
            {
                DO.Product product = dal.Product.GetByID(item?.ProductID??0);
                if (item == null)
                    break;
                if(product.InStock<=item.Amount)
                    throw new NotInStockException();
                if (item.Amount <= 0)
                    throw new AmountNotPossitiveException();
                newlist.Add(item);
            }
            if (newlist.Count < 0)
                throw new NotItemsInCartException();

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
            
            dal.Order.Add(neworder);  //הוספת ההזמנה למאגר ההזמנות

           //עידכון מלאי המוצרים
            foreach (OrderItem item in newlist)
            {
                DO.Product product = dal.Product.GetByID(item?.ProductID ?? 0);
                DO.OrderItem temp=new DO.OrderItem();
                temp.OrderID=neworder.ID;
                dal.OrderItem.Add(item.CopyFields(temp));
                product.InStock-=item.Amount;
            }
            return neworder.ID;
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
