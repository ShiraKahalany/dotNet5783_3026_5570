using DalApi;
using DO;
using System.Security.Cryptography.X509Certificates;

namespace Dal;
public class DalOrderItem :IOrderItem
{
    DataSource dataSource = DataSource.s_instance;
    int Add(OrderItem item)
    {
        OrderItem? temp = dataSource.OrderItems.Find(x => x?.ID == item.ID);
        if (temp == null)
            throw new Exception("The orderItem already exists");
       dataSource.OrderItems.Add(item);
        //dataSource.OrderItems.Add(new OrderItem { ID = item.ID, Amount = item.Amount, IsDeleted = false, OrderID = item.OrderID, Price = item.Price, ProductID = item.ProductID }); ;
        return item.ID;
    }
    OrderItem GetByID(int id)
    {
        foreach(OrderItem? item in dataSource.OrderItems) { if(item?.IsDeleted==false && item.GetValueOrDefault().ID == id) return (OrderItem)item; }
        throw new Exception("The orderitem is not exist");
    }
    void Update(OrderItem item)
    {
        OrderItem? temp = dataSource.OrderItems.Find(x => x?.ID == item.ID);
        if (temp == null) //if it is not exist throw exception
            throw new Exception("The OrderItem is not exist");
        if (temp?.IsDeleted == true)
            throw new Exception("The OrderItem is deleted");
        Delete(item.ID);
        Add(item);
    }
    void Delete(int id)
    {
        OrderItem? temp = dataSource.OrderItems.Find(x => x?.ID == id); //check if the element exist in the orders list
        if (temp == null) //if it is not exist throw exception
            throw new Exception("The orderItem is not exist");
        if (temp?.IsDeleted == true)
            throw new Exception("The orderItem is already deleted");
        dataSource.OrderItems.Remove(temp);
        OrderItem orderItem = new OrderItem { IsDeleted=true, ID=temp.GetValueOrDefault().ID, Amount=temp?.Amount, OrderID=temp?.OrderID, Price=temp?.Price, ProductID=temp?.ProductID};
        Add(orderItem);
    }
    OrderItem GetByOrderAndId(int orderId, int productId)
    {
        foreach(OrderItem? item in dataSource.OrderItems) { if(item?.IsDeleted==false&&item?.ProductID == productId && item?.OrderID==orderId) return (OrderItem)item; }
        throw new Exception("The product is not exist");
    }

    //IEnumerable<T?> GetAll(Func<T?, bool>? filter = null);
    IEnumerable<OrderItem> GetAll(int id)
    {
        Order? order = dataSource.Orders.Find(x => x.GetValueOrDefault().ID == id);
        if (order == null)
            throw new Exception("The order is not exist");
        if(order?.IsDeleted==true)
            throw new Exception("The order is deleted");
        List<OrderItem> listGet = new List<OrderItem>();
        foreach (OrderItem? item in dataSource.OrderItems) {if(item?.OrderID==id) listGet.Add((OrderItem)item); }
        return listGet;
    }
    IEnumerable<OrderItem> GetAll()
    {
        List<OrderItem> listGet = new List<OrderItem>();
        foreach (OrderItem? item in dataSource.OrderItems) { if(item?.IsDeleted==false)listGet.Add((OrderItem)item); }
        return listGet;
    }
}
