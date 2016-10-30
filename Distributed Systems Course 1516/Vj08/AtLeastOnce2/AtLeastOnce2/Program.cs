using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtLeastOnce
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var system = ActorSystem.Create("AtLeastOnceSystem"))
            {
                Props props1 = Props.Create(() => new ProviderActor());

                var receiver = system.ActorOf(props1);

                Props props2 = Props.Create(() => new ReaderActor(receiver));

                var reader = system.ActorOf(props2);

                reader.Tell(new Send(""));

                system.WhenTerminated.Wait();
            }
        }
    }
}
