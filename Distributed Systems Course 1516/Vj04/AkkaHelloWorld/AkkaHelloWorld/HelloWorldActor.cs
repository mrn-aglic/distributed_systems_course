using Akka.Actor;
using AkkaHelloWorld.Messages;
using System;
using System.Threading;

namespace AkkaHelloWorld
{
    class HelloWorldActor : ReceiveActor
    {
        public HelloWorldActor()
        {
            Receive<Greet>(s => Console.WriteLine("Hello {0} on thread: {1}", s.Who, Thread.CurrentThread.ManagedThreadId)); 
        }
    }
}
