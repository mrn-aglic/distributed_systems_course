using Akka.Actor;
using System.Collections.Generic;
using System.Linq;

namespace MaekawasAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            using(var system = ActorSystem.Create("MaekawasAlgorithm"))
            {
                var R1 = new List<int> { 1, 2, 3, 5 };
                var R2 = new List<int> { 3, 4, 7, 8 };
                var R3 = new List<int> { 4, 5, 6, 9 };

                var Rs = new List<List<int>> { R1, R2, R3 };

                for(int i = 1; i <= 9; i++)
                {
                    var r = Rs.First(x => x.Contains(i));

                    system.ActorOf(Props.Create(() => new WorkerActor(r)), name: "WorkerActor_" + i);
                }

                system.WhenTerminated.Wait();
            }
        }
    }
}
