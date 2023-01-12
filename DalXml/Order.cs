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
        IEnumerable<DO.Order?> x= (filter == null) ? listOrders.OrderBy(lec => ((DO.Order)lec!).ID)
                              : listOrders.Where(filter).OrderBy(lec => ((DO.Order)lec!).ID);
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
        Order.ID =
        listOrders.Add(Order);
        XMLTools.SaveListToXMLSerializer(listOrders, s_Orders);
        return Order.ID;
    }

    public void Delete(int id)
    {
        ////var listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_Orders);

        ////if (listOrders.RemoveAll(p => p?.ID == id) == 0)
        ////    throw new Exception("missing id"); //new DalMissingIdException(id, "Order");

        ////XMLTools.SaveListToXMLSerializer(listOrders, s_Orders);

        var listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_Orders);

        var o = listOrders.FirstOrDefault(p => p?.ID == id) ?? throw new Exception("missing id");

        if (o.IsDeleted)
            throw new Exception("missing id"); //new DalMissingIdException(id, "Order");

        DO.Order order = new()
        {
            ID = id,
            CustomerName = o.CustomerName,
            CustomerEmail = o.CustomerEmail,
            CustomerAddress = o.CustomerAddress,
            OrderDate = o.OrderDate,
            DeliveryDate = o.DeliveryDate,
            ShipDate = o.ShipDate,
            IsDeleted = true
        };

        Update(order);
    }

    public void Update(DO.Order Order)
    {
        Delete(Order.ID);
        Add(Order);
    }

    public void DeletePermanently(int id)
    {

    }

    public void Restore(Order item)
    {

    }

    public Order? GetTByFilter(Func<Order?, bool> filter)
    {
        return new Order();
    }
}