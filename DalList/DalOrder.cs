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
        //Order? temp = dataSource.Orders.Find(x => x?.ID == item.ID);  //כנראה שהבדיקה  מיותרת כי את המספר מזהה יוצרים בעצמינו.
        //if (temp != null)
        //    throw new MyExceptionAlreadyExist("The item already exists");
        dataSource.Orders.Add(item);
        //dataSource.Orders.Add(new Order { ID = item.ID, IsDeleted = false, CustomerAdress=item.CustomerAdress, CustomerEmail=item.CustomerEmail, CustomerName=item.CustomerName, DeliveryrDate=item.DeliveryrDate, OrderDate=item.OrderDate, shipDate=item.shipDate }); ;
        return item.ID;
    }
    public Order GetByID(int id) 
        //מתודה המקבלת מספר ת"ז ומחזירה את ההזמנה המתאימה
    {
        foreach(Order? item in dataSource.Orders)
        {
            if((item?.IsDeleted==false) && (item.GetValueOrDefault().ID == id))
                return (Order)item;
        }
        throw new MyExceptionNotExist("The item is not exist");
    }

    public Order GetDeletedById(int id)
    {
        foreach (Order? item in dataSource.Orders)
        {
            if (item == null)
                throw new MyExceptionNotExist("The item is not exist");
            if (item?.ID == id)
            {
                if (item?.IsDeleted == false)
                    throw new MyExceptionNotExist("The item is not deleted");
                return (Order)item;
            }
        }
        throw new MyExceptionNotExist("The item is not exist");
    }

    public void Update(Order item)
        //מתודה המעדכנת את הזמנה להזמנה המעודכנת שהתקבלה (שיש לה אותו ת"ז)ו
    {
        Order? temp = dataSource.Orders.Find(x => x?.ID == item.ID);
        if (temp == null) //if it is not exist throw exception
            throw new MyExceptionNotExist("The item is not exist");
        if (temp?.IsDeleted == true)
            throw new MyExceptionNotExist("The item is not exist");
        DeletePermanently(item.ID);
        Add(item);
    }

    public void Restore(Order item)
    //מתודה המעדכנת את הזמנה להזמנה המעודכנת שהתקבלה (שיש לה אותו ת"ז)ו
    {
        Order? temp = dataSource.Orders.Find(x => x?.ID == item.ID);
        if (temp == null) //if it is not exist throw exception
            throw new MyExceptionNotExist("The item is not exist");
        if (temp?.IsDeleted == false)
            throw new MyExceptionNotExist("The item is not deleted");
        DeletePermanently(item.ID);
        item.IsDeleted = false;
        Add(item);
    }

   public void DeletePermanently(int id)
    {
        Order? temp = dataSource.Orders.Find(x => x?.ID == id); //check if the element exist in the orders list
        if (temp == null) //if it is not exist throw exception
            throw new MyExceptionNotExist("The item is not exist");
        if (temp?.IsDeleted == false)
            throw new MyExceptionNotExist("The item is not deleted - cant delete permanently");
        dataSource.Orders.Remove(temp);
    }
    public void Delete(int id)
        //מתודה המוחקת את ההזמנה בעלת הת"ז שהתקבל
    {
        Order? temp = dataSource.Orders.Find(x => x?.ID == id); //check if the element exist in the orders list
        if (temp == null) //if it is not exist throw exception
            throw new MyExceptionNotExist("The item is not exist");
        if (temp?.IsDeleted == true)
            throw new MyExceptionNotExist("The item is already deleted");
        dataSource.Orders.Remove(temp);
        Order order = new Order { IsDeleted = true, ID = temp.GetValueOrDefault().ID, CustomerAddress = temp?.CustomerAddress, CustomerEmail = temp?.CustomerEmail, CustomerName = temp?.CustomerName, DeliveryDate = temp?.DeliveryDate, OrderDate = temp?.OrderDate, ShipDate = temp?.OrderDate };
        Add((Order)order);
    }

    //IEnumerable<T?> GetAll(Func<T?, bool>? filter = null);
   public IEnumerable<Order> GetAll()
        //מתודה שמחזירה את רשימת כל ההזמנות
    {
        List<Order> listGet = new List<Order>();
        foreach (Order? item in dataSource.Orders)
        {
            if(item!=null && item?.IsDeleted==false)
                listGet.Add((Order)item);
        }
        return listGet;
    }

   public IEnumerable<Order> GetAllWithDeleted()
        //מתודה המחזירה את רשימת כל ההזמנות, כולל אלו שנמחקו
    {
        List<Order> listGet = new List<Order>();
        foreach (Order? item in dataSource.Orders)
        {
            if(item != null)
            listGet.Add((Order)item);
        }
        return listGet;
    }


    public IEnumerable<Order> GetAllDeleted()
    {
        List<Order> listGet = new List<Order>();
        foreach (Order? item in dataSource.Orders)
        {
            if (item != null && item?.IsDeleted == true)
                listGet.Add((Order)item); }
        return listGet;
    }
}
