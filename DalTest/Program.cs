using System;

class Program
{

    public static void PrintMenu()
    {
        Console.WriteLine("@ enter 1 to add" +
                          "enter 2 to delete" +
                          "enter 3 to update" +
                          "enter 4 to get by id" +
                          "enter 5 to print all of the objects");
    }
    public static int GetAnswer()
    {
        int ans = int.Parse(Console.ReadLine());
        if (ans != 1 && ans != 2 && ans != 3 && ans != 4 && ans != 5)
            throw new Exception("input error");
        return ans;
    }

    public static int GetAnswerForOrderItem()
    {
        Console.WriteLine("enter 6 to get by order number and ID" +
                           "enter 7 to print all of the items of specific order");
        int ans = int.Parse(Console.ReadLine());
        if (ans != 1 && ans != 2 && ans != 3 && ans != 4 && ans != 5&&ans!=6&&ans!=7)
            throw new Exception("input error");
        return ans;
    }
    static void Main(string[] args)
    {
        Console.WriteLine("@ enter O to check the orders" +
                          "enter OI to check the order items" +
                          "enter P to check the products" +
                          "enter any different key to exit");
        int ans = 0;
        string input = Console.ReadLine();
        while (input == "O" || input == "OI" || input == "P")
        {
        switch (input) 
        {
                case "O":
                    PrintMenu();
                    ans = GetAnswer();
                    switch(ans)
                    {
                        case 1:
                            Console.WriteLine("enter order Id:");
                            int id=

                    }
                    
                    break;
                case "OI":
                    PrintMenu();
                    ans=GetAnswerForOrderItem();
                    

                    break;
                case "P":
                    PrintMenu();
                    ans = GetAnswer();

                    break;

        }

        Console.WriteLine("@ Enter your next choice/");
        input = Console.ReadLine();
        }
    }
}