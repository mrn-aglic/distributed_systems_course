using Akka.Actor;
using System.Collections.Generic;

namespace ChangRobertsAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            using(var system = ActorSystem.Create("Chang-Roberts"))
            {
                List<int> nodes = new List<int> { 1, 5, 3, 0, 2, 4 };
 
                for(int i = 0; i < 6; i++)
                {
                    system.ActorOf(Props.Create(() => new NodeActor(i, nodes[i])), i.ToString());
                }

                system.WhenTerminated.Wait();
            }
        }
    }
}
