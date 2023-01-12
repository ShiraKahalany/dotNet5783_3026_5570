using System.Collections;
using System.Reflection;
//namespace BO;
namespace BlApi;

//מתודות הרחבה
//public static class Tools
//{

internal static class Tools
{
    public static string ToStringProperty<T>(this T t, string suffix = "") =>

        t!.GetType().GetProperties().Aggregate(suffix, (str, prop) =>

        {

            str += "\n" + suffix;

            var value = prop!.GetValue(t, null);

            if (prop.Name == "isDeleted" && (bool)value!) return str + " * * * DELETED * * *:";

            str += prop.Name + ": ";

            if (value is not string && value is IEnumerable)

                return str + ((IEnumerable<object>)value).Aggregate("", (str, item) => str + item.ToStringProperty(suffix + "    "));

            return str + value;

        })

        + "\n" + suffix + "==============";

    //public static string ToStringProperty<T>(this T t, string suffix = "")
    ////מתודה להפיכת ישות למחרוזת לצורך הצגת הפרטים
    //{
    //    string str = "";
    //    foreach (PropertyInfo prop in t.GetType().GetProperties())
    //    {
    //        if (prop.Name == "IsDeleted")
    //        {
    //            bool? val = (bool?)prop.GetValue(t, null);
    //            if (val ?? false)
    //                str += " * * * DELETED * * *:";
    //            continue;
    //        }
    //        var value = prop.GetValue(t, null);
    //        if (value is not string && value is IEnumerable)
    //        {
    //            str = str + "\n" + prop.Name + ":";
    //            foreach (var item in (IEnumerable)value)
    //                str += item.ToStringProperty("      ");
    //        }

    //        else
    //            str += "\n" + suffix + prop.Name + ": " + value;
    //    }
    //    str += "\n";
    //    return str;
    //}

    public static Target CopyFields<Source, Target>(this Source source, Target target)
    {

        if (source is not null && target is not null)
        {
            Dictionary<string, PropertyInfo> propertiesInfoTarget = target.GetType().GetProperties()
                .ToDictionary(p => p.Name, p => p);

            IEnumerable<PropertyInfo> propertiesInfoSource = source.GetType().GetProperties();

            foreach (var propertyInfo in propertiesInfoSource)
            {
                if (propertiesInfoTarget.ContainsKey(propertyInfo.Name)
                    && (propertyInfo.PropertyType == typeof(string) || !(propertyInfo.PropertyType.IsClass)))
                {
                    propertiesInfoTarget[propertyInfo.Name].SetValue(target, propertyInfo.GetValue(source));
                }
            }
        }
        return target;
    }

    public static object CopyPropToStruct<S>(this S from, Type type)//get the typy we want to copy to 
    {
        object to = Activator.CreateInstance(type)!; // new object of the Type
        from.CopyFields(to);//copy all value of properties with the same name to the new object
        return to;
    }

    public static BO.OrderStatus GetStatus(this DO.Order order)
    {
        if (order.DeliveryDate != null && order.DeliveryDate < DateTime.Now)
            return BO.OrderStatus.Delivered;
        else
        {
            if (order.ShipDate != null && order.ShipDate < DateTime.Now)
                return BO.OrderStatus.Shipped;
            else
            {
                if (order.OrderDate != null && order.OrderDate < DateTime.Now)
                    return BO.OrderStatus.Ordered;
                else
                    return BO.OrderStatus.None;
            }
        }
    }


    public static List<BO.OrderItem> GetItems(this DO.Order order, ref double totalprice)
    {
        DalApi.IDal dal = DalApi.DalFactory.GetDal() ?? throw new NullReferenceException("Missing Dal");  //מופע הנתונים
        IEnumerable<DO.OrderItem?> items = dal.OrderItem.GetAll((DO.OrderItem? orderItem) => orderItem.GetValueOrDefault().OrderID == order.ID && orderItem.GetValueOrDefault().IsDeleted == false);
        List<BO.OrderItem> list = new List<BO.OrderItem>();
        double? sum = 0;
        foreach (DO.OrderItem item in items)
        {
            sum += item.Amount * item.Price;
            BO.OrderItem temp = new BO.OrderItem();
            list.Add(item.CopyFields(temp));
        }
        totalprice = Math.Round(sum ?? 0, 2);
        return list;
    }



    public static BO.Order OrderToBO(this DO.Order order)
    //
    {
        BO.Order or = new BO.Order();
        or = order.CopyFields(or);
        or.Status = order.GetStatus();
        double totalPrice = 0;
        or.Items = order.GetItems(ref totalPrice);
        or.TotalPrice = totalPrice;
        return or;
    }

    public static BO.OrderForList OrderToOrderForList(this DO.Order order)
    //
    {
        BO.OrderForList or = new BO.OrderForList();
        or = order.CopyFields(or);
        or.Status = order.GetStatus();
        double totalPrice = 0;
        int amountOfItems = 0;
        order.CalcPriceAndAmount(ref totalPrice, ref amountOfItems);
        or.TotalPrice = totalPrice;
        or.AmountOfItems = amountOfItems;
        return or;
    }

    public static void CalcPriceAndAmount(this DO.Order order, ref double totalPrice, ref int amountOfItems)
    {
        DalApi.IDal dal = DalApi.DalFactory.GetDal() ?? throw new NullReferenceException("Missing Dal");  //מופע הנתונים
        IEnumerable<DO.OrderItem?> items = dal.OrderItem.GetAll((DO.OrderItem? orderItem) => orderItem.GetValueOrDefault().OrderID == order.ID && orderItem.GetValueOrDefault().IsDeleted == false);
        foreach (var item in items)
        {
            amountOfItems += item?.Amount ?? 0;
            totalPrice += item?.Price * item?.Amount ?? 0;
        }
        totalPrice = Math.Round(totalPrice, 2);
    }

    public static void UpdateTotalpriceInCart(ref BO.Cart cart, BO.OrderItem item, int dif)
    {
        cart.TotalPrice = (cart.TotalPrice ?? 0) + (item.Price * dif);
        cart.TotalPrice = Math.Round(cart.TotalPrice ?? 0, 2);
        //return cart;
    }

    public static int CheckAmount(this BO.OrderItem item, int amount)
    //בדיקה האם ניתן לשנות את הכמות המבוקשת לפי הזמינות במלאי. אם ניתן - החזרת הכמות
    {
        DalApi.IDal dal = DalApi.DalFactory.GetDal() ?? throw new NullReferenceException("Missing Dal");  //מופע הנתונים
        DO.Product? product = dal.Product.GetTByFilter((DO.Product? product) => (product.GetValueOrDefault().ID == item.ProductID) && product.GetValueOrDefault().IsDeleted == false);
        //int? difference = amount - item.Amount;
        if (product?.InStock < amount)  //אם אין מספיק במלאי מהמוצר
            throw new BO.NotInStockException();
        return amount;
    }



}




