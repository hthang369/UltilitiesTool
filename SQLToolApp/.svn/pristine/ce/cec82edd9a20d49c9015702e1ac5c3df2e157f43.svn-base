using SQLAppLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SQLQQ.Util
{
    public class FunctionList
    {
        public static string strPath = Application.StartupPath + "\\Scripts\\";
        //Ctrl + 1 tìm module
        public static DataTable FindModule()
        {
            string strQuery = SQLApp.GetFile(strPath + "FindModule.sql");
            strQuery = strQuery.Replace("@ModuleName@", "");
            return SQLDBUtil.GetDataTable(strQuery);
        }
        
        public static DataTable FindColumn(string strTableName, string strColumnName)
        {
            DataSet ds = SQLDBUtil.GetAllTableColumns(strTableName, strColumnName);
            return SQLDBUtil.GetDataTableByDataSet(ds);
        }
    }
}
