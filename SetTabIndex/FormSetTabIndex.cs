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
using System.Text.RegularExpressions;

namespace SetTabIndex
{
    public partial class FormSetTabIndex : Form
    {
        public List<STFieldsInfo> ListStfield;
        public SetTabIndexController control;
        public string fileName = Application.StartupPath + "\\info.txt";
        public FormSetTabIndex()
        {
            InitializeComponent();
            ListStfield = new List<STFieldsInfo>();
            control = new SetTabIndexController();
        }

        #region Function

        public STFieldsInfo ConvertToSTFieldsInfoFromDataRow(DataRow r)
        {
            STFieldsInfo stfinfo = new STFieldsInfo();
            stfinfo.STFieldID = int.Parse(r["STFieldID"].ToString());
            stfinfo.STFieldName = r["STFieldName"].ToString();
            stfinfo.STFieldText = r["STFieldText"].ToString();
            stfinfo.STFieldType = r["STFieldType"].ToString();
            stfinfo.STFieldLocationX = int.Parse(r["STFieldLocationX"].ToString());
            stfinfo.STFieldLocationY = int.Parse(r["STFieldLocationY"].ToString());
            stfinfo.STFieldParentID = int.Parse(r["STFieldParentID"].ToString());

            return stfinfo;
        }
        public void getTreeNode(TreeFieldNode tfn, int scrennid, string aligntype)
        {
            DataSet ds = control.getChildControl(tfn.STField.STFieldID, scrennid, aligntype);
            foreach (DataRow r in ds.Tables[0].Select())
            {
                STFieldsInfo stf = ConvertToSTFieldsInfoFromDataRow(r);
                TreeFieldNode child = new TreeFieldNode();
                child.STField = stf;
                getTreeNode(child, scrennid, aligntype);
                tfn.ListTreeFieldNodeChild.Add(child);
            }
        }
        public void ReadChild(ref List<STFieldsInfo> list, TreeFieldNode node, String[] listfilter)
        {

            foreach (TreeFieldNode n in node.ListTreeFieldNodeChild)
            {
                foreach (string s in listfilter)
                {
                    if (s == n.STField.STFieldType)
                    {

                        list.Add(n.STField);
                        break;
                    }
                }
                ReadChild(ref list, n, listfilter);
            }

        }
        public String[] ReadStringFilter()
        {
            string filepath = Application.StartupPath + "\\stringfilter.txt";
            String[] ListFilter;
            //FileStream fs=new FileStream("C:\stringfilter.txt",FileMode.Open);
            ListFilter = File.ReadAllLines(filepath);
            return ListFilter;
        }

        public void setTabIndexForOnlyOneScreen(TreeFieldNode tree, int screenid, string[] ListFilter)
        {
            tree.STField.STFieldID = 0;
            getTreeNode(tree, screenid, "TopLeft");
            List<STFieldsInfo> ListField = new List<STFieldsInfo>();
            ReadChild(ref ListField, tree, ListFilter);
            int i = 1;
            foreach (STFieldsInfo a in ListField)
            {
                a.STFieldTabIndex = i++;
                control.UpdateTabIndex(a.STFieldID, a.STFieldTabIndex);
            }
            grcControl.DataSource = ListField;
        }
        #endregion

        #region Event

        private void Form1_Load(object sender, EventArgs e)
        {
            docFile(fileName);
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

        private void txtServerName_Validated(object sender, EventArgs e)
        {
            docFile(fileName);
        }

        #region Databases
        private void cbbDatabase_SelectedIndexChanged(object sender, EventArgs e)
        {
            string dbName = cbbDatabase.SelectedItem.ToString();
            if (SQLConnect.ChangeServer(txtServerName.Text, dbName, txtUserName.Text, txtPassword.Text))
                SQLConnect.GetQuery(string.Format("use {0}", dbName)).ExecuteNonQuery();
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
                for (int i = 0; i < strChuoi.Count; i++)
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
                    else if (i == strChuoi.Count - 1 && flag)
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

        #region Modules
        private void cbbModule_Enter(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(cbbDatabase.Text)) return;
            DataSet ds = control.GetModules();
            if (ds == null)
                return;
            cbbModule.DataSource = ds.Tables[0];
            cbbModule.DisplayMember = "STModuleName";
            cbbModule.ValueMember = "STModuleID";
        }
        #endregion

        #region UserGroups
        private void cbbUserGroup_Enter(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(cbbDatabase.Text)) return;
            DataSet ds = control.GetUserGroups();
            if (ds == null || ds.Tables.Count == 0)
                return;
            cbbUserGroup.DataSource = ds.Tables[0];
            cbbUserGroup.DisplayMember = "ADUserGroupName";
            cbbUserGroup.ValueMember = "ADUserGroupID";
        }
        private void cbbGroup_Enter(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(cbbDatabase.Text)) return;
            if (String.IsNullOrEmpty(cbbModule.SelectedValue.ToString())) return;
            if (String.IsNullOrEmpty(cbbUserGroup.SelectedValue.ToString())) return;
            DataSet ds = control.GetGroupControls(Convert.ToInt32(cbbScreen.SelectedValue));
            if (ds == null || ds.Tables.Count == 0)
                return;
            cbbGroup.DataSource = ds.Tables[0];
            cbbGroup.DisplayMember = "STFieldName";
            cbbGroup.ValueMember = "STFieldID";
        }
        #endregion

        #region Screens
        private void cbbScreen_Enter(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(cbbDatabase.Text)) return;
            if (cbbModule.SelectedValue == null || string.IsNullOrEmpty(cbbModule.SelectedValue.ToString())) return;
            int iUserGroupID = 0;
            if (cbbUserGroup.SelectedValue != null) iUserGroupID = Convert.ToInt32(cbbUserGroup.SelectedValue);
            DataSet ds = control.GetScreenByModuleAndUserGroup(Convert.ToInt32(cbbModule.SelectedValue), iUserGroupID);
            if (ds == null || ds.Tables.Count == 0)
                return;
            cbbScreen.DataSource = ds.Tables[0];
            cbbScreen.DisplayMember = "STScreenText";
            cbbScreen.ValueMember = "STScreenID";
        }
        private void cbbScreen_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbScreen.SelectedValue == null || string.IsNullOrEmpty(cbbScreen.SelectedValue.ToString())) return;
            try
            {
                int istscreenid = Convert.ToInt32(cbbScreen.SelectedValue);
                DataSet ds = control.GetFieldsByScreen(istscreenid);
                if (ds == null || ds.Tables.Count == 0)
                    return;
                grcControl.DataSource = ds.Tables[0];
            }
            catch
            { }
        }
        #endregion

        #region Actions
        private void btnSetTabindex_Click(object sender, EventArgs e)
        {
            string[] ListFilter = ReadStringFilter();
            TreeFieldNode tree = new TreeFieldNode();
            tree.STField = new STFieldsInfo();
            int screenid = Convert.ToInt32(cbbScreen.SelectedValue);
            if (cbbGroup.Text != "")
            {
                string AlignType = cbbAlignType.SelectedItem.ToString();
                tree.STField.STFieldID = Convert.ToInt32(cbbGroup.SelectedValue);
                getTreeNode(tree, screenid, AlignType);
                List<STFieldsInfo> ListField = new List<STFieldsInfo>();
                ReadChild(ref ListField, tree, ListFilter);
                if (ListField == null)
                    return;
                int indexmin = ListField.First().STFieldTabIndex;
                foreach (STFieldsInfo a in ListField)
                {
                    if (a.STFieldTabIndex < indexmin)
                        indexmin = a.STFieldTabIndex;
                }

                foreach (STFieldsInfo a in ListField)
                {
                    a.STFieldTabIndex = indexmin++;
                    control.UpdateTabIndex(a.STFieldID, a.STFieldTabIndex);
                }
                grcControl.DataSource = ListField;


            }
            else if (cbbScreen.Text != "")
            {
                setTabIndexForOnlyOneScreen(tree, screenid, ListFilter);
            }
            else if (cbbModule.Text != "")
            {
                int STModuleID = int.Parse(cbbModule.SelectedValue.ToString());
                List<int> ListScreenID = new List<int>();
                DataSet ds = control.getScreenOfModule(STModuleID);
                if (ds == null)
                    return;
                foreach (DataRow r in ds.Tables[0].Select())
                {
                    int i = int.Parse(r["STScreenID"].ToString());
                    ListScreenID.Add(i);
                }
                foreach (int id in ListScreenID)
                {
                    TreeFieldNode tfn = new TreeFieldNode();
                    tfn.STField = new STFieldsInfo();
                    setTabIndexForOnlyOneScreen(tfn, id, ListFilter);
                }
            }
        }
        private void btnDefault_Click(object sender, EventArgs e)
        {
            int STModuleID = int.Parse(cbbModule.SelectedValue.ToString());
            List<int> ListScreenID = new List<int>();
            DataSet ds = control.getScreenOfModule(STModuleID);
            if (ds == null)
                return;
            foreach (DataRow r in ds.Tables[0].Select())
            {
                int i = int.Parse(r["STScreenID"].ToString());
                ListScreenID.Add(i);
            }
            foreach (int id in ListScreenID)
            {

                control.setAllTabIndexToDefaut(id);
            }
        }
        #endregion
        #endregion

    }
    
}
