using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class OrderItem
{
    public bool IsDeleted { get; set; }
    //
    public int ID { get; set; }
    //
    public int? ProductID { get; set; }
    //
    public double? Price { get; set; }
    //
    public int? Amount { get; set; }

    public override string ToString() => this.ToStringProperty();
}
