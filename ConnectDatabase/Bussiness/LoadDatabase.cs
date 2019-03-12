using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using System.Data;

namespace ConnectDatabase
{
    public class LoadDatabase
    {
        private static List<string> lstDatabases;
        public static Boolean LoadDatabaseCombobox(ref ComboBoxEdit cbx, String strServer, String strUser, String strPassword)
        {
            bool flag = true;
            flag = LoadDatabaseControl(strServer, strUser, strPassword);
            cbx.Properties.Items.Clear();
            if (lstDatabases.Count == 0) return false;
            foreach (string dbName in lstDatabases)
            {
                cbx.Properties.Items.Add(dbName);
            }
            return flag;
        }

        public static Boolean LoadDatabaseCombobox(ref System.Windows.Forms.ComboBox cbx, String strServer, String strUser, String strPassword)
        {
            bool flag = true;
            flag = LoadDatabaseControl(strServer, strUser, strPassword);
            cbx.Items.Clear();
            if (lstDatabases.Count == 0) return false;
            foreach (string dbName in lstDatabases)
            {
                cbx.Items.Add(dbName);
            }
            return flag;
        }

        private static Boolean LoadDatabaseControl(String strServer, String strUser, String strPassword)
        {
            #region Exception

            if (string.IsNullOrEmpty(strServer))
            {
                MessageBox.Show("Nhập tên server!");
                return false;
            }

            if (string.IsNullOrEmpty(strUser))
            {
                MessageBox.Show("Nhập user name!");
                return false;
            }

            if (string.IsNullOrEmpty(strPassword))
            {
                MessageBox.Show("Nhập password!");
                return false;
            }

            #endregion

            lstDatabases = new List<string>();

            ExpertLib.GMCWaitingDialog.Title = "Get Database";
            ExpertLib.GMCWaitingDialog.Caption = "Please waiting ...";
            ExpertLib.GMCWaitingDialog.Show();

            try
            {
                SQLConnect.ChangeServer(strServer, "", strUser, strPassword);
                DataSet ds = SQLConnect.GetDatabases();
                if (ds == null && ds.Tables.Count == 0) return false;
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    string tableName = item["name"].ToString();
                    lstDatabases.Add(tableName);
                }
            }
            catch
            {
                ExpertLib.GMCWaitingDialog.HideDialog();
                MessageBox.Show("Kết nối database " + strServer + " thất bại !");
                return false;
            }

            ExpertLib.GMCWaitingDialog.HideDialog();
            return true;
        }
    }
}
