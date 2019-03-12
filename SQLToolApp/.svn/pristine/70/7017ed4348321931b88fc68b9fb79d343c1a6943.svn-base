using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SQLAppLib;
using System.Threading;
using System.Diagnostics;
using SQLQQ.Util;

namespace SQLQQ.FormUtil
{
    public partial class DataGridViewUtil : UserControl
    {
        public string strQueryMain;
        public string strFillter;
        private Dictionary<DataGridViewColumn, string> lstFillters;
        private int n;
        private delegate void Thread_LoadData();
        private BindingSource bds;
        string strCuunt;
        string strRowsCount;
        string strCaptionFillter;
        private bool _allowNew;
        private bool _allowEdit = false;
        public DataGridViewUtil()
        {
            InitializeComponent();
        }
        private void dgvResult_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (dgvResult.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == DBNull.Value)
            {
                e.Cancel = true;
            }
        }

        private void DataGridViewUtil_Load(object sender, EventArgs e)
        {
            //progressBar.Visible = false;
            progressBar.Style = ProgressBarStyle.Blocks;
            n = 0;
            strCuunt = lblCount.Text;
            strRowsCount = lblRow.Text;
            lblCount.Text = string.Format(strCuunt, SQLDBUtil._strServer, SQLDBUtil._strDatabase, 0);
            lblRow.Text = string.Format(strRowsCount, 0, 0);
            lstFillters = new Dictionary<DataGridViewColumn, string>();
            bds = new BindingSource();
            strCaptionFillter = "Fillter: {0}";
        }

        public void LoadData(string strQuery)
        {
            strQueryMain = strQuery;
            lstFillters.Clear();
            dgvResult.ReadOnly = !_allowEdit;
            Thread thead = new Thread(LoadDataResult);
            thead.IsBackground = true;
            thead.Start();
        }
        private void LoadDataResult(object strFillter)
        {
            RunTime();

            Thread ts = new Thread(() =>
            {
                if (strFillter != null)
                {
                    bds.Filter = Convert.ToString(strFillter);
                    dgvResult.Invoke((Action)(() => dgvResult.Refresh()));
                }
                else
                {
                    dgvResult.Invoke((Action)(() =>
                    {
                        bds.Filter = "";
                        bds.DataSource = SQLDBUtil.GetDataTable(strQueryMain);
                        dgvResult.DataSource = bds;
                    }));
                }
                //lblRow.Text = string.Format(strRowsCount, 0, dgvResult.RowCount.ToString());
                toolStrip1.Invoke((Action)(() => lblRow.Text = string.Format(strRowsCount, 0, dgvResult.RowCount.ToString())));
                toolStrip1.Invoke((Action)(() =>
                {
                    if (strFillter == null) strCaptionFillter = "{0}";
                    else strCaptionFillter = "Fillter: {0}";
                    lblFillter.Text = string.Format(strCaptionFillter, Convert.ToString(strFillter));
                }));
            });
            ts.Start();
            ts.Join();

            StopTime();
        }
        public void FillterData(string strFillter)
        {
            Thread thead = new Thread(LoadDataResult);
            thead.IsBackground = true;
            thead.Start(strFillter);
        }
        private void RunTime()
        {
            Thread tsStart = new Thread(() =>
            {
                timerCount.Start();
                //toolStrip1.Invoke((Action)(() => progressBar.Visible = true));
                toolStrip1.Invoke((Action)(() => progressBar.Style = ProgressBarStyle.Marquee));
            });
            tsStart.Start();
            tsStart.Join();
        }
        private void StopTime()
        {
            Thread tsStop = new Thread(() =>
            {
                timerCount.Stop();
                //toolStrip1.Invoke((Action)(() => progressBar.Visible = false));
                toolStrip1.Invoke((Action)(() => progressBar.Style = ProgressBarStyle.Blocks));
            });
            tsStop.Start();
            tsStop.Join();
        }
        public bool AllowNew
        {
            set { _allowNew = value; }
        }
        public bool AllowEdit
        {
            set { _allowEdit = value; }
        }
        private void dgvResult_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.RowIndex % 2 == 0)
                    e.CellStyle.BackColor = Color.AliceBlue;
                else
                    e.CellStyle.BackColor = Color.White;
            }
        }

        public virtual void dgvResult_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvResult.CurrentRow != null)
                lblRow.Text = string.Format(strRowsCount, dgvResult.CurrentRow.Index + 1, dgvResult.RowCount);
        }

        private void dgvResult_AllowUserToOrderColumnsChanged(object sender, EventArgs e)
        {

        }

        private void dgvResult_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.M)
            {
                if (e.Alt)
                    dgvResult.CurrentCell.OwningColumn.Width -= 20;
                else
                    dgvResult.CurrentCell.OwningColumn.Width += 20;
            }
            if (e.KeyCode == Keys.F)
            {
                DataGridViewColumn strColumn = dgvResult.Columns[dgvResult.CurrentCell.ColumnIndex];
                string strNewQuery = string.Empty;
                Dictionary<string, string> dicFillter = new Dictionary<string, string>();
                if (!e.Alt)
                {
                    string strValue = dgvResult.CurrentCell.Value.ToString();
                    lstFillters.AddItem(strColumn, strValue);
                    dicFillter = SQLQuery.GenerateFillter(lstFillters);
                    string strFillter = dicFillter[strColumn.Name];
                    DialogResult result = PromptForm.ShowText("Fillter", strColumn.Name, ref strFillter);
                    if (result == DialogResult.Cancel) return;
                    dicFillter[strColumn.Name] = strFillter;
                    strNewQuery = SQLQuery.GenerateFillter(dicFillter);
                }
                else
                {

                }
                FillterData(strNewQuery);
            }
            if(e.Control && e.KeyCode == Keys.M)
            {
                string strColumn = string.Empty;
                DialogResult result = PromptForm.ShowText("Find Column", "Find Column", ref strColumn);
                if (result == DialogResult.Cancel) return;
                List<DataGridViewColumn> lstColumns = dgvResult.Columns.Cast<DataGridViewColumn>().Where(x => x.Name.ToLower().Contains(strColumn)).ToList();
                if (lstColumns.Count > 0)
                {
                    int idx = lstColumns.First().Index;
                    dgvResult.CurrentCell = dgvResult[idx, dgvResult.CurrentRow.Index];
                }
                else
                    MessageBox.Show("Find not found Column!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (e.KeyCode == Keys.Escape)
            {
                FillterData("");
            }
        }
    }
}
