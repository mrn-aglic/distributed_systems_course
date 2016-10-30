using System;

namespace Delegati
{
    class Program
    {
        delegate double MyDelegate(bool b, double d);
        delegate void NestoSaStrane(string msg);

        static private double SquareOrExp(bool b, double d)
        {
            return b ? Math.Pow(d, 2) : Math.Exp(d);
        }

        static private void Ispis(string message)
        {
            Console.WriteLine(message);
        }

        static void Main(string[] args)
        {
            NestoSaStrane ispis = Ispis;
            MyDelegate mojaMetoda = SquareOrExp;

            ispis("Hello world");

            // Pozivamo i ispisujemo rezultat metode spremljene u varijablu mojaMetoda
            Console.WriteLine(mojaMetoda(true, 2));
            // Pozivamo i ispisujemo rezultat metode spremljene u varijablu mojaMetoda
            Console.WriteLine(mojaMetoda(false, 5));

            // Koristimo ugrađeni delegat Action za akcije - akcija je bilo koja metoda kojoj je povratna vrijednost "void"
            Action<string> akcija1 = Ispis;

            akcija1("Hello world");

            // Koristimo ugrađeni delegat Func za funkcije - funkcija je bilo koja metoda kojoj je povratna vrijednost različita od "void"
            Func<bool, double, double> funkcija = SquareOrExp;

            Console.WriteLine(funkcija(true, 2));
            
            Console.ReadLine();
        }
    }
}
