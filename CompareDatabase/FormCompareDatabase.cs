using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using ExpertLib;
using ConnectDatabase;

namespace CompareDatabase
{
    public enum FunctionType
    {
        Table = 0,
        StoreProcedure = 1
    }

    public partial class FormCompareDatabase : Form
    {
        #region List Data

        List<AllDataSources> lstAllDataSources = new List<AllDataSources>();
        List<Tables> lstTables = new List<Tables>();
        List<Columns> lstColumns = new List<Columns>();
        List<References> lstReferences = new List<References>();
        Dictionary<String, String> lstStoreProcedure = new Dictionary<String, String>();

        #endregion

        #region Control

        CompareController compareCtrl = new CompareController();
        CompareDatabasesController objCompareDatabaseCtrl = new CompareDatabasesController();
        Tables Selected_Table = null;
        Columns Selected_Column = null;
        References Selected_Reference = null;
        ConfigConnectSQL SQLConnectCfg = null;

        #endregion

        public FormCompareDatabase()
        {
            InitializeComponent();
        }

        private void FormCompareDatabase_Load(object sender, EventArgs e)
        {
            InitLoad();
        }

        #region Event

        private void btnProcess_Click(object sender, EventArgs e)
        {
            ActionProcess();
        }

        private void btnCompare_Click(object sender, EventArgs e)
        {
            ActionCompare();
        }

        private void cbxDatabaseFrom_QueryPopUp(object sender, CancelEventArgs e)
        {
            LoadDatabase(ref cbxDatabaseFrom, txtServerFrom.Text, txtUserFrom.Text, txtPasswordFrom.Text);
        }

        private void cbxDatabaseTo_QueryPopUp(object sender, CancelEventArgs e)
        {
            LoadDatabase(ref cbxDatabaseTo, txtServerTo.Text, txtUserTo.Text, txtPasswordTo.Text);
        }

        private void gridViewTables_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            Tables tb = (Tables)gridViewTables.GetRow(e.RowHandle);
            if (tb != null)
            {
                foreach (Tables table in lstTables)
                {
                    if (tb.Table_Name == table.Table_Name)
                    {
                        table.Selected = (Boolean)e.Value;
                    }
                }
            }
            RefreshTable();
        }

        void gridViewReferences_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            GridView gv = (GridView)sender;
            Selected_Reference = (References)gv.GetFocusedRow();
        }

        void gridViewColumns_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            GridView gv = (GridView)sender;
            Selected_Column = (Columns)gv.GetFocusedRow();
        }

        void gridViewTables_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            GridView gv = (GridView)sender;
            Selected_Table = (Tables)gv.GetFocusedRow();
        }

        private void gridControlColumns_ProcessGridKey(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (Selected_Column == null) return;

            switch (e.KeyCode)
            {
                case Keys.Space:
                    Selected_Column.Selected = !Selected_Column.Selected;
                    break;
                default:
                    if (e.Control)
                    {
                        if (e.KeyCode == Keys.A || e.KeyCode == Keys.U || e.KeyCode == Keys.R)
                        {
                            foreach (Columns column in lstColumns)
                            {
                                if (!e.Shift || (e.Shift && Selected_Column.Table_Name == column.Table_Name))
                                {
                                    if (e.KeyCode == Keys.A)
                                        column.Selected = true;
                                    else if (e.KeyCode == Keys.U)
                                        column.Selected = false;
                                    else if (e.KeyCode == Keys.R)
                                        column.Selected = !column.Selected;
                                }
                            }
                        }
                        break;
                    }
                    else
                        return;

            }
            gridControlColumns.RefreshDataSource();
        }

        private void gridControlTables_ProcessGridKey(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (Selected_Table == null) return;
            switch (e.KeyCode)
            {
                case Keys.Space:
                    Selected_Table.Selected = !Selected_Table.Selected;
                    break;
                default:
                    if (e.Control)
                    {
                        if (e.KeyCode == Keys.A || e.KeyCode == Keys.U || e.KeyCode == Keys.R)
                            foreach (Tables table in lstTables)
                            {
                                if (e.KeyCode == Keys.A)
                                    table.Selected = true;
                                else if (e.KeyCode == Keys.U)
                                    table.Selected = false;
                                else if (e.KeyCode == Keys.R)
                                    table.Selected = !table.Selected;
                            }
                        break;
                    }
                    else
                        return;

            }

            gridControlTables.RefreshDataSource();

            RefreshTable();
        }

        private void gridControlReferences_ProcessGridKey(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (Selected_Reference == null) return;
            switch (e.KeyCode)
            {
                case Keys.Space:
                    Selected_Reference.Selected = !Selected_Reference.Selected;
                    break;
                default:
                    if (e.Control)
                    {
                        if (e.KeyCode == Keys.A || e.KeyCode == Keys.U || e.KeyCode == Keys.R)
                            foreach (References reference in lstReferences)
                            {
                                if (!e.Shift || (e.Shift && Selected_Reference.TableNameForeignKey == reference.TableNameForeignKey))
                                {

                                    if (e.KeyCode == Keys.A)
                                        reference.Selected = true;
                                    else if (e.KeyCode == Keys.U)
                                        reference.Selected = false;
                                    else if (e.KeyCode == Keys.R)
                                        reference.Selected = !reference.Selected;
                                }
                            }
                        break;
                    }
                    else
                        return;

            }
            gridControlReferences.RefreshDataSource();
        }

        #endregion

        #region Function

        #region Init Load

        public void InitLoad()
        {
            SQLConnectCfg = new ConfigConnectSQL();
            LoadComboboxFunction();
            LoadInitGridControl();
            LoadConfigConnect();
        }

        public void LoadComboboxFunction()
        {
            cbxFunction.Properties.Items.Add(FunctionType.Table);
            cbxFunction.Properties.Items.Add(FunctionType.StoreProcedure);
            cbxFunction.SelectedIndex = 0;
        }

        public void LoadInitGridControl()
        {
            LoadGridControlTables();
            LoadGridControlColumns();
            LoadGridControlReferences();
        }

        public void LoadGridControlTables()
        {
            gridControlTables.DataSource = lstTables;
        }

        public void LoadGridControlColumns()
        {
            gridControlColumns.DataSource = lstColumns;
        }

        public void LoadGridControlReferences()
        {
            gridControlReferences.DataSource = lstReferences;
        }

        #endregion

        #region Create file connect

        public void UpdateConfigConnect()
        {
            SQLConnectCfg.FromServerName = txtServerFrom.Text;
            SQLConnectCfg.FromUserName = txtUserFrom.Text;
            SQLConnectCfg.FromPassword = txtPasswordFrom.Text;
            SQLConnectCfg.FromDatabase = cbxDatabaseFrom.Text;
            SQLConnectCfg.ToServerName = txtServerTo.Text;
            SQLConnectCfg.ToUserName = txtUserTo.Text;
            SQLConnectCfg.ToPassword = txtPasswordTo.Text;
            SQLConnectCfg.ToDatabase = cbxDatabaseTo.Text;

            // Write to XML
            XmlSerializer writer = new XmlSerializer(typeof(ConfigConnectSQL));
            if (File.Exists("ConfigConnect.xml"))
            {
                File.Delete("ConfigConnect.xml");
            }

            using (FileStream file = File.OpenWrite("ConfigConnect.xml"))
            {
                writer.Serialize(file, SQLConnectCfg);
            }
        }

        public void LoadConfigConnect()
        {
            ConfigConnectSQL tx = new ConfigConnectSQL();

            XmlSerializer reader = new XmlSerializer(typeof(ConfigConnectSQL));
            try
            {
                using (FileStream input = File.OpenRead("ConfigConnect.xml"))
                {
                    tx = reader.Deserialize(input) as ConfigConnectSQL;
                }

                CopySQLConnect(tx, ref SQLConnectCfg);

                txtServerFrom.Text = SQLConnectCfg.FromServerName;
                txtUserFrom.Text = SQLConnectCfg.FromUserName;
                txtPasswordFrom.Text = SQLConnectCfg.FromPassword;
                cbxDatabaseFrom.Text = SQLConnectCfg.FromDatabase;
                txtServerTo.Text = SQLConnectCfg.ToServerName;
                txtUserTo.Text = SQLConnectCfg.ToUserName;
                txtPasswordTo.Text = SQLConnectCfg.ToPassword;
                cbxDatabaseTo.Text = SQLConnectCfg.ToDatabase;
            }
            catch { }
        }

        public void CopySQLConnect(ConfigConnectSQL SQLConnectFrom, ref ConfigConnectSQL SQLConnectTo)
        {
            SQLConnectTo.FromServerName = SQLConnectFrom.FromServerName;
            SQLConnectTo.FromUserName = SQLConnectFrom.FromUserName;
            SQLConnectTo.FromPassword = SQLConnectFrom.FromPassword;
            SQLConnectTo.FromDatabase = SQLConnectFrom.FromDatabase;
            SQLConnectTo.ToServerName = SQLConnectFrom.ToServerName;
            SQLConnectTo.ToUserName = SQLConnectFrom.ToUserName;
            SQLConnectTo.ToPassword = SQLConnectFrom.ToPassword;
            SQLConnectTo.ToDatabase = SQLConnectFrom.ToDatabase;
        }

        #endregion

        public void LoadDatabase(ref ComboBoxEdit cbx, String strServer, String strUser, String strPassword)
        {
            if (ConnectDatabase.LoadDatabase.LoadDatabaseCombobox(ref cbx, strServer, strUser, strPassword))
            {
                UpdateConfigConnect();
            }
        }

        public FunctionType GetFunctionType()
        {
            try
            {
                FunctionType function = (FunctionType)cbxFunction.SelectedItem;
                return function;
            }
            catch
            {
                return FunctionType.Table;
            }
        }

        #region Process

        public void ActionProcess()
        {
            UpdateConfigConnect();

            if (GetFunctionType() == FunctionType.Table)
            {
                ActionProcessTable();
            }
            else if (GetFunctionType() == FunctionType.StoreProcedure)
            {
                ActionProcessStoreProcedure();
            }
        }

        public void ActionProcessTable()
        {
            lstTables.Clear();
            lstColumns.Clear();
            lstReferences.Clear();
            GMCWaitingDialog.HideDialog();
            GMCWaitingDialog.Title = "Vui lòng chờ ";
            GMCWaitingDialog.Caption = "Đang lấy dữ liệu ....";
            GMCWaitingDialog.Show();

            if (SQLConnect.ChangeServer(SQLConnectCfg, true) && SQLConnect.ChangeServer(SQLConnectCfg, false))
            {
                lstAllDataSources = objCompareDatabaseCtrl.GetAllTableDifference(SQLConnectCfg);
                SetDataTables(lstAllDataSources);
            }

            GMCWaitingDialog.HideDialog();
            gridControlTables.RefreshDataSource();
            gridControlColumns.RefreshDataSource();
            gridControlReferences.RefreshDataSource();
        }

        public void ActionProcessStoreProcedure()
        {
            lstTables.Clear();
            lstColumns.Clear();
            lstReferences.Clear();
            GMCWaitingDialog.HideDialog();
            GMCWaitingDialog.Title = "Vui lòng chờ ";
            GMCWaitingDialog.Caption = "Đang lấy dữ liệu ....";
            GMCWaitingDialog.Show();

            if (SQLConnect.ChangeServer(SQLConnectCfg, true) && SQLConnect.ChangeServer(SQLConnectCfg, false))
            {
                lstStoreProcedure = objCompareDatabaseCtrl.GetAllStoreProcedureDifference(SQLConnectCfg);
                foreach (String key in lstStoreProcedure.Keys)
                {
                    Tables tb = new Tables();
                    tb.Table_Name = key;
                    tb.Selected = false;
                    lstTables.Add(tb);
                }
                gridControlTables.DataSource = lstTables;
                gridControlTables.RefreshDataSource();
            }

            GMCWaitingDialog.HideDialog();
            gridControlTables.RefreshDataSource();
            gridControlColumns.RefreshDataSource();
            gridControlReferences.RefreshDataSource();
        }

        public void SetDataTables(List<AllDataSources> lstAllDatas)
        {
            foreach (AllDataSources Data in lstAllDatas)
            {
                Tables tb = new Tables();
                tb.Selected = false;
                tb.Table_Name = Data.Table_Name;
                tb.IsExist = Data.IsExist;
                tb.IsMoreColumn = Data.IsMoreColumn;
                lstTables.Add(tb);
            }
            gridControlTables.DataSource = lstTables;
            gridControlTables.RefreshDataSource();
        }

        #endregion

        #region Compare

        public void ActionCompare()
        {
            if (GetFunctionType() == FunctionType.Table)
            {
                ActionCompareTable();
            }
            else if (GetFunctionType() == FunctionType.StoreProcedure)
            {
                ActionCompareStoreProcedure();
            }
        }

        public void ActionCompareTable()
        {
            #region Create Table

            List<String> dsTableNotExists = new List<String>();
            foreach (Tables tb in lstTables)
            {
                if (tb.IsExist == false && tb.Selected)
                {
                    dsTableNotExists.Add(tb.Table_Name);
                }
            }

            if (dsTableNotExists.Count > 0)
            {
                if (SQLConnect.ChangeServer(SQLConnectCfg, false))
                {
                    compareCtrl.CreateTable(dsTableNotExists);
                }
            }
            GMCWaitingDialog.HideDialog();
            #endregion

            #region Create Column

            if (lstColumns.Count == 0 && lstReferences.Count == 0)
            {
                MessageBox.Show("Không có Columns để thực hiện");
                return;
            }

            List<String> dsTables = compareCtrl.GetTableNotDuplicate(lstTables);

            if (dsTables.Count == 0)
            {
                MessageBox.Show("Không lấy được Table nào!");
                return;
            }

            if (SQLConnect.ChangeServer(SQLConnectCfg, false))
            {
                compareCtrl.CreateColumn(dsTables, lstColumns);
            }
            GMCWaitingDialog.HideDialog();
            #endregion

            #region Create ForeignKey

            if (SQLConnect.ChangeServer(SQLConnectCfg, false))
            {
                compareCtrl.CreateReference(lstReferences);
            }
            GMCWaitingDialog.HideDialog();
            #endregion

            compareCtrl.GenStoreTable(dsTables);

            ActionProcess();
        }

        public void ActionCompareStoreProcedure()
        {
            GMCWaitingDialog.HideDialog();
            GMCWaitingDialog.Title = "Vui lòng chờ ";
            GMCWaitingDialog.Caption = "Đang lấy dữ liệu ....";
            GMCWaitingDialog.Show();

            #region Create Store

            Dictionary<String, String> lstStoreGen = new Dictionary<String, String>();

            foreach (Tables tb in lstTables)
            {
                if (tb.Selected)
                {
                    lstStoreGen.Add(tb.Table_Name, lstStoreProcedure[tb.Table_Name]);
                }
            }

            #endregion

            GMCWaitingDialog.HideDialog();

            ActionProcess();
        }

        #endregion

        public void RefreshTable()
        {
            int i = 0;
            foreach (Tables tb in lstTables)
            {
                if (tb.Selected)
                {
                    foreach (AllDataSources AllData in lstAllDataSources)
                    {
                        if (AllData.Table_Name == tb.Table_Name)
                        {
                            foreach (Columns cl in AllData.ListColumns)
                            {
                                if (!CheckTable(cl.Table_Name, "Column", cl.Column_Name))
                                {
                                    lstColumns.Add(cl);
                                }
                            }

                            foreach (References rf in AllData.ListReferences)
                            {
                                if (!CheckTable(rf.TableNameForeignKey, "Reference", rf.ColumnNameForeignKey))
                                {
                                    lstReferences.Add(rf);
                                }
                            }
                        }
                    }
                }
                else
                {
                    foreach (AllDataSources AllData in lstAllDataSources)
                    {
                        if (AllData.Table_Name == tb.Table_Name)
                        {
                            foreach (Columns cl in AllData.ListColumns)
                            {
                                if (CheckTable(cl.Table_Name, "Column", cl.Column_Name))
                                {
                                    cl.Selected = false;
                                    lstColumns.Remove(cl);
                                }
                            }

                            foreach (References rf in AllData.ListReferences)
                            {
                                if (CheckTable(rf.TableNameForeignKey, "Reference", rf.ColumnNameForeignKey))
                                {
                                    rf.Selected = false;
                                    lstReferences.Remove(rf);
                                }
                            }
                        }
                    }
                }
                i++;
            }
            gridControlColumns.RefreshDataSource();
            gridControlReferences.RefreshDataSource();
        }

        public Boolean CheckTable(String strTableNames, String strType, String strField)
        {
            if (strType == "Column")
            {
                foreach (Columns cl in lstColumns)
                {
                    if (cl.Table_Name == strTableNames && cl.Column_Name == strField)
                    {
                        return true;
                    }
                }
            }
            else
            {
                foreach (References rf in lstReferences)
                {
                    if (rf.TableNameForeignKey == strTableNames && rf.ColumnNameForeignKey == strField)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        #endregion
    }
}
