using Akka.Actor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtLeastOnce
{
    class WorkerActor : ReceiveActor
    {
        private const string Path = "../../BlogPostovi.txt";

        public WorkerActor()
        {
            Receive<Post>(x => HandlePost(x));
        }

        private void HandlePost(Post x)
        {
            MaybeThrowException();

            using (var writer = File.AppendText(Path))
            {
                writer.WriteLine(x.Content);
            }

            Context.Parent.Tell(new PostSaved(x.Key));
        }

        private void MaybeThrowException()
        {
            Random rnd = new Random();

            int n = rnd.Next(1, 11);

            if (n > 7)
            {
                try
                {
                    throw new Exception("Random number exception");
                }
                catch(Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }
    }
}
