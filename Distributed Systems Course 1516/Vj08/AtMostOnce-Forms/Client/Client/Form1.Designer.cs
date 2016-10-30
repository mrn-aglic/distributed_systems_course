namespace Client
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
            this.btnDeposit = new System.Windows.Forms.Button();
            this.lblAmount = new System.Windows.Forms.Label();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblExpected = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnDeposit
            // 
            this.btnDeposit.Location = new System.Drawing.Point(91, 88);
            this.btnDeposit.Name = "btnDeposit";
            this.btnDeposit.Size = new System.Drawing.Size(75, 23);
            this.btnDeposit.TabIndex = 0;
            this.btnDeposit.Text = "Deposit";
            this.btnDeposit.UseVisualStyleBackColor = true;
            this.btnDeposit.Click += new System.EventHandler(this.btnDeposit_Click);
            // 
            // lblAmount
            // 
            this.lblAmount.AutoSize = true;
            this.lblAmount.Location = new System.Drawing.Point(12, 40);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(46, 13);
            this.lblAmount.TabIndex = 1;
            this.lblAmount.Text = "Amount:";
            // 
            // txtAmount
            // 
            this.txtAmount.Location = new System.Drawing.Point(91, 32);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(100, 20);
            this.txtAmount.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(196, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Expected amount:";
            // 
            // lblExpected
            // 
            this.lblExpected.AutoSize = true;
            this.lblExpected.Location = new System.Drawing.Point(254, 32);
            this.lblExpected.Name = "lblExpected";
            this.lblExpected.Size = new System.Drawing.Size(13, 13);
            this.lblExpected.TabIndex = 4;
            this.lblExpected.Text = "0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(437, 129);
            this.Controls.Add(this.lblExpected);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtAmount);
            this.Controls.Add(this.lblAmount);
            this.Controls.Add(this.btnDeposit);
            this.Name = "Form1";
            this.Text = "Client";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDeposit;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblExpected;
    }
}

