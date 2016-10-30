using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MaekawasAlgorithm
{
    class WorkerActor : ReceiveActor
    {
        private bool _sentReply;

        private List<IActorRef> _requestSet;
        private Queue<Tuple<IActorRef, Request>> _deferredRequests;

        private List<Tuple<IActorRef, Reply>> _replies;

        private List<int> _requestSetids;

        public WorkerActor(List<int> requestSetIds)
        {
            _requestSetids = requestSetIds;

            _sentReply = false;

            _requestSet = new List<IActorRef>();
            _deferredRequests = new Queue<Tuple<IActorRef, Request>>();

            _replies = new List<Tuple<IActorRef, Reply>>();

            Receive<ActorIdentity>(x => 
            {
                _requestSet.Add(Sender);

                if (_requestSet.Count == 3) Context.System.Scheduler.ScheduleTellOnce(TimeSpan.FromSeconds(1), Self, new Initiate(), Self);
            });

            Receive<Initiate>(x => RequestAccess());
            Receive<Request>(x => HandleRequest(x));
            Receive<Reply>(x => HandleReply(x));
            Receive<Release>(x => HandleRelease(x));
        }

        protected override void PreStart()
        {
            string pathBase = "akka://MaekawasAlgorithm/user/WorkerActor_";

            foreach(var el in _requestSetids)
            {
                string fullPath = pathBase + el;

                if(fullPath != Self.Path.ToString())
                    Context.ActorSelection(fullPath).Tell(new Identify(el.ToString()));
            }
        }

        private void RequestAccess()
        {
            foreach(var process in _requestSet)
            {
                process.Tell(new Request());
            }
        }

        private void HandleRequest(Request msg)
        {
            if (!_sentReply)
            {
                Sender.Tell(new Reply());
                _sentReply = true;
            }
            else
            {
                _deferredRequests.Enqueue(Tuple.Create(Sender, msg));
            }
        }

        private void HandleReply(Reply msg)
        {
            _replies.Add(Tuple.Create(Sender, msg));

            bool receivedReplyFromAll = _requestSet.All(x => _replies.Any(y => y.Item1.Path == x.Path));

            if(receivedReplyFromAll)
            {
                EnterCriticalSection();
            }
        }

        private void HandleRelease(Release x)
        {
            if(_deferredRequests.Any())
            {
                var next = _deferredRequests.Dequeue();

                next.Item1.Tell(new Reply());
            }
            else
            {
                _sentReply = false;
            }
        }

        private void EnterCriticalSection()
        {
            Console.WriteLine("HI! I'm in the critical section: " + Self.Path);

            Enumerable.Range(1, 10).ToList().ForEach(x => Console.Write(x + " "));

            foreach (IActorRef actor in _requestSet)
            {
                Context.System.Scheduler.ScheduleTellOnce(TimeSpan.FromSeconds(2), actor, new Release(), Self);
            }

            Console.WriteLine("Releasing critical section...");
        }
    }
}
