using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BO;
using BlApi;

//מחלקה לייצוג פריט בהזמנה
public class OrderItem
{
    public bool IsDeleted { get; set; }     //האם המוצר בהזמנה מחוק
    public int ID { get; set; }     //מספר מזהה של המוצר-בהזמנה
    public int? ProductID { get; set; }      //המספר המזהה של המוצר 
    public double? Price { get; set; }       //מחיר המוצר
    public int? Amount { get; set; }       //כמות המוצר בהזמנה
    public string? Path { get; set; } //כתובת תמונה של המוצר
    public override string ToString() => this.ToStringProperty();       //הפיכה למחרוזת
}
