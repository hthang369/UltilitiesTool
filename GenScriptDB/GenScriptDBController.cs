using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using ExpertLib;
using System.IO;
using System.Data;
using ConnectDatabase;

namespace SetTabIndex
{
    public class GenScriptDBController : CompareDatabasesController
    {
        List<Tables> lstObjects = new List<Tables>();
        string newLine = "\n";
        public List<Tables> ObjectsList
        {
            get { return lstObjects; }
        }
        public DataSet GetAllTableStores(string strFill = "")
        {
            string sqlquery = "select * from sys.objects {0}";
            if (string.IsNullOrEmpty(strFill)) sqlquery = string.Format(sqlquery, "where type in('U','P')");
            else sqlquery = string.Format(sqlquery, string.Format("where type = '{0}'", strFill));
            return GetDataSet(sqlquery);
        }

        private string GetSchemaTable(string tableName)
        {
            DataSet ds = GetDataSet(string.Format("SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '{0}'", tableName));
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                return (ds.Tables[0].Rows[0]["TABLE_SCHEMA"]).ToString();
            return string.Empty;
        }

        public List<Tables> GetAllDataSource(string fill)
        {
            DataSet ds = GetAllTableStores(fill);
            lstObjects.Clear();
            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Tables obj = new Tables(false, dr["name"].ToString(), false);
                    obj.Type = dr["type"].ToString().Trim();
                    lstObjects.Add(obj);
                }
            }
            return lstObjects;
        }

        public DataSet GenerateTable(string tableName)
        {
            return GenerateObjects(tableName, "U", "CreateTable.sql");
        }

        public DataSet GenerateStore(string storeName)
        {
            return GenerateObjects(storeName, "P", "CreateStore.sql");
        }

        public DataSet GenerateObjects(string objectName, string type, string file)
        {
            string fileName = Application.StartupPath + "\\" + file;
            string strQuery = string.Empty;
            if (File.Exists(fileName))
            {
                using (StreamReader read = new StreamReader(fileName))
                {
                    strQuery = read.ReadToEnd();
                }
                strQuery = string.Format(strQuery, "''" + objectName + "''", type);
            }

            return GetDataSet(strQuery);
        }

        public string GenerateInsert(string tableName)
        {
            string strTemplate = "INSERT INTO {0}({1}) VALUES";
            string strNew = string.Empty;
            DataSet dsColumn = GetDanhSachColumn(tableName);
            List<object> lstColumn = (from c in dsColumn.Tables[0].Rows.Cast<DataRow>().ToList() select c[0]).ToList();
            string strCol = "[" + string.Join("],[", lstColumn.Cast<string>().ToArray()) + "]";
            strTemplate = string.Format(strTemplate, GetTableNameString(tableName), strCol);
            DataSet dsInfo = GetDataSet(string.Format("SELECT {0} FROM {1}", strCol, tableName));
            int idx = 0;
            foreach (DataRow dr in dsInfo.Tables[0].Rows)
            {
                List<string> lstcol = new List<string>();
                foreach (string field in lstColumn.Cast<string>().ToArray())
                {
                    Type type = dr[field].GetType();
                    if (type == typeof(String) || type == typeof(DateTime))
                        lstcol.Add("'" + dr[field].ToString() + "'");
                    else if(type == typeof(DBNull))
                        lstcol.Add("NULL");
                    else
                        lstcol.Add(dr[field].ToString());
                }
                string strInsert = "(" + string.Join(",", lstcol.ToArray()) + ")";
                if (idx % 20 == 0)
                {
                    if (!string.IsNullOrEmpty(strNew)) strNew = trim(trim(strNew, '\n')) + newLine;
                    strNew += strTemplate + strInsert + "," + newLine;
                }
                else
                    strNew += strInsert + "," + newLine;
                idx++;
            }
            return trim(trim(strNew, '\n'));
        }

        private string trim(string str, char param = ',')
        {
            char[] newParam = new char[] { param };
            return str.TrimEnd(newParam);
        }

        private string GetTableNameString(string tableName)
        {
            return string.Format("[{0}].[{1}]", GetSchemaTable(tableName), tableName);
        }

        public string GenerateForeignKey(string tableName)
        {
            string strTemplate = "ALTER TABLE [{0}].[{1}] ADD CONSTRAINT [{2}] FOREIGN KEY ([{3}]) REFERENCES [{0}].[{4}] ([{5}])";
            string strNew = string.Empty;
            DataSet ds = GetConstraintTable(tableName);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    strNew += string.Format(strTemplate, dr["TABLE_SCHEMA"].ToString(), dr["TABLE_NAME"].ToString(),
                        dr["CONSTRAINT_NAME"].ToString(), dr["COLUMN_NAME"].ToString(), dr["FOREIGN_TABLE_NAME"].ToString(), dr["PK_COLUMN_NAME"].ToString()) + "\n\n";
                }
            }
            return strNew;
        }

        public DataSet GetConstraintTable(string tableName)
        {
            string strQuery = string.Format(@"SELECT DISTINCT tc.TABLE_NAME,tc.TABLE_SCHEMA,kc.COLUMN_NAME,cc.CONSTRAINT_NAME,cc.FOREIGN_TABLE_NAME,cc.UNIQUE_CONSTRAINT_NAME,cc.COLUMN_NAME as PK_COLUMN_NAME
                                                FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS tc
                                                INNER JOIN INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE kc ON (tc.CONSTRAINT_NAME = kc.CONSTRAINT_NAME)
                                                INNER JOIN (SELECT tc.TABLE_NAME as FOREIGN_TABLE_NAME,rc.UNIQUE_CONSTRAINT_NAME,rc.CONSTRAINT_NAME,kcu.COLUMN_NAME
                                                            FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS rc
                                                            INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS tc on (rc.UNIQUE_CONSTRAINT_NAME = tc.CONSTRAINT_NAME)
                                                            INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE kcu on (kcu.CONSTRAINT_NAME = rc.UNIQUE_CONSTRAINT_NAME)) as cc
                                                    ON cc.CONSTRAINT_NAME = tc.CONSTRAINT_NAME
                                                WHERE tc.TABLE_NAME = '{0}' and CONSTRAINT_TYPE = 'FOREIGN KEY'", tableName);
            return GetDataSet(strQuery);
        }
    }
}
