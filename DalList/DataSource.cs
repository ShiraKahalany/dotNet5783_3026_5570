using DO;
namespace Dal;


//מחלקה עבור הנתונים
internal class DataSource
{
    internal static DataSource s_instance { get; } = new DataSource();   //יצירת מופע נתונים
    public DataSource() { s_Initialize(); }    /// <summary>
    /// ???private??
    /// </summary>

    public readonly Random rnd = new Random(); //a random number
    internal List<Order?> Orders { get;}=new List<Order?> { };  //רשימת הזמנות
    internal List<Product?> Products { get; } = new List<Product?> { };  //רשימת מוצרים
    internal List<OrderItem?> OrderItems { get; } = new List<OrderItem?> { };  //רשימת פריטי הזמנות

    internal static class Config 
        //מחלקה עבור הגדרת מספרים רצים להזמנות ולפריטי ההזמנות
    {
        //nextOrderNumber
        internal const int s_startOrderNumber = 1000;
        private static int s_nextOrderNumber = s_startOrderNumber;
        internal static int NextOrderNumber { get => ++s_nextOrderNumber; }
        //NextOrderItemNumber
        internal const int s_startOrderItemNumber = 100000;
        private static int s_nextOrderItemNumber = s_startOrderItemNumber;
        internal static int NextOrderItemNumber { get => ++s_nextOrderItemNumber; }

        internal const int s_startProductNumber = 100000;
        private static int s_nextProductNumber = s_startProductNumber;
        internal static int NextProductNumber { get => ++s_nextProductNumber; }

    }
    private void s_Initialize()
        // יצירת מופע נתונים
    {
        createProducts();
        createOrders();
        createOrderItems();
    }

    private void createOrders()  //יצירת הזמנות
    {
        string[] namesArr = { "Moshe", "Dina", "Shira", "Hallel", "Dani", "Yael", "Josef", "Michael", "Ruth", "Daniel", "Roei", "Shay", "Ron", "Yaron", "Shoshana", "Miryam", "Reut", "Eva", "Ayala", "Tikva", "Tirza", "Dikla", "Moriya", "Hadas", "Yafit", "Yossi", "Avi", "Merav", "Gital", "Helen" };
        string[] lastNamesArr = { "Azulay", "Edri", "Cohen", "Levi", "Biton", "Fridman", "Hadad", "Gabai", "Catz", "Zilberstein", "Malka", "Avraham", "Cherner", "Ben-David", "Orgad", "Ichler", "Alush", "Amsalem", "Papel", "Rapaport", "Buchner", "Bibass", "Bechman", "Block", "Blich", "Ben-shabat", "Braun", "Malool", "Dagan", "Glick", "Hod" };
        string[] citiesArr = { "Ben-Yehuda", "Elyakim", "Jerusalem", "Yokneam", "Raanana", "Tel-Aviv", "Beer-Sheva", "Eilat", "Avivim", "Netanya", "Tzfat", "Ashdod", "Heifa", "Afula", "Bet-Shean", "Ofakim", "Yeruham", "Hadera", "Dimona", "Bet-Shemesh", "Modiin", "Bat-Yam", "Yavne" };
        string[] streetArr = { "Hashaked", "Hagefen", "Hazait", "Hertzel", "Ben-Guryon", "Bialik", "Migdal", "Hateena", "Eli-Cohen", "Hardufim", "Rambam", "Hertzog", "Berner" };

        int myID;
        string? myCustomerName, myCustomerEmail, myCustomerAdress, myCusFirstName, myCusLastName;
        DateTime? myOrderDate, myShipDate, myDeliveryDate;

        for (int i = 0; i < 40; i++)
        {
            myID = Config.NextOrderNumber;
            myCusFirstName = namesArr[rnd.Next(namesArr.Length)];
            myCusLastName = lastNamesArr[rnd.Next(lastNamesArr.Length)];
            myCustomerName = myCusFirstName + " " + myCusLastName;
            myCustomerEmail = myCusFirstName + myCusLastName + "@gmail.com";
            myCustomerAdress = (streetArr[rnd.Next(streetArr.Length)]) + " " + rnd.Next(200) + ", " + citiesArr[rnd.Next(citiesArr.Length)];
            myOrderDate = DateTime.Now.AddDays(-(rnd.Next(14,31)));
            myShipDate = null;
            if (i > 25)
                myShipDate = DateTime.Now.AddDays(-(rnd.Next(7, 14)))- new TimeSpan(rnd.NextInt64(40000000000000));
            myDeliveryDate = null;
            if (i > 31)
                myDeliveryDate = DateTime.Now.AddDays(-(rnd.Next(1, 7))) - new TimeSpan(rnd.NextInt64(6000000000));
            //myOrderDate = DateTime.Now - new TimeSpan(rnd.n());
            //myShipDate = null;
            //if (i >25)
            //    myShipDate = DateTime.Now - new TimeSpan(rnd.NextInt64(10L * 1000L * 3600L * 24L * 50L));
            //myDeliveryDate = null;
            //if (i > 31)
            //    myDeliveryDate = DateTime.Now - new TimeSpan(rnd.NextInt64(10L * 1000L * 3600L * 24L * 20L));

            Orders.Add(
            new Order
            {
                IsDeleted=false,
                ID= myID,
                CustomerName= myCustomerName,
                CustomerEmail= myCustomerEmail,
                CustomerAddress= myCustomerAdress,
                OrderDate= myOrderDate,
                ShipDate= myShipDate,
                DeliveryDate= myDeliveryDate
            });
        }
    }
    private void createProducts()  //יצירת מוצרים
    {
        string[] productnamesArr = { "Kitchen rug", "Sea view picture", "Table cloth", "Pine wood youth bed", "Children's bed", "One and a half beds", "Double bed", "White kitchen cabinet", "Wooden chest of drawers", "Iron dresser", "Kitchen chair", "Room mirror", "Body mirror", "Duck bath rug", "Large white fridge", "Small white fridge", "Black oven", "White kettle", "Black microwave", "toaster", "large stainless steel pot", "large glass pot", "small glass pot", "medium glass pot", "medium stainless steel pot", "small stainless steel pot", "green leather sofa", "microwave White", "powerful red microwave", "blue fabric sofa", "glass kettle", "vintage kettle", "white lamb", "Yellow lamb" };
        Products.Add(
        new Product
        {
            IsDeleted = false,
            ID = Config.NextProductNumber,
            Name = "Kitchen rug",
            Price = 60,
            Category = Category.Kitchen,
            InStock = 3,
            Path= "/image/Kitchen rug.jpg"
        });

        Products.Add(
        new Product
        {
            IsDeleted = false,
            ID = Config.NextProductNumber,
            Name = "Black Soft Luxury Bath Rug",
            Price = 40,
            Category = Category.Bathroom,
            InStock = 198,
            Path = "/image/Black Soft Luxury Bath Rug.jpg"
        });
        Products.Add(
        new Product
        {
            IsDeleted = false,
            ID = Config.NextProductNumber,
            Name = "Astoria Stoneware Holder",
            Price = 29,
            Category = Category.Kitchen,
            InStock = 45,
            Path = "/image/Astoria Stoneware Holder.jpg"
        });
        Products.Add(
        new Product
        {
            IsDeleted = false,
            ID = Config.NextProductNumber,
            Name = "White Bunk Bed With Stairs and Slide",
            Price = 1999,
            Category = Category.Bedroom,
            InStock = 30,
            Path = "/image/White Bunk Bed With Stairs and Slide.jpg"

        });

        Products.Add(
        new Product
        {
            IsDeleted = false,
            ID = Config.NextProductNumber,
            Name = "White Fluted Ceramic Indoor-outdoor-windowsill Planter",
            Price = 99,
            Category = Category.Garden,
            InStock = 44,
            Path = "/image/White Fluted Ceramic Indoor-outdoor-windowsill Planter.jpg"
        });

        Products.Add(
        new Product
        {
            IsDeleted = false,
            ID = Config.NextProductNumber,
            Name = "3-Pieces Green Sofa",
            Price = 3347,
            Category = Category.Living_room,
            InStock = 15,
            Path = "/image/3-Pieces Green Sofa.jpg"
        });
        //till here xml
        Products.Add(
        new Product
        {
            IsDeleted = false,
            ID = Config.NextProductNumber,
            Name = "Mid-Century Heathered Wool Rug 8'x 10'",
            Price = 539.10,
            Category = Category.Living_room,
            InStock = 64,
            Path = "/image/Mid-Century Heathered Wool Rug.jpg"
        });

        Products.Add(
        new Product
        {
            IsDeleted = false,
            ID = Config.NextProductNumber,
            Name = "Thin-Wood-floor Mirror 30W x 72H",
            Price = 279.99,
            Category = Category.Bedroom,
            InStock = 104,
            Path = "/image/Thin-Wood-floor Mirror.jpg"
        });
        Products.Add(
        new Product
        {
            IsDeleted = false,
            ID = Config.NextProductNumber,
            Name = "Round Outdoor Market Umbrella",
            Price = 503.10,
            Category = Category.Garden,
            InStock = 2,
            Path = "/image/Round Outdoor Market Umbrella.jpg"
        });
        Products.Add(
        new Product
        {
            IsDeleted = false,
            ID = Config.NextProductNumber,
            Name = "Dark-Bronze Mid-Century-Contour-Bathroom Towel Hook",
            Price = 35,
            Category = Category.Bathroom,
            InStock = 82,
            Path = "/image/Dark-Bronze Mid-Century-Contour-Bathroom Hardware.jpg"
        });
        //////newwww
        Products.Add(
        new Product
        {
            IsDeleted = false,
            ID = Config.NextProductNumber,
            Name = "3-piece dark gray sofa",
            Price = 2398.40,
            Category = Category.Living_room,
            InStock = 82,
            Path = "/image/3-piece dark gray sofa.jpg"
        });

        Products.Add(
        new Product
        {
            IsDeleted = false,
            ID = Config.NextProductNumber,
            Name = "Mid-Century Single Bathroom Vanity",
            Price = 800,
            Category = Category.Bathroom,
            InStock = 102,
            Path = "/image/Mid-Century Single Bathroom Vanity.jpg"
        });

        Products.Add(
        new Product
        {
            IsDeleted = false,
            ID = Config.NextProductNumber,
            Name = "Fluted Glass Indoor or Outdoor Sconce",
            Price = 179,
            Category = Category.Bathroom,
            InStock = 102,
            Path = "/image/Fluted Glass Indoor or Outdoor Sconce.jpg"
        });

        Products.Add(
        new Product
        {
            IsDeleted = false,
            ID = Config.NextProductNumber,
            Name = "Mid-Century Bath Ladder Storage",
            Price = 359.10,
            Category = Category.Bathroom,
            InStock = 102,
            Path = "/image/Mid-Century Bath Ladder Storage.jpg"
        });

        Products.Add(
        new Product
        {
            IsDeleted = false,
            ID = Config.NextProductNumber,
            Name = "Utility Stoneware Kitchen Canisters",
            Price = 69.90,
            Category = Category.Kitchen,
            InStock = 102,
            Path = "/image/Utility Stoneware Kitchen Canisters.jpg"
        });

        Products.Add(
        new Product
        {
            IsDeleted = false,
            ID = Config.NextProductNumber,
            Name = "Modern Kitchen Drying Rack",
            Price = 35,
            Category = Category.Kitchen,
            InStock = 150,
            Path = "/image/Modern Kitchen Drying Rack.jpg"
        });

        Products.Add(
        new Product
        {
            IsDeleted = false,
            ID = Config.NextProductNumber,
            Name = "Kaloh Storage Carts",
            Price = 250,
            Category = Category.Kitchen,
            InStock = 150,
            Path = "/image/Kaloh Storage Carts.jpg"
        });

        Products.Add(
        new Product
        {
            IsDeleted = false,
            ID = Config.NextProductNumber,
            Name = "Huron Outdoor Lounge Chair",
            Price = 1298,
            Category = Category.Garden,
            InStock = 150,
            Path = "/image/Huron Outdoor Lounge Chair.jpg"
        });

        Products.Add(
        new Product
        {
            IsDeleted = false,
            ID = Config.NextProductNumber,
            Name = "Coastal Outdoor Swivel Chair",
            Price = 1079.10,
            Category = Category.Garden,
            InStock = 150,
            Path = "/image/Coastal Outdoor Swivel Chair.jpg"
        });

        Products.Add(
       new Product
       {
           IsDeleted = false,
           ID = Config.NextProductNumber,
           Name = "Concrete Indoor or Outdoor Pedestal Round Dining Table",
           Price = 599,
           Category = Category.Garden,
           InStock = 150,
           Path = "/image/Concrete Indoor or Outdoor Pedestal Round Dining Table.jpg"
       });

      Products.Add(
      new Product
      {
          IsDeleted = false,
          ID = Config.NextProductNumber,
          Name = "Porto Pedestal Concrete Outdoor Dining Table",
          Price = 999,
          Category = Category.Garden,
          InStock = 150,
          Path = "/image/Porto Pedestal Concrete Outdoor Dining Table.jpg"
      });

        Products.Add(
     new Product
     {
         IsDeleted = false,
         ID = Config.NextProductNumber,
         Name = "Porto Pedestal Concrete Outdoor Dining Table",
         Price = 180,
         Category = Category.Bedroom,
         InStock = 150,
         Path = "/image/Lakeside Matelasse Duvet Cover & Shams.jpg"
     });

        Products.Add(
   new Product
   {
       IsDeleted = false,
       ID = Config.NextProductNumber,
       Name = "Porto Pedestal Concrete Outdoor Dining Table",
       Price = 125,
       Category = Category.Bedroom,
       InStock = 150,
       Path = "/image/Crinkle Velvet Duvet Cover & Shams.jpg"
   });


    }
    private void createOrderItems()  // יצירת פריטי-הזמנה
    {
        for(int j=0; j < 40; j++)
        {
            Product? product = Products[rnd.Next(15)];
            int NumOfItemsToThisOrder = rnd.Next(1,5);
            Order? order = Orders[j];
            for (int k=0; k < NumOfItemsToThisOrder; k++)
            {
                int amount = rnd.Next(1, 5);
            OrderItems.Add(
                new OrderItem
                {
                    IsDeleted = false,
                    ID = Config.NextOrderItemNumber,
                    ProductID = product?.ID ?? 0,
                    OrderID = order?.ID ?? 0,
                    Price = product?.Price ?? 0,
                    Amount = amount,
                    TotalItem =amount*(product?.Price ?? 0),
                    Name = product?.Name,
                    Path = product?.Path
                });

                product = Products[rnd.Next(10)];
            }           
        }
    }
    

    
}





