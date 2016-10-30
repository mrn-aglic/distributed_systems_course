using Akka.Actor;
using System;
using SharedMessages;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteHelloWorldClient
{
    class QuerierActor : ReceiveActor
    {
        private string _adresaServera = "akka.tcp://MyRemoteSystem@localhost:12000/user/replier";

        private ActorSelection _actorNaServeru;

        public QuerierActor()
        {
            _actorNaServeru = Context.ActorSelection(_adresaServera);

            Receive<Send>(x => Send());
            Receive<Answer>(x => ProcessAnswer(x));
        }

        private void Send()
        {
            Console.WriteLine("Unesite tekst");
            string text = Console.ReadLine();

            var query = new Query(text);

            _actorNaServeru.Tell(query);
        }

        private void ProcessAnswer(Answer ans)
        {
            Console.WriteLine("Answer from server " + ans.Text);

            Context.System.Scheduler.ScheduleTellOnce(TimeSpan.FromSeconds(1), Self, new Send(), Self);
        }
    }
}
