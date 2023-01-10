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
        if (item.ID>=1000 && dataSource.Orders.Find(x => x?.ID == item.ID) == null)
        {
            dataSource.Orders.Add(item);
            return item.ID;
        }
        item.ID = DataSource.Config.NextOrderNumber;
        dataSource.Orders.Add(item);
        return item.ID;
    }

    public void Update(Order item)
        //מתודה המעדכנת את הזמנה להזמנה המעודכנת שהתקבלה (שיש לה אותו ת"ז)ו
    {
        Order? temp = dataSource.Orders.Find(x => x?.ID == item.ID);
        if (temp == null) //if it is not exist throw exception
            throw new DO.NotExistException("The item is not exist");
        if (temp?.IsDeleted == true)
            throw new DO.NotExistException("The item is not exist");
        DeletePermanently(item.ID);
        Add(item);
    }

    public void Restore(Order item)
    //מתודה המעדכנת את הזמנה להזמנה המעודכנת שהתקבלה (שיש לה אותו ת"ז)ו
    {
        Order? temp = dataSource.Orders.Find(x => x?.ID == item.ID);
        if (temp == null) //if it is not exist throw exception
            throw new DO.NotExistException("The item is not exist");
        if (temp?.IsDeleted == false)
            throw new DO.NotExistException("The item is not deleted");
        DeletePermanently(item.ID);
        item.IsDeleted = false;
        Add(item);
    }
  
public void DeletePermanently(int id)
    {
        Order? temp = dataSource.Orders.Find(x => x?.ID == id); //check if the element exist in the orders list
        dataSource.Orders.Remove(temp);
    }
    public void Delete(int id)
        //מתודה המוחקת את ההזמנה בעלת הת"ז שהתקבל
    {
        Order? temp = dataSource.Orders.Find(x => x?.ID == id); //check if the element exist in the orders list
        if (temp == null) //if it is not exist throw exception
            throw new DO.NotExistException("The item is not exist");
        if (temp?.IsDeleted == true)
            throw new DO.NotExistException("The item is already deleted");
        dataSource.Orders.Remove(temp);
        Order order = new Order { IsDeleted = true, ID = temp.GetValueOrDefault().ID, CustomerAddress = temp?.CustomerAddress, CustomerEmail = temp?.CustomerEmail, CustomerName = temp?.CustomerName, DeliveryDate = temp?.DeliveryDate, OrderDate = temp?.OrderDate, ShipDate = temp?.ShipDate };
        Add((Order)order);
    }


    public IEnumerable<Order?> GetAll(Func<Order?, bool>? filter = null)
    {
        if(filter == null)
            return dataSource.Orders;
        var ieorders=from order in dataSource.Orders
                    where filter(order) ==true 
                    select order;
        if (!ieorders.Any())
            throw new DO.NotExistException("Not Orders");
        return ieorders;
    }

    public Order? GetTByFilter(Func<Order?, bool> filter)
    {
        DO.Order? item = dataSource.Orders.Find((DO.Order? order) => filter(order));
        if (item == null)
            throw new DO.NotExistException("Not Exist - Order");
        return item;

    }

}
