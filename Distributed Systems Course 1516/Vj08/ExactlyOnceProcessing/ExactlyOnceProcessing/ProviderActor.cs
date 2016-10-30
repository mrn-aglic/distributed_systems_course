using Akka.Actor;

namespace ExactlyOnceProcessing
{
    class ProviderActor : ReceiveActor
    {
        private IActorRef _child;

        private int expectedId = 0;
               
        public ProviderActor()
        {
            Props props = Props.Create(() => new WorkerActor());

            _child = Context.ActorOf(props);

            Receive<Post>(x => x.Id > expectedId, x => { });
            Receive<Post>(x => x.Id < expectedId, x => Sender.Tell(new PostAck(x.Id)));
            Receive<Post>(x=> x.Id == expectedId, x => HandlePost(x));
            Receive<PostSaved>(x => HandlePostSavedAck(x));
        }
        
        private void HandlePost(Post x)
        {
            var savePost = new SavePost(Sender.Path, x);

            _child.Tell(savePost);

            expectedId += 1;
        }

        private void HandlePostSavedAck(PostSaved saved)
        {
            var ack = new PostAck(saved.Id);

            Context.ActorSelection(saved.From).Tell(ack);
        }
    }
}
