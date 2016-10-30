using Akka.Actor;
using SharedMessages;
using System;

namespace RemoteHelloWorld
{
    class Replier : ReceiveActor
    {
        public Replier()
        {
            Receive<Query>(x => ProcessQuery(x));
        }

        private void ProcessQuery(Query query)
        {
            Console.WriteLine("Got query: " + query.Text);

            var answer = new Answer("Hello " + query.Text);

            Sender.Tell(answer);
        }

        protected override void Unhandled(object message)
        {
            Console.WriteLine(message);

            base.Unhandled(message);
        }
    }
}
