namespace MCSimulator
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
            this.label1 = new System.Windows.Forms.Label();
            this.lblResult = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.txtNumActors = new System.Windows.Forms.TextBox();
            this.lblBrojActora = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBrojGeneriranja = new System.Windows.Forms.TextBox();
            this.myPictureBox = new MCSimulator.MyPictureBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblPointsCreated = new System.Windows.Forms.Label();
            this.lblNumPoints = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.myPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 58);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "PI approx. = ";
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Location = new System.Drawing.Point(406, 58);
            this.lblResult.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(30, 25);
            this.lblResult.TabIndex = 2;
            this.lblResult.Text = "...";
            // 
            // btnStart
            // 
            this.btnStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnStart.Location = new System.Drawing.Point(832, 269);
            this.btnStart.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(438, 138);
            this.btnStart.TabIndex = 3;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // txtNumActors
            // 
            this.txtNumActors.Location = new System.Drawing.Point(1020, 135);
            this.txtNumActors.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtNumActors.Name = "txtNumActors";
            this.txtNumActors.Size = new System.Drawing.Size(246, 31);
            this.txtNumActors.TabIndex = 4;
            // 
            // lblBrojActora
            // 
            this.lblBrojActora.AutoSize = true;
            this.lblBrojActora.Location = new System.Drawing.Point(842, 148);
            this.lblBrojActora.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblBrojActora.Name = "lblBrojActora";
            this.lblBrojActora.Size = new System.Drawing.Size(122, 25);
            this.lblBrojActora.TabIndex = 5;
            this.lblBrojActora.Text = "Broj actora:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(842, 202);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(170, 25);
            this.label2.TabIndex = 6;
            this.label2.Text = "Broj generiranja:";
            // 
            // txtBrojGeneriranja
            // 
            this.txtBrojGeneriranja.Location = new System.Drawing.Point(1020, 196);
            this.txtBrojGeneriranja.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtBrojGeneriranja.Name = "txtBrojGeneriranja";
            this.txtBrojGeneriranja.Size = new System.Drawing.Size(246, 31);
            this.txtBrojGeneriranja.TabIndex = 7;
            // 
            // myPictureBox
            // 
            this.myPictureBox.Location = new System.Drawing.Point(22, 142);
            this.myPictureBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.myPictureBox.Name = "myPictureBox";
            this.myPictureBox.Size = new System.Drawing.Size(800, 769);
            this.myPictureBox.TabIndex = 8;
            this.myPictureBox.TabStop = false;
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnExit.Location = new System.Drawing.Point(832, 756);
            this.btnExit.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(438, 156);
            this.btnExit.TabIndex = 9;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblPointsCreated
            // 
            this.lblPointsCreated.AutoSize = true;
            this.lblPointsCreated.Location = new System.Drawing.Point(24, 99);
            this.lblPointsCreated.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblPointsCreated.Name = "lblPointsCreated";
            this.lblPointsCreated.Size = new System.Drawing.Size(156, 25);
            this.lblPointsCreated.TabIndex = 10;
            this.lblPointsCreated.Text = "Points created:";
            // 
            // lblNumPoints
            // 
            this.lblNumPoints.AutoSize = true;
            this.lblNumPoints.Location = new System.Drawing.Point(406, 99);
            this.lblNumPoints.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblNumPoints.Name = "lblNumPoints";
            this.lblNumPoints.Size = new System.Drawing.Size(30, 25);
            this.lblNumPoints.TabIndex = 11;
            this.lblNumPoints.Text = "...";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1298, 873);
            this.Controls.Add(this.lblNumPoints);
            this.Controls.Add(this.lblPointsCreated);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.myPictureBox);
            this.Controls.Add(this.txtBrojGeneriranja);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblBrojActora);
            this.Controls.Add(this.txtNumActors);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.myPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.TextBox txtNumActors;
        private System.Windows.Forms.Label lblBrojActora;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBrojGeneriranja;
        private MyPictureBox myPictureBox;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lblPointsCreated;
        private System.Windows.Forms.Label lblNumPoints;
    }
}

