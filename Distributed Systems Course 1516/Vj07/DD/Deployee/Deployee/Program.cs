using Akka.Actor;
using System;
using Akka.Configuration.Hocon;
using System.Configuration;

namespace Deployee
{
    class Program
    {
        static void Main(string[] args)
        {
            var conf = (AkkaConfigurationSection)ConfigurationManager.GetSection("akka");

            using (var system = ActorSystem.Create("DeployTarget", conf.AkkaConfig))
            {
                Console.ReadLine();
            }
        }
    }
}
