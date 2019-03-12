﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using SQLAppLib;
using System.Runtime.InteropServices;

namespace SQLQQ
{
    public partial class frmMain : CustomFormStyle.CustomForm
    {
        //#region Kết nối SQL

        //public static SqlConnection sqlconn;
        public static String strServer_Public;
        public static String strDatabase_Public;
        public static String strUserName_Public;
        public static String strPassWord_Public;
        //private string section = "SqlServer";
        //private string serverCnt = "ServerCount";
        //private string _serverDesc = "Descreiption";
        //private string _serverName = "Server";
        //private string _serverUID = "UID";
        //private string _serverPWD = "PWD";
        private string strFileName = Application.StartupPath + "\\config.ini";
        public string strPath_DetailCfg_Ini = Application.StartupPath + "\\DetailCfg.ini";  // Cấu hình các thông số
        public String _strQuery_Error;
        frmSearch _frmSearch;
        public static int iCheckShowFormMain = 1;

        public int _iCheckShowFormMain
        {
            get { return iCheckShowFormMain; }
            set { iCheckShowFormMain = value; }
        }

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            //SQLAppWaitingDialog.ShowWaitForm();
            Application.DoEvents();
            _frmSearch = new frmSearch();
            SQLApp.ShowAnimate(this.Handle, Util.FunctionList.GetAnimateWindowTime());
            this.isVisibleOpacity = false;
            //frmMain = new frmMain();
            // Fill out data in Combobox Sever
            Util.FunctionList.LoadConfigIniFromServer(cbbServer);
            LoadListFunction();
            // Login Server and Database from Login Oldest.
            ReadFromFileTextForLogin();
            if (cbbDatabase.Text != "")
            {
                strDatabase_Public = cbbDatabase.Text;
                SQLDBUtil.ChangeDatabase(strDatabase_Public);
            }

            //notifyIcon1.Click += new EventHandler(notifyIcon1_Click);
            //this.Resize += new EventHandler(frmMain_Resize);

            cbbServer.SelectedIndexChanged += new EventHandler(cbbServer_SelectedIndexChanged);
            cbbDatabase.SelectedIndexChanged += new EventHandler(cbbDatabase_SelectedIndexChanged);
            cbbDatabase.DisplayMemberChanged += new EventHandler(cbbDatabase_SelectedIndexChanged);
            SQLApp.SetFormTitle(this);
            ShowNotifyIconMessage("Hello!", "Welcome my toolSQL");
        }

        void _frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {

            SQLApp.HideAnimate(this.Handle, Util.FunctionList.GetAnimateWindowTime());
            Util.FunctionList.SetDBHistoryConfig(cbbServer.SelectedIndex, Convert.ToString(cbbDatabase.SelectedValue));
            Util.FunctionList.SetServerHistoryConfig(cbbServer.Text);
            Application.Exit();
        }

        #region Event

        //void frmMain_Resize(object sender, EventArgs e)
        //{
        //    // Nếu Form đang Minimize thì ẩn luôn Form
        //    if (FormWindowState.Minimized == WindowState)
        //        Hide();
        //}

        //void notifyIcon1_Click(object sender, EventArgs e)
        //{
        //    Show();
        //    WindowState = FormWindowState.Normal;
        //}

        private void cbbDatabase_SelectedIndexChanged(object sender, EventArgs e)
        {
            strDatabase_Public = cbbDatabase.Text;
            if (strDatabase_Public != "")
                SQLDBUtil.ChangeDatabase(strDatabase_Public);
        }

        private void cbbServer_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbbDatabase.Text = string.Empty;
            cbbDatabase.Refresh();
            ReadFromFileTextForLogin(cbbServer.SelectedIndex);
        }

        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (cbbDatabase.Text != "" && cbbServer.Text != "")
            {
                #region Key Ctr 1 - Show all module

                if (e.Control && e.KeyCode == Keys.D1)
                {
                    Util.FunctionList.FindModule(this);
                }

                #endregion

                #region Key Alt + 1 - Search data By Table

                if (e.Alt && e.KeyCode == Keys.D1)
                {
                    Util.FunctionList.GetViewDataByNo(this);
                }

                #endregion

                #region Ctr + Q SQL Execute

                frmQuery_SQL _frmQuery_SQL = new frmQuery_SQL();

                if (e.Control && e.KeyCode == Keys.Q)
                {
                    _frmQuery_SQL.Show();
                }

                #endregion

                #region Ctrl + F
                if (e.Control && e.KeyCode == Keys.F)
                {
                    Util.FunctionList.FindColumn(this);
                }
                #endregion

                #region Funtion List Extra F10
                if (e.KeyCode == Keys.F10)
                {
                    Util.FunctionList.ShowFunctionList();
                }
                #endregion

                #region Function list view connect sql Ctrl + 0
                if (e.Control && e.KeyCode == Keys.D0)
                {
                    Util.FunctionList.GetViewConnectToSQL(this);
                }
                #endregion

                #region Function List Ctrl + Shift + A Create Module
                if (e.Control)
                {
                    if (e.Shift && e.KeyCode == Keys.A)
                        Util.FunctionList.CreateMoudle();
                }
                #endregion

                #region Function List Ctrl + Shift + T Gen Create Table
                if (e.Control && e.Shift && e.KeyCode == Keys.T)
                {
                    //Util.FunctionList.GenScriptCreateTable();
                }
                #endregion

                #region Alt + Shift + F6
                if (e.Alt && e.Shift && e.KeyCode == Keys.F6)
                {
                    frmUpdateVersion frmUpdate = new frmUpdateVersion();
                    frmUpdate.ShowDialog();
                }
                #endregion

                #region Refresh data F5
                if (e.KeyCode == Keys.F5)
                {
                    Util.FunctionList.LoadConfigIniFromServer(cbbServer);
                    ReadFromFileTextForLogin();
                }
                #endregion

                #region Notifycation

                if (e.KeyCode == Keys.F3)
                {
                    frmNotifycation _frmNotifycation = new frmNotifycation();
                    _frmNotifycation.DesktopLocation = new Point(0, 0);
                    _frmNotifycation.ShowDialog();
                    //NotifycationAler _frmNotifycation = new NotifycationAler();
                    //_frmNotifycation.DesktopLocation = new Point(0, 0);
                    //_frmNotifycation.ShowDialog();
                }

                #endregion

                #region Gen Info / Controller - Ctrl + 6
                if (e.Control && e.KeyCode == Keys.D6)
                {
                    Util.FunctionList.GenInfoController();
                }
                #endregion
            }

            #region Key Esc

            if (e.KeyCode == (Keys.Back | Keys.RButton | Keys.LButton | Keys.ShiftKey))
            {
                ShowNotifyIconMessage("Goodbye!", "See you later...");

                DialogResult dr = MessageBox.Show("Are you quit!", "Warning!", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.OK)
                {
                    SQLApp.SetIniFile(Application.StartupPath + "\\config.ini", "LoginHistory", "DB_Old", cbbDatabase.Text);
                    Application.Exit();
                }
            }

            #endregion

            #region Show Main

            //if (e.Control && e.KeyCode == Keys.M)
            //{
            //    frmMain.Show();
            //}

            #endregion
        }

        #endregion

        #region Functions
        private void ReadFromFileTextForLogin(int idx = 0)
        {
            try
            {
                cbbServer.SelectedIndex = idx;
                if (string.IsNullOrEmpty(Convert.ToString(cbbServer.SelectedItem)))
                    cbbServer.SelectedItem = Util.FunctionList.GetServerHistoryConfig();

                strServer_Public = Util.FunctionList.GetServerConfig(cbbServer.SelectedIndex);
                strUserName_Public = Util.FunctionList.GetUserNameConfig(cbbServer.SelectedIndex);
                strPassWord_Public = Util.FunctionList.GetPassWordConfig(cbbServer.SelectedIndex);
                strDatabase_Public = Util.FunctionList.GetDBHistoryConfig(cbbServer.SelectedIndex);

                SQLDBUtil.ChangeConnection(strServer_Public, strUserName_Public, strPassWord_Public);
                SQLDBUtil.ChangeDatabase(strDatabase_Public);
                GetAllDatabaseByServer();
            }
            catch
            {

            }
        }

        private void GetAllDatabaseByServer()
        {
            //cbbDatabase.DataSource = null;
            cbbDatabase.DisplayMember = "name";
            cbbDatabase.ValueMember = "name";
            DataSet ds = SQLDBUtil.GetAllDatabases();
            string strDBSelected = strDatabase_Public;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                cbbDatabase.DataSource = ds.Tables[0];
                cbbDatabase.SelectedValue = strDBSelected;
            }
        }
        private void LoadListFunction()
        {
            this.FunctionList.Width = lstFunction.Width - 10;
            string[] arrFunctions = Directory.GetFiles(Application.StartupPath + "\\Scripts", "*.sql");
            foreach (string item in arrFunctions)
            {
                string fileName = Path.GetFileNameWithoutExtension(item);
                lstFunction.Items.Add(new ListViewItem(fileName));
            }
        }

        public void UpdateData(DataGridView dgrData)
        {
            for (int i = 0; i < dgrData.Columns.Count - 1; i++)
            {
                String strColumnName = dgrData.Columns[i].Name.ToString();
                if (strColumnName.Contains("ID"))
                {
                    String strPrimaryKey = strColumnName;
                    String strTableName = strPrimaryKey.Replace("ID", "s");
                    String strDataUpdate = dgrData.CurrentCell.Value.ToString();
                    String strColumnUpdate = dgrData.CurrentCell.OwningColumn.Name.ToString();
                    int ID = (int)dgrData.CurrentRow.Cells[strPrimaryKey].Value;

                    String strQueryUpdate = String.Format(@"
                                                            UPDATE {0}
                                                            SET    {1} = '{2}'
                                                            WHERE  {3} =  {4}
                                                          ", strTableName, strColumnUpdate, strDataUpdate, strPrimaryKey, dgrData.CurrentRow.Cells[strPrimaryKey].Value.ToString());


                    try
                    {
                        //SqlCommand cmd = new SqlCommand(strQueryUpdate, sqlconn);
                        //cmd.ExecuteNonQuery();
                        SQLDBUtil.ExecuteNonQuery(strQueryUpdate);
                    }
                    catch
                    { }
                }
            }
        }

        public void ShowNotifyIconMessage(String strTitle, String strMessage)
        {
            notifyIcon1.Icon = new Icon(String.Format(@"{0}\Notify.ico", Application.StartupPath));
            notifyIcon1.Visible = true;
            notifyIcon1.ShowBalloonTip(500, strTitle, strMessage, ToolTipIcon.Info);
        }

        #endregion

        private void sQLToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Util.FunctionList.GetConfigConnectSQL("add");
        }

        private void syncDBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSyncDB frmSync = new frmSyncDB();
            frmSync.Show();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Util.FunctionList.GetConfigConnectSQL("edit", cbbServer.SelectedIndex + 1);
        }

        private void setTabIndexMenuItem_Click(object sender, EventArgs e)
        {
            SetTabIndex frmTab = new SetTabIndex();
            frmTab.Show();
        }

        private void unPostToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUnPost _frmUnPort = new frmUnPost();
            _frmUnPort.ShowDialog();
        }

        private void importConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmImportConfig frmImport = new frmImportConfig();
            frmImport.Show();
        }

        private void lstFunction_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                Util.FunctionList.LoadFunction(this);
            }
        }
    }


}
