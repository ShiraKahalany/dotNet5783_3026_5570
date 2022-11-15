using DalApi;
using DO;
namespace Dal;
internal class DalOrder : IOrder
{
    DataSource dataSource=DataSource.s_instance;
    public int Add(Order item) 
    {
        Order? temp = dataSource.Orders.Find(x => x?.ID == item.ID);
        if (temp == null)
            throw new Exception("The order already exists");
        dataSource.Orders.Add(item);
        //dataSource.Orders.Add(new Order { ID = item.ID, IsDeleted = false, CustomerAdress=item.CustomerAdress, CustomerEmail=item.CustomerEmail, CustomerName=item.CustomerName, DeliveryrDate=item.DeliveryrDate, OrderDate=item.OrderDate, shipDate=item.shipDate }); ;
        return item.ID;
    }
    public Order GetByID(int id) 
    {
        foreach(Order? item in dataSource.Orders) { if(item?.IsDeleted==false && item.GetValueOrDefault().ID == id) return (Order)item; }
        throw new Exception("The order is not exist");
    }
    public void Update(Order item) //delete and Add the item to the end of the list
    {
        Order? temp = dataSource.Orders.Find(x => x?.ID == item.ID);
        if (temp == null) //if it is not exist throw exception
            throw new Exception("The Order is not exist");
        if (temp?.IsDeleted == true)
            throw new Exception("The Order is deleted");
        Delete(item.ID);
        Add(item);
    }
    public void Delete(int id)
    {
        Order? temp = dataSource.Orders.Find(x => x?.ID == id); //check if the element exist in the orders list
        if (temp == null) //if it is not exist throw exception
            throw new Exception("The order is not exist");
        if (temp?.IsDeleted == true)
            throw new Exception("The order is already deleted");
        dataSource.Orders.Remove(temp);
        Order order = new Order { IsDeleted = true, ID = temp.GetValueOrDefault().ID, CustomerAdress = temp?.CustomerAdress, CustomerEmail = temp?.CustomerEmail, CustomerName = temp?.CustomerName, DeliveryrDate = temp?.DeliveryrDate, OrderDate = temp?.OrderDate, shipDate = temp?.OrderDate };
        Add((Order)order);
    }

    //IEnumerable<T?> GetAll(Func<T?, bool>? filter = null);
   public IEnumerable<Order> GetAll()
    {
        List<Order> listGet = new List<Order>();
        foreach (Order? item in dataSource.Orders) { if(item?.IsDeleted==false)listGet.Add((Order)item); }
        return listGet;
    }
}
