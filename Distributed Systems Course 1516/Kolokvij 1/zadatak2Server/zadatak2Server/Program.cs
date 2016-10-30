using Akka.Actor;
using Akka.Configuration;

namespace zadatak2Server
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

            using (var system = ActorSystem.Create("server", config))
            {
                Props props = Props.Create(() => new MainActor());

                system.ActorOf(props, "main");

                system.WhenTerminated.Wait();
            }
        }
    }
}
