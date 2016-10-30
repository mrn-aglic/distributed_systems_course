using Akka.Actor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Loggy
{
    public partial class Form1 : Form
    {
        private IActorRef _logger;
        private string[] names = new[] { "john", "george", "ringo", "paul" };
        private Dictionary<string, IActorRef> _actors = new Dictionary<string, IActorRef>();

        public Form1()
        {
            InitializeComponent();

            var loggerProps = Props.Create(() => new LoggerActor(names, lstBox))
                .WithDispatcher("akka.actor.synchronized-dispatcher"); 

            _logger = Program.System.ActorOf(loggerProps, "Logger");
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            btnSend.Enabled = false;

            foreach(var el in names)
            {
                var props = Props.Create(() => new WorkerActor((int)el[0], 2000, _logger));

                // el je ime actora
                var actor = Program.System.ActorOf(props, el);

                _actors.Add(el, actor);
            }

            foreach(var actor in _actors.Values)
            {
                // dovavimo listu actora koje trenutni actor mora poznavati
                List<IActorRef> others = _actors.Values.Where(x => x.Path != actor.Path).ToList();

                // kreiramo poruku
                WorkerStart msg = new WorkerStart(others);

                // posaljimo trenutnom actoru poruku da se pokrene
                actor.Tell(msg);
            }
        }
    }
}
