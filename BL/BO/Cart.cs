using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

//סל קניות של לקוח
public class Cart
{
    public bool IsDeleted { get; set; }   //האם העגלה מחוקה
    public string? CustomerName { get; set; }   //שם הלקוח
    public string? CustomerEmail { get; set; }   //כתובת אימייל של הלקוח
    public string? CustomerAddress { get; set; }    //כתובת מגורים של הלקוח
    public List<BO.OrderItem?>? Items { get; set; }   //רשימת המוצרים שיש בעגלה
    public double? TotalPrice { get; set; }   //מחיר כולל
    public override string ToString() => this.ToStringProperty();   //הפיכה למחרוזת
}
