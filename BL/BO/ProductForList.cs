using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class ProductForList
{
    public int ID { get; set; }
    public string? Name { get; set; }
    public double? Price { get; set; }
    public Category? category { get; set; }
    public override string ToString() => this.ToStringProperty();
}
