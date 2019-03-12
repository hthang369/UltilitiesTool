using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace SQLQQ
{
    public partial class frmSyncDB : Form
    {
        string strServerFrom;
        string strServerTo;
        string strDatabaseFrom;
        string strDatabaseTo;
        int indexFrom;
        int indexTo;
        public frmSyncDB()
        {
            InitializeComponent();
        }

        private void frmSyncDB_Load(object sender, EventArgs e)
        {
            Util.FunctionList.LoadConfigIniFromServer(cboServerFrom);
            Util.FunctionList.LoadConfigIniFromServer(cboServerTo);
            progressBar1.Visible = false;
            lblProcess.Visible = false;
        }

        private void cboServerFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            Util.FunctionList.LoadDatabaseByServer(cboServerFrom, cboDatabaseFrom);
        }

        private void cboServerTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Util.FunctionList.LoadDatabaseByServer(cboServerTo, cboDatabaseTo);
        }

        private void btnCompare_Click(object sender, EventArgs e)
        {
            //progressBar1.PerformStep();
            if (_worker.IsBusy)
                _worker.CancelAsync();
            else
            {
                indexFrom = cboServerFrom.SelectedIndex;
                indexTo = cboServerTo.SelectedIndex;
                strServerFrom = Util.FunctionList.GetServerConfig(cboServerFrom.SelectedIndex);
                strServerTo = Util.FunctionList.GetServerConfig(cboServerTo.SelectedIndex);
                strDatabaseFrom = Convert.ToString(cboDatabaseFrom.SelectedItem);
                strDatabaseTo = Convert.ToString(cboDatabaseTo.SelectedItem);
                progressBar1.Value = progressBar1.Minimum;
                //Thread newThread = new Thread(ActionCompare);
                _worker.RunWorkerAsync();
                //newThread.Start();
            }
        }

        private void SyncTable(DataGridView dgvTableView, DataGridView dgvColView, int idxFrom, int idxTo, string strDBFrom, string strDBTo)
        {
            DataSet dsSource = Util.FunctionList.SynchronizeTable(idxFrom, idxTo, strDBFrom, strDBTo);
            if (dsSource != null && dsSource.Tables.Count > 0)
            {
                //float length = dtFrom.Rows.Count;
                //progressBar1.Invoke((Action)(() => progressBar1.Maximum = (int)length));
                SetDataSource(dgvTableView, dsSource.Tables[0]);
                SetDataSource(dgvColView, dsSource.Tables[1]);
                //_worker.ReportProgress(100);
            }
        }
        private void SetDataSource(DataGridView dgvView, DataTable dt)
        {
            dgvView.Invoke((Action)(() =>
            {
                dgvView.DataSource = dt;
                dgvView.Refresh();
            }));
        }
        private void btnSync_Click(object sender, EventArgs e)
        {
            SyncDB(dgvTableFrom, "Copy");
            SyncDB(dgvTableTo, "Copy");
            SyncDB(dgvColFrom, "Copy");
            SyncDB(dgvColTo, "Copy");

            SyncDB(dgvTableFrom, "Del");
            SyncDB(dgvTableTo, "Del");
            SyncDB(dgvColFrom, "Del");
            SyncDB(dgvColTo, "Del");
        }
        public void SyncDB(DataGridView dgvResult, string type)
        {
            List<DataGridViewRow> lstResults = dgvResult.Rows.Cast<DataGridViewRow>().Where(x => x.Cells[0].Value.Equals(type)).ToList();
            foreach (DataGridViewRow itemResult in lstResults)
            {
                string strQuery = itemResult.Cells["Script Add"].Value.ToString();
                SQLAppLib.SQLDBUtil.ExecuteNonQuery(strQuery);
            }
        }
        private void ActionCompare()
        {
            progressBar1.Invoke((Action)(() => progressBar1.Visible = true));
            //lblProcess.Invoke((Action)(() => lblProcess.Visible = true));
            btnCompare.Invoke((Action)(() => btnCompare.Enabled = false));
            btnSync.Invoke((Action)(() => btnSync.Enabled = false));
            cboServerFrom.Invoke((Action)(() => cboServerFrom.Enabled = false));
            cboServerTo.Invoke((Action)(() => cboServerTo.Enabled = false));
            cboDatabaseFrom.Invoke((Action)(() => cboDatabaseFrom.Enabled = false));
            cboDatabaseTo.Invoke((Action)(() => cboDatabaseTo.Enabled = false));
            if (strServerFrom == strServerTo)
            {
                SyncTable(dgvTableFrom, dgvColFrom, indexFrom, indexTo, strDatabaseFrom, strDatabaseTo);
                SyncTable(dgvTableTo, dgvColTo, indexTo, indexFrom, strDatabaseTo, strDatabaseFrom);
            }
            progressBar1.Invoke((Action)(() => progressBar1.Visible = false));
        }
        private void Action_DoWork(object sender, DoWorkEventArgs e)
        {
            ActionCompare();
        }

        private void Action_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (!_worker.CancellationPending)
            {
                lblProcess.Text = e.ProgressPercentage + "%";
                progressBar1.PerformStep();
            }
        }

        private void Action_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnCompare.Enabled = true;
            btnSync.Enabled = true;
            cboDatabaseFrom.Enabled = true;
            cboDatabaseTo.Enabled = true;
            cboServerFrom.Enabled = true;
            cboServerTo.Enabled = true;
        }

        private void dgvViewResult_KeyDown(object sender, KeyEventArgs e)
        {
            DataGridView dgvResult = (DataGridView)sender;
            if(e.KeyCode == Keys.C)
                dgvResult.CurrentRow.Cells[0].Value = "Copy";
            else if(e.KeyCode == Keys.D)
                dgvResult.CurrentRow.Cells[0].Value = "Del";
            else if(e.KeyCode == Keys.Space)
                dgvResult.CurrentRow.Cells[0].Value = "";
        }
    }
}
