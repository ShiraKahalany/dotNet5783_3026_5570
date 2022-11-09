namespace DO;

/// Structure for an item on order.
public struct OrderItem
{
    //
    public int ID { get; set; }
    //
    public int? ProductID { get; set; }
    //
    public double? Price { get; set; }
   //
    public int? Amount { get; set; }
    public override string ToString() => $@"
	OrderItem ID={ID}: {ID}, 
	ProductID - {ProductID}
    	Price: {Price}
    	Amount: {Amount}
	";
}
