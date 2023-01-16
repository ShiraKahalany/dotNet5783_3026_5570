namespace Dal;
using DalApi;
using DO;
using System;
using System.Security.Principal;

internal class Order : IOrder
{
    const string s_Orders = "Orders"; //XML Serializer

    public IEnumerable<DO.Order?> GetAll(Func<DO.Order?, bool>? filter = null)
    {
        var listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_Orders)!;
        if(filter == null)
            return listOrders;
        IEnumerable<DO.Order?> x= (filter == null) ? listOrders.OrderBy(or => ((DO.Order)or!).ID)
                              : listOrders.Where(filter).OrderBy(or => ((DO.Order)or!).ID);
        if(!x.Any())
            throw new DO.NotExistException();
        return x;
    }

    //public DO.Order GetById(int id) =>
    //    XMLTools.LoadListFromXMLSerializer<DO.Order>(s_Orders).FirstOrDefault(p => p?.ID == id)
    //    //DalMissingIdException(id, "Order");
    //    ?? throw new DO.NotExistException();

    public int Add(DO.Order Order)
    {
        var listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_Orders);

        if (Order.ID >= 1000 && listOrders.Find(lec => lec?.ID == Order.ID) == null)
        {
            listOrders.Add(Order);
            XMLTools.SaveListToXMLSerializer(listOrders, s_Orders);
            return Order.ID;
        }
        Order.ID = XMLTools.RunningOrderID();
        listOrders.Add(Order);
       XMLTools.SaveListToXMLSerializer(listOrders, s_Orders);
        return Order.ID;
    }

    public void Delete(int id)
    {
        var listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_Orders);
        
        DO.Order or = listOrders.Find(p => p?.ID == id) ?? throw new DO.NotExistException("missing id");

        if (or.IsDeleted == true)
            throw new DO.NotExistException();
        listOrders.Remove(or);
        DO.Order order =new DO.Order { IsDeleted = true,ID=or.ID, CustomerAddress=or.CustomerAddress, CustomerEmail=or.CustomerEmail, CustomerName=or.CustomerName, DeliveryDate=or.DeliveryDate, OrderDate=or.OrderDate, ShipDate=or.ShipDate };
        listOrders.Add(order);
        XMLTools.SaveListToXMLSerializer(listOrders, s_Orders);
    }

    public void Update(DO.Order Order)
    {
        var listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_Orders);

        DO.Order or = listOrders.Find(p => p?.ID == Order.ID) ?? throw new DO.NotExistException("missing id");

        if (or.IsDeleted == true)
            throw new DO.NotExistException();
        DeletePermanently(Order.ID);
        Add(Order);
    }

    public void DeletePermanently(int id)
    {
        var listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_Orders);

        DO.Order or = listOrders.Find(p => p?.ID == id) ?? throw new DO.NotExistException("missing id");
        listOrders.Remove(or);
        XMLTools.SaveListToXMLSerializer(listOrders, s_Orders);
    }

    public void Restore(DO.Order item)
    {
        var listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_Orders);

        DO.Order or = listOrders.Find(p => p?.ID == item.ID) ?? throw new DO.NotExistException("missing id");
        if (or.IsDeleted == false)
            throw new DO.NotExistException();
        DeletePermanently(item.ID);
        item.IsDeleted = false;
        Add(item);
    }

    

    public DO.Order? GetTByFilter(Func<DO.Order?, bool> filter)
    {
        var listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_Orders);

        DO.Order or = listOrders.Find((DO.Order? p) => filter(p)) ?? throw new DO.NotExistException("missing id");
        return or;
    }

   
}