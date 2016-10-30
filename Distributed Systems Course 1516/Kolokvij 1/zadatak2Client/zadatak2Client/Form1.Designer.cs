namespace zadatak2Client
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
            this.lstBox = new System.Windows.Forms.ListBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lstBox
            // 
            this.lstBox.FormattingEnabled = true;
            this.lstBox.ItemHeight = 25;
            this.lstBox.Location = new System.Drawing.Point(12, 12);
            this.lstBox.Name = "lstBox";
            this.lstBox.Size = new System.Drawing.Size(797, 254);
            this.lstBox.TabIndex = 0;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(371, 272);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(111, 47);
            this.btnSend.TabIndex = 1;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(821, 432);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.lstBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstBox;
        private System.Windows.Forms.Button btnSend;
    }
}

