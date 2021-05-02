using DevExpress.XtraGrid.Views.Grid;
using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        string strListUser = "ListUser";
        UserAccount currentAccount;
        string strDomain = "";
        Services.RequestApiHelper apiHelper;

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
            apiHelper = new Services.RequestApiHelper(strDomain);
            ConfigGridUser();
            LoadDataUsers();
        }

        private void ConfigGridUser()
        {
            GridView view = (gdcListUser.MainView as GridView);
            view.Columns.ToList().ForEach(x => x.OptionsColumn.AllowEdit = false);
            view.Columns.ColumnByFieldName("chkSelected").OptionsColumn.AllowEdit = true;

            view.FocusedRowChanged += View_FocusedRowChanged;
        }

        private void View_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            GridView view = sender as GridView;
            int iRow = e.FocusedRowHandle;
            if (iRow >= 0)
            {
                currentAccount = view.GetFocusedRow() as UserAccount;
                userAuth1.txtUsername.Text = currentAccount.Username;
                userAuth1.txtPassword.Text = currentAccount.Password;
                userAuth1.chkAuthoCheck.Checked = currentAccount.AutoCheckIn;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            int timeSystem = TotalMinute(dt);
            int iCheckInStart = TotalMinute(Convert.ToDateTime(txtCheckInStart.Value));
            int iCheckInEnd = TotalMinute(Convert.ToDateTime(txtCheckInEnd.Value));
            int iCheckOutStart = TotalMinute(Convert.ToDateTime(txtCheckOutStart.Value));
            int iCheckOutEnd = TotalMinute(Convert.ToDateTime(txtCheckOutEnd.Value));
            if ((iCheckInStart <= timeSystem) && (timeSystem <= iCheckInEnd))
            {
                this.timer1.Interval = (1000 * 60 * Convert.ToInt32(txtRecheck.Text));
                this.notifyIcon1.ShowBalloonTip(100, "Nhắc nhở", "Đến giờ check in", ToolTipIcon.Info);
                if (count == 0)
                {
                    RunChromeCmd(timeCheckUrl);
                    RunAutoCheckIn();
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

        private void mnItemCloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void mnItemShowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            float BatteryLifePercent = SystemInformation.PowerStatus.BatteryLifePercent;
            if (BatteryLifePercent * 100 <= 20)
                this.notifyIcon1.ShowBalloonTip(100, "Thông báo", string.Format("Dung lượng pin còn {0}", BatteryLifePercent.ToString("P0")), ToolTipIcon.Info);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int iCntUser = getCntUser();

            iCntUser++;

            UserAccount item = new UserAccount();
            item.Username = userAuth1.txtUsername.Text;
            item.Password = userAuth1.txtPassword.Text ?? "Hito@123";
            item.AutoCheckIn = userAuth1.chkAuthoCheck.Checked;
            item.UserAgent = userAuth1.txtUserAgent.Text ?? "";

            SQLAppLib.SQLApp.SetIniFile(fileName, strListUser, "CntUser", iCntUser.ToString());

            SaveUserAccountToFile(item);
        }

        private void SaveUserAccountToFile(UserAccount item)
        {
            int iCntUser = item.index;

            SQLAppLib.SQLApp.SetIniFile(fileName, strListUser, "Username" + iCntUser, item.Username);
            SQLAppLib.SQLApp.SetIniFile(fileName, strListUser, "Password" + iCntUser, item.Password);
            SQLAppLib.SQLApp.SetIniFile(fileName, strListUser, "AutoCheckIn" + iCntUser, item.AutoCheckIn ? "1" : "0");

            userAuth1.txtUsername.Text = "";
            userAuth1.txtPassword.Text = "";
            userAuth1.chkAuthoCheck.Checked = false;
        }

        private int getCntUser()
        {
            string strCnt = SQLAppLib.SQLApp.GetIniFile(fileName, strListUser, "CntUser");
            int iCntUser = 0;
            if (!string.IsNullOrEmpty(strCnt))
                iCntUser = Convert.ToInt32(strCnt);

            return iCntUser;
        }

        private void LoadDataUsers()
        {
            int iCntUser = getCntUser();

            List<UserAccount> listUser = new List<UserAccount>();

            for (int i = 1; i <= iCntUser; i++)
            {
                UserAccount item = new UserAccount();
                item.Username = SQLAppLib.SQLApp.GetIniFile(fileName, strListUser, "Username" + i);
                item.Password = SQLAppLib.SQLApp.GetIniFile(fileName, strListUser, "Password" + i);
                item.index = i;
                string strAutoCheck = SQLAppLib.SQLApp.GetIniFile(fileName, strListUser, "AutoCheckIn" + i);
                if (!string.IsNullOrEmpty(strAutoCheck))
                    item.AutoCheckIn = Convert.ToBoolean(Convert.ToInt32(strAutoCheck));

                listUser.Add(item);
            }

            gdcListUser.DataSource = listUser;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            SaveUserAccountToFile(currentAccount);
            LoadDataUsers();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //SQLAppLib.SQLApp.del
        }

        private void RunAutoCheckIn()
        {
            string strUrl = "";
            List<UserAccount> listUser = (gdcListUser.DataSource as List<UserAccount>);
            foreach (UserAccount item in listUser)
            {
                if (!item.AutoCheckIn) continue;
                apiHelper = new Services.RequestApiHelper(strDomain);
                apiHelper.AddDefaultRequestHeader();
                apiHelper.SetUserAgent(item.UserAgent);
                string token = RunLoginUser(item);
                apiHelper.SetApiToken(token);
                apiHelper.AddRequestParam("", "");
                string content = apiHelper.Post(strUrl).ToString();

                item.lblShow = "";
                SaveUserAccountToFile(item);
            }
            LoadDataUsers();
        }

        private string RunLoginUser(UserAccount item)
        {
            string strUrl = "";
            if (apiHelper == null)
            {
                apiHelper = new Services.RequestApiHelper(strDomain);
                apiHelper.AddDefaultRequestHeader();
            }
            apiHelper.AddRequestParam("", "");
            xNet.HttpResponse response = apiHelper.Post(strUrl);
            string content = response.ToString();

            object objContent = JsonConvert.DeserializeObject(content);
            string resultData = ((JObject)objContent).GetValue("user").ToString();
            UserInfo info = JsonConvert.DeserializeObject<UserInfo>(resultData);

            return "";
        }
    }
}
