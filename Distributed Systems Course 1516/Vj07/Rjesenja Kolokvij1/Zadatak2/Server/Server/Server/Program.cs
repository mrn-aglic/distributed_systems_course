using Akka.Actor;
using Akka.Configuration.Hocon;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = (AkkaConfigurationSection) ConfigurationManager.GetSection("akka");

            using (var system = ActorSystem.Create("Server", config.AkkaConfig))
            {
                Props props = Props.Create(() => new ProviderActor());

                system.ActorOf(props, "provider");

                system.WhenTerminated.Wait();
            }
        }
    }
}
