namespace Messages
{
    public class Deposit
    {
        public int Amount { get; private set; }

        public Deposit(int amount)
        {
            Amount = amount;
        }
    }

    public class DepositFailed
    {
        public string Reason { get; private set; }

        public DepositFailed(string reason)
        {
            Reason = reason;
        }
    }

    public class Registered { }
}
