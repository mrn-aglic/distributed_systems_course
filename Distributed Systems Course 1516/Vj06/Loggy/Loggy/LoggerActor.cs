using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Loggy
{
    class LoggerActor : ReceiveActor
    {
        private ListBox _lb;

        // Korisnik moze poslati vise poruka, a za svaku nam je bitan Lamportov sat i sadrzaj poruke
        private Dictionary<string, List<TimeStampMsg>> _userMessages = new Dictionary<string, List<TimeStampMsg>>();

        public LoggerActor(string[] names, ListBox lb)
        {
            _lb = lb;

            // instancirajmo odmah sve liste u _userMessages na prazne liste kako ne bismo morali brinuti o 
            // null reference exception i provjeravati za null-ove
            foreach(var name in names)
            {
                // Ovako se dodaje element u dictionary
                _userMessages.Add(name, new List<TimeStampMsg>());
            }

            Receive<Log>(x => ProcessLog(x));
        }

        private void ProcessLog(Log log)
        {
            // Izvucimo ime iz poruke
            string name = log.Name;

            // Izvucimo Lamportov sat i poruku
            TimeStampMsg msg = log.TimeStampMsg;

            // Provjerimo jesmo li primili vec poruku od ovoga korisnika
            if (_userMessages.ContainsKey(name) && _userMessages[name].Count > 0)
            {
                // ako jesmo
                List<TimeStampMsg> listaKorisnikovihPoruka = _userMessages[name];

                // Prvo provjerimo je li novo primljeno vrijeme vece od starog:
                // Dohvatimo maksimalno vrijeme koje se vec nalazi u nekoj od poruka u listi:
                int lastTime = listaKorisnikovihPoruka.Max(x => x.LamportTime);

                // trenutno vrijeme je ono koje smo primili
                int currentTime = msg.LamportTime;

                // ako je trenutno vrijeme vece od onog koje vec imamo, dodajmo element
                if(currentTime > lastTime)
                {
                    // dodajmo novu poruku
                    listaKorisnikovihPoruka.Add(msg);
                }
            }
            else
            {
                // u suprotnom (stvaranje liste s jednim elementom - msg)
                _userMessages[name] = new List<TimeStampMsg> { msg };
            }

            LogIt(_userMessages);
        }

        private void LogIt(Dictionary<string, List<TimeStampMsg>> usermsgs)
        {
            // Provjerimo da u za svakog usera postoji barem jedna primljena poruka
            bool jeLiIstina = usermsgs.Values.All(x => x.Any());

            if (jeLiIstina)
            {
                // Ako je istina, pronadi poruku sa najmanjom vremenskom oznakom, ali i ime korisnika
                var par = usermsgs.Aggregate((x, y) =>
                {
                    if(x.Value.Min(a => a.LamportTime) < y.Value.Min(b => b.LamportTime))
                    {
                        return x;
                    }
                    else
                    {
                        return y;
                    }
                });

                // najmanja pronadjena vrijednost sata za korisnika
                var min = par.Value.Min(x => x.LamportTime);

                // poruka sa najmanjom vrijednoscu
                var msg = par.Value.First(x => x.LamportTime == min);

                // Slozi poruku za isprintati
                var msgPrint = "[Log]: " + par.Key + " " + msg.LamportTime + " " + msg.Text;

                // Ispisi poruku
                _lb.Items.Add(msgPrint);

                // izbrisi logiranu poruku
                usermsgs[par.Key].Remove(msg);

                // Ponovno pozovemo metodu Log
                LogIt(usermsgs);
            }
        }
    }
}
