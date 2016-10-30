using System.IO;

namespace AtMosteOnce
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

        #region Spremi u trajnu memoriju
        private int GetSavedSavings()
        {
            return int.Parse(File.ReadAllText(_savedLocation));
        }
        #endregion

        #region Procitaj iz trajne memorije
        public void SaveSavings()
        {
            File.WriteAllText(_savedLocation, Savings.ToString());
        }
        #endregion
    }
}
