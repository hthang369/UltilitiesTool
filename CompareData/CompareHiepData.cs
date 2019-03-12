using ExpertERP.BusinessEntities;
using ExpertLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ConnectDatabase;


namespace CreateCompany
{
    public partial class CompareHiepData : Form
    {
        public CompareDatabasesController CompareDatabaseCtrl = new CompareDatabasesController();
        public CompareController CompareData = new CompareController();
        public CompareHiepData()
        {
            InitializeComponent();
            txtServerFrom.Text = "10.6.8.252,2000";
            txtUsernameFrom.Text = "sa";
            txtPasswordFrom.Text = "abc123";

            txtServerTo.Text = "10.6.8.252,2000";
            txtUsernameTo.Text = "sa";
            txtPasswordTo.Text = "abc123";
        }

        #region Kết Nối Database
        private void cbxDatabaseFrom_QueryPopUp(object sender, CancelEventArgs e)
        {
            int i = KiemTraTextKetNoi(txtServerFrom, txtUsernameFrom, txtPasswordFrom);
            if (i != 0) return;
            cbotable1.Properties.Items.Clear();
            LoadDatabase.LoadDatabaseCombobox(ref cbxDatabaseFrom, txtServerFrom.Text, txtUsernameFrom.Text, txtPasswordFrom.Text);
        }
        private void cbxDatabaseTo_QueryPopUp(object sender, CancelEventArgs e)
        {
            int i = KiemTraTextKetNoi(txtServerTo, txtUsernameTo, txtPasswordTo);
            if (i != 0) return;
            cbotable2.Properties.Items.Clear();
            LoadDatabase.LoadDatabaseCombobox(ref cbxDatabaseTo, txtServerTo.Text, txtUsernameTo.Text, txtPasswordTo.Text);
        }
        #endregion


        #region Compare Data

        public DataTable SetDataTable(DataSet ds, List<String> split)
        { 
             DataTable mydt = new DataTable();

             DataRow mydr;
            foreach(string s in split)
             mydt.Columns.Add(new DataColumn(s, typeof(string)));
            mydt.Columns.Add(new DataColumn("Nguon",typeof(bool)));
            mydt.Columns.Add(new DataColumn("Dich", typeof(bool)));
             foreach (DataRow row in ds.Tables[0].Rows)
             {
                 mydr = mydt.NewRow();
                 for (int i = 0; i < split.Count; i++)
                 {
                     mydr[i] = row[i];
                 }
                 mydr["Nguon"] = false;
                 mydr["Dich"] = false;
                 mydt.Rows.Add(mydr);
             }
            return mydt;
        }
        public DataTable SoSanhDuLieu(DataTable dt1, DataTable dt2, List<String> split, String columnName)
        {
            Dictionary<Object, CompareFromTo> dicFromTo = LayDsTableTheoKey(dt1, dt2,columnName);
            if (dicFromTo == null) return null;
            DataTable dt = new DataTable();
            foreach (string s in split)
            dt.Columns.Add(new DataColumn(s, typeof(string)));
            dt.Columns.Add(new DataColumn("Nguon", typeof(bool)));
            dt.Columns.Add(new DataColumn("Dich", typeof(bool)));
            dt.Columns.Add(new DataColumn("Chon", typeof(bool)));
            foreach (Object key in dicFromTo.Keys)
            {
                CompareFromTo comFromTo = dicFromTo[key];
                if (comFromTo.DataRowFrom.Count == 0) 
                {
                    foreach (DataRow row in comFromTo.DataRowTo)
                    {
                        row["Dich"] = true;
                        dt.Rows.Add(CreateDataTableFromDataRow(dt, row, split));
                    }
                }
                else if(comFromTo.DataRowTo.Count==0)
                {
                    foreach (DataRow row in comFromTo.DataRowFrom)
                    {
                        row["Nguon"] = true;
                        dt.Rows.Add(CreateDataTableFromDataRow(dt, row, split));
                    }
                }
               //else if(comFromTo.DataRowFrom.Count>comFromTo.DataRowTo.Count)
               // {
               //     foreach (DataRow row in comFromTo.DataRowTo)
               //     {
               //         row["Nguon"] = true;
               //         dt.Rows.Add(CreateDataTableFromDataRow(dt, row, split));
               //     }
               // }
               // else if (comFromTo.DataRowTo.Count > comFromTo.DataRowFrom.Count)
               // {
               //     foreach (DataRow row in comFromTo.DataRowFrom)
               //     {
               //         row["Dich"] = true;
               //         dt.Rows.Add(CreateDataTableFromDataRow(dt, row, split));
               //     }
               // }
                else
                {
                    //for (int i = 0; i < comFromTo.DataRowFrom.Count; i++)
                    //{
                        DataRow datarowfrom = comFromTo.DataRowFrom[0];
                        DataRow datarowto = comFromTo.DataRowTo[0];

                        if (KiemTraDuLieuGiongNhau(datarowfrom, datarowto, split) == false)
                        {
                            datarowfrom["Nguon"] = true;
                            dt.Rows.Add(CreateDataTableFromDataRow(dt, datarowfrom, split));
                            datarowto["Dich"] = true;
                            dt.Rows.Add(CreateDataTableFromDataRow(dt, datarowto, split));
                        }
                       
                  //  }
                }
            }
        
            return dt;
        }
        public bool KiemTraDuLieuGiongNhau(DataRow from, DataRow to, List<String> cols)
        {
            foreach (string col in cols)
            {
                if (from[col].Equals(to[col]) == false) return false;
            }
            return true;
        }
        public DataRow CreateDataTableFromDataRow(DataTable td, DataRow dr, List<String> plit)
        {
            DataRow drow = td.NewRow();
            foreach (String col in plit)
            {
                if (dr[col] != null && drow[col] != null)
                {
                    drow[col] = dr[col];                    
                }
            }
            drow["Nguon"]=dr["Nguon"];
            drow["Dich"] = dr["Dich"];
            return drow;
        }
        public  Dictionary<Object,CompareFromTo> LayDsTableTheoKey(DataTable dtfrom,DataTable dtto,String columnName)
        {
             if (!dtfrom.Columns.Contains(columnName)) return null;
            Dictionary<Object, CompareFromTo> listCompareFromTo = new  Dictionary<Object,CompareFromTo>();
            foreach (DataRow row in dtfrom.Rows)
            {
                Object key = row[columnName];
                if (key == null) continue;

                if(listCompareFromTo.ContainsKey(key)==false)
                {
                    CompareFromTo conFromTo=new CompareFromTo();
                    conFromTo.DataRowFrom.Add(row);
                    listCompareFromTo.Add(key,conFromTo);
                }
                else
                {
                    listCompareFromTo[key].DataRowFrom.Add(row);
                }
            }

            foreach (DataRow row in dtto.Rows)
            {
                Object key = row[columnName];
                if (key == null) continue;

                if (listCompareFromTo.ContainsKey(key) == false)
                {
                    CompareFromTo conFromTo = new CompareFromTo();
                    conFromTo.DataRowTo.Add(row);
                    listCompareFromTo.Add(key, conFromTo);
                }
                else
                {
                    listCompareFromTo[key].DataRowTo.Add(row);
                }
            }
            return listCompareFromTo;
        }
        public int KiemTraTextKetNoi(TextBox txtserver, TextBox txtuser, TextBox txtpass)
        {
            String s = String.Empty;
            if (string.IsNullOrEmpty(txtserver.Text))
            {
                s += "Nhập tên server!";
            }

            if (string.IsNullOrEmpty(txtuser.Text))
            {
                s += "\nNhập user name!";
            }

            if (string.IsNullOrEmpty(txtpass.Text))
            {
                s += "\nNhập password!";
            }
            if (!String.IsNullOrEmpty(s))
            {
                MessageBox.Show(s);
                return 1;
            }
            return 0;
        }
       
        #endregion    
           
        #region Hàm Sự kiện

        private void btCompare_Click(object sender, EventArgs e)
        {
            List<String> split = new List<string>();
            split.Add(cbokeys.Text);
            String chuoi1 = txtField1.Text;
            String[] splits = chuoi1.Split(new Char[] { ';' });
            List<String> splitTemp = splits.ToList();
            foreach (String strColumn in splitTemp)
            {
                if (split.Contains(strColumn) == false)
                {
                    split.Add(strColumn);
                }
            }

            SQLConnect.ChangeServer(txtServerFrom.Text, cbxDatabaseFrom.SelectedItem.ToString(), txtUsernameFrom.Text, txtPasswordFrom.Text);
            DataSet ds1 = new DataSet();
            ds1 = CompareData.GetDataFromTableName(cbotable1.Text, split);
            SQLConnect.ChangeServer(txtServerTo.Text, cbxDatabaseTo.SelectedItem.ToString(), txtUsernameTo.Text, txtPasswordTo.Text);
            DataSet ds2 = new DataSet();
            ds2 = CompareData.GetDataFromTableName(cbotable2.Text, split);
            if (ds1 == null || ds2 == null)
                return;

            gridViewTables.Columns.Clear();
            DataTable dt1 = SetDataTable(ds1, split);
            DataTable dt2 = SetDataTable(ds2, split);
            DataTable dtmain = SoSanhDuLieu(dt1, dt2, split, cbokeys.Text);
            if (dtmain == null) return;
            gridControlTables.DataSource = dtmain;
        }
        private void cbxDatabaseFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            SQLConnect.ChangeServer(txtServerFrom.Text, cbxDatabaseFrom.SelectedItem.ToString(), txtUsernameFrom.Text, txtPasswordFrom.Text);
            DataSet ds = CompareDatabaseCtrl.GetDanhSachTables();
            if (ds == null) return;
            cbotable2.Properties.Items.Clear();
            foreach (DataRow dr in ds.Tables[0].Rows)
                cbotable1.Properties.Items.Add(dr[1].ToString());

        }
        private void cbxDatabaseTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            SQLConnect.ChangeServer(txtServerTo.Text, cbxDatabaseTo.SelectedItem.ToString(), txtUsernameTo.Text, txtPasswordTo.Text);
            DataSet ds = CompareDatabaseCtrl.GetDanhSachTables();
            if (ds == null) return;
            cbotable2.Properties.Items.Clear();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                cbotable2.Properties.Items.Add(dr[1].ToString());
            }
        }
        private void btnaddfield_Click(object sender, EventArgs e)
        {
            SQLConnect.ChangeServer(txtServerTo.Text, cbxDatabaseTo.SelectedItem.ToString(), txtUsernameTo.Text, txtPasswordTo.Text);
            DataSet ds = CompareDatabaseCtrl.GetDanhSachColumn(cbotable1.Text);
            GridColumn gridColumn = new GridColumn();
            gridColumn.dataSet = ds;
            gridColumn.memofield = txtField1.Text;
            gridColumn.ShowDialog();
            txtField1.Text = gridColumn.str;
        }
        private void cbokeys_QueryPopUp(object sender, CancelEventArgs e)
        {

            SQLConnect.ChangeServer(txtServerTo.Text, cbxDatabaseTo.SelectedItem.ToString(), txtUsernameTo.Text, txtPasswordTo.Text);
            DataSet ds = CompareDatabaseCtrl.GetDanhSachColumn(cbotable2.Text);
            SQLConnect.ChangeServer(txtServerFrom.Text, cbxDatabaseFrom.SelectedItem.ToString(), txtUsernameFrom.Text, txtPasswordFrom.Text);
            DataSet ds2 = CompareDatabaseCtrl.GetDanhSachColumn(cbotable1.Text);
            if (ds == null || ds2 == null) return;
            if (ds.Tables[0].Rows.Count != ds2.Tables[0].Rows.Count)
            {
                MessageBox.Show("Hai Table không giống nhau");
                return;
            }
            cbokeys.Properties.Items.Clear();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                cbokeys.Properties.Items.Add(dr[0].ToString());
            }

        }
       
        #endregion
    }
}
