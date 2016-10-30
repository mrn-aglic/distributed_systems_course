namespace AkkaReadWriteConsole.Messages
{
    class Vrijednost
    {
        public int Broj { get; private set; }

        public Vrijednost(int broj)
        {
            Broj = broj;
        }
    }
}
