namespace DO;

/// Structure for a product in the store.
public struct Product
{
    public bool? IsDeleted;
    //
    public int ID { get; set; } 
    //
    public string? Name { get; set; }
    //
    public double? Price { get; set; }
    //
    public Category? Category { get; set; }
    //
    public int? InStock { get; set; }
    public override string ToString() => $@"
	Product ID={ID}: {Name}, 
	category - {Category}
    	Price: {Price}
    	Amount in stock: {InStock}
	";

}
