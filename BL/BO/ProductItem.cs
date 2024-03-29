﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BO;
using BlApi;
//מחלקה לייצוג מוצר בקטלוג
public class ProductItem
{
    public bool IsDeleted { get; set; }  //האם המוצר מחוק
    public int ID { get; set; }  //מספר מזהה של המוצר
    public string? Name { get; set; }  //שם המוצר
    public double? Price { get; set; }  //מחיר המוצר
    public Category Category { get; set; }  //הקטגוריה של המוצר
    public int? Amount { get; set; }  //כמה יש מהמוצר בסל
    public bool? IsInStock { get; set; }  //האם המוצר קיים במלאי
    public string? Path { get; set; }
    public override string ToString() => this.ToStringProperty();  //הפיכה למחרוזת
}
