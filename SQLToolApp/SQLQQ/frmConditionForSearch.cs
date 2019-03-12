using SQLAppLib;
using SQLQQ.Util;
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
    public partial class frmConditionForSearch : Form
    {
        public frmConditionForSearch()
        {
            InitializeComponent();
        }

        #region Property

        public ComboBox _cbbConditionForSearchData
        {
            get
            {
                return cbbConditionFroSearchData;
            }
            set
            {
                cbbConditionFroSearchData = value;
            }
        }

        #endregion

        private void frmConditionForSearch_Load(object sender, EventArgs e)
        {

        }

        private void frmConditionForSearch_KeyDown(object sender, KeyEventArgs e)
        {
            frmMain _frmMain = new frmMain();
            frmData _frmData = new frmData();

            #region Esc - Thoát

            if (e.KeyCode == (Keys.Back | Keys.RButton | Keys.LButton | Keys.ShiftKey))
            {
                this.Close();
            }

            #endregion

            #region Key Enter - View data by condition for search of table name

            if (e.KeyCode == Keys.Enter)
            {
                String strQuerySearchDataByAlive = string.Empty;
                String strQuerySearchDataNotAlive = string.Empty;

                if (_frmData._strTableName == "STModules")
                {
                    String strConditionSearch = String.Format(@"    Where {0} LIKE N'{1}'", _frmData._strCurrentCellColumnName, cbbConditionFroSearchData.Text);
                    strQuerySearchDataByAlive = _frmData._strQuery_RecentData + strConditionSearch;
                }
                else
                {
                    if (_frmData._strCurrentCellColumnName.Contains("FK_"))
                    {
                        try
                        {
                            int iFK_ID = Convert.ToInt32(cbbConditionFroSearchData.Text); // Check data is Number
                        }
                        catch 
                        {
                            cbbConditionFroSearchData.Text = SQLDBUtil.GetIDObjectByNoAndTableName(cbbConditionFroSearchData.Text, SQLQQ.Util.BaseUtil.GetForeignTableNameByForeignColum(_frmData._strCurrentCellColumnName)).ToString();
                        }
                    }

                    strQuerySearchDataByAlive = String.Format(@" Select *
                                                                     From   {0}
                                                                     Where {1} LIKE N'{2}'
                                                              ", _frmData._strTableName, _frmData._strCurrentCellColumnName, cbbConditionFroSearchData.Text);

                    strQuerySearchDataNotAlive = String.Format(@"  Select *
                                                                     From   {0}
                                                                     Where  AAStatus = 'Alive' AND {1} LIKE N'{2}'
                                                                      
                                                              ", _frmData._strTableName, _frmData._strCurrentCellColumnName, cbbConditionFroSearchData.Text);
                }

                DataTable dtData = new DataTable();
                bool isData_Query = false;

                dtData = SQLDBUtil.GetDataTable(strQuerySearchDataByAlive);
                if (dtData != null && dtData.Rows.Count > 0)
                {
                    _frmData._strQuery_RecentData = strQuerySearchDataByAlive;
                    isData_Query = true;
                }
                else
                {
                    dtData = SQLDBUtil.GetDataTable(strQuerySearchDataNotAlive);
                    if (dtData != null && dtData.Rows.Count > 0)
                    {
                        _frmData._strQuery_RecentData = strQuerySearchDataNotAlive;
                        isData_Query = true;
                    }
                }

                if ( isData_Query == true)
                {
                    _frmData._iCountSearch = dtData.Rows.Count;
                    _frmData._sttRowCount.Text = "Row : " + _frmData._iCountSearch + "| ";
                    _frmData._dgrSearch.DataSource = dtData;
                    _frmData._dgrSearch.Refresh();
                    this.Hide();
                    _frmData.Text = "Table Name : " + _frmData._strTableName;
                    _frmData.Text += " - (Server : " + SQLDBUtil._strServer + " Database : " + SQLDBUtil._strDatabase + ")";
                    _frmData.ShowDialog();
                    _frmData.StartPosition = FormStartPosition.CenterScreen;
                }
            }

            #endregion
        }
    }
}
