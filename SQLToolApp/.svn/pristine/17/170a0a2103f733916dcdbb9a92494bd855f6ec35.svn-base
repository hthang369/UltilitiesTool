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
    public partial class frmModule : Form
    {
        public frmModule()
        {
            InitializeComponent();
        }

        #region Property

        public ComboBox _cbbModule
        {
            get
            {
                return cbbModule;
            }
            set
            {
                cbbModule = value;
            }
        }

        #endregion

        private void frmModule_Load(object sender, EventArgs e)
        {
            frmMain _frmMain = new frmMain();
     
        }

        private void frmModule_KeyDown(object sender, KeyEventArgs e)
        {
            frmMain _frmMain = new frmMain();
            frmData _frmData = new frmData();
            frmSearch _frmSearch = new frmSearch();

            #region Key Enter - View data by condition for search of table name

            if (cbbModule.Text != "")
            {
                if (e.KeyCode == Keys.Enter)
                {
                    String strQuerySearchDataBySTGridColumns = String.Format(@" 
                                                                        SELECT STGridColumns.*
                                                                        FROM STModules INNER JOIN STScreens ON STModules.STModuleID = STScreens.STModuleID
			                                                                           INNER JOIN STFields ON STScreens.STScreenID = STFields.STScreenID
			                                                                           INNER JOIN STGridColumns ON STFields.STFieldID = STGridColumns.FK_STFieldID
                                                                        Where STModules.STModuleName = '{0}'
                                                              ", cbbModule.Text);

                 
                    DataTable dtData = new DataTable();

                    dtData = SQLDBUtil.GetDataTable(strQuerySearchDataBySTGridColumns);
                    if (dtData != null && dtData.Rows.Count > 0)
                    {
                        _frmData._strQuery_RecentData = strQuerySearchDataBySTGridColumns;
                        _frmData._sttRowCount.Text = "Row : " + dtData.Rows.Count.ToString();
                        _frmData._dgrSearch.DataSource = dtData;
                        _frmData._dgrSearch.Refresh();
                    }

                    _frmSearch.Hide();
                    this.Hide();
                    _frmData.Show();
                    _frmData.StartPosition = FormStartPosition.CenterParent;
                }
            }

            #endregion

            #region Key Esc - Close form

            if (e.KeyCode == (Keys.Back | Keys.RButton | Keys.LButton | Keys.ShiftKey))
            {
                this.Hide();
                _frmMain.ShowDialog();
            }

            #endregion
        }
    }
}
