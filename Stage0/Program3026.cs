// See https://aka.ms/new-console-template for more information
using System;

/// <summary>
/// Stage0
/// Shira Kahalany 325283026
/// Hallel Ishon 212455570
/// </summary>
namespace Targil0
{
    partial class Program
    {
        static void Main(string[] args)
        {
            //Welcome3026();
            //Welcome5570();
           // Console.ReadKey();
           Random random = new Random();
            List<int> list = new List<int>();
            for (int i = 0; i < 10; i++)
                list.Add(i);
            Console.WriteLine(list[random.Next(10)]);
            int index = list.IndexOf(0);
            Console.WriteLine(index);
            //Console.WriteLine(list[9]);

        }
        static partial void Welcome5570(); 
        private static void Welcome3026()
        {
            Console.Write("Enter your name: ");
            string input = Console.ReadLine()!;
            Console.WriteLine("{0}, welcome to my first console application", input);
        }
    }
}