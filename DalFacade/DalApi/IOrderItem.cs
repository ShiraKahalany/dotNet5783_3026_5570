using DO;


namespace DalApi;

public interface IOrderItem : ICrud<OrderItem>
{ 
    OrderItem GetByOrderAndId(int orderId, int productId);

    //IEnumerable<T?> GetAll(Func<T?, bool>? filter = null);
    IEnumerable<OrderItem> GetAll(int id);
}
