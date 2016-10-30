using Akka.Actor;
using System;

namespace AtMosteOnce
{
    class RequesterActor : ReceiveActor
    {
        private IActorRef _receiver;
        private int _amount;

        private int _expectedAmount;

        public RequesterActor(int amount, IActorRef receiver)
        {
            _amount = amount;
            _receiver = receiver;
            _expectedAmount = 0;

            Receive<Deposit>(x => DoDeposit(x));
        }

        protected override void PreStart()
        {
            Context.System.Scheduler.ScheduleTellOnce(TimeSpan.FromMilliseconds(1), Self, new Deposit(_amount), Self);
        }

        private void DoDeposit(Deposit msg)
        {
            _receiver.Tell(msg);

            // expectedAmount varijabla nam sluzi da bi znali koju vrijednost ocekivati u datoteci te ju usporedili sa stvarnom vrijednoscu
            _expectedAmount += msg.Amount;

            Console.WriteLine("Expected amount:> " + _expectedAmount);

            Context.System.Scheduler.ScheduleTellOnce(TimeSpan.FromMilliseconds(1), Self, new Deposit(_amount), Self);
        }
    }
}
