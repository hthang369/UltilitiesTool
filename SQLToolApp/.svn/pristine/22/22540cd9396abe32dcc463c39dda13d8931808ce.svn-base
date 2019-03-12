using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using SQLAppLib;

namespace SQLQQ
{
    public partial class frmQuery_SQL : Form
    {
        public frmQuery_SQL()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void frmQuery_SQL_Load(object sender, EventArgs e)
        {
            this.KeyDown += new KeyEventHandler(frmQuery_SQL_KeyDown);
        }

        #region Event

        void frmQuery_SQL_KeyDown(object sender, KeyEventArgs e)
        {
            frmData _frmData = new frmData();
            frmMain _frmMain = new frmMain();

            #region Key F5 - Refresh data in DataGridView

            if (e.KeyCode == Keys.F5)
            {
                if (txtQuery.Text != "")
                {
                    DataTable dtRecentData = SQLDBUtil.GetDataTable(txtQuery.Text);
                    if (dtRecentData != null && dtRecentData.Rows.Count > 0)
                    {
                        txtError.Text = "Success!";
                        _frmData._strQuery_RecentData = txtQuery.Text;
                        _frmData._dgrSearch.DataSource = dtRecentData;
                        _frmData.SetColorForDataGridView(_frmData._dgrSearch);
                        _frmData._dgrSearch.Refresh();
                        _frmData.Show();
                        this.Hide();
                    }
                    else
                    {
                        txtError.Text = "Error! " +_frmMain._strQuery_Error;
                        return;
                    }
                }
            }

            #endregion

            #region Key Alt + M/N - Size from - Min/Max

            if (e.Alt && (e.KeyCode == Keys.M))
            {
                this.Size = new System.Drawing.Size(500, 500);
            }
            
            if (e.Alt && (e.KeyCode == Keys.N))
            {
                this.Size = new System.Drawing.Size(300, 300);
            }

            #endregion


            #region Ctr + V Past Current data on GridData

            if (e.Control && e.KeyCode == Keys.V)
            {
                txtQuery.Text = txtQuery.Text + _frmData._strQuery_RecentData;
            }

            #endregion

            #region Key Esc

            if (e.KeyCode == (Keys.Back | Keys.RButton | Keys.LButton | Keys.ShiftKey))
            {
                this.Hide();
            }

            #endregion

        }

        #endregion
    }
}
