using System.Collections.Generic;
using System.Drawing;

namespace MCSimulator
{
    class Start
    {
        // Broj actora za stvoriti
        public int NumActors { get; private set; }
        // Broj generiranja po actoru
        public int NumGen { get; private set; }

        public Start(int numActors, int numGen)
        {
            NumActors = numActors;
            NumGen = numGen;
        }
    }

    class InfoForWorker
    {
        public int NumGen { get; private set; }

        public InfoForWorker(int numGen)
        {
            NumGen = numGen;
        }
    }

    class PointsMsg
    {
        public IEnumerable<Point> Points { get; private set; }

        public PointsMsg(IEnumerable<Point> points)
        {
            Points = points;
        }
    }
}
