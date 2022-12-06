using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BO;
using BlApi;
//מחלקה של ישות עבור מעקב הזמנה
public class OrderTracking
{
    public int ID { get; set; }  //מספר מזהה של ההזמנה
    public OrderStatus? Status { get; set; }  //סטטוס ההזמנה- באיזה שלב היא כרגע נמצאת
    public List<Tuple<DateTime?,string>?>? Tracking { get; set; }  //השלבים שעברה ההזמנה - צמדים של אירוע והתאריך בו היה
    public override string ToString() => this.ToStringProperty();   //הפיכה למחרוזת
}
