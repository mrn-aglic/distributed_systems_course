using Akka.Actor;
using Akka.Configuration;
using Akka.Configuration.Hocon;
using System;
using System.Configuration;

namespace AkkaClusterExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var ports = new[] { "2552", "2551", "0" };

            var section = (AkkaConfigurationSection)ConfigurationManager.GetSection("akka");
            
            foreach(var port in ports)
            {
                // Ne mozemo pokrenuti svaki sustav na isti port:
                var config = ConfigurationFactory.ParseString("akka.remote.helios.tcp.port=" + port).WithFallback(section.AkkaConfig);

                var system = ActorSystem.Create("ClusterSystem", config);

                var actor = system.ActorOf(Props.Create(() => new SampleClusterListener()), "clusterListener");
            }

            Console.ReadKey();
        }
    }
}
