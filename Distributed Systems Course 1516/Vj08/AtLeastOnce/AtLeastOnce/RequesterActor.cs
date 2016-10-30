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

        public RequesterActor(IActorRef providerActor)
        {
            _providerActor = providerActor;

            Receive<Send>(x => HandleSend());
            Receive<PostAck>(x => HandlePostAck(x));
        }

        private void HandlePostAck(PostAck x)
        {
            Console.WriteLine("Post " + x.Id + " saved!");
        }

        private void HandleSend()
        {
            Console.WriteLine("Unesite text za post: ");
            string text = Console.ReadLine();

            JObject jObject = new JObject();

            jObject["id"] = id;
            jObject["content"] = text;

            // key ne znamo, njega ce dodijeliti onaj kojemu saljemo poruku
            _providerActor.Tell(new Post(id, jObject.ToString(), 0));
            
             IncremenetId();

            Context.System.Scheduler.ScheduleTellOnce(TimeSpan.FromSeconds(1), Self, new Send(), Self);
        }

        private int IncremenetId()
        {
            id++;
            return id;
        }
    }
}
