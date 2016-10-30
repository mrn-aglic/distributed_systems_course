using Akka.Actor;
using Messages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Frontend
{
    class TransformationFrontend : ReceiveActor
    {
        private List<IActorRef> backends = new List<IActorRef>();
        private int jobCounter = 0;

        public TransformationFrontend()
        {
            Receive<TransformationJob>(x => !backends.Any(), x => Sender.Tell(new JobFailed("Service unavailable, try again later")));
            Receive<TransformationJob>(x => HandleJob(x));
            //Receive<TransformationResult>(x => Console.WriteLine(x.Text));
            Receive<BackendRegistration>(x => !backends.Contains(Sender), x => HandleBackendRegistration(x));
            Receive<Terminated>(x => HandleTerminated(x));
        }

        private void HandleJob(TransformationJob x)
        {
            backends[jobCounter % backends.Count].Forward(x);
        }

        private void HandleBackendRegistration(BackendRegistration x)
        {
            Context.Watch(Sender);
            backends.Add(Sender);
        }

        private void HandleTerminated(Terminated a)
        {
            backends.Remove(a.ActorRef);
        }
    }
}
