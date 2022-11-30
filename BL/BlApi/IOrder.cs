using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;

public interface IOrder
{
    public List <BO.OrderForList> GetOrders ();
    public BO.Order GetOrderById (int id);
    public BO.Order UpdateStatusToShipped  (int id);
    public BO.Order UpdateStatusToProvided(int id);
    public BO.OrderTracking FollowOrder (int id);
    public BO.Order UpdateAmountOfProduct (int orderId,int productId, int amount);
    
}
