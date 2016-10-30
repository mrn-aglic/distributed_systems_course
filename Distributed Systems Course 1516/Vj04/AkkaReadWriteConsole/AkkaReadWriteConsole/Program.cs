using Akka.Actor;
using AkkaReadWriteConsole.Messages;
using System;

namespace AkkaReadWriteConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            using(var actorSystem = ActorSystem.Create("Sustav"))
            {
                // Kreiramo props te ga pošaljemo u ActorOf metodu da kreiramo novog actora
                IActorRef writer = actorSystem.ActorOf(Props.Create(() => new WriterActor()));
                // Kreiramo reader-a isto kao i writera, samo što šaljemo referencu na writer-a prilikom kreiranja
                IActorRef reader = actorSystem.ActorOf(Props.Create(() => new ReaderActor(writer)));

                // Započinjemo izvršavanje prvog actora
                reader.Tell(new ConsoleInput());
                actorSystem.WhenTerminated.Wait();
            }
        }
    }
}
