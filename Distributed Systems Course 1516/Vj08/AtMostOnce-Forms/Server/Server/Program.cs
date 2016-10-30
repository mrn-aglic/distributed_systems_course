using Akka.Actor;
using Akka.Configuration;
using Akka.Configuration.Hocon;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        private static string _savedLocation = "../../Savings.txt";

        static void Main(string[] args)
        {
            string[] ports = new[] { "12000", "12001", "0" };

            File.WriteAllText(_savedLocation, 0.ToString());

            foreach (var port in ports)
            {
                var config = (AkkaConfigurationSection)ConfigurationManager.GetSection("akka");

                var akkaConfig = ConfigurationFactory.ParseString("akka.remote.helios.tcp.port=" + port)
                                    .WithFallback("akka.cluster.roles=[provider]")
                                    .WithFallback(config.AkkaConfig);

                var system = ActorSystem.Create("AtMostOnceCluster", akkaConfig);

                Props props = Props.Create(() => new ProviderActor());

                system.ActorOf(props);
            }

            Console.ReadLine();
        }
    }
}
