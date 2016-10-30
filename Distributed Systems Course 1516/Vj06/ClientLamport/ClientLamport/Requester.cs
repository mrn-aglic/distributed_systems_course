using Akka.Actor;
using Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientLamport
{
    class Requester : ReceiveActor
    {
        private int _lamportClock = 0;
        private string _adresa = "akka.tcp://LamportClock@localhost:12000/user/Distributor";

        ActorSelection _actorSelection;

        private IActorRef _privateWorker;

        public Requester()
        {
            _actorSelection = Context.ActorSelection(_adresa);

            Receive<Start>(x => SendRegister());
            Receive<Deny>(x => HandleDeny());
            Receive<Confirm>(x => HandleConfirm(x));
            Receive<Add>(x => ProcessAdd(x));
        }

        private void HandleDeny()
        {

        }

        private void ProcessAdd(Add x)
        {
            _lamportClock++;

            _privateWorker.Tell(new Add(x.A, x.B, new TimeStamp(_lamportClock)));
        }

        private void HandleConfirm(Confirm x)
        {
            _lamportClock = Math.Max(x.Time.Clock, _lamportClock) + 1;

            _privateWorker = x.Actor;

            _lamportClock++;

            Context.System.Scheduler.ScheduleTellOnce(TimeSpan.FromSeconds(2), Self, new Add(2, 5, new TimeStamp(_lamportClock)), Self);
        }

        private void SendRegister()
        {
            _lamportClock++;

            _actorSelection.Tell(new Register("test", new TimeStamp(_lamportClock)));
        }
    }
}
