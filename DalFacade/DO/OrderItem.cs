﻿namespace DO;

/// Structure for an item on order.
public struct OrderItem
{
    public bool IsDeleted { get; set; }  //שדה המציין האם הפריט-בהזמנה תקף או מחוק
    public int ID { get; set; }  //שדה המציין את המספר המזהה של הפריט-בהזמנה
    public string? Name { get; set; }  //שם המוצר
    public int? OrderID { get; set; }  //המספר המזהה של ההזמנה אליה שייך הפריט
    public int? ProductID { get; set; }  // המספר המזהה של סוג המוצר המוזמן
    public double? Price { get; set; }  //מחיר המוצר
    public int? Amount { get; set; }  //כמות מהמוצר בהזמנה
    public string? Path { get; set; } //כתובת תמונה של המוצר
    public double? TotalItem { get; set; }       //מחיר כולל של מוצר בהזמנה

    //תאור פריט כמחרוזת
    public override string ToString() { return Tools.ToStringProperty(this); }

}
