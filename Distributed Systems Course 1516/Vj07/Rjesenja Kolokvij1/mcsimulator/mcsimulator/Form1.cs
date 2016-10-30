using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MCSimulator
{
    public partial class Form1 : Form
    {
        private IActorRef _writer;
        private List<Point> _points = new List<Point>();

        public Form1()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, System.EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNumActors.Text) || string.IsNullOrWhiteSpace(txtBrojGeneriranja.Text))
            {
                return;
            }

            int numActors = int.Parse(txtNumActors.Text);
            int genNum = int.Parse(txtBrojGeneriranja.Text);

            _writer.Tell(new Start(numActors, genNum));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Props props = Props.Create(() => new WriterActor(myPictureBox, lblResult, lblNumPoints)).WithDispatcher("akka.actor.synchronized-dispatcher");

            _writer = Program.system.ActorOf(props, "writer");
        }

        #region Ovdje je implementacija Exit botuna koju ne morate mijenjati

        private void btnExit_Click(object sender, EventArgs e)
        {
            Program.system.Terminate().Wait();
            Program.system.WhenTerminated.Wait();

            Application.Exit();
        }

        #endregion
    }
}
