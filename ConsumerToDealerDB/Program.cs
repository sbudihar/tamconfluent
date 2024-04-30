using System;

namespace ConsumerToDealerDB
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Test");

            new ConfluentListener().Listen();

            Console.ReadKey();
        }


    }
}
