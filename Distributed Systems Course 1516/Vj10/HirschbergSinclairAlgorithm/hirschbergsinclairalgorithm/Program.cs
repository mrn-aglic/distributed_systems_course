using Akka.Actor;
using System;
using System.Collections.Generic;

namespace ChangRobertsAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            using(var system = ActorSystem.Create("HS"))
            {
                // 0 -> left: 2, right: 3
                // 1 -> left: 5, right: 4 
                // 2 -> left: 4, right: 0
                // 3 -> left: 0, right: 5
                // 4 -> left: 1, right: 2 
                // 5 -> left: 3, right: 1
                List<Tuple<int, int>> nodes = new List<Tuple<int, int>>
                {
                    Tuple.Create(2, 3),
                    Tuple.Create(5, 4),
                    Tuple.Create(4, 0),
                    Tuple.Create(0, 5),
                    Tuple.Create(1, 2),
                    Tuple.Create(3, 1)
                };
 
                for(int i = 0; i < 6; i++)
                {
                    system.ActorOf(Props.Create(() => new NodeActor3(i, nodes[i].Item1, nodes[i].Item2)), i.ToString());
                }

                system.WhenTerminated.Wait();
            }
        }
    }
}
