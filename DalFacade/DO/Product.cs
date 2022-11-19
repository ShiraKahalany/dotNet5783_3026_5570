namespace DO;

/// Structure for a product in the store.
public struct Product
{
    public bool IsDeleted { get; set; }  //שדה המציין האם המוצר תקף או מחוק
    public int ID { get; set; }   //שזה המציין את המספר המזהה של המוצר בחנות
    public string? Name { get; set; }  //שם המוצר
    public double? Price { get; set; }  //מחיר המוצר
    public Category? Category { get; set; }  //לאיזה קטגוריה המוצר משתייך
    public int? InStock { get; set; }  //כמה פריטים מהמוצר יש במלאי

    //תיאור מוצר כמחרוזת
    public override string ToString() => $@"
	Product ID: {ID}, 
	category: {Category}
    	Price: {Price}
    	Amount in stock: {InStock}
	";

}
