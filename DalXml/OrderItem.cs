﻿namespace Dal;
using DalApi;
using DO;
using System.Security.Principal;

internal class OrderItem : IOrderItem
{
    const string s_OrderItems = "OrderItems"; //XML Serializer

    #region Get All The OrderItems
    public IEnumerable<DO.OrderItem?> GetAll(Func<DO.OrderItem?, bool>? filter = null)
    {
        var listOrderItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_OrderItems)!;
        
        return filter == null ? listOrderItems.OrderBy(or => ((DO.OrderItem)or!).ID)
                              : listOrderItems.Where(filter).OrderBy(or => ((DO.OrderItem)or!).ID);
    }
    #endregion

    #region Add
    public int Add(DO.OrderItem OrderItem)
    {
        var listOrderItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_OrderItems);

        if (OrderItem.ID>=100000 && !listOrderItems.Exists(or => or?.ID == OrderItem.ID))
        {
           listOrderItems.Add(OrderItem);
            XMLTools.SaveListToXMLSerializer(listOrderItems, s_OrderItems);
            return OrderItem.ID;
        }
        OrderItem.ID = XMLTools.RunningOrderItemID();
        listOrderItems.Add(OrderItem);
        XMLTools.SaveListToXMLSerializer(listOrderItems, s_OrderItems);
        return OrderItem.ID;
    }
    #endregion

    #region Delete
    public void Delete(int id)
    {
        var listOrderItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_OrderItems);

        DO.OrderItem or = listOrderItems.Find(p => p?.ID == id) ?? throw new DO.NotExistException("missing id");

        if (or.IsDeleted == true)
            throw new DO.NotExistException();
        listOrderItems.Remove(or);
        DO.OrderItem orderitem = new DO.OrderItem { IsDeleted = true, ID = or.ID, Amount=or.Amount, Name=or.Name, OrderID=or.OrderID, Path=or.Path, Price=or.Price, ProductID=or.ProductID, TotalItem=or.TotalItem};
        listOrderItems.Add(orderitem);
        XMLTools.SaveListToXMLSerializer(listOrderItems, s_OrderItems);
    }
    #endregion

    #region Update
    public void Update(DO.OrderItem OrderItem)
    {
        var listOrderItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_OrderItems);

        DO.OrderItem or = listOrderItems.Find(p => p?.ID == OrderItem.ID) ?? throw new DO.NotExistException("missing id");

        //if (or.IsDeleted == true)
        //    throw new DO.NotExistException();
        DeletePermanently(OrderItem.ID);
        Add(OrderItem);
    }
    #endregion

    #region Delete OrderItem Permanently
    public void DeletePermanently(int id)
    {
        var listOrderItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_OrderItems);

        DO.OrderItem or = listOrderItems.Find(p => p?.ID == id) ?? throw new DO.NotExistException("missing id");
        listOrderItems.Remove(or);
        XMLTools.SaveListToXMLSerializer(listOrderItems, s_OrderItems);
    }
    #endregion

    #region Restore
    public void Restore(DO.OrderItem item)
    {
        var listOrderItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_OrderItems);

        DO.OrderItem or = listOrderItems.Find(p => p?.ID == item.ID) ?? throw new DO.NotExistException("missing id");
        if (or.IsDeleted == false)
            throw new DO.NotExistException();
        DeletePermanently(item.ID);
        item.IsDeleted = false;
        Add(item);
    }
    #endregion

    #region Get OrderItem By Filter
    public DO.OrderItem? GetTByFilter(Func<DO.OrderItem?, bool> filter)
    {
        var listOrderItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_OrderItems);

        DO.OrderItem or = listOrderItems.Find((DO.OrderItem? p) => filter(p)) ?? throw new DO.NotExistException("missing id");
        return or;
    }
    #endregion
}