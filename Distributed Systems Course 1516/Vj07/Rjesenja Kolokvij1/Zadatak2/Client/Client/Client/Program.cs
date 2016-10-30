using Akka.Actor;
using Akka.Configuration.Hocon;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    static class Program
    {
        public static ActorSystem System { get; private set; }

        [STAThread]
        static void Main()
        {
            var config = (AkkaConfigurationSection)ConfigurationManager.GetSection("akka");

            System = ActorSystem.Create("Client", config.AkkaConfig);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
