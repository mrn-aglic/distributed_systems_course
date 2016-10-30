using Akka.Actor;
using System;

namespace ChangRobertsAlgorithm
{
    class NodeActor : ReceiveActor
    {
        private readonly int id;
        private ActorSelection neighbour;
        private int leaderId;

        private bool participant = false;

        public NodeActor(int id, int neighborId)
        {
            neighbour = Context.ActorSelection(Context.Parent.Path.ToString() + "/" + neighborId);

            this.id = id;

            LeaderElection();
        }

        protected override void PreStart()
        {
            Console.WriteLine("[Actor system] Started actor " + id);

            Random rnd = new Random(id);

            int num = rnd.Next(0, 5);

            if (id == 2)
                Context.System.Scheduler.ScheduleTellOnce(TimeSpan.FromSeconds(2), Self, new Init(), Self);
        }

        private void LeaderElection()
        {
            Receive<Init>(x =>
            {
                Console.WriteLine("[Actor " + id + "] Initializing...");
                neighbour.Tell(new Election(id));
            }
            );
            Receive<Election>(x => id < x.Id, x => HandleLowerId(x));
            Receive<Election>(x => id > x.Id && participant, x => Console.WriteLine("[Actor " + id + "] Discarding message..."));
            Receive<Election>(x => id > x.Id && !participant, x => HandleLargerId(x));
            Receive<Election>(x => HandleEqualId(x));

            Receive<Elected>(x => x.LeaderId != id, x => HandleElected(x));
        }

        private void HandleLowerId(Election msg)
        {
            participant = true;

            Console.WriteLine("[Actor " + id + "] Received message with " + msg.Id + ". Sending " + msg.Id + "!");

            neighbour.Tell(msg);
        }

        private void HandleLargerId(Election msg)
        {
            participant = true;

            Console.WriteLine("[Actor " + id + "] Received message with " + msg.Id + ". Sending " + id + "!");

            neighbour.Tell(new Election(id));
        }

        private void HandleEqualId(Election msg)
        {
            participant = false;

            Console.WriteLine("[Actor " + id + "] Received message with " + msg.Id + ". Sending " + msg.Id + "! \n" +
                "[Actor " + id + "] Becoming leader...");

            leaderId = id;

            neighbour.Tell(new Elected(id));
        }

        private void HandleElected(Elected msg)
        {
            participant = false;

            Console.WriteLine("[Actor " + id + "] Leader elected " + msg.LeaderId);

            leaderId = id;

            neighbour.Tell(msg);
        }
    }
}
