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
}
