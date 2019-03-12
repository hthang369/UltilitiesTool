using System;
using System.Data;
using System.Text;
using System.Collections.Generic;

namespace ConnectDatabase
{	
	public class CompareDatabasesController
	{
        public CompareDatabasesController()
		{
		}

        public DataSet GetDataSet(String strQuery)
        {
            return SQLConnect.RunQuery(strQuery);
        }
        
        public List<AllDataSources> GetAllTableDifference(ConfigConnectSQL SQLConnectCfg)
        {
            #region Danh sách AllDataSources ở ServerFrom

            Dictionary<String, AllDataSources> lstAllDataSourceFroms = GetDataSources(SQLConnectCfg, true);

            #endregion

            #region Danh sách AllDataSources ở ServerTo

            Dictionary<String, AllDataSources> lstAllDataSourceTos = GetDataSources(SQLConnectCfg, false);
            
            #endregion

            List<AllDataSources> lstAllDataSources = new List<AllDataSources>();

            #region Init lstAllDataSource

            foreach (String key in lstAllDataSourceFroms.Keys)
            {
                if (!lstAllDataSourceTos.ContainsKey(key))
                {
                    lstAllDataSources.Add(lstAllDataSourceFroms[key]);
                }
                else
                {
                    AllDataSources AllDataSource = new AllDataSources(false, key, true);
                    AllDataSources AllDataSourceFrom = lstAllDataSourceFroms[key];
                    AllDataSources AllDataSourceTo = lstAllDataSourceTos[key];

                    #region Check column isExist

                    foreach (Columns ColumnFrom in AllDataSourceFrom.ListColumns)
                    {
                        if (!CheckColumnExist(ColumnFrom, AllDataSourceTo))
                        {
                            AllDataSource.ListColumns.Add(ColumnFrom);
                        }
                    }

                    foreach (References ReferenceFrom in AllDataSourceFrom.ListReferences)
                    {
                        if (!CheckReferenceExist(ReferenceFrom, AllDataSourceTo))
                        {
                            AllDataSource.ListReferences.Add(ReferenceFrom);
                        }
                    }

                    #endregion

                    #region Check column isExist

                    foreach (Columns ColumnTo in AllDataSourceTo.ListColumns)
                    {
                        if (!CheckColumnExist(ColumnTo, AllDataSourceFrom))
                        {
                            AllDataSource.IsMoreColumn = true;
                            break;
                        }
                    }

                    if (AllDataSource.IsMoreColumn == false)
                    {
                        foreach (References ReferenceTo in AllDataSourceTo.ListReferences)
                        {
                            if (!CheckReferenceExist(ReferenceTo, AllDataSourceFrom))
                            {
                                AllDataSource.IsMoreColumn = true;
                                break;
                            }
                        }
                    }

                    #endregion

                    if (AllDataSource.ListColumns.Count > 0 || AllDataSource.ListReferences.Count > 0)
                    {
                        lstAllDataSources.Add(AllDataSource);
                    }
                }
            }

            #endregion

            return lstAllDataSources;
        }

        public Boolean ConnectDatabase(ConfigConnectSQL SQLConnectCfg, Boolean isFrom)
        {
            try
            {
                return SQLConnect.ChangeServer(SQLConnectCfg, isFrom);
            }
            catch
            {
                return false;
            }
        }

        public Boolean CheckColumnExist(Columns Column, AllDataSources AllDataSource)
        {
            foreach (Columns ColumnCheck in AllDataSource.ListColumns)
            {
                if (ColumnCheck.Column_Name == Column.Column_Name)
                {
                    return true;
                }
            }
            return false;
        }

        public Boolean CheckReferenceExist(References Reference, AllDataSources AllDataSource)
        {
            foreach (References ReferenceCheck in AllDataSource.ListReferences)
            {
                if (Reference.ForeignKey == ReferenceCheck.ForeignKey)
                {
                    return true;
                }
            }
            return false;
        }

        public Dictionary<String, AllDataSources> GetDataSources(ConfigConnectSQL SQLConnectCfg, Boolean isFrom)
        {
            Dictionary<String, AllDataSources> lstAllDataSources = new Dictionary<string, AllDataSources>();

            #region Danh sách Tables ở Server

            ConnectDatabase(SQLConnectCfg, isFrom);

            //Lấy danh sách Tables và Columns của Table
            String str = String.Format(@"SELECT TABLE_NAME, COLUMN_NAME, IS_NULLABLE, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH, ORDINAL_POSITION FROM INFORMATION_SCHEMA.COLUMNS-- ORDER BY TABLE_NAME,ORDINAL_POSITION");
            DataSet dsColumns = GetDataSet(str);

            //Lấy danh sách References của tất cả các Table
            str = String.Format(@"SELECT f.name AS ForeignKey,
                                        OBJECT_NAME (f.referenced_object_id) AS TableNamePrimaryKey,
                                        COL_NAME(fc.referenced_object_id,fc.referenced_column_id) AS ColumnNamePrimaryKey,
                                        OBJECT_NAME(f.parent_object_id) AS TableNameForeignKey,
                                        COL_NAME(fc.parent_object_id,fc.parent_column_id) AS ColumnNameForeignKey
                                        FROM sys.foreign_keys AS f
                                        INNER JOIN sys.foreign_key_columns AS fc ON f.OBJECT_ID = fc.constraint_object_id
                                        INNER JOIN sys.objects AS o ON o.OBJECT_ID = fc.referenced_object_id");
            DataSet dsReferences = GetDataSet(str);

            if (dsColumns == null || dsColumns.Tables.Count == 0 || dsReferences == null || dsReferences.Tables.Count == 0) return lstAllDataSources;            

            foreach (DataRow dr in dsColumns.Tables[0].Rows)
            {
                String TableName = dr["TABLE_NAME"].ToString();
                if (!lstAllDataSources.ContainsKey(TableName))
                {
                    AllDataSources AllDataSource = new AllDataSources(false, TableName, false);

                    Columns Column = new Columns(false, TableName, dr["COLUMN_NAME"].ToString(), dr["IS_NULLABLE"].ToString(), dr["DATA_TYPE"].ToString(), dr["CHARACTER_MAXIMUM_LENGTH"].ToString(), int.Parse(dr["ORDINAL_POSITION"].ToString()));
                    AllDataSource.ListColumns.Add(Column);
                    
                    //Lấy danh sách References cho Table này
                    if (dsReferences != null && dsReferences.Tables.Count > 0)
                    {
                        foreach (DataRow drReference in dsReferences.Tables[0].Rows)
                        {
                            if (drReference["TableNameForeignKey"].ToString() == TableName)
                            {
                                References Reference = new References(false, drReference["ForeignKey"].ToString(), drReference["TableNamePrimaryKey"].ToString(), drReference["ColumnNamePrimaryKey"].ToString(), drReference["TableNameForeignKey"].ToString(), drReference["ColumnNameForeignKey"].ToString());
                                AllDataSource.ListReferences.Add(Reference);
                            }
                        }
                    }

                    lstAllDataSources.Add(TableName, AllDataSource);
                }
                else
                {
                    AllDataSources AllDataSource = lstAllDataSources[TableName];
                    Columns Column = new Columns(false, TableName, dr["COLUMN_NAME"].ToString(), dr["IS_NULLABLE"].ToString(), dr["DATA_TYPE"].ToString(), dr["CHARACTER_MAXIMUM_LENGTH"].ToString(), int.Parse(dr["ORDINAL_POSITION"].ToString()));
                    AllDataSource.ListColumns.Add(Column);
                }
            }

            #endregion

            return lstAllDataSources;
        }

        public Dictionary<String, String> GetAllStoreProcedureDifference(ConfigConnectSQL SQLConnectCfg)
        {
            #region Danh sách References ở ServerFrom

            Dictionary<String, String> lstStoreProcedureFrom = GetListStoreProcedure(SQLConnectCfg, true);
            
            #endregion

            #region Danh sách References ở ServerTo

            Dictionary<String, String> lstStoreProcedureTo = GetListStoreProcedure(SQLConnectCfg, false);
            
            #endregion

            Dictionary<String, String> lstStoreProcedure = new Dictionary<String, String>();

            #region Init lstReferences

            ConnectDatabase(SQLConnectCfg, false);
            
            foreach (String key in lstStoreProcedureFrom.Keys)
            {
                if (!lstStoreProcedureTo.ContainsKey(key))
                {
                    String str = String.Format(@"sp_helptext @objname = '{0}';", key);
                    DataSet ds = GetDataSet(str);
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        String strValue = "";
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            if (dr[0] != DBNull.Value)
                            {
                                String strStore = dr[0].ToString();
                                strStore = XoaChuoiDau(strStore, '=');
                                strValue += strStore;
                            }
                        }
                        lstStoreProcedure.Add(key, strValue);
                    }
                }
            }

            #endregion

            return lstStoreProcedure;
        }

        public String XoaChuoiDau(String strOrg, Char character)
        {
            String strFix = "";
            int iPos = 0;
            while (strOrg.Length > iPos)
            {
                if (strOrg[iPos] != character)
                {
                    strFix += strOrg[iPos];
                    iPos++;                    
                }
                else
                {
                    int iCount = 0;
                    for (int i = iPos; i < strOrg.Length; i++)
                    {
                        if (strOrg[i] != character)
                        {                            
                            break;
                        }
                        iCount++;
                    }
                    if (iCount > 1) iPos += iCount;
                    else
                    {
                        strFix += character;
                        iPos++;
                    }
                }
            }            

            return strFix;
        }
        
        public Dictionary<String, String> GetListStoreProcedure(ConfigConnectSQL SQLConnectCfg, Boolean isFrom)
        {
            #region Danh sách References ở Server

            ConnectDatabase(SQLConnectCfg, isFrom);

            String str = String.Format(@"SELECT name FROM SYS.PROCEDURES");
            DataSet dsStoreProcedures = GetDataSet(str);

            Dictionary<String, String> lstStoreProcedure = new Dictionary<String, String>();
            if (dsStoreProcedures != null && dsStoreProcedures.Tables.Count > 0)
            {
                foreach (DataRow dr in dsStoreProcedures.Tables[0].Rows)
                {
                    if (dr["name"] != DBNull.Value && dr["name"].ToString() != String.Empty && !lstStoreProcedure.ContainsKey(dr["name"].ToString()))
                    {
                        lstStoreProcedure.Add(dr["name"].ToString(), dr["name"].ToString());
                    }
                }
            }

            #endregion

            return lstStoreProcedure;
        }

        public DataSet GetDanhSachTables()
        {
            String str = "select object_id,name from sys.tables";
            return GetDataSet(str);
        }

        public DataSet GetDanhSachColumn(String TableName)
        {
            String str = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS where TABLE_NAME ='" + TableName + "'";
            return GetDataSet(str);
        }

        public DataSet DetailField_IsExitSource(string Module, string Group)
        {
            String strcom = string.Format(@"select STFieldID,STFieldName,'FALSE' as STFieldKhac  from STFields where STScreenID in (select STScreenID from STScreens where STModuleID in (select STModuleID from STModules where STModuleName='{0}') and STUserGroupID = ( select ADUserGroupID from ADUserGroups where ADUserGroupID = {1}))", Module, Group);
            return GetDataSet(strcom);
        }
	}
}