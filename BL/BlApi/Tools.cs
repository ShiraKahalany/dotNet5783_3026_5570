using System.Collections;
using System.Reflection;
namespace BlApi;

public static class Tools
{
    //מתודה להפיכת ישות למחרוזת לצורך הצגת הפרטים
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

    public static string ToStringProperty2<T>(this T t, string suffix = "")
    //מתודה להפיכת ישות למחרוזת לצורך הצגת הפרטים
    {
        string str = "";
        foreach (PropertyInfo prop in t!.GetType().GetProperties())
        {
            if (prop.Name == "IsDeleted")
            {
                bool? val = (bool?)prop.GetValue(t, null);
                if (val ?? false)
                    str += " * * * DELETED * * *:";
                continue;
            }
            var value = prop.GetValue(t, null);
            if (value is not string && value is IEnumerable)
            {
                str = str + "\n" + prop.Name + ":";
                foreach (var item in (IEnumerable)value)
                    str += item.ToStringProperty("      ");
            }

            else
                str += "\n" + suffix + prop.Name + ": " + value;
        }
        str += "\n";
        return str;
    }

    public static Target CopyFields<Source, Target>(this Source source, Target target)
        //מתודת עזר - העתקת ערכי שדות עם שם זהה מאובייקט מקור לאובייקט יעד
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



    public static object CopyPropToStruct<S>(this S from, Type type)
        //מתודה ליצירת אובייקט חדש לפי הסוג שהתקבל, והעתקת כל השדות  הזהים מאובייקט המקור ולאובייקט החדש
    {
        object to = Activator.CreateInstance(type)!; // new object of the Type
        from.CopyFields(to);//copy all value of properties with the same name to the new object
        return to;
    }

    public static BO.OrderStatus GetStatus(this DO.Order order)
        //מתודה המקבלת הזמנה מסוג דו ומחזירה את הסטטוס שלה
    //פונקציה המקבלת הזמנה ומחזירה את הסטטוס שלה
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
    //מתודה המק בלת הזמנה ומחזירה את רשימת המוצרים שלה ואת המחיר הכולל של ההזמנה
    {
        DalApi.IDal dal = DalApi.DalFactory.GetDal() ?? throw new NullReferenceException("Missing Dal");  //מופע הנתונים
        IEnumerable<DO.OrderItem?> items;
        List<BO.OrderItem> list = new List<BO.OrderItem>();
        try
        {
            items = dal.OrderItem.GetAll((DO.OrderItem? orderItem) => orderItem?.OrderID == order.ID && order.IsDeleted == orderItem?.IsDeleted);
            double? sum = 0;
            foreach (DO.OrderItem? item in items)
            {
                sum += item?.Amount * item?.Price;
                BO.OrderItem temp = new BO.OrderItem();
                list.Add(item.CopyFields(temp));
            }
            totalprice = Math.Round(sum ?? 0, 2);
        }
        catch (DO.NotExistException)
        { }
        return list;
    }



    public static BO.Order OrderToBO(this DO.Order order)
    //מתודה להעתקת הזמנה מסוג דו להזמנה מסוג בו
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
    //מתודה להפיכת הזמנה להזמנה-לרשימה
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
        //מתודה המקבלת הזמנה מסוג דו ומחשבת ומחזירה את כמות המוצרים שלה ואת המחיר הכולל שלה
    {
        DalApi.IDal dal = DalApi.DalFactory.GetDal() ?? throw new NullReferenceException("Missing Dal");  //מופע הנתונים
        try
        {
            IEnumerable<DO.OrderItem?> items = dal.OrderItem.GetAll((DO.OrderItem? orderItem) => (orderItem?.OrderID == order.ID) && (order.IsDeleted == orderItem?.IsDeleted));
            foreach (var item in items)
            {
                amountOfItems += item?.Amount ?? 0;
                totalPrice += item?.Price * item?.Amount ?? 0;
            }
            totalPrice = Math.Round(totalPrice, 2);
        }
        catch (DO.NotExistException)
        { }

    }

    public static void UpdateTotalpriceInCart(ref BO.Cart cart, BO.OrderItem item, int dif)
        //מתודה לעדכון המחיר לתשלום הכולל של עגלה - מוסיפה את ההפרש ומעגלת
    {
        cart.TotalPrice = (cart.TotalPrice ?? 0) + (item.Price * dif);
        cart.TotalPrice = Math.Round(cart.TotalPrice ?? 0, 2);
    }

    public static int CheckAmount(this BO.OrderItem item, int amount)
    //בדיקה האם ניתן לשנות את הכמות המבוקשת לפי הזמינות במלאי. אם ניתן - החזרת הכמות
    {
        DalApi.IDal dal = DalApi.DalFactory.GetDal() ?? throw new NullReferenceException("Missing Dal");  //מופע הנתונים
        DO.Product? product;
        try
        {
            product = dal.Product.GetTByFilter((DO.Product? product) => (product?.ID == item.ProductID) && product?.IsDeleted == false);
            if (product?.InStock < amount)  //אם אין מספיק במלאי מהמוצר
                throw new BO.NotInStockException();
            return amount;
        }
        catch (DO.NotExistException)
        {
            throw new BO.NotInStockException();
        }
    }

    public static void updateItemsStock(this BO.Order order)
    //מתודה לעדכון מלאי המוצרים בביטול הזמנה
    {
        DalApi.IDal dal = DalApi.DalFactory.GetDal() ?? throw new NullReferenceException("Missing Dal");  //מופע הנתונים
        foreach (var item in order.Items!)
        {
            DO.Product product;
            try
            {
                product = dal.Product.GetTByFilter(x => x?.ID == item.ProductID) ?? throw new BO.NotExistException();
            }
            catch (DO.NotExistException)
            {
                throw new BO.NotExistException("The product is no longer available");
            }
            product.InStock += item.Amount;
            dal.Product.Update(product);
            dal.OrderItem.Delete(item.ID);
        }
    }


    public static void checkStock(this List<BO.OrderItem> items)
    //מתודת הרחבה - מקבלת רשימת מוצרי-הזמנה ובודקת האם כל המוצרים נמצאים במלאי ובכמות המבוקשת
    {
        DalApi.IDal dal = DalApi.DalFactory.GetDal() ?? throw new NullReferenceException("Missing Dal");  //מופע הנתונים
        foreach (var item in items)
        {
            DO.Product product;
            try
            {
                product = dal.Product.GetTByFilter(x => x?.ID == item.ProductID && x?.IsDeleted == false) ?? throw new BO.NotExistException();
            }
            catch (DO.NotExistException)
            {
                throw new BO.NotExistException("The product is no longer available");
            }
            if (product.InStock < item.Amount)
                throw new BO.NotInStockException();
        }
    }

    public static double updateStockAndReturnTotalPrice(this List<BO.OrderItem> items)
        //מתודה לעדכון מלאי המוצרים בעקבות שיחזור הזמנה, שגם מחשבת את המחיר הכולל של ההזמנה המשוחזרת
    {
        DalApi.IDal dal = DalApi.DalFactory.GetDal() ?? throw new NullReferenceException("Missing Dal");  //מופע הנתונים
        double totalPrice = 0;
        foreach (BO.OrderItem item in items)
        {

            DO.Product product;
            DO.OrderItem orderItem;
            try
            {
                product = dal.Product.GetTByFilter(x => x?.ID == item.ProductID && x?.IsDeleted == false) ?? throw new BO.NotExistException();
            }
            catch (DO.NotExistException)
            {
                throw new BO.NotExistException("The product is no longer available");
            }
            try
            {
                orderItem = dal.OrderItem.GetTByFilter(x => x?.ID == item.ID && x?.IsDeleted == true) ?? throw new BO.NotExistException();
            }
            catch (DO.NotExistException)
            {
                throw new BO.NotExistException();
            }

            orderItem.Price = product.Price;
            double total = (orderItem.Price ?? 0) * (orderItem.Amount ?? 0);
            orderItem.TotalItem = Math.Round(total, 2);
            orderItem.Path = product.Path;
            product.InStock -= orderItem.Amount;
            dal.OrderItem.Update(orderItem);
            dal.OrderItem.Restore(orderItem);
            product.InStock = product.InStock - item.Amount;
            dal.Product.Update((DO.Product)product);
            totalPrice = totalPrice + orderItem.TotalItem ?? 0;
        }
        return Math.Round(totalPrice, 2);
    }


    public static void refreshCart(this BO.Cart cart)
        //מתודה ל"רענון" פריטי העגלה - מביאה את רשימת המוצרים מחדש, שאולי התעדכנו בינתיים
    {
        DalApi.IDal dal = DalApi.DalFactory.GetDal() ?? throw new NullReferenceException("Missing Dal");  //מופע הנתונים
        if (cart.Items == null)
            return;
        try
        {
            var x = from item in cart.Items
                    let product = dal.Product.GetTByFilter(x => x?.ID == item.ProductID)
                    where product?.IsDeleted == false && product?.InStock >= item.Amount
                    select new BO.OrderItem
                    {
                        Amount = item.Amount,
                        Price = product?.Price,
                        ProductID = item.ProductID,
                        Path = product?.Path,
                        ID = item.ID,
                        IsDeleted = item.IsDeleted,
                        TotalItem = Math.Round((item?.Amount * product?.Price) ?? 0, 2),
                        Name = product?.Name
                    };
            cart.Items = x.ToList();
            cart.TotalPrice = Math.Round(x.Sum(item => item.Price * item.Amount) ?? 0, 2);
        }
        catch (DO.NotExistException)
        {
            throw new BO.NotExistException();
        }
    }


}





