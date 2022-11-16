using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
namespace Dal;

sealed internal class DalList:IDal
{
    public IOrder order => new DalOrder() ;
    public IProduct product => new DalProduct() ;
    public IOrderItem orderItem => new DalOrderItem() ;
}
