using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LamportME
{
    class WorkerActor : ReceiveActor
    {
        private Queue<Tuple<int, ActorPath>> _requestQueue;
        private ActorSelection workersSelection;

        private List<Tuple<int, ActorPath>> _replies;
        private List<IActorRef> _allOthers;

        private ICancelable waitForIdentityCancelable;
        private int timestamp;

        public WorkerActor()
        {
            _requestQueue = new Queue<Tuple<int, ActorPath>>();

            timestamp = 0;

            _replies = new List<Tuple<int, ActorPath>>();
            _allOthers = new List<IActorRef>();

            Receive<ActorIdentity>(x => HandleActorIdentity(x));

            Receive<Initiate>(x => RequestCriticalSection());
            Receive<Reply>(x => HandleReply(x));
            Receive<Request>(x => EnqueueRequest(x));
            Receive<Release>(x => ReleaseCriticalSection());
        }

        protected override void PreStart()
        {
            workersSelection = Context.ActorSelection("akka://LamportME/user/*");

            Context.System.Scheduler.ScheduleTellOnce(TimeSpan.FromSeconds(1), workersSelection, new Identify("1"), Self);
        }

        private void HandleActorIdentity(ActorIdentity x)
        {
            waitForIdentityCancelable.CancelIfNotNull();

            if (Sender.Path != Self.Path)
                _allOthers.Add(Sender);

            waitForIdentityCancelable = Context.System.Scheduler.ScheduleTellOnceCancelable(TimeSpan.FromSeconds(2), Self, new Initiate(), Self);
        }

        private void ReleaseCriticalSection()
        {
            if (!_requestQueue.Any()) return;

            _requestQueue.Dequeue();

            bool isMineOnTop = _requestQueue.Any() ? _requestQueue.Peek().Item2 == Self.Path : false;

            if (isMineOnTop)
            {
                EnterCriticalSection();
            }
        }

        private void RequestCriticalSection()
        {
            timestamp += 1;

            foreach (IActorRef actor in _allOthers)
            {
                Context.System.Scheduler.ScheduleTellOnce(TimeSpan.FromSeconds(1), actor, new Request(timestamp), Self);
            }

            _requestQueue.Enqueue(Tuple.Create(timestamp, Self.Path));

            _requestQueue = new Queue<Tuple<int, ActorPath>>(_requestQueue.OrderBy(x => x.Item1).ThenBy(x => x.Item2.ToString()));
        }

        private void EnqueueRequest(Request request)
        {
            timestamp = Math.Max(request.TimeStamp, timestamp) + 1;

            _requestQueue.Enqueue(Tuple.Create(request.TimeStamp, Sender.Path));

            _requestQueue = new Queue<Tuple<int, ActorPath>>(_requestQueue.OrderBy(x => x.Item1).ThenBy(x => x.Item2.ToString()));

            timestamp += 1;

            Sender.Tell(new Reply(timestamp));
        }

        private void HandleReply(Reply reply)
        {
            _replies.Add(Tuple.Create(reply.TimeStamp, Sender.Path));

            timestamp = Math.Max(reply.TimeStamp, timestamp) + 1;

            Tuple<int, ActorPath> myLatestPair = _requestQueue.Where(x => x.Item2 == Self.Path).OrderBy(x => x.Item1).FirstOrDefault();

            Request myLatestRequest = new Request(myLatestPair.Item1);

            bool receivedLargerFromAllOthers = _allOthers.TrueForAll(x =>
                
                _replies.Where(y => y.Item1 > myLatestRequest.TimeStamp).Select(y => y.Item2).Contains(x.Path)
            );

            bool isMineOnTop = _requestQueue.Peek().Item2 == Self.Path;

            if (receivedLargerFromAllOthers && isMineOnTop) EnterCriticalSection();
        }

        private void EnterCriticalSection()
        {
            Console.WriteLine("HI! I'm in the critical section: " + Self.Path);

            _requestQueue.Dequeue();

            foreach (IActorRef actor in _allOthers)
            {
                Context.System.Scheduler.ScheduleTellOnce(TimeSpan.FromSeconds(2), actor, new Release(), Self);
            }

            Console.WriteLine("Releasing critical section...");
        }

        protected override void Unhandled(object message)
        {
            Console.WriteLine("Unhandled message: " + message);
        }
    }
}
