using Akka.Actor;
using System;

namespace AkkaPingPong
{
    class PingActor : ReceiveActor
    {
        private int _brojac;

        private IActorRef _myPongPartner;

        public PingActor(IActorRef myPongPartner)
        {
            _brojac = 0;
            _myPongPartner = myPongPartner;

            Receive<Ping>(p => WriteAndSendResponse(p));
        }

        private void WriteAndSendResponse(Ping p)
        {
            _brojac++;

            Console.WriteLine("Received PING");

            _myPongPartner.Tell(new Pong());
        }
    }
}
