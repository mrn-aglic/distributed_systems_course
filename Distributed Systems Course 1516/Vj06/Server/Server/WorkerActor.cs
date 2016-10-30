using Akka.Actor;
using Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class WorkerActor : ReceiveActor
    {
        private int LamportClock = 0;

        private Queue<Add> _queue = new Queue<Add>();

        public WorkerActor()
        {
            DefaultBehavoiur();   
        }

        private void DefaultBehavoiur()
        {
            Receive<Add>(x => PreProcessAdd(x));
            Receive<SendClockValue>(x => ProcessSendClockValue(x));
        }
        
        private void AskClockValue()
        {
            LamportClock++;

            Context.Parent.Tell(new SendClockValue(new TimeStamp(LamportClock)));
        }

        private void PreProcessAdd(Add x)
        {
            LamportClock = Math.Max(x.Time.Clock, LamportClock) + 1;

            _queue.Enqueue(x);

            AskClockValue();
        }

        private void ProcessSendClockValue(SendClockValue x)
        {
            LamportClock = Math.Max(x.Time.Clock, LamportClock) + 1;

            ProcessAdd(_queue.Dequeue());
        }

        private void ProcessAdd(Add x)
        {
            LamportClock = Math.Max(x.Time.Clock, LamportClock) + 1;

            Console.WriteLine("[Adding]: " + x.A + " " + x.B + " = " + (x.A + x.B));

            LamportClock++;
            Sender.Tell(new Result(x.A + x.B, new TimeStamp(LamportClock)));
        }
    }
}
