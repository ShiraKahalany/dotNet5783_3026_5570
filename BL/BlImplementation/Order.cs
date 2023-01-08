
using BlApi;
using System.Security.Cryptography.X509Certificates;

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
            DO.Order? order = dal.Order.GetTByFilter((DO.Order? order) => (order.GetValueOrDefault().ID == id) && order.GetValueOrDefault().IsDeleted == false);
            return order?.OrderToBO();
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
            DO.Order order = (DO.Order)dal.Order.GetTByFilter((DO.Order? order) => (order.GetValueOrDefault().ID == id) && order.GetValueOrDefault().IsDeleted == false);
            if (order.ShipDate != null && order.ShipDate < DateTime.Now)
                throw new BO.OrderHasShippedException();
            order.ShipDate = DateTime.Now;
            dal.Order.Update((DO.Order)order);

            return order.OrderToBO();
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
            DO.Order order = (DO.Order)dal.Order.GetTByFilter((DO.Order? order) => (order.GetValueOrDefault().ID == id) && order.GetValueOrDefault().IsDeleted == false);
            if (order.DeliveryDate != null && order.DeliveryDate < DateTime.Now)
                throw new BO.OrderHasDeliveredException();
            if (order.ShipDate == null)
                throw new BO.OrderHasNotShippedException();
            order.DeliveryDate = DateTime.Now;
            dal.Order.Update((DO.Order)order);
            return order.OrderToBO();
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
            DO.Order? order = dal.Order.GetTByFilter((DO.Order? order) => (order.GetValueOrDefault().ID == id) && order.GetValueOrDefault().IsDeleted == false);
            BO.OrderTracking orderTracking = new BO.OrderTracking();
            orderTracking.ID = order.GetValueOrDefault().ID;
            List<Tuple<DateTime?, string>?>? tuples = new List<Tuple<DateTime?, string>?>();
            if (order?.OrderDate != null && order?.OrderDate <= DateTime.Now)
            {
                orderTracking.Status = BO.OrderStatus.Ordered;
                tuples.Add(Tuple.Create(order?.OrderDate, "The order was successfully received"));
            }
            if (order?.ShipDate != null && order?.ShipDate <= DateTime.Now)
            {
                orderTracking.Status = BO.OrderStatus.Shipped;
                tuples.Add(Tuple.Create(order?.ShipDate, "The order was shipped"));
            }
            if (order?.DeliveryDate != null && order?.DeliveryDate <= DateTime.Now)
            {
                orderTracking.Status = BO.OrderStatus.Delivered;
                tuples.Add(Tuple.Create(order?.DeliveryDate, "The order was delivered"));
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
            DO.Order? order = dal.Order.GetTByFilter((DO.Order? order) => (order.GetValueOrDefault().ID == orderId) && order.GetValueOrDefault().IsDeleted == false);
            if (order?.ShipDate != null && order?.ShipDate < DateTime.Now)
                throw new BO.CanNotUpdateOrderException();
            DO.Product? product = dal.Product.GetTByFilter((DO.Product? product) => (product.GetValueOrDefault().ID == productId) && product.GetValueOrDefault().IsDeleted == false);
            BO.Order border = new BO.Order();
            border = order.CopyFields(border);
            DO.OrderItem? theItem = dal.OrderItem.GetTByFilter((DO.OrderItem? orderItem) => orderItem.GetValueOrDefault().OrderID == orderId && orderItem.GetValueOrDefault().IsDeleted == false && orderItem.GetValueOrDefault().ProductID == productId);
            border.Status = BO.OrderStatus.Ordered;     /////??????????
            IEnumerable<DO.OrderItem?>? items = dal.OrderItem.GetAll((DO.OrderItem? orderItem) => orderItem.GetValueOrDefault().OrderID == order?.ID && orderItem.GetValueOrDefault().IsDeleted == false);
            if (items == null || !items.Any())
                return border;
            int difference = 0;

            var x = from DO.OrderItem it in items
                    let temp= Tools.checkAmount(it, productId, amount, ref difference, product)
                    where temp != null
                    select temp;


            //foreach (DO.OrderItem it in items)
            //{
            //    if (productId == it.ProductID)
            //    {
            //        if (amount == 0)
            //            break;
            //        difference = amount - it.Amount ?? 0;
            //        if (difference > 0)
            //            if (product?.InStock < amount)
            //                throw new BO.NotInStockException();
            //        BO.OrderItem temp = new BO.OrderItem();
            //        temp = it.CopyFields(temp);
            //        temp.Amount = amount;
            //        list.Add(temp);
            //    }
            //    else
            //        list.Add(it.CopyFields(new BO.OrderItem()));
            //}

            //border.TotalPrice += product?.Price *  difference; //////////(amount*)
            border.TotalPrice = Math.Round(border.TotalPrice ?? 0, 2);
            border.Items = x.ToList();

            //if (amount == 0)
            //{
            //    dal.OrderItem.Delete(theItem?.ID ?? 0);
            //    return border;
            //}
            dal.OrderItem.Update(new DO.OrderItem
            {
                ID = theItem?.ID ?? 0,
                Name = theItem?.Name,
                OrderID = orderId,
                ProductID = productId,
                Price = theItem?.Price ?? 0,
                Amount = amount,
                IsDeleted = false,
                Path = theItem?.Path
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

        if (id < 1000)
            throw new BO.IllegalIdException();
        try
        {
            DO.Order? order = dal.Order.GetTByFilter((DO.Order? order) => (order.GetValueOrDefault().ID == id) && order.GetValueOrDefault().IsDeleted);
            return order?.OrderToBO();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public List<BO.OrderForList?>? GetDeletedOrders()
    //קבלת רשימת כל ההזמנות המחוקות
    {
        IEnumerable<DO.Order?> listor = dal.Order.GetAll((DO.Order? order) => order.GetValueOrDefault().IsDeleted);
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

    public List<BO.OrderForList?> GetOrdersWithDeleted()
    //קבלת רשימת כל ההזמנות - כולל אלו שנמחקו
    {
        IEnumerable<DO.Order?> listor = dal.Order.GetAll(null);
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

    public void Restore(int id)
    //שיחזור הזמנה שנמחקה, לפי מזהה הזמנה
    {
        if (id <= 0)
            throw new BO.NotExistException();
        try
        {
            DO.Order? order = dal.Order.GetTByFilter((DO.Order? order) => (order.GetValueOrDefault().ID == id) && order.GetValueOrDefault().IsDeleted);
            dal.Order.Restore((DO.Order)order);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public void CancelOrder(int id)
    //מתודה לביטול הזמנה
    {
        //DO.Order order = dal.Order.GetByID(id);
        DO.Order? order = dal.Order.GetTByFilter((DO.Order? order) => (order.GetValueOrDefault().ID == id) && order.GetValueOrDefault().IsDeleted == false);
        if (order?.ShipDate != null && order?.ShipDate < DateTime.Now)
            throw new BO.CanNotUpdateOrderException();
        // TimeSpan twentyfourhours = new TimeSpan(24, 00, 00);
        //if ((order?.OrderDate != null) && (DateTime.Now - order?.OrderDate) < twentyfourhours)
        //    dal.Order.Delete(id);
        if (order?.OrderDate != null)
            dal.Order.Delete(id);
        else
            throw new BO.CantCancelOrderException();
    }

    public IEnumerable<BO.OrderForList> GetOrderList(BO.Filters enumFilter = BO.Filters.None, Object? filterValue = null)
    {

        IEnumerable<DO.Order?> doOrderList =
        enumFilter switch
        {
            BO.Filters.filterByStatus =>
             dal!.Order.GetAll(dp => (dp?.GetStatus()) == (BO.OrderStatus)filterValue && dp?.IsDeleted == false),

            BO.Filters.None =>
            dal!.Order.GetAll((DO.Order? order) => order.GetValueOrDefault().IsDeleted == false),
            _ => dal!.Order.GetAll((DO.Order? order) => order.GetValueOrDefault().IsDeleted == false),
        };

        //return (from DO.Product doProduct in doProductList
        //        select BlApi.Tools.CopyFields(doProduct, new BO.ProductForList()))
        //       .ToList();
        return (from DO.Order doorder in doOrderList
                select doorder.OrderToOrderForList());
    }

}




