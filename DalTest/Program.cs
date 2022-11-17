using System;
using DalApi;
using DO;
namespace Dal;
class Program
{

    static void testOrder(DalOrder order)
    {
        try
        {
            Console.WriteLine(@"test order:
                Enter your choice:
                a - ADD ORDER
                b - GET ORDER BY ID
                c - GET THE ORDERS LIST
                d - UPDATE ORDER
                e - DELETE ORDER");
            string option = Console.ReadLine();
            switch (option)
            {
                case "a":
                    Order tmpOrder = new Order();
                    Console.WriteLine("enter the new order ID");
                    int id;
                    int.TryParse(Console.ReadLine(), out id);
                    tmpOrder.ID = id;
                    Console.WriteLine("enter the costumer name");
                    tmpOrder.CustomerName = Console.ReadLine();
                    Console.WriteLine("enter the costumer email");
                    tmpOrder.CustomerEmail = Console.ReadLine();
                    Console.WriteLine("enter the costumer adress");
                    tmpOrder.CustomerAdress = Console.ReadLine();
                    order.Add(tmpOrder);
                    break;
                case "b":
                    Console.WriteLine("enter the order ID");
                    int.TryParse(Console.ReadLine(), out id);
                    int myId = id;
                    Console.WriteLine(order.GetByID(myId));
                    break;
                case "c":
                    foreach (Order? item in order.GetAll())
                    {
                        Console.WriteLine(item);
                    }
                    /// מדפיסים את הכל
                    break;
                case "d":
                    Order tmpOrder2 = new Order();
                    Console.WriteLine("enter the order ID");
                    int.TryParse(Console.ReadLine(), out id);
                    tmpOrder2.ID = id;
                    Console.WriteLine(order.GetByID(id));
                    Console.WriteLine("enter the new costumer name");
                    tmpOrder2.CustomerName = Console.ReadLine();
                    Console.WriteLine("enter the new costumer email");
                    tmpOrder2.CustomerEmail = Console.ReadLine();
                    Console.WriteLine("enter the new costumer adress");
                    tmpOrder2.CustomerAdress = Console.ReadLine();
                    order.Update(tmpOrder2);
                    break;
                case "e":
                    Console.WriteLine("enter the product ID");
                    int.TryParse(Console.ReadLine(), out id);
                    myId = id;
                    order.Delete(myId);
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }      
    }

    static void testOrderItem(DalOrderItem item)
    {
        try
        {
            Console.WriteLine(@"test order item:
                Enter your choice:
                a - ADD ORDER ITEM
                b - GET ORDER ITEM
                c - GET ORDER-ITEMS LIST
                d - UPDATE ORDER ITEM
                e - DELETE ORDER ITEM");
            string option = Console.ReadLine();
            switch (option)
            {
                case "a":
                    OrderItem tmpItem = new OrderItem();
                    Console.WriteLine("enter the new item ID");
                    int id;
                    int.TryParse(Console.ReadLine(), out id);
                    tmpItem.ID = id;
                    Console.WriteLine("enter the new product ID");
                    int.TryParse(Console.ReadLine(), out id);
                    tmpItem.ProductID = id;
                    Console.WriteLine("enter the new Order ID");
                    int.TryParse(Console.ReadLine(), out id);
                    tmpItem.OrderID = id;
                    Console.WriteLine("enter the new order item price");
                    double pri;
                    double.TryParse(Console.ReadLine(), out pri);
                    tmpItem.Price = pri;
                    Console.WriteLine("enter the new order item amount");
                    int.TryParse(Console.ReadLine(), out id);
                    tmpItem.Amount = id;
                    item.Add(tmpItem);
                    break;
                case "b":
                    Console.WriteLine("enter the order item ID");
                    int myId;
                    int.TryParse(Console.ReadLine(), out myId);
                    Console.WriteLine(item.GetByID(myId));
                    break;
                case "c":
                    foreach (OrderItem oItem in item.GetAll())
                    {
                        Console.WriteLine(oItem);
                    }
                    /// מדפיסים את הכל
                    break;
                case "d":
                    OrderItem tmpItem2 = new OrderItem();
                    Console.WriteLine("enter the item ID");
                    int.TryParse(Console.ReadLine(), out id);
                    tmpItem2.ID = id;
                    Console.WriteLine(item.GetByID(id));
                    Console.WriteLine("enter the new product ID");
                    int.TryParse(Console.ReadLine(), out id);
                    tmpItem2.ProductID = id;
                    Console.WriteLine("enter the new Order ID");
                    int.TryParse(Console.ReadLine(), out id);
                    tmpItem2.OrderID = id;
                    Console.WriteLine("enter the new order item price");
                    double pric;
                    double.TryParse(Console.ReadLine(), out pric);
                    tmpItem2.Price = pric;

                    Console.WriteLine("enter the new order item amount");
                    int.TryParse(Console.ReadLine(), out id);
                    tmpItem2.Amount = id;
                    item.Update(tmpItem2);
                    break;
                case "e":
                    Console.WriteLine("enter the product ID");
                    int.TryParse(Console.ReadLine(), out myId);
                    item.Delete(myId);
                    break;
            }
        
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    static void testProduct(DalProduct product)
    {
        try
        {
            Console.WriteLine(@"test product:
                Enter your choice:
                a - ADD PRODUCT
                b - GET PRODUCT BY ID
                c - GET THE PRODUCTS LIST
                d - UPDATE PRODUCT
                e - DELETE PRODUCT");
            string option = Console.ReadLine();
            switch (option)
            {
                case "a":
                    Product tmpProduct = new Product();
                    Console.WriteLine("enter the new product ID");
                    int id;
                    int.TryParse(Console.ReadLine(), out id);
                    tmpProduct.ID = id;
                    Console.WriteLine("enter the new product name");
                    tmpProduct.Name = Console.ReadLine();
                    Console.WriteLine(@"enter the new product catgory: 
                                        Garden-0, 
                                        Bed_room-1, 
                                        Living_room-2, 
                                        Bath_room-3, 
                                        Kitchen-4");
                    int.TryParse(Console.ReadLine(), out id);
                    int ctg = id;
                    switch (ctg)
                    {
                        case 0:
                            tmpProduct.Category = Category.Garden;
                            break;
                        case 1:
                            tmpProduct.Category = Category.Bed_room;
                            break;
                        case 2:
                            tmpProduct.Category = Category.Living_room;
                            break;
                        case 3:
                            tmpProduct.Category = Category.Bath_room;
                            break;
                        case 4:
                            tmpProduct.Category = Category.Kitchen;
                            break;
                        default:
                            Console.WriteLine("ERROR");
                            break;
                    }
                    Console.WriteLine("enter the new product price");
                    double pri;
                    double.TryParse(Console.ReadLine(), out pri);
                    tmpProduct.Price = pri;
                    Console.WriteLine("enter the new product amount");
                    int.TryParse(Console.ReadLine(), out id);
                    tmpProduct.InStock = id;
                    product.Add(tmpProduct);
                    break;
                case "b":
                    Console.WriteLine("enter the product ID");
                    int myId;
                    int.TryParse(Console.ReadLine(), out myId);
                    Console.WriteLine(product.GetByID(myId));
                    break;
                case "c":
                    foreach (Product item in product.GetAll())
                    {
                        Console.WriteLine(item);
                    }

                        ;/// מדפיסים את הכל
                    break;
                case "d":
                    Product tmpProduct2 = new Product();
                    Console.WriteLine("enter the product ID");
                    int.TryParse(Console.ReadLine(), out id);
                    tmpProduct2.ID = id;
                    Console.WriteLine(product.GetByID(id));
                    Console.WriteLine("enter the new product name");
                    tmpProduct2.Name = Console.ReadLine();
                    Console.WriteLine(@"enter the catgory: 
                                        Garden-0, 
                                        Bed_room-1, 
                                        Living_room-2, 
                                        Bath_room-3, 
                                        Kitchen-4");
                    int.TryParse(Console.ReadLine(), out ctg);
                    switch (ctg)
                    {
                        case 0:
                            tmpProduct2.Category = Category.Garden;
                            break;
                        case 1:
                            tmpProduct2.Category = Category.Bed_room;
                            break;
                        case 2:
                            tmpProduct2.Category = Category.Living_room;
                            break;
                        case 3:
                            tmpProduct2.Category = Category.Bath_room;
                            break;
                        case 4:
                            tmpProduct2.Category = Category.Kitchen;
                            break;
                        default:
                            Console.WriteLine("ERROR");
                            break;
                    }
                    double pric;
                    Console.WriteLine("enter the new product price");
                    double.TryParse(Console.ReadLine(), out pric);
                    tmpProduct2.Price = pric;
                    Console.WriteLine("enter the new product amount");
                    int.TryParse(Console.ReadLine(), out id);
                    tmpProduct2.InStock = id;
                    product.Update(tmpProduct2);
                    break;
                case "e":
                    Console.WriteLine("enter the product ID");
                    int.TryParse(Console.ReadLine(), out myId);
                    product.Delete(myId);
                    break;
            }
        
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    static void Main(string[] args)
    {
        DalProduct product = new DalProduct();
        DalOrder order = new DalOrder();
        DalOrderItem item = new DalOrderItem();
        int num = 1;
        while (num != 0)
        {
            Console.WriteLine(@"Hello!
                Enter your choice:
                0-exit
                1-test Order
                2-test OrderItem
                3-test Product");
            string option = Console.ReadLine();
            bool b = int.TryParse(option, out num);
            if (!b)
            {
                Console.WriteLine("ERROR");
                break;
            }
            switch (num)
            {
                case 1:
                    testOrder(order);
                    break;
                case 2:
                    testOrderItem(item);
                    break;
                case 3:
                    testProduct(product);
                    break;
                default:
                    break;
            }

        }
    }
}