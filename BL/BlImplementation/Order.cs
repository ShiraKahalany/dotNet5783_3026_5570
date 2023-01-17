
using BlApi;

namespace BlImplementation;


//מימוש ממשק ההזמנות
internal class Order : IOrder
{
    DalApi.IDal dal = DalApi.DalFactory.GetDal() ?? throw new NullReferenceException("Missing Dal");  //מופע הנתונים


    public List<BO.OrderForList?>? GetOrders()
    //מתודה לקבלת רשימת כל ההזמנות התקפות
    {
        IEnumerable<DO.Order?> listor;
        try
        {
            listor = dal.Order.GetAll((DO.Order? order) => order.GetValueOrDefault().IsDeleted == false);
        }
        catch (DO.NotExistException ex)
        {
            throw new BO.NoItemsException();
        }
        if (!listor.Any())
            throw new BO.NoItemsException();

        var x = from DO.Order order in listor
                select order.OrderToOrderForList();
        return x.ToList();

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
        catch (DO.NotExistException ex)
        {
            throw new BO.OrderNotExistException(ex.Message);
        }
    }

    public BO.Order UpdateStatusToShipped(int id)
    //עידכון הזמנה ששולחה
    {
        try
        {
            DO.Order order = (DO.Order)dal.Order.GetTByFilter((DO.Order? order) => (order.GetValueOrDefault().ID == id) && order.GetValueOrDefault().IsDeleted == false);
            if (order.ShipDate != null)
                throw new BO.OrderHasShippedException();
            order.ShipDate = DateTime.Now;
            dal.Order.Update((DO.Order)order);
            return order.OrderToBO();
        }
        catch (DO.NotExistException ex)
        {
            throw new BO.NotExistException(ex.Message);
        }
    }
    public BO.Order UpdateStatusToProvided(int id)
    //עידכון הזמנה שסופקה
    {
        try
        {
            DO.Order order = (DO.Order)dal.Order.GetTByFilter((DO.Order? order) => (order.GetValueOrDefault().ID == id) && order.GetValueOrDefault().IsDeleted == false);
            if (order.DeliveryDate != null)
                throw new BO.OrderHasDeliveredException();
            if (order.ShipDate == null)
                throw new BO.OrderHasNotShippedException();
            order.DeliveryDate = DateTime.Now;
            dal.Order.Update((DO.Order)order);
            return order.OrderToBO();
        }
        catch (DO.NotExistException ex)
        {
            throw new BO.NotExistException(ex.Message);
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
        catch (DO.NotExistException ex)
        {
            throw new BO.NotExistException(ex.Message);
        }

    }

    public static BO.OrderItem? checkAmount(DO.OrderItem item, int id, int newAmount, ref int difference, DO.Product? product)
    {
        try
        {
            if (id == item.ProductID)
            {
                if (newAmount == 0)
                {
                    DalApi.IDal dal = DalApi.DalFactory.GetDal() ?? throw new NullReferenceException("Missing Dal");  //מופע הנתונים
                    dal.OrderItem.Delete(item.ID);
                    return null;
                }
                difference = newAmount - item.Amount ?? 0;
                if (newAmount > item.Amount)
                    if (product?.InStock < difference)
                        throw new BO.NotInStockException();
                BO.OrderItem temp = new BO.OrderItem();
                temp = item.CopyFields(temp);
                temp.Amount = newAmount;
                return temp;
            }
            else
                return item.CopyFields(new BO.OrderItem());
        }
        catch (DO.NotExistException ex)
        {
            throw new BO.NotExistException(ex.Message);
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
                    let temp = checkAmount(it, productId, amount, ref difference, product)
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
                Path = theItem?.Path,
                TotalItem = (theItem?.Price ?? 0)*amount
            });
            return border;
        }
        catch (DO.NotExistException ex)
        {
            throw new BO.NotExistException(ex.Message);
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
        catch (DO.NotExistException ex)
        {
            throw new BO.NotExistException(ex.Message);
        }
    }
    public List<BO.OrderForList?>? GetDeletedOrders()
    //קבלת רשימת כל ההזמנות המחוקות
    {
        IEnumerable<DO.Order?> listor = new List<DO.Order?>();
        try
        {
            listor = dal.Order.GetAll((DO.Order? order) => order.GetValueOrDefault().IsDeleted);
        }
        catch (DO.NotExistException ex)
        {
            throw new BO.NotExistException(ex.Message);
        }
        if (!listor.Any())
            throw new BO.NoItemsException();
        var x = from DO.Order order in listor
                select order.OrderToOrderForList();
        return x.ToList();
    }

    public List<BO.OrderForList?> GetOrdersWithDeleted()
    //קבלת רשימת כל ההזמנות - כולל אלו שנמחקו
    {
        IEnumerable<DO.Order?> listor = new List<DO.Order?>();
        try
        {
            listor = dal.Order.GetAll(null);
        }
        catch (DO.NotExistException ex)
        {
            throw new BO.NotExistException(ex.Message);
        }
        if (!listor.Any())
            throw new BO.NoItemsException();
        var x = from DO.Order order in listor
                select order.OrderToOrderForList();
        return x.ToList();
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
        catch (DO.NotExistException ex)
        {
            throw new BO.NotExistException(ex.Message);
        }
    }

    public void CancelOrder(int id)
    //מתודה לביטול הזמנה
    {
        try
        {
            DO.Order? order = dal.Order.GetTByFilter((DO.Order? order) => (order.GetValueOrDefault().ID == id) && order.GetValueOrDefault().IsDeleted == false);
            if (order?.ShipDate != null)
                throw new BO.CanNotUpdateOrderException();
            if (order?.OrderDate != null)
                dal.Order.Delete(id);
            else
                throw new BO.CantCancelOrderException();
        }
        catch (DO.NotExistException ex)
        {
            throw new BO.NotExistException(ex.Message);
        }

    }

    public IEnumerable<BO.OrderForList> GetOrderList(BO.Filters enumFilter = BO.Filters.None, Object? filterValue = null)
    {
        try
        {
            IEnumerable<DO.Order?> doOrderList =
            enumFilter switch
            {
                BO.Filters.filterByStatus =>
                 dal!.Order.GetAll(dp => ((filterValue != null ? (dp?.GetStatus() == (BO.OrderStatus)filterValue && dp?.IsDeleted == false) : (dp?.IsDeleted == false)))),

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
        catch (DO.NotExistException ex)
        {
            throw new BO.NotExistException(ex.Message);
        }
    }

}




