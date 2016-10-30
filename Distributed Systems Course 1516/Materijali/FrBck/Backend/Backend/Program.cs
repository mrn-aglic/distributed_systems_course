using Akka.Actor;
using Akka.Configuration;
using Akka.Configuration.Hocon;
using Messages;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Backend
{
    class Program
    {
        static void Main(string[] args)
        {
            SurrogateMain(args);
            //SurrogateMain(new string[] { "9000" });
            //SurrogateMain(new string[] { "9001" });
            //SurrogateMain(new string[] { });
            //SurrogateMain(new string[] { });

            Console.ReadKey();
        }

        static void SurrogateMain(string[] args)
        {
            var port = args.Any() ? args[0] : "0";

            var configsection = (AkkaConfigurationSection)ConfigurationManager.GetSection("akka");

            var config = ConfigurationFactory.ParseString("akka.remote.helios.tcp.port=" + port)
                .WithFallback(ConfigurationFactory.ParseString("akka.cluster.roles = [backend]"))
                .WithFallback(configsection.AkkaConfig);

            var system = ActorSystem.Create("ClusterSystem", config);

            system.ActorOf(Props.Create(() => new TransformationBackend()), "backend");

        }
    }
}
