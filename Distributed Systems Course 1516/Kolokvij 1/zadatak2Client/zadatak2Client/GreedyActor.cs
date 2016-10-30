using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Messages;

namespace zadatak2Client
{
    class GreedyActor : ReceiveActor
    {
        private ListBox _lstBox;

        private string _remoteAddress = "akka.tcp://server@localhost:12000/user/main";
        private ActorSelection _remoteProvider;

        private List<IActorRef> _otherActors;

        private int LogicalClock = 0;

        enum RequestType
        {
            Request,
            RequestAck
        }

        private List<Tuple<string, Rq, RequestType>> _requestPerActor = new List<Tuple<string, Rq, RequestType>>();

        public GreedyActor(ListBox lstBox)
        {
            _lstBox = lstBox;

            _remoteProvider = Context.ActorSelection(_remoteAddress);

            Receive<Register>(x => ProcessRegister(x));
            Receive<RegisterAck>(x => RegisterAckProcess(x));
            Receive<StartRequest>(x => ProcessStartRequest());
            Receive<Request>(x => ProcessRequest(x));
            Receive<RequestAck>(x => ProcessRequestAck(x));
            Receive<StartRelease>(x => ProcessStartRelease());
            Receive<Release>(x => ProcessRelease(x));
            Receive<UpdateAck>(x => ProcessUpdateAck(x));
        }

        private void ProcessUpdateAck(UpdateAck x)
        {
            _lstBox.Items.Add(x.Text);

            Context.System.Scheduler.ScheduleTellOnce(TimeSpan.FromMilliseconds(1), Self, new StartRelease(), Self);
        }

        private void ProcessRegister(Register x)
        {
            _remoteProvider.Tell(x);
        }

        private void RegisterAckProcess(RegisterAck x)
        {
            _otherActors = x.OtherActors.ToList();

            if(x.OtherActors.Any())
                _lstBox.Items.AddRange(x.OtherActors.Select(y => y.Path.ToString()).ToArray());
        }

        private void ProcessStartRequest()
        {
            var msg = new Request(LogicalClock);

            if (!_otherActors.Any()) CheckIfGotResource();

            foreach (var actor in _otherActors)
            {
                actor.Tell(msg);
            }

            var text = Self.Path.Name + " " + Self.Path.Address.Port;

            var triple = Tuple.Create(Context.Self.Path.ToString(), (Rq)msg, RequestType.Request);

            _requestPerActor.Add(triple);

            LogicalClock++;
        }

        private void ProcessRequest(Request x)
        {
            LogicalClock = Math.Max(x.Time, LogicalClock) + 1;

            var triple = Tuple.Create(Context.Sender.Path.ToString(), (Rq)x, RequestType.Request);

            _requestPerActor.Add(triple);

            Context.Sender.Tell(new RequestAck(LogicalClock));

            LogicalClock++;
        }

        private void ProcessRequestAck(RequestAck x)
        {
            LogicalClock = Math.Max(x.Time, LogicalClock) + 1;

            var triple = Tuple.Create(Context.Sender.Path.ToString(), (Rq)x, RequestType.RequestAck);

            _requestPerActor.Add(triple);

            CheckIfGotResource();
        }

        private void ProcessStartRelease()
        {
            _lstBox.Items.AddRange(_otherActors.Select(x => x.Path.ToString()).ToArray());

            foreach (var actor in _otherActors)
            {
                var msg = new Release(LogicalClock);

                actor.Tell(msg);
            }

            LogicalClock++;

            _requestPerActor = _requestPerActor.Where(y => y.Item1 != Context.Self.Path.ToString()).ToList();
        }

        private void ProcessRelease(Release r)
        {
            _requestPerActor =  _requestPerActor.Where(x => x.Item1 != Context.Sender.Path.ToString()).ToList();

            CheckIfGotResource();
        }

        private void CheckIfGotResource()
        {
            var sorted = _requestPerActor.OrderBy(x => x.Item2.Time);

            var filteredSorted = sorted.SkipWhile(x => x.Item3 == RequestType.RequestAck);

            _requestPerActor = filteredSorted.ToList();

            if (!filteredSorted.Any()) { return; }

            var gotIt1 = filteredSorted.First().Item1 == Context.Self.Path.ToString();

            var gotIt2 = _otherActors.All(x => filteredSorted.Any(y => y.Item1 == x.Path.ToString()));

            var gotIt = gotIt1 && gotIt2;

            if(gotIt)
            {
                var text = Self.Path.Name + " @port: " + Self.Path.Address.Port;

                var update = new Update(LogicalClock, text);

                _remoteProvider.Tell(update);
            }
        }
    }
}
