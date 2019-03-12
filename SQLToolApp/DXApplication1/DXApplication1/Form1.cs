using DevExpress.Utils.Menu;
using SQLAppLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DXApplication1
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        public string strFileCfg = Application.StartupPath + "\\config.ini";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadConfigComboBox();
            double db = Math.Round(555085.23, 2, MidpointRounding.ToEven);
        }

        private void LoadConfigComboBox()
        {
            SQLUtil.LoadConfigIniFromServer(cboServer);
            cboDatabase.Properties.Items.AddRange(SQLUtil.LoadDatabaseByServer(cboServer.SelectedIndex));
        }

        private void cboDatabase_QueryPopUp(object sender, CancelEventArgs e)
        {
            cboDatabase.Properties.Items.AddRange(SQLUtil.LoadDatabaseByServer(cboServer.SelectedIndex));
        }

        private void cboDatabase_SelectedIndexChanged(object sender, EventArgs e)
        {
            SQLDBUtil.ChangeDatabase(cboDatabase.SelectedText);
            this.fld_dgcImportCfgs.FieldDataSource = "GLAccountingImportMapCols";
            this.fld_dgcImportCfgs.isAllowNew = true;
            this.fld_dgcImportCfgs.isAllowFilter = true;
            this.fld_dgcImportCfgs.InitializeGridControl();
        }
    }
}
