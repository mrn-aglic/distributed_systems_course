using Akka.Actor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExactlyOnceProcessing
{
    class WorkerActor : ReceiveActor
    {
        private const string Path = "../../BlogPostovi.txt";

        public WorkerActor()
        {
            Receive<SavePost>(x => HandlePost(x));
        }

        private void HandlePost(SavePost x)
        {
            MaybeThrowException();

            using (var writer = File.AppendText(Path))
            {
                writer.WriteLine(x.Post.Content);
            }

            Context.Parent.Tell(new PostSaved(x.From, x.Post.Id));
        }

        private void MaybeThrowException()
        {
            Random rnd = new Random();

            int n = rnd.Next(1, 11);

            if (n < 7)
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
