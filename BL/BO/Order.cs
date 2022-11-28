using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class Order
{
    public bool IsDeleted { get; set; }
    //
    public int ID { get; set; }
    //
    public string? CustomerName { get; set; }
    //
    public string? CustomerEmail { get; set; }
    //
    public string? CustomerAddress { get; set; }
    //
    public DateTime? OrderDate { get; set; }
    //
    public OrderStatus? Status { get; set; }
    //
    public DateTime? PaymentDate { get; set; }
    //
    public DateTime? shipDate { get; set; }
    //
    public DateTime? DeliveryrDate { get; set; }
    //
    public List<OrderItem>? Items { get; set; }
    //
    public double? TotalPrice { get; set; }

    public override string ToString() => this.ToStringProperty();
}
