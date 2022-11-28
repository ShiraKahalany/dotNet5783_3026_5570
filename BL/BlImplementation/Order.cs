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
    DalApi.IDal dal = DalApi.DalFactory.GetDal() ?? throw new NullReferenceException("Missing Dal");
    public List<BO.OrderForList> GetOrders()
    {
        IEnumerable<DO.Order> listor = dal.Order.GetAll();
        if (!listor.Any())
            throw new NoItemsException();
        List<BO.OrderForList> list = new List<BO.OrderForList>();
        foreach (DO.Order order in listor)
        {
            try
            {
                OrderForList or = new OrderForList();
                list.Add(order.CopyFields(or));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        return listproducts;
    }
    public BO.Order GetOrderById(int id);
    public BO.Order UpdateStatusToShipped(int id);
    public BO.Order UpdateStatusToProvided(int id);
    public BO.OrderTracking FollowOrder(int id);
    public void UpdateAmountOfProduct(int productId, int amount);

}
