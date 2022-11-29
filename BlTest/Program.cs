using BO;
//תכנית ראשית לבדיקות - שלב מספר2
//שירה קהלני והלל אישון
namespace BlTest;
using BlApi;
using BlImplementation;
class Program
{
    //תוכנית ראשית זמנית הבודקת את נכונות כל הקבצים בשכבת הנתונים שעשינו עד כה
    static void testOrder(IBL bOrder)
    {
        int id;
        Console.WriteLine(@"test order:
                Choose one of the following:
                a - TRACKING ORDER
                b - DISPLAY ORDER DETAILS
                c - DISPLAY ORDER LIST
                d - UPDATE SHIP DATE ORDER
                e - UPDATE DELIVERY DATE ORDER
                f - UPDATE ORDER");
        string option = Console.ReadLine();
        switch (option)
        {
            case "a":
                Console.WriteLine("enter the order ID");
                int.TryParse(Console.ReadLine(), out id);
                Console.WriteLine(bOrder.Order.Tracking(id));
                break;
            case "b":
                Console.WriteLine("enter the order ID");
                int.TryParse(Console.ReadLine(), out id);
                Console.WriteLine(bOrder.Order.getDetailsOrder(id));
                break;
            case "c":
                foreach (var item in bOrder.Order.getOrderList())
                {
                    Console.WriteLine(item);
                }
                /// מדפיסים את הכל
                break;
            case "d":
                Console.WriteLine("enter the order ID:");
                int.TryParse(Console.ReadLine(), out id);
                Console.WriteLine(bOrder.Order.UpdateShipDate(id));
                break;
            case "e":
                Console.WriteLine("enter the order ID:");
                int.TryParse(Console.ReadLine(), out id);
                Console.WriteLine(bOrder.Order.UpdateDeliveryDate(id));
                break;
            case "f":
                Console.WriteLine("enter the order ID:");
                int.TryParse(Console.ReadLine(), out id);
                int orderID = id;
                Console.WriteLine("enter the product ID:");
                int.TryParse(Console.ReadLine(), out id);
                int productID = id;
                Console.WriteLine("enter the new amount:");
                int amount;
                int.TryParse(Console.ReadLine(), out amount);
                Console.WriteLine(bOrder.Order.UpdateOrder(orderID, productID, amount));
                break;
        }
    }

    static void testCart(IBl bCart, BO.Cart? myCart)
    {
        int id = 0;
        string option = "";
        while (option != "d")
        {
            int numOfProduct = myCart.orderItems.Count();
            Console.WriteLine("Your basket has" + numOfProduct + "products");
            Console.WriteLine(@"Choose one of the following:
                a - ADD PRODUCT TO THE CART
                b - UPDATE AMOUNT OF PRODUCT
                c - APPROVE ORDER
                d - BACK TO THE MAIN MENU
                ");
            option = Console.ReadLine();
            switch (option)
            {
                case "a":
                    Console.WriteLine("enter the product ID:");
                    int.TryParse(Console.ReadLine(), out id);
                    Console.WriteLine(bCart.Cart.AddProduct(myCart, id));
                    break;
                case "b":
                    Console.WriteLine("enter the product ID:");
                    int.TryParse(Console.ReadLine(), out id);
                    Console.WriteLine("enter the new amount:");
                    int amount;
                    int.TryParse(Console.ReadLine(), out amount);
                    Console.WriteLine(bCart.Cart.UpdateAmountProduct(myCart, id, amount));
                    break;
                case "c":
                    id = bCart.Cart.MakeOrder(myCart);
                    Console.WriteLine("Your order has been confirmed.");
                    Console.WriteLine("Your order id is: " + id);
                    break;
                case "d":
                    break;
                default:
                    Console.WriteLine("ERROR");
                    break;
            }
        }
    }

    static void testProduct(IBl bProduct)
    {
        int id;
        Console.WriteLine(@"test product:
                Choose one of the following:
                a - Get product list to the manager
                b - Get product details
                c - Add product
                d - Remove product
                e - Update product details
                f - Get catalog");
        string option = Console.ReadLine();
        switch (option)
        {
            case "a":
                foreach (var item in bProduct.Product.GetProductList())
                {
                    Console.WriteLine(item);
                }
                /// מדפיסים את הכל
                break;
            case "b":
                Console.WriteLine("enter the product ID");
                int.TryParse(Console.ReadLine(), out id);
                Console.WriteLine(bProduct.Product.GetProductDetails(id));
                break;
            case "c":
                BO.Product? tmpProduct = new BO.Product();
                Console.WriteLine("enter the new product ID");
                int.TryParse(Console.ReadLine(), out id);
                tmpProduct.ID = id;
                Console.WriteLine("enter the new product name");
                tmpProduct.Name = Console.ReadLine();
                Console.WriteLine(@"enter the new product catgory: 
                                        Clothes-0, 
                                        Toys-1, 
                                        Carts-2, 
                                        Bottles-3, 
                                        Diapers-4");
                int.TryParse(Console.ReadLine(), out id);
                int ctg = id;
                switch (ctg)
                {
                    case 0:
                        tmpProduct.Category = BO.Category.Clothes;
                        break;
                    case 1:
                        tmpProduct.Category = BO.Category.Toys;
                        break;
                    case 2:
                        tmpProduct.Category = BO.Category.Carts;
                        break;
                    case 3:
                        tmpProduct.Category = BO.Category.Bottles;
                        break;
                    case 4:
                        tmpProduct.Category = BO.Category.Diapers;
                        break;
                    default:
                        Console.WriteLine("ERROR");
                        break;
                }
                Console.WriteLine("enter the new product price");
                int.TryParse(Console.ReadLine(), out id);
                tmpProduct.Price = id;
                Console.WriteLine("enter the new product amount");
                int.TryParse(Console.ReadLine(), out id);
                tmpProduct.InStock = id;
                bProduct.Product.AddProduct(tmpProduct);
                break;
            case "d":
                Console.WriteLine("enter the product ID");
                int.TryParse(Console.ReadLine(), out id);
                bProduct.Product.RemoveProduct(id);
                Console.WriteLine("DELETED");
                break;
            case "e":
                BO.Product? tmpProduct2 = new BO.Product();
                Console.WriteLine("enter the new product ID");
                int.TryParse(Console.ReadLine(), out id);
                tmpProduct2.ID = id;
                Console.WriteLine("enter the new product name");
                tmpProduct2.Name = Console.ReadLine();
                Console.WriteLine(@"enter the new product catgory: 
                                        Clothes-0, 
                                        Toys-1, 
                                        Carts-2, 
                                        Bottles-3, 
                                        Diapers-4");
                int.TryParse(Console.ReadLine(), out id);
                ctg = id;
                switch (ctg)
                {
                    case 0:
                        tmpProduct2.Category = BO.Category.Clothes;
                        break;
                    case 1:
                        tmpProduct2.Category = BO.Category.Toys;
                        break;
                    case 2:
                        tmpProduct2.Category = BO.Category.Carts;
                        break;
                    case 3:
                        tmpProduct2.Category = BO.Category.Bottles;
                        break;
                    case 4:
                        tmpProduct2.Category = BO.Category.Diapers;
                        break;
                    default:
                        Console.WriteLine("ERROR");
                        break;
                }
                Console.WriteLine("enter the new product price");
                int.TryParse(Console.ReadLine(), out id);
                tmpProduct2.Price = id;
                Console.WriteLine("enter the new product amount");
                int.TryParse(Console.ReadLine(), out id);
                tmpProduct2.InStock = id;
                bProduct.Product.UpdateProductDetails(tmpProduct2);
                break;
            case "f":
                foreach (var item in bProduct.Product.GetProductList())//לשנות לקטלוג!!
                {
                    Console.WriteLine(item);
                }
                /// מדפיסים את הכל
                break;
        }
    }

    static void Main(string[] args)
    {
        IBl bl = BlFactory.GetBl();
        // IDal dal = DalFactory.GetDal();
        BO.Cart? myCart = new BO.Cart();
        int num = 1;
        while (num != 0)
        {
            Console.WriteLine(@"welcome to our store!
                Choose one of the following:
                0-exit
                1-test Order
                2-test Cart
                3-test Product");
            string option = Console.ReadLine();
            bool b = int.TryParse(option, out num);
            try
            {
                if (b)
                {
                    switch (num)
                    {
                        case 1:
                            testOrder(bl);
                            break;
                        case 2:
                            testCart(bl, myCart);
                            break;
                        case 3:
                            testProduct(bl);
                            break;
                        default:
                            break;
                    }
                }
                else
                    Console.WriteLine("ERROR");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            //, או לעשות קצ' כללי או להוסיף עוד קצ'ים
        }
    }
}

