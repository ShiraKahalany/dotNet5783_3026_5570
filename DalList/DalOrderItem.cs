using DalApi;
using DO;
namespace Dal;
internal class DalOrderItem :IOrderItem
{
    DataSource dataSource = new DataSource();
    int Add(OrderItem item)
    {
        dataSource.AddOrderItem(item);
        return item.ID;
    }
    OrderItem GetByID(int id)
    {
        foreach(OrderItem item in dataSource.OrderItems) { if(item.ID == id) return item; }
        throw new Exception("The orderitem is not exist");
    }
    void Update(OrderItem item)
    {
        Delete(item.ID);
        Add(item);
    }
    void Delete(int id)
    {
        OrderItem? temp = dataSource.OrderItems.Find(x => x.GetValueOrDefault().ID == id); //check if the element exist in the orders list
        if (temp == null) //if it is not exist throw exception
            throw new Exception("The order is not exist");
        temp.GetValueOrDefault().IsDeleted = true; //update the isdeleted field of the order
    }
    OrderItem GetByOrderAndId(int orderId, int productId)
    {
        Order? order = dataSource.Orders.Find(x => x.GetValueOrDefault().ID == orderId);
        if(order == null)
            throw new Exception("The order is not exist");
        foreach(OrderItem item in order.GetValueOrDefault().Items) { if(item.ProductID == productId) return item; }
        throw new Exception("The product is not exist");
    }

    //IEnumerable<T?> GetAll(Func<T?, bool>? filter = null);
    IEnumerable<OrderItem> GetAll(int id)
    {
        Order? order = dataSource.Orders.Find(x => x.GetValueOrDefault().ID == id);
        if (order == null)
            throw new Exception("The order is not exist");
        List<OrderItem> listGet = new List<OrderItem>();
        foreach (OrderItem item in order.GetValueOrDefault().Items) { listGet.Add(item); }
        return listGet;
    }
    IEnumerable<OrderItem> GetAll()
    {
        List<OrderItem> listGet = new List<OrderItem>();
        foreach (OrderItem item in dataSource.OrderItems) { listGet.Add(item); }
        return listGet;
    }
}
