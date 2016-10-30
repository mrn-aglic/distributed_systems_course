using Akka.Actor;
using Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class WorkDistributor : ReceiveActor
    {
        private int LamportClock = 0;
        private Dictionary<string, IActorRef> _actors = new Dictionary<string, IActorRef>();

        public WorkDistributor()
        {
            Receive<Register>(x => ProcessRegister(x));
            Receive<SendClockValue>(x => ProcessClockValue(x));
        }

        private void ProcessClockValue(SendClockValue x)
        {
            LamportClock = Math.Max(x.Time.Clock, LamportClock) + 1;

            LamportClock++;

            Sender.Tell(new SendClockValue(new TimeStamp(LamportClock)));
        }

        private void ProcessRegister(Register x)
        {
            Console.WriteLine("[Register]: " + x.Name);

            LamportClock = Math.Max(x.Time.Clock, LamportClock) + 1;

            if (_actors.ContainsKey(x.Name))
            {
                LamportClock++;
                Sender.Tell(new Deny("Name already taken", new TimeStamp(LamportClock)));
            }
            else
            {
                Props props = Props.Create(() => new WorkerActor());

                var actor = Context.ActorOf(props, x.Name);

                // Interni dogadaj - mijenja stanje actora 
                LamportClock++;
                _actors.Add(x.Name, actor);

                LamportClock++;
                Sender.Tell(new Confirm(actor, new TimeStamp(LamportClock))); 
            }
        }
    }
}
