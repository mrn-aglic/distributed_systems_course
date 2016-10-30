namespace WinFormHelloWorld
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
            this.rtbporuka = new System.Windows.Forms.RichTextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.rtbResult = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // rtbporuka
            // 
            this.rtbporuka.Location = new System.Drawing.Point(12, 12);
            this.rtbporuka.Name = "rtbporuka";
            this.rtbporuka.Size = new System.Drawing.Size(547, 216);
            this.rtbporuka.TabIndex = 0;
            this.rtbporuka.Text = "";
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(12, 303);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(164, 93);
            this.btnSend.TabIndex = 1;
            this.btnSend.Text = "Posalji";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(217, 303);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(201, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Posljednja primljena poruka";
            // 
            // rtbResult
            // 
            this.rtbResult.Enabled = false;
            this.rtbResult.Location = new System.Drawing.Point(221, 341);
            this.rtbResult.Name = "rtbResult";
            this.rtbResult.Size = new System.Drawing.Size(338, 139);
            this.rtbResult.TabIndex = 3;
            this.rtbResult.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 492);
            this.Controls.Add(this.rtbResult);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.rtbporuka);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbporuka;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox rtbResult;
    }
}

