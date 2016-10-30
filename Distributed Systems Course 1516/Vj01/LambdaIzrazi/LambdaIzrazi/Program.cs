using System;

namespace LambdaIzrazi
{
    class Program
    {
        static void Main(string[] args)
        {
            Action<string> a1 = (string x) => Console.WriteLine(x);
            Action<string> a2 = x => Console.WriteLine(x);

            Action prazno = () => { };

            Action<string> akcijaSViseLinija = x =>
            {
                x = "Pozdrav " + x;
                Console.WriteLine(x);
            };

            Func<int> f1 = () => 42;
            Func<int, int> f2 = x => x + 42;
            Func<int, int, int> f3 = (x, y) => x + y;

            // Pozivi definiranih akcija
            a1("Hello world");
            a2("Hello world");

            prazno();

            akcijaSViseLinija("Marin");

            // Pozivi definiranih funkcija
            Console.WriteLine(f1());
            Console.WriteLine(f2(5));
            Console.WriteLine(f3(5, 10));

            // Slanje funkcije u metodu kao parametra
            Console.WriteLine(PozoviFunkciju(f2));

            Console.ReadLine();
        }

        private static int PozoviFunkciju(Func<int, int> f)
        {
            int x = -345;

            return f(x);
        }
    }
}
