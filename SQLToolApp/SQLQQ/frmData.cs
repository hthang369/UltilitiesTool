﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using SQLAppLib;
using System.Threading;

namespace SQLQQ
{
    public partial class frmData : Form
    {
        public frmData()
        {
            InitializeComponent();
        }

        public frmData( Form frmParent )
        {
            _frmParent = frmParent;
            InitializeComponent();
        }

        public bool IsActionUpdate = false;
        public bool IsAction_ColorForeignColumn = false;
        public bool isAction_ToolTipTextForForeignData = false;

        public static int iCountSearch;
        public static String strCurrentCellColumnName;
        public static String strPrimaryKey;
        public static String strTableName;
        public static String strTableName_SearchPublic;
        public static String strTableName_Foreign;
        public static String strCurrentCellData;
        public static String strQuery_RecentData;
        public static DataGridView dgvData_Current;
        public Form _frmParent;
        public static bool isRowCount;
        public static List<String> lstCondition_Query = new List<string>();

        #region Property

        frmMain _frmMain;
        frmConditionForSearch _frmConditionForSearch;

        public  DataGridView _dgrSearch
        {
            get
            {
                return dgrSearch;
            }
            set
            {
                dgrSearch = value;
            }
        }

        public ToolStripStatusLabel _sttRowCount
        {
            get
            {
                return sttRowCount;
            }
            set
            {
                sttRowCount = value;
            }
        }

        public  String _strCurrentCellColumnName
        {
            get
            {
                return strCurrentCellColumnName;
            }
            set
            {
                strCurrentCellColumnName = value;
            }
        }

        public  String _strPrimaryKey
        {
            get
            {
                return strPrimaryKey;
            }
            set
            {
                strPrimaryKey = value;
            }
        }

        public int _iCountSearch
        {
            get
            {
                return iCountSearch;
            }
            set
            {
                iCountSearch = value;
            }
        }

        public  String _strTableName
        {
            get
            {
                return strTableName;
            }
            set
            {
                strTableName = value;
            }
        }

        public String _strTableName_SearchPublic
        {
            get
            {
                return strTableName_SearchPublic;
            }
            set
            {
                strTableName_SearchPublic = value;
            }
        }

        public String _strTableName_Foreign
        {
            get
            {
                return strTableName_Foreign;
            }
            set
            {
                strTableName_Foreign = value;
            }
        }

        public  String _strCurrentCellData
        {
            get
            {
                return strCurrentCellData;
            }
            set
            {
                strCurrentCellData = value;
            }
        }

        public String _strQuery_RecentData
        {
            get
            {
                return strQuery_RecentData;
            }
            set
            {
                strQuery_RecentData = value;
            }
        }

        public DataGridView _dgvData_Current
        {
            get
            {
                return dgvData_Current;
            }
            set
            {
                dgvData_Current = value;
            }
        }

        public ProgressBar _probWaiting
        {
            get
            {
                return probWaiting;
            }
            set
            {
                probWaiting = value;
            }
        }

        #endregion
        private delegate void Thread_LoadData(); 
        private void frmSearch_Load(object sender, EventArgs e)
        {
            
            _frmMain = new frmMain();
            _frmMain._iCheckShowFormMain++;

            if (IsActionUpdate == false)
            {
                dgrSearch.ReadOnly = true;
            }

            dgrSearch.ColumnHeaderMouseClick += new DataGridViewCellMouseEventHandler(dgrSearch_ColumnHeaderMouseClick);
            dgrSearch.CellValueChanged += new DataGridViewCellEventHandler(dgrSearch_CellValueChanged);
            dgrSearch.KeyDown += new KeyEventHandler(dgrSearch_KeyDown);
            dgrSearch.MouseHover += new EventHandler(dgrSearch_MouseHover);
            dgrSearch.Click += new EventHandler(dgrSearch_Click);
            dgrSearch.SelectionChanged += dgrSearch_SelectionChanged;

            dgrSearch.Font = new Font("Segoe UI", 9);

            cbbOperation.Validated += cbbOperation_Validated;

            ReadAllOperation();

            Thread newThread = new Thread(LoadData);
            //
            newThread.Start();
            
        }

        void cbbOperation_Validated(object sender, EventArgs e)
        {
            lblOperation.Text = cbbOperation.Text;
            //lblOperation =lblOperation.Font.Bold;
        }
        private void LoadData()
        {
            SQLAppWaitingDialog.Show();
            if (dgrSearch.InvokeRequired)
            {
                this.Invoke(new Thread_LoadData(LoadData));
            }
            else
            {
                SetColorForDataGridView(dgrSearch);
                SetIDNoNameByCurrentRowFocusOnGrid_Click();
                SetFontStyleForBottomForm();
                #region Events
                dgrSearch.AutoResizeColumns();
                dgrSearch.AutoResizeRows();
                #endregion
            }
            SQLAppWaitingDialog.Close();
        }

        #region Functions

        public void SetColorForDataGridView(DataGridView dgr)
        {
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.ForeColor = Color.Black;
            style.BackColor = Color.AliceBlue;

            DataGridViewCellStyle style_White = new DataGridViewCellStyle();
            style_White.ForeColor = Color.Black;
            style_White.BackColor = Color.White;

            if (dgr.RowCount < 0)
                return;
            else
            {
                for (int i = 0; i < dgr.RowCount - 1; i++)
                {
                    //if (i == 0)
                    //{
                    //    dgr.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
                    //}
                    //else
                    //{
                    if (i % 2 == 0)
                    {
                        dgr.Rows[i].DefaultCellStyle = style;
                    }
                    else
                    {
                        dgr.Rows[i].DefaultCellStyle = style_White;
                    }
                    //}
                }
                //dgr.Refresh();
            }
        }

        public void SetColorForeignColumn(DataGridView dgr)
        {
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.ForeColor = Color.Black;
            style.BackColor = Color.AliceBlue;

            for (int i = 0; i < dgr.RowCount - 1; i++)
            {
                for (int j = 0; j < dgr.Columns.Count - 1; j++)
                {
                    String str = dgr.Columns[j].Name;

                    //if (dgr.Columns[j].Name.Contains("FK_"))
                    //{
                    //    dgr.Rows[
                    //}
                }
            }
        }

        public void SetFontStyleForBottomForm ()
        {
            sttID.Font.Bold.ToString();
            sttNo.Font.Bold.ToString();
            sttName.Font.Bold.ToString();
        }

        public void ShowDataFromSearch(String strCondition, String strColumn)
        {
            String strConditionForSearch = "";

            if (strCondition != "")
            {
                if (strColumn == "STModuleName")
                    strConditionForSearch = String.Format(@"Where STModules.STModuleName LIKE '%{0}%'", strCondition);
                else if (strColumn == "STModuleDescriptionDescription")
                    strConditionForSearch = String.Format(@"Where STModuleDescriptions.STModuleDescriptionDescription LIKE N'%{0}%'", strCondition);
            }

            String strQuerySearch = String.Format(@"    Select *
                                                            From
                                                            (
                                                            SELECT TOP 1 NULL AS STModuleName,NULL AS STModuleDescriptionDescription
                                                            FROM dbo.STModules INNER JOIN dbo.STModuleDescriptions ON STModuleDescriptions.STModuleID = STModules.STModuleID
                                                            Union ALL
                                                            SELECT STModules.STModuleName,STModuleDescriptions.STModuleDescriptionDescription
                                                            FROM dbo.STModules INNER JOIN dbo.STModuleDescriptions ON STModuleDescriptions.STModuleID = STModules.STModuleID
                                                            {0}
                                                            ) AS SearchModule
                                                            
                                                            ORDER BY STModuleDescriptionDescription
                                               ", strConditionForSearch);


            DataTable dtModule = SQLDBUtil.GetDataTable(strQuerySearch);
            if (dtModule != null && dtModule.Rows.Count > 0)
            {
                try
                {
                    dgrSearch.DataSource = dtModule;
                    SetColorForDataGridView(dgrSearch);
                }
                catch { }
            }
        }

        public DataTable GetObjectByMouseHover(String str)
        {
            if (isAction_ToolTipTextForForeignData == true)
            {
                try
                {
                    if (dgrSearch.CurrentCell.OwningColumn.Name.Contains("FK_"))
                    {
                        String[] strTableAndPrimaryKey = dgrSearch.CurrentCell.OwningColumn.Name.Split('_');

                        String strTable = strTableAndPrimaryKey[strTableAndPrimaryKey.Length - 1].ToString().Replace("ID", "s").ToString();
                        String strPrimaryKey = strTableAndPrimaryKey[strTableAndPrimaryKey.Length - 1];

                        String strQuery_ForeignData = String.Format(@" SELECT *
                                                                   FROM {0}
                                                                   WHERE {1} = {2}
                                                               ", strTable, strPrimaryKey, dgrSearch.CurrentCell.Value);

                        DataTable dtForeignData = SQLDBUtil.GetDataTable(strQuery_ForeignData);
                        if (dtForeignData != null && dtForeignData.Rows.Count > 0)
                        {
                            return dtForeignData;
                        }
                        else
                            return null;
                    }
                }
                catch
                { }
            }

            return null;
        }

        public void GetForeignTableName()
        {
            try
            {
                strCurrentCellColumnName = dgrSearch.CurrentCell.OwningColumn.Name.ToString();
                if (strCurrentCellColumnName.Contains("FK_") )
                {
                    String[] strForeignColumn = strCurrentCellColumnName.Split('_');
                    if (strForeignColumn.Length > 0)
                    {
                        strTableName_Foreign = (strForeignColumn[strForeignColumn.Length - 1].ToString()).Replace("ID", "s");
                    }
                }
            }
            catch { }
        }

        public void GetCurrentTableName(String strCurrentColumnName)
        {
            try
            {
                if (!strCurrentColumnName.Contains("AA"))
                {
                    for (int i = 0; i < dgrSearch.Columns.Count - 1; i++)
                    {
                        String strColumnName = dgrSearch.Columns[i].Name.ToString();
                        if (strColumnName.Contains("ID"))
                        {
                            strPrimaryKey = strColumnName;
                            //int ID = (int)dgrSearch.CurrentRow.Cells[strPrimaryKey].Value;
                        }
                    }

                    strTableName = strPrimaryKey.Replace("ID", "s");
                    strCurrentCellData = dgrSearch.CurrentCell.Value.ToString();
                }
            }
            catch { }
        }

        private void ResizeWidthFocusColum(int iWidth)
        {
            DataGridViewColumn column = dgrSearch.CurrentCell.OwningColumn;
            if (column != null)
                column.Width = column.Width + iWidth;
        }

        private void ResizeAllWidthAllFK_Column(String strColumn,int iWidth_Min,int iWidth_Max)
        {
            foreach ( DataGridViewColumn column in  dgrSearch.Columns)
            {
                if (column.Name.Contains(strColumn) || strColumn == "All")
                {
                    if (column.Width <= iWidth_Min)
                        column.Width = iWidth_Max;
                    else
                        column.Width = iWidth_Min;
                }
            }
        }

        #region Set ID,No,Name by FocusDataRow on Grid

        // Set ID,No,Name by FocusDataRow on Grid
        public void SetInformationByRowCurrentByKeyUpDown(int iKeyUpDown)
        {
            String strTableFix = _strTableName_SearchPublic.Substring(0, _strTableName_SearchPublic.Length - 1);
            sttRowCount.Text = "Count : " + Convert.ToInt32(_dgrSearch.CurrentRow.Index + 1 + iKeyUpDown) + "/" + iCountSearch.ToString();

            try
            {
                sttID.Text = "ID : " + _dgrSearch.Rows[Convert.ToInt32(_dgrSearch.CurrentRow.Index) + iKeyUpDown].Cells[strTableFix + "ID"].Value;
                sttNo.Text = "No : " + _dgrSearch.Rows[Convert.ToInt32(_dgrSearch.CurrentRow.Index) + iKeyUpDown].Cells[strTableFix + "No"].Value;
                sttDate.Text = "Date : " + Convert.ToDateTime( _dgrSearch.Rows[Convert.ToInt32(_dgrSearch.CurrentRow.Index) + iKeyUpDown].Cells[strTableFix + "Date"].Value).ToShortDateString();
                //sttName.Text = "Name : " + _dgrSearch.Rows[Convert.ToInt32(_dgrSearch.CurrentRow.Index) + iKeyUpDown].Cells[strTableFix + "Name"].Value;
            }
            catch { }
        }

        private void SetIDNoNameByCurrentRowFocusOnGrid_Click()
        {
            try
            {
                String strTableFix = _strTableName_SearchPublic.Substring(0, _strTableName_SearchPublic.Length - 1);
                sttRowCount.Text = "Count : " + Convert.ToInt32(_dgrSearch.CurrentRow.Index + 1) + "/" + iCountSearch.ToString();

                sttID.Text = "ID : " + _dgrSearch.CurrentRow.Cells[strTableFix + "ID"].Value;
                sttNo.Text = "No : " + _dgrSearch.CurrentRow.Cells[strTableFix + "No"].Value;
                sttDate.Text = "Date : " + Convert.ToDateTime(_dgrSearch.CurrentRow.Cells[strTableFix + "Date"].Value).ToShortDateString();
                //sttName.Text = "Name : " +  _dgrSearch.CurrentRow.Cells[strTableFix + "Name"].Value;
            }
            catch
            {

            }
        }

        // Set ForeignData : ID,No,Name by FK_ FocusDataRow on Grid
        private void SetIDNoNameByForeignDataCurrentRowFocusOnGrid_Click()
        {
            try
            {
                if (dgrSearch.CurrentCell.OwningColumn.Name.Contains("FK_"))
                {

                    DataInfo objDataInfo = SQLDBUtil.GetObjectByIDAndTableName(Convert.ToInt32(dgrSearch.CurrentCell.Value.ToString()), strTableName_Foreign);
                    if (objDataInfo != null)
                    {
                        sttRowCount.Text = "Count : " + Convert.ToInt32(_dgrSearch.CurrentRow.Index + 1) + "/" + iCountSearch.ToString();


                        sttFrNo.Text = "No : " + objDataInfo.strNo;
                        sttFrName.Text = "Name : " + objDataInfo.strName;
                        sttFrDate.Text = "Date : " + objDataInfo.dtDate;

                    }
                }
            }
            catch { }
        }

        public void SetInformationByForeignDataCurrentByKeyUpDown(int iKeyUpDown)
        {
            String strTableFix = _strTableName_SearchPublic.Substring(0, _strTableName_SearchPublic.Length - 1);
            
            try
            {
                DataInfo objDataInfo = SQLDBUtil.GetObjectByIDAndTableName(Convert.ToInt32(dgrSearch.CurrentCell.Value.ToString()), strTableName_Foreign);
                if (objDataInfo != null)
                {
                    sttRowCount.Text = "Count : " + Convert.ToInt32(_dgrSearch.CurrentRow.Index + 1) + "/" + iCountSearch.ToString();

                    try
                    {
                        sttFrNo.Text = "No : " + objDataInfo.strNo;
                        sttFrName.Text = "Name : " + objDataInfo.strName;
                        sttFrDate.Text = "Date : " + objDataInfo.dtDate;
                    }
                    catch { }
                }
            }
            catch { }
        }

        #endregion

        private void ReadAllOperation ()
        {
            System.IO.StreamReader docFile = new System.IO.StreamReader(Application.StartupPath + "\\Operation.txt");
            string  strLine = docFile.ReadLine();
            while (strLine != null)
            {
                cbbOperation.Items.Add(strLine);
                strLine = docFile.ReadLine();
            }
            docFile.Close();
            docFile.Dispose();
        }

        public void ShowViewColumnOnGridByText(String StrViewFromfrmSeachViewColum, int iNotView_Min)
        {
            String[] strViewColumn = StrViewFromfrmSeachViewColum.Split(',');

            if (strViewColumn.Length > 0)
            {
                bool isFocus = false;

                foreach (DataGridViewColumn column in dgrSearch.Columns)
                {
                    if (!CheckResizeForView(strViewColumn, column))
                    {
                        column.Width = iNotView_Min;
                    }
                    else
                    {
                        if ( isFocus == false )
                        {
                            dgrSearch.CurrentCell = dgrSearch[column.Index, 0];
                            isFocus = true;
                        }
                    }
                }
            }
        }

        public void SetColorColumnByCondition(String StrViewFromfrmSeachViewColum, String strTypeColor)
        {
            DataGridViewCellStyle cellStyle_Default = SetColorGridControl_Default();

            DataGridViewCellStyle cellStyle = new DataGridViewCellStyle();
            if (strTypeColor == "ForeColor")
            {
                dgrSearch.EnableHeadersVisualStyles = false;
                cellStyle.Font = new Font("Segoe UI", 8, FontStyle.Underline);
                cellStyle.BackColor = Color.Yellow;
            }

            else if (strTypeColor == "BackColor")
                cellStyle.BackColor = Color.Yellow;


            String[] strViewColumn = StrViewFromfrmSeachViewColum.Split(',');

            if (strViewColumn.Length > 0)
            {
                bool isFocus = false;

                foreach (DataGridViewColumn column in dgrSearch.Columns)
                {
                    foreach (String strView in strViewColumn)
                    {
                        if (column.Name.Contains(strView))
                        {
                            //dgrSearch.Columns[column.Name].DefaultCellStyle = cellStyle;
                            column.HeaderCell.Style = cellStyle;

                            if (isFocus == false)
                            {
                                dgrSearch.CurrentCell = dgrSearch[column.Index, 0];
                                isFocus = true;
                            }
                        }
                        else
                            column.HeaderCell.Style = cellStyle_Default;
                    }
                }
            }
        }

        private static DataGridViewCellStyle SetColorGridControl_Default()
        {
            DataGridViewCellStyle cellStyle_Default = new DataGridViewCellStyle();
            cellStyle_Default.BackColor = SystemColors.Control;
            cellStyle_Default.ForeColor = SystemColors.ControlText;
            return cellStyle_Default;
        }

        private bool CheckResizeForView(String[] strViewColumn, DataGridViewColumn column)
        {
            foreach (String strView in strViewColumn)
            {
                if (column.Name.Contains(strView))
                    return true;
            }

            return false;
        }

        private void SetTextShowOperation(String strStatusOperation, String strTextOperation)
        {
            lblOperation.Text = strStatusOperation;
            txtOperation.Text = strTextOperation;
        }

        #endregion

        #region Events

        void dgrSearch_Click(object sender, EventArgs e)
        {
            if (dgrSearch.Rows.Count > 0)
            {
                try
                {
                    strCurrentCellColumnName = dgrSearch.CurrentCell.OwningColumn.Name.ToString();

                    // Get ID,No,Name from focus Row
                    SetIDNoNameByCurrentRowFocusOnGrid_Click();

                    if (strCurrentCellColumnName.Contains("FK"))
                    {
                        GetForeignTableName();

                        // Get ID, No, Name From Foreign Key
                         SetIDNoNameByForeignDataCurrentRowFocusOnGrid_Click();
                    }
                    else
                    {
                        GetCurrentTableName(strCurrentCellColumnName);
                    }


                    DataTable dtForeignData = GetObjectByMouseHover(e.ToString());

                    if (dtForeignData.Rows.Count > 0)
                    {
                        String strToolTipText = String.Empty;
                        String strForeignTable_Re = strTableName_Foreign.Substring(0, strTableName_Foreign.Length - 1);
                        if (strForeignTable_Re != "")
                        {
                            for (int i = 0; i <= dtForeignData.Columns.Count - 1; i++)
                            {
                                if (dtForeignData.Rows[dgrSearch.CurrentRow.Index][i].ToString() == dtForeignData.Rows[0][strForeignTable_Re + "ID"].ToString())
                                {
                                    strToolTipText += "ID" + dtForeignData.Rows[0][i].ToString() + " |";
                                }


                                if (dtForeignData.Rows[dgrSearch.CurrentRow.Index][i].ToString() == dtForeignData.Rows[0][strForeignTable_Re + "No"].ToString())
                                {
                                    strToolTipText += "No" + dtForeignData.Rows[0][i].ToString() + " |";
                                }

                                if (dtForeignData.Rows[dgrSearch.CurrentRow.Index][i].ToString() == dtForeignData.Rows[0][strForeignTable_Re + "Name"].ToString())
                                {
                                    strToolTipText += "Name" + dtForeignData.Rows[0][i].ToString() + " |";
                                }


                                if (dtForeignData.Rows[dgrSearch.CurrentRow.Index][i].ToString() == dtForeignData.Rows[0][strForeignTable_Re + "Desc"].ToString())
                                    strToolTipText += "Desc" + dtForeignData.Rows[0][i].ToString() + " ";
                            }
                        }

                        dgrSearch.CurrentCell.ToolTipText = strToolTipText;
                    }
                }
              catch { }
            }
        }

        void dgrSearch_MouseHover(object sender, EventArgs e)
        {

        }

        void dgrSearch_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            SetColorForDataGridView(dgrSearch);
        }

        void dgrSearch_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (IsActionUpdate == true)
            {
                _frmMain.UpdateData(dgrSearch);
            }
        }

        void dgrSearch_KeyDown(object sender, KeyEventArgs e)
        {
            frmSearch _frmSearch = new frmSearch();
            frmColumn _frmColumn = new frmColumn();

            #region Key Alt + M/N - Size from - Min/Max

            if (e.Alt && (e.KeyCode == Keys.M))
            {
                this.Size = new System.Drawing.Size(1000, 500);
            }

            if (e.Alt && (e.KeyCode == Keys.N))
            {
                this.Size = new System.Drawing.Size(700, 500);
            }

            #endregion

            #region Key Alt + F - Focus to cell want to find in DataGridView

            if (e.Alt && e.KeyCode == Keys.F)
            {

                for (int i = 0; i < dgrSearch.Columns.Count - 1; i++)
                {
                    String strColumnName = dgrSearch.Columns[i].Name.ToString();
                    _frmColumn._cbbColumn.Items.Add(strColumnName);
                }

                _dgvData_Current = dgrSearch;
                _frmColumn._cbbColumn.DisplayMember = strPrimaryKey;
                _frmColumn.ShowDialog();


                if (_frmColumn._iIndexColumn > 0)
                {
                    dgrSearch.CurrentCell = dgrSearch[_frmColumn._iIndexColumn, dgrSearch.CurrentRow.Index];  // Đưa Control về vị trí của nó
                    //dgrSearch.Rows[0].Cells[_frmColumn._iIndexColumn].Selected = true;                      // Set trạng thái Selected
                }

                return;
            }

            #endregion

            #region Key F - View data by condition for search of focus column data on DataGridView

            if (e.KeyCode == Keys.F)
            {
                frmConditionForSearch _frmConditionForSearch = new frmConditionForSearch();

                strCurrentCellData = dgrSearch.CurrentCell.Value.ToString();
                strTableName = "";

                //if (strCurrentCellData != "")
                //{
                strCurrentCellColumnName = dgrSearch.CurrentCell.OwningColumn.Name.ToString();

                for (int i = 0; i < dgrSearch.Columns.Count - 1; i++)
                {
                    String strColumnName = dgrSearch.Columns[i].Name.ToString();
                    if (strColumnName.Contains("ID"))
                    {
                        strPrimaryKey = strColumnName;
                        strTableName = strPrimaryKey.Replace("ID", "s");

                        break;
                    }

                    if (strColumnName.Contains("STModule"))
                    {
                        strTableName = "STModules";
                    }
                }

                try
                {
                    String strQueryByFocusColumnData = String.Empty;

                    if (strTableName == "STModules")
                    {
                        strQueryByFocusColumnData = strQuery_RecentData.Replace("*", strCurrentCellColumnName);
                    }
                    else
                    {
                        // Kiểm tra Tên bảng có tồn tại trong hệ thống
                        if (SQLDBUtil.CheckTableNameIsExixst(strTableName) == false)
                            return;

                        strQueryByFocusColumnData = String.Format(@" Select {0}
                                                                            From {1}
                                                                            Group By {0}
                                                                      ", strCurrentCellColumnName, strTableName);
                    }

                    DataTable dtQueryByFocusColumnData = SQLDBUtil.GetDataTable(strQueryByFocusColumnData);
                    if (dtQueryByFocusColumnData != null && dtQueryByFocusColumnData.Rows.Count > 0)
                    {
                        _frmConditionForSearch.Text = strCurrentCellColumnName;
                        _frmConditionForSearch._cbbConditionForSearchData.DisplayMember = strCurrentCellColumnName;
                        _frmConditionForSearch._cbbConditionForSearchData.DataSource = dtQueryByFocusColumnData;
                        _frmConditionForSearch._cbbConditionForSearchData.Text = strCurrentCellData;
                        _frmConditionForSearch.ShowDialog();

                    }
                }
                catch
                {
                    MessageBox.Show("Can not Excute sql!");
                }
                //}
            }

            #endregion

            #region Key Alt + T _ Show tool tip text for Foreign data

            if (e.Alt && e.KeyCode == Keys.T)
            {
                // Step 1
                if (isAction_ToolTipTextForForeignData == false)
                {
                    GetForeignTableName();
                    isAction_ToolTipTextForForeignData = true;
                    sttToolTipText.Text = "ToolTip : Foreign Data";
                }
                else
                {
                    isAction_ToolTipTextForForeignData = false;
                    sttToolTipText.Text = "ToolTip : Not Action";
                }

                // Step 2 : Mouse Hover on Grid Foreign Data
            }

            #endregion 

            #region Key Up/Down/Left/Right

            if (e.KeyCode == Keys.Up)
            {
                //if (dgrSearch.CurrentRow.Index != 0)
                //    SetInformationByRowCurrentByKeyUpDown(-1);
            }

            if ( e.KeyCode == Keys.Down)
            {
                //SetInformationByRowCurrentByKeyUpDown(1);
            }

            if (e.KeyCode == Keys.Left)
            {
              
            }

            if (e.KeyCode == Keys.Right)
            {

            }

            #endregion

            #region Esc - Thoát

            if (e.KeyCode == (Keys.Back | Keys.RButton | Keys.LButton | Keys.ShiftKey))
            {
                _frmMain._iCheckShowFormMain--;

                this.Close();
                if (_frmParent != null)
                    _frmParent.Show();

            }

            #endregion

            #region F2 - Update

            if (e.KeyCode == Keys.F1)
            {
                dgrSearch.ReadOnly = true;
                IsActionUpdate = false;
                sttAction.Text = "Not Action";
            }

            if (e.KeyCode == Keys.F2)
            {
                dgrSearch.ReadOnly = false;
                IsActionUpdate = true;
                sttAction.Text = "Action : Update";
            }

            #endregion

            #region Key Ctrl + C - Set Color for foreign column

            if (e.Alt && e.KeyCode == Keys.C)
            {
                if (IsAction_ColorForeignColumn == false)
                    IsAction_ColorForeignColumn = true;
                else
                    IsAction_ColorForeignColumn = false;

                if (IsAction_ColorForeignColumn == true)
                {
                    sttColorForeignColumn.Text = "Color : Foreign Column";
                    SetColorForeignColumn(dgrSearch);
                }
                else
                {
                    sttColorForeignColumn.Text = "Color : Not Action";
                    SetColorForDataGridView(dgrSearch);
                }
            }

            #endregion

            #region Key F5 - Refresh data in DataGridView

            if (e.KeyCode == Keys.F5)
            {
                if (strQuery_RecentData != null && strQuery_RecentData != "")
                {
                    DataTable dtRecentData = SQLDBUtil.GetDataTable(strQuery_RecentData);
                    if (dtRecentData != null && dtRecentData.Rows.Count > 0)
                    {
                        try
                        {
                            int iCurentIndex_Row = dgrSearch.CurrentRow.Index;
                            int iCurentIndex_Column = dgrSearch.CurrentCell.ColumnIndex;

                            sttRowCount.Text = "Row : " + dtRecentData.Rows.Count.ToString();

                            dgrSearch.DataSource = null;
                            dgrSearch.DataSource = dtRecentData;
                            dgrSearch.CurrentCell = dgrSearch[iCurentIndex_Column, iCurentIndex_Row];
                            
                            SetColorForDataGridView(dgrSearch);
                            ResizeAllWidthAllFK_Column("All", 200, 200);
                            dgrSearch.Refresh();
                        }
                        catch
                        { }
                    }
                }
            }

            #endregion

            #region Key Ctr + 1 - Show all module

            if (e.Control && e.KeyCode == Keys.D1)
            {
                frmMain _frmMain = new frmMain();

                String strQuery_AllModule = String.Format(@"  SELECT *
                                                              FROM
                                                              (
                                                                 SELECT STModules.STModuleName,STModuleDescriptions.STModuleDescriptionDescription
                                                                 FROM dbo.STModules INNER JOIN dbo.STModuleDescriptions ON STModuleDescriptions.STModuleID = STModules.STModuleID
                                                              )  AS SearchModule
                                               ");

                DataTable dtModule = SQLDBUtil.GetDataTable(strQuery_AllModule);
                if (dtModule != null && dtModule.Rows.Count > 0)
                {
                    strQuery_RecentData = strQuery_AllModule;
                    dgrSearch.DataSource = dtModule;
                    dgrSearch.Refresh();
                }

                _frmSearch.ShowDialog();
                _frmSearch.StartPosition = FormStartPosition.CenterParent;

            }

            #endregion

            #region Key Alt + 1 - Search data By Table

            if (e.Alt && e.KeyCode == Keys.D1)
            {
                Util.FunctionList.GetViewDataByNo(this);
            }

            #endregion

            #region Key Ctr S - Search data By Table

            if (e.Control && e.KeyCode == Keys.S)
            {
                //ShowAllTableNameByComboBox();
                _frmSearch.ShowDialog();
            }

            #endregion

            #region Ctr + Q SQL Execute

            frmQuery_SQL _frmQuery_SQL = new frmQuery_SQL();

            if (e.Control && e.KeyCode == Keys.Q)
            {
                _frmQuery_SQL.Show();
            }

            #endregion

            #region Ctr + M Show Form Main Connect Sever

            //if (e.Control && e.KeyCode == Keys.M)
            //{
            //    _frmMain.Show();
            //}

            #endregion

            #region Ctrl + I - Into Row Status is Dummy

            // Use From Column is insert type function
            //_frmMain.ReadFromFileTextForServer(_frmColumn._cbbColumn);


            #endregion

            #region Key M - Resize width size Column Focus current ++

            if (e.KeyCode == Keys.M)
            {
                ResizeWidthFocusColum(Convert.ToInt32(SQLApp.GetIniFile(_frmMain.strPath_DetailCfg_Ini, "resize_widthcolumn", "max_width")));
            }

            #endregion

            #region Key N - Resize width size Column Focus current --

            if (e.KeyCode == Keys.N)
            {
                ResizeWidthFocusColum(Convert.ToInt32(SQLApp.GetIniFile(_frmMain.strPath_DetailCfg_Ini, "resize_widthcolumn", "min_width")));
            }

            #endregion

            #region Key Alt + B - Resize "FK_" width size FK_Column on Grid

            if (e.Alt && e.KeyCode == Keys.B)
            {
                ResizeAllWidthAllFK_Column(SQLApp.GetIniFile(_frmMain.strPath_DetailCfg_Ini, "resize_widthcolumn", "Column_width_all"), Convert.ToInt32(SQLApp.GetIniFile(_frmMain.strPath_DetailCfg_Ini, "resize_widthcolumn", "min_width_all")), Convert.ToInt32(SQLApp.GetIniFile(_frmMain.strPath_DetailCfg_Ini, "resize_widthcolumn", "max_width_all")));
                return;
            }

            #endregion

            #region Key Shift + B - Resize All width size FK_Column on Grid

            if (e.Shift && e.KeyCode == Keys.B)
            {
                ResizeAllWidthAllFK_Column("All", Convert.ToInt32(SQLApp.GetIniFile(_frmMain.strPath_DetailCfg_Ini, "resize_widthcolumn", "default")), Convert.ToInt32(SQLApp.GetIniFile(_frmMain.strPath_DetailCfg_Ini, "resize_widthcolumn", "default")));
            }

            #endregion

            #region Key H - Copy Header to ClipBoard (Column Name)

            if (e.KeyCode == Keys.H)
            {
                IDataObject iData = Clipboard.GetDataObject();
                if (strCurrentCellColumnName != null && strCurrentCellColumnName != "")
                {
                    Clipboard.SetText(strCurrentCellColumnName);

                    String strMessage = String.Format(@"'{0}' is copy", String.Format(@"{0}", strCurrentCellColumnName));
                    _frmMain.ShowNotifyIconMessage("Notification!", strMessage);
                }
            }

            #endregion

            #region Key Alt + V - Show data column by condition search column

            if (e.Alt && e.KeyCode == Keys.V)
            {
                frmSearchColumByTest _frmSearchColumByTest = new frmSearchColumByTest();
                _frmSearchColumByTest.ShowDialog();

                if (_frmSearchColumByTest._cbbViewColumnByText.Text != "")
                    ShowViewColumnOnGridByText(_frmSearchColumByTest._cbbViewColumnByText.Text,0);
            }

            #endregion

            #region Key Copy (Ctrl + C) - Paste (Ctrl + V)

            if (e.Control && e.KeyCode == Keys.C)
            {

            }

            if (e.Control && e.KeyCode == Keys.V)
            {
               
            }

            #endregion

            #region Key Alt + C - Set color by condition search column

            if (e.Alt && e.KeyCode == Keys.C)
            {
                frmSearchColumByTest _frmSearchColumByTest = new frmSearchColumByTest();
                _frmSearchColumByTest.ShowDialog();

                if (_frmSearchColumByTest._cbbViewColumnByText.Text != "")
                    SetColorColumnByCondition(_frmSearchColumByTest._cbbViewColumnByText.Text, "ForeColor");
                return;
            }

            #endregion
        }

        void dgrSearch_SelectionChanged(object sender, EventArgs e)
        {
            GetForeignTableName();
            SetIDNoNameByCurrentRowFocusOnGrid_Click();
            SetIDNoNameByForeignDataCurrentRowFocusOnGrid_Click();
        }

        #endregion

        private void sttAction_Click(object sender, EventArgs e)
        {

        }

        public void ProcessBar ()
        {
            //bắt buộc nấc trong progress bar xuất phát từ số 0 sử dụng thuộc tính Minimum
            probWaiting.Minimum = 0;
            //và chỉ cho phép nấc này chạy đến giá trị tối đa là 2000 sử dụng thuộc tính Maximum
            probWaiting.Maximum = 2000;
            //khởi tạo giá trị ban đầu cho progress bar sử thuộc tính Value
            probWaiting.Value = 1;
            //khoảng tăng giữa các nấc trong ProgressBar
            probWaiting.Step = 3;
            //bắt đầu chạy
            for (int i = 0; i <= 2000; i++)
            {
                // do something
                //.....
                //thi hành tăng ProgressBar
                probWaiting.PerformStep();
            }
        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void dgrSearch_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void frmData_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_frmParent != null) _frmParent.Show();
            if (_frmParent == null && _frmMain != null && _frmMain._iCheckShowFormMain == 0) _frmMain.Show();
        }
    }
}
