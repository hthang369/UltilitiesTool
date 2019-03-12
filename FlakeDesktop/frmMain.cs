using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlakeDesktop
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }
        private void OnLoad(object sender, EventArgs e)
        {
            Rectangle rcWorrk = Screen.PrimaryScreen.WorkingArea;
            int iWidth = rcWorrk.Width;
            int iTimer = 5;
            int iPosX = 0;

            Random r = new Random();
            for (int i = 0; i < 10; i++)
            {
                iTimer = r.Next(50);
                iPosX = r.Next(iWidth);

                frmFlake fd = new frmFlake(iTimer, iPosX);
                fd.Show();
            }
        }
        private void OnTrayIcon(object sender, MouseEventArgs e)
        {
            if (e.Button.Equals(MouseButtons.Left))
            {
                frmAbout fAbout = new frmAbout();
                fAbout.ShowDialog();
            }
        }
        private void OnExit(object sender, EventArgs e)
        {
            this.Close();
        }
        private void OnAbout(object sender, EventArgs e)
        {
            frmAbout fAbout = new frmAbout();
            fAbout.ShowDialog();
        }
    }
}
