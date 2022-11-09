namespace DO;
namespace Dal;

internal class DataSource 
{
    internal static DataSource s_instance { get; } = new DataSource();
    private DataSource()
        initialize();
    public readonly int RandomNum() { }
    internal List<Order>? Orders;
    internal List<Product>? Products;
    internal List<OrderItem>? OrderItems;
}
