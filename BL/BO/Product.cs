using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BO;

//מחלקה עבור ייצוג מוצר
public class Product
{
    public bool IsDeleted { get; set; }  //האם המוצר מחוק
    public int ID { get; set; }  // מספר מזהה של המוצר
    public string? Name { get; set; }  //שם המוצר
    public double? Price { get; set; }  //מחיר המוצר
    public Category? Category { get; set; }  //הקטגוריה אליה שייך המוצר
    public int? InStock { get; set; }  //כמה פריטים של המוצר יש במלאי
    
    public string? path = null;  //כתובת תמונה של המוצר
    public override string ToString() => this.ToStringProperty();  //הפיכה למחרוזת
} 
