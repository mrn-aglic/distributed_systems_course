using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtMosteOnce
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var system = ActorSystem.Create("AtMostOnceSystem"))
            {
                Props props1 = Props.Create(() => new ReceiverActor());

                var receiver = system.ActorOf(props1);

                Props props2 = Props.Create(() => new RequesterActor(100, receiver));

                system.ActorOf(props2);

                system.WhenTerminated.Wait();
            }
        }
    }
}
