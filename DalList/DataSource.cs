using DO;
namespace Dal;

internal class DataSource
{
    internal static DataSource s_instance { get; } = new DataSource();
    public DataSource() { s_Initialize(); }    /// <summary>
    /// ???private??
    /// </summary>

    public readonly Random rnd = new Random(); //a random number
    internal List<Order?> Orders { get;}=new List<Order?> { };
    internal List<Product?> Products { get; } = new List<Product?> { };
    internal List<OrderItem?> OrderItems { get; } = new List<OrderItem?> { };
    public void AddOrder(Order order) { Orders.Add(order); }
    public void AddOrderItem(OrderItem orderitem) { OrderItems.Add(orderitem); }
    public void AddProduct(Product product) { Products.Add(product); }

    internal static class Config //static?????
    {
        //nextOrderNumber
        internal const int s_startOrderNumber = 1000;
        private static int s_nextOrderNumber = s_startOrderNumber;
        internal static int NextOrderNumber { get => ++s_nextOrderNumber; }
        //NextOrderItemNumber
        internal const int s_startOrderItemNumber = 100000;
        private static int s_nextOrderItemNumber = s_startOrderItemNumber;
        internal static int NextOrderItemNumber { get => ++s_nextOrderItemNumber; }

    }
    private void s_Initialize()
    {
        createProducts();
        createOrders();
        createOrderItems();
    }

    string[] namesArr = { "Moshe", "Dina", "Shira", "Hallel", "Dani", "Yael", "Josef", "Michael", "Ruth", "Daniel", "Roei", "Shay", "Ron", "Yaron", "Shoshana", "Miryam", "Reut", "Eva", "Ayala", "Tikva", "Tirza", "Dikla", "Moriya", "Hadas", "Yafit", "Yossi", "Avi", "Merav", "Gital", "Helen" };
    string[] lastNamesArr = { "Azulay", "Edri", "Cohen", "Levi", "Biton", "Fridman", "Hadad", "Gabai", "Catz", "Zilberstein", "Malka", "Avraham", "Cherner", "Ben-David", "Orgad", "Ichler", "Alush", "Amsalem", "Papel", "Rapaport", "Buchner", "Bibass", "Bechman", "Block", "Blich", "Ben-shabat", "Braun", "Malool", "Dagan", "Glick", "Hod" };
    string[] citiesArr = { "Ben-Yehuda", "Elyakim", "Jerusalem", "Yokneam", "Raanana", "Tel-Aviv", "Beer-Sheva", "Eilat", "Avivim", "Netanya", "Tzfat", "Ashdod", "Heifa", "Afula", "Bet-Shean", "Ofakim", "Yeruham", "Hadera", "Dimona", "Bet-Shemesh", "Modiin", "Bat-Yam", "Yavne" };
    string[] streetArr = { "Hashaked", "Hagefen", "Hazait", "Hertzel", "Ben-Guryon", "Bialik", "Migdal", "Hateena", "Eli-Cohen", "Hardufim", "Rambam", "Hertzog", "Berner" };
    string[] productnamesArr = { "Kitchen rug", "Sea view picture", "Table cloth", "Pine wood youth bed", "Children's bed", "One and a half beds", "Double bed", "White kitchen cabinet", "Wooden chest of drawers", "Iron dresser", "Kitchen chair", "Room mirror", "Body mirror", "Duck bath rug", "Large white fridge", "Small white fridge", "Black oven", "White kettle", "Black microwave", "toaster", "large stainless steel pot", "large glass pot", "small glass pot", "medium glass pot", "medium stainless steel pot", "small stainless steel pot", "green leather sofa", "microwave White", "powerful red microwave", "blue fabric sofa", "glass kettle", "vintage kettle", "white lamb", "Yellow lamb" };
    private void createOrders()
    {
        int myID;
        string? myCustomerName, myCustomerEmail, myCustomerAdress;
        DateTime? myOrderDate, myShipDate, myDeliveryDate;

        for (int i = 0; i < 20; i++)
        {
            myID = Config.NextOrderNumber;
            myCustomerName = (namesArr[rnd.Next(namesArr.Length)]) + " " + (lastNamesArr[rnd.Next(lastNamesArr.Length)]);
            myCustomerEmail = myCustomerName + "@gmail.com";
            myCustomerAdress = (streetArr[rnd.Next(streetArr.Length)]) + " " + rnd.Next(200) + ", " + citiesArr[rnd.Next(citiesArr.Length)];
            myOrderDate = DateTime.Now - new TimeSpan(rnd.NextInt64(10L * 1000L * 3600L * 24L * 100L));
            myShipDate = null;
            if (i < 16)
                myShipDate = DateTime.Now - new TimeSpan(rnd.NextInt64(10L * 1000L * 3600L * 24L * 50L));
            myDeliveryDate = null;
            if (i < 12)
                myDeliveryDate = DateTime.Now - new TimeSpan(rnd.NextInt64(10L * 1000L * 3600L * 24L * 20L));
           
            Orders.Add(
            new Order
            {
                IsDeleted=false,
                ID= myID,
                CustomerName= myCustomerName,
                CustomerEmail= myCustomerEmail,
                CustomerAdress= myCustomerAdress,
                OrderDate= myOrderDate,
                shipDate= myShipDate,
                DeliveryrDate= myDeliveryDate
            });
        }
    }
    private void createProducts()
    {
        Products.Add(
        new Product
        {
            IsDeleted = false,
            ID = 175894,
            Name = "Kitchen rug",
            Price = 79.99,
            Category = Category.Kitchen,
            InStock = 0
        });

        Products.Add(
        new Product
        {
            IsDeleted = false,
            ID = 576346,
            Name = "Duck bath rug",
            Price = 39.99,
            Category = Category.Bath_room,
            InStock = 198
        });
        Products.Add(
        new Product
        {
            IsDeleted = false,
            ID = 864357,
            Name = "powerful red microwave",
            Price = 299.99,
            Category = Category.Kitchen,
            InStock = 45
        });
        Products.Add(
        new Product
        {
            IsDeleted = false,
            ID = 853267,
            Name = "Children's bed",
            Price = 1999.99,
            Category = Category.Bed_room,
            InStock = 8
        });

        Products.Add(
        new Product
        {
            IsDeleted = false,
            ID = 923654,
            Name = "Garden swing",
            Price = 859.99,
            Category = Category.Garden,
            InStock = 12
        });

        Products.Add(
        new Product
        {
            IsDeleted = false,
            ID = 856245,
            Name = "green leather sofa",
            Price = 7589.99,
            Category = Category.Living_room,
            InStock = 15
        });

        Products.Add(
        new Product
        {
            IsDeleted = false,
            ID = 235765,
            Name = "Sea view picture",
            Price = 139.99,
            Category = Category.Living_room,
            InStock = 64
        });

        Products.Add(
        new Product
        {
            IsDeleted = false,
            ID = 834675,
            Name = "Room mirror",
            Price = 279.99,
            Category = Category.Bed_room,
            InStock = 38
        });
        Products.Add(
        new Product
        {
            IsDeleted = false,
            ID = 235676,
            Name = "Colorful garden umbrella",
            Price = 499.99,
            Category = Category.Garden,
            InStock = 2
        });
        Products.Add(
        new Product
        {
            IsDeleted = false,
            ID = 153217,
            Name = "Bath curtain",
            Price = 49.99,
            Category = Category.Bath_room,
            InStock = 82
        });
    }
    private void createOrderItems()
    {
        int ordersCounter = 0;
        for(int i = 0, j=0; i < 40; j++)
        {
            Product? product = Products[rnd.Next(Products.Count - 1)];
            while (product?.IsDeleted == true)
                product = Products[++j];
            int NumOfItemsToThisOrder = rnd.Next(4);
            Order? order = Orders[j];
            while (order?.IsDeleted == true)
                order = Orders[++j];
            ordersCounter++;
            for (int k=0; k < NumOfItemsToThisOrder; k++)
            {
            OrderItems.Add(
                new OrderItem
                {
                    IsDeleted = false,
                    ID = Config.NextOrderItemNumber,
                    ProductID = product?.ID ?? 0,
                    OrderID = order?.ID ?? 0,
                    Price = product?.Price ?? 0,
                    Amount = rnd.Next(5)
                });
                product = Products[rnd.Next(Products.Count - 1)];
                i++;
                if (ordersCounter == 20)
                    j = 0;
            }           
        }
    }
    

    
}





