using Akka.Actor;
using Akka.Configuration;
using Akka.Configuration.Hocon;
using System;
using System.Configuration;
using System.Windows.Forms;

namespace Client
{
    static class Program
    {
        public static ActorSystem system { get; private set; }

        [STAThread]
        static void Main()
        {
            var config = (AkkaConfigurationSection)ConfigurationManager.GetSection("akka");

            var akkaConfig = ConfigurationFactory.ParseString("akka.cluster.roles=[client]").WithFallback(config.AkkaConfig);

            system = ActorSystem.Create("AtMostOnceCluster", akkaConfig);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
