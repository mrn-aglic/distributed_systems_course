using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MCSimulator
{
    static class Program
    {
        public static ActorSystem system { get; private set; }

        [STAThread]
        static void Main()
        {
            system = ActorSystem.Create("Simulator");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
