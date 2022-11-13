using DalApi;
using DO;
namespace Dal;
internal class DalOrder : IOrder
{
    DataSource dataSource=new DataSource();
    public int Add(Order item) 
    {
        dataSource.AddOrder(item);
        return item.ID;
    }
    public Order GetByID(int id) 
    {
        foreach(Order item in dataSource.Orders) { if(item.ID == id) return item; }
        return null;
    }
    public void Update(Order item)
    {
        
    }
    public void Delete(int id)
    {

    }

    //IEnumerable<T?> GetAll(Func<T?, bool>? filter = null);
   public IEnumerable<Order> GetAll()
    {

    }
}
