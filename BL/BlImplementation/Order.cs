
using BlApi;

namespace BlImplementation;


//מימוש ממשק ההזמנות
internal class Order : IOrder
{
    DalApi.IDal dal = DalApi.DalFactory.GetDal() ?? throw new NullReferenceException("Missing Dal");  //מופע הנתונים

    #region Get All Undeleted Orders
    public List<BO.OrderForList?>? GetOrders()
    //מתודה לקבלת רשימת כל ההזמנות התקפות
    {
        IEnumerable<DO.Order?> listor;
        try
        {
            listor = dal.Order.GetAll((DO.Order? order) => order.GetValueOrDefault().IsDeleted == false);
        }
        catch (DO.NotExistException)
        {
            throw new BO.NoItemsException();
        }
        if (!listor.Any())
            throw new BO.NoItemsException();

        var x = from DO.Order order in listor
                orderby order.ID
                select order.OrderToOrderForList();
        return x.ToList();
    }
    #endregion

    #region Get All Orders With Deleted
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
    #endregion

    #region Get Order By ID
    public BO.Order GetOrderById(int id)
    //קבלת הזמנה לפי מספר מזהה
    {
        if (id < 1000)
            throw new BO.IllegalIdException();
        try
        {
            DO.Order? order = dal.Order.GetTByFilter((DO.Order? order) => (order.GetValueOrDefault().ID == id) && order.GetValueOrDefault().IsDeleted == false);
            return order?.OrderToBO()!;
        }
        catch (DO.NotExistException ex)
        {
            throw new BO.OrderNotExistException(ex.Message);
        }
    }
    #endregion

    #region Update Status In Order To Shipped
    public BO.Order UpdateStatusToShipped(int id)
    //עידכון הזמנה ששולחה
    {
        try
        {
            DO.Order order = (DO.Order)dal.Order.GetTByFilter((DO.Order? order) => (order?.ID == id) && order?.IsDeleted == false)!;
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
    #endregion

    #region Update Status In Order To Provided
    public BO.Order UpdateStatusToProvided(int id)
    //עידכון הזמנה שסופקה
    {
        try
        {
            DO.Order order = (DO.Order)dal.Order.GetTByFilter((DO.Order? order) => (order.GetValueOrDefault().ID == id) && order.GetValueOrDefault().IsDeleted == false)!;

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
    #endregion

    #region Follow Order
    public BO.OrderTracking FollowOrder(int id)
    //מעקב הזמנה - הצגת האירועים של ההזמנה והתאריכים שלהם
    {
        try
        {
            DO.Order? order = dal.Order.GetTByFilter((DO.Order? order) => (order?.ID == id) && order?.IsDeleted == false);
            BO.OrderTracking orderTracking = new BO.OrderTracking();
            orderTracking.ID = order?.ID ?? 0;
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
    #endregion

    #region check Amount
    public static BO.OrderItem? checkAmount(DO.OrderItem item, int id, int newAmount, ref int difference, DO.Product? product)
    //פונקציה המקבלת מוצר-בהזמנה, ומוצר, ובודקת האם אפשר להזמין את המוצר, מעדכנת את ההפרש בין הכמות במלאי לכמות המבוקשת
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
    #endregion

    #region Update Amount Of Product
    public BO.Order UpdateAmountOfProduct(int orderId, int productId, int amount)
    //עידכון כמות מוצר בהזמנה- על ידי המנהל
    {
        try
        {
            DO.Order? order = dal.Order.GetTByFilter((DO.Order? order) => (order?.ID == orderId) && order?.IsDeleted == false);
            if (order?.ShipDate != null && order?.ShipDate < DateTime.Now)
                throw new BO.CanNotUpdateOrderException();
            DO.Product? product = dal.Product.GetTByFilter((DO.Product? product) => (product?.ID == productId) && product?.IsDeleted == false);
            BO.Order border = order?.OrderToBO()!;
            DO.OrderItem? theItem = dal.OrderItem.GetTByFilter((DO.OrderItem? orderItem) => orderItem?.OrderID == orderId && orderItem?.IsDeleted == false && orderItem?.ProductID == productId);
            
            if (amount-theItem?.Amount > product?.InStock)
                throw new BO.NotInStockException();

            BO.OrderItem? itemBO = border.Items?.FirstOrDefault(x => x.ProductID == product?.ID);
            if (itemBO == null)
                throw new BO.NotExistException();
            border.Items?.Remove(itemBO);

            DO.Product p = new DO.Product
            {
                ID = product?.ID ?? 0,
                IsDeleted = false,
                Category = product?.Category ?? DO.Category.All,
                InStock = product?.InStock + theItem?.Amount - amount,
                Name = product?.Name,
                Path = product?.Path,
                Price = product?.Price
            };
            dal.Product.Update(p);
            border.TotalPrice += p.Price * (amount - itemBO.Amount);
            itemBO.Amount = amount;
            if (amount == 0)
            {
                dal.OrderItem.DeletePermanently(theItem?.ID ?? 0);
                return border;
            }

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
                TotalItem = (theItem?.Price ?? 0) * amount
            });

            border.Items?.Add(itemBO);
            return border;
        }
        catch (DO.NotExistException ex)
        {
            throw new BO.NotExistException(ex.Message);
        }
    }
    #endregion

    #region Get Deleted Order By ID
    public BO.Order GetDeletedOrderById(int id)
    //קבלת הזמנה שנמחקה, לפי מזהה הזמנה
    {

        if (id < 1000)
            throw new BO.IllegalIdException();
        try
        {
            DO.Order? order = dal.Order.GetTByFilter((DO.Order? order) => (order.GetValueOrDefault().ID == id) && order.GetValueOrDefault().IsDeleted);
            return order?.OrderToBO()!;
        }
        catch (DO.NotExistException ex)
        {
            throw new BO.NotExistException(ex.Message);
        }
    }
    #endregion

    #region Get Deleted Orders
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
    #endregion

    #region Restore 
    public double Restore(int id)
    //שיחזור הזמנה שנמחקה, לפי מזהה הזמנה
    {
        if (id <= 0)
            throw new BO.NotExistException();
        try
        {
            BO.Order boOrder = GetDeletedOrderById(id);
            boOrder.Items!.checkStock();
            double totalPrice = boOrder.Items!.updateStockAndReturnTotalPrice();
            DO.Order? order = dal.Order.GetTByFilter((DO.Order? order) => (order.GetValueOrDefault().ID == id) && order.GetValueOrDefault().IsDeleted);
            dal.Order.Restore((DO.Order)order!);
            return totalPrice;
        }
        catch (DO.NotExistException ex)
        {
            throw new BO.NotExistException(ex.Message);
        }
    }
    #endregion

    #region Cancel Order
    public void CancelOrder(BO.Order or)
    //מתודה לביטול הזמנה
    {
        try
        {
            DO.Order? order = dal.Order.GetTByFilter((DO.Order? order) => (order.GetValueOrDefault().ID == or.ID) && order.GetValueOrDefault().IsDeleted == false);
            if (order?.ShipDate != null || order?.DeliveryDate != null)
                throw new BO.CanNotUpdateOrderException();
            if (order?.OrderDate != null)
            {
                or.updateItemsStock();
                dal.Order.Delete(or.ID);
            }
            else
                throw new BO.CantCancelOrderException();
        }
        catch (DO.NotExistException ex)
        {
            throw new BO.NotExistException(ex.Message);
        }
    }
    #endregion

    #region Get Order
    public BO.Order GetOrder(Func<BO.Order?, bool> filter)
    {
        List<BO.Order> orders;
        try
        {
            orders = GetOrdersByFilter().ToList();
        }
        catch (DO.NotExistException ex)
        {
            throw new BO.OrderNotExistException(ex.Message);
        }
        BO.Order? order = orders.FirstOrDefault(or => filter(or));
        return order ?? throw new BO.OrderNotExistException();
    }
    #endregion

    #region Get Order List By Filter
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
            return (from DO.Order doorder in doOrderList
                    select doorder.OrderToOrderForList());
        }
        catch (DO.NotExistException ex)
        {
            throw new BO.NotExistException(ex.Message);
        }
    }
    #endregion

    #region Get Orders By Filter
    public IEnumerable<BO.Order> GetOrdersByFilter(BO.Filters enumFilter = BO.Filters.None, Object? filterValue = null)
    //מתודה שמקבלת סוג סינון+מסנן רצוי ומחזירה את כל ההזמנות ששייכות אליו
    {
        try
        {
            IEnumerable<DO.Order?> doOrderList =
            enumFilter switch
            {
                BO.Filters.filterByStatus =>
                 dal!.Order.GetAll(dp => ((filterValue != null ? (dp?.GetStatus() == (BO.OrderStatus)filterValue && dp?.IsDeleted == false) : (dp?.IsDeleted == false)))).OrderBy(x => x?.ID),

                BO.Filters.None =>
                dal!.Order.GetAll((DO.Order? order) => order.GetValueOrDefault().IsDeleted == false).OrderBy(x => x?.ID),
                _ => dal!.Order.GetAll((DO.Order? order) => order.GetValueOrDefault().IsDeleted == false).OrderBy(x => x?.ID),
            };

            return (from DO.Order doorder in doOrderList
                    select doorder.OrderToBO());
        }
        catch (DO.NotExistException ex)
        {
            throw new BO.NotExistException(ex.Message);
        }
    }
    #endregion
}




