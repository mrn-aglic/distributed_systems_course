using Akka.Actor;
using Akka.Configuration;
using Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientLamport
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
                                            port = 0
                                            hostname = localhost
                                        }
                                    }
                                }
                                ");

            using (var system = ActorSystem.Create("klijent", config))
            {
                var props = Props.Create(() => new Requester());

                var actor = system.ActorOf(props, "mojklijent");

                actor.Tell(new Start());

                system.WhenTerminated.Wait();
            }
        }
    }
}
