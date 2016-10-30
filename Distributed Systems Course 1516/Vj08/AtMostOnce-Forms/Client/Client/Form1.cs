using Akka.Actor;
using Messages;
using System;
using System.Windows.Forms;

namespace Client
{
    public partial class Form1 : Form
    {
        private IActorRef _requester;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Props props = Props.Create(() => new RequesterActor()); ///.WithDispatcher("akka.actor.synchronized-dispatcher");

            _requester = Program.system.ActorOf(props, "client");
        }

        private void btnDeposit_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txtAmount.Text))
            {
                return;
            }
            else
            {
                int amount = int.Parse(txtAmount.Text);

                lblExpected.Text = (int.Parse(lblExpected.Text) + amount).ToString();

                _requester.Tell(new Deposit(amount));
            }
        }
    }
}
