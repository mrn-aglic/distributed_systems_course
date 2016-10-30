using Akka.Actor;
using Akka.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormHelloWorld
{
    static class Program
    {
        public static ActorSystem System;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
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
                       
                            port = 12000
                            hostname = localhost
                        } 
                    }
                }
            ");

            System = ActorSystem.Create("ClientWinFormSystem", config);
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
