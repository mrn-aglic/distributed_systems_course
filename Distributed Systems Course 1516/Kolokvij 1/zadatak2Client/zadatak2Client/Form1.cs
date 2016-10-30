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

namespace zadatak2Client
{
    public partial class Form1 : Form
    {
        IActorRef _greedy;

        public Form1()
        {
            InitializeComponent();

            Props props = Props.Create(() => new GreedyActor(lstBox)).WithDispatcher("akka.actor.synchronized-dispatcher");

            _greedy = Program.system.ActorOf(props);

            _greedy.Tell(new Register());
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            _greedy.Tell(new StartRequest());  
        }
    }
}
