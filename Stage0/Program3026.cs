// See https://aka.ms/new-console-template for more information
using System;
//shish
namespace Targil0
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Welcome3026();
            Welcome5570();
            Console.ReadKey();
        }
        static partial void Welcome5570(); 

        private static void Welcome3026()
        {
            Console.Write("Enter your name: ");
            string input = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", input);
        }
    }
}