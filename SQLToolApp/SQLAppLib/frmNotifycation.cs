using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SQLAppLib
{
    public partial class frmNotifycation : Form
    {
        public static bool isMoving = false;
        public frmNotifycation()
        {
            InitializeComponent();
        }
        public void SetCaption(string caption)
        {
            lblNotifycation.Text = caption;
        }
        private void frmNotifycation_Load(object sender, EventArgs e)
        {
            timerNotifycation.Enabled = true;
            timerNotifycation.Interval = 18;
            timerNotifycation.Tick += timerNotifycation_Tick;
        }

        void timerNotifycation_Tick(object sender, EventArgs e)
        {
            if (lblNotifycation.Top >= 40)
                isMoving = true;

            if (isMoving)
            {
                lblNotifycation.Top -= 2;
            }
            else
            {
                lblNotifycation.Top += 2;
            }

            if (isMoving && lblNotifycation.Top == 0)
            {
                this.Close();
                isMoving = false;
                timerNotifycation.Enabled = false;
            }
                
        }

        private void frmNotifycation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
                lblNotifycation.Top -= 5;

            if (e.KeyCode == Keys.S)
                lblNotifycation.Top += 5;

            if (e.KeyCode == Keys.A)
                lblNotifycation.Left -= 5;

            if (e.KeyCode == Keys.D)
                lblNotifycation.Left += 5;

            #region Key Esc

            if (e.KeyCode == (Keys.Back | Keys.RButton | Keys.LButton | Keys.ShiftKey))
            {
                this.Close();
            }

            #endregion
        }
    }
}
