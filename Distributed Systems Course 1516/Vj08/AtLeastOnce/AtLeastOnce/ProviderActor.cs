using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtLeastOnce
{
    class ProviderActor : ReceiveActor
    {
        private IActorRef _child;

        // moramo zapamtiti sve postove za koje nismo primili potvrdu
        private Dictionary<int, Tuple<ActorPath, Post>> notAck;

        private ICancelable retry = Context.System.Scheduler.ScheduleTellRepeatedlyCancelable(1000, 1000, Context.Self, new RetrySaveAll(), Context.Self);

        private int id = 0;
        
        protected override void PostStop()
        {
            retry.Cancel();
        }

        public ProviderActor()
        {
            notAck = new Dictionary<int, Tuple<ActorPath, Post>>();

            Props props = Props.Create(() => new WorkerActor());

            _child = Context.ActorOf(props);

            Receive<Post>(x => HandlePost(x));
            Receive<PostSaved>(x => HandlePostSavedAck(x));
            Receive<RetrySaveAll>(x => HandleRetry());
        }

        private void HandleRetry()
        {
            foreach(var pair in notAck)
            {
                Post post = pair.Value.Item2;

                _child.Tell(post);
            }
        }

        private void HandlePost(Post x)
        {
            int key = IncrementId();

            var post = new Post(x.Id, x.Content, key);

            _child.Tell(post);
            
            notAck.Add(key, Tuple.Create(Sender.Path, post));
        }

        private void HandlePostSavedAck(PostSaved saved)
        {
            var tuple = notAck[saved.Key];

            var ack = new PostAck(tuple.Item2.Id);

            Context.ActorSelection(tuple.Item1).Tell(ack);

            notAck.Remove(saved.Key);
        }

        private int IncrementId()
        {
            id++;

            return id;
        }
    }
}
