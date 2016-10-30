using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RestartActor
{
    class Messages
    {
        public class Start { }
        public class Unisti { }
        public class DajPodatke { }
    }

    class TestActor : ReceiveActor
    {
        public DateTime dateTime = DateTime.Now;

        public TestActor()
        {
            Receive<Messages.Unisti>(x => 
            {
                throw new Exception("Crash");
                return;
            });

            Receive<Messages.DajPodatke>(x => Sender.Tell(dateTime));
        }
    }

    class OtacActor : ReceiveActor
    {
        public OtacActor()
        {
            Receive<Messages.Start>(x =>
            {
                var sin = Context.ActorOf<TestActor>();

                sin.Tell(new Messages.DajPodatke());
                Thread.Sleep(1000);

                sin.Tell(new Messages.Unisti());
                Thread.Sleep(1000);

                sin.Tell(new Messages.DajPodatke());
                Thread.Sleep(1000);

            });

            Receive<DateTime>(x => Console.WriteLine(x));
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            using (var system = ActorSystem.Create("RestartSustav"))
            {
                var actor = system.ActorOf(Props.Create(() => new OtacActor()));
                

                actor.Tell(new Messages.Start());

                system.WhenTerminated.Wait();
            }
        }
    }
}
