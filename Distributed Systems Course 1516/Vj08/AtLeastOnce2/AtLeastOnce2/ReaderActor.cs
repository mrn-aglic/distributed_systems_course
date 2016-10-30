using Akka.Actor;
using System;

namespace AtLeastOnce
{
    class ReaderActor : ReceiveActor
    {
        private IActorRef _providerActor;

        private IActorRef _child;

        public ReaderActor(IActorRef providerActor)
        {
            _providerActor = providerActor;

            Receive<Send>(x => HandleSend());
        }

        protected override void PreStart()
        {
            _child = Context.ActorOf(Props.Create(() => new RequesterActor(_providerActor)));
        }

        private void HandleSend()
        {
            Console.WriteLine("Unesite text za post: ");
            string text = Console.ReadLine();

            _child.Tell(new Send(text));

            Context.System.Scheduler.ScheduleTellOnce(TimeSpan.FromSeconds(1), Self, new Send(""), Self);
        }
    }
}
