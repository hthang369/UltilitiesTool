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
using DevExpress.XtraEditors;
namespace SetTabIndex
{
    public partial class FormGenScriptDB : Form
    {
        public GenScriptDBController control;
        public string fileName = Application.StartupPath + "\\info.txt";
        public FormGenScriptDB()
        {
            InitializeComponent();
            control = new GenScriptDBController();
            InitGridView(new Dictionary<string, string>() { { "Selected", "Chọn" }, { "Table_Name", "Tên bảng" } });
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
                column.AppearanceHeader.Options.UseTextOptions = true;
                column.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
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
            string chkFill = radTable.Checked ? radTable.Tag.ToString() : (radStore.Checked ? radStore.Tag.ToString() : string.Empty);
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

        private void btnGenScript_Click(object sender, EventArgs e)
        {
            GMCWaitingDialog.SetTitle("Đang ghi dữ liệu ...");
            GMCWaitingDialog.Show();
            List<Tables> lstTable = (List<Tables>)grcControl.DataSource;
            List<Tables> lstObject = new List<Tables>();
            if (chkAll.Checked)
                lstObject = lstTable;
            else
                lstObject = lstTable.Where(x => x.Selected == true).ToList();

            if (lstObject.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                GMCWaitingDialog.HideDialog();
                return;
            }

            string filename = cbbDatabase.SelectedItem.ToString() + DateTime.Now.ToString("yyMMddhhmmss") + ".txt";
            FileStream FS = new FileStream(Application.StartupPath + "\\" + filename, FileMode.OpenOrCreate);
            using (StreamWriter w = new StreamWriter(FS))
            {
                int idx = 0;
                GMCWaitingDialog.SetProgress(0, lstObject.Count);
                foreach (Tables objName in lstObject)
                {
                    //if (radTable.Checked && objName.Type == "U")
                    //{
                    //    w.WriteLine(GenerateObjectFromDataSet(control.GenerateTable(objName.Table_Name)));
                    //    w.WriteLine(control.GenerateForeignKey(objName.Table_Name));
                    //    if (chkData.Checked)
                    //        w.WriteLine(control.GenerateInsert(objName.Table_Name));
                    //}

                    //if (radStore.Checked && objName.Type == "P")
                    //    w.WriteLine(GenerateObjectFromDataSet(control.GenerateStore(objName.Table_Name)));
                    //w.Flush();
                    idx++;
                    GMCWaitingDialog.SetPosition(idx);
                }
            }
            GMCWaitingDialog.HideDialog();
        }

        private void LoadDataSource(string fill)
        {
            GMCWaitingDialog.SetTitle("Đang lấy dữ liệu ...");
            GMCWaitingDialog.Show();
            List<Tables> lstObject = new List<Tables>();
            if (control.ObjectsList.Count == 0)
                lstObject = control.GetAllDataSource(fill);
            else if (!string.IsNullOrEmpty(fill))
                lstObject = control.ObjectsList.Where(x => x.Type.Contains(fill)).ToList();
            else lstObject = control.ObjectsList;
            if (lstObject.Count > 0)
                grcControl.DataSource = lstObject;
            GMCWaitingDialog.HideDialog();
            grcControl.MainView.GridControl.RefreshDataSource();
        }

        private void radObject_CheckedChanged(object sender, EventArgs e)
        {
            string fillTag = string.Empty;
            if (radTable.Checked && !radStore.Checked)
                fillTag = radTable.Tag.ToString();
            else if (!radTable.Checked && radStore.Checked)
                fillTag = radStore.Tag.ToString();
            LoadDataSource(fillTag);
        }

        private string GenerateObjectFromDataSet(DataSet ds)
        {
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                return string.Join(",", ds.Tables[0].Rows.Cast<string>().ToArray());
            return string.Empty;
        }

        private void radAll_CheckedChanged(object sender, EventArgs e)
        {
            radTable.Enabled = radStore.Enabled = btnGenScript.Enabled = radAll.Checked;
        }
    }
  
}
