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
    public partial class frmColumn : Form
    {
        public frmColumn()
        {
            InitializeComponent();
        }

        public static int iIndexColumn;

        #region Property

        public ComboBox _cbbColumn
        {
            get
            {
                return cbbColumn;
            }
            set
            {
                cbbColumn = value;
            }
        }

        public int _iIndexColumn
        {
            get
            {
                return iIndexColumn;
            }
            set
            {
                iIndexColumn = value;
            }
        }

        #endregion

        private void frmColumn_Load(object sender, EventArgs e)
        {

        }

        private void frmColumn_KeyDown(object sender, KeyEventArgs e)
        {
            frmMain _frmMain = new frmMain();
            frmData _frmData = new frmData();
            frmModule _frmModule = new frmModule();

            #region Key Enter - View data by condition for search of table name
            if (e.KeyCode == Keys.Enter)
            {
                if (cbbColumn.Text != "")
                {
                    GetIndexFocusColumnOnGrid(cbbColumn.Text);

                    this.Hide();
                }
            }

            #endregion

            #region Key Esc - Close form

            if (e.KeyCode == (Keys.Back | Keys.RButton | Keys.LButton | Keys.ShiftKey))
            {
                this.Hide();
            }

            #endregion
        }


        #region Function

        public void GetIndexFocusColumnOnGrid(String strColumnName)
        {
            frmMain _frmMain = new frmMain();
            frmData _frmData = new frmData();

            if (_frmData._dgvData_Current.RowCount > 0)
            {
                for (int i = 0; i < _frmData._dgvData_Current.ColumnCount - 1; i++)
                {
                    if (_frmData._dgvData_Current.Columns[i].Name.ToString() == strColumnName)
                    {
                        _iIndexColumn = i;
                        break;
                    }
                }
            }
        }

        #endregion
    }
}
