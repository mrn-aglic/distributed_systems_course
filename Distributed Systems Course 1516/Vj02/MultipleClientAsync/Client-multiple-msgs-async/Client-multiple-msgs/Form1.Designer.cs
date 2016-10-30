namespace Client_multiple_msgs
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
            this.txtMsg = new System.Windows.Forms.TextBox();
            this.lblPoruka = new System.Windows.Forms.Label();
            this.btnSend = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lstMsgs = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // txtMsg
            // 
            this.txtMsg.Location = new System.Drawing.Point(59, 75);
            this.txtMsg.Name = "txtMsg";
            this.txtMsg.Size = new System.Drawing.Size(100, 20);
            this.txtMsg.TabIndex = 0;
            // 
            // lblPoruka
            // 
            this.lblPoruka.AutoSize = true;
            this.lblPoruka.Location = new System.Drawing.Point(12, 78);
            this.lblPoruka.Name = "lblPoruka";
            this.lblPoruka.Size = new System.Drawing.Size(41, 13);
            this.lblPoruka.TabIndex = 1;
            this.lblPoruka.Text = "Poruka";
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(15, 171);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(144, 23);
            this.btnSend.TabIndex = 2;
            this.btnSend.Text = "Posalji";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(15, 212);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(144, 23);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lstMsgs
            // 
            this.lstMsgs.FormattingEnabled = true;
            this.lstMsgs.Location = new System.Drawing.Point(196, 13);
            this.lstMsgs.Name = "lstMsgs";
            this.lstMsgs.Size = new System.Drawing.Size(397, 277);
            this.lstMsgs.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(605, 311);
            this.Controls.Add(this.lstMsgs);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.lblPoruka);
            this.Controls.Add(this.txtMsg);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtMsg;
        private System.Windows.Forms.Label lblPoruka;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ListBox lstMsgs;
    }
}

