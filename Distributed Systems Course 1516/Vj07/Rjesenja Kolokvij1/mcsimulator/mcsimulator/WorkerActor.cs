using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCSimulator
{
    class WorkerActor : ReceiveActor
    {
        private Random _rnd;
        private int _upperBound;

        public WorkerActor(int seed, int upperBound)
        {
            _upperBound = upperBound;
            _rnd = new Random(seed);

            Receive<InfoForWorker>(x => ProcessStartWorker(x.NumGen));
        }

        private void ProcessStartWorker(int genNum)
        {
            int x = _rnd.Next(0, _upperBound);
            int y = _rnd.Next(0, _upperBound);

            Point point = new Point(x, y);

            List<Point> points = new List<Point> { point };

            PointsMsg msg = new PointsMsg(points);

            Context.Parent.Tell(msg);

            genNum = genNum - 1;

            if (genNum == 0) Context.Self.Tell(PoisonPill.Instance);
            else {

                Context.System.Scheduler.ScheduleTellOnce(TimeSpan.FromMilliseconds(200), Self, new InfoForWorker(genNum), Self);
            }
        }
    }
}
