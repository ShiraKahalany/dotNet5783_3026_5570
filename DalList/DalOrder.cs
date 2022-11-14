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
    public void Update(Order item) //delete and Add the item to the end of the list
    {
        Delete(item.ID);
        Add(item);
    }
    public void Delete(int id)
    {
        Order? temp = dataSource.Orders.Find(x => x.GetValueOrDefault().ID == id); //check if the element exist in the orders list
        if (temp == null) //if it is not exist throw exception
            throw new Exception("The order is not exist");
        foreach (OrderItem item1 in temp.GetValueOrDefault().Items) { item1.IsDeleted } //update the isdeleted field in each itemorder 

        temp.GetValueOrDefault().IsDeleted = true; //update the isdeleted field of the order
    }

    //IEnumerable<T?> GetAll(Func<T?, bool>? filter = null);
   public IEnumerable<Order> GetAll()
    {
        List<Order> listGet = new List<Order>();
        foreach (Order item in dataSource.Orders) { listGet.Add(item); }
        return listGet;
    }
}
