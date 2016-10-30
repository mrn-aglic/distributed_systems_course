using Akka.Actor;
using System;
using System.Windows.Forms;

namespace Loggy
{
    static class Program
    {
        public static ActorSystem System
        {
            get;
            private set;
        }

        [STAThread]
        static void Main()
        {
            System = ActorSystem.Create("Loggy");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
