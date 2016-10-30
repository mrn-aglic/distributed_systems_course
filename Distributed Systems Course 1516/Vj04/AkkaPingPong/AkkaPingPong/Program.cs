using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkkaPingPong
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var system = ActorSystem.Create("PingPong"))
            {
                var pongActor = system.ActorOf(Props.Create(() => new PongActor()));

                var pingActor = system.ActorOf(Props.Create(() => new PingActor(pongActor)));

                pingActor.Tell(new Ping());

                Console.ReadLine();

                system.Terminate().Wait();
            }
        }
    }
}
