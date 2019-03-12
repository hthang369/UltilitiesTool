using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SQLAppLib;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Repository;

namespace ImportConfigTool
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        DataTable dtSource;
        BindingSource bds;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SQLUtil.LoadConfigIniFromServer(cboServer);
            dtSource = new DataTable();
            bds = new BindingSource();
            bds.DataSource = dtSource;
            gridControl1.DataSource = bds;
        }

        private void cboServer_Validated(object sender, EventArgs e)
        {
            int idx = (sender as DevExpress.XtraEditors.ComboBoxEdit).SelectedIndex;
            cboDatabase.Properties.Items.AddRange(SQLUtil.LoadDatabaseByServer(idx)); 
        }

        private void LoadDataGridConfig()
        {
            DataTable dtColumns = SQLDBUtil.GetTableColumnCaptions("GLAccountingImportMapCols");
            foreach (DataRow dr in dtColumns.Rows)
            {
                string strColumnName = Convert.ToString(dr["COLUMN_NAME"]);
                dtSource.Columns.Add(strColumnName);
                if (SQLDBUtil.ColumnIsPrimaryKey("GLAccountingImportMapCols", strColumnName)) continue;
                if (strColumnName.EndsWith("TypeCombo") || strColumnName.EndsWith("IsCheck")) continue;
                DevExpress.XtraGrid.Columns.GridColumn col = ((GridView)gridControl1.MainView).Columns.AddVisible(strColumnName, Convert.ToString(dr["CAPTION_NAME"]));
                if (strColumnName.EndsWith("TableName"))
                {
                    RepositoryItemLookUpEdit lke = new RepositoryItemLookUpEdit();
                    lke.QueryPopUp += Lke_QueryPopUp;
                    lke.DataSource = SQLDBUtil.GetDataTableByDataSet(SQLDBUtil.GetAllTables());
                    lke.ValueMember = "TableName";
                    lke.DisplayMember = "TableName";
                    col.ColumnEdit = lke;
                }
            }
        }

        private void Lke_QueryPopUp(object sender, CancelEventArgs e)
        {
            DataTable dt = LoadTableNameByModule(Convert.ToString((sender as DevExpress.XtraEditors.LookUpEdit).EditValue));
        }

        private void cboDatabase_Validated(object sender, EventArgs e)
        {
            string strDBName = Convert.ToString((sender as DevExpress.XtraEditors.ComboBoxEdit).EditValue);
            if (string.IsNullOrEmpty(strDBName)) return;
            SQLDBUtil.ChangeDatabase(strDBName);
            LoadDataGridConfig();
            cboModule.Properties.Items.AddRange(LoadModuleConfig().Select().Select(x => Convert.ToString(x["GLAccountingImportMapColTypeCombo"])).ToList());
        }
        private DataTable LoadTableNameByModule(string strModule)
        {
            string strQuery = string.Format(@"SELECT STModuleTableName FROM STModules a JOIN STModuleTables b ON a.STModuleID = b.STModuleID
                                WHERE STModuleName = '{0}'", strModule);
            return SQLDBUtil.GetDataTable(strQuery);
        }
        private DataTable LoadModuleConfig()
        {
            string strQuery = "SELECT GLAccountingImportMapColTypeCombo FROM GLAccountingImportMapCols GROUP BY GLAccountingImportMapColTypeCombo";
            return SQLDBUtil.GetDataTable(strQuery);
        }
        private DataTable LoadConfigByModule(string strModule)
        {
            return SQLDBUtil.GetDataTable(string.Format("SELECT * FROM GLAccountingImportMapCols WHERE GLAccountingImportMapColTypeCombo = '{0}'", strModule));
        }

        private void cboModule_Validated(object sender, EventArgs e)
        {
            string strModule = Convert.ToString((sender as DevExpress.XtraEditors.ComboBoxEdit).EditValue);
            if (string.IsNullOrEmpty(strModule)) return;
            bds.DataSource = LoadConfigByModule(strModule);
            //gridControl1.DataSource = dtSource;
            gridControl1.RefreshDataSource();
        }
    }
}
