using Akka.Actor;
using System;

namespace AkkaPingPong
{
    class PongActor : ReceiveActor
    {
        private int _brojac;

        public PongActor()
        {
            _brojac = 0;

            Receive<Pong>(p => WriteAndSendResponse(p));
        }

        private void WriteAndSendResponse(Pong p)
        {
            _brojac++;

            Console.WriteLine("Received PONG");

            Sender.Tell(new Ping());
        }
    }
}
