namespace DO;

/// Structure for an item on order.
public struct OrderItem
{
    public bool IsDeleted { get; set; }  //שדה המציין האם הפריט-בהזמנה תקף או מחוק
    public int ID { get; set; }  //שדה המציין את המספר המזהה של הפריט-בהזמנה
    public int? OrderID { get; set; }  //המספר המזהה של ההזמנה אליה שייך הפריט
    public int? ProductID { get; set; }  // המספר המזהה של סוג המוצר המוזמן
    public double? Price { get; set; }  //מחיר המוצר
    public int? Amount { get; set; }  //כמות מהמוצר בהזמנה

    //תאור פריט כמחרוזת
 public override string ToString() => $@"
	OrderItem ID: {ID}, 
	ProductID: {ProductID}
    	Price: {Price}
    	Amount: {Amount}
	";
}
