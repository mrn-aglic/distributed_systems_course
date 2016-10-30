using Akka.Actor;
using AkkaReadWriteConsole.Messages;
using System;

namespace AkkaReadWriteConsole
{
    class ReaderActor : ReceiveActor
    {
        private IActorRef _writerActor;

        // Readeru šaljemo IActorRef actora koji će ispisivati na ekran.
        public ReaderActor(IActorRef writerActor)
        {
            _writerActor = writerActor;

            // Kada primi poruku tipa ConsoleInput, pozvat će metodu Inputs
            Receive<ConsoleInput>(x => Inputs());
        }

        private void Inputs()
        {
            try
            {
                Console.WriteLine("Enter a number:> ");
                string x = Console.ReadLine();

                _writerActor.Tell(new Vrijednost(int.Parse(x)));
            }
            catch
            {
                Console.WriteLine("Neispravan unos");
            }

            // Actor može sebi poslati poruku
            Self.Tell(new ConsoleInput());
        }
    }
}
