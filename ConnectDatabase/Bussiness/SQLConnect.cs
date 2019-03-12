using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.Common;

namespace ConnectDatabase
{	
	public static class SQLConnect
	{
        public static SqlConnection dbConn = null;

        public static Boolean ChangeServer(ConfigConnectSQL SQLConnect, Boolean isFrom)
        {
            try
            {
                dbConn = new SqlConnection(GetStringConnect(SQLConnect, isFrom));
                dbConn.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static Boolean ChangeServer(String strServer, String strDatabase, String strUsername, String strPassword)
        {
            try
            {
                dbConn = new SqlConnection(GetStringConnect(strServer, strDatabase, strUsername, strPassword));
                dbConn.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static String GetStringConnect(ConfigConnectSQL SQLConnect, Boolean isFrom)
        {
            String strConnect = String.Empty;
            
            if (isFrom)
            {
                strConnect = GetStringConnect(SQLConnect.FromServerName, SQLConnect.FromDatabase, SQLConnect.FromUserName, SQLConnect.FromPassword);
            }
            else
            {
                strConnect = GetStringConnect(SQLConnect.ToServerName, SQLConnect.ToDatabase, SQLConnect.ToUserName, SQLConnect.ToPassword);
            }

            return strConnect;
        }

        public static String GetStringConnect(String strServer, String strDatabase, String strUsername, String strPassword)
        {
            return String.Format(@"Server={0};Database={1};User Id={2};Password={3};"
                                 , strServer, strDatabase, strUsername, strPassword);
        }

        public static SqlCommand GetQuery(string strQueryCommand)
        {
            try
            {
                return new SqlCommand(strQueryCommand, dbConn);
            }
            catch
            {
                return null;
            }
        }

        public static DataSet RunQuery(SqlCommand cmd)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        
        public static DataSet RunQuery(String cmdText)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand(cmdText, dbConn);
                adapter.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                return null;
            }            
        }

        public static DataSet RunStoredProcedure(string spName, params object[] values)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = dbConn;
            command.CommandText = spName;
            command.CommandType = CommandType.StoredProcedure;

            SqlCommandBuilder.DeriveParameters(command);
            command.Parameters.Remove(command.Parameters["@RETURN_VALUE"]);

            // Add the input parameter and set value
            for (int iIndex = 0; iIndex < command.Parameters.Count; iIndex++)
            {
                if (values.Length > iIndex)
                {
                    command.Parameters[iIndex].Value = values[iIndex];
                }
            }

            // Open the connection and execute the reader.
            try
            {
                return RunQuery(command);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static Boolean IsExistObjectName(String strTableName, String strObjectName)
        {
            String strTableNameRef = strTableName.Substring(0, strTableName.Length - 1);
            String strColumnName = strTableNameRef + "Name";
            String str = String.Format(@"SELECT * FROM {0} WHERE {1} = '{2}'", strTableName, strColumnName, strObjectName);
            DataSet ds = SQLConnect.RunQuery(str);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return true;
            }

            return false;
        }

        public static DataSet GetDatabases()
        {
            string strQuery = @"select db_name(s_mf.database_id) as name from sys.master_files s_mf
                                group by s_mf.database_id
                                having db_name(s_mf.database_id) not in ('master','model','msdb','tempdb')
                                order by 1";
            return RunQuery(strQuery);
        }

        #region Generate StoreProcedure

        #region StoreProcedure

        private static readonly string spGetTableColumns = "GEDBUtil_SelectTableColumns";
        private static readonly string spGetAllowNullTableColumn = "GEDBUtil_SelectAllowNullTableColumn";
        private static readonly string spGetTableForeignKeys = "GEDBUtil_SelectTableForeignKeys";
        private static readonly string spGetViewColumns = "GEDBUtil_SelectViewColumns";
        private static readonly string spGetViewColumn = "GEDBUtil_SelectViewColumn";

        private static Dictionary<String, DataSet> dicTableForeignKeys = new Dictionary<String,DataSet>();
        private static Dictionary<String, DataSet> dicTablePrimaryColumns = new Dictionary<String, DataSet>();
        private static Dictionary<String, DataSet> dicTableColumns = new Dictionary<String, DataSet>();

        #endregion

        #region Function

        public static DataSet GetTableForeignKeys(string strTableName)
        {
            if (dicTableForeignKeys.ContainsKey(strTableName))
            {
                return dicTableForeignKeys[strTableName];
            }

            DataSet dataSet = SQLConnect.RunStoredProcedure(SQLConnect.spGetTableForeignKeys, new object[1]
              {
                (object) strTableName
              });
            if (dataSet == null)
            {
                dataSet = new DataSet();
                dataSet.Tables.Add(new DataTable());
            }
            else
            {
                dicTableForeignKeys.Add(strTableName, dataSet);
            }
            return dataSet;
        }

        public static DataRow GetTableForeignKey(string strTableName, string strColumnName)
        {
            DataSet tableForeignKeys = SQLConnect.GetTableForeignKeys(strTableName);
            if (tableForeignKeys != null && tableForeignKeys.Tables[0].Rows.Count > 0)
            {
                DataRow[] dataRowArray = tableForeignKeys.Tables[0].Select(string.Format("[COLUMN_NAME]='{0}'", (object)strColumnName));
                if (dataRowArray.Length > 0)
                    return dataRowArray[0];
            }
            return (DataRow)null;
        }

        public static bool IsForeignKey(string strTableName, string strColumnName)
        {
            if (strColumnName.StartsWith("FK_"))
                return true;
            bool flag = false;
            if (SQLConnect.GetTableForeignKey(strTableName, strColumnName) != null)
                flag = true;
            return flag;
        }

        public static bool IsPrimaryKey(string strTableName, string strColumnName)
        {
            return SQLConnect.GetTablePrimaryColumn(strTableName).ToUpper().Equals(strColumnName.ToUpper());
        }

        public static string GetTablePrimaryColumn(string strTableName)
        {
            DataSet dataSet = null;

            if (dicTablePrimaryColumns.ContainsKey(strTableName))
            {
                dataSet = dicTablePrimaryColumns[strTableName];
            }
            else
            {
                dataSet = SQLConnect.RunStoredProcedure("GEDBUtil_SelectTablePrimaryKeys", new object[1]
              {
                (object) strTableName
              });
                if (dataSet != null) dicTablePrimaryColumns.Add(strTableName, dataSet);
            }
            if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                return dataSet.Tables[0].Rows[0]["COLUMN_NAME"].ToString();
            else
                return string.Empty;
        }

        public static string GetColumnDbType(string strTableName, string strColumnName)
        {
            string str = string.Empty;
            DataRow tableColumn = SQLConnect.GetTableColumn(strTableName, strColumnName);
            if (tableColumn != null)
            {
                str = tableColumn["DATA_TYPE"].ToString();
                if (str == "varchar" || str == "nvarchar" || str == "varbinary")
                    str = str + "(" + tableColumn["CHARACTER_MAXIMUM_LENGTH"].ToString() + ")";
            }
            return str;
        }

        public static DataSet GetTableColumns(string strTableName)
        {
            if (dicTableColumns.ContainsKey(strTableName))
            {
                return dicTableColumns[strTableName];
            }

            DataSet dataSet = SQLConnect.RunStoredProcedure(SQLConnect.spGetTableColumns, new object[1]
              {
                (object) strTableName
              });
            if (dataSet == null)
            {
                dataSet = new DataSet();
                dataSet.Tables.Add(new DataTable());
            }
            else
            {
                dicTableColumns.Add(strTableName, dataSet);
            }
            return dataSet;
        }

        public static DataRow GetTableColumn(string strTableName, string strColumnName)
        {
            DataSet tableColumns = SQLConnect.GetTableColumns(strTableName);
            if (tableColumns != null && tableColumns.Tables[0].Rows.Count > 0)
            {
                DataRow[] dataRowArray = tableColumns.Tables[0].Select(string.Format("[COLUMN_NAME]='{0}'", (object)strColumnName));
                if (dataRowArray.Length > 0)
                    return dataRowArray[0];
            }
            return (DataRow)null;
        }

        public static bool ColumnIsAllowNull(string strTableName, string strColumnName)
        {
            DataRow tableColumn = SQLConnect.GetTableColumn(strTableName, strColumnName);
            return tableColumn == null || tableColumn["IS_NULLABLE"].ToString() == "YES";
        }

        public static bool ColumnIsExist(string strTableName, string strColumnName)
        {
            return SQLConnect.GetTableColumn(strTableName, strColumnName) != null;
        }

        public static string ExecuteStoredProcedureScript(string strStoredProcedureScript)
        {
            try
            {
                SQLConnect.RunQuery(strStoredProcedureScript);
                return "Command excute succesfully!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string GetTableNameByViewNameAndColumnName(string strViewName, string strColumnName)
        {
            DataSet viewColumn = SQLConnect.GetViewColumn(strViewName, strColumnName);
            if (viewColumn != null && viewColumn.Tables.Count > 0 && viewColumn.Tables[0].Rows.Count > 0)
                return viewColumn.Tables[0].Rows[0]["TABLE_NAME"].ToString();
            else
                return string.Empty;
        }

        public static DataSet GetViewColumns(string strViewName)
        {
            return SQLConnect.RunStoredProcedure(SQLConnect.spGetViewColumns, new object[1]
              {
                (object) strViewName
              });
        }

        public static DataSet GetViewColumn(string strViewName, string strColumnName)
        {
            return SQLConnect.RunStoredProcedure(SQLConnect.spGetViewColumn, (object)strViewName, (object)strColumnName);
        }

        public static DataSet GetAllowNullTableColumn(string strTableName, string colName)
        {
            return SQLConnect.RunStoredProcedure(SQLConnect.spGetAllowNullTableColumn, (object)strTableName, (object)colName);
        }

        public static string GetColumnDataType(string strTableName, string strColumnName)
        {
            string str = string.Empty;
            DataRow tableColumn = SQLConnect.GetTableColumn(strTableName, strColumnName);
            if (tableColumn != null)
                str = tableColumn["DATA_TYPE"].ToString();
            return str;
        }

        #endregion

        #endregion
    }

    public class ConfigConnectSQL
    {
        public ConfigConnectSQL()
        {
        }

        public String FromServerName { get; set; }
        public String FromUserName { get; set; }
        public String FromPassword { get; set; }
        public String FromDatabase { get; set; }
        public String ToServerName { get; set; }
        public String ToUserName { get; set; }
        public String ToPassword { get; set; }
        public String ToDatabase { get; set; }
    }
}