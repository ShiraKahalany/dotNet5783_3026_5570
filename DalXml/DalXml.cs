using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using System.Diagnostics;
using DalApi;
namespace Dal;

public struct ImportentNumbers
{
    public double numberSaved { get; set; }
    public string typeOfnumber { get; set; }
}
sealed  internal class DalXml:IDal
{
    public static IDal Instance { get; } = new DalXml();
    private DalXml() { }
    public IOrder Order { get; } = new Dal.Order();
    public IOrderItem OrderItem { get; } = new Dal.OrderItem();
    public IProduct Product { get; } = new Dal.Product();
    

}
