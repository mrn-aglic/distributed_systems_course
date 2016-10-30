using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Loggy
{
    class WorkerActor : ReceiveActor
    {
        private int LamportClock = 0;

        private Random _rnd;
        private IActorRef _logger;
        private List<IActorRef> _others;
        private int _sleep;

        public WorkerActor(int seed, int sleep, IActorRef logger)
        {
            _rnd = new Random(seed);

            _logger = logger;

            Receive<WorkerStart>(x => ProcessStart(x, sleep));
        }

        private void ProcessStart(WorkerStart msg, int sleep)
        {
            _others = msg.OtherActors;
            _sleep = sleep;

            // Mijenja ponasanje actora na nacin da 
            Become(NewBehaviour);

            // Pricekat cemo "sleep" prije nego posaljemo novu poruku
            Context.System.Scheduler.ScheduleTellOnce(TimeSpan.FromMilliseconds(sleep), Self, new Loop(), Self);
        }

        private void NewBehaviour()
        {
            Receive<RandomMsg>(x => ProcessRandomMsg(x));
            Receive<Loop>(x => Loop(_others, _sleep));
        }

        private void ProcessRandomMsg(RandomMsg msg)
        {
            // Pravilo Lamporovog sata
            LamportClock = Math.Max(msg.TimeStampMsg.LamportTime, LamportClock) + 1;

            string mojeIme = Self.Path.Name;

            TimeStampMsg timeStampPoruka = new TimeStampMsg(LamportClock, "Hello from " + mojeIme);

            _logger.Tell(new Log(mojeIme, timeStampPoruka));
        }

        private void Loop(List<IActorRef> others, int sleep)
        {
            IActorRef actor = SelectOne(others);

            // Pravilo Lamportovog sata
            LamportClock++;

            // Ime actora mozemo dohvatiti uz pomoc putanje
            string mojeIme = Self.Path.Name;

            TimeStampMsg timeStampPoruka = new TimeStampMsg(LamportClock, "Hello from " + mojeIme);

            RandomMsg randommsg = new RandomMsg(_rnd.Next(2000), timeStampPoruka);

            actor.Tell(randommsg);

            Thread.Sleep(_rnd.Next(100));

            // Posaljimo poruku loggeru da logira trenutno stanje
            _logger.Tell(new Log(mojeIme, timeStampPoruka));

            // Pricekat cemo "sleep" prije nego posaljemo novu poruku
            Context.System.Scheduler.ScheduleTellOnce(TimeSpan.FromMilliseconds(sleep), Self, new Loop(), Self);
        }

        private IActorRef SelectOne(List<IActorRef> others)
        {
            int br = _rnd.Next(0, others.Count);

            return others[br];
        }
    }
}
