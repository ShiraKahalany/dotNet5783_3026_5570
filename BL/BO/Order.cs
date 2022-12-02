using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;
//מחלקה עבור ייצוג הזמנה
public class Order
{
    public bool IsDeleted { get; set; }      //האם ההזמנה מחוקה
    public int ID { get; set; }      //מספר מזהה של ההזמנה
    public string? CustomerName { get; set; }      //שם הלקוח
    public string? CustomerEmail { get; set; }      //כתובת אימייל של הלקוח
    public string? CustomerAddress { get; set; }      //כתובת המגורים של הלקוח
    public DateTime? OrderDate { get; set; }     //תאריך יצירת ההזמנה
    public OrderStatus? Status { get; set; }      //סטטוס ההזמנה - באיזה שלב היא כרגע
    public DateTime? ShipDate { get; set; }  //תאריך יצירת ההזמנה
    public DateTime? DeliveryDate { get; set; }      //תאריך מסירת ההזמנה ללקוח
    public List<OrderItem>? Items { get; set; }     //רשימת המוצרים שכוללת ההזמנה
    public double? TotalPrice { get; set; }      //המחיר הכולל של ההזמנה
    public override string ToString() => this.ToStringProperty();      //הפיכה למחרוזת לצורך הדפסה
}
