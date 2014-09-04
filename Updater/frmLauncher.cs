using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Updater
{
    public partial class frmLauncher : Form
    {

        public string sPath;
        public Bitmap hover;
        public Bitmap btn;
        public Bitmap click;
        public Bitmap disabled;
        public Bitmap StatusBar;

        public bool en;
        private config configuration;
        private int MaxStatusBarSize;

        public frmLauncher()
        {

            
            InitializeComponent();
            this.sPath = Application.StartupPath;
            hover = (Bitmap)Image.FromFile(this.sPath + @"\launcher\btn_hover.png");
            btn = (Bitmap)Image.FromFile(this.sPath + @"\launcher\btn.png");
            click = (Bitmap)Image.FromFile(this.sPath + @"\launcher\btn_mousedown.png");
            disabled = (Bitmap)Image.FromFile(this.sPath + @"\launcher\btn_disabled.png");
            this.en = true;
            this.configuration = new config();
            this.StatusBar = (Bitmap)Image.FromFile(this.sPath + @"\launcher\barr.png");
            this.MaxStatusBarSize = 543;
            if (File.Exists(sPath + @"\launcher\updates.json")) File.Delete(sPath + @"\launcher\updates.json");
        }

        private void frmLauncher_Load(object sender, EventArgs e)
        {
            this.BackgroundImage = Image.FromFile(this.sPath + @"\launcher\background.png");
            this.pbBtn.Image = btn;
            this.pbStatusBar.Image = StatusBar;
            this.pbStatusBar.Width = 0;
        }

        private void pbBtn_MouseHover(object sender, EventArgs e)
        {
            
        }

        private void pbBtn_MouseLeave(object sender, EventArgs e)
        {
            if(en) this.pbBtn.Image = btn;
        }

        private void pbBtn_MouseDown(object sender, MouseEventArgs e)
        {
            if (en) this.pbBtn.Image = click;
        }

        private void pbBtn_MouseEnter(object sender, EventArgs e)
        {
            if (en) this.pbBtn.Image = hover;
        }

        private void pbBtn_MouseUp(object sender, MouseEventArgs e)
        {
            if (en)
            {
                this.pbBtn.Image = disabled;
                this.en = false;
                updater update = new updater(this);
            }
        }

        private void StartUpdate()
        {
        }

        public delegate void UpdateStatusMessage(string message);
        public delegate void UpdateStatusPercentage(float percentage);

        public void UpdateStatusBar(float percentage)
        {
            if (InvokeRequired)
            {
                Invoke(new UpdateStatusPercentage(UpdateStatusBar), new object[] { percentage });
            }
            else
            {
                this.pbStatusBar.Width = (int)((MaxStatusBarSize) * (percentage));
            }
        }

        public void UpdateStatus(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new UpdateStatusMessage(UpdateStatus), new object[] { message });
            }
            else
            {
                this.lblStatus.Text = message;
            }
        }
    }
}
