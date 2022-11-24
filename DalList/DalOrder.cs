using DalApi;
using DO;
namespace Dal;

//מימוש ממשק ההזמנות
public class DalOrder : IOrder
{
    DataSource dataSource =DataSource.s_instance;
    public int Add(Order item)
        //מתודה שמקבלת הזמנה ומוסיפה אותה לרשימת ההזמנות
    {
        Order? temp = dataSource.Orders.Find(x => x?.ID == item.ID);
        if (temp != null&&temp?.IsDeleted==false)
            throw new MyException("The item already exists");
        dataSource.Orders.Add(item);
        //dataSource.Orders.Add(new Order { ID = item.ID, IsDeleted = false, CustomerAdress=item.CustomerAdress, CustomerEmail=item.CustomerEmail, CustomerName=item.CustomerName, DeliveryrDate=item.DeliveryrDate, OrderDate=item.OrderDate, shipDate=item.shipDate }); ;
        return item.ID;
    }
    public Order GetByID(int id) 
        //מתודה המקבלת מספר ת"ז ומחזירה את ההזמנה המתאימה
    {
        foreach(Order? item in dataSource.Orders) { if(item?.IsDeleted==false && item.GetValueOrDefault().ID == id) return (Order)item; }
        throw new MyException("The item is not exist");
    }
    public void Update(Order item)
        //מתודה המעדכנת את הזמנה להזמנה המעודכנת שהתקבלה (שיש לה אותו ת"ז)ו
    {
        Order? temp = dataSource.Orders.Find(x => x?.ID == item.ID);
        if (temp == null) //if it is not exist throw exception
            throw new MyException("The item is not exist");
        if (temp?.IsDeleted == true)
            throw new MyException("The item is not exist");
        Delete(item.ID);
        Add(item);
    }
    public void Delete(int id)
        //מתודה המוחקת את ההזמנה בעלת הת"ז שהתקבל
    {
        Order? temp = dataSource.Orders.Find(x => x?.ID == id); //check if the element exist in the orders list
        if (temp == null) //if it is not exist throw exception
            throw new MyException("The item is not exist");
        if (temp?.IsDeleted == true)
            throw new MyException("The item is already deleted");
        dataSource.Orders.Remove(temp);
        Order order = new Order { IsDeleted = true, ID = temp.GetValueOrDefault().ID, CustomerAdress = temp?.CustomerAdress, CustomerEmail = temp?.CustomerEmail, CustomerName = temp?.CustomerName, DeliveryrDate = temp?.DeliveryrDate, OrderDate = temp?.OrderDate, shipDate = temp?.OrderDate };
        Add((Order)order);
    }

    //IEnumerable<T?> GetAll(Func<T?, bool>? filter = null);
   public IEnumerable<Order> GetAll()
        //מתודה שמחזירה את רשימת כל ההזמנות
    {
        List<Order> listGet = new List<Order>();
        foreach (Order? item in dataSource.Orders) { if(item?.IsDeleted==false)listGet.Add((Order)item); }
        return listGet;
    }

   public IEnumerable<Order> GetAllWithDeleted()
        //מתודה המחזירה את רשימת כל ההזמנות, כולל אלו שנמחקו
    {
        List<Order> listGet = new List<Order>();
        foreach (Order? item in dataSource.Orders) {listGet.Add((Order)item); }
        return listGet;
    }
}
