using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.Configuration.Hocon;
using System.Configuration;
using Akka.Configuration;
using System.Threading;
using Messages;

namespace Frontend
{
    class Program
    {
        static void Main(string[] args)
        {
            SurrogateMain(args);
            //SurrogateMain(new string[] { "9000" });
            //SuroggatMain(new string[] { "9001" });
            //SuroggatMain(new string[] { });
            //SuroggatMain(new string[] { });

            //SurrogateMain(new string[] { });

            Console.ReadKey();
        }

        static void SurrogateMain(string[] args)
        {
            var port = args.Any() ? args[0] : "0";

            var configsection = (AkkaConfigurationSection)ConfigurationManager.GetSection("akka");

            var config = ConfigurationFactory.ParseString("akka.remote.helios.tcp.port=" + port)
                .WithFallback(ConfigurationFactory.ParseString("akka.cluster.roles = [frontend]"))
                .WithFallback(configsection.AkkaConfig);

            var system = ActorSystem.Create("ClusterSystem", config);

            var frontend = system.ActorOf(Props.Create(() => new TransformationFrontend()), "frontend");

            var counter = 0;

            system.Scheduler.Advanced.ScheduleRepeatedly(TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(2), () =>
            {
                frontend.Ask(new TransformationJob("hello " + Interlocked.Add(ref counter, 1))).ContinueWith(x =>
                    Console.WriteLine(x.Result)
                );
            });

        }
    }
}
