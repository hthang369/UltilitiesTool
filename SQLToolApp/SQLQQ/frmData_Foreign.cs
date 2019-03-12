using SQLAppLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SQLQQ
{
    public partial class frmData_Foreign : Form
    {
        public bool IsActionUpdate = false;

        public static String strCurrentCellColumnName;
        public static String strPrimaryKey;
        public static String strTableName;
        public static String strCurrentCellData;
        public static String strQuery_RecentData;
        public static DataGridView dgvData_Current;

        public frmData_Foreign()
        {
            InitializeComponent();
        }

        public DataGridView _dgvDataForeign
        {
            get
            {
                return dgvDataForeign;
            }
            set
            {
                dgvDataForeign = value;
            }
        }

        private void frmData_Foreign_Load(object sender, EventArgs e)
        {
            this.KeyDown += new KeyEventHandler(frmData_Foreign_KeyDown);
            dgvDataForeign.KeyDown += new KeyEventHandler(dgvDataForeign_KeyDown);
        }

        #region Event

        void frmData_Foreign_KeyDown(object sender, KeyEventArgs e)
        {
            frmSearch _frmSearch = new frmSearch();
            frmColumn _frmColumn = new frmColumn();
            frmMain _frmMain = new frmMain();

            #region Esc - Thoát

            if (e.KeyCode == (Keys.Back | Keys.RButton | Keys.LButton | Keys.ShiftKey))
            {
                this.Close();
            }

            #endregion

            #region F2 - Update

            if (e.KeyCode == Keys.F1)
            {
                dgvDataForeign.ReadOnly = true;
                IsActionUpdate = false;
                sttAction.Text = "Not Action";
            }

            if (e.KeyCode == Keys.F2)
            {
                dgvDataForeign.ReadOnly = false;
                IsActionUpdate = true;
                sttAction.Text = "Action : Update";
            }

            #endregion

            #region Key F5 - Refresh data in DataGridView

            if (e.KeyCode == Keys.F5)
            {
                if (strQuery_RecentData != "")
                {
                    DataTable dtRecentData = SQLDBUtil.GetDataTable(strQuery_RecentData);
                    if (dtRecentData != null && dtRecentData.Rows.Count > 0)
                    {
                        try
                        {
                            int iCurentIndex_Row = dgvDataForeign.CurrentRow.Index;
                            int iCurentIndex_Column = dgvDataForeign.CurrentCell.ColumnIndex;

                            dgvDataForeign.DataSource = null;
                            dgvDataForeign.DataSource = dtRecentData;
                            dgvDataForeign.CurrentCell = dgvDataForeign[iCurentIndex_Column, iCurentIndex_Row];

                            //SetColorForDataGridView(dgvDataForeign);
                            dgvDataForeign.Refresh();
                        }
                        catch
                        { }
                    }
                }
            }

            #endregion

            #region Ctr + M Show Main - Connect Sever

            if (e.Control && e.KeyCode == Keys.M)
            {
                _frmMain.Show();
            }

            #endregion
        }

        void dgvDataForeign_KeyDown(object sender, KeyEventArgs e)
        {
            #region Key Alt + M/N - Size from - Min/Max

            if (e.Alt && (e.KeyCode == Keys.M))
            {
                this.Size = new System.Drawing.Size(1000, 500);
            }

            if (e.Alt && (e.KeyCode == Keys.N))
            {
                this.Size = new System.Drawing.Size(700, 500);
            }

            #endregion

            #region Key Alt + F - Focus to cell want to find in DataGridView

            if (e.Alt && e.KeyCode == Keys.F)
            {
                frmColumn _frmColumn = new frmColumn();

                if (strCurrentCellData != "")
                {
                    for (int i = 0; i < dgvDataForeign.Columns.Count - 1; i++)
                    {
                        String strColumnName = dgvDataForeign.Columns[i].Name.ToString();
                        _frmColumn._cbbColumn.Items.Add(strColumnName);
                    }

                    _frmColumn._cbbColumn.DisplayMember = strPrimaryKey;
                    _frmColumn.ShowDialog();
                }

                if (_frmColumn._iIndexColumn > 0)
                {
                    dgvDataForeign.CurrentCell = dgvDataForeign[_frmColumn._iIndexColumn, dgvDataForeign.CurrentRow.Index];  // Đưa Control về vị trí của nó
                    //dgvDataForeign.Rows[0].Cells[_frmColumn._iIndexColumn].Selected = true;  // Set trạng thái Selected
                }
            }

            #endregion
        }

        #endregion
    }
}
