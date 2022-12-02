using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BO;

//מחלקה שמייצגת מוצר ברשימת המוצרים
public class ProductForList
{
    public bool IsDeleted { get; set; } //האם המוצר מחוק
    public int ID { get; set; }  //מספר מזהה של המוצר
    public string? Name { get; set; }  //שם המוצר
    public double? Price { get; set; }  //מחיר המוצר
    public Category? Category { get; set; }  //הקטגוריה אליה שייך המוצר
    public override string ToString() => this.ToStringProperty();  //הפיכה למחרוזת
}
