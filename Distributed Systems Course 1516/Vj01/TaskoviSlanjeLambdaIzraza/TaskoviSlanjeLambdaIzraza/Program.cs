using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaskoviSlanjeLambdaIzraza
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
        static double Square(object n)
        {
            return Math.Pow((int)n, 2);
        }

        static double Square2(int n)
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

        static double Pow(int m, int n)
        {
            return Math.Pow(m, n);
        }

        static void Print(object msg)
        {
            Console.WriteLine(msg);
        }

        static void Main(string[] args)
        {
            string msg = "Hello world";

            // Pokrecemo task i saljemo msg kao objekt u poslanu metodu, 
            // ali u nekim slucajevima onda taj objekt moramo castati (pretvoriti) nazad u originalni tip podatka koji nam je potreban
            Task t1 = Task.Factory.StartNew(Print, msg);

            // Primjer sa slanjem broja kao objecta. U metodi Square moramo poslani object pretvoriti u int ponovno da bi ga mogli koristiti
            int broj = 4;
            Task<double> t2 = Task.Factory.StartNew(Square, broj);

            int potencija = 3;

            Info info = new Info(broj, potencija);
            Task<double> t3 = Task.Factory.StartNew(Pow, info);

            Console.WriteLine(t2.Result);
            Console.WriteLine(t3.Result);

            // Da bismo izbjegli pretvaranje parametara u object i nazad u originalni tip podatka, mozemo metodu
            // koju zelimo poslati u task zapakirati u novu funkciju koja ce sa sobom ponjeti sve informacije koje su potrebne
            // metodi da bi ista mogla obaviti svoj posao
            Task tt1 = Task.Factory.StartNew(() => Print(msg));

            // Uočite razlike između Square i Square2 - nema pretvaranja iz object u int te Square2 prima int
            Task<double> tt2 = Task.Factory.StartNew(() => Square2(broj));

            // Uo;ite da smo zamotali varijable broj i potencija u anonimnu funkciju skupa s metodom koju pozivamo
            Task<double> tt3 = Task.Factory.StartNew(() => Pow(broj, potencija));

            Console.WriteLine(tt2.Result);
            Console.WriteLine(tt3.Result);

            Console.ReadLine();
        }
    }
}
