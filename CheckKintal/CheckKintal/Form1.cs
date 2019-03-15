using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheckKintal
{
    public partial class Form1 : Form
    {
        int count = 0;
        string fileName = Application.StartupPath + "\\config.ini";
        string strSection = "CheckKintai";
        string keyCheckInStart = "CheckInStart";
        string keyCheckInEnd = "CheckInEnd";
        string keyCheckOutStart = "CheckOutStart";
        string keyCheckOutEnd = "CheckOutEnd";
        string keyCheckRecheck = "CheckRecheck";
        string keyCheckUrl = "CheckUrl";
        string timeCheckUrl = "";
        public Form1()
        {
            InitializeComponent();
            this.notifyIcon1.Icon = new Icon(Application.StartupPath + "\\calendar.ico");
            this.notifyIcon1.ShowBalloonTip(500, "Thông báo", "OK", ToolTipIcon.Info);
            this.btnStart.Visible = !this.timer1.Enabled;
            this.btnStop.Visible = this.timer1.Enabled;
            string timeCheckInStart = SQLAppLib.SQLApp.GetIniFile(fileName, strSection, keyCheckInStart);
            string timeCheckInEnd = SQLAppLib.SQLApp.GetIniFile(fileName, strSection, keyCheckInEnd);
            string timeCheckOutStart = SQLAppLib.SQLApp.GetIniFile(fileName, strSection, keyCheckOutStart);
            string timeCheckOutEnd = SQLAppLib.SQLApp.GetIniFile(fileName, strSection, keyCheckOutEnd);
            string timeCheckRecheck = SQLAppLib.SQLApp.GetIniFile(fileName, strSection, keyCheckRecheck);
            timeCheckUrl = SQLAppLib.SQLApp.GetIniFile(fileName, strSection, keyCheckUrl);
            txtCheckInStart.Value = Convert.ToDateTime(timeCheckInStart);
            txtCheckInEnd.Value = Convert.ToDateTime(timeCheckInEnd);
            txtCheckOutStart.Value = Convert.ToDateTime(timeCheckOutStart);
            txtCheckOutEnd.Value = Convert.ToDateTime(timeCheckOutEnd);
            txtRecheck.Value = Convert.ToInt32(timeCheckRecheck);
            txtUrl.Text = timeCheckUrl;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            int timeSystem = TotalMinute(dt);
            int iCheckInStart = TotalMinute(Convert.ToDateTime(txtCheckInStart.Text));
            int iCheckInEnd = TotalMinute(Convert.ToDateTime(txtCheckInEnd.Text));
            int iCheckOutStart = TotalMinute(Convert.ToDateTime(txtCheckOutStart.Text));
            int iCheckOutEnd = TotalMinute(Convert.ToDateTime(txtCheckOutEnd.Text));
            if ((iCheckInStart <= timeSystem) && (timeSystem <= iCheckInEnd))
            {
                this.timer1.Interval = (1000 * 60 * Convert.ToInt32(txtRecheck.Text));
                this.notifyIcon1.ShowBalloonTip(100, "Nhắc nhở", "Đến giờ check in", ToolTipIcon.Info);
                if (count == 0)
                {
                    RunChromeCmd(timeCheckUrl);
                }
                count++;
            }
            else if((iCheckOutStart <= timeSystem) && (timeSystem <= iCheckOutEnd))
            {
                this.timer1.Interval = (1000 * 60 * Convert.ToInt32(txtRecheck.Text));
                this.notifyIcon1.ShowBalloonTip(100, "Nhắc nhở", "Đến giờ check out", ToolTipIcon.Info);
                if (count == 0)
                {
                    RunChromeCmd(timeCheckUrl);
                }
                count++;
            }
            else
            {
                this.timer1.Interval = 1000;
                count = 0;
            }
            this.lblTime.Text = dt.ToLongTimeString();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            this.timer1.Enabled = true;
            this.btnStop.Visible = true;
            this.btnStart.Visible = false;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            this.timer1.Enabled = false;
            this.btnStop.Visible = false;
            this.btnStart.Visible = true;
        }

        private int TotalMinute(DateTime dt)
        {
            return (dt.Hour * 60) + dt.Minute;
        }

        private void RunChromeCmd(string strUrl)
        {
            Process pro = new Process();
            pro.StartInfo.FileName = "cmd.exe";
            pro.StartInfo.Arguments = string.Format("/k start chrome --new-windown \"{0}\"", strUrl);
            pro.StartInfo.UseShellExecute = false;
            pro.StartInfo.CreateNoWindow = true;
            pro.StartInfo.RedirectStandardOutput = false;
            pro.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            pro.Start();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SQLAppLib.SQLApp.SetIniFile(fileName, strSection, keyCheckInStart, txtCheckInStart.Value.ToString());
            SQLAppLib.SQLApp.SetIniFile(fileName, strSection, keyCheckInEnd, txtCheckInEnd.Value.ToString());
            SQLAppLib.SQLApp.SetIniFile(fileName, strSection, keyCheckOutStart, txtCheckOutStart.Value.ToString());
            SQLAppLib.SQLApp.SetIniFile(fileName, strSection, keyCheckOutEnd, txtCheckOutEnd.Value.ToString());
            SQLAppLib.SQLApp.SetIniFile(fileName, strSection, keyCheckRecheck, txtRecheck.Value.ToString());
            SQLAppLib.SQLApp.SetIniFile(fileName, strSection, keyCheckUrl, txtUrl.Text);
            if(chkAutoSystem.Checked)
            {
                RegistryKey regkey = Registry.CurrentUser.CreateSubKey("Software\\CheckKintal");
                RegistryKey regstart = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run");
                try
                {
                    regkey.SetValue("Index", 1);
                    regstart.SetValue("CheckKintal", Application.StartupPath + "\\CheckKintal.exe");
                }
                catch (Exception ex)
                {

                }

            }
            MessageBox.Show("Lưu thành Công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
