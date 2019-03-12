using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;

namespace CreateCompany
{
    public partial class GridColumn : Form
    {
        public DataSet dataSet;
        public String memofield;
        public String str;
        public GridColumn()
        {
            InitializeComponent();
        }

        private void GridColumn_Load(object sender, EventArgs e)
        {
            if (dataSet == null) return;
            String chuoi1 = memofield;
            String[] splits = chuoi1.Split(new Char[] { ';' });
            DataTable mydt = new DataTable();
            DataRow mydr;
            mydt.Columns.Add(new DataColumn("Column", typeof(string)));
            mydt.Columns.Add(new DataColumn("Check", typeof(bool)));
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                mydr = mydt.NewRow();
                mydr["Column"] = row[0];
                if(splits.Contains(row[0]))
                  mydr["Check"] = true;
                else
                  mydr["Check"] = false;
                mydt.Rows.Add(mydr);
                gridControlColumns.DataSource = mydt;
                gridControlColumns.RefreshDataSource();
            }
        }

        private void btnPutSelectedItems_Click(object sender, EventArgs e)
        {
            DataTable tbselected = new DataTable();
            for (int iRow = 0; iRow < gridViewColumns.RowCount; iRow ++ )
            {
               DataRowView dr = (DataRowView)gridViewColumns.GetRow(iRow);
               bool icheck = false;
               try {
                   icheck = Convert.ToBoolean(dr["Check"]);
               }
               catch { }
               if (icheck == true)
               {
                   String col = String.Empty;
                   try
                   {
                       col= dr["Column"].ToString();
                   }
                   catch { }
                   if (String.IsNullOrEmpty(col) == false)
                   {
                       if (String.IsNullOrEmpty(str))
                       {
                           str += col;
                       }
                       else
                           str += ";" + col;
                   }
               }
            }
            this.Close();
        }
    }
}