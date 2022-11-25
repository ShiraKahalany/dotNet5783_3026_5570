﻿using DalApi;
using DO;
using System.Security.Cryptography.X509Certificates;

namespace Dal;
//מימוש ממשק המוצרים-בהזמנה
public class DalOrderItem :IOrderItem
{
    DataSource dataSource = DataSource.s_instance;
    public int Add(OrderItem item)
        //מתודת הוספת מוצר בהזמנה
    {
        OrderItem? temp = dataSource.OrderItems.Find(x => x?.ID == item.ID);
        if (temp != null && temp?.IsDeleted == false)
            throw new MyExceptionAlreadyExist("The item already exists");
       dataSource.OrderItems.Add(item);
        //dataSource.OrderItems.Add(new OrderItem { ID = item.ID, Amount = item.Amount, IsDeleted = false, OrderID = item.OrderID, Price = item.Price, ProductID = item.ProductID }); ;
        return item.ID;
    }
    public OrderItem GetByID(int id)
        //מתודה המקבלת מספר ייחודי של מוצר-בהזמנה, ומחזירה את המוצר-בהזמנה
    {
        foreach(OrderItem? item in dataSource.OrderItems) { if(item?.IsDeleted==false && item.GetValueOrDefault().ID == id) return (OrderItem)item; }
        throw new MyExceptionNotExist("The item is not exist");
    }
    public void Update(OrderItem item)
        //מתודת עידכון. מקבלת עצם חדש, ומעדכנת את העצם עם הת"ז הזה להיות העצם המעודכן
    {
        OrderItem? temp = dataSource.OrderItems.Find(x => x?.ID == item.ID);
        if (temp == null) //if it is not exist throw exception
            throw new MyExceptionNotExist("The item is not exist");
        if (temp?.IsDeleted == true)
            throw new MyExceptionNotExist("The item is not exist");
        Delete(item.ID);
        Add(item);
    }
    public void Delete(int id)
        //מתודה למחיקת המוצר-בהזמנה בעל ה ת"ז שהתקבלה
    {
        OrderItem? temp = dataSource.OrderItems.Find(x => x?.ID == id); //check if the element exist in the orders list
        if (temp == null) //if it is not exist throw exception
            throw new MyExceptionNotExist("The item is not exist");
        if (temp?.IsDeleted == true)
            throw new MyExceptionNotExist("The item is already deleted");
        dataSource.OrderItems.Remove(temp);
        OrderItem orderItem = new OrderItem { IsDeleted=true, ID=temp.GetValueOrDefault().ID, Amount=temp?.Amount, OrderID=temp?.OrderID, Price=temp?.Price, ProductID=temp?.ProductID};
        Add(orderItem);
    }
    public OrderItem? GetByOrderAndId(int orderId, int productId)
        //מתודה המקבלת מספר מוצר ומספר הזמנה ומחזירה את המוצר-בהזמנה שמתאים להזמנה הזאת ולמוצר הזה
    {
        foreach(OrderItem? item in dataSource.OrderItems) { if(item?.IsDeleted==false&&item?.ProductID == productId && item?.OrderID==orderId) return (OrderItem)item; }
        return null;
        //throw new MyExceptionNotExist("The item is not exist");
    }

    //IEnumerable<T?> GetAll(Func<T?, bool>? filter = null);
    public IEnumerable<OrderItem> GetAll(int id)
        //מתודה המחזירה את כל המוצרים-בהזמנה של ההזמנה שהת"ז שלה התקבל
    {
        Order? order = dataSource.Orders.Find(x => x.GetValueOrDefault().ID == id);
        if (order == null)
            throw new MyExceptionNotExist("The item is not exist");
        if(order?.IsDeleted==true)
            throw new MyExceptionNotExist("The item is deleted");
        List<OrderItem> listGet = new List<OrderItem>();
        foreach (OrderItem? item in dataSource.OrderItems) {if(item?.OrderID==id) listGet.Add((OrderItem)item); }
        return listGet;
    }
    public IEnumerable<OrderItem> GetAll()
        //מתודה המחזירה רשימה של כל המוצרים-בהזמנה
    {
        List<OrderItem> listGet = new List<OrderItem>();
        foreach (OrderItem? item in dataSource.OrderItems) { if(item?.IsDeleted==false)listGet.Add((OrderItem)item); }
        return listGet;
    }

    public IEnumerable<OrderItem> GetAllWithDeleted()
    //מתודה המחזירה את רשימת כל המוצרים, כולל אלו שנחמקו
    {
        List<OrderItem> listGet = new List<OrderItem>();
        foreach (OrderItem? item in dataSource.OrderItems) {listGet.Add((OrderItem)item); }
        return listGet;
    }
}
