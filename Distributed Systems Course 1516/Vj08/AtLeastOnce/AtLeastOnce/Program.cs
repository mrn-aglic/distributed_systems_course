using Akka.Actor;

namespace AtLeastOnce
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var system = ActorSystem.Create("AtLeastOnce"))
            {
                Props props = Props.Create(() => new ProviderActor());

                var provider = system.ActorOf(props, "provider");

                Props propsR = Props.Create(() => new RequesterActor(provider));

                var requester = system.ActorOf(propsR, "requester");

                requester.Tell(new Send());

                system.WhenTerminated.Wait();
            }
        }
    }
}
