using Akka.Actor;
using System;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace ExactlyOnceProcessing
{
    class RequesterActor : ReceiveActor
    {
        private IActorRef _providerActor;

        int id = 0;

        private Dictionary<int, Post> notAck;

        private ICancelable retry =
        Context.System.Scheduler.ScheduleTellRepeatedlyCancelable(TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(5), Context.Self, new Retry(), Context.Self);

        public RequesterActor(IActorRef providerActor)
        {
            notAck = new Dictionary<int, Post>();
            _providerActor = providerActor;

            Receive<Send>(x => HandleSend(x));
            Receive<PostAck>(x => HandlePostAck(x));
            Receive<Retry>(x => HandleRetry());
        }

        private void HandlePostAck(PostAck x)
        {
            Console.WriteLine("Post " + x.Id + " saved!");

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

            notAck.Add(id, post);

            IncremenetId();
        }

        private void HandleRetry()
        {
            foreach (var pair in notAck)
            {
                Console.WriteLine("Ponovno slanje za: " + pair.Key);
                _providerActor.Tell(pair.Value);
            }
        }

        private int IncremenetId()
        {
            id++;
            return id;
        }
    }
}
