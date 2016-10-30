using Akka.Actor;
using Akka.Cluster;
using Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class RequesterActor : ReceiveActor
    {
        List<IActorRef> knownNodes;

        public RequesterActor()
        {
            knownNodes = new List<IActorRef>();
            
            Receive<Deposit>(x => !knownNodes.Any(), x => Sender.Tell(new DepositFailed("No known nodes available")));
            Receive<Deposit>(x => HandleDeposit(x));
            Receive<Registered>(x => HandleRegistered(x));
            Receive<Terminated>(x => HandleTerminated(x));
        }

        private void HandleDeposit(Deposit x)
        {
            knownNodes.First().Forward(x);
        }

        private void HandleRegistered(Registered x)
        {
            Context.Watch(Sender);
            knownNodes.Add(Sender);
        }

        private void HandleTerminated(Terminated x)
        {
            knownNodes.Remove(x.ActorRef);
        }
    }
}
