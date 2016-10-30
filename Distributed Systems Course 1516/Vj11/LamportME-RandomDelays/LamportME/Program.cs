using Akka.Actor;

namespace LamportME
{
    class Program
    {
        static void Main(string[] args)
        {
            using(var system = ActorSystem.Create("LamportME"))
            {
                for(int i = 0; i < 7; i++)
                {
                    int j = i;

                    system.ActorOf(Props.Create(() => new WorkerActor(j)), "Worker_" + i);
                }

                system.WhenTerminated.Wait();
            }
        }
    }
}
