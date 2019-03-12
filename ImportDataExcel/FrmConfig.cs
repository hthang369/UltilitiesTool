using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ExpertLib;
using ExpertERP.BaseProvider;
using ExpertERP.BusinessEntities;

namespace SetTabIndex
{
    public partial class FrmConfig : Form
    {
        public GMCList<GLAccountingImportMapColsInfo> lstGLAccountingImportMapCols;
        public FrmConfig()
        {
            InitializeComponent();
        }

        public void InitControl()
        {
            lstGLAccountingImportMapCols = new GMCList<GLAccountingImportMapColsInfo>();
            lstGLAccountingImportMapCols.InitGMCList(null, "", GMCUtil.GetTableNameFromBusinessObjectType(typeof(GLAccountingImportMapColsInfo)));
        }

        private void InitGridControl()
        {
            GMCGridColumn columns = new GMCGridColumn();
            List<string> lstField = new List<string>() { "GLAccountingImportMapColTableName", "GLAccountingImportMapColColumnName" };
        }
    }
}
