using BlApi;
namespace BlImplementation;

//מימוש המתודות של סל קניות
internal class Cart : ICart
{
    DalApi.IDal dal = DalApi.DalFactory.GetDal() ?? throw new NullReferenceException("Missing Dal");  //מופע הנתונים

    public BO.Cart? AddProductToCart(BO.Cart? cart, int id, int amountToAdd)
    //מתודה המקבלת עגלה,מספר מזהה של מוצר, וכמות להוספה, ומוסיפה את המוצר לעגלה
    {
        if (cart == null)
            throw new ArgumentNullException();
        DO.Product? product = new();
        try
        {
            product = dal.Product.GetTByFilter((DO.Product? product) => (product.GetValueOrDefault().ID == id) && product.GetValueOrDefault().IsDeleted == false);
        }
        catch (DO.NotExistException ex)
        {
            throw new BO.NotExistException(ex.Message);
        }
        if (product?.InStock < amountToAdd)
            throw new BO.NotInStockException();
        if (cart.Items != null)
        {
            BO.OrderItem orderitem = cart.Items.Find(x => x != null && x.ProductID == id)!;
            if (orderitem != null)
            {
                cart.Items.Remove(orderitem);
                orderitem.Amount += amountToAdd;
                cart.TotalPrice = (cart.TotalPrice ?? 0) + (product?.Price * amountToAdd);
                cart.TotalPrice = Math.Round(cart.TotalPrice ?? 0, 2);
                cart.Items.Add(orderitem);
                return cart;
            }
        }
        //אם המוצר עוד לא קיים בסל הקניות
        BO.OrderItem temp = new BO.OrderItem
        {
            ID = 0,
            ProductID = id,
            Name = product?.Name,
            Price = product?.Price,
            IsDeleted = false,
            Amount = amountToAdd,
            Path = product?.Path
        };
        if (cart.Items == null)
            cart.Items = new List<BO.OrderItem?>();
        cart.Items.Add(temp);
        cart.TotalPrice = (cart.TotalPrice ?? 0) + (product?.Price * amountToAdd);
        cart.TotalPrice = Math.Round(cart.TotalPrice ?? 0, 2);
        return cart;
    }


    public BO.Cart UpdateAmountOfProductInCart(BO.Cart cart, int id, int amount)
    //מתודה המקבלת סל קניות, מזהה מוצר, וכמות רצויה - ומעדכנת את הכמות של המורצ בסל לכמות הרצויה
    {
        try
        {
            DO.Product? product = dal.Product.GetTByFilter((DO.Product? product) => (product.GetValueOrDefault().ID == id) && product.GetValueOrDefault().IsDeleted == false);
        }
        catch (DO.NotExistException ex)
        {
            throw new BO.NotExistException(ex.Message);
        }
        if (cart.Items == null)
            throw new BO.NotExistException();
        var x = from item in cart.Items
                where item != null
                where !(item.ProductID == id && amount == 0)
                select new BO.OrderItem
                {
                    ID = item.ID,
                    ProductID = item.ProductID,
                    IsDeleted = false,
                    Price = item.Price,
                    Name = item.Name,
                    Path = item.Path,
                    Amount = (item.ProductID == id) ? item.CheckAmount(amount) : item.Amount
                };
        cart.TotalPrice = Math.Round(x.Sum(item => (double)(item.Price * item.Amount)), 2);
        return cart;
    }


    public int MakeAnOrder(BO.Cart? cart)
    //מתודה המקבלת סל קניות ויוצרת ממנו הזמנה
    {
        if (cart == null)
            throw new BO.NotExistException();

        if (cart.CustomerName == null)
            throw new BO.NoNameException();
        if (cart.CustomerAddress == null)
            throw new BO.NoAddressException();
        if ((cart.CustomerEmail == null) || (!cart.CustomerEmail.Contains('@')))
            throw new BO.IllegalEmailException();
        if (cart.Items == null || cart.Items.Count == 0)
            throw new BO.NotItemsInCartException();


        //בדיקת זמינות המוצרים במלאי
        //******פה במקום פוראיצ' לנסות להשתמש בפונקציות של רשימות, כמו פיינד*******

        List<BO.OrderItem> newlist = new List<BO.OrderItem>();  //מה הפואנטה של הרשימה הזאת??
        try
        {
            foreach (BO.OrderItem item in cart.Items)
            {
                DO.Product? product = dal.Product.GetTByFilter((DO.Product? product) => (product.GetValueOrDefault().ID == item.ProductID) && product.GetValueOrDefault().IsDeleted == false);
                if (item == null)
                    break;
                if (product?.InStock <= item.Amount)
                    throw new BO.NotInStockException("The product " + item.ProductID + " is not in stock");
                if (item.Amount <= 0)
                    throw new BO.AmountNotPossitiveException();
                // newlist.Add(item);
            }
            if (/*newlist.Count*/ cart.Items.Count < 0)
                throw new BO.NotItemsInCartException("The cart is empty");

            //יצירת ההזמנה
            DO.Order neworder = new DO.Order
            {
                IsDeleted = false,
                ID = 0,
                CustomerName = cart.CustomerName,
                CustomerEmail = cart.CustomerEmail,
                CustomerAddress = cart.CustomerAddress,
                OrderDate = DateTime.Now,
                ShipDate = null,
                DeliveryDate = null
            };

            int newId = dal.Order.Add(neworder);  //הוספת ההזמנה למאגר ההזמנות


            // var x= from item in newlist


            //עידכון מלאי המוצרים
            foreach (BO.OrderItem item in newlist)
            {
                DO.Product product = (DO.Product)dal.Product.GetTByFilter((DO.Product? product) => (product.GetValueOrDefault().ID == item.ProductID) && product.GetValueOrDefault().IsDeleted == false);
                DO.OrderItem temp = new DO.OrderItem();
                temp.ProductID = product.ID;
                temp = (DO.OrderItem)Tools.CopyPropToStruct(item, typeof(DO.OrderItem));
                temp.OrderID = newId;
                dal.OrderItem.Add(temp);
                product.InStock = product.InStock - item.Amount;
                dal.Product.Update((DO.Product)product);
            }
            return newId;
        }
        catch (DO.AlreadyExistException ex)
        {
            throw new BO.AlreadyExistException(ex.Message);
        }
        catch (DO.NotExistException ex)
        {
            throw new BO.NotExistException(ex.Message);
        }

    }

}
