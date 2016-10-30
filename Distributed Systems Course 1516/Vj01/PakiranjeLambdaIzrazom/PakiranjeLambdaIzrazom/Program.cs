using System;
using System.Threading;
using System.Threading.Tasks;

namespace PakiranjeLambdaIzrazom
{
    class Primjer
    {
        private int _a;
        private int _b;

        public int A
        {
            get { return _a; }
            set { _a = value; }
        }

        public int B
        {
            get { return _b; }
            set { _b = value; }
        }
    }

    class Program
    {
        private static void PosaoKojiDugoTraje()
        {
            Thread.Sleep(1000);
        }

        private static int Zbroji(Primjer primjer)
        {
            Console.WriteLine("Primljeno: " + primjer.A + " + " + primjer.B);

            PosaoKojiDugoTraje();

            Console.WriteLine("Zbraja se: " + primjer.A + " + " + primjer.B);

            return primjer.A + primjer.B;
        }

        private static void Zbroji(int x)
        {
            Console.WriteLine("For petlja, prolaz: " + x);
        }

        static void Main(string[] args)
        {
            int a = 5;
            int b = 3;

            Primjer primjer = new Primjer();

            primjer.A = a;
            primjer.B = b;

            Task<int> t1 = Task.Factory.StartNew(() => Zbroji(primjer));

            Thread.Sleep(100);

            primjer.A = primjer.A + 1;

            Console.WriteLine(t1.Result);

            // Pogledajmo jednu čestu pogrešku koja nastaje korištenjem ovog pakiranja uz pomoć funkcija
            for(int i = 0; i < 10; i++)
            {
                Task t = Task.Factory.StartNew(() => Zbroji(i));
            }

            Console.WriteLine("Malo drugačije");

            for (int i = 0; i < 10; i++)
            {
                Task t = Task.Factory.StartNew(() => Console.WriteLine(i));
            }

            Console.ReadLine();
        }
    }
}
