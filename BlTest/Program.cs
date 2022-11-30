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
                a - TRACKING ORDER8*
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
                Console.WriteLine(bOrder.Order.FollowOrder(id));
                break;
            case "b":
                Console.WriteLine("enter the order ID");
                int.TryParse(Console.ReadLine(), out id);
                Console.WriteLine(bOrder.Order.GetOrderById(id));
                break;
            case "c":
                foreach (var item in bOrder.Order.GetOrders())
                {
                    Console.WriteLine(item);
                }
                /// מדפיסים את הכל
                break;
            case "d":
                Console.WriteLine("enter the order ID:");
                int.TryParse(Console.ReadLine(), out id);
                Console.WriteLine(bOrder.Order.UpdateStatusToShipped(id));
                break;
            case "e":
                Console.WriteLine("enter the order ID:");
                int.TryParse(Console.ReadLine(), out id);
                Console.WriteLine(bOrder.Order.UpdateStatusToProvided(id));
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
                Console.WriteLine(bOrder.Order.UpdateAmountOfProduct(orderID, productID, amount));
                break;
        }
    }

    static void testCart(IBL bCart, BO.Cart? myCart)
    {
        int id = 0;
        string option = "";
        while (option != "d")
        {
            int numOfProduct = myCart.Items.Count();
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
                    Console.WriteLine(bCart.Cart.AddProductToCart(myCart, id));
                    break;
                case "b":
                    Console.WriteLine("enter the product ID:");
                    int.TryParse(Console.ReadLine(), out id);
                    Console.WriteLine("enter the new amount:");
                    int amount;
                    int.TryParse(Console.ReadLine(), out amount);
                    Console.WriteLine(bCart.Cart.UpdateAmountOfProductInCart(myCart, id, amount));
                    break;
                case "c":
                    id = bCart.Cart.MakeAnOrder(myCart);
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

    static void testProduct(IBL bProduct)
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
                foreach (var item in bProduct.Product.GetListedProducts())
                {
                    Console.WriteLine(item);
                }
                /// מדפיסים את הכל
                break;
            case "b":
                Console.WriteLine("enter the product ID");
                int.TryParse(Console.ReadLine(), out id);
                Console.WriteLine(bProduct.Product.GetProduct(id));
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
                        tmpProduct.Category = BO.Category.Bed_room;
                        break;
                    case 1:
                        tmpProduct.Category = BO.Category.Bath_room;
                        break;
                    case 2:
                        tmpProduct.Category = BO.Category.Living_room;
                        break;
                    case 3:
                        tmpProduct.Category = BO.Category.Garden;
                        break;
                    case 4:
                        tmpProduct.Category = BO.Category.Kitchen;
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
                bProduct.Product.DeleteProduct(id);
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
                        tmpProduct2.Category = BO.Category.Bed_room;
                        break;
                    case 1:
                        tmpProduct2.Category = BO.Category.Bath_room;
                        break;
                    case 2:
                        tmpProduct2.Category = BO.Category.Living_room;
                        break;
                    case 3:
                        tmpProduct2.Category = BO.Category.Garden;
                        break;
                    case 4:
                        tmpProduct2.Category = BO.Category.Kitchen;
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
                bProduct.Product.UpdateProduct(tmpProduct2);
                break;
            case "f":
                foreach (var item in bProduct.Product.GetListedProducts())//לשנות לקטלוג!!
                {
                    Console.WriteLine(item);
                }
                /// מדפיסים את הכל
                break;
        }
    }
    //[STAThread]
    public static void Main(string[] args) 
    {
        IBL bl = BL.instance;
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

