using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BlApi;
namespace BlImplementation;


//מימוש ממשק ההזמנות
internal class Order : IOrder
{
    DalApi.IDal dal = DalApi.DalFactory.GetDal() ?? throw new NullReferenceException("Missing Dal");  //מופע הנתונים
    public List<BO.OrderForList?>? GetOrders()
    //מתודה לקבלת רשימת כל ההזמנות התקפות
    {
        IEnumerable<DO.Order?> listor = dal.Order.GetAll((DO.Order? order) => order.GetValueOrDefault().IsDeleted == false);
        if (!listor.Any())
            throw new BO.NoItemsException();
        try
        {
            var x = from DO.Order order in listor
                    select order.OrderToOrderForList();
            return x.ToList();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public BO.Order GetOrderById(int id)
    //קבלת הזמנה לפי מספר מזהה
    {
        if (id < 1000)
            throw new BO.IllegalIdException();
        try
        {
            DO.Order order = dal.Order.GetByID(id);
            return order.OrderToBO();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public BO.Order UpdateStatusToShipped(int id)
    //עידכון הזמנה ששולחה
    {
        try
        {
            DO.Order order = dal.Order.GetByID(id);
            if (order.ShipDate != null && order.ShipDate < DateTime.Now)
                throw new BO.OrderHasShippedException();
            order.ShipDate = DateTime.Now;
            dal.Order.Update(order);
            BO.Order or = new BO.Order();
            or = order.CopyFields(or);
            or.Status = BO.OrderStatus.Shipped;

            IEnumerable<DO.OrderItem> items = dal.OrderItem.GetAll(id);
            List<BO.OrderItem> list = new List<BO.OrderItem>();

            double? sum = 0;
            foreach (DO.OrderItem item in items)
            {
                sum += item.Amount * item.Price;
                BO.OrderItem temp = new BO.OrderItem();
                list.Add(item.CopyFields(temp));
            }
            or.Items = list;
            or.TotalPrice = Math.Round(sum ?? 0, 2);
            return or;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public BO.Order UpdateStatusToProvided(int id)
    //עידכון הזמנה שסופקה
    {
        try
        {
            DO.Order order = dal.Order.GetByID(id);
            if (order.DeliveryDate != null && order.DeliveryDate < DateTime.Now)
                throw new BO.OrderHasDeliveredException();
            if (order.ShipDate == null /*|| order.ShipDate > DateTime.Now*/)
                throw new BO.OrderHasNotShippedException();
            order.DeliveryDate = DateTime.Now;
            dal.Order.Update(order);
            BO.Order or = new BO.Order();
            or = (order.CopyFields(or));
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
            or.TotalPrice = Math.Round(total ?? 0, 2);
            return or;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public BO.OrderTracking FollowOrder(int id)
    //מעקב הזמנה - הצגת האירועים של ההזמנה והתאריכים שלהם
    {
        try
        {
            DO.Order order = dal.Order.GetByID(id);
            BO.OrderTracking orderTracking = new BO.OrderTracking();
            orderTracking.ID = order.ID;
            List<Tuple<DateTime?, string>?>? tuples = new List<Tuple<DateTime?, string>?>();
            if (order.OrderDate != null && order.OrderDate <= DateTime.Now)
            {
                orderTracking.Status = BO.OrderStatus.Ordered;
                tuples.Add(Tuple.Create(order.OrderDate, "The order was successfully received"));
            }
            if (order.ShipDate != null && order.ShipDate <= DateTime.Now)
            {
                orderTracking.Status = BO.OrderStatus.Shipped;
                tuples.Add(Tuple.Create(order.ShipDate, "The order was shipped"));
            }
            if (order.DeliveryDate != null && order.DeliveryDate <= DateTime.Now)
            {
                orderTracking.Status = BO.OrderStatus.Delivered;
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
    public BO.Order UpdateAmountOfProduct(int orderId, int productId, int amount)
    //עידכון כמות מוצר בהזמנה
    {
        try
        {
            DO.Order order = dal.Order.GetByID(orderId);
            if (order.ShipDate != null && order.ShipDate < DateTime.Now)
                throw new BO.CanNotUpdateOrderException();
            DO.Product product = dal.Product.GetByID(productId);
            BO.Order border = new BO.Order();
            border = order.CopyFields(border);
            DO.OrderItem? theItem = dal.OrderItem.GetByOrderAndId(orderId, productId);
            border.Status = BO.OrderStatus.Ordered;
            IEnumerable<DO.OrderItem>? items = dal.OrderItem.GetAll(orderId);
            List<BO.OrderItem?> list = new List<BO.OrderItem?>();
            if (items == null || !items.Any())
                return border;
            int difference = 0;
            foreach (DO.OrderItem it in items)
            {
                if (productId == it.ProductID)
                {
                    if (amount == 0)
                        break;
                    difference = amount - it.Amount ?? 0;
                    if (difference > 0)
                        if (product.InStock < amount)
                            throw new BO.NotInStockException();
                    BO.OrderItem temp = new BO.OrderItem();
                    temp = it.CopyFields(temp);
                    temp.Amount = amount;
                    list.Add(temp);
                }
                else
                    list.Add(it.CopyFields(new BO.OrderItem()));
            }

            border.TotalPrice += product.Price * difference;
            border.TotalPrice = Math.Round(border.TotalPrice ?? 0, 2);
            border.Items = list;
            if (amount == 0)
            {
                dal.OrderItem.Delete(theItem?.ID ?? 0);
                return border;
            }
            dal.OrderItem.Update(new DO.OrderItem
            {
                ID = theItem?.ID ?? 0,
                OrderID = orderId,
                ProductID = productId,
                Price = theItem?.Price ?? 0,
                Amount = amount,
                IsDeleted = false
            });
            return border;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public BO.Order GetDeletedOrderById(int id)
    //קבלת הזמנה שנמחקה, לפי מזהה הזמנה
    {

        if (id <= 1000)
            throw new BO.IllegalIdException();
        try
        {
            DO.Order order = dal.Order.GetDeletedById(id);
            BO.Order or = new BO.Order();
            or = (order.CopyFields(or));
            or.Status = order.GetStatus();
            IEnumerable<DO.OrderItem> items = dal.OrderItem.GetAll(id);
            if (items == null || !items.Any())
                throw new BO.NotItemsInCartException();
            List<BO.OrderItem> list = new List<BO.OrderItem>();

            double? sum = 0;
            foreach (DO.OrderItem? item in items)
            {
                if (item == null)
                    break;
                sum += item?.Amount * item?.Price;
                BO.OrderItem temp = new BO.OrderItem();
                list.Add(item.CopyFields(temp));
            }
            or.Items = list;
            or.TotalPrice = Math.Round(sum ?? 0, 2);
            return or;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public List<BO.OrderForList?>? GetDeletedOrders()
    //קבלת רשימת כל ההזמנות המחוקות
    {
        IEnumerable<DO.Order> listor = dal.Order.GetAllDeleted();
        if (!listor.Any())
            throw new BO.NoItemsException();
        List<BO.OrderForList> list = new List<BO.OrderForList>();
        if (!listor.Any())
            return null;
        foreach (DO.Order order in listor)
        {
            try
            {
                BO.OrderForList or = new BO.OrderForList();
                or.Status = order.GetStatus();
                IEnumerable<DO.OrderItem> items = dal.OrderItem.GetAll(order.ID);
                if (!items.Any())
                    throw new BO.NotItemsInCartException();
                or = order.CopyFields(or);
                int? counter = 0;
                double? sum = 0;
                foreach (DO.OrderItem item in items)
                {
                    counter += item.Amount;
                    sum += item.Price * item.Amount;
                }
                or.TotalPrice = Math.Round(sum ?? 0, 2);
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

    public List<BO.OrderForList?> GetOrdersWithDeleted()
    //קבלת רשימת כל ההזמנות - כולל אלו שנמחקו
    {
        IEnumerable<DO.Order> listor = dal.Order.GetAllWithDeleted();
        if (!listor.Any())
            throw new BO.NoItemsException();
        List<BO.OrderForList> list = new List<BO.OrderForList>();
        foreach (DO.Order order in listor)
        {
            try
            {
                BO.OrderForList or = new BO.OrderForList();
                or.Status = order.GetStatus();
                IEnumerable<DO.OrderItem> items = dal.OrderItem.GetAll(order.ID);

                or = order.CopyFields(or);
                int? counter = 0;
                double? sum = 0;
                foreach (DO.OrderItem item in items) { counter += item.Amount; sum += item.Price * item.Amount; }
                or.TotalPrice = Math.Round(sum ?? 0, 2);
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

    public void Restore(int id)
    //שיחזור הזמנה שנמחקה, לפי מזהה הזמנה
    {
        if (id <= 0)
            throw new BO.NotExistException();
        try
        {
            DO.Order o = dal.Order.GetDeletedById(id);
            dal.Order.Restore(o);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public void CancelOrder(int id)
    //מתודה לביטול הזמנה
    {
        DO.Order order = dal.Order.GetByID(id);
        if (order.ShipDate != null && order.ShipDate < DateTime.Now)
            throw new BO.CanNotUpdateOrderException();
        TimeSpan twentyfourhours = new TimeSpan(24, 00, 00);
        if ((order.OrderDate != null) && (DateTime.Now - order.OrderDate) < twentyfourhours)
            dal.Order.Delete(id);
        else
            throw new BO.CantCancelOrderException();
    }
}


