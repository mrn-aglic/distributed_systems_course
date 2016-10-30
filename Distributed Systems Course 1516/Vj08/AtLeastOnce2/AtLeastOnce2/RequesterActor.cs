using Akka.Actor;
using System;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace AtLeastOnce
{
    class RequesterActor : ReceiveActor
    {
        private IActorRef _providerActor;

        int id = 0;

        private Dictionary<int, Tuple<Post, ICancelable>> notAck;

        //private ICancelable retry =
        //Context.System.Scheduler.ScheduleTellRepeatedlyCancelable(TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(5), Context.Self, new Retry(), Context.Self);

        public RequesterActor(IActorRef providerActor)
        {
            notAck = new Dictionary<int, Tuple<Post, ICancelable>>();
            _providerActor = providerActor;

            Receive<Send>(x => HandleSend(x));
            Receive<PostAck>(x => HandlePostAck(x));
            Receive<Retry>(x => HandleRetry(x));
        }

        private void HandlePostAck(PostAck x)
        {
            Console.WriteLine("Post " + x.Id + " saved!");

            notAck[x.Id].Item2.Cancel();
            notAck.Remove(x.Id);
        }

        private void HandleSend(Send x)
        {
            JObject jObject = new JObject();

            jObject["id"] = id;
            jObject["content"] = x.Text;

            var post = new Post(id, jObject.ToString());

            // key ne znamo, njega ce dodijeliti onaj kojemu saljemo poruku
            _providerActor.Tell(post);

            var retry = Context.System.Scheduler.ScheduleTellRepeatedlyCancelable(TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(5), Self, new Retry(post), Self);

            notAck.Add(id, Tuple.Create(post, retry));

            IncremenetId();
        }

        private void HandleRetry(Retry x)
        {
            Console.WriteLine("Ponovno slanje za: " + x.Post.Id);
            _providerActor.Tell(x.Post);
        }

        private int IncremenetId()
        {
            id++;
            return id;
        }
    }
}
