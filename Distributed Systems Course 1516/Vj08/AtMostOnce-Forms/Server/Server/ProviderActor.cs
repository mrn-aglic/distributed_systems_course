using Akka.Actor;
using Akka.Cluster;
using Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class ProviderActor : ReceiveActor
    {
        private Cluster cluster = Cluster.Get(Context.System);
        private IActorRef _child;

        protected override void PreStart()
        {
            cluster.Subscribe(Self, new[] { typeof(ClusterEvent.MemberUp) });
        }

        protected override void PostStop()
        {
            cluster.Unsubscribe(Self);
        }

        public ProviderActor()
        {
            _child = Context.ActorOf(Props.Create(() => new WorkerActor()));

            Receive<Deposit>(x => HandleDeposit(x));
            Receive<ClusterEvent.MemberUp>(x => HandleUp(x));
        }

        private void HandleDeposit(Deposit x)
        {
            _child.Forward(x);
        }

        private void HandleUp(ClusterEvent.MemberUp x)
        {
            Console.WriteLine("[UP]: " + x.Member);

            if(x.Member.HasRole("client"))
            {
                var path = new RootActorPath(x.Member.Address).ToString() + "/user/client";

                Context.ActorSelection(path).Tell(new Registered());
            }
            else
            {
                Console.WriteLine("[UP Warning]: Don't know what to do with member " + x.Member.Address);
            }
        }
    }
}
