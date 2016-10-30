using Akka.Actor;
using Akka.Configuration;

namespace RemoteHelloWorld
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

            using(var system = ActorSystem.Create("MyRemoteSystem", config))
            {
                var props = Props.Create(() => new Replier());

                // Uocite da actoru kojeg stvorimo dajemo nekakvo ime
                var actor = system.ActorOf(props, name: "replier");

                system.WhenTerminated.Wait();
            }
        }
    }
}
