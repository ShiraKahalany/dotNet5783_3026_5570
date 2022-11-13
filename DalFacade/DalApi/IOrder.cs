using DO;


namespace DalApi;

public interface IOrder :ICrud<Order>
{
    public int Add(Order item);
   
    public Order GetByID(int id);
    public void Update(Order item);
    public void Delete(int id);

    //IEnumerable<T?> GetAll(Func<T?, bool>? filter = null);
    IEnumerable<Order> GetAll();
}
