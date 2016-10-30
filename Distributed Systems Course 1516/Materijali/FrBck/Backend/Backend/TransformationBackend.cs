using Akka.Actor;
using Akka.Cluster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Messages;

namespace Backend
{
    class TransformationBackend : ReceiveActor
    {
        private Cluster cluster = Cluster.Get(Context.System);

        // subscribe to cluster changes, MemberUp
        // re-subscribe when restart
        protected override void PreStart()
        {
            cluster.Subscribe(Self, new[] { typeof(ClusterEvent.MemberUp) });
        }

        protected override void PostStop()
        {
            cluster.Unsubscribe(Self);
        }

        public TransformationBackend()
        {
            Receive<TransformationJob>(x => Sender.Tell(new TransformationResult(x.Text.ToUpper())));
            Receive<ClusterEvent.CurrentClusterState>
                (x => x.Members.Where(y => y.Status == MemberStatus.Up).ToList().ForEach(Register));
            Receive<ClusterEvent.MemberUp>(x => Register(x.Member));
        }

        private void Register(Member member)
        {
            //Console.Clear();
            Console.WriteLine(member.Address);

            if (member.HasRole("frontend"))
            {
                var path = new RootActorPath(member.Address).ToString() + "/user/frontend";

                Context.ActorSelection(path).Tell(new BackendRegistration());
            }
        }
    }
}
