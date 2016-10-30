using Akka.Actor;
using System;
using System.Threading;
using AkkaHelloWorld.Messages;

namespace AkkaHelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var actorSystem = ActorSystem.Create("AkkaHelloWorld"))
            {
                var greeter = actorSystem.ActorOf<HelloWorldActor>();

                Console.WriteLine("Running on thread: " + Thread.CurrentThread.ManagedThreadId);

                greeter.Tell(new Greet("Marin"));

                Console.ReadLine();
            }
        }
    }
}
