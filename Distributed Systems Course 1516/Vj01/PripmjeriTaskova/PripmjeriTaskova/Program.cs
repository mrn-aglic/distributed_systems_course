using System;
using System.Threading.Tasks;

namespace PripmjeriTaskova
{
    class Program
    {
        static void Main(string[] args)
        {
            // task koji nema rezultat
            Task t1 = Task.Factory.StartNew(WriteMsg);
            // task kojemu ce rezultati biti tipa int
            Task<int> t2 = Task.Factory.StartNew<int>(Get42);
            // task kojemu ce rezultat biti tipa string
            Task<string> t3 = Task.Factory.StartNew<string>(GetMsg);

            Console.WriteLine("t1 is completed: " + t1.Status);

            Console.WriteLine("t2 value: " + t2.Result);
            Console.WriteLine("t3 value: " + t3.Result);

            Console.ReadLine();
        }

        private static void WriteMsg()
        {
            Console.WriteLine("Hello world");
        }

        private static int Get42()
        {
            return 42;
        }

        private static string GetMsg()
        {
            return "Hello world";
        }
    }
}
