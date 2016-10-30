using Akka.Actor;
using System;

namespace AtMosteOnce
{
    class ProviderActor : ReceiveActor
    {
        private BankAccount _account;

        public ProviderActor()
        {
            Receive<Deposit>(x => DoDeposit(x));
        }

        protected override void PreStart()
        {
            _account = new BankAccount();
        }

        private void DoDeposit(Deposit msg)
        {
            Random rnd = new Random();

            int num = rnd.Next(1, 11);

            _account.Add(msg.Amount);

            if (num > 5)
            {
                _account.SaveSavings();
            }

            Console.WriteLine("True amount:> " + _account.Savings);
        }
    }
}
