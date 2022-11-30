using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using DalApi;

namespace BlImplementation;
using BO;

internal class Cart:ICart
{
    DalApi.IDal dal = DalApi.DalFactory.GetDal() ?? throw new NullReferenceException("Missing Dal");
    
    public BO.Cart AddProductToCart(BO.Cart cart, int id)
    {
        try
        {
            DO.Product product = dal.Product.GetByID(id);
            if (!(product.InStock < 0))
                throw new NotInStockException();
            foreach (BO.OrderItem? item in cart.Items)
            {
                if (item.ProductID == id) //אם המוצר קיים בסל קניות
                {
                        item.Amount++;
                        cart.TotalPrice+=product.Price;
                        return cart;
                }
            }
            Random rand = new Random();
            int newId = rand.Next(8999)+1000;
            BO.OrderItem temp = new BO.OrderItem
            {
                ID = newId, /////how he should put
                ProductID = id,
                Price = product.Price,
                IsDeleted = false,
                Amount = 1
            };
            cart.Items.Add(temp);
            cart.TotalPrice += product.Price;
            return cart;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public BO.Cart UpdateAmountOfProductInCart(BO.Cart cart, int id, int amount)
    {
        try
        {
            DO.Product product = dal.Product.GetByID(id);
            foreach (BO.OrderItem item in cart.Items)
            {
                if (item.ProductID == id)
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

    public int MakeAnOrder(BO.Cart cart)
    {
        try
        {
            if (cart.CustomerName == null)
                throw new NoNameException();
            if (cart.CustomerAddress == null)
                throw new NoAddressException();
            if ((cart.CustomerEmail == null)||(!cart.CustomerEmail.Contains('@')))
                throw new IllegalEmailException();
            List <OrderItem> newlist = new List<OrderItem>();  
            foreach (OrderItem item in cart.Items)
            {
                DO.Product product = dal.Product.GetByID(item?.ProductID??0);
                if(product.InStock<=0)
                    throw new NotInStockException();
                if (item.Amount <= 0)
                    throw new AmountNotPossitiveException();
                newlist.Add(item);
            }
            DO.Order neworder = new DO.Order
            {
                IsDeleted = false,
                ID = 1234,///how to bring id
                CustomerName = cart.CustomerName,
                CustomerEmail = cart.CustomerEmail,
                CustomerAddress = cart.CustomerAddress,
                OrderDate = DateTime.Now,
                ShipDate = null, 
                DeliveryDate = null
            };
            dal.Order.Add(neworder);
            foreach(OrderItem item in newlist)
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

}
