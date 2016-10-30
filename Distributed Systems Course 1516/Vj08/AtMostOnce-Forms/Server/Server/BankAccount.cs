using System.IO;

namespace Server
{
    class BankAccount
    {
        private const string _savedLocation = "../../Savings.txt";

        public int Savings { get; private set; }

        public BankAccount()
        {
            Savings = GetSavedSavings();
        }
        
        public void Add(int amount)
        {
            Savings += amount;
        }

        #region Procitaj iz trajne memorije
        private int GetSavedSavings()
        {
            return int.Parse(File.ReadAllText(_savedLocation));
        }
        #endregion

        #region Spremi u trajnu memoriju
        public void SaveSavings()
        {
            File.WriteAllText(_savedLocation, Savings.ToString());
        }
        #endregion
    }
}
