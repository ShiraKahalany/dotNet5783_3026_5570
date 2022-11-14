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
        throw new Exception("The order is not exist");
    }
    public void Update(Order item)
    {
        foreach(Order temp in dataSource.Orders)
        {
            if(temp.ID == item.ID)
            {
                foreach(OrderItem item1 in temp.Items) { item1.IsDeleted. }
                dataSource.Orders.Remove(temp);
            }
        }
    }
    public void Delete(int id)
    {

    }

    //IEnumerable<T?> GetAll(Func<T?, bool>? filter = null);
   public IEnumerable<Order> GetAll()
    {

    }
}
