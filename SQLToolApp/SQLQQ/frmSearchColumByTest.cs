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
    public partial class frmSearchColumByTest : Form
    {
        public frmSearchColumByTest()
        {
            InitializeComponent();
        }

        frmData _frmData;

        #region Property

        public ComboBox _cbbViewColumnByText
        {
            get
            {
                return cbbViewColumnByText;
            }
            set
            {
                cbbViewColumnByText = value;
            }
        }

        #endregion
        private void frmSearchColumByTest_Load_1(object sender, EventArgs e)
        {
            cbbViewColumnByText.KeyDown += cbbViewColumnByText_KeyDown;
        }

        void cbbViewColumnByText_KeyDown(object sender, KeyEventArgs e)
        {
            frmMain _frmMain = new frmMain();
            frmData _frmData = new frmData();
            frmModule _frmModule = new frmModule();

            #region Key Enter - View data by condition for search of table name
            if (e.KeyCode == Keys.Enter)
            {
                if (cbbViewColumnByText.Text != "")
                {
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

       

        #region Event

      
        #endregion


        #region Function


        #endregion

    }
}
