using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal;
using BlApi;
namespace BlImplementation;
using BO;
internal class Order:IOrder
{
    private IDal dal = new DalList(); 
}
