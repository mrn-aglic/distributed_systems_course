using Akka.Actor;
using Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    class RequestActor : ReceiveActor
    {
        private string Path = "akka.tcp://Server@127.0.0.1:9090/user/provider";
        private ActorSelection Selection;
        private ListBox _lstBox;
        private Button _btn;

        public RequestActor(ListBox lstBox, Button btn)
        {
            Selection = Context.ActorSelection(Path);

            _lstBox = lstBox;
            _btn = btn;

            Receive<Update>(x => HandleUpdate());
            Receive<UpdateAck>(x => HandleUpdateAck(x));
            Receive<Wait>(x => HandleWait());
        }

        private void HandleUpdate()
        {
            Selection.Tell(new Update());
        }

        private void HandleUpdateAck(UpdateAck x)
        {
            _lstBox.Items.Add(x.Text);
            
            _btn.Enabled = true;
            _lstBox.Enabled = true;
        }

        private void HandleWait()
        {
            _btn.Enabled = false;
            _lstBox.Enabled = false;
        }
    }
}
