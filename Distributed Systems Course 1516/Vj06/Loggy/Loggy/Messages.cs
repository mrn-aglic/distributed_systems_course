using Akka.Actor;
using System.Collections.Generic;

namespace Loggy
{
    // Ove poruke nisu morale izgledati ovako
    class Log
    {
        public string Name { get; private set; }
        public TimeStampMsg TimeStampMsg { get; private set; }

        public Log(string name, TimeStampMsg timeStampMsg)
        {
            Name = name;
            TimeStampMsg = timeStampMsg;
        }
    }

    class TimeStampMsg
    {
        public int LamportTime { get; private set; }
        public string Text { get; private set; }

        public TimeStampMsg(int lamportTime, string text)
        {
            LamportTime = lamportTime;
            Text = text;
        }
    }

    class RandomMsg
    {
        public int Br { get; private set; }
        public TimeStampMsg TimeStampMsg { get; private set; }

        public RandomMsg(int br, TimeStampMsg timeStampMsg)
        {
            Br = br;
            TimeStampMsg = timeStampMsg;
        }
    }

    class WorkerStart
    {
        public List<IActorRef> OtherActors { get; private set; }

        public WorkerStart(List<IActorRef> otherActors)
        {
            OtherActors = otherActors;
        }
    }

    class Loop { }
}
