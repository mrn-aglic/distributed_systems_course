using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace StanjeTaskova
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<int, Task> taskDict = new Dictionary<int, Task>();

            for (int i = 0; i < 10; i++)
            {
                Task t = new Task(WriteInfo);

                t.Start();

                taskDict.Add(i, t);
            }

            foreach(var el in taskDict)
            {
                Console.WriteLine("Key " + el.Key + " State " + el.Value.Status);
            }

            Thread.Sleep(200);

            foreach (var el in taskDict)
            {
                Console.WriteLine("Key " + el.Key + " State " + el.Value.Status);
            }
            
            Console.ReadLine();
        }

        private static void WriteInfo()
        {
            Console.WriteLine("ID: " + Thread.CurrentThread.ManagedThreadId + " Milliseconds: " + DateTime.Now.Millisecond);

            Thread.Sleep(100);
        }
    }
}
