using Akka.Actor;
using Messages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace zadatak2Server
{
    class MainActor : ReceiveActor
    {
        private string MyResource = "Ovo je neki resurs!";

        private List<IActorRef> _registered = new List<IActorRef>();

        public MainActor()
        {
            Receive<Register>(x => ProcessRegister());
            Receive<Update>(x => ProcessUpdate(x));
        }

        private void ProcessRegister()
        {
            _registered.Add(Context.Sender);

            var t = new IActorRef[_registered.Count];

            _registered.CopyTo(t);

            _registered.ForEach(x =>
                x.Tell(new RegisterAck(t.ToList().Where(z => z.Path != x.Path).ToList()))
            );

            _registered.ForEach(Console.WriteLine);
        }

        private void IspisiStanjeResursa()
        {
            Console.WriteLine("[Stanje resursa]: " + MyResource);
        }

        private void ProcessUpdate(Update x)
        {
            MyResource = MyResource + x.Clock;

            var answer = MyResource + " [Updated by]: " + x.Text + "  with clock: " + x.Clock;

            IspisiStanjeResursa();

            Context.Sender.Tell(new UpdateAck(answer));
        }
    }
}
