using Akka.Actor;

namespace AtLeastOnce
{
    class ProviderActor : ReceiveActor
    {
        private IActorRef _child;

        private int id = 0;
        
        public ProviderActor()
        {
            Props props = Props.Create(() => new WorkerActor());

            _child = Context.ActorOf(props);

            Receive<Post>(x => HandlePost(x));
            Receive<PostSaved>(x => HandlePostSavedAck(x));
        }
        
        private void HandlePost(Post x)
        {
            var savePost = new SavePost(Sender.Path, x);

            _child.Tell(savePost);
        }

        private void HandlePostSavedAck(PostSaved saved)
        {
            var ack = new PostAck(saved.Id);

            Context.ActorSelection(saved.From).Tell(ack);
        }

        private int IncrementId()
        {
            id++;

            return id;
        }
    }
}
