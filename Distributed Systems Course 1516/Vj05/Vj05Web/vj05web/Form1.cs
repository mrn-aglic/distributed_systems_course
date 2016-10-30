using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vj05Web
{
    public partial class Form1 : Form
    {
        RemoteQueries queries = new RemoteQueries();

        public Form1()
        {
            InitializeComponent();
        }

        private async void btnTrazi_Click(object sender, EventArgs e)
        {
            lbRezultati.Items.Clear();

            List<string> results = await queries.GetWikipediaSearchResults(txtPojam.Text);

            results.ForEach(x => lbRezultati.Items.Add(x));
        }

        private async void btnDohvatiStranicu_Click(object sender, EventArgs e)
        {
            if (lbRezultati.SelectedIndex < 0) return;
            else
            {
                string expression = lbRezultati.SelectedItem.ToString().Replace(" ", "_");

                WebPage webPage = await queries.GetWebPage(expression);

                rtbStranica.Text = webPage.Text;
                webBrowser.DocumentText = webPage.Text;
            }
        }
    }
}
