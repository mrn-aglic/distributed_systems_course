using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages
{
    public class TimeStamp
    {
        public int Clock { get; private set; }

        public TimeStamp(int clock)
        {
            Clock = clock;
        }
    }

    public class Register
    {
        public string Name { get; private set; }

        public TimeStamp Time { get; private set; }

        public Register(string name, TimeStamp clock)
        {
            Name = name;
            Time = clock;
        }
    }

    public class Confirm
    {
        public IActorRef Actor { get; private set; }
        public TimeStamp Time { get; private set; }

        public Confirm(IActorRef actor, TimeStamp clock)
        {
            Actor = actor;
            Time = clock;
        }
    }
    
    public class Deny 
    {
        public string Reason { get; private set; }
        public TimeStamp Time { get; private set; }

        public Deny(string reason, TimeStamp clock)
        {
            Time = clock;
            Reason = reason;
        }
    }

    public class Add 
    {
        public int A { get; private set; }
        public int B { get; private set; }
        public TimeStamp Time { get; private set; }

        public Add(int a, int b, TimeStamp clock)
        {
            A = a;
            B = b;
            Time = clock;
        }
    }

    public class Result 
    {
        public double A { get; private set; }
        public string ReasonIfFailed { get; private set; }
        public TimeStamp Time { get; private set; }

        public Result(int a, TimeStamp clock)
        {
            A = a;
            Time = clock;
        }

        public Result(int a, string reason, TimeStamp clock)
        {
            A = a;
            ReasonIfFailed = reason;
            Time = clock;
        }
    }

    public class SendClockValue
    {
        public TimeStamp Time { get; private set; }

        public SendClockValue(TimeStamp time)
        {
            Time = time;
        }
    }
}
