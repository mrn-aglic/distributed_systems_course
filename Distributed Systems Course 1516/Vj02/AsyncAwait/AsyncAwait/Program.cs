using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwait
{
    class Program
    {
        static void Main(string[] args)
        {
            Test();

            Test2();

            Console.ReadLine();
        }
        
        private static async void Test()
        {
            Console.WriteLine("Na niti: " + Thread.CurrentThread.ManagedThreadId);

            await Task.Delay(1000);

            Console.WriteLine("Na niti: " + Thread.CurrentThread.ManagedThreadId);
        }

        private static void Test2()
        {
            ZbrojiNekad(7, 5).ContinueWith(x => Console.WriteLine("Rezultat: " + x.Result));
        }

        private static async Task<int> ZbrojiNekad(int a, int b)
        {
            int x = await Task.Factory.StartNew(() => a);
            int y = await Task.Factory.StartNew(() => b);
            
            return x + y;
        }
    }
}
