namespace Dal;
using DalApi;
using DO;
using System.Security.Principal;

internal class OrderItem : IOrderItem
{
    const string s_OrderItems = "OrderItems"; //XML Serializer

    public IEnumerable<DO.OrderItem?> GetAll(Func<DO.OrderItem?, bool>? filter = null)
    {
        var listOrderItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_OrderItems)!;
        return filter == null ? listOrderItems.OrderBy(lec => ((DO.OrderItem)lec!).ID)
                              : listOrderItems.Where(filter).OrderBy(lec => ((DO.OrderItem)lec!).ID);
    }

    public DO.OrderItem GetById(int id) =>
        XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_OrderItems).FirstOrDefault(p => p?.ID == id)
        //DalMissingIdException(id, "OrderItem");
        ?? throw new Exception("missing id");

    public int Add(DO.OrderItem OrderItem)
    {
        var listOrderItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_OrderItems);

        if (listOrderItems.Exists(lec => lec?.ID == OrderItem.ID))
            throw new Exception("id already exist");//DalAlreadyExistIdException(OrderItem.ID, "OrderItem");

        listOrderItems.Add(OrderItem);

        XMLTools.SaveListToXMLSerializer(listOrderItems, s_OrderItems);

        return OrderItem.ID;
    }

    public void Delete(int id)
    {
        var listOrderItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_OrderItems);

        if (listOrderItems.RemoveAll(p => p?.ID == id) == 0)
            throw new Exception("missing id"); //new DalMissingIdException(id, "OrderItem");

        XMLTools.SaveListToXMLSerializer(listOrderItems, s_OrderItems);
    }

    public void Update(DO.OrderItem OrderItem)
    {
        Delete(OrderItem.ID);
        Add(OrderItem);
    }

    public DO.OrderItem GetItem(int IdOrder, int IdProduct)
    {
        var listOrderItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_OrderItems);

        return listOrderItems.FirstOrDefault(item => item?.ProductID == IdProduct && item?.OrderID == IdOrder)
            ?? throw new DoesNotExistException(IdOrder); ;
    }
}