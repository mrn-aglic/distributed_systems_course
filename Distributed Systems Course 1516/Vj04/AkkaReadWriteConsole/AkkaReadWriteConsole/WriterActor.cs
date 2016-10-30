using System;
using System.Collections.Generic;
using System.Linq;
using Akka.Actor;
using AkkaReadWriteConsole.Messages;

namespace AkkaReadWriteConsole
{
    class WriterActor : ReceiveActor
    {
        public WriterActor()
        {
            Receive<Vrijednost>(x => IspitajIIsprintaj(x.Broj));
        }

        private void IspitajIIsprintaj(int x)
        {
            if(x % 2 == 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Broj je paran!");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Broj je neparan");
            }
        }
    }
}
