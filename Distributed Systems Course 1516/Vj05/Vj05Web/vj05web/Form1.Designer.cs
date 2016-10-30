namespace Vj05Web
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblPojam = new System.Windows.Forms.Label();
            this.lbRezultati = new System.Windows.Forms.ListBox();
            this.txtPojam = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnTrazi = new System.Windows.Forms.Button();
            this.btnDohvatiStranicu = new System.Windows.Forms.Button();
            this.rtbStranica = new System.Windows.Forms.RichTextBox();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // lblPojam
            // 
            this.lblPojam.AutoSize = true;
            this.lblPojam.Location = new System.Drawing.Point(6, 5);
            this.lblPojam.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPojam.Name = "lblPojam";
            this.lblPojam.Size = new System.Drawing.Size(113, 13);
            this.lblPojam.TabIndex = 0;
            this.lblPojam.Text = "Pojam za pretrazivanje";
            // 
            // lbRezultati
            // 
            this.lbRezultati.FormattingEnabled = true;
            this.lbRezultati.Location = new System.Drawing.Point(6, 77);
            this.lbRezultati.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.lbRezultati.Name = "lbRezultati";
            this.lbRezultati.Size = new System.Drawing.Size(118, 238);
            this.lbRezultati.TabIndex = 1;
            // 
            // txtPojam
            // 
            this.txtPojam.Location = new System.Drawing.Point(6, 27);
            this.txtPojam.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtPojam.Name = "txtPojam";
            this.txtPojam.Size = new System.Drawing.Size(118, 20);
            this.txtPojam.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 50);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Rezultati:";
            // 
            // btnTrazi
            // 
            this.btnTrazi.Location = new System.Drawing.Point(8, 322);
            this.btnTrazi.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnTrazi.Name = "btnTrazi";
            this.btnTrazi.Size = new System.Drawing.Size(113, 22);
            this.btnTrazi.TabIndex = 4;
            this.btnTrazi.Text = "Prertazivanje";
            this.btnTrazi.UseVisualStyleBackColor = true;
            this.btnTrazi.Click += new System.EventHandler(this.btnTrazi_Click);
            // 
            // btnDohvatiStranicu
            // 
            this.btnDohvatiStranicu.Location = new System.Drawing.Point(8, 355);
            this.btnDohvatiStranicu.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnDohvatiStranicu.Name = "btnDohvatiStranicu";
            this.btnDohvatiStranicu.Size = new System.Drawing.Size(113, 22);
            this.btnDohvatiStranicu.TabIndex = 5;
            this.btnDohvatiStranicu.Text = "Dohvati stranicu";
            this.btnDohvatiStranicu.UseVisualStyleBackColor = true;
            this.btnDohvatiStranicu.Click += new System.EventHandler(this.btnDohvatiStranicu_Click);
            // 
            // rtbStranica
            // 
            this.rtbStranica.BackColor = System.Drawing.Color.White;
            this.rtbStranica.Location = new System.Drawing.Point(128, 27);
            this.rtbStranica.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rtbStranica.Name = "rtbStranica";
            this.rtbStranica.Size = new System.Drawing.Size(802, 223);
            this.rtbStranica.TabIndex = 6;
            this.rtbStranica.Text = "";
            // 
            // webBrowser
            // 
            this.webBrowser.Location = new System.Drawing.Point(128, 255);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(802, 295);
            this.webBrowser.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(941, 562);
            this.Controls.Add(this.webBrowser);
            this.Controls.Add(this.rtbStranica);
            this.Controls.Add(this.btnDohvatiStranicu);
            this.Controls.Add(this.btnTrazi);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPojam);
            this.Controls.Add(this.lbRezultati);
            this.Controls.Add(this.lblPojam);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPojam;
        private System.Windows.Forms.ListBox lbRezultati;
        private System.Windows.Forms.TextBox txtPojam;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnTrazi;
        private System.Windows.Forms.Button btnDohvatiStranicu;
        private System.Windows.Forms.RichTextBox rtbStranica;
        private System.Windows.Forms.WebBrowser webBrowser;
    }
}

