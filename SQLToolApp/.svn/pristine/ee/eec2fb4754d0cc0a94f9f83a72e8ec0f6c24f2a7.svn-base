using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SQLAppLib;
using SQLQQ.Util;
using System.IO;

namespace SQLQQ
{
    public partial class SetTabIndex : Form
    {
        string Flags = string.Empty;
        string strMess = "Thành công!";
        public SetTabIndex()
        {
            InitializeComponent();
        }
        private void InitTitle()
        {
            SQLApp.SetFormTitle(this);
        }

        private void SetTabIndex_Load(object sender, EventArgs e)
        {
            InitTitle();
            SetDataSourceByCombobox(cboModules, "STModules");
            SetDataSourceByCombobox(cboUserGroup, "ADUserGroups");
            if (Convert.ToInt32(cboUserGroup.SelectedValue) != 0)
            {
                SetDataSourceByCombobox(cboScreens, "STScreens", string.Format("STModuleID = '{0}' AND STUserGroupID = '{1}'", GetDataByCombobox(cboModules), GetDataByCombobox(cboUserGroup)), "STScreenText");
                SetDataSourceByCombobox(cboGroup, "STFields", string.Format("STScreenID = '{0}' AND STFieldType IN('GMCGroupControl','GMCPanelControl','GMCTabPageControl')", GetDataByCombobox(cboScreens)), "STFieldText");
            }
            progressBar1.Location = new Point((this.Width / 2) - (progressBar1.Width / 2), (this.Height / 2) - (progressBar1.Height / 2));
        }
        private object GetDataByCombobox(ComboBox cbo, bool isWorker = false)
        {
            object iObjectID = 0;
            if (!isWorker)
            {
                if (cbo.SelectedValue == null) return iObjectID;
                if (cbo.SelectedValue.GetType() == typeof(DataRowView)) return iObjectID;
                iObjectID = cbo.SelectedValue;
            }
            else
            {
                cbo.Invoke((Action)(() =>
                {
                    if (cbo.SelectedValue == null) iObjectID = 0;
                    if (cbo.SelectedValue.GetType() == typeof(DataRowView)) iObjectID = 0;
                    iObjectID = cbo.SelectedValue;
                }));
            }
            return iObjectID;
        }
        private void SetDataSourceByCombobox(ComboBox cboDataSource, string strTableName, string strWhere = "", string strDisplayName = "", string strValueName = "")
        {
            if (string.IsNullOrEmpty(strDisplayName)) strDisplayName = SQLApp.GetTableNamePrefix(strTableName) + "Name";
            if (string.IsNullOrEmpty(strValueName)) strValueName = SQLApp.GetTableNamePrefix(strTableName) + "ID";
            cboDataSource.Text = string.Empty;
            DataTable dt = SQLDBUtil.GetDataByTable(strTableName, strWhere);
            cboDataSource.DataSource = dt;
            cboDataSource.DisplayMember = strDisplayName;
            cboDataSource.ValueMember = strValueName;
        }
        private void SetDataSourceByGridView(DataGridView dgv, string strTableName, string strWhere = "")
        {
            DataTable dt = SQLDBUtil.GetDataByTable(strTableName, strWhere);
            dgv.DataSource = dt;
        }
        private void cboControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cbo = (ComboBox)sender;
            if (cbo == null) return;
            if (cbo.Name.Contains("Module") || cbo.Name.Contains("UserGroup"))
            {
                SetDataSourceByCombobox(cboScreens, "STScreens", string.Format("STModuleID = '{0}' AND STUserGroupID = '{1}'", GetDataByCombobox(cboModules), GetDataByCombobox(cboUserGroup)), "STScreenText");
                if (string.IsNullOrEmpty(cbo.Text) || string.IsNullOrEmpty(cboScreens.Text))
                {
                    ResetDataSourceControl(cboScreens);
                    ResetDataSourceControl(cboGroup);
                    ResetDataSourceControl(dgvData);
                }
            }
            else if (cbo.Name.Contains("Screen"))
            {
                SetDataSourceByCombobox(cboGroup, "STFields", string.Format("STScreenID = '{0}' AND STFieldType IN('GMCGroupControl','GMCPanelControl','GMCTabPageControl')", GetDataByCombobox(cboScreens)), "STFieldText");
                SetDataSourceByGridView(dgvData, "STFields", string.Format("STScreenID = '{0}'  AND STFieldParentID <> 0", GetDataByCombobox(cboScreens)));
            }
            else
                SetDataSourceByGridView(dgvData, "STFields", string.Format("STScreenID = '{0}' AND STFieldParentID = {1}", GetDataByCombobox(cboScreens), GetDataByCombobox(cboGroup)));
        }
        private void cboControl_Enter(object sender, EventArgs e)
        {

        }
        private void ResetDataSourceControl(Control ctrl)
        {
            ctrl.ResetText();
            if (ctrl.GetType() == typeof(ComboBox))
                ((ComboBox)ctrl).DataSource = null;
            else if (ctrl.GetType() == typeof(DataGridView))
                ((DataGridView)ctrl).DataSource = null;
            ctrl.Refresh();
        }
        private void btnSetTabIndex_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            Flags = btn.Name;
            if (_worker.IsBusy)
                _worker.CancelAsync();
            else
            {
                _worker.RunWorkerAsync();
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
        private void GetTreeNode(TreeFieldNode tfn, int scrennid, string aligntype)
        {
            DataTable dt = GetChildControl(tfn.STField.STFieldID, scrennid, aligntype);
            if (dt != null)
            {
                foreach (DataRow row in dt.Select())
                {
                    STFieldsInfo objField = new STFieldsInfo();
                    objField = (STFieldsInfo)SQLApp.GetObjectFromDataRow(row, objField);
                    TreeFieldNode child = new TreeFieldNode();
                    child.STField = objField;
                    GetTreeNode(child, scrennid, aligntype);
                    tfn.ListTreeFieldNodeChild.Add(child);
                }
            }
        }
        private DataTable GetChildControl(int stfieldparentID, int screenid, string aligntype)
        {
            string strQuery = "";
            if (aligntype == "TopLeft")
                strQuery = string.Format(@"SELECT * FROM STFields WHERE STFieldParentID = {0} AND STScreenID = {1} ORDER BY STFieldLocationY, STFieldLocationX", stfieldparentID, screenid);
            else if (aligntype == "LeftTop")
                strQuery = string.Format(@"SELECT * FROM STFields WHERE STFieldParentID = {0} AND STScreenID = {1} ORDER BY STFieldLocationX, STFieldLocationY", stfieldparentID, screenid);
            return SQLDBUtil.GetDataTable(strQuery);
        }
        private void SetTabIndexForOnlyOneScreen(TreeFieldNode tree, int screenid, string[] ListFilter)
        {
            tree.STField.STFieldID = 0;
            GetTreeNode(tree, screenid, "TopLeft");
            List<STFieldsInfo> ListField = new List<STFieldsInfo>();
            ReadChild(ref ListField, tree, ListFilter);
            int i = 1;
            foreach (STFieldsInfo a in ListField)
            {
                a.STFieldTabIndex = i++;
                UpdateTabIndex(a.STFieldID, a.STFieldTabIndex);
            }
            SetDataSourceByDataGridView(dgvData, ListField);
        }
        private void UpdateTabIndex(int STFieldID, int index)
        {
            string strQuery = String.Format(@"UPDATE STFields SET STFieldTabIndex = {0} WHERE STFieldID = {1}", index, STFieldID);
            SQLDBUtil.ExecuteNonQuery(strQuery);
        }
        private string GetTextByCombobox(ComboBox cbo)
        {
            string strText = string.Empty;
            cbo.Invoke((Action)(() =>
            {
                strText = cbo.Text;
            }));
            return strText;
        }
        private void SetDataSourceByDataGridView(DataGridView dgv, object objData)
        {
            dgv.Invoke((Action)(() => dgv.DataSource = objData));
        }
        private void SetTabIndexDefault()
        {
            object iModuleID = GetDataByCombobox(cboModules, true);
            DataTable dt = SQLDBUtil.GetDataByTable("STScreens", string.Format("STModuleID = '{0}'", iModuleID));
            if (dt != null)
            {
                string[] arrObjectID = dt.Select().Select(x => x["STScreenID"].ToString()).ToArray();
                SQLDBUtil.ExecuteNonQuery(string.Format("UPDATE STFields SET STFieldTabIndex = 1000 WHERE STScreenID IN('{0}')", string.Join("','", arrObjectID)));
            }
        }
        private void SetTabIndexUnDefault()
        {
            string[] ListFilter = ReadStringFilter();
            TreeFieldNode tree = new TreeFieldNode();
            tree.STField = new STFieldsInfo();
            int iScreenID = Convert.ToInt32(GetDataByCombobox(cboScreens, true));
            if (!string.IsNullOrEmpty(GetTextByCombobox(cboGroup)))
            {
                string AlignType = "TopLeft";
                cboAlignType.Invoke((Action)(() =>
                {
                    AlignType = Convert.ToString(cboAlignType.SelectedItem);
                }));
                if (!string.IsNullOrEmpty(AlignType))
                {
                    tree.STField.STFieldID = Convert.ToInt32(GetDataByCombobox(cboGroup, true));
                    GetTreeNode(tree, iScreenID, AlignType);
                    List<STFieldsInfo> ListField = new List<STFieldsInfo>();
                    ReadChild(ref ListField, tree, ListFilter);
                    if (ListField == null)
                        return;
                    int indexmin = ListField.Min(x => x.STFieldTabIndex);

                    foreach (STFieldsInfo a in ListField)
                    {
                        a.STFieldTabIndex = indexmin++;
                        UpdateTabIndex(a.STFieldID, a.STFieldTabIndex);
                    }
                    SetDataSourceByDataGridView(dgvData, ListField);
                }
                else
                    strMess = "Vui lòng chọn Align Type";
            }
            else if (!string.IsNullOrEmpty(GetTextByCombobox(cboScreens)))
            {
                SetTabIndexForOnlyOneScreen(tree, iScreenID, ListFilter);
            }
            else if (!string.IsNullOrEmpty(GetTextByCombobox(cboModules)))
            {
                int STModuleID = Convert.ToInt32(GetDataByCombobox(cboModules, true));
                DataTable dt = SQLDBUtil.GetDataByTable("STScreens", string.Format("STModuleID = '{0}'", STModuleID));
                if (dt == null)
                    return;
                foreach (DataRow r in dt.Select())
                {
                    int idx = Convert.ToInt32(r["STScreenID"]);
                    TreeFieldNode tfn = new TreeFieldNode();
                    tfn.STField = new STFieldsInfo();
                    SetTabIndexForOnlyOneScreen(tfn, idx, ListFilter);
                }
            }
        }
        private void Action_DoWork(object sender, DoWorkEventArgs e)
        {
            progressBar1.Invoke((Action)(() => progressBar1.Visible = true));
            btnTabIndex.Invoke((Action)(() => btnTabIndex.Enabled = false));
            btnDefault.Invoke((Action)(() => btnDefault.Enabled = false));
            if (Flags.Contains("Default"))
            {
                SetTabIndexDefault();
            }
            else
            {
                SetTabIndexUnDefault();
            }
        }

        private void _worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            SQLNotifycationAler.Show(strMess);
            progressBar1.Visible = false;
            btnTabIndex.Enabled = true;
            btnDefault.Enabled = true;
        }
    }
}
