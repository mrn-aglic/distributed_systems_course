using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MCSimulator
{
    #region Ovo je implementacija MyPictureBox kontrole, nemojte ju mijenjati!
    public partial class MyPictureBox : PictureBox
    {
        #region Polja i svojstva kontrole
        private List<Point> _points = new List<Point>();

        public int AllPoints { get; private set; }
        public int InsideCirclePoints { get; private set; }
        #endregion

        #region Konstruktor kontrole
        public MyPictureBox()
        {
            InitializeComponent();
        }
        #endregion

        #region Metode kontrole
        protected override void OnPaint(PaintEventArgs pe)
        {
            pe.Graphics.Clear(Color.White);

            Brush b = new SolidBrush(Color.Green);

            using (Pen p = new Pen(b, 2))
            {
                Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);

                foreach (var el in _points)
                {
                    Rectangle r = new Rectangle(el.X, el.Y, 2, 2);

                    Pen red = new Pen(new SolidBrush(Color.Red), 2);

                    pe.Graphics.DrawRectangle(red, r);
                }

                pe.Graphics.DrawEllipse(p, rect);
                pe.Graphics.DrawRectangle(p, rect);
            }

            
            GC.Collect();
            GC.WaitForPendingFinalizers();

            base.OnPaint(pe);
        }

        public void Add(Point point)
        {
            _points.Add(point);
            Refresh();

            AllPoints = AllPoints + 1;

            if(IsWithinCircle(point))
            {
                InsideCirclePoints = InsideCirclePoints + 1;
            }
        }

        public void AddRange(IEnumerable<Point> points)
        {
            _points.AddRange(points);
            Refresh();

            AllPoints = AllPoints + points.Count();
            InsideCirclePoints = InsideCirclePoints + points.Count(x => IsWithinCircle(x));
        }

        [Obsolete]
        public int NumberOfPointsInCircle()
        {
            return _points.Count(x => IsWithinCircle(x));
        }

        private bool IsWithinCircle(Point point)
        {
            int radius = this.Width / 2;

            var x = Math.Pow((point.X - radius), 2);
            var y = Math.Pow((point.Y - radius), 2);

            return x + y < Math.Pow(radius, 2);
        }

        #endregion
    }
    #endregion
}
