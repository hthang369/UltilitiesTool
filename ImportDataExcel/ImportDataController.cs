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
using System.Text.RegularExpressions;

namespace SetTabIndex
{
    public class ImportDataController : CompareDatabasesController
    {
        List<Tables> lstObjects = new List<Tables>();
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

        public List<Tables> GetAllDataSource(string fill)
        {
            DataSet ds = GetAllTableStores(fill);
            lstObjects.Clear();
            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Tables obj = new Tables(false, dr["name"].ToString(), false);
                    obj.Type = dr["type"].ToString();
                    lstObjects.Add(obj);
                }
            }
            return lstObjects;
        }

        public DataSet GetForeignKeyTable(string tableName, string fieldName)
        {
            string strQuery = @"SELECT tc.TABLE_NAME,ccu.COLUMN_NAME
                                FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE kcu 
                                INNER JOIN INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS rc ON (kcu.CONSTRAINT_NAME = rc.CONSTRAINT_NAME)
                                INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS tc ON (rc.UNIQUE_CONSTRAINT_NAME = tc.CONSTRAINT_NAME)
                                INNER JOIN INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE ccu ON (ccu.CONSTRAINT_NAME = tc.CONSTRAINT_NAME)
                                AND (ccu.TABLE_NAME = tc.TABLE_NAME)
                                WHERE (kcu.TABLE_NAME = {0}) AND (kcu.COLUMN_NAME = {1} AND tc.CONSTRAINT_TYPE = 'PRIMARY KEY')";
            strQuery = string.Format(strQuery, tableName, fieldName);
            return GetDataSet(strQuery);
        }

        public string GetDataForeignKey(string tableName, string fieldName, string strValue)
        {
            string[] strArr = fieldName.Split('|');
            string fk_fieldName = strArr[0];
            string objFieldName = (strArr.Length > 1) ? strArr[1] : "No";
            DataSet ds = GetForeignKeyTable(tableName, fieldName);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                string _objTable = dr["TABLE_NAME"].ToString();
                string _tablePrefix = _objTable.Substring(0, _objTable.Length - 1);
                string _objWhere = string.Format("{0} = '{1}'", _tablePrefix + objFieldName, strValue);
                string _colName = dr["COLUMN_NAME"].ToString();
                string strQuery = string.Format(@"SELECT {0} FROM {1} WHERE {2}", _colName, _objTable, _objWhere);
                DataSet dsData = GetDataSet(strQuery);
                if (dsData != null && dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
                    return (dsData.Tables[0].Rows[0][_colName]).ToString();
            }
            return string.Empty;
        }
    }
    public class ObjectStringUtil
    {
        public static string GetTableNameByColumName(string columnName)
        {
            string strQuery = string.Format(@"SELECT t.name FROM sys.tables AS t INNER JOIN sys.columns AS c ON t.object_id = c.object_id
                                                WHERE c.name = '{0}'", columnName);
            DataSet ds = GMCDbUtil.ExecuteQuery(strQuery);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                return (ds.Tables[0].Rows[0][0]).ToString();
            return null;
        }
        public static BaseBusinessController GetControllerByTableName(string tableName)
        {
            return BusinessControllerFactory.GetBusinessController(tableName + "Controller");
        }
        public static BusinessObject GetInfoByTableName(string tableName)
        {
            return BusinessObjectFactory.GetBusinessObject(tableName + "Info");
        }
        public static string RegexField(string fieldName, string strRegex = @"(\d)")
        {
            Regex myregex = new Regex(strRegex);
            return myregex.Replace(fieldName, "");
        }
    }
}
