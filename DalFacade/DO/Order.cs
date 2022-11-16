namespace DO;

/// Structure for representing  an order of a customer.
public struct Order
{
    public bool IsDeleted { get; set; } 
    //
    public int ID { get; set; }
    //
    public string? CustomerName { get; set; }
    //
    public string? CustomerEmail { get; set; }
    //
    public string? CustomerAdress { get; set; }
    //
    public DateTime? OrderDate { get; set; }
    //
    //public OrderStatus? Status { get; set; }
    //
    //public DateTime? PaymentDate { get; set; }
    //
    public DateTime? shipDate { get; set; }
    //
    public DateTime? DeliveryrDate { get; set; }
    //
    // public List<OrderItem>? Items { get; set; }
    //
    //public double CalculateTotalPrice() { }
    //public double? TotalPrice { get; set; }

    public override string ToString() => $@"
	Order ID={ID}: {ID}, 
	CustomerName- {CustomerName}
    	CustomerEmail: {CustomerEmail}
    	CustomerAdress: {CustomerAdress}
    	OrderDate: {OrderDate}
    	shipDate: {shipDate}
    	DeliveryrDate: {DeliveryrDate}

	";
}
