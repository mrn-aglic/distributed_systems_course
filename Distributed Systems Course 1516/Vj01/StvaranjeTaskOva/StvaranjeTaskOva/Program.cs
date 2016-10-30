using System;
using System.Threading;

// Task-ovi se nalaze u ovome namespaceu:
using System.Threading.Tasks;

namespace StvaranjeTaskOva
{
    class Program
    {
        static void Main(string[] args)
        {
            for(int i = 0; i < 10; i++)
            {
                // Instanciramo task pri čemu mu šaljemo kao argument metodu koju mora izvršiti
                Task t = new Task(WriteInfo);

                // Pokrećemo stvoreni task 
                t.Start();
            }

            Console.WriteLine("[Main] ID: " + Thread.CurrentThread.ManagedThreadId + " Milliseconds: " + DateTime.Now.Millisecond);

            Console.ReadLine();
        }

        private static void WriteInfo()
        {
            Console.WriteLine("ID: " + Thread.CurrentThread.ManagedThreadId + " Milliseconds: " + DateTime.Now.Millisecond);
        }
    }
}
