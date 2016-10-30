using Akka.Actor;
using System;
using System.Windows.Forms;

namespace MCSimulator
{
    class WriterActor : ReceiveActor
    {
        private MyPictureBox _pictureBox;
        private Label _lblResult;
        private Label _lblPointsCreated;

        public WriterActor(MyPictureBox pictureBox, Label lblResult, Label lblPointsCreated)
        {
            _pictureBox = pictureBox;
            _lblResult = lblResult;
            _lblPointsCreated = lblPointsCreated;

            Receive<Start>(x => Start(x.NumActors, x.NumGen));
            Receive<PointsMsg>(x => DrawPoints(x));
        }

        private void Start(int numActors, int numGen)
        {
            Random rnd = new Random();

            for(int i = 0; i < numActors; i++)
            {
                int j = i * rnd.Next();

                Props props = Props.Create(() => new WorkerActor(j, _pictureBox.Width));

                var actor = Context.ActorOf(props);

                actor.Tell(new InfoForWorker(numGen));
            }
        }

        private void DrawPoints(PointsMsg x)
        {
            _pictureBox.AddRange(x.Points);
            _lblResult.Text = ProcjeniPi().ToString();
            _lblPointsCreated.Text = _pictureBox.AllPoints.ToString();
        }

        private double ProcjeniPi()
        {
            return 4 * ((double)_pictureBox.InsideCirclePoints / _pictureBox.AllPoints);
        }
    }
}
