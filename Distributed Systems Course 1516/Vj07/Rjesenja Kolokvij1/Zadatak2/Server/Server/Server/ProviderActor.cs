using Akka.Actor;
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
        private string myResource = "Hello world";

        private bool locked = false;

        private List<IActorRef> _actors;

        public ProviderActor()
        {
            _actors = new List<IActorRef>();

            Receive<Update>(x => HandleUpdate(x));
        }

        private void HandleUpdate(Update update)
        {
            _actors.Add(Sender);

            if(!locked)
            {
                locked = true;

                var current = _actors.First();

                myResource = myResource + current.Path;
                
                current.Tell(new UpdateAck(myResource));

                _actors.RemoveAt(0);

                if (_actors.Any())
                {
                    var next = _actors.First();

                    _actors.RemoveAt(0);

                    Context.System.Scheduler.ScheduleTellOnce(TimeSpan.FromMilliseconds(10), Self, new Update(), next);                       
                }

                locked = false;
            }
        }

        protected override void Unhandled(object message)
        {
            Console.WriteLine("[Unhandled]: " + message);

            base.Unhandled(message);
        }
    }
}
