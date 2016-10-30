using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client_multiple_msgs
{
    public partial class Form1 : Form
    {
        string ip = "127.0.0.1";
        int port = 8080;
        Client client;

        Task receiveTask;

        CancellationTokenSource cts = new CancellationTokenSource();
        CancellationToken ct;

        public Form1()
        {
            InitializeComponent();

            ct = cts.Token;
            client = new Client(ip, port, ct);

            // Pokusamo li bez progressa izmijenit element na sucelju dobiti cemo pogresku
            IProgress<string> progress = new Progress<string>(x => lstMsgs.Items.Add(x));
            receiveTask = Task.Factory.StartNew(() => Receive(progress));
        }

        private void Receive(IProgress<string> progress)
        {
            while(!ct.IsCancellationRequested)
            {
                string msg = client.Read();

                string longMsg = DateTime.Now + " : " + msg;

                progress.Report(longMsg);
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            client.Write(txtMsg.Text);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            client.Close();
            Application.Exit();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            client.Close();
            
            base.OnFormClosing(e);
        }
    }
}
