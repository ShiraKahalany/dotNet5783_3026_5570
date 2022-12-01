using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Dal;
using BlApi;
namespace BlImplementation;
using BO;
internal class Order : IOrder
{
    DalApi.IDal dal = DalApi.DalFactory.GetDal() ?? throw new NullReferenceException("Missing Dal");
    public List<BO.OrderForList> GetOrders()
    //מחזירה את רשימת ההזמנות
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
                if (order.DeliveryDate <= DateTime.Now)
                    or.Status = OrderStatus.Delivered;
                else
                {
                    if (order.ShipDate <= DateTime.Now)
                        or.Status = OrderStatus.Shipped;
                    else
                    {
                        if (order.OrderDate <= DateTime.Now)
                            or.Status = OrderStatus.Ordered;
                        else
                            or.Status = OrderStatus.None;
                    }
                }
                IEnumerable<DO.OrderItem> items = dal.OrderItem.GetAll(order.ID);

                list.Add(order.CopyFields(or));
                int? counter = 0;
                double? sum = 0;
                foreach (DO.OrderItem item in items) { counter += item.Amount; sum += item.Price * item.Amount; }
                or.TotalPrice = sum;
                or.AmountOfItems = counter;
                list.Add(or);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        return list;
    }
    public BO.Order GetOrderById(int id)
    {
        if (id <= 0)
            throw new OrderNotExistException();
        try
        {
            DO.Order order = dal.Order.GetByID(id);
            BO.Order or = new BO.Order();
            or = order.CopyFields(or);
            if (order.DeliveryDate < DateTime.Now)
                or.Status = OrderStatus.Delivered;
            else
            {
                if (order.ShipDate < DateTime.Now)
                    or.Status = OrderStatus.Shipped;
                else
                {
                    if (order.OrderDate < DateTime.Now)
                        or.Status = OrderStatus.Ordered;
                    else
                        or.Status = OrderStatus.None;
                }
            }
            
            IEnumerable<DO.OrderItem> items = dal.OrderItem.GetAll(id);
            List<BO.OrderItem> list = new List<BO.OrderItem>();

            double? total = 0;
            foreach (DO.OrderItem item in items)
            {
                total += item.Amount * item.Price;
                BO.OrderItem temp = new BO.OrderItem();
                list.Add(item.CopyFields(temp));
            }
            or.Items = list;
            or.TotalPrice = total;
            return or;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public BO.Order UpdateStatusToShipped(int id)
    {
        try
        {
            DO.Order order = dal.Order.GetByID(id);
            if (order.ShipDate < DateTime.Now)
                throw new OrderHasShippedException();
            order.ShipDate =DateTime.Now;
            dal.Order.Update(order);
            BO.Order or = new BO.Order();
            or = order.CopyFields(or);
            or.Status =BO.OrderStatus.Shipped;
            
            IEnumerable<DO.OrderItem> items = dal.OrderItem.GetAll(id);
            List<BO.OrderItem> list = new List<BO.OrderItem>();

            double? total = 0;
            foreach (DO.OrderItem item in items)
            {
                total += item.Amount * item.Price;
                BO.OrderItem temp = new BO.OrderItem();
                list.Add(item.CopyFields(temp));
            }
            or.Items = list;
            or.TotalPrice = total;
            return or;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public BO.Order UpdateStatusToProvided(int id)
    {
        try
        {
            DO.Order order = dal.Order.GetByID(id);
            if (order.DeliveryDate < DateTime.Now)
                throw new OrderHasDeliveredException();
            if (order.ShipDate > DateTime.Now)
                throw new OrderHasNotShippedException();
            order.DeliveryDate = DateTime.Now;
            dal.Order.Update(order);
            BO.Order or = new BO.Order();
            or = order.CopyFields(or);
            or.Status = BO.OrderStatus.Delivered;
            
            IEnumerable<DO.OrderItem> items = dal.OrderItem.GetAll(id);
            List<BO.OrderItem> list = new List<BO.OrderItem>();

            double? total = 0;
            foreach (DO.OrderItem item in items)
            {
                total += item.Amount * item.Price;
                BO.OrderItem temp = new BO.OrderItem();
                list.Add(item.CopyFields(temp));
            }
            or.Items = list;
            or.TotalPrice = total;
            return or;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public BO.OrderTracking FollowOrder(int id)
    {
        try
        {
            DO.Order order = dal.Order.GetByID(id);
            BO.OrderTracking orderTracking = new BO.OrderTracking();
            orderTracking.ID = order.ID;
            List<Tuple<DateTime?,string>>? tuples = new List<Tuple<DateTime?,string>>();
            if (order.OrderDate <= DateTime.Now)
            {
                orderTracking.Status = OrderStatus.Ordered;
                tuples.Add(Tuple.Create(order.OrderDate, "The order was successfully received"));
            }
            if (order.ShipDate <= DateTime.Now)
            {
                orderTracking.Status = OrderStatus.Shipped;
                tuples.Add(Tuple.Create(order.ShipDate, "The order was shipped"));
            }
            if (order.DeliveryDate <= DateTime.Now)
            {
                orderTracking.Status = OrderStatus.Delivered;
                tuples.Add(Tuple.Create(order.DeliveryDate, "The order was delivered"));
            }
            orderTracking.Tracking = tuples;
            return orderTracking;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public BO.Order UpdateAmountOfProduct(int orderIId ,int productId, int amount)
    {
        try
        {
            DO.Order order =dal.Order.GetByID(orderIId);
            if (order.ShipDate< DateTime.Now)
                throw new CanNotUpdateOrderException();
            DO.Product product = dal.Product.GetByID(productId);
            DO.OrderItem? myItem = dal.OrderItem.GetByOrderAndId(orderIId, productId);
            dal.OrderItem.Update(new DO.OrderItem
            {
                ID = myItem.GetValueOrDefault().ID,
                OrderID = orderIId,
                ProductID = productId,
                Price = myItem.GetValueOrDefault().Price,
                Amount = amount,
                IsDeleted = false
            });

            BO.Order or = new BO.Order();
            or = order.CopyFields(or);
            or.Status = BO.OrderStatus.Delivered;
            IEnumerable<DO.OrderItem> items = dal.OrderItem.GetAll(orderIId);
            List<BO.OrderItem> list = new List<BO.OrderItem>();

            foreach (BO.OrderItem item in or.Items)
            {
                if (item.ProductID == productId)
                {
                    if (amount == 0)
                    {
                        or.Items.Remove(item);
                        or.TotalPrice -= item.Price * item.Amount;
                        return or;
                    }
                    int? difference = amount - item.Amount;
                    if (item.Amount < amount)
                    {
                        if (!(product.InStock >= difference))
                            throw new NotInStockException();
                        item.Amount = amount;
                        or.TotalPrice += item.Price * difference;
                        return or;
                    }
                    if (item.Amount > amount)
                    {
                        item.Amount = amount;
                        or.TotalPrice += item.Price * difference;
                        return or;
                    }
                }
            }
            return or;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

}
