namespace Updater
{
    partial class frmLauncher
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
            this.pbBtn = new System.Windows.Forms.PictureBox();
            this.pbStatusBar = new System.Windows.Forms.PictureBox();
            this.lblStatus = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbStatusBar)).BeginInit();
            this.SuspendLayout();
            // 
            // pbBtn
            // 
            this.pbBtn.Location = new System.Drawing.Point(575, 518);
            this.pbBtn.Name = "pbBtn";
            this.pbBtn.Size = new System.Drawing.Size(214, 70);
            this.pbBtn.TabIndex = 0;
            this.pbBtn.TabStop = false;
            this.pbBtn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbBtn_MouseDown);
            this.pbBtn.MouseEnter += new System.EventHandler(this.pbBtn_MouseEnter);
            this.pbBtn.MouseLeave += new System.EventHandler(this.pbBtn_MouseLeave);
            this.pbBtn.MouseHover += new System.EventHandler(this.pbBtn_MouseHover);
            this.pbBtn.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbBtn_MouseUp);
            // 
            // pbStatusBar
            // 
            this.pbStatusBar.BackColor = System.Drawing.Color.Transparent;
            this.pbStatusBar.Location = new System.Drawing.Point(12, 567);
            this.pbStatusBar.Name = "pbStatusBar";
            this.pbStatusBar.Size = new System.Drawing.Size(17, 23);
            this.pbStatusBar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbStatusBar.TabIndex = 1;
            this.pbStatusBar.TabStop = false;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblStatus.ForeColor = System.Drawing.Color.White;
            this.lblStatus.Location = new System.Drawing.Point(12, 548);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(10, 13);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.Text = " ";
            // 
            // frmLauncher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(796, 602);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.pbStatusBar);
            this.Controls.Add(this.pbBtn);
            this.MaximumSize = new System.Drawing.Size(812, 640);
            this.MinimumSize = new System.Drawing.Size(812, 640);
            this.Name = "frmLauncher";
            this.Text = "Game Launcher";
            this.Load += new System.EventHandler(this.frmLauncher_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbStatusBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbBtn;
        private System.Windows.Forms.PictureBox pbStatusBar;
        private System.Windows.Forms.Label lblStatus;
    }
}

