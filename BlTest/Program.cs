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
        Console.WriteLine(@"Test Order:
                Choose one of the following:
                a - TRACKING ORDER
                b - GET ORDER DETAILS
                c - GET ORDER LIST
                d - UPDATE SHIP DATE
                e - UPDATE DELIVERY DATE
                f - UPDATE ORDER");
        string option = Console.ReadLine();
        switch (option)
        {
            case "a":
                Console.WriteLine("Enter the order ID");
                int.TryParse(Console.ReadLine(), out id);
                Console.WriteLine(bOrder.Order.FollowOrder(id));
                break;
            case "b":
                Console.WriteLine("Enter the order ID");
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
                Console.WriteLine("Enter the order ID:");
                int.TryParse(Console.ReadLine(), out id);
                Console.WriteLine(bOrder.Order.UpdateStatusToShipped(id));
                break;
            case "e":
                Console.WriteLine("Enter the order ID:");
                int.TryParse(Console.ReadLine(), out id);
                Console.WriteLine(bOrder.Order.UpdateStatusToProvided(id));
                break;
            case "f":
                Console.WriteLine("Enter the order ID:");
                int.TryParse(Console.ReadLine(), out id);
                int orderID = id;
                Console.WriteLine("Enter the product ID:");
                int.TryParse(Console.ReadLine(), out id);
                int productID = id;
                Console.WriteLine("Enter the new amount:");
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
            Console.WriteLine("Your cart has" + numOfProduct + "products");
            Console.WriteLine(@"Choose one of the following:
                a - ADD  PRODUCT TO THE CART
                b - UPDATE AMOUNT OF  PRODUCT
                c - APPROVE ORDER
                d - BACK TO THE MAIN MENU
                ");
            option = Console.ReadLine();
            switch (option)
            {
                case "a":
                    Console.WriteLine("Enter the product ID:");
                    int.TryParse(Console.ReadLine(), out id);
                    Console.WriteLine("Enter the amount to add:");
                    int amountToAdd = 1;
                    int.TryParse(Console.ReadLine(), out amountToAdd);
                    Console.WriteLine(bCart.Cart.AddProductToCart(myCart, id, amountToAdd));
                    break;
                case "b":
                    Console.WriteLine("Enter the product ID:");
                    int.TryParse(Console.ReadLine(), out id);
                    Console.WriteLine("Enter the new amount:");
                    int amount;
                    int.TryParse(Console.ReadLine(), out amount);
                    Console.WriteLine(bCart.Cart.UpdateAmountOfProductInCart(myCart, id, amount));
                    break;
                case "c":
                    id = bCart.Cart.MakeAnOrder(myCart);
                    Console.WriteLine("Nice! Your order has been confirmed");
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
        Console.WriteLine(@"Test product:
                Choose one of the following:
                a - Get product list to the manager
                b - Get product details
                c - Add product
                d - Remove product
                e - Update product details
                f - Get catalog
                g - Get the list of deleted products (manager)
                h - Get the complete products list (including deleted products)
                i - Restore a deleted product (manager)");
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
                Console.WriteLine("Enter the product ID");
                int.TryParse(Console.ReadLine(), out id);
                Console.WriteLine(bProduct.Product.GetProduct(id));
                break;
            case "c":
                BO.Product? tmpProduct = new BO.Product();
                Console.WriteLine("Enter the new product ID");
                int.TryParse(Console.ReadLine(), out id);
                tmpProduct.ID = id;
                Console.WriteLine("Enter the new product name");
                tmpProduct.Name = Console.ReadLine();
                Console.WriteLine(@"Enter the new product catgory: 
                                        0: Living-room, 
                                        1: Bathroom, 
                                        2: Kitchen, 
                                        3: Bedroom, 
                                        4: Garden");
                int.TryParse(Console.ReadLine(), out id);
                int ctg = id;
                switch (ctg)
                {
                    case 0:
                        tmpProduct.Category = BO.Category.Living_room;
                        break;
                    case 1:
                        tmpProduct.Category = BO.Category.Bathroom;
                        break;
                    case 2:
                        tmpProduct.Category = BO.Category.Kitchen;
                        break;
                    case 3:
                        tmpProduct.Category = BO.Category.Bedroom;
                        break;
                    case 4:
                        tmpProduct.Category = BO.Category.Garden;
                        break;
                    default:
                        Console.WriteLine("ERROR");
                        break;
                }
                Console.WriteLine("Enter the new product price");
                int.TryParse(Console.ReadLine(), out id);
                tmpProduct.Price = id;
                Console.WriteLine("Enter the new product amount");
                int.TryParse(Console.ReadLine(), out id);
                tmpProduct.InStock = id;
                bProduct.Product.AddProduct(tmpProduct);
                break;
            case "d":
                Console.WriteLine("Enter the product ID");
                int.TryParse(Console.ReadLine(), out id);
                bProduct.Product.DeleteProduct(id);
                Console.WriteLine("DELETED");
                break;
            case "e":
                BO.Product? tmpProduct2 = new BO.Product();
                Console.WriteLine("Enter the new product ID");
                int.TryParse(Console.ReadLine(), out id);
                tmpProduct2.ID = id;
                Console.WriteLine("Enter the new product name");
                tmpProduct2.Name = Console.ReadLine();
                Console.WriteLine(@"Enter the new product catgory: 
                                        0: Living-room, 
                                        1: Bathroom, 
                                        2: Kitchen, 
                                        3: Bedroom, 
                                        4: Garden");
                int.TryParse(Console.ReadLine(), out id);
                ctg = id;
                switch (ctg)
                {
                    case 0:
                        tmpProduct2.Category = BO.Category.Living_room;
                        break;
                    case 1:
                        tmpProduct2.Category = BO.Category.Bathroom;
                        break;
                    case 2:
                        tmpProduct2.Category = BO.Category.Kitchen;
                        break;
                    case 3:
                        tmpProduct2.Category = BO.Category.Bedroom;
                        break;
                    case 4:
                        tmpProduct2.Category = BO.Category.Garden;
                        break;
                    default:
                        Console.WriteLine("ERROR");
                        break;
                }
                Console.WriteLine("Enter the new product price");
                int.TryParse(Console.ReadLine(), out id);
                tmpProduct2.Price = id;
                Console.WriteLine("Enter the new product amount");
                int.TryParse(Console.ReadLine(), out id);
                tmpProduct2.InStock = id;
                bProduct.Product.UpdateProduct(tmpProduct2);
                break;
            case "f":
                foreach (var item in bProduct.Product.GetListedProducts())
                {
                    Console.WriteLine(item);
                }
                break;
            case "g":
                foreach (var item in bProduct.Product.GetListedDeletedProducts())
                {
                    Console.WriteLine(item);
                }
                break;
            case "h":
                foreach (var item in bProduct.Product.GetListedProductsWithDeleted())
                {
                    Console.WriteLine(item);
                }
                break;
            case "i":
                Console.WriteLine("Enter the deleted product ID");
                int.TryParse(Console.ReadLine(), out id);
                BO.Product? tmpProduct3 = bProduct.Product.GetDeletedById(id);
                Console.WriteLine("The product is:");
                Console.WriteLine(tmpProduct3);
                Console.WriteLine("Do you want to restore it? 1- YES, 0- NO");
                if(id==1)
                {
                    tmpProduct3.IsDeleted = false;
                    bProduct.Product.Restore(id);
                }
                Console.WriteLine("Restored successfully!");
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
            Console.WriteLine(@"Hello!
                Choose one of the following:
                0-exit
                1-Test order
                2-Test cart
                3-Test product");
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
            
        }
    }
}

