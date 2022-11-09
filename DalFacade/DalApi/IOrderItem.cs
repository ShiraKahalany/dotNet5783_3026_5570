using DO;


namespace DalApi;

public interface IOrderItem : ICrud<OrderItem>
{
    int Add(OrderItem item);
    OrderItem GetByID(int id);
    void Update(OrderItem item);
    void Delete(int id);
    OrderItem GetByOrderAndId(int orderId, int productId);

    //IEnumerable<T?> GetAll(Func<T?, bool>? filter = null);
    IEnumerable<OrderItem> GetAll(int id);
}
