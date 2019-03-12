﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using SQLAppLib;
using System.Runtime.InteropServices;

namespace SQLQQ
{
    public partial class frmSearch : Form
    {
        public Form _frmParent;
        public bool inputText = true;
        public bool inputCombobox = false;
        public bool multiline = false;
        public bool bShowLstDB = true;
        AutoCompleteStringCollection autoComplete;
        frmSearch _frmSearch;
        private const int EM_SETCUEBANNER = 0x1501;
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)]string lParam);
        public frmSearch()
        {
            InitializeComponent();
            InitDataControl();
        }

        private void InitDataControl()
        {
            txtInput.Visible = inputText;
            cboItems.Visible = inputCombobox;
            txtInput.WordWrap = true;
            txtInput.Multiline = multiline;
            autoComplete = new AutoCompleteStringCollection();

            cboItems.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cboItems.AutoCompleteSource = AutoCompleteSource.CustomSource;
            cboItems.AutoCompleteCustomSource = autoComplete;

            txtInput.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtInput.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtInput.AutoCompleteCustomSource = autoComplete;
        }

        public frmSearch(Form frmParent)
        {
            _frmParent = frmParent;
            InitializeComponent();
            InitDataControl();
        }

        public frmSearch(Form frmParent, bool isText, bool isCombobox, bool isShowLstTbl)
        {
            _frmParent = frmParent;
            inputText = isText;
            inputCombobox = isCombobox;
            bShowLstDB = isShowLstTbl;
            InitializeComponent();
            InitDataControl();
        }

        private void frmSearch_Load(object sender, EventArgs e)
        {
            _frmSearch = new frmSearch();

            if (inputCombobox && bShowLstDB)
                ShowAllTableNameByComboBox();

            GetControlFocus();
        }

        private void frmSearch_KeyDown(object sender, KeyEventArgs e)
        {
            frmMain _frmMain = new frmMain();
            frmData _frmData = new frmData(_frmParent);
            frmModule _frmModule = new frmModule();

            #region Key Enter - View data by condition for search of table name

            //if (cbbSearchData.Text != "")
            //{
            //    if (e.KeyCode == Keys.Enter)
            //    {
            //        if (cbbSearchData.Text == "STGridColumns")
            //        {
            //            String strQuery_AllModule = String.Format(@"  Select STModuleName
            //                                                          From STModules
            //                                                      ");

            //            DataTable dtAllModule = SQLDBUtil.GetDataTable(strQuery_AllModule);
            //            if (dtAllModule != null && dtAllModule.Rows.Count > 0)
            //            {
            //                _frmModule._cbbModule.DisplayMember = "STModuleName";
            //                _frmModule._cbbModule.DataSource = dtAllModule;
            //                _frmModule._cbbModule.Text = "Customer";
            //                _frmModule.ShowDialog();
            //            }
            //        }
            //        else
            //        {
            //            String strQuerySearchDataNotAlive = String.Format(@" SELECT * FROM {0}
            //                                                  ", cbbSearchData.Text);

            //            String strQuerySearchDataByAlive = String.Format(@" SELECT * FROM {0} WHERE AAStatus = 'Alive'
            //                                                  ", cbbSearchData.Text);
            //            DataTable dtData = new DataTable();

            //            dtData = SQLDBUtil.GetDataTable(strQuerySearchDataByAlive);
            //            if (dtData != null && dtData.Rows.Count > 0)
            //            {
            //                _frmData._iCountSearch = dtData.Rows.Count;
            //                _frmData._strQuery_RecentData = strQuerySearchDataByAlive;
            //                _frmData._sttRowCount.Text = "Row : " + dtData.Rows.Count.ToString();
            //                _frmData._dgrSearch.DataSource = dtData;
            //                _frmData._dgvData_Current = _frmData._dgrSearch;
            //                _frmData._strTableName_SearchPublic = cbbSearchData.Text;
            //                _frmData._dgrSearch.Refresh();
            //            }
            //            else
            //            {
            //                dtData = SQLDBUtil.GetDataTable(strQuerySearchDataNotAlive);
            //                if (dtData != null && dtData.Rows.Count > 0)
            //                {
            //                    _frmData._iCountSearch = dtData.Rows.Count;
            //                    _frmData._strQuery_RecentData = strQuerySearchDataNotAlive;
            //                    _frmData._sttRowCount.Text = "Row : " + dtData.Rows.Count.ToString();
            //                    _frmData._dgrSearch.DataSource = dtData;
            //                    _frmData._dgvData_Current = _frmData._dgrSearch;
            //                    _frmData._strTableName_SearchPublic = cbbSearchData.Text;
            //                    _frmData._dgrSearch.Refresh();
            //                }
            //            }

            //            this.Hide();
            //            //this.Size = new System.Drawing.Size(1200, 700);
            //            _frmData.StartPosition = FormStartPosition.CenterScreen;
            //            _frmData.Text = " Information Data : " + cbbSearchData.Text;

            //            _frmData.Show();
            //        }
            //    }
            //}

            #endregion

            #region Key Esc - Close form

            if (e.KeyCode == (Keys.Back | Keys.RButton | Keys.LButton | Keys.ShiftKey))
            {
                this.Hide();
                _frmParent.Show();
                //_frmMain.Show();
                //_frmMain.WindowState = FormWindowState.Normal;
            }

            #endregion
        }
        public void SetDataSourceCombobox(string[] arrSource)
        {
            if (arrSource != null)
            {
                foreach (string item in arrSource)
                {
                    autoComplete.Add(item);
                    cboItems.Items.Add(item);
                }
            }
        }
        public void SetDataSourceCombobox(DataTable dtSource)
        {
            if (dtSource == null || dtSource.Rows.Count == 0) return;
            cboItems.DataSource = dtSource;
            cboItems.DisplayMember = "TableName";
            cboItems.ValueMember = "TableName";
            autoComplete.AddRange(dtSource.Select().Select(x => Convert.ToString(x["TableName"])).ToArray());
        }
        public void SetCaption(string caption)
        {
            lblCaption.Text = caption;
            SendMessage(txtInput.Handle, EM_SETCUEBANNER, 0, caption);
            SendMessage(cboItems.Handle, EM_SETCUEBANNER, 0, caption);
        }

        #region Function

        private void ShowAllTableNameByComboBox()
        {
            DataTable dtSource = SQLDBUtil.GetDataTableByDataSet(SQLDBUtil.GetAllTables());
            SetDataSourceCombobox(dtSource);
        }
        public string GetText()
        {
            return txtInput.Text;
        }
        public string GetSelectedText()
        {
            return Convert.ToString(cboItems.SelectedValue);
        }
        public void GetControlFocus()
        {
            txtInput.Focus();
            cboItems.Focus();
        }
        private void GetData()
        {
            frmData _frmData = new frmData();

        }
        public string _cboItems
        {
            set { _cboItems = value; }
            get { return _cboItems; }
        }
        public void SetText(string value)
        {
            txtInput.Text = value;
        }
        public void SetSelectedText(string value)
        {
            cboItems.SelectedItem = value;
        }
        #endregion

        private void btnOK_Click(object sender, EventArgs e)
        {
            //SQLAppWaitingDialog.ShowWaitForm();
            Close();
        }
    }
}
