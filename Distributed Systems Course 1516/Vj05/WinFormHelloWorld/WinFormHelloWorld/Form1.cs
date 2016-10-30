using Akka.Actor;
using SharedMessages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormHelloWorld
{
    public partial class Form1 : Form
    {
        private IActorRef _queryActor;

        public Form1()
        {
            InitializeComponent();
        }
        
        private void btnSend_Click(object sender, EventArgs e)
        {
            Send send = new Send(rtbporuka.Text);

            _queryActor.Tell(send);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var props = Props.Create(() => new QuerierActor(rtbResult)).WithDispatcher("akka.actor.synchronized-dispatcher");

            _queryActor = Program.System.ActorOf(props, "querier");
        }
    }
}
