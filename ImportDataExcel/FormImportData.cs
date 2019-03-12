using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using ExpertLib;
using System.IO;
using ConnectDatabase;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using ExpertERPProcedure.Utilities;
using ExpertERP.BusinessEntities;
using GemBox.Spreadsheet;

namespace SetTabIndex
{
    public partial class FormImportData : Form
    {
        public ImportDataController control;
        public string fileName = Application.StartupPath + "\\info.txt";
        DataSet dsImport;
        public FormImportData()
        {
            InitializeComponent();
            control = new ImportDataController();
            
        }

        #region Event

        private void Form1_Load(object sender, EventArgs e)
        {
            docFile(fileName);
        }

        private void InitGridView(Dictionary<string,string> dicField)
        {
            List<GridColumn> lstColumn = new List<GridColumn>();
            foreach (string field in dicField.Keys)
            {
                GridColumn column = new GridColumn();
                column.FieldName = field;
                column.Name = field;
                column.Caption = dicField[field];
                column.Visible = true;
                column.VisibleIndex++;
                lstColumn.Add(column);
            }
            gvControl.Columns.AddRange(lstColumn.ToArray());
            
        }

        private void docFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                var lines = from line in File.ReadAllLines(fileName) where line.Contains(txtServerName.Text) select line.Split('|');
                string[][] strChuoi = lines.ToArray();
                if (strChuoi.Count() > 0)
                {
                    txtServerName.Text = strChuoi[0][0];
                    txtUserName.Text = strChuoi[0][1];
                    txtPassword.Text = strChuoi[0][2];
                }
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            LoadDatabase.LoadDatabaseCombobox(ref cbbDatabase, txtServerName.Text, txtUserName.Text, txtPassword.Text);
        }

        private void cbbDatabase_SelectedIndexChanged(object sender, EventArgs e)
        {
            SQLConnect.ChangeServer(txtServerName.Text, cbbDatabase.SelectedItem.ToString(), txtUserName.Text, txtPassword.Text);
            string chkFill = string.Empty;
            LoadDataSource(chkFill);
        }

        private void cbbDatabase_Enter(object sender, EventArgs e)
        {
            LoadDatabase.LoadDatabaseCombobox(ref cbbDatabase, txtServerName.Text, txtUserName.Text, txtPassword.Text);
            List<string> strChuoi = new List<string>();
            FileStream FS = null;
            if (!File.Exists(fileName))
                FS = new FileStream(fileName, FileMode.CreateNew);
            else
            {
                var lines = from line in File.ReadAllLines(fileName) select line;
                FS = new FileStream(fileName, FileMode.Truncate);
                strChuoi = lines.ToList();
            }
            using (StreamWriter w = new StreamWriter(FS))
            {
                if (strChuoi.Count == 0)
                {
                    w.WriteLine(string.Join("|", new string[] { txtServerName.Text, txtUserName.Text, txtPassword.Text }));
                    w.Flush();
                }
                bool flag = true;
                for (int i = 0; i < strChuoi.Count; i++ )
                {
                    string[] item = null;
                    if (strChuoi[i].Contains(txtServerName.Text))
                    {
                        item = strChuoi[i].Split('|');
                        item[1] = txtUserName.Text;
                        item[2] = txtPassword.Text;
                        strChuoi[i] = string.Join("|", item);
                        flag = false;
                    }
                    else if(i == strChuoi.Count - 1 && flag)
                    {
                        item = new string[] { txtServerName.Text, txtUserName.Text, txtPassword.Text };
                        string newStr = string.Join("|", item);
                        strChuoi.Add(newStr);
                    }
                    w.WriteLine(strChuoi[i]);
                    w.Flush();
                }
            }
        }

        #endregion

        private void txtServerName_Validated(object sender, EventArgs e)
        {
            docFile(fileName);
        }

        private void LoadDataSource(string fill)
        {
            GMCWaitingDialog.SetTitle("Đang lấy dữ liệu ...");
            GMCWaitingDialog.Show();
            List<Tables> lstObject = control.GetAllDataSource(fill);
            if (lstObject.Count > 0)
                grcControl.DataSource = lstObject;
            GMCWaitingDialog.HideDialog();
            grcControl.MainView.GridControl.RefreshDataSource();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            OpenDataFromExcel();
            
        }

        public void OpenDataFromExcel()
        {
            #region đổ dữ liệu từ excel vào dataset
            dsImport = new DataSet();
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Excel file 2007(*.xlsx)|*.xlsx|Excel file 2003(*.xls)|*.xls|All Files (*.*)|*.*";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = fileDialog.FileName;
                if (File.Exists(fileName))
                {
                    
                    ExcelFile ef = new ExcelFile(fileName);
                    ExcelWorksheet ew = ef.Worksheets[0];
                    //DataTable dt = ew.ge
                    //ExcelClient exlclient = new ExcelClient(fileName);
                    //exlclient.OpenConnection();
                    //string[] sheets = exlclient.GetSheetName();
                    //if (sheets.Length > 0)
                    //    dsImport.Tables.Add(exlclient.GetAllDataFromSheet(sheets[0]));
                }
            }
            #endregion
        }

        

        private void btnImport_Click(object sender, EventArgs e)
        {
            //DataTable dt = (DataTable)grcControl.DataSource;

            ////DataSet ds = new DataSet();
            ////ds.Tables.Add(dt);
            //BaseBusinessController ctrl = new BaseBusinessController();
            ////List<BusinessObject> lstObj = ctrl.GetListFromDataset(ds);
            //foreach (DataRow dr in dt.Rows)
            //{
            //    BusinessObject obj = new BusinessObject();
            //    obj = (BusinessObject)ctrl.GetObjectFromDataRow(dr);
            //}
            
        }

        private void configToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmConfig frm = new FrmConfig();
            frm.Show();
        }
    }
  
}
