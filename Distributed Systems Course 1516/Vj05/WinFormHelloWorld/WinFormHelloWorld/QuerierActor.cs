using Akka.Actor;
using System;
using SharedMessages;
using System.Windows.Forms;

namespace WinFormHelloWorld
{
    class QuerierActor : ReceiveActor
    {
        private string _adresaServera = "akka.tcp://MyRemoteSystem@localhost:12000/user/replier";

        private RichTextBox _rtb;
        private ActorSelection _actorNaServeru;

        public QuerierActor(RichTextBox rtb)
        {
            _rtb = rtb;
            _actorNaServeru = Context.ActorSelection(_adresaServera);

            Receive<Send>(x => Send(x));
            Receive<Answer>(x => ProcessAnswer(x));
        }

        private void Send(Send send)
        {
            var query = new Query(send.Text);

            _actorNaServeru.Tell(query);
        }

        private void ProcessAnswer(Answer ans)
        {
            _rtb.Text = "Answer from server " + ans.Text;
        }
    }
}
