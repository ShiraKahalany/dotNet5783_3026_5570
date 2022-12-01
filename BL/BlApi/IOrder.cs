using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;

//ממשק עבור הזמנות
public interface IOrder
{
    public List <BO.OrderForList?>? GetOrders ();  //קבלת רשימת ההזמנות התקפות
    public BO.Order? GetOrderById (int id);  //קבלת הזמנה ספציפית לפי מספר מזהה

    public BO.Order? GetDeletedOrderById(int id);  //קבלת הזמנה מחוקה לפי מספר מזהה
    public List<BO.OrderForList?>? GetDeletedOrders();  //קבלת רשימת כל ההזמנות המחוקות

    public List<BO.OrderForList?>? GetOrdersWithDeleted();  //קבלת הרשימה המלאה של כל ההזמנות, גם המחוקות וגם התקפות
    public void Restore (int id);  //שיחזור הזמנה מחוקה
    //public BO.Order Restore(int id);   //

    public BO.Order? UpdateStatusToShipped  (int id);  //עידכון הזמנה ל "שולחה"
    public BO.Order? UpdateStatusToProvided(int id);  //עידכון הזמנה ל "סופקה"
    public BO.OrderTracking? FollowOrder (int id);  //מעקב הזמנה, הצגת השלבים של ההזמנה והתאריכים
    public BO.Order? UpdateAmountOfProduct (int orderId,int productId, int amount);   //עידכון כמות של מוצר בהזמנה
    
}
