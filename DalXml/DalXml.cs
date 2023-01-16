using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using System.Diagnostics;
using DalApi;
namespace Dal;

sealed  internal class DalXml:IDal
{
    public static IDal Instance { get; } = new DalXml();
    private DalXml() { }
    public IOrder order { get; } = new Dal.Order();
    public IOrderItem orderItem { get; } = new Dal.OrderItem();
    public IProduct product { get; } = new Dal.Product();
}
