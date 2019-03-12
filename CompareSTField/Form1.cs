using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.Repository;
using ConnectDatabase;

namespace CompareSTFields
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        List<CSTFields> lstSTFields1 = new List<CSTFields>();
        List<CSTFields> lstSTFields2 = new List<CSTFields>();
        List<CSTFields> lstSTFieldsKQ = new List<CSTFields>();

        CompareDatabasesController CompareCtrl = new CompareDatabasesController();
        DataSet ds;

        private void Form1_Load(object sender, EventArgs e)
        {

            //server 1
            txtServer1.Text = "10.6.8.252,2000";
            txtUsername1.Text = "sa";
            txtPassword1.Text = "abc123";

            // server 2
            txtServer2.Text = "10.6.8.252,2000";
            txtUsername2.Text = "sa";
            txtPassword2.Text = "abc123";

            show_dbName1();
            show_dbName_2();

            cotGrid1();
            cotGrid2();
            cotGridKQ();

            gvctrlCompare.DataSource = lstSTFieldsKQ;


        }

        #region Tạo cột cho GridControl
        private void cotGrid1()
        {
            gvctrl1.DataSource = lstSTFields1;
        }

        private void cotGrid2()
        {
            gvctrl2.DataSource = lstSTFields2;
        }

        private void cotGridKQ()
        {
            gvctrlCompare.DataSource = lstSTFieldsKQ;
        }

        #endregion

        #region Show Combobox ket noi server

        private void show_dbName1()
        {
            LoadDatabase.LoadDatabaseCombobox(ref cboDatabase1, txtServer1.Text, txtUsername1.Text, txtPassword1.Text);
        }

        private void show_dbName_2()
        {
            LoadDatabase.LoadDatabaseCombobox(ref cboDatabase2, txtServer2.Text, txtUsername2.Text, txtPassword2.Text);
        }


        private void show_STModuleName()
        {
            //  connectdb();
            cboModule1.DataSource = new DataTable();
            String strcom = @"select * from STModules";
            CompareDatabasesController CompareCtrl = new CompareDatabasesController();
            DataSet ds = CompareCtrl.GetDataSet(strcom);
            if (ds != null)
            {
                cboModule1.DataSource = ds.Tables[0];
                cboModule1.DisplayMember = "STModuleName";
                cboModule1.ValueMember = "STModuleID";
            }
            else
            {
                cboModule1.Text = "Không có dữ liệu";
            }
        }



        private void show_ADUserGroupName()
        {
            cboGroup1.DataSource = new DataTable();
            string strcom = "select * from ADUserGroups";
            CompareDatabasesController CompareCtrl = new CompareDatabasesController();
            DataSet ds = CompareCtrl.GetDataSet(strcom);
            if (ds != null)
            {
                cboGroup1.DataSource = ds.Tables[0];
                cboGroup1.DisplayMember = "ADUserGroupName";
                cboGroup1.ValueMember = "ADUserGroupID";
            }
            else
            {
                cboGroup1.Text = "Không có dữ liệu";
                return;
            }
        }

        private void show_STModuleName2()
        {
            //  connectdb();
            cboModule2.DataSource = new DataTable();
            String strcom = @"select * from STModules";
            CompareDatabasesController CompareCtrl = new CompareDatabasesController();
            DataSet ds = CompareCtrl.GetDataSet(strcom);
            if (ds != null)
            {
                cboModule2.DataSource = ds.Tables[0];
                cboModule2.DisplayMember = "STModuleName";
                cboModule2.ValueMember = "STModuleID";
            }
            else
            {
                cboModule2.Text = "Không có dữ liệu";
            }
        }



        private void show_ADUserGroupName2()
        {
            cboGroup2.DataSource = new DataTable();
            string strcom = "select * from ADUserGroups";
            CompareDatabasesController CompareCtrl = new CompareDatabasesController();
            DataSet ds = CompareCtrl.GetDataSet(strcom);
            if (ds != null)
            {
                cboGroup2.DataSource = ds.Tables[0];
                cboGroup2.DisplayMember = "ADUserGroupName";
                cboGroup2.ValueMember = "ADUserGroupID";
            }
            else
            {
                cboGroup2.Text = "Không có dữ liệu";
                return;
            }
        }


        private void Show_GridView()
        {
            //  if ( cboModule1 
        }

   

        #endregion

        private void connectdb()
        {
            bool conn = SQLConnect.ChangeServer(txtServer1.Text, cboDatabase1.SelectedItem.ToString(), txtUsername1.Text, txtPassword1.Text);
        }


        private void cboDatabase1_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool conn = SQLConnect.ChangeServer(txtServer1.Text, cboDatabase1.SelectedItem.ToString(), txtUsername1.Text, txtPassword1.Text);
            if (conn == true)
            {
                MessageBox.Show("Bạn đã kết nối Database thành công");
                show_STModuleName();
                show_ADUserGroupName();
            }
            else
                MessageBox.Show("Kết nối thất bại");            
        }

        private void cboGroup2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnHienThi1_Click(object sender, EventArgs e)
        {
            if (cboModule1.Text != "" && cboGroup1.Text != "")
            {
                // gan list bang datasource , khai bao value stfield... list voi grid 1-1... gan list do bang voi list khac thi list nay se mat... neu mun list xoa.. thi list.clear()... list = new la mat het du lieu
          
                DataSet ds = CompareCtrl.DetailField_IsExitSource(cboModule1.Text, cboGroup1.SelectedValue.ToString());
                lstSTFields1.Clear();
                if (ds == null || ds.Tables.Count<=0)
                    return;
                else
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        CSTFields cSTfields = new CSTFields();
                        cSTfields.STFieldID = Convert.ToInt32(row["STFieldID"]);
                        cSTfields.STFieldName = Convert.ToString(row["STFieldName"]);
                        lstSTFields1.Add(cSTfields);
                    }
                }
                gvctrl1.RefreshDataSource();
            }
        }

        private void cboDatabase2_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool conn = SQLConnect.ChangeServer(txtServer2.Text, cboDatabase2.SelectedItem.ToString(), txtUsername2.Text, txtPassword2.Text);
            if (conn == true)
            {
                MessageBox.Show("Bạn đã kết nối Database thành công");
                show_STModuleName2();
                show_ADUserGroupName2();
            }
            else
                MessageBox.Show("Kết nối thất bại");
        }

        private void btnHienThi2_Click(object sender, EventArgs e)
        {
            if (cboModule2.Text != "" && cboGroup2.Text != "")
            {
                // gan list bang datasource , khai bao value stfield... list voi grid 1-1... gan list do bang voi list khac thi list nay se mat... neu mun list xoa.. thi list.clear()... list = new la mat het du lieu
                DataSet ds = CompareCtrl.DetailField_IsExitSource(cboModule2.Text, cboGroup2.SelectedValue.ToString());
                lstSTFields2.Clear();
                if (ds == null || ds.Tables.Count <= 0)
                    return;
                else
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        CSTFields cSTfields = new CSTFields();
                        cSTfields.STFieldID = Convert.ToInt32(row["STFieldID"]);
                        cSTfields.STFieldName = Convert.ToString(row["STFieldName"]);
                        lstSTFields2.Add(cSTfields);
                    }

                    //  gv1.RefreshDataSource();
                }
              //  gvctrl2.DataSource = new DataTable();
                gvctrl2.RefreshDataSource();
            }
        }

        private void cboModule2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnCompare_Click(object sender, EventArgs e)
        {
            CSTFields cfieldKQ = new CSTFields();

            // dữ liệu grid1
            if (cboModule1.Text != "" && cboGroup1.Text != "")
            {
                String strcom = string.Format(@"select STFieldID,STFieldName,'FALSE' as STFieldKhac  from STFields where STScreenID in (select STScreenID from STScreens where STModuleID in (select STModuleID from STModules where STModuleName='{0}') and STUserGroupID = ( select ADUserGroupID from ADUserGroups where ADUserGroupID = {1}))", cboModule1.Text, cboGroup1.SelectedValue.ToString());
                //MessageBox.Show(strcom);
                // gan list bang datasource , khai bao value stfield... list voi grid 1-1... gan list do bang voi list khac thi list nay se mat... neu mun list xoa.. thi list.clear()... list = new la mat het du lieu
                ds = CompareCtrl.GetDataSet(strcom);
                lstSTFields1.Clear();
                if (ds == null || ds.Tables.Count <= 0)
                    return;
                else
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        CSTFields cSTfields = new CSTFields();
                        cSTfields.STFieldID = Convert.ToInt32(row["STFieldID"]);
                        cSTfields.STFieldName = Convert.ToString(row["STFieldName"]);
                        lstSTFields1.Add(cSTfields);
                    }

                    //  gv1.RefreshDataSource();
                }
                // end 

                // du lieu grid 2
                  if (cboModule2.Text != "" && cboGroup2.Text != "")
            {
                String strcom1 = string.Format(@"select STFieldID,STFieldName,'FALSE' as STFieldKhac  from STFields where STScreenID in (select STScreenID from STScreens where STModuleID in (select STModuleID from STModules where STModuleName='{0}') and STUserGroupID = ( select ADUserGroupID from ADUserGroups where ADUserGroupID = {1}))", cboModule2.Text, cboGroup2.SelectedValue.ToString());
                //MessageBox.Show(strcom);
                // gan list bang datasource , khai bao value stfield... list voi grid 1-1... gan list do bang voi list khac thi list nay se mat... neu mun list xoa.. thi list.clear()... list = new la mat het du lieu
                ds = CompareCtrl.GetDataSet(strcom1);
                lstSTFields2.Clear();
                if (ds == null || ds.Tables.Count <= 0)
                    return;
                else
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        CSTFields cSTfields = new CSTFields();
                        cSTfields.STFieldID = Convert.ToInt32(row["STFieldID"]);
                        cSTfields.STFieldName = Convert.ToString(row["STFieldName"]);
                        lstSTFields2.Add(cSTfields);
                    }

                    //  gv1.RefreshDataSource();
                }
            }
                // end 
            }
            bool flag = true;
            // lay những fields list1 khac list2
            foreach (CSTFields cSTField1 in lstSTFields1)
            {
                foreach (CSTFields cSTField2 in lstSTFields2)
                {
                    if (cSTField1.STFieldName.ToString() == cSTField2.STFieldName.ToString())
                    {
                        flag = false;
                    } 
                }
                if (flag == true)
                {
                    cSTField1.IsDestination= true;
                    lstSTFieldsKQ.Add(cSTField1);
                }
                else
                    flag = true;
                    
            }
            // end lay những fields list1 khac list2

            // lay những fields list2 khac list1
            foreach (CSTFields cSTField2 in lstSTFields2)
            {
                foreach (CSTFields cSTField1 in lstSTFields1)
                {
                    if (cSTField2.STFieldName.ToString() == cSTField1.STFieldName.ToString())
                    {
                        flag = false;
                    }
                }
                if (flag == true)
                {
                    cSTField2.IsExitSource = true;
                    lstSTFieldsKQ.Add(cSTField2);
                }
                else
                    flag = true;

            }
            // end lay những fields list2 khac list1


            gvctrlCompare.RefreshDataSource();    
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {


            DataTable dtLocation1 = new DataTable();
            DataSet ds1 =  CompareCtrl.DetailField_IsExitSource(cboModule1.Text, cboGroup1.SelectedValue.ToString()); // lay ra cac fields
            dtLocation1 = ds1.Tables[0];
            DateTime a = DateTime.Now;
           // DataRow row= new DataRow();
            DataColumn col = dtLocation1.Columns["STfieldLocationX"];
         //   row = dtLocation1.Rows["STfieldLocationX"];

//if (dtLocation1.Rows.Count > 0)
           //   MessageBox.Show ()

            
        }

    }
}