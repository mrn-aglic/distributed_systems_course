using Akka.Actor;
using Akka.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = ConfigurationFactory.ParseString(@"
                                akka {
                                    actor {
                                        provider = ""Akka.Remote.RemoteActorRefProvider, Akka.Remote""
                                    }

                                    remote {
                                        helios.tcp {
                                            port = 12000
                                            hostname = localhost
                                        }
                                    }
                                }
                                ");

            using (var system = ActorSystem.Create("LamportClock", config))
            {
                Props props = Props.Create(() => new WorkDistributor());

                system.ActorOf(props, "Distributor");

                system.WhenTerminated.Wait();
            }
        }
    }
}
