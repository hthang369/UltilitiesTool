using SQLAppLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SQLQQ
{
    public partial class frmUnPost : Form
    {
        public frmUnPost()
        {
            InitializeComponent();
            SQLApp.SetFormTitle(this);
        }

        private void cboTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtQuery.Text = string.Format("SELECT * FROM {0}", cboTable.SelectedValue);
        }

        private void frmUnPost_Load(object sender, EventArgs e)
        {
            //dgvResult.Scroll += GridView_Scroll;
            DataTable dt = SQLDBUtil.GetDataTable("SELECT t.name,rows from sys.tables t join sys.columns c on t.object_id = c.object_id join sysindexes on t.object_id = id where indid < 2 AND rows > 0 and c.name like 'AAPostStatus' order by t.name");
            cboTable.DataSource = dt;
            cboTable.DisplayMember = "name";
            cboTable.ValueMember = "name";
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            dataGridViewUtil1.LoadData(txtQuery.Text);
        }

        private void btnUnPost_Click(object sender, EventArgs e)
        {
            BindingSource bds = (BindingSource)dataGridViewUtil1.dgvResult.DataSource;
            DataTable dt = (DataTable)bds.DataSource;
            //int count = dataGridViewUtil1.dgvResult.Rows.Cast<DataGridViewRow>();
            //DataColumn dc = dt.PrimaryKey.FirstOrDefault();
            DataColumn[] dc1 = dt.Columns.Cast<DataColumn>().Where(x => x.ColumnName.EndsWith("ID") && !x.ColumnName.StartsWith("FK")).OrderBy(x => x.ColumnName).ToArray();
            DataColumn dc = dc1.FirstOrDefault();
            string strColumnName = dc.ColumnName.Replace("ID", "No");
            List<string> lstDataNo = dataGridViewUtil1.dgvResult.Rows.Cast<DataGridViewRow>().Select(x => Convert.ToString(x.Cells[strColumnName].Value)).ToList();
            string strQuery = string.Format("Delete From GLJournals Where GLJournalDocumentNo IN('{0}')", string.Join("','", lstDataNo.ToArray()));
            SQLDBUtil.ExecuteNonQuery(strQuery);
            SQLNotifycationAler.Show("Thành công!");
        }

        private void cboTable_Enter(object sender, EventArgs e)
        {
            txtQuery.Text = string.Format("SELECT * FROM {0}", cboTable.SelectedValue);
        }
    }
}
