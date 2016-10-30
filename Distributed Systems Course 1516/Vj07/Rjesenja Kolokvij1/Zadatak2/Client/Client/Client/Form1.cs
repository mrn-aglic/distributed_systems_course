using Akka.Actor;
using Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Form1 : Form
    {
        private IActorRef _actor;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            _actor.Tell(new Update());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Props props = Props.Create(() => new RequestActor(listBox1, btnSend)).WithDispatcher("akka.actor.synchronized-dispatcher");

            _actor = Program.System.ActorOf(props, "requester");
        }
    }
}
