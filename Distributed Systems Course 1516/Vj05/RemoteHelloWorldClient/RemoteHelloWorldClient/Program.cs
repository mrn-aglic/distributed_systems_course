using Akka.Actor;
using Akka.Configuration;
using SharedMessages;

namespace RemoteHelloWorldClient
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
                       
                            port = 12001
                            hostname = localhost
                        } 
                    }
                }
            ");

            using (var system = ActorSystem.Create("MyClientSystem", config))
            {
                var props = Props.Create((() => new QuerierActor()));

                var actor = system.ActorOf(props);

                actor.Tell(new Send());

                system.WhenTerminated.Wait();
            }
        }
    }
}
