using DalApi;
using DO;
using System.Security.Cryptography.X509Certificates;

namespace Dal;
//מימוש ממשק המוצרים-בהזמנה
public class DalOrderItem : IOrderItem
{
    DataSource dataSource = DataSource.s_instance;

    #region Add
    public int Add(OrderItem item)
    //מתודת הוספת מוצר בהזמנה
    {

        if (item.ID >= 100000 && dataSource.OrderItems.Find(x => x?.ID == item.ID) == null)
        {
            dataSource.OrderItems.Add(item);
            return item.ID;
        }
        item.ID = DataSource.Config.NextOrderItemNumber;
        dataSource.OrderItems.Add(item);
        return item.ID;
    }
    #endregion

    #region Update
    public void Update(OrderItem item)
    //מתודת עידכון. מקבלת עצם חדש, ומעדכנת את העצם עם הת"ז הזה להיות העצם המעודכן
    {
        OrderItem? temp = dataSource.OrderItems.Find(x => x?.ID == item.ID);
        if (temp == null) //if it is not exist throw exception
            throw new DO.NotExistException("The item is not exist");
        DeletePermanently(item.ID);
        Add(item);
    }
    #endregion

    #region Restore
    public void Restore(OrderItem item)
    //מתודה המעדכנת את הזמנה להזמנה המעודכנת שהתקבלה (שיש לה אותו ת"ז)ו
    {
        OrderItem? temp = dataSource.OrderItems.Find(x => x?.ID == item.ID);
        if (temp == null) //if it is not exist throw exception
            throw new DO.NotExistException("The item is not exist");
        if (temp?.IsDeleted == false)
            throw new DO.NotExistException("The item is not deleted");
        DeletePermanently(item.ID);
        item.IsDeleted = false;
        Add(item);
    }
    #endregion

    #region Delete Permanently
    public void DeletePermanently(int id)
    {
        OrderItem? temp = dataSource.OrderItems.Find(x => x?.ID == id); //check if the element exist in the orders list
        if (temp == null) //if it is not exist throw exception
            throw new DO.NotExistException("The order is not exist");
        dataSource.OrderItems.Remove(temp);
    }
    #endregion

    #region Delete
    public void Delete(int id)
    //מתודה למחיקת המוצר-בהזמנה בעל ה ת"ז שהתקבלה
    {
        OrderItem? temp = dataSource.OrderItems.Find(x => x?.ID == id); //check if the element exist in the orders list
        if (temp == null) //if it is not exist throw exception
            throw new DO.NotExistException("The order is not exist");
        if (temp?.IsDeleted == true)
            throw new DO.NotExistException("The order is already deleted");
        dataSource.OrderItems.Remove(temp);
        OrderItem orderItem = new OrderItem
        {
            IsDeleted = true,
            ID = temp.GetValueOrDefault().ID,
            Amount = temp?.Amount,
            OrderID = temp?.OrderID,
            Price = temp?.Price,
            ProductID = temp?.ProductID,
            TotalItem =(temp?.Price)*(temp?.Amount),
            Name = temp?.Name,
            Path = temp?.Path
        };
        Add(orderItem);
    }
    #endregion

    #region Get All The OrderItems By Filter
    public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? filter = null)
        //מתודה לקבלת רשימת כל המוצרי הזמנה שעונים על התנאי המבוקש
    {
        if (filter == null)
            return dataSource.OrderItems;
        var ieorderitems = from orderitem in dataSource.OrderItems
                           where filter(orderitem) == true
                           select orderitem;
        return ieorderitems;
    }
    #endregion

    #region Get OrderItem By Filter
    public OrderItem? GetTByFilter(Func<OrderItem?, bool> filter)
        //מתודה לקבלת פריט-הזמנה לפי פילטר
    {
        DO.OrderItem? item = dataSource.OrderItems.Find((DO.OrderItem? orderitem) => filter(orderitem));
        if (item == null)
            throw new DO.NotExistException("Not Exist - OrderItem");
        return item;

    }
    #endregion
}
