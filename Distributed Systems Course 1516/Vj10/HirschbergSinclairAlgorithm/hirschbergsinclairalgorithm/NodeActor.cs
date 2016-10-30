using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChangRobertsAlgorithm
{
    class NodeActor : ReceiveActor
    {
        private readonly int id;
        private ActorSelection leftNeighbour;
        private ActorSelection rightNeighbour;
        private int leaderId;

        private Dictionary<int, int> phaseToReply = new Dictionary<int, int>();

        private int replies = 0;

        public NodeActor(int id, int leftId, int rightId)
        {
            leftNeighbour = Context.ActorSelection(Context.Parent.Path.ToString() + "/" + leftId);
            rightNeighbour = Context.ActorSelection(Context.Parent.Path.ToString() + "/" + rightId);

            this.id = id;

            LeaderElection();
        }

        protected override void PreStart()
        {
            Console.WriteLine("[Actor system] Started actor " + id);

            Random rnd = new Random(id);

            int num = rnd.Next(0, 5);

            Context.System.Scheduler.ScheduleTellOnce(TimeSpan.FromSeconds(2), Self, new Init(), Self);
        }

        private void LeaderElection()
        {
            Receive<Init>(x =>
            {
                Election msg = new Election(id, 0, 0);

                Console.WriteLine("[Actor " + id + "] Initializing... sending: " + msg);

                leftNeighbour.Tell(msg);
                rightNeighbour.Tell(msg);
            }
            );

            Receive<Elected>(x => x.Id == id, x => Console.WriteLine("All notified that " + id + " is elected!"));
            Receive<Elected>(x => {
                leaderId = x.Id;
                rightNeighbour.Tell(x);
            });

            Receive<Election>(x => x.Id < id, x => Console.WriteLine("Smaller id: " + x.Id + ", my id: " + id));
            Receive<Election>(x => x.Id > id && x.Hop < Math.Pow(2, x.CurrentPhase), x => HandleForward(x));
            Receive<Election>(x => x.Id > id && x.Hop == Math.Pow(2, x.CurrentPhase), x => HandleSendReply(x));

            Receive<Election>(x => x.Id == id, x => AnnounceAsLeader());

            //Receive<Reply>(x => x.Id != id && !(leftReply || rightReply), 
            //x => HandleReply(x));
            Receive<Reply>(x => x.Id != id, x => HandleReply(x));
            Receive<Reply>(x => PropagateElection(x));
        }

        private void PropagateElection(Reply x)
        {
            replies++;
            if (replies >= 2)
            {
                Console.WriteLine("[Actor " + id + "] Propagating election... New phase: " + (x.CurrentPhase + 1));

                replies = 0;

                Election newMsg = new Election(x.Id, x.CurrentPhase + 1, 1);

                leftNeighbour.Tell(newMsg);
                rightNeighbour.Tell(newMsg);
            }
        }

        private void HandleReply(Reply x)
        {
            Reply newMsg = new Reply(x.Id, x.CurrentPhase);

            string leftNeighbourPath = leftNeighbour.Anchor.Path + leftNeighbour.Path.Select(y => y.ToString())
                .Aggregate((z, y) => z + "/" + y);

            string rightNeighbourPath = rightNeighbour.Anchor.Path + rightNeighbour.Path.Select(y => y.ToString())
                .Aggregate((z, y) => z + "/" + y);

            if (Sender.Path.ToString() == leftNeighbourPath)
            {
                Console.WriteLine("[Actor " + id + "] Received reply " + x + " from " + leftNeighbourPath);

                rightNeighbour.Tell(newMsg);
            }
            else if (Sender.Path.ToString() == rightNeighbourPath)
            {
                Console.WriteLine("[Actor " + id + "] Received reply " + x + " from " + rightNeighbourPath);

                leftNeighbour.Tell(newMsg);
            }
        }

        private void AnnounceAsLeader()
        {
            Console.WriteLine("[Actor " + id + "] Is leader...");
            leaderId = id;

            rightNeighbour.Tell(new Elected(id));
        }

        private void HandleSendReply(Election x)
        {
            Reply reply = new Reply(x.Id, x.CurrentPhase);

            Console.WriteLine("[Actor " + id + "] Sending reply: " + Sender.Path + " with: " + reply);

            Sender.Tell(new Reply(x.Id, x.CurrentPhase));
        }

        private void HandleForward(Election x)
        {
            Election newMsg = new Election(x.Id, x.CurrentPhase, x.Hop + 1);

            string leftNeighbourPath = leftNeighbour.Anchor.Path + leftNeighbour.Path.Select(y => y.ToString())
                .Aggregate((z, y) => z + "/" + y);

            string rightNeighbourPath = rightNeighbour.Anchor.Path + rightNeighbour.Path.Select(y => y.ToString())
                .Aggregate((z, y) => z + "/" + y);

            //Console.WriteLine("2 ^ x.CurrentPhase: " + (2 ^ x.CurrentPhase));
            //Console.WriteLine("Math.Pow(2, x.CurrentPhase): " + Math.Pow(2, x.CurrentPhase));

            if (Sender.Path.ToString() == leftNeighbourPath)
            {
                Console.WriteLine("[Actor " + id + "|] Got msg from left: " + leftNeighbourPath + " with: " + x);

                rightNeighbour.Tell(newMsg);
            }
            else if (Sender.Path.ToString() == rightNeighbourPath)
            {
                Console.WriteLine("[Actor " + id + "] Got msg from right: " + rightNeighbourPath + " with: " + x);

                leftNeighbour.Tell(newMsg);
            }
        }

    }
}
