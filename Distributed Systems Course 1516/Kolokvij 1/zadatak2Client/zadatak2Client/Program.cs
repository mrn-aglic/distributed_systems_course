using Akka.Actor;
using Akka.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace zadatak2Client
{
    static class Program
    {
        public static ActorSystem system { get; private set; }

        [STAThread]
        static void Main()
        {
            var config = ConfigurationFactory.ParseString(@"
                                akka {
                                    actor {
                                        provider = ""Akka.Remote.RemoteActorRefProvider, Akka.Remote""
                                    }

                                    remote {
                                        helios.tcp {
                                            port = 0
                                            hostname = localhost
                                        }
                                    }
                                }
                                ");

            system = ActorSystem.Create("clientSystem", config);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
