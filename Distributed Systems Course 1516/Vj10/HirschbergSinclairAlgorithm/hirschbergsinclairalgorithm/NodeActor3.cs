using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangRobertsAlgorithm
{
    class NodeActor3 : ReceiveActor
    {
        private readonly int id;
        private ActorSelection leftNeighbour;
        private ActorSelection rightNeighbour;
        private int leaderId;

        private Dictionary<int, int> phaseToReply = new Dictionary<int, int>();

        private Dictionary<string, int> gotReplyFrom = new Dictionary<string, int>();

        private int replies = 0;

        public NodeActor3(int id, int leftId, int rightId)
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
            Receive<Election>(x => x.Id < id, x => Console.WriteLine("[Actor " + id + "] Ignoring message: " + x));
            Receive<Election>(x => x.Id > id, x => HandleReceivedLargerId(x));
            Receive<Election>(x => x.Id == id, x => Winner(x));

            Receive<Elected>(x => HandleElected(x));

            Receive<Reply>(x => x.Id != id, x => ForwardReply(x));
            Receive<Reply>(x => HandleSameIdReply(x));
        }

        private void HandleSameIdReply(Reply x)
        {
            if (!gotReplyFrom.ContainsKey(Sender.Path.ToString()))
            {
                gotReplyFrom.Add(Sender.Path.ToString(), 0);
            }

            string leftNeighbourPath = leftNeighbour.Anchor.Path + leftNeighbour.Path.Select(y => y.ToString())
                    .Aggregate((z, y) => z + "/" + y);

            string rightNeighbourPath = rightNeighbour.Anchor.Path + rightNeighbour.Path.Select(y => y.ToString())
                .Aggregate((z, y) => z + "/" + y);

            gotReplyFrom[Sender.Path.ToString()]++;

            if (!gotReplyFrom.ContainsKey(leftNeighbourPath)) gotReplyFrom.Add(leftNeighbourPath, 0);
            if (!gotReplyFrom.ContainsKey(rightNeighbourPath)) gotReplyFrom.Add(rightNeighbourPath, 0);

            if (gotReplyFrom[leftNeighbourPath] > 0 && gotReplyFrom[rightNeighbourPath] > 0)
            {
                Console.WriteLine("[Actor " + id + "] Propagating election... New phase: " + (x.CurrentPhase + 1));

                gotReplyFrom[leftNeighbourPath] = 0;
                gotReplyFrom[rightNeighbourPath] = 0;

                Election newMsg = new Election(x.Id, x.CurrentPhase + 1, 1);

                leftNeighbour.Tell(newMsg);
                rightNeighbour.Tell(newMsg);
            }
        }

        private void HandleElected(Elected x)
        {
            if (x.Id == id)
            {
                Console.WriteLine("All have been notified...Leader is " + id);
            }
            else
            {
                leaderId = x.Id;
                rightNeighbour.Tell(x);
            }
        }

        private void Winner(Election x)
        {
            leaderId = x.Id;

            rightNeighbour.Tell(new Elected(id));
        }

        private void HandleReceivedLargerId(Election x)
        {
            double maxHops = Math.Pow(2, x.CurrentPhase);

            if (x.Hop < maxHops)
            {
                ForwardElection(x);
            }
            else if (x.Hop == maxHops)
            {
                SendReply(new Reply(x.Id, x.CurrentPhase));
            }
        }

        private void SendReply(Reply x)
        {
            Sender.Tell(new Reply(x.Id, x.CurrentPhase));
        }

        private void ForwardElection(Election x)
        {
            Forward(new Election(x.Id, x.CurrentPhase, x.Hop + 1));
        }

        private void ForwardReply(Reply x)
        {
            Forward(x);
        }

        private void Forward(IMessage newMsg)
        {
            string leftNeighbourPath = leftNeighbour.Anchor.Path + leftNeighbour.Path.Select(y => y.ToString())
                    .Aggregate((z, y) => z + "/" + y);

            string rightNeighbourPath = rightNeighbour.Anchor.Path + rightNeighbour.Path.Select(y => y.ToString())
                .Aggregate((z, y) => z + "/" + y);

            //Console.WriteLine("2 ^ x.CurrentPhase: " + (2 ^ x.CurrentPhase));
            //Console.WriteLine("Math.Pow(2, x.CurrentPhase): " + Math.Pow(2, x.CurrentPhase));

            if (Sender.Path.ToString() == leftNeighbourPath)
            {
                Console.WriteLine("[Actor " + id + "|] Got msg from left: " + leftNeighbourPath + " with: " + newMsg);

                rightNeighbour.Tell(newMsg);
            }
            else if (Sender.Path.ToString() == rightNeighbourPath)
            {
                Console.WriteLine("[Actor " + id + "] Got msg from right: " + rightNeighbourPath + " with: " + newMsg);

                leftNeighbour.Tell(newMsg);
            }
        }
    }
}
