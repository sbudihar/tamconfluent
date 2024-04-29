using System;

namespace consumer_kafka_tam
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
