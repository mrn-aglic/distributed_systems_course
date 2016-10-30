using Akka.Actor;
using System.Collections.Generic;

namespace Messages
{
    public class Register { }

    public class RegisterAck
    {
        public IEnumerable<IActorRef> OtherActors { get; private set; }

        public RegisterAck(IEnumerable<IActorRef> otherActors)
        {
            OtherActors = otherActors;
        }
    }

    public class Update
    {
        public int Clock { get; private set; }
        public string Text { get; private set; }

        public Update(int clock, string text)
        {
            Clock = clock;
            Text = text;
        }
    }

    public class UpdateAck
    {
        public string Text { get; private set; }

        public UpdateAck(string text)
        {
            Text = text;
        }
    }

    public class StartRequest
    {
    }

    public abstract class Rq
    {
        public int Time { get; private set; }

        public Rq(int time)
        {
            Time = time;
        }
    }

    public class Request : Rq
    {
        public Request(int time) : base(time)
        {
        }
    }

    public class RequestAck : Rq
    {
        public RequestAck(int time): base(time)
        {
        }
    }

    public class StartRelease { }

    public class Release
    {
        public int Time { get; private set; }

        public Release(int time)
        {
            Time = time;
        }
    }

    public class ReleaseAck
    {
        public int Time { get; private set; }

        public ReleaseAck(int time)
        {
            Time = time;
        }
    }
}
