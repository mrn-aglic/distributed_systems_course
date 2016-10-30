using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimjerTaskMetodaSParametrima
{
    class Info
    {
        private int _broj;

        public int Broj
        {
            get { return _broj; }
            set { _broj = value; }
        }

        private int _potencija;

        public int Potencija
        {
            get { return _potencija; }
            set { _potencija = value; }
        }

        public Info(int broj, int potencija)
        {
            _broj = broj;
            _potencija = potencija;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string msg = "Hello world";

            Task t1 = Task.Factory.StartNew(Print, msg);

            int broj = 4;
            Task<double> t2 = Task.Factory.StartNew(Square, broj);

            int potencija = 3;

            Info info = new Info(broj, potencija);
            Task<double> t3 = Task.Factory.StartNew(Pow, info);

            Console.WriteLine(t2.Result);
            Console.WriteLine(t3.Result);

            Console.ReadLine();
        }

        static double Square(object n)
        {
            return Math.Pow((int)n, 2);
        }

        static double Pow(object info)
        {
            Info i = (Info)info;

            int m = i.Broj;
            int n = i.Potencija;

            return Math.Pow(m, n);
        }

        static void Print(object msg)
        {
            Console.WriteLine(msg);
        }
    }
}
