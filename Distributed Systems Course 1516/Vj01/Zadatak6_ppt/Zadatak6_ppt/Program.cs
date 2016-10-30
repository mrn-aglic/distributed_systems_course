using System;
using System.Threading.Tasks;
using System.Threading;

namespace Zadatak6_ppt
{
    class Program
    {
        private static bool kreni;

        static void Main(string[] args)
        {
            Task A = Task.Factory.StartNew(SimEvent);
            //Task A = Task.Factory.StartNew(SimEvent2);
            Task B = Task.Factory.StartNew(SimServer);

            Console.ReadLine();
        }

        static void SimEvent()
        {
            // 5000 milisekundi predstavlja period
            System.Timers.Timer timer = new System.Timers.Timer(5000);

            // Elapsed predstavlja event... Shvatite ga kao svojstvo u koje stavite
            // jednu ili vise funkcije i sve ce se pozvati kada se event podigne
            // U ovome slucaju event ce se podignuti nakon 5 sekundi
            // EVENT SPOMINJEMO SAMO ZATO STO JE U OVOME SLUCAJU LAKSE RIJESITI ZADATAK!
            timer.Elapsed += (sender, e) => { kreni = true; Console.WriteLine("Event: " + DateTime.Now); };

            timer.Start();
        }

        // Bez timera
        static void SimEvent2()
        {
            while(true)
            {
                Thread.Sleep(5000);

                kreni = true;

                Console.WriteLine("Event: " + DateTime.Now);
            }
        }

        static void SimServer()
        {
            while (true)
            {
                if (kreni)
                {
                    Task.Factory.StartNew(SimClient);
                    kreni = false;
                }
            }
        }

        static void SimClient()
        {
            Console.WriteLine("Spaja se task s id: " + Thread.CurrentThread.ManagedThreadId);

            Random rnd = new Random();

            for(int i = 0; i < 10; i++)
            {
                Console.WriteLine(Thread.CurrentThread.ManagedThreadId + " : " + rnd.Next(0, 10));
                Thread.Sleep(1000);
            }

            Console.WriteLine("Odjavljujem se " + Thread.CurrentThread.ManagedThreadId);
        }
    }
}
