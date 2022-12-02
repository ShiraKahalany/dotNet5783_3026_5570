using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;
//מחלקה עבור מסך רשימת הזמנות - מייצגת הזמנה ברשימת ההזמנות
public class OrderForList
{
    public int ID { get; set; }  //מספר מזהה הזמנה
    public string? CustomerName { get; set; }  //שם הלקוח
    public OrderStatus? Status { get; set; }  //סטטוס ההזמנה - באיזה שלב היא נמצאת
    public int? AmountOfItems { get; set; }  //כמות מוצרים בהזמנה
    public double? TotalPrice { get; set; }  //מחיר כולל של ההזמנה
    public override string ToString() => this.ToStringProperty();  //הפיכה למחרוזת
}
