using Akka.Actor;
using Messages;
using System;

namespace Server
{
    class WorkerActor : ReceiveActor
    {
        private BankAccount _account;

        public WorkerActor()
        {
            Receive<Deposit>(x => HandleDeposit(x));
        }

        protected override void PreStart()
        {
            _account = new BankAccount();
        }

        private void HandleDeposit(Deposit x)
        {
            Random rnd = new Random();

            int num = rnd.Next(1, 11);

            _account.Add(x.Amount);

            if (num > 5)
            {
                _account.SaveSavings();
            }
            else
            {
                throw new Exception("Random number exception");
            }

            Console.WriteLine("Current amount:> " + _account.Savings);
        }
    }
}
