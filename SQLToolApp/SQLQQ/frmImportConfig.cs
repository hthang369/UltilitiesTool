using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SQLAppLib;

namespace SQLQQ
{
    public partial class frmImportConfig : Form
    {
        DataTable dtTbl;
        DataTable dtCol;
        BindingSource bds;
        public frmImportConfig()
        {
            InitializeComponent();
        }

        private void frmImportConfig_Load(object sender, EventArgs e)
        {
            bds = new BindingSource();
            DataTable dt = SQLDBUtil.GetDataTable("SELECT DISTINCT GLAccountingImportMapColTypeCombo FROM GLAccountingImportMapCols WHERE GLAccountingImportMapColTypeCombo <> ''");
            cboType.DataSource = dt;
            cboType.DisplayMember = "GLAccountingImportMapColTypeCombo";
            cboType.ValueMember = "GLAccountingImportMapColTypeCombo";
            dt = SQLDBUtil.GetDataTable("SELECT name FROM sys.columns WHERE object_id = OBJECT_ID('GLAccountingImportMapCols')");
            dgv.dgvResult.SelectionChanged += view_SelectionChanged;
            dgv.AllowEdit = true;
            dgv.dgvResult.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
            dgv.dgvResult.AutoGenerateColumns = false;
            dtTbl = SQLDBUtil.GetDataByTable("sys.tables", "name like 'View%'", "name");
            bds.DataSource = SQLDBUtil.GetDataTable("select c.name, t.name as TblName from sys.columns c join sys.tables t on t.object_id = c.object_id and t.name like 'View%'");
            dgv.dgvResult.Columns.AddRange(new DataGridViewColumn[]
            {
                GenarateColumn("#ID","GLAccountingImportMapColID","GLAccountingImportMapCols",50,1),
                GenarateColumn("Table Name","GLAccountingImportMapColTableName","GLAccountingImportMapCols",150,2,true,false, dtTbl,"name","name"),
                GenarateColumn("Column Name","GLAccountingImportMapColColumnName","GLAccountingImportMapCols",250,3,true,false, bds.DataSource,"name","name"),
                GenarateColumn("Mapping Column Name","GLAccountingImportMapColMapColumnName","GLAccountingImportMapCols",250,4),
                GenarateColumn("Caption","GLAccountingImportMapColCaption","GLAccountingImportMapCols",200,5),
                GenarateColumn("Copy Data","GLAccountingImportMapColCopyCheck","GLAccountingImportMapCols",120,6),
                GenarateColumn("Required Check","GLAccountingImportMapColRequiredCheck","GLAccountingImportMapCols",150,7),
                GenarateColumn("Exsits Check","GLAccountingImportMapColExsitsCheck","GLAccountingImportMapCols",120,8),
                GenarateColumn("Not Exsits Check","GLAccountingImportMapColNotExsitsCheck","GLAccountingImportMapCols",180,9),
                GenarateColumn("FK Check","GLAccountingImportMapColFKCheck","GLAccountingImportMapCols",120,10),
                GenarateColumn("Is Number Check","GLAccountingImportMapColIsNumberCheck","GLAccountingImportMapCols",150,11),
                GenarateColumn("Is Date Check","GLAccountingImportMapColIsDateCheck","GLAccountingImportMapCols",120,12),
                GenarateColumn("Is Length Check","GLAccountingImportMapColIsLengthCheck","GLAccountingImportMapCols",150,13),
                GenarateColumn("Is Account Check","GLAccountingImportMapColIsAccountCheck","GLAccountingImportMapCols",150,14),
                GenarateColumn("ADConfigValue Check","GLAccountingImportMapColADConfigValueCheck","GLAccountingImportMapCols",180,15),
                GenarateColumn("Analysis Cost Check","GLAccountingImportMapColAnalysisCostCheck","GLAccountingImportMapCols",180,16),
                GenarateColumn("Dupplicate Check","GLAccountingImportMapColDupplicateCheck","GLAccountingImportMapCols",150,17),
            });
        }

        private DataGridViewComboBoxColumn GenarateComboboxColumn(DataGridViewColumn column, DataTable dtSource)
        {
            DataGridViewComboBoxColumn cboColumn = new DataGridViewComboBoxColumn();
            cboColumn.Name = column.Name;
            cboColumn.DataPropertyName = column.DataPropertyName;
            cboColumn.Width = column.Width;
            cboColumn.HeaderText = column.HeaderText;
            DataGridViewComboBoxCell cboCell = new DataGridViewComboBoxCell();
            cboCell.DataSource = dtSource;
            cboCell.DisplayMember = "name";
            cboCell.ValueMember = "name";
            cboColumn.CellTemplate = cboCell;
            cboColumn.DisplayIndex = column.DisplayIndex;
            return cboColumn;
        }
        private void btnView_Click(object sender, EventArgs e)
        {
            dgv.LoadData(string.Format("SELECT * FROM GLAccountingImportMapCols WHERE GLAccountingImportMapColTypeCombo = '{0}'", cboType.SelectedValue));
        }

        public void view_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView dgvResult = (DataGridView)sender;
            if (dgvResult.CurrentCell == null || dgvResult.CurrentRow == null) return;
            DataGridViewColumn column = dgvResult.Columns[dgvResult.CurrentCell.ColumnIndex];
            string columnName = column.DataPropertyName;
            dgvResult.CurrentCell.ReadOnly = false;
            int idxRow = dgvResult.CurrentRow.Index;
            if (columnName == "GLAccountingImportMapColColumnName")
            {
                string tableName = Convert.ToString(dgvResult["GLAccountingImportMapColTableName", idxRow].Value);
                bds.Filter = string.Format("TblName like '{0}'", tableName);
                if (!Convert.ToString(dgvResult.CurrentCell.Value).StartsWith("FK_"))
                    dgvResult["GLAccountingImportMapColFKCheck", idxRow].ReadOnly = true;
            }
            else if(columnName == "GLAccountingImportMapColFKCheck")
            {
                if (!Convert.ToString(dgvResult["GLAccountingImportMapColColumnName", idxRow].Value).StartsWith("FK_"))
                    dgvResult.CurrentCell.ReadOnly = true;
            }
            else if(columnName == "GLAccountingImportMapColIsNumberCheck")
            {
                string table = Convert.ToString(dgvResult["GLAccountingImportMapColTableName", idxRow].Value);
                string colName = Convert.ToString(dgvResult["GLAccountingImportMapColColumnName", idxRow].Value);
                string type = SQLDBUtil.GetColumnDataType(table, colName);
                List<string> lstType = new List<string>() { "int", "float", "smallint", "tinyint", "decimal", "numeric", "bigint" };
                if (lstType.IndexOf(type) == -1 || colName.StartsWith("FK"))
                    dgvResult.CurrentCell.ReadOnly = true;
            }
            else if (columnName == "GLAccountingImportMapColIsDateCheck")
            {
                string table = Convert.ToString(dgvResult["GLAccountingImportMapColTableName", idxRow].Value);
                string colName = Convert.ToString(dgvResult["GLAccountingImportMapColColumnName", idxRow].Value);
                string type = SQLDBUtil.GetColumnDataType(table, colName);
                List<string> lstType = new List<string>() { "date", "time", "datetime2", "datetimeoffset", "smalldatetime", "datetime", "timestamp" };
                if (lstType.IndexOf(type) == -1)
                    dgvResult.CurrentCell.ReadOnly = true;
            }
            else if (columnName == "GLAccountingImportMapColIsLengthCheck")
            {
                string table = Convert.ToString(dgvResult["GLAccountingImportMapColTableName", idxRow].Value);
                string colName = Convert.ToString(dgvResult["GLAccountingImportMapColColumnName", idxRow].Value);
                string type = SQLDBUtil.GetColumnDataType(table, colName);
                List<string> lstType = new List<string>() { "text", "ntext", "varchar", "char", "nvarchar", "nchar" };
                if (lstType.IndexOf(type) == -1)
                    dgvResult.CurrentCell.ReadOnly = true;
            }
            else if (columnName == "GLAccountingImportMapColIsAccountCheck")
            {
                string table = Convert.ToString(dgvResult["GLAccountingImportMapColTableName", idxRow].Value);
                string colName = Convert.ToString(dgvResult["GLAccountingImportMapColColumnName", idxRow].Value);
                bool isEdit = true;
                DataSet ds = SQLDBUtil.GetPrimaryTableWhichForeignKeyTable(table, colName);
                DataTable dt = SQLDBUtil.GetDataTableByDataSet(ds);
                if (dt.Rows.Count == 0) isEdit = false;
                else if(!dt.Rows[0]["ReferenceTableName"].Equals("GLAccounts")) isEdit = false;
                if (!isEdit)
                    dgvResult.CurrentCell.ReadOnly = true;
            }
        }
        private DataGridViewColumn GenarateColumn(string strCaption, string strName, string strTableName, int iWidth, int index, bool isVisible = true, bool isReadOnly = true, object strRespontoryTable = null, string strRespontoryDisplayColumn = "", string strRespontoryValueColumn = "", string strRespontoryColumn = "")
        {
            DataGridViewColumn column = new DataGridViewColumn();
            string type = SQLDBUtil.GetColumnDataType(strTableName, strName);
            if (type == "bit")
                column = GenarateCheckBoxColumn(strCaption, strName, strTableName, iWidth, index, isVisible, isReadOnly);
            else if(type == "image")
                column = GenarateImageColumn(strCaption, strName, strTableName, iWidth, index, isVisible, isReadOnly);
            else if(strRespontoryTable != null && strRespontoryTable.GetType() == typeof(string))
                column = GenarateComboBoxColumn(strCaption, strName, strTableName, iWidth, index, isVisible, isReadOnly, Convert.ToString(strRespontoryTable), strRespontoryDisplayColumn, strRespontoryValueColumn, strRespontoryColumn);
            else if (strRespontoryTable != null && strRespontoryTable.GetType() == typeof(DataTable))
                column = GenarateComboBoxColumn(strCaption, strName, strTableName, iWidth, index, isVisible, isReadOnly, (DataTable)strRespontoryTable, strRespontoryDisplayColumn, strRespontoryValueColumn);
            else
                column = GenarateTextBoxColumn(strCaption, strName, strTableName, iWidth, index, isVisible, isReadOnly);
            return column;
        }
        private DataGridViewTextBoxColumn GenarateTextBoxColumn(string strCaption, string strName, string strTableName, int iWidth, int index, bool isVisible, bool isReadOnly)
        {
            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
            column.HeaderText = strCaption;
            column.Name = column.DataPropertyName = strName;
            column.Visible = isVisible;
            column.ReadOnly = isReadOnly;
            column.Width = iWidth;
            column.DisplayIndex = index;
            return column;
        }
        private DataGridViewLinkColumn GenarateLinkColumn(string strCaption, string strName, string strTableName, int iWidth, int index, bool isVisible, bool isReadOnly)
        {
            DataGridViewLinkColumn column = new DataGridViewLinkColumn();
            column.HeaderText = strCaption;
            column.Name = column.DataPropertyName = strName;
            column.Visible = isVisible;
            column.ReadOnly = isReadOnly;
            column.Width = iWidth;
            column.DisplayIndex = index;
            return column;
        }
        private DataGridViewCheckBoxColumn GenarateCheckBoxColumn(string strCaption, string strName, string strTableName, int iWidth, int index, bool isVisible, bool isReadOnly)
        {
            DataGridViewCheckBoxColumn column = new DataGridViewCheckBoxColumn();
            column.HeaderText = strCaption;
            column.Name = column.DataPropertyName = strName;
            column.Visible = isVisible;
            column.ReadOnly = isReadOnly;
            column.Width = iWidth;
            column.DisplayIndex = index;
            return column;
        }
        private DataGridViewImageColumn GenarateImageColumn(string strCaption, string strName, string strTableName, int iWidth, int index, bool isVisible, bool isReadOnly)
        {
            DataGridViewImageColumn column = new DataGridViewImageColumn();
            column.HeaderText = strCaption;
            column.Name = column.DataPropertyName = strName;
            column.Visible = isVisible;
            column.ReadOnly = isReadOnly;
            column.Width = iWidth;
            column.DisplayIndex = index;
            return column;
        }
        private DataGridViewComboBoxColumn GenarateComboBoxColumn(string strCaption, string strName, string strTableName, int iWidth, int index, bool isVisible, bool isReadOnly, string strRespontoryTable = "", string strRespontoryDisplayColumn = "", string strRespontoryValueColumn = "", string strRespontoryColumn = "")
        {
            DataGridViewComboBoxColumn column = new DataGridViewComboBoxColumn();
            column.HeaderText = strCaption;
            column.Name = column.DataPropertyName = strName;
            column.Visible = isVisible;
            column.ReadOnly = isReadOnly;
            column.Width = iWidth;
            column.DisplayIndex = index;
            if (!string.IsNullOrEmpty(strRespontoryTable))
            {
                List<string> lstColumns = strRespontoryColumn.Split(',').ToList();
                lstColumns.AddItem(strRespontoryDisplayColumn);
                lstColumns.AddItem(strRespontoryValueColumn);
                DataGridViewComboBoxCell cell = new DataGridViewComboBoxCell();
                string where = (SQLDBUtil.ColumnIsExistInTable(strRespontoryTable, SQLDBUtil.AAStatusColumn)) ? string.Format("{0} = 'Alive'", SQLDBUtil.AAStatusColumn) : "";
                cell.DataSource = SQLDBUtil.GetDataByTable(strRespontoryTable, where, string.Join(",", lstColumns.ToArray()));
                cell.ValueMember = strRespontoryValueColumn;
                cell.DisplayMember = strRespontoryDisplayColumn;
                column.CellTemplate = cell;
            }
            return column;
        }
        private DataGridViewComboBoxColumn GenarateComboBoxColumn(string strCaption, string strName, string strTableName, int iWidth, int index, bool isVisible, bool isReadOnly, DataTable dtSource = null, string strRespontoryDisplayColumn = "", string strRespontoryValueColumn = "")
        {
            DataGridViewComboBoxColumn column = new DataGridViewComboBoxColumn();
            column.HeaderText = strCaption;
            column.Name = column.DataPropertyName = strName;
            column.Visible = isVisible;
            column.ReadOnly = isReadOnly;
            column.Width = iWidth;
            column.DisplayIndex = index;
            if (dtSource != null)
            {
                DataGridViewComboBoxCell cell = new DataGridViewComboBoxCell();
                cell.DataSource = dtSource;
                cell.ValueMember = strRespontoryValueColumn;
                cell.DisplayMember = strRespontoryDisplayColumn;
                column.CellTemplate = cell;
            }
            return column;
        }
        private DataGridViewButtonColumn GenarateButtonColumn(string strCaption, string strName, string strTableName, int iWidth, int index, bool isVisible, bool isReadOnly)
        {
            DataGridViewButtonColumn column = new DataGridViewButtonColumn();
            column.HeaderText = strCaption;
            column.Name = column.DataPropertyName = strName;
            column.Visible = isVisible;
            column.ReadOnly = isReadOnly;
            column.Width = iWidth;
            column.DisplayIndex = index;
            return column;
        }
    }
}
