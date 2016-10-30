using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.Cluster;
using Akka.Event;

namespace AkkaClusterExample
{
    class SampleClusterListener : ReceiveActor
    {
        private Cluster Cluster = Cluster.Get(Context.System);
        private ILoggingAdapter Log = Context.GetLogger();

        protected override void PreStart()
        {
            Cluster.Subscribe(Self, ClusterEvent.SubscriptionInitialStateMode.InitialStateAsEvents, new []{ typeof(ClusterEvent.UnreachableMember), typeof(ClusterEvent.IMemberEvent) });
        }

        protected override void PostStop()
        {
            Cluster.Unsubscribe(Self);
        }

        public SampleClusterListener()
        {
            Receive<ClusterEvent.MemberUp>(x => HandleMemberUp(x));
            Receive<ClusterEvent.UnreachableMember>(x => HandleUnreachable(x));
            Receive<ClusterEvent.MemberRemoved>(x => HandleMemberRemoved(x));
        }

        private void HandleMemberUp(ClusterEvent.MemberUp up)
        {
            Log.Info("Member is up: {0}", up.Member);   
        }

        private void HandleUnreachable(ClusterEvent.UnreachableMember unreachable)
        {
            Log.Info("Member detected as unreachable: {0}", unreachable.Member);
        }

        private void HandleMemberRemoved(ClusterEvent.MemberRemoved removed)
        {
            Log.Info("Member is removed: {0}", removed.Member);
        }

        protected override void Unhandled(object message)
        {
            Log.Warning("Got unhandled message: {0}", message);

            base.Unhandled(message);
        }
    }
}
