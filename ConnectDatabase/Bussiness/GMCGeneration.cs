using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ConnectDatabase
{
    public class GMCGeneration
    {
        public const String MySQL = "MySQL";
        public const String MSSQL = "MSSQL";
        public const String Oracle = "Oracle";
        private const String AAStatusColumn = "AAStatus";
        private const String AAUpdatedDateColumn = "AAUpdatedDate";
        private const String AACreatedDateColumn = "AACreatedDate";
        private const String DefaultStatus = "Alive";
        protected String _tableName;
        protected String _database;
        protected String _moduleName;        

        #region Constructor
        public GMCGeneration()
        {

        }

        public GMCGeneration(String strTableName)
        {
            TableName = strTableName;
        }
        public GMCGeneration ( String strTableName,String strModuleName )
        {
            TableName = strTableName;
            ModuleName = strModuleName;
        }
        #endregion        

        #region Functions for Generate Stored Procedure
        
        public String GenerateTableStoredProcedures()
        {            
            StringBuilder strSPBuilder = new StringBuilder();
            //Generate Insert Stored Procedure
            strSPBuilder.Append(GenerateInsertStoredProcedure(TableName)+NewLine);

            //Generate Update Stored Procedure
            strSPBuilder.Append(GenerateUpdateStoredProcedure(TableName)+NewLine);

            //Generate Select Stored Procedure
            strSPBuilder.Append(GenerateSelectStoredProcedure(TableName)+NewLine);

            strSPBuilder.Append(GenerateSelectDeletedByIDStoredProcedure(TableName) + NewLine);

            //Generate Select By Name Stored Procedure
            if (IsExistNameColumn(TableName))
                strSPBuilder.Append(GenerateSelectByNameStoredProcedure(TableName) + NewLine);
            if (IsExistColumnNo(TableName))
                strSPBuilder.Append(GenerateSelectByNoStoredProcedure(TableName) + NewLine);
            
            //Generate Select All Stored Procedure
            strSPBuilder.Append(GenerateSelectAllStoredProcedure(TableName)+NewLine);

            //Generate All Stored Procedure Select By Foreign Key
            strSPBuilder.Append(GenerateAllStoredProcedureSelectByForeignColumn(TableName)+NewLine);            

            //Generate Delete Stored Procedure
            strSPBuilder.Append(GenerateDeleteStoredProcedure(TableName)+NewLine);

            //Generate Delete All Stored Procedure
            strSPBuilder.Append(GenerateDeleteAllStoredProcedure(TableName)+NewLine);

            //Generate All Stored Procedure Delete By Foreign Column
            strSPBuilder.Append(GenerateAllStoredProcedureDeleteByForeignColumn(TableName)+NewLine);
            
            String strModuleName = TableName.Substring(2, TableName.Length - 3);
            if (strModuleName.EndsWith("ie"))
                strModuleName = strModuleName.Substring(0, strModuleName.Length - 2) + "y";
            if (SQLConnect.IsExistObjectName("STModules", strModuleName))
            {                
                //Vuong add generate Search Stored Procedure from Main Table,02.03.2007
                strSPBuilder.Append(GenerateSearchStoredProcedure(TableName) + NewLine);
            }
            
            return strSPBuilder.ToString();
        }        

        #region Utility(Generate Header,Begin Query,After Query,Drop Procedure if Exist)
        private String GenerateStoredProcedureHeader(String strStoredProcedureName)
        {
            StringBuilder strSPHeaderBuilder = new StringBuilder();
            strSPHeaderBuilder.Append(NewLine);           
            //Generate Stored Procedure Description 
            strSPHeaderBuilder.Append(GenerateStoredProcedureDescription(strStoredProcedureName));            
            strSPHeaderBuilder.Append(NewLine);
            if (Database == MySQL)
                strSPHeaderBuilder.Append(String.Format("CREATE PROCEDURE `{0}`", strStoredProcedureName));
            else
                strSPHeaderBuilder.Append(String.Format("CREATE PROCEDURE [dbo].[{0}]", strStoredProcedureName));
            strSPHeaderBuilder.Append(NewLine);            
            return strSPHeaderBuilder.ToString();
        }

        public String DropProcedure(String strStoredProcedureName)
        {
            StringBuilder strDropBuilder = new StringBuilder();            
            strDropBuilder.Append(String.Format("IF OBJECT_ID(N'[dbo].[{0}]') IS NOT NULL", strStoredProcedureName));
            strDropBuilder.Append(NewLine + Tab + String.Format("DROP PROCEDURE [dbo].[{0}]", strStoredProcedureName));                        
            strDropBuilder.Append(NewLine);

            return strDropBuilder.ToString();
        }

        private String GenerateStoredProcedureDescription(String strStoredProcedureName)
        {
            StringBuilder strSPDescription = new StringBuilder();
            strSPDescription.Append("-- ======================================================================");
            strSPDescription.Append(NewLine);
            strSPDescription.Append("-- Generated By: GMCDotNet Studio");
            strSPDescription.Append(NewLine);
            strSPDescription.Append(String.Format("-- Procedure Name:{0}", strStoredProcedureName));
            strSPDescription.Append(NewLine);
            strSPDescription.Append(String.Format("-- Generate date:{0}", DateTime.Now.ToString("dd/MM/yyyy hh:ss")));
            strSPDescription.Append(NewLine);
            strSPDescription.Append("-- ======================================================================");
            strSPDescription.Append(NewLine);
            return strSPDescription.ToString();
        }

        private String GenerateBeginQuery()
        {
            StringBuilder strBeginQueryBuilder = new StringBuilder();
            if(Database==MySQL)
                strBeginQueryBuilder.Append(NewLine + "BEGIN" + NewLine);
            else
                strBeginQueryBuilder.Append(NewLine + "AS" + NewLine+"BEGIN"+NewLine + "SET NOCOUNT ON" + NewLine);
            return strBeginQueryBuilder.ToString();
        }

        private String GenerateAfterQuery()
        {
            StringBuilder strAfterQueryBuilder = new StringBuilder();
            strAfterQueryBuilder.Append("END" + NewLine);
            return strAfterQueryBuilder.ToString();
        }        

        #endregion
        
        private String GenerateInsertStoredProcedure(String strTableName)
        {
            StringBuilder strSPBuilder = new StringBuilder();            

            strSPBuilder.Append(GenerateStoredProcedureHeader(GenerateInsertStoredProcedureName(strTableName)));
            
            //Generate Parameters
            strSPBuilder.Append(GenerateParameters(strTableName));
            
            //Generate Before Query
            strSPBuilder.Append(GenerateBeginQuery());
            //Generate Query
            strSPBuilder.Append(GenerateInsertQuery(strTableName));

            //Generate After Query
            strSPBuilder.Append(GenerateAfterQuery());
            return strSPBuilder.ToString().Trim();
        }

        private String GenerateUpdateStoredProcedure(String strTableName)
        {
            StringBuilder strSPBuilder = new StringBuilder();

            strSPBuilder.Append(GenerateStoredProcedureHeader(GenerateUpdateStoredProcedureName(strTableName)));

            //Generate Parameters
            strSPBuilder.Append(GenerateParameters(strTableName));

            //Generate Before Query
            strSPBuilder.Append(GenerateBeginQuery());

            //Generate Query
            strSPBuilder.Append(GenerateUpdateQuery(strTableName));

            //Generate After Query
            strSPBuilder.Append(GenerateAfterQuery());

            return strSPBuilder.ToString().Trim();
        }

        private String GenerateSelectStoredProcedure(String strTableName)
        {
            StringBuilder strSPBuilder = new StringBuilder();

            strSPBuilder.Append(GenerateStoredProcedureHeader(GenerateSelectStoredProcedureName(strTableName)));

            //Generate Parameters
            strSPBuilder.Append(Tab + GeneratePrimaryColumnParameter(strTableName) + NewLine);

            //Generate Before Query
            strSPBuilder.Append(GenerateBeginQuery());

            //Generate Query
            strSPBuilder.Append(GenerateSelectQuery(strTableName));

            //Generate After Query
            strSPBuilder.Append(GenerateAfterQuery());

            return strSPBuilder.ToString().Trim();
        }

        public String GenerateSelectDeletedByIDStoredProcedure(String strTableName)
        {
            StringBuilder strSPBuilder = new StringBuilder();

            strSPBuilder.Append(GenerateStoredProcedureHeader(GenerateSelectDeletedByIDStoredProcedureName(strTableName)));

            //Generate Parameters
            strSPBuilder.Append(Tab + GeneratePrimaryColumnParameter(strTableName) + NewLine);

            //Generate Before Query
            strSPBuilder.Append(GenerateBeginQuery());

            //Generate Query
            strSPBuilder.Append(GenerateSelectDeletedByIDQuery(strTableName));

            //Generate After Query
            strSPBuilder.Append(GenerateAfterQuery());

            return strSPBuilder.ToString().Trim();
        }
        

        private String GenerateSelectByNameStoredProcedure(String strTableName)
        {
            StringBuilder strSPBuilder = new StringBuilder();
            strSPBuilder.Append(GenerateStoredProcedureHeader(GenerateSelectByNameStoredProcedureName(strTableName)));

            //Generate Parameters
            strSPBuilder.Append(Tab + GenerateNameColumnParameter(strTableName) + NewLine);

            //Generate Before Query
            strSPBuilder.Append(GenerateBeginQuery());

            //Generate Query
            strSPBuilder.Append(GenerateSelectByNameQuery(strTableName));

            //Generate After Query
            strSPBuilder.Append(GenerateAfterQuery());

            return strSPBuilder.ToString().Trim();
        }

        private String GenerateSelectByNoStoredProcedure(String strTableName)
        {
            StringBuilder strSPBuilder = new StringBuilder();
            strSPBuilder.Append(GenerateStoredProcedureHeader(GenerateSelectByNoStoredProcedureName(strTableName)));

            //Generate Parameters
            strSPBuilder.Append(Tab + GenerateColumnNoParameter(strTableName) + NewLine);

            //Generate Before Query
            strSPBuilder.Append(GenerateBeginQuery());

            //Generate Query
            strSPBuilder.Append(GenerateSelectByNoQuery(strTableName));

            //Generate After Query
            strSPBuilder.Append(GenerateAfterQuery());

            return strSPBuilder.ToString().Trim();
        }

        public String GenerateSelectAllStoredProcedure(String strTableName)
        {
            StringBuilder strSPBuilder = new StringBuilder();

            strSPBuilder.Append(GenerateStoredProcedureHeader(GenerateSelectAllStoredProcedureName(strTableName)));
            if (Database == MySQL)
                strSPBuilder.Append("()"+NewLine);
            //Generate Before Query
            strSPBuilder.Append(GenerateBeginQuery());

            //Generate Query
            strSPBuilder.Append(GenerateSelectAllQuery(strTableName));

            strSPBuilder.Append(NewLine);
            
            //Generate After Query
            strSPBuilder.Append(GenerateAfterQuery());

            return strSPBuilder.ToString().Trim();
        }

        private String GenerateSelectByForeignColumnStoredProcedure(String strTableName, String strForeignColumn)
        {
            StringBuilder strSPBuilder = new StringBuilder();
            strSPBuilder.Append(GenerateStoredProcedureHeader(GenerateSelectByForeignColumnStoredProcedureName(strTableName,strForeignColumn)));

            //Generate Parameters
            strSPBuilder.Append(Tab + GenerateParameterFromColumn(strForeignColumn, SQLConnect.GetColumnDbType(strTableName, strForeignColumn)));

            //Generate Before Query
            strSPBuilder.Append(GenerateBeginQuery());

            //Generate Query
            strSPBuilder.Append(GenerateSelectByForeignColumnQuery(strTableName,strForeignColumn));

            //Generate After Query
            strSPBuilder.Append(GenerateAfterQuery());
            return strSPBuilder.ToString().Trim();
        }

        private String GenerateAllStoredProcedureSelectByForeignColumn(String strTableName)
        {
            StringBuilder strSPBuilder = new StringBuilder();
            DataSet dsForeignColumns = SQLConnect.GetTableForeignKeys(strTableName);
            if (dsForeignColumns.Tables.Count > 0)
            {
                foreach (DataRow rowForeignColumn in dsForeignColumns.Tables[0].Rows)
                {
                    String strForeignColumn = rowForeignColumn["COLUMN_NAME"].ToString();
                    strSPBuilder.Append(GenerateSelectByForeignColumnStoredProcedure(strTableName, strForeignColumn) + NewLine);
                }
            }

            return strSPBuilder.ToString().Trim();
        }


        private String GenerateDeleteStoredProcedure(String strTableName)
        {
            StringBuilder strSPBuilder = new StringBuilder();
            strSPBuilder.Append(GenerateStoredProcedureHeader(GenerateDeleteStoredProcedureName(strTableName)));

            //Generate Parameters
            strSPBuilder.Append(GeneratePrimaryColumnParameter(strTableName));

            //Generate Before Query
            strSPBuilder.Append(GenerateBeginQuery());

            //Generate Query            
            strSPBuilder.Append(GenerateDeleteQuery(strTableName));
            //Generate After Query
            strSPBuilder.Append(GenerateAfterQuery());
            return strSPBuilder.ToString().Trim();
        }

        private String GenerateDeleteAllStoredProcedure(String strTableName)
        {
            StringBuilder strSPBuilder = new StringBuilder();
            strSPBuilder.Append(GenerateStoredProcedureHeader(GenerateDeleteAllStoredProcedureName(strTableName)));
            if (Database == MySQL)
                strSPBuilder.Append("()" + NewLine);
            //Generate Before Query
            strSPBuilder.Append(GenerateBeginQuery());

            //Generate Query
            strSPBuilder.Append(GenerateDeleteAllQuery(strTableName));

            //Generate After Query
            strSPBuilder.Append(GenerateAfterQuery());
            return strSPBuilder.ToString().Trim();
        }

        private String GenerateDeleteByForeignColumnStoredProcedure(String strTableName, String strForeignColumn)
        {
            StringBuilder strSPBuilder = new StringBuilder();
            strSPBuilder.Append(GenerateStoredProcedureHeader(GenerateDeleteByForeignColumnStoredProceduredName(strTableName, strForeignColumn)));

            //Generate Parameters
            strSPBuilder.Append(Tab + GenerateParameterFromColumn(strForeignColumn, SQLConnect.GetColumnDbType(strTableName, strForeignColumn)));

            //Generate Before Query
            strSPBuilder.Append(GenerateBeginQuery());

            //Generate Query
            strSPBuilder.Append(GenerateDeleteByForeignColumnQuery(strTableName, strForeignColumn));

            //Generate After Query
            strSPBuilder.Append(GenerateAfterQuery());

            return strSPBuilder.ToString().Trim();
        }

        private String GenerateAllStoredProcedureDeleteByForeignColumn(String strTableName)
        {
            StringBuilder strSPBuilder = new StringBuilder();
            DataSet dsForeignColumns = SQLConnect.GetTableForeignKeys(strTableName);
            if (dsForeignColumns.Tables.Count > 0)
            {
                foreach (DataRow rowForeignColumn in dsForeignColumns.Tables[0].Rows)
                {
                    String strForeignColumn = rowForeignColumn["COLUMN_NAME"].ToString();
                    strSPBuilder.Append(GenerateDeleteByForeignColumnStoredProcedure(strTableName, strForeignColumn).Trim() + NewLine);
                }
            }

            return strSPBuilder.ToString().Trim();
        }

        public String GenerateSearchStoredProcedure(String strViewName,String strTableName)
        {
            StringBuilder strSPBuilder = new StringBuilder();
            strSPBuilder.Append(GenerateStoredProcedureHeader(GenerateSearchStoredProcedureName(strTableName)));
            
            //Generate Parameters
            strSPBuilder.Append(GenerateParametersForSearch(strViewName));

            //Generate Before Query
            strSPBuilder.Append(GenerateBeginQuery());

            //Generate Query
            strSPBuilder.Append(GenerateSearchQuery(strViewName));

            //Generate After Query
            strSPBuilder.Append(GenerateAfterQuery());



            return strSPBuilder.ToString();

        }

        public String GenerateSearchStoredProcedure(String strTableName)
        {
            StringBuilder strSPBuilder = new StringBuilder();

            strSPBuilder.Append(GenerateStoredProcedureHeader(GenerateSearchStoredProcedureName(strTableName)));

            //Generate Parameters
            strSPBuilder.Append(GenerateParametersForSearch(strTableName));

            //Generate Before Query
            strSPBuilder.Append(GenerateBeginQuery());

            //Generate Query
            strSPBuilder.Append(GenerateSearchQuery(strTableName));

            //Generate After Query
            strSPBuilder.Append(GenerateAfterQuery());



            return strSPBuilder.ToString();
        }        

        #endregion

        #region Functions for Generate Drop Stored Procedure
        public String GenerateDropStoredProcedure(String strStoredProcedureName)
        {
            StringBuilder strDropBuilder= new StringBuilder();
            strDropBuilder.Append(String.Format("IF OBJECT_ID(N'[dbo].[{0}]') IS NOT NULL",strStoredProcedureName));            
            strDropBuilder.Append(NewLine);
            strDropBuilder.Append(String.Format("DROP PROCEDURE [dbo].[{0}]", strStoredProcedureName));
            return strDropBuilder.ToString();            	
        }
        #endregion

        #region Function For Execute Stored Procedure
        public String ExecuteScriptTableStoredProcedures()
        {
            StringBuilder strMessageBuilder = new StringBuilder();
            //Execute Insert Stored Procedure
            //Drop if exist
            strMessageBuilder.Append(ExecuteDropStoredProcedureScript(GenerateInsertStoredProcedureName(TableName)));
            strMessageBuilder.Append(ExecuteInsertStoredProcedureScript(TableName));

            //Execute Update Stored Procedure
            strMessageBuilder.Append(ExecuteDropStoredProcedureScript(GenerateUpdateStoredProcedureName(TableName)));
            strMessageBuilder.Append(ExecuteUpdateStoredProcedureScript(TableName));

            //Execute Select Stored Procedure
            strMessageBuilder.Append(ExecuteDropStoredProcedureScript(GenerateSelectStoredProcedureName(TableName)));
            strMessageBuilder.Append(ExecuteSelectStoredProcedureScript(TableName));

            //Execute Select Deleted By ID Stored Procedure
            strMessageBuilder.Append(ExecuteDropStoredProcedureScript(GenerateSelectDeletedByIDStoredProcedureName(TableName)));
            strMessageBuilder.Append(ExecuteSelectDeletedByIDStoredProcedureScript(TableName));

            //Execute Select By Name Stored Procedure if exist
            if (IsExistNameColumn(TableName))
            {
                strMessageBuilder.Append(ExecuteDropStoredProcedureScript(GenerateSelectByNameStoredProcedureName(TableName)));
                strMessageBuilder.Append(ExecuteSelectByNameStoredProcedureScript(TableName));
            }

            if (IsExistColumnNo(TableName))
            {
                strMessageBuilder.Append(ExecuteDropStoredProcedureScript(GenerateSelectByNoStoredProcedureName(TableName)));
                strMessageBuilder.Append(ExecuteSelectByNoStoredProcedureScript(TableName));                
            }

            //Execute Select All Stored Procedure
            strMessageBuilder.Append(ExecuteDropStoredProcedureScript(GenerateSelectAllStoredProcedureName(TableName)));
            strMessageBuilder.Append(ExecuteSelectAllStoredProcedureScript(TableName));

            //Execute All Stored Procedure Select By Foreign Column
            strMessageBuilder.Append(ExecuteAllStoredProcedureScriptSelectByForeignColumns(TableName));

            //Execute Delete Stored Procedure
            strMessageBuilder.Append(ExecuteDropStoredProcedureScript(GenerateDeleteStoredProcedureName(TableName)));
            strMessageBuilder.Append(ExecuteDeleteStoredProcedureScript(TableName));

            //Execute Delete All Stored Procedure
            strMessageBuilder.Append(ExecuteDropStoredProcedureScript(GenerateDeleteAllStoredProcedureName(TableName)));
            strMessageBuilder.Append(ExecuteDeleteAllStoredProcedureScript(TableName));

            //Execute All Stored Procedure Delete By Foreign Column
            strMessageBuilder.Append(ExecuteAllStoredProcedureDeleteByForeignColumnScript(TableName));

            String strModuleName = TableName.Substring(2, TableName.Length - 3);
            if (strModuleName.EndsWith("ie"))
                strModuleName = strModuleName.Substring(0, strModuleName.Length - 2) + "y";
            if (SQLConnect.IsExistObjectName("STModules", strModuleName))
            {                
                //Vuong add, Execute Search Stored Procedure get from Main Table,02.03.2007
                strMessageBuilder.Append(ExecuteDropStoredProcedureScript(GenerateSearchStoredProcedureName(TableName)));
                strMessageBuilder.Append(ExecutedSearchStoredProcedure(TableName));
            }


            return strMessageBuilder.ToString();
        }

        public String ExecuteDropStoredProcedureScript(String strStoredProcedureName)
        {
            StringBuilder strMessageBuilder = new StringBuilder();
            strMessageBuilder.Append(GenerateDropStoredProcedure(strStoredProcedureName) + ":" + NewLine);
            strMessageBuilder.Append(Tab + SQLConnect.ExecuteStoredProcedureScript(GenerateDropStoredProcedure(strStoredProcedureName)));
            strMessageBuilder.Append(NewLine);
            return strMessageBuilder.ToString();
        }

        private String ExecuteInsertStoredProcedureScript(String strTableName)
        {
            StringBuilder strMessageBuilder = new StringBuilder();            
            strMessageBuilder.Append(GenerateInsertStoredProcedureName(strTableName) + ":" + NewLine);
            strMessageBuilder.Append(Tab + SQLConnect.ExecuteStoredProcedureScript(GenerateInsertStoredProcedure(strTableName)));            
            strMessageBuilder.Append(NewLine);            
            return strMessageBuilder.ToString();
        }

        private String ExecuteUpdateStoredProcedureScript(String strTableName)
        {
            StringBuilder strMessageBuilder = new StringBuilder();
            strMessageBuilder.Append(GenerateUpdateStoredProcedureName(strTableName) + ":" + NewLine);
            strMessageBuilder.Append(Tab + SQLConnect.ExecuteStoredProcedureScript(GenerateUpdateStoredProcedure(strTableName)));
            strMessageBuilder.Append(NewLine);

            return strMessageBuilder.ToString();
        }

        private String ExecuteSelectStoredProcedureScript(String strTableName)
        {
            StringBuilder strMessageBuilder = new StringBuilder();
            strMessageBuilder.Append(GenerateSelectStoredProcedureName(strTableName) + ":" + NewLine);
            strMessageBuilder.Append(Tab + SQLConnect.ExecuteStoredProcedureScript(GenerateSelectStoredProcedure(strTableName)));
            strMessageBuilder.Append(NewLine);

            return strMessageBuilder.ToString();
        }


        public String ExecuteSelectDeletedByIDStoredProcedureScript(String strTableName)
        {
            StringBuilder strMessageBuilder = new StringBuilder();
            strMessageBuilder.Append(GenerateSelectDeletedByIDStoredProcedureName(strTableName) + ":" + NewLine);
            strMessageBuilder.Append(Tab + SQLConnect.ExecuteStoredProcedureScript(GenerateSelectDeletedByIDStoredProcedure(strTableName)));
            strMessageBuilder.Append(NewLine);

            return strMessageBuilder.ToString();
        }


        private String ExecuteSelectByNameStoredProcedureScript(String strTableName)
        {
            StringBuilder strMessageBuilder = new StringBuilder();
            strMessageBuilder.Append(GenerateSelectByNameStoredProcedureName(strTableName) + ":" + NewLine);
            strMessageBuilder.Append(Tab + SQLConnect.ExecuteStoredProcedureScript(GenerateSelectByNameStoredProcedure(strTableName)));
            strMessageBuilder.Append(NewLine);

            return strMessageBuilder.ToString();
        }


        private String ExecuteSelectByNoStoredProcedureScript(String strTableName)
        {
            StringBuilder strMessageBuilder = new StringBuilder();
            strMessageBuilder.Append(GenerateSelectByNoStoredProcedureName(strTableName) + ":" + NewLine);
            strMessageBuilder.Append(Tab + SQLConnect.ExecuteStoredProcedureScript(GenerateSelectByNoStoredProcedure(strTableName)));
            strMessageBuilder.Append(NewLine);

            return strMessageBuilder.ToString();           
        }
       
        public String ExecuteSelectAllStoredProcedureScript(String strTableName)
        {
            StringBuilder strMessageBuilder = new StringBuilder();
            strMessageBuilder.Append(GenerateSelectAllStoredProcedureName(strTableName) + ":" + NewLine);
            strMessageBuilder.Append(Tab + SQLConnect.ExecuteStoredProcedureScript(GenerateSelectAllStoredProcedure(strTableName)));
            strMessageBuilder.Append(NewLine);

            return strMessageBuilder.ToString();
        }

        private String ExecuteAllStoredProcedureScriptSelectByForeignColumns(String strTableName)
        {
            StringBuilder strMessageBuilder = new StringBuilder();

            DataSet dsForeignColumns = SQLConnect.GetTableForeignKeys(strTableName);
            if (dsForeignColumns.Tables.Count > 0)
            {
                foreach (DataRow rowForeignColumn in dsForeignColumns.Tables[0].Rows)
                {
                    String strForeignColumn = rowForeignColumn["COLUMN_NAME"].ToString();
                    strMessageBuilder.Append(ExecuteDropStoredProcedureScript(GenerateSelectByForeignColumnStoredProcedureName(TableName, strForeignColumn)));
                    strMessageBuilder.Append(ExecuteSelectByForeignColumnStoredProcedureScript(strTableName, strForeignColumn));                    
                }
            }
            return strMessageBuilder.ToString();
        }

        private String ExecuteSelectByForeignColumnStoredProcedureScript(String strTableName, String strForeignColumn)
        {
            StringBuilder strMessageBuilder = new StringBuilder();
            strMessageBuilder.Append(GenerateSelectByForeignColumnStoredProcedureName(strTableName,strForeignColumn) + ":" + NewLine);
            strMessageBuilder.Append(Tab + SQLConnect.ExecuteStoredProcedureScript(GenerateSelectByForeignColumnStoredProcedure(strTableName, strForeignColumn)));
            strMessageBuilder.Append(NewLine);

            return strMessageBuilder.ToString();
        }

        private String ExecuteDeleteStoredProcedureScript(String strTableName)
        {
            StringBuilder strMessageBuilder = new StringBuilder();
            strMessageBuilder.Append(GenerateDeleteStoredProcedureName(strTableName) + ":" + NewLine);
            strMessageBuilder.Append(Tab + SQLConnect.ExecuteStoredProcedureScript(GenerateDeleteStoredProcedure(strTableName)));
            strMessageBuilder.Append(NewLine);

            return strMessageBuilder.ToString();
        }

        private String ExecuteDeleteAllStoredProcedureScript(String strTableName)
        {
            StringBuilder strMessageBuilder = new StringBuilder();
            strMessageBuilder.Append(GenerateDeleteAllStoredProcedureName(strTableName) + ":" + NewLine);
            strMessageBuilder.Append(Tab + SQLConnect.ExecuteStoredProcedureScript(GenerateDeleteAllStoredProcedure(strTableName)));
            strMessageBuilder.Append(NewLine);

            return strMessageBuilder.ToString();
        }

        private String ExecuteDeleteByForeignColumnStoredProcedureScript(String strTableName, String strForeignColumn)
        {
            StringBuilder strMessageBuilder = new StringBuilder();
            strMessageBuilder.Append(GenerateDeleteByForeignColumnStoredProceduredName(strTableName, strForeignColumn) + ":" + NewLine);
            strMessageBuilder.Append(Tab + SQLConnect.ExecuteStoredProcedureScript(GenerateDeleteByForeignColumnStoredProcedure(strTableName, strForeignColumn)));
            strMessageBuilder.Append(NewLine);

            return strMessageBuilder.ToString();
        }

        private String ExecuteAllStoredProcedureDeleteByForeignColumnScript(String strTableName)
        {
            StringBuilder strMessageBuilder = new StringBuilder();

            DataSet dsForeignColumns = SQLConnect.GetTableForeignKeys(strTableName);
            if (dsForeignColumns.Tables.Count > 0)
            {
                foreach (DataRow rowForeignColumn in dsForeignColumns.Tables[0].Rows)
                {
                    String strForeignColumn = rowForeignColumn["COLUMN_NAME"].ToString();
                    strMessageBuilder.Append(ExecuteDropStoredProcedureScript(GenerateDeleteByForeignColumnStoredProceduredName(TableName, strForeignColumn)));
                    strMessageBuilder.Append(ExecuteDeleteByForeignColumnStoredProcedureScript(strTableName, strForeignColumn));
                }
            }
            return strMessageBuilder.ToString();
        }        

        private String ExecutedSearchStoredProcedure(String strViewName, String strTableName)
        {
            StringBuilder strMessageBuilder = new StringBuilder();
            strMessageBuilder.Append(GenerateSearchStoredProcedureName(strTableName) + ":" + NewLine);
            strMessageBuilder.Append(Tab + SQLConnect.ExecuteStoredProcedureScript(GenerateSearchStoredProcedure(strViewName, strTableName)));
            strMessageBuilder.Append(NewLine);

            return strMessageBuilder.ToString();
        }

        private String ExecutedSearchStoredProcedure(String strTableName)
        {
            StringBuilder strMessageBuilder = new StringBuilder();
            strMessageBuilder.Append(GenerateSearchStoredProcedureName(strTableName) + ":" + NewLine);
            strMessageBuilder.Append(Tab + SQLConnect.ExecuteStoredProcedureScript(GenerateSearchStoredProcedure(strTableName)));
            strMessageBuilder.Append(NewLine);

            return strMessageBuilder.ToString();
        }

        #endregion

        #region Functions For Generate Parameters
        private String GenerateParameters(String strTableName)
        {
            StringBuilder strParameterBuilder = new StringBuilder();

            DataSet dsColumns = SQLConnect.GetTableColumns(strTableName);
            if (Database == MySQL)
                strParameterBuilder.Append("(");
            if (dsColumns.Tables.Count > 0)
            {
                for (int i=0;i<dsColumns.Tables[0].Rows.Count;i++)
                {
                    String strColumnName = dsColumns.Tables[0].Rows[i]["COLUMN_NAME"].ToString();
                    String strParameter = Tab+ GenerateParameterFromColumn(strColumnName, GetColumnDbType(dsColumns.Tables[0].Rows[i]));
                    if (i < dsColumns.Tables[0].Rows.Count - 1)
                    {
                        strParameter += ",";
                    }
                    strParameterBuilder.Append(strParameter);
                    strParameterBuilder.Append(NewLine);
                }
            }
            if (Database == MySQL)
                strParameterBuilder.Append(")");
            return strParameterBuilder.ToString();
        }      

        private String GenerateParametersForSearch(String strTableName)
        {
            StringBuilder strParametersBuilder = new StringBuilder();

            DataSet dsColumns = SQLConnect.GetTableColumns(strTableName);

            //Init Parameter TopResults
            strParametersBuilder.Append(Tab + GenerateParameterFromColumn("TopResults", "int") + ",");
            strParametersBuilder.Append(NewLine);            
            
            if (dsColumns.Tables.Count > 0)
            {
                for (int i = 0; i < dsColumns.Tables[0].Rows.Count; i++)
                {
                    String strColumnName = dsColumns.Tables[0].Rows[i]["COLUMN_NAME"].ToString();
                    String strColumDbType = SQLConnect.GetColumnDbType(strTableName, strColumnName);
                    if (!SQLConnect.IsForeignKey(strTableName, strColumnName))
                    {
                        if (strColumDbType.Contains("varchar") || strColumDbType.Contains("nvarchar"))
                        {
                            if (strColumnName.Contains("SaveStatusInSession"))
                            {
                                strColumnName = "SaveStatusInSession";
                            }
                            String strParameter = Tab + GenerateParameterFromColumn(strColumnName, strColumDbType);
                            //if (i < dsColumns.Tables[0].Rows.Count - 1)
                            //{
                            //    strParameter += ",";
                            //}
                            strParameter += ",";
                            strParametersBuilder.Append(strParameter);
                            strParametersBuilder.Append(NewLine);
                        }
                    }
                    else
                    {
                        String strParameter = Tab + GenerateParameterFromColumn(strColumnName, strColumDbType);
                        //if (i < dsColumns.Tables[0].Rows.Count - 1)
                        //{
                        //    strParameter += ",";
                        //}
                        strParameter += ",";
                        strParametersBuilder.Append(strParameter);
                        strParametersBuilder.Append(NewLine);
                    }
                }
            }
            strParametersBuilder.Remove(strParametersBuilder.Length - 2,1);
            return strParametersBuilder.ToString();
        }

        private String GeneratePrimaryColumnParameter(String strTableName)
        {
            String strPrimaryColumn = SQLConnect.GetTablePrimaryColumn(strTableName);
            return GenerateParameterFromColumn(strPrimaryColumn, SQLConnect.GetColumnDbType(strTableName, strPrimaryColumn));
        }

        private String GenerateNameColumnParameter(String strTableName)
        {
            String strPrimaryColumn = SQLConnect.GetTablePrimaryColumn(strTableName);
            String strNameColumn = strPrimaryColumn.Substring(0, strPrimaryColumn.Length - 2) + "Name";
            return GenerateParameterFromColumn(strNameColumn, SQLConnect.GetColumnDbType(strTableName, strNameColumn));
        }

        private String GenerateColumnNoParameter(String strTableName)
        {
            String strPrimaryColumn = SQLConnect.GetTablePrimaryColumn(strTableName);
            String strNameColumn = strPrimaryColumn.Substring(0, strPrimaryColumn.Length - 2) + "No";
            return GenerateParameterFromColumn(strNameColumn, SQLConnect.GetColumnDbType(strTableName, strNameColumn));
        }
        
        private String GenerateParameterFromColumn(String strColumnName, String strColumnDbType)
        {
            if (Database == MSSQL)
                return String.Format("@{0} {1}", strColumnName, strColumnDbType);
            else if (Database == MySQL)
                return String.Format("{0} {1}", strColumnName, strColumnDbType);
            else
                return String.Format("@{0} {1}", strColumnName, strColumnDbType);
        }

        private String GetColumnDbType(DataRow rowColumn)
        {
            String strColumnDataType = rowColumn["DATA_TYPE"].ToString();
            if (strColumnDataType == "varchar" || strColumnDataType == "nvarchar")
            {
                strColumnDataType += "(" + rowColumn["CHARACTER_MAXIMUM_LENGTH"].ToString() + ")";
            }
            if (strColumnDataType == "varbinary")
            {
                strColumnDataType += "(MAX)";
            }
            return strColumnDataType;
        }
        #endregion

        #region Function For Generate Query
        private String GenerateInsertQuery(String strTableName)
        {
            StringBuilder strQueryBuilder = new StringBuilder();
            if(Database==MySQL)
                strQueryBuilder.Append(String.Format("INSERT INTO `{0}`(", strTableName) + NewLine);
            else
                strQueryBuilder.Append(String.Format("INSERT INTO [dbo].[{0}](", strTableName)+NewLine);
            strQueryBuilder.Append(GenerateColumns(strTableName)+NewLine);
            strQueryBuilder.Append(") VALUES ( " + NewLine);

            strQueryBuilder.Append(GenerateValuesForInsert(strTableName) + NewLine);
            if(Database==MySQL)
                strQueryBuilder.Append(");" + NewLine);
            else
            strQueryBuilder.Append(")" + NewLine);

            return strQueryBuilder.ToString();
        }

        private String GenerateUpdateQuery(String strTableName)
        {
            StringBuilder strUpdateQueryBuilder = new StringBuilder();
            if(Database==MySQL)
                strUpdateQueryBuilder.Append(String.Format("UPDATE `{0}` SET", strTableName) + NewLine);
            else
                strUpdateQueryBuilder.Append(String.Format("UPDATE [dbo].[{0}] SET", strTableName) + NewLine);
            DataSet dsColumns = SQLConnect.GetTableColumns(strTableName);
            if (dsColumns.Tables.Count > 0)
            {
                for (int i = 0; i < dsColumns.Tables[0].Rows.Count; i++)
                {
                    String strColumnName = dsColumns.Tables[0].Rows[i]["COLUMN_NAME"].ToString();
                    if (!SQLConnect.IsPrimaryKey(strTableName, strColumnName))
                    {
                        String strStatement = String.Empty;
                        if(Database==MySQL)
                            strStatement = String.Format("`{0}`={0}", strColumnName);
                        else
                            strStatement = String.Format("[{0}]=@{0}", strColumnName);
                        if (i < dsColumns.Tables[0].Rows.Count - 1)
                            strStatement += ",";
                        strUpdateQueryBuilder.Append(Tab + strStatement + NewLine);
                    }
                }
                strUpdateQueryBuilder.Append("WHERE "+NewLine+Tab+GenerateConditionForPrimaryColumn(strTableName));
                if (Database == MySQL)
                    strUpdateQueryBuilder.Append(";" + NewLine);
                else
                    strUpdateQueryBuilder.Append(NewLine);
               
            }
            return strUpdateQueryBuilder.ToString();
        }        

        private String GenerateColumns(String strTableName)
        {
            StringBuilder strColumnBuilder = new StringBuilder();
            DataSet dsColumns = SQLConnect.GetTableColumns(strTableName);
            if (dsColumns.Tables.Count > 0)
            {
                for (int i = 0; i < dsColumns.Tables[0].Rows.Count; i++)
                {
                   String strColumnName = dsColumns.Tables[0].Rows[i]["COLUMN_NAME"].ToString();
                    strColumnBuilder.Append(Tab);
                    if(Database==MySQL)
                        strColumnBuilder.Append(String.Format("`{0}`", strColumnName));
                    else
                        strColumnBuilder.Append(String.Format("[{0}]",strColumnName));
                   if (i < dsColumns.Tables[0].Rows.Count - 1)
                   {
                       strColumnBuilder.Append(",");                       
                   }
                   strColumnBuilder.Append(NewLine);
                }
            }            
            return strColumnBuilder.ToString();
        }

        private String GenerateValuesForInsert(String strTableName)
        {
            StringBuilder strValueBuilder = new StringBuilder();
            DataSet dsColumns = SQLConnect.GetTableColumns(strTableName);
            if (dsColumns.Tables.Count > 0)
            {
                for (int i = 0; i < dsColumns.Tables[0].Rows.Count; i++)
                {
                    String strColumnName = dsColumns.Tables[0].Rows[i]["COLUMN_NAME"].ToString();
                    strValueBuilder.Append(Tab);
                    if (Database == MySQL)
                        strValueBuilder.Append(strColumnName);
                    else
                        strValueBuilder.Append(String.Format("@{0}", strColumnName));
                    if (i < dsColumns.Tables[0].Rows.Count - 1)
                    {
                        strValueBuilder.Append(",");
                    }
                    strValueBuilder.Append(NewLine);
                }
            }
            return strValueBuilder.ToString();
        }

        private String GenerateSelectQueryHeader(String strTableName)
        {
            StringBuilder strSelectQueryHeaderBuilder = new StringBuilder();
            strSelectQueryHeaderBuilder.Append("SELECT" + NewLine);
            strSelectQueryHeaderBuilder.Append(Tab + "*" + NewLine);
            strSelectQueryHeaderBuilder.Append("FROM" + NewLine);
            if(Database==MySQL)
                strSelectQueryHeaderBuilder.Append(Tab + String.Format("`{0}`", strTableName) + NewLine);
            else
                strSelectQueryHeaderBuilder.Append(Tab + String.Format("[dbo].[{0}]", strTableName) + NewLine);
            return strSelectQueryHeaderBuilder.ToString();
        }

        private String GenerateSearchQueryHeader(String strTableName)
        {
            StringBuilder strSearchQueryHeaderBuilder = new StringBuilder();
            strSearchQueryHeaderBuilder.Append("SELECT TOP(@TopResults)" + NewLine);
            strSearchQueryHeaderBuilder.Append(Tab + "*" + NewLine);
            strSearchQueryHeaderBuilder.Append("FROM" + NewLine);
            if (Database == MySQL)
                strSearchQueryHeaderBuilder.Append(Tab + String.Format("`{0}`", strTableName) + NewLine);
            else
                strSearchQueryHeaderBuilder.Append(Tab + String.Format("[dbo].[{0}]", strTableName) + NewLine);
            return strSearchQueryHeaderBuilder.ToString();
        }

        private String GenerateSelectQuery(String strTableName)
        {
            StringBuilder strSelectQueryBuilder = new StringBuilder();
            strSelectQueryBuilder.Append(GenerateSelectQueryHeader(strTableName));
            strSelectQueryBuilder.Append("WHERE" + NewLine);
            strSelectQueryBuilder.Append(Tab + GenerateConditionForPrimaryColumn(strTableName));

            if (!strTableName.StartsWith("ST"))
            {
                strSelectQueryBuilder.Append(NewLine);
                //hieu nguyen Modify Feb 03,2009
                if (IsExistsAAStatusColumn())
                {
                    strSelectQueryBuilder.Append(Tab + "AND" + NewLine);
                    strSelectQueryBuilder.Append(Tab + GenerateSelectConditionForAAStatusColumn());
                }
            }

            if (Database == MySQL)
                strSelectQueryBuilder.Append(";" + NewLine);
            else
                strSelectQueryBuilder.Append(NewLine);

            return strSelectQueryBuilder.ToString();
        }

        private String GenerateSelectDeletedByIDQuery(String strTableName)
        {
            StringBuilder strSelectQueryBuilder = new StringBuilder();
            strSelectQueryBuilder.Append(GenerateSelectQueryHeader(strTableName));
            strSelectQueryBuilder.Append("WHERE" + NewLine);
            strSelectQueryBuilder.Append(Tab + GenerateConditionForPrimaryColumn(strTableName));
            if (!strTableName.StartsWith("ST"))
            {
                strSelectQueryBuilder.Append(NewLine);
                //hieu nguyen modify Feb 03,2009
                if (Database == MySQL)
                {
                    if (IsExistsAAStatusColumn())
                    {
                        strSelectQueryBuilder.Append(Tab + "AND" + NewLine);
                        strSelectQueryBuilder.Append(Tab + String.Format("`{0}`='Delete'", AAStatusColumn));
                    }
                }
                else
                {
                    if (IsExistsAAStatusColumn())
                    {
                        strSelectQueryBuilder.Append(Tab + "AND" + NewLine);
                        strSelectQueryBuilder.Append(Tab + String.Format("[{0}]='Delete'", AAStatusColumn));
                        
                    }
                }
            }

            if (Database == MySQL)
                strSelectQueryBuilder.Append(";" + NewLine);
            else
                strSelectQueryBuilder.Append(NewLine);

            return strSelectQueryBuilder.ToString();
        }

        private String GenerateSelectByNameQuery(String strTableName)
        {
            StringBuilder strSelectByNameQueryBuilder = new StringBuilder();
            strSelectByNameQueryBuilder.Append(GenerateSelectQueryHeader(strTableName));
            strSelectByNameQueryBuilder.Append("WHERE" + NewLine);
            strSelectByNameQueryBuilder.Append(Tab + GenerateConditionForNameColumn(strTableName));
            
            if (!strTableName.StartsWith("ST"))
            {
                strSelectByNameQueryBuilder.Append(NewLine);
                //hieu nguyen modify Feb 03,2009
                if (IsExistsAAStatusColumn())
                {
                    strSelectByNameQueryBuilder.Append(Tab + "AND" + NewLine);
                    strSelectByNameQueryBuilder.Append(Tab + GenerateSelectConditionForAAStatusColumn());
                }
            }

            if (Database == MySQL)
                strSelectByNameQueryBuilder.Append(";" + NewLine);
            else
                strSelectByNameQueryBuilder.Append(NewLine);

            return strSelectByNameQueryBuilder.ToString();
        }

        private String GenerateSelectByNoQuery(String strTableName)
        {
            StringBuilder strSelectByNameQueryBuilder = new StringBuilder();
            strSelectByNameQueryBuilder.Append(GenerateSelectQueryHeader(strTableName));
            strSelectByNameQueryBuilder.Append("WHERE" + NewLine);
            strSelectByNameQueryBuilder.Append(Tab + GenerateConditionForNoColumn(strTableName));

            if (!strTableName.StartsWith("ST"))
            {
                strSelectByNameQueryBuilder.Append(NewLine);
                 //hieu nguyen modify Feb 03,2009
                if (IsExistsAAStatusColumn())
                {
                    strSelectByNameQueryBuilder.Append(Tab + "AND" + NewLine);
                    strSelectByNameQueryBuilder.Append(Tab + GenerateSelectConditionForAAStatusColumn());
                }
            }

            if (Database == MySQL)
                strSelectByNameQueryBuilder.Append(";" + NewLine);
            else
                strSelectByNameQueryBuilder.Append(NewLine);

            return strSelectByNameQueryBuilder.ToString();
        }

       
        private String GenerateSelectAllQuery(String strTableName)
        {
            StringBuilder strSelectAllQueryBuilder = new StringBuilder();
            strSelectAllQueryBuilder.Append(GenerateSelectQueryHeader(strTableName));
            
            if (!strTableName.StartsWith("ST"))
            {
                strSelectAllQueryBuilder.Append(NewLine);
                //hieu nguyen modify Feb 03,2009
                if (IsExistsAAStatusColumn())
                {
                    strSelectAllQueryBuilder.Append("WHERE" + NewLine);
                    strSelectAllQueryBuilder.Append(GenerateSelectConditionForAAStatusColumn());
                }
            }
            //HIEU NGUYEN change not order by ,Feb 19,2009
            //if (IsExistSortOrderColumn(strTableName)!=String.Empty)
            //{
            //    strSelectAllQueryBuilder.Append(NewLine);
            //    strSelectAllQueryBuilder.Append("ORDER BY" + NewLine + Tab);
            //    strSelectAllQueryBuilder.Append(GenerateSelectConditionForSortOrder(IsExistSortOrderColumn(strTableName)));
            //}
            if (Database == MySQL)
                strSelectAllQueryBuilder.Append(";");
            return strSelectAllQueryBuilder.ToString();
        }

        private String GenerateSelectByForeignColumnQuery(String strTableName, String strForeignColumn)
        {
            StringBuilder strSelectQueryBuilder = new StringBuilder();
            strSelectQueryBuilder.Append(GenerateSelectQueryHeader(strTableName));
            strSelectQueryBuilder.Append("WHERE" + NewLine);
            strSelectQueryBuilder.Append(NewLine + GenerateConditionForColumn(strTableName, strForeignColumn));

            if (!strTableName.StartsWith("ST"))
            {
                strSelectQueryBuilder.Append(NewLine);
                //hieu nguyen modify Feb 03,2009
                if (IsExistsAAStatusColumn())
                {
                    strSelectQueryBuilder.Append(Tab + "AND" + NewLine);
                    strSelectQueryBuilder.Append(Tab + GenerateSelectConditionForAAStatusColumn());
               }

            }
            //hieu nguyen add ,select Order by,Feb 19,2009
            if (!String.IsNullOrEmpty(IsExistSortOrderColumn(strTableName)))
            {
                strSelectQueryBuilder.Append(NewLine);
                strSelectQueryBuilder.Append("ORDER BY" + NewLine + Tab);
                strSelectQueryBuilder.Append(GenerateSelectConditionForSortOrder(IsExistSortOrderColumn(strTableName)));
            }
            if (Database == MySQL)
                strSelectQueryBuilder.Append(";" + NewLine);
            else
                strSelectQueryBuilder.Append(NewLine);

            return strSelectQueryBuilder.ToString();
        }


        private String GenerateDeleteQueryHeader(String strTableName)
        {
            StringBuilder strDeleteQueryHeaderBuilder = new StringBuilder();
            String strPrimaryColumn = SQLConnect.GetTablePrimaryColumn(strTableName);
            String strStatusColumn=strPrimaryColumn.Substring(0,strPrimaryColumn.Length-2)+"StatusCombo";
            if (!strTableName.StartsWith("ST"))
            {
                if (Database == MySQL)
                {
                    //hieu nguyen modify Feb 03,2009
                    if (IsExistsAAStatusColumn())
                    {
                        strDeleteQueryHeaderBuilder.Append(String.Format("UPDATE `{0}`", strTableName) + NewLine);
                        strDeleteQueryHeaderBuilder.Append(String.Format("SET `{0}`='Delete' ", AAStatusColumn) + NewLine);
                    }
                    else
                    {
                        strDeleteQueryHeaderBuilder.Append(String.Format("DELETE FROM `{0}`", strTableName) + NewLine);
                    }
                }
                else
                {
                    //hieu nguyen modify Feb 03,2009
                    if (IsExistsAAStatusColumn())
                    {
                        strDeleteQueryHeaderBuilder.Append(String.Format("UPDATE [dbo].[{0}]", strTableName) + NewLine);
                        strDeleteQueryHeaderBuilder.Append(String.Format("SET [{0}]='Delete'", AAStatusColumn));
                        if (SQLConnect.ColumnIsExist(strTableName, AAUpdatedDateColumn))
                        {
                            strDeleteQueryHeaderBuilder.Append(String.Format(" , [{0}] = GETDATE()",AAUpdatedDateColumn)+NewLine);
                        }
                    }
                    else
                    {
                        strDeleteQueryHeaderBuilder.Append(String.Format("DELETE FROM [dbo].{0}", strTableName) + NewLine);
                    }
                }
            }
            else
            {
                if (Database == MySQL)
                {
                    //hieu nguyen modify Feb 03,2009
                    if (IsExistsAAStatusColumn())
                    {
                        strDeleteQueryHeaderBuilder.Append(String.Format("UPDATE `{0}`", strTableName) + NewLine);
                        strDeleteQueryHeaderBuilder.Append(String.Format("SET `{0}`='Delete'", AAStatusColumn) + NewLine);
                    }
                    else
                        strDeleteQueryHeaderBuilder.Append(String.Format("DELETE FROM `{0}`", strTableName) + NewLine);
                    
                }
                else
                {
                    //hieu nguyen modify Feb 03,2009
                    if (IsExistsAAStatusColumn())
                    {
                        strDeleteQueryHeaderBuilder.Append(String.Format("UPDATE [dbo].[{0}]", strTableName) + NewLine);
                        strDeleteQueryHeaderBuilder.Append(String.Format("SET [{0}]='Delete'", AAStatusColumn) + NewLine);
                    }
                    else
                        strDeleteQueryHeaderBuilder.Append(String.Format("DELETE FROM [dbo].[{0}]", strTableName) + NewLine);
                    
                }
            }
               
            
            return strDeleteQueryHeaderBuilder.ToString();
        }

        private String GenerateDeleteQuery(String strTableName)
        {
            StringBuilder strDeleteQueryBuilder = new StringBuilder();
            strDeleteQueryBuilder.Append(GenerateDeleteQueryHeader(strTableName));
            strDeleteQueryBuilder.Append("WHERE" + NewLine);
            strDeleteQueryBuilder.Append(Tab + GenerateConditionForPrimaryColumn(strTableName));

            if (Database == MySQL)
                strDeleteQueryBuilder.Append(";" + NewLine);
            else
                strDeleteQueryBuilder.Append(NewLine);

            return strDeleteQueryBuilder.ToString();
        }

        private String GenerateDeleteAllQuery(String strTableName)
        {
            StringBuilder strDeleteAllQueryBuilder = new StringBuilder();
            strDeleteAllQueryBuilder.Append(GenerateDeleteQueryHeader(strTableName));
            if (Database == MySQL)
                strDeleteAllQueryBuilder.Append(";"+NewLine);

            return strDeleteAllQueryBuilder.ToString();
        }

        private String GenerateDeleteByForeignColumnQuery(String strTableName, String strForeignColumn)
        {
            StringBuilder strDeleteQueryBuilder = new StringBuilder();
            strDeleteQueryBuilder.Append(GenerateDeleteQueryHeader(strTableName));
            strDeleteQueryBuilder.Append("WHERE" + NewLine);
            strDeleteQueryBuilder.Append(NewLine + GenerateConditionForColumn(strTableName, strForeignColumn));

            if (Database == MySQL)
                strDeleteQueryBuilder.Append(";" + NewLine);
            else
                strDeleteQueryBuilder.Append(NewLine);

            return strDeleteQueryBuilder.ToString();
        }
      

        private String GenerateSearchQuery(String strTableName)
        {
            StringBuilder strSearchQueryBuilder = new StringBuilder();
            strSearchQueryBuilder.Append(GenerateSearchQueryHeader(strTableName));
            strSearchQueryBuilder.Append("WHERE" + NewLine);
            strSearchQueryBuilder.Append(GenerateConditionsForSearch(strTableName));

            if (Database == MySQL)
                strSearchQueryBuilder.Append(";" + NewLine);
            else
                strSearchQueryBuilder.Append(NewLine);

            return strSearchQueryBuilder.ToString();
        }

        //private String GenerateSelectNewInSessionQuery(String strTableName)
        //{
        //    StringBuilder strSelectQueryBuilder = new StringBuilder();
        //    strSelectQueryBuilder.Append(GenerateSelectQueryHeader(strTableName));
        //    strSelectQueryBuilder.Append("WHERE" + NewLine);
        //    strSelectQueryBuilder.Append(String.Format("[{0}]=@UserName+'new'", SQLConnect.GetSaveStatusInSessionColumn(strTableName)));
        //    strSelectQueryBuilder.Append(NewLine);
        //    //hieu nguyen modify Feb 03,2009
        //    if (IsExistsAAStatusColumn())
        //    {
        //        strSelectQueryBuilder.Append(Tab + "AND" + NewLine);
        //        strSelectQueryBuilder.Append(Tab + GenerateSelectConditionForAAStatusColumn());
        //    }
        //    if (Database == MySQL)
        //        strSelectQueryBuilder.Append(";" + NewLine);
        //    else
        //        strSelectQueryBuilder.Append(NewLine);

        //    return strSelectQueryBuilder.ToString();
        //}

        //private String GenerateUpdateSaveStatusInSessionQuery(String strTableName)
        //{
        //   // SQLConnect dbUtil= new SQLConnect();
        //    StringBuilder strUpdateQueryBuilder = new StringBuilder();
        //    strUpdateQueryBuilder.Append(String.Format("UPDATE [dbo].[{0}]", strTableName));
        //    strUpdateQueryBuilder.Append(NewLine);
        //    strUpdateQueryBuilder.Append("SET");
        //    strUpdateQueryBuilder.Append(NewLine);
        //    strUpdateQueryBuilder.Append(Tab + String.Format("[{0}]='Done'", SQLConnect.GetSaveStatusInSessionColumn(strTableName)));
        //    strUpdateQueryBuilder.Append(NewLine);
        //    strUpdateQueryBuilder.Append("WHERE");
        //    strUpdateQueryBuilder.Append(NewLine);
        //    strUpdateQueryBuilder.Append(Tab + String.Format("[{0}]=@UserName+'new'", SQLConnect.GetSaveStatusInSessionColumn(strTableName)));
        //    strUpdateQueryBuilder.Append(NewLine);
        //     //hieu nguyen modify Feb 03,2009
        //    if (IsExistsAAStatusColumn())
        //    {
        //        strUpdateQueryBuilder.Append(Tab + "AND" + NewLine);
        //        strUpdateQueryBuilder.Append(GenerateSelectConditionForAAStatusColumn());
        //    }
        //    if (Database == MySQL)
        //        strUpdateQueryBuilder.Append(";" + NewLine);
        //    else
        //        strUpdateQueryBuilder.Append(NewLine);

        //    return strUpdateQueryBuilder.ToString();

        //}

        #endregion

        #region Functions for Generate Conditions        
        private String GenerateConditionForPrimaryColumn(String strTableName)
        {
            StringBuilder strConditionBuilder = new StringBuilder();
            String strPrimaryColumn = SQLConnect.GetTablePrimaryColumn(strTableName);
            if(Database==MySQL)
                strConditionBuilder.Append(Tab + String.Format("`{0}`={0}", strPrimaryColumn));
            else
                strConditionBuilder.Append(Tab+String.Format("[{0}]=@{0}", strPrimaryColumn));
            return strConditionBuilder.ToString();
        }

        private String GenerateConditionForNameColumn(String strTableName)
        {
            StringBuilder strConditionBuilder = new StringBuilder();
            String strPrimaryColumn = SQLConnect.GetTablePrimaryColumn(strTableName);
            String strNameColumn = strPrimaryColumn.Substring(0, strPrimaryColumn.Length - 2) + "Name";
            if (Database == MySQL)
                strConditionBuilder.Append(Tab + String.Format("`{0}`={0}", strNameColumn));
            else
                strConditionBuilder.Append(Tab + String.Format("[{0}]=@{0}", strNameColumn));

            return strConditionBuilder.ToString();
        }

        private String GenerateConditionForNoColumn(String strTableName)
        {
            StringBuilder strConditionBuilder = new StringBuilder();
            String strPrimaryColumn = SQLConnect.GetTablePrimaryColumn(strTableName);
            String strNameColumn = strPrimaryColumn.Substring(0, strPrimaryColumn.Length - 2) + "No";
            if (Database == MySQL)
                strConditionBuilder.Append(Tab + String.Format("`{0}`={0}", strNameColumn));
            else
                strConditionBuilder.Append(Tab + String.Format("[{0}]=@{0}", strNameColumn));

            return strConditionBuilder.ToString();
        }

     
        private String GenerateConditionForColumn(String strTableName, String strColumName)
        {
            StringBuilder strConditionBuilder = new StringBuilder();
            if(Database==MySQL)
                strConditionBuilder.Append(Tab + String.Format("`{0}`={0}", strColumName));
            else
                strConditionBuilder.Append(Tab+String.Format("[{0}]=@{0}", strColumName));
            return strConditionBuilder.ToString();
        }
        //hieu nguyen add GenerateCondition for sort order column
        private String GenerateSelectConditionForSortOrder(String strColumnSortOrder)
        {
            StringBuilder strConditionBuilder = new StringBuilder();
            if (Database == MySQL)
                strConditionBuilder.Append(Tab + String.Format("`{0}`", strColumnSortOrder));
            else
                strConditionBuilder.Append(Tab + String.Format("[{0}]",strColumnSortOrder));
            return strConditionBuilder.ToString();
        }
        private String GenerateSelectConditionForAAStatusColumn()
        {
            StringBuilder strConditionBuilder = new StringBuilder();
            if (Database == MySQL)            
                strConditionBuilder.Append(Tab+String.Format("`{0}`='{1}'",AAStatusColumn,DefaultStatus));            
            else
                strConditionBuilder.Append(Tab + String.Format("[{0}]='{1}'", AAStatusColumn, DefaultStatus));
            return strConditionBuilder.ToString();
        }

        private String GenerateConditionsForSearch(String strTableName)
        {
            StringBuilder strConditionBuilder = new StringBuilder();
            DataSet dsColumns = SQLConnect.GetTableColumns(strTableName);

            ////Generate Condition for AANumberInt get From AANumberIntFrom to AANumberIntTo

            //strConditionBuilder.Append(Tab + "(([AANumberInt]>=@AANumberIntFrom) AND ([AANumberInt]<=@AANumberIntTo))");
            //strConditionBuilder.Append(NewLine + Tab + "AND" + NewLine);

            if (dsColumns.Tables.Count > 0)
            {
                int itemp = 0;
                foreach (DataRow rowColumn in dsColumns.Tables[0].Rows)
                {
                   
                    String strColumnName = rowColumn["COLUMN_NAME"].ToString();
                    String strColumnDbType = SQLConnect.GetColumnDbType(strTableName, strColumnName);
                    if (!SQLConnect.IsForeignKey(strTableName, strColumnName))
                    {
                        if (strColumnDbType.Contains("varchar") || strColumnDbType.Contains("nvarchar"))
                        {
                            if(itemp!=0)
                                strConditionBuilder.Append(NewLine + Tab + "AND" + NewLine);
                            itemp++;
                            if (Database == MySQL)
                            {
                                if (SQLConnect.ColumnIsAllowNull(strTableName, strColumnName))
                                    strConditionBuilder.Append(Tab + String.Format("((`{0}` IS NULL)OR(`{0}` LIKE {0} +'%'))", strColumnName));
                                else
                                    strConditionBuilder.Append(Tab + String.Format("(`{0}` LIKE {0} +'%')", strColumnName));
                            }

                            else
                            {
                                if (SQLConnect.ColumnIsAllowNull(strTableName, strColumnName))
                                    strConditionBuilder.Append(Tab + String.Format("(([{0}] IS NULL)OR([{0}] LIKE @{0} +'%'))", strColumnName));
                                else
                                    strConditionBuilder.Append(Tab + String.Format("([{0}] LIKE @{0} +'%')", strColumnName));
                            }
                           //if(iCountRow>1)
                           //     strConditionBuilder.Append(NewLine + Tab + "AND" + NewLine);
                        }
                        
                    }
                    else
                    {
                        if (itemp != 0)
                            strConditionBuilder.Append(NewLine + Tab + "AND" + NewLine);
                        itemp++;
                         //strConditionBuilder.Append(NewLine + Tab + "AND" + NewLine);
                        strConditionBuilder.Append(Tab + String.Format("(([{0}] =@{0})OR(@{0} =0))", strColumnName));
                        
                           
                    }
                    
                }
                //hieu nguyen modify Feb 03,2009
                if (IsExistsAAStatusColumn())
                {
                     strConditionBuilder.Append(NewLine + Tab + "AND" + NewLine);
                    strConditionBuilder.Append(Tab + String.Format("([{0}]='{1}')", AAStatusColumn, DefaultStatus) + NewLine);
                }
                if (strConditionBuilder.ToString().Trim().EndsWith("AND"))
                {
                    strConditionBuilder.ToString().Trim().Substring(0, strConditionBuilder.Length - ("AND").Length - NewLine.Length);
                }
            }            
            ////hieu nguyen modify Feb 03,2009
            //if (IsExistsAAStatusColumn())
            //{
            //   // strConditionBuilder.Append(NewLine + Tab + "AND" + NewLine);
            //    strConditionBuilder.Append(Tab + String.Format("([{0}]='{1}')", AAStatusColumn, DefaultStatus) + NewLine);
            //}
            return strConditionBuilder.ToString();
        }
        #endregion

        #region Functions Generate Stored Procedure Name
        private String GenerateInsertStoredProcedureName(String strTableName)
        {
            return String.Format("{0}_Insert", strTableName);
        }

        private String GenerateUpdateStoredProcedureName(String strTableName)
        {
            return String.Format("{0}_Update", strTableName);
        }

        private String GenerateDeleteStoredProcedureName(String strTableName)
        {
            return String.Format("{0}_Delete", strTableName);
        }

        private String GenerateDeleteByForeignColumnStoredProceduredName(String strTableName,String strForeignColumnName)
        {
            return String.Format("{0}_DeleteBy{1}", strTableName,strForeignColumnName);
        }

        private String GenerateDeleteAllStoredProcedureName(String strTableName)
        {
            return String.Format("{0}_DeleteAll", strTableName);
        }

        private String GenerateSelectStoredProcedureName(String strTableName)
        {
            return String.Format("{0}_Select", strTableName);
        }

        public String GenerateSelectDeletedByIDStoredProcedureName(String strTableName)
        {
            return String.Format("{0}_SelectDeletedByID", strTableName);
        }

        private String GenerateSelectByNameStoredProcedureName(String strTableName)
        {
            return String.Format("{0}_SelectByName", strTableName);
        }

        private String GenerateSelectByNoStoredProcedureName(String strTableName)
        {
            return String.Format("{0}_SelectByNo", strTableName);
        }

        private String GenerateSelectByAANumberIntStoredProcedureName(String strTableName)
        {
            return String.Format("{0}_SelectByAANumberInt", strTableName);
        }

        public String GenerateSelectAllStoredProcedureName(String strTableName)
        {
            return String.Format("{0}_SelectAll", strTableName);
        }

        private String GenerateSelectByForeignColumnStoredProcedureName(String strTableName, String strForeignColumnName)
        {
            return String.Format("{0}_SelectBy{1}", strTableName,strForeignColumnName);
        }

        private String GenerateSearchStoredProcedureName(String strTableName)
        {
            return String.Format("{0}_Search", strTableName);
        }
        
        #endregion

        #region Public Properties
        public String TableName
        {
            get
            {
                return _tableName;
            }
            set
            {
                _tableName = value;
            }
        }
        public String ModuleName
        {
            get
            {
                return _moduleName;
            }
            set
            {
                _moduleName = value;
            }
        }
        public String Database
        {
            get
            {
                return _database;
            }
            set
            {
                _database = value;
            }
        }

        public String NewLine
        {
            get
            {
                return "\n";
            }
        }

        public String Tab
        {
            get
            {
                return "\t";
            }
        }
        #endregion

        #region --hieu nguyen add generate class object entity,Feb 12,2009--
        public String GenerateObjectEntity()
        {
            StringBuilder strClassBuilder = new StringBuilder();
            //Generate class Header
            strClassBuilder.Append(GenerateItemEntityClassHeader(TableName));
             //Generate Variable Declarations
            strClassBuilder.Append(Tab + Tab + "#region Variables"+NewLine);
            strClassBuilder.Append(GenerateEntityVariableDeclarations(TableName));
            strClassBuilder.Append(Tab + Tab + "#endregion"+NewLine+NewLine);
            //Generate Property Declarations
            strClassBuilder.Append(Tab + Tab + "#region Public properties" + NewLine);
            //hieu nguyen change ,replace false to true,April 22,2009
            //strClassBuilder.Append(GeneratePropertyDeclarations(TableName, false));
            strClassBuilder.Append(GenerateEntityPropertyDeclarations(TableName));
            strClassBuilder.Append(Tab + Tab + "#endregion" + NewLine);
            //Generate footer clase
            strClassBuilder.Append(GenerateClassFooter());

            return strClassBuilder.ToString();
            
        }
      
        #region--fuctions for Generate class entity created by April 22,2009--
        //hieu nguyen add Generate entity header class
        private String GenerateItemEntityClassHeader(String strTableName)
        {
            StringBuilder strClassHeaderBuilder = new StringBuilder();
            strClassHeaderBuilder.Append("using System;");
            strClassHeaderBuilder.Append(NewLine);
            strClassHeaderBuilder.Append("using System.Text;");
            strClassHeaderBuilder.Append(NewLine);
            strClassHeaderBuilder.Append("using System.Collections.Generic;");
            strClassHeaderBuilder.Append(NewLine);
            strClassHeaderBuilder.Append("using ExpertERP.BusinessEntities; ");
            strClassHeaderBuilder.Append(NewLine);
            strClassHeaderBuilder.Append("using ExpertERP.BaseProvider;");
            strClassHeaderBuilder.Append(NewLine);
            //get table name without prifex 
            String strNameNoPrifex = strTableName.Substring(2);
            // get name without "Items"
            String strEntityName = strNameNoPrifex.Substring(0, strNameNoPrifex.IndexOf("Items"));

            String strNamespace = String.Format("namespace ExpertERP.Modules.{0}", strEntityName);
            strClassHeaderBuilder.Append(strNamespace);
            strClassHeaderBuilder.Append(NewLine + "{" + NewLine);
            strClassHeaderBuilder.Append(Tab + String.Format("#region {0}", strNameNoPrifex) + NewLine);
            strClassHeaderBuilder.Append(Tab + "//-----------------------------------------------------------" + NewLine);
            strClassHeaderBuilder.Append(Tab + "//Generated By: Expert Studio" + NewLine);
            strClassHeaderBuilder.Append(Tab + String.Format("//Class:{0}Entity", strNameNoPrifex) + NewLine);
            strClassHeaderBuilder.Append(Tab + String.Format("//Created Date:{0}", DateTime.Today.ToLongDateString()) + NewLine);
            strClassHeaderBuilder.Append(Tab + "//-----------------------------------------------------------" + NewLine);
            strClassHeaderBuilder.Append(Tab + NewLine);
            //append Entity
            strClassHeaderBuilder.Append(Tab + String.Format("public class {0}Entity:ERPModuleItemsEntity", strNameNoPrifex) + NewLine);
            strClassHeaderBuilder.Append(Tab + "{" + NewLine);

            return strClassHeaderBuilder.ToString();

        }
        //gen 
        private String GenerateEntityVariableDeclarations(String strTableName)
        {
            StringBuilder strVariableDeclarationBuilder = new StringBuilder();
            DataSet dsTableColumns = SQLConnect.GetTableColumns(strTableName);
            if (dsTableColumns.Tables.Count > 0)
            {
                foreach (DataRow rowColumn in dsTableColumns.Tables[0].Rows)
                {
                    String strColumnName = rowColumn["COLUMN_NAME"].ToString();
                    strVariableDeclarationBuilder.Append(Tab + Tab + GenerateEntityVariableDeclaration(strTableName, strColumnName));
                    strVariableDeclarationBuilder.Append(NewLine);
                   
                }
            }

            return strVariableDeclarationBuilder.ToString();
        }
        private String GenerateEntityVariableDeclaration(String strTableName, String strColumnName)
        {
            StringBuilder strVariableBuilder = new StringBuilder();
            strVariableBuilder.Append(String.Format("protected {0} ", GetCSharpVariableType(strTableName, strColumnName)));
            String strDefaultValue = GenerateVariableDefautValue(strTableName, strColumnName, true);
            if (!String.IsNullOrEmpty(strDefaultValue))
            {
                if (strColumnName.Contains(AAStatusColumn))
                {
                    strVariableBuilder.Append(GenerateVariable(strColumnName) + "= BusinessObject." + strDefaultValue + ";");
                }
                else
                {
                    strVariableBuilder.Append(GenerateVariable(strColumnName) + "=" + strDefaultValue + ";");
                }
            }
            else
                strVariableBuilder.Append(GenerateVariable(strColumnName) + ";");
            return strVariableBuilder.ToString();
        }
        private String GenerateEntityPropertyDeclarations(String strTableName)
        {
            StringBuilder strPropertyDeclarationBuilder = new StringBuilder();
            DataSet dsTableColumns = SQLConnect.GetTableColumns(strTableName);
            if (dsTableColumns.Tables.Count > 0)
            {
                foreach (DataRow rowColumn in dsTableColumns.Tables[0].Rows)
                {
                    String strColumnName = rowColumn["COLUMN_NAME"].ToString();
                    strPropertyDeclarationBuilder.Append(GenerateEntityPropertyDeclaration(strTableName, strColumnName));
                    strPropertyDeclarationBuilder.Append(NewLine);
                 }
            }

            return strPropertyDeclarationBuilder.ToString();
        }
        private String GenerateEntityPropertyDeclaration(String strTableName, String strColumnName)
        {
            StringBuilder strPropertyBuilder = new StringBuilder();
             strPropertyBuilder.Append(Tab + Tab + String.Format("public {0} {1}", GetCSharpVariableType(strTableName, strColumnName), strColumnName));
            strPropertyBuilder.Append(NewLine);
            strPropertyBuilder.Append(Tab + Tab + "{" + NewLine);
            //Generate Get Property
            strPropertyBuilder.Append(Tab + Tab + Tab + GenerateGetPropertyDeclaration(strTableName, strColumnName, true));
            strPropertyBuilder.Append(NewLine);
            //Generate Set Property
            strPropertyBuilder.Append(Tab + Tab + Tab + GenerateSetEntityPropertyDeclaration(strTableName, strColumnName));
            strPropertyBuilder.Append(NewLine);
            strPropertyBuilder.Append(Tab + Tab + "}");

            return strPropertyBuilder.ToString();

        }
        private String GenerateSetEntityPropertyDeclaration(String strTableName, String strColumnName)
        {
            StringBuilder strSetPropertyDeclaration = new StringBuilder();
            strSetPropertyDeclaration.Append("set" + NewLine);
            strSetPropertyDeclaration.Append(Tab + Tab + Tab + "{" + NewLine);
            strSetPropertyDeclaration.Append(Tab + Tab + Tab + Tab + String.Format("if (value != this.{0})", GenerateVariable(strColumnName)));
            strSetPropertyDeclaration.Append(NewLine);
            strSetPropertyDeclaration.Append(Tab + Tab + Tab + Tab + "{" + NewLine);
            strSetPropertyDeclaration.Append(Tab + Tab + Tab + Tab + String.Format("{0}=value;", GenerateVariable(strColumnName)));
            strSetPropertyDeclaration.Append(NewLine);
            strSetPropertyDeclaration.Append(Tab + Tab + Tab + Tab + "}");
            strSetPropertyDeclaration.Append(NewLine);
            strSetPropertyDeclaration.Append(Tab + Tab + Tab + "}");

            return strSetPropertyDeclaration.ToString();
        }


        #endregion
        #endregion --end hieu nguyen--

        #region Functions For Generate Class Object Info

        public String GenerateItemEntityClass ( )
        {
            StringBuilder strClassBuilder = new StringBuilder( );
            //Generate Class Header            
            strClassBuilder.Append( GenerateItemEntityClassHeader( TableName ) );

            //Generate Default Constructor
            strClassBuilder.Append( GenerateObjectInfoClassDefaultConstructor( TableName ) );

            //Generate Variable Declarations
            strClassBuilder.Append( Tab + Tab + "#region Variables" + NewLine );
            strClassBuilder.Append( GenerateVariableDeclarations( TableName , true ) );
            strClassBuilder.Append( Tab + Tab + "#endregion" + NewLine + NewLine );

            //Generate Property Declarations
            strClassBuilder.Append( Tab + Tab + "#region Public properties" + NewLine );
            strClassBuilder.Append( GeneratePropertyDeclarations( TableName , true ) );
            strClassBuilder.Append( Tab + Tab + "#endregion" + NewLine );

            strClassBuilder.Append( GenerateClassFooter( ) );

            return strClassBuilder.ToString( );

        }

//         private String GenerateItemEntityClassHeader ( String strTableName )
//         {
//             StringBuilder strClassHeaderBuilder = new StringBuilder( );
//             strClassHeaderBuilder.Append( "using System;" );
//             strClassHeaderBuilder.Append( NewLine );
//             strClassHeaderBuilder.Append( "using System.Text;" );
//             strClassHeaderBuilder.Append( NewLine );
//             strClassHeaderBuilder.Append( "using System.Collections.Generic;" );
//             strClassHeaderBuilder.Append( NewLine );
//             if ( strTableName.StartsWith( "ST" ) )
//             {
//                 strClassHeaderBuilder.Append( "namespace ExpertLib" );
//             } else
//             {
//                 strClassHeaderBuilder.Append( "using ExpertLib;" );
//                 strClassHeaderBuilder.Append( NewLine );
//                 //hieu nguyen change namespace ExpertERP ----> ExpertERP.BusinessEntities,Feb 12,2009
//                 strClassHeaderBuilder.Append( "namespace ExpertERP.Module." +ModuleName);
//             }
//             strClassHeaderBuilder.Append( NewLine + "{" + NewLine );
//             strClassHeaderBuilder.Append( Tab + String.Format( "#region {0}" , strTableName ) + NewLine );
//             strClassHeaderBuilder.Append( Tab + "//-----------------------------------------------------------" + NewLine );
//             strClassHeaderBuilder.Append( Tab + "//Generated By: Expert Studio" + NewLine );
//             strClassHeaderBuilder.Append( Tab + String.Format( "//Class:{0}" , strTableName ) + NewLine );
//             strClassHeaderBuilder.Append( Tab + String.Format( "//Created Date:{0}" , DateTime.Today.ToLongDateString( ) ) + NewLine );
//             strClassHeaderBuilder.Append( Tab + "//-----------------------------------------------------------" + NewLine );
//             strClassHeaderBuilder.Append( Tab + NewLine );
// 
//             strClassHeaderBuilder.Append( Tab + String.Format( "public class {0}Info:BusinessObject" , strTableName ) + NewLine );
// 
//             strClassHeaderBuilder.Append( Tab + "{" + NewLine );
// 
//             return strClassHeaderBuilder.ToString( );
//         }

        public String GenerateObjectInfoClass()
        {
            StringBuilder strClassBuilder = new StringBuilder();
            //Generate Class Header            
            strClassBuilder.Append(GenerateObjectInfoClassHeader(TableName));

            //Generate Default Constructor
            strClassBuilder.Append(GenerateObjectInfoClassDefaultConstructor(TableName));

            //Generate Variable Declarations
            strClassBuilder.Append(Tab + Tab + "#region Variables"+NewLine);
            strClassBuilder.Append(GenerateVariableDeclarations(TableName,true));
            strClassBuilder.Append(Tab + Tab + "#endregion"+NewLine+NewLine);
            
            //Generate Property Declarations
            strClassBuilder.Append(Tab + Tab + "#region Public properties" + NewLine);
            strClassBuilder.Append(GeneratePropertyDeclarations(TableName,true));
            strClassBuilder.Append(Tab + Tab + "#endregion" + NewLine);

            strClassBuilder.Append(GenerateClassFooter());

            return strClassBuilder.ToString();

        }

        private String GenerateClassFooter()
        {
            StringBuilder strClassFooterBuilder = new StringBuilder();
            strClassFooterBuilder.Append(Tab+"}"+NewLine);            
            strClassFooterBuilder.Append(Tab+"#endregion"+NewLine);
            strClassFooterBuilder.Append("}");
            return strClassFooterBuilder.ToString();
        }
     
        private String GenerateObjectInfoClassHeader(String strTableName)
        {
            StringBuilder strClassHeaderBuilder = new StringBuilder();            
            strClassHeaderBuilder.Append("using System;");
            strClassHeaderBuilder.Append(NewLine);
            strClassHeaderBuilder.Append("using System.Text;");
            strClassHeaderBuilder.Append(NewLine);
            strClassHeaderBuilder.Append("using System.Collections.Generic;");
            strClassHeaderBuilder.Append(NewLine);
            if (strTableName.StartsWith("ST"))
            {
                strClassHeaderBuilder.Append("namespace ExpertLib");
            }
            else
            {
                strClassHeaderBuilder.Append("using ExpertLib;");
                strClassHeaderBuilder.Append(NewLine);
                //hieu nguyen change namespace ExpertERP ----> ExpertERP.BusinessEntities,Feb 12,2009
                strClassHeaderBuilder.Append("namespace ExpertERP.BusinessEntities");                
            }
            strClassHeaderBuilder.Append(NewLine + "{"+NewLine);
            strClassHeaderBuilder.Append(Tab + String.Format("#region {0}", strTableName)+NewLine);
            strClassHeaderBuilder.Append(Tab + "//-----------------------------------------------------------"+NewLine);
            strClassHeaderBuilder.Append(Tab + "//Generated By: Expert Studio"+NewLine);
            strClassHeaderBuilder.Append(Tab + String.Format("//Class:{0}Info",strTableName)+NewLine);
            strClassHeaderBuilder.Append(Tab + String.Format("//Created Date:{0}", DateTime.Today.ToLongDateString()) + NewLine);
            strClassHeaderBuilder.Append(Tab + "//-----------------------------------------------------------" + NewLine);
            strClassHeaderBuilder.Append(Tab + NewLine);

            strClassHeaderBuilder.Append(Tab + String.Format("public class {0}Info:BusinessObject", strTableName)+NewLine);
            
            strClassHeaderBuilder.Append(Tab + "{"+NewLine);

            return strClassHeaderBuilder.ToString();
        }

        private String GenerateObjectInfoClassDefaultConstructor(String strTableName)
        {
            StringBuilder strDefaultConstructorBuilder = new StringBuilder();
            strDefaultConstructorBuilder.Append(Tab+Tab+String.Format("public {0}Info()", strTableName)+NewLine);
            strDefaultConstructorBuilder.Append(Tab + Tab + "{" + NewLine);
            strDefaultConstructorBuilder.Append(Tab + Tab + "}" + NewLine);

            return strDefaultConstructorBuilder.ToString();
        }

        private String GenerateVariableDeclarations(String strTableName,bool isObjectInfo)
        {
            StringBuilder strVariableDeclarationBuilder = new StringBuilder();
            DataSet dsTableColumns = SQLConnect.GetTableColumns(strTableName);
            if (dsTableColumns.Tables.Count > 0)
            {
                foreach (DataRow rowColumn in dsTableColumns.Tables[0].Rows)
                {
                    String strColumnName=rowColumn["COLUMN_NAME"].ToString();                    
                    if (isObjectInfo)
                    {
                        strVariableDeclarationBuilder.Append(Tab + Tab + GenerateVariableDeclaration(strTableName, strColumnName, isObjectInfo));
                        strVariableDeclarationBuilder.Append(NewLine);
                    }
                    else
                    {
                        //String strTableOfColumn = dbUtil.GetTableNameByViewNameAndColumnName(strTableName, strColumnName);
                        String strColumnDbType = SQLConnect.GetColumnDbType(strTableName, strColumnName);
                        if (!SQLConnect.IsForeignKey(strTableName, strColumnName))
                        {
                            if ((strColumnDbType.Contains("varchar") || strColumnDbType.Contains("nvarchar")) && (!strColumnName.Contains("SaveStatusInSession")))
                            {
                                strVariableDeclarationBuilder.Append(Tab + Tab + GenerateVariableDeclaration(strTableName, strColumnName, isObjectInfo));
                                strVariableDeclarationBuilder.Append(NewLine);
                            }
                        }
                        else
                        {
                            strVariableDeclarationBuilder.Append(Tab + Tab + GenerateVariableDeclaration(strTableName, strColumnName, isObjectInfo));
                            strVariableDeclarationBuilder.Append(NewLine);
                        }
                    }
                }
            }

            return strVariableDeclarationBuilder.ToString();
        }

        private String GenerateVariableDeclaration(String strTableName, String strColumnName,bool isObjectInfo)
        {
            StringBuilder strVariableBuilder = new StringBuilder();            
            if (isObjectInfo)
            {
                strVariableBuilder.Append(String.Format("protected {0} ", GetCSharpVariableType(strTableName, strColumnName)));
            }
            else
            {
                strVariableBuilder.Append(String.Format("protected {0} ", GetSearcObjectVariableType(strTableName, strColumnName)));               
            }
            String strDefaultValue = GenerateVariableDefautValue(strTableName, strColumnName,isObjectInfo);
            if(!String.IsNullOrEmpty(strDefaultValue))
                {
                    strVariableBuilder.Append(GenerateVariable(strColumnName) + "=" + strDefaultValue + ";");
                }
            else
                strVariableBuilder.Append(GenerateVariable(strColumnName) + ";");
            return strVariableBuilder.ToString();
        }

        private String GenerateVariableDefautValue(String strTableName, String strColumnName,bool isObjectInfo)
        {
            if (isObjectInfo)
            {
                if (!SQLConnect.ColumnIsAllowNull(strTableName, strColumnName))
                {
                    if (GetCSharpVariableType(strTableName, strColumnName) == "String")
                    {
                        if (strColumnName.Contains("Status"))
                            return "DefaultStatus";
                        else if (strColumnName.Equals(AAStatusColumn))
                            return "DefaultAAStat";
                        else
                            return "String.Empty";

                    }

                    else if (GetCSharpVariableType(strTableName, strColumnName) == "DateTime")
                    {
                        if (strColumnName.Contains("StartDate"))
                            return "DefaultDate";
                        else
                            //hieu nguyen change,Datetime =now
                            //return "DateTime.MaxValue";
                            return "DateTime.Now";
                        
                    }
                    else if (GetCSharpVariableType(strTableName, strColumnName) == "bool")
                        return "true";
                }
                else
                {
                    if (GetCSharpVariableType(strTableName, strColumnName) == "String")
                    {
                        
                        if (strColumnName.Equals(AAStatusColumn))
                            return "DefaultAAStatus";
                        else if (strColumnName.Contains("Status"))
                            return "DefaultStatus";
                        else
                            return "String.Empty";
                    }

                    else if (GetCSharpVariableType(strTableName, strColumnName) == "DateTime")
                    {
                        if (strColumnName.Contains("StartDate"))
                            return "DefaultDate";
                        else
                            //hieu nguyen change ,July 29,2009
                             //return "DateTime.MaxValue";
                             return "DateTime.Now";
                    }
                    else if (GetCSharpVariableType(strTableName, strColumnName) == "bool")
                        return "true";

                }
            }
            else
            {
                if (!SQLConnect.IsForeignKey(strTableName, strColumnName))
                {
                    if (GetSearcObjectVariableType(strTableName, strColumnName) == "String")
                        return "String.Empty";
                    else
                        return "DateEdit.MinimumDateTime";
                }
            }
            return String.Empty;
        }

        private String GenerateVariable(String strColumnName)
        {
            return "_" + strColumnName.Substring(0, 1).ToLower() + strColumnName.Substring(1);
        }

        private String GeneratePropertyDeclarations(String strTableName,bool isObjectInfo)
        {
            StringBuilder strPropertyDeclarationBuilder = new StringBuilder();
            DataSet dsTableColumns = SQLConnect.GetTableColumns(strTableName);
            if (dsTableColumns.Tables.Count > 0)
            {
                foreach (DataRow rowColumn in dsTableColumns.Tables[0].Rows)
                {
                    String strColumnName = rowColumn["COLUMN_NAME"].ToString();                    
                    if (isObjectInfo)
                    {
                        strPropertyDeclarationBuilder.Append(GeneratePropertyDeclaration(strTableName, strColumnName, isObjectInfo));
                        strPropertyDeclarationBuilder.Append(NewLine);
                    }
                    else
                    {
                        //String strTableOfColumn = dbUtil.GetTableNameByViewNameAndColumnName(strTableName, strColumnName);
                        //String strColumnDbType = dbUtil.GetColumnDbType(strTableOfColumn, strColumnName);
                        String strColumnDbType = SQLConnect.GetColumnDbType(strTableName, strColumnName);
                        if (!SQLConnect.IsForeignKey(strTableName, strColumnName))
                        {
                            if ((strColumnDbType.Contains("varchar") || (strColumnDbType.Contains("nvarchar"))) && (!strColumnName.Contains("SaveStatusInSession")))
                            {
                                strPropertyDeclarationBuilder.Append(GeneratePropertyDeclaration(strTableName, strColumnName, isObjectInfo));
                                strPropertyDeclarationBuilder.Append(NewLine);
                            }
                        }
                        else
                        {
                            strPropertyDeclarationBuilder.Append(GeneratePropertyDeclaration(strTableName, strColumnName, isObjectInfo));
                            strPropertyDeclarationBuilder.Append(NewLine);
                        }
                    }
                }
            }

            return strPropertyDeclarationBuilder.ToString();
        }

        private String GeneratePropertyDeclaration(String strTableName, String strColumnName,bool isObjectInfo)
        {
            StringBuilder strPropertyBuilder = new StringBuilder();
            if (isObjectInfo)
                strPropertyBuilder.Append(Tab + Tab + String.Format("public {0} {1}", GetCSharpVariableType(strTableName, strColumnName), strColumnName));
            else
            {
                strPropertyBuilder.Append(Tab + Tab + String.Format("public {0} {1}", GetSearcObjectVariableType(strTableName, strColumnName), strColumnName));
                if (strColumnName.Contains("SaveStatusInSession"))
                    strColumnName = "SaveStatusInSession";
            }
            strPropertyBuilder.Append(NewLine);
            strPropertyBuilder.Append(Tab+Tab+"{" + NewLine);
            //Generate Get Property
            strPropertyBuilder.Append(Tab + Tab + Tab + GenerateGetPropertyDeclaration(strTableName, strColumnName,isObjectInfo));
            strPropertyBuilder.Append(NewLine);
            //Generate Set Property
            strPropertyBuilder.Append(Tab + Tab + Tab + GenerateSetPropertyDeclaration(strTableName, strColumnName,isObjectInfo));
            strPropertyBuilder.Append(NewLine);
            strPropertyBuilder.Append(Tab+Tab+"}");

            return strPropertyBuilder.ToString();

        }

        private String GenerateGetPropertyDeclaration(String strTableName, String strColumnName,bool isObjectInfo)
        {
            StringBuilder strGetPropertyDeclaration = new StringBuilder();
            strGetPropertyDeclaration.Append("get{return "+GenerateVariable(strColumnName)+";}" );
            return strGetPropertyDeclaration.ToString();
        }

        private String GenerateSetPropertyDeclaration ( String strTableName , String strColumnName , bool isObjectInfo )
        {
            StringBuilder strSetPropertyDeclaration = new StringBuilder( );
            strSetPropertyDeclaration.Append( "set" + NewLine );
            strSetPropertyDeclaration.Append( Tab + Tab + Tab + "{" + NewLine );
            strSetPropertyDeclaration.Append( Tab + Tab + Tab + Tab + String.Format( "if (value != this.{0})" , GenerateVariable( strColumnName ) ) );
            strSetPropertyDeclaration.Append( NewLine );
            strSetPropertyDeclaration.Append( Tab + Tab + Tab + Tab + "{" + NewLine );
            strSetPropertyDeclaration.Append( Tab + Tab + Tab + Tab + String.Format( "{0}=value;" , GenerateVariable( strColumnName ) ) );
            strSetPropertyDeclaration.Append( NewLine );
            if ( isObjectInfo )
            {
                strSetPropertyDeclaration.Append( Tab + Tab + Tab + Tab + "NotifyChanged(" + "\"" + strColumnName + "\"" + ");" );
                strSetPropertyDeclaration.Append( NewLine );
            }
            strSetPropertyDeclaration.Append( Tab + Tab + Tab + Tab + "}" );
            strSetPropertyDeclaration.Append( NewLine );
            strSetPropertyDeclaration.Append( Tab + Tab + Tab + "}" );

            return strSetPropertyDeclaration.ToString( );
        }

        private String GetCSharpVariableType(String strTableName, String strColumnName)
        {
            string typestr;

            String strColumnDataType = SQLConnect.GetColumnDataType(strTableName, strColumnName);
            switch ( strColumnDataType )
            {
                case "varchar":
                    typestr = "String";
                    break;
                case "nvarchar":
                    typestr = "String";
                    break;
                case "text":
                    typestr = "String";
                    break;
                case "ntext":
                    typestr = "String";
                    break;
                case "char":
                    typestr = "String";
                    break;
                case "int":
                    typestr = "int";
                    break;
                case "float":
                    typestr = "double";
                    break;
                case "real":
                    typestr = "double";
                    break;
                case "decimal":
                    typestr = "decimal";
                    break;
                case "datetime":
                    typestr = "DateTime";
                    break;
                case "bit":
                    typestr = "bool";
                    break;
                case "image":
                    typestr = "byte[]";
                    break;
                case "varbinary":
                    typestr = "byte[]";
                    break;
                default:
                    typestr = "_UNKNOWN_";
                    break;


            }

            DataSet ds = SQLConnect.GetAllowNullTableColumn( strTableName , strColumnName );

            if ( ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && typestr.Equals( "DateTime" ) )
                return string.Format( "Nullable<{0}>" , typestr );
            else
                return typestr;          
        }

        private String GetSearcObjectVariableType(String strTableName, String strColumnName)
        {
            String strTableNameOfColumn = SQLConnect.GetTableNameByViewNameAndColumnName(strTableName, strColumnName);
            String strColumnDataType = SQLConnect.GetColumnDataType(strTableNameOfColumn, strColumnName);
            if (SQLConnect.IsForeignKey(strTableName, strColumnName))
            {
                return "int";
            }
            else
            {
                if (strColumnDataType == "datetime")
                    return "DateTime";                
            }
            return "String";
        }
        #endregion

        #region Functions for generate Search Object Class
        //public String GenerateSearchObjectClass(String strModuleName,String strViewName)
        //{
        //    StringBuilder strClassBuilder = new StringBuilder();
        //    //Generate Class Header
        //    strClassBuilder.Append(GenerateSearchObjectClassHeader(strModuleName,strViewName));

        //    //Generate Variable Declarations
        //    strClassBuilder.Append(GenerateVariableDeclarations(strViewName, false));

        //    //Generate Property Declarations
        //    strClassBuilder.Append(GeneratePropertyDeclarations(strViewName, false));

        //    strClassBuilder.Append(NewLine);
        //    //Generate Class Footer
        //    strClassBuilder.Append(GenerateClassFooter());

        //    return strClassBuilder.ToString();
        //}

        public String GenerateSearchObjectClass(String strModuleName, String strTableName)
        {
            StringBuilder strClassBuilder = new StringBuilder();
            //Generate Class Header
            strClassBuilder.Append(GenerateSearchObjectClassHeader(strModuleName, strTableName));

            //Generate Variable Declarations
            strClassBuilder.Append(GenerateVariableDeclarations(strTableName, false));

            //Generate Property Declarations
            strClassBuilder.Append(GeneratePropertyDeclarations(strTableName, false));

            strClassBuilder.Append(NewLine);
            //Generate Class Footer
            strClassBuilder.Append(GenerateClassFooter());

            return strClassBuilder.ToString();
        }

        private String GenerateSearchObjectClassHeader(String strModuleName,String strTableName)
        {
            StringBuilder strClassHeaderBuilder = new StringBuilder();
            strClassHeaderBuilder.Append("using System;");
            strClassHeaderBuilder.Append(NewLine);
            strClassHeaderBuilder.Append("using System.Text;");
            strClassHeaderBuilder.Append(NewLine);
            strClassHeaderBuilder.Append("using System.Collections.Generic;");
            strClassHeaderBuilder.Append(NewLine);
            strClassHeaderBuilder.Append("");
            strClassHeaderBuilder.Append(NewLine);
            strClassHeaderBuilder.Append("namespace ExpertERP.Modules."+strModuleName);
            strClassHeaderBuilder.Append(NewLine + "{" + NewLine);
            strClassHeaderBuilder.Append(Tab + String.Format("#region {0}", strTableName) + NewLine);
            strClassHeaderBuilder.Append(Tab + "//-----------------------------------------------------------" + NewLine);
            strClassHeaderBuilder.Append(Tab + "//Generated By: Expert Studio" + NewLine);
            strClassHeaderBuilder.Append(Tab + String.Format("//Class:{0}SearchObject", strModuleName) + NewLine);
            strClassHeaderBuilder.Append(Tab + String.Format("//Created Date:{0}", DateTime.Today.ToLongDateString()) + NewLine);
            strClassHeaderBuilder.Append(Tab + "//-----------------------------------------------------------" + NewLine);
            strClassHeaderBuilder.Append(Tab + NewLine);

            strClassHeaderBuilder.Append(Tab + String.Format("public class {0}SearchObject:BaseSearchObject", strModuleName) + NewLine);

            strClassHeaderBuilder.Append(Tab + "{" + NewLine);

            return strClassHeaderBuilder.ToString();
        }

        

        #endregion

        #region Functions for Generate Class Object Controller
        public String GenerateObjectControllerClass()
        {
            StringBuilder strClassBuilder = new StringBuilder();
            //Generate Class Header            
            strClassBuilder.Append(GenerateControllerClassHeader(TableName));

            //Generate Default Constructor
            strClassBuilder.Append(GenerateControllerClassDefaultConstructor(TableName));            

            //Generate Functions
            //strClassBuilder.Append(GenerateControllerClassFunctions(TableName));

            strClassBuilder.Append(GenerateClassFooter());

            return strClassBuilder.ToString();
        }

        private String GenerateControllerClassHeader(String strTableName)
        {
            StringBuilder strClassHeaderBuilder = new StringBuilder();
            strClassHeaderBuilder.Append("using System;");
            strClassHeaderBuilder.Append(NewLine);
            strClassHeaderBuilder.Append("using System.Data;");
            strClassHeaderBuilder.Append(NewLine);
            strClassHeaderBuilder.Append("using System.Text;");
            strClassHeaderBuilder.Append(NewLine);
            strClassHeaderBuilder.Append("using System.Collections.Generic;");
            strClassHeaderBuilder.Append(NewLine);
            if (strTableName.StartsWith("ST"))
            {
                strClassHeaderBuilder.Append("namespace ExpertLib");
            }
            else
            {
                strClassHeaderBuilder.Append("using ExpertLib;");
                strClassHeaderBuilder.Append(NewLine);
                //hieu nguyen change namespace ExpertERP ------> ExpertERP.BusinessEntities ,Feb 12,2009
                strClassHeaderBuilder.Append("namespace ExpertERP.BusinessEntities");
            }
            //strClassHeaderBuilder.Append("using ExpertLib;");
            //strClassHeaderBuilder.Append(NewLine);
            //strClassHeaderBuilder.Append("");
            //strClassHeaderBuilder.Append(NewLine);
            //strClassHeaderBuilder.Append("");
            //strClassHeaderBuilder.Append(NewLine);
            //strClassHeaderBuilder.Append("namespace ExpertERP");
            strClassHeaderBuilder.Append(NewLine + "{" + NewLine);
            strClassHeaderBuilder.Append(Tab + String.Format("#region {0}", strTableName) + NewLine);
            strClassHeaderBuilder.Append(Tab + "//-----------------------------------------------------------" + NewLine);
            strClassHeaderBuilder.Append(Tab + "//Generated By: Expert Studio" + NewLine);
            strClassHeaderBuilder.Append(Tab + String.Format("//Class:{0}Controller", strTableName) + NewLine);
            strClassHeaderBuilder.Append(Tab + String.Format("//Created Date:{0}", DateTime.Today.ToLongDateString()) + NewLine);
            strClassHeaderBuilder.Append(Tab + "//-----------------------------------------------------------" + NewLine);
            strClassHeaderBuilder.Append(Tab + NewLine);

            strClassHeaderBuilder.Append(Tab + String.Format("public class {0}Controller:BaseBusinessController", strTableName) + NewLine);

            strClassHeaderBuilder.Append(Tab + "{" + NewLine);

            return strClassHeaderBuilder.ToString();
        }

        private String GenerateControllerClassDefaultConstructor(String strTableName)
        {
            StringBuilder strConstructorBuilder = new StringBuilder();
            strConstructorBuilder.Append(Tab+Tab+String.Format("public {0}Controller()",strTableName)+NewLine);
            strConstructorBuilder.Append(Tab+Tab+"{"+NewLine);
            strConstructorBuilder.Append(Tab + Tab+Tab + "dal= new DALBaseProvider(");
            strConstructorBuilder.Append("\"" + strTableName + "\"" + ",");
            strConstructorBuilder.Append(String.Format("typeof({0}Info));",strTableName));
            strConstructorBuilder.Append(NewLine);
            strConstructorBuilder.Append(Tab+Tab+"}"+NewLine);

            return strConstructorBuilder.ToString();
        }

        private String GenerateControllerClassFunctions(String strTableName)
        {
            StringBuilder strFunctionsBuilder = new StringBuilder();;
            if (SQLConnect.IsExistObjectName("STModules", strTableName.Substring(2, strTableName.Length - 3)))
            {
                strFunctionsBuilder.Append(GenerateGetObjectIDFunction(strTableName));
                strFunctionsBuilder.Append(GenerateCreateObjectFunction(strTableName));
            }


            return strFunctionsBuilder.ToString();
        }

        private String GenerateGetObjectIDFunction(String strTableName)
        {
            StringBuilder strFunctionBuilder = new StringBuilder();
            String strPrimaryColumn = SQLConnect.GetTablePrimaryColumn(strTableName);
            strFunctionBuilder.Append(Tab + Tab + String.Format("private int Get{0}(int i{0})", strPrimaryColumn));
            strFunctionBuilder.Append(NewLine);
            //Begin Function
            #region Function
            strFunctionBuilder.Append(Tab + Tab + "{");

            strFunctionBuilder.Append(NewLine);
            strFunctionBuilder.Append(Tab + Tab + Tab + String.Format("int iResult{0}=0;", strPrimaryColumn));

            strFunctionBuilder.Append(NewLine);
            strFunctionBuilder.Append(Tab + Tab + Tab + "int iMaxID=GetMaxID();");

            strFunctionBuilder.Append(NewLine);
            strFunctionBuilder.Append(Tab + Tab + Tab + "GENumberingInfo objGENumberingInfo=(GENumberingInfo)new GENumberingController().GetObjectByName(");
            strFunctionBuilder.Append("\"" + strTableName.Substring(2, strTableName.Length - 3) + "\""+");");

            #region if(objGENumberingInfo!=null)
            strFunctionBuilder.Append(NewLine);
            strFunctionBuilder.Append(Tab+Tab+Tab+"if (objGENumberingInfo!=null)");

                    
            //Begin if objGENumberingInfo!=null
            strFunctionBuilder.Append(NewLine);
            strFunctionBuilder.Append(Tab + Tab + Tab + "{");

            strFunctionBuilder.Append(NewLine);
            strFunctionBuilder.Append(Tab + Tab + Tab + Tab + "int iGENumberingStart = objGENumberingInfo.GENumberingStart;");

            #region if (objGENumberingInfo.GENumberingModus == 0)
            //Begin if objGENumberingInfo.GENumberingModus==0
            strFunctionBuilder.Append(NewLine);

            strFunctionBuilder.Append(Tab + Tab + Tab + Tab + "if (objGENumberingInfo.GENumberingModus == 0)");

            strFunctionBuilder.Append(NewLine);
            strFunctionBuilder.Append(Tab + Tab + Tab + Tab + "{");

            #region if (iGENumberingStart > iMaxID)
            strFunctionBuilder.Append(NewLine);
            strFunctionBuilder.Append(Tab + Tab + Tab + Tab + Tab + "if (iGENumberingStart > iMaxID)");

            strFunctionBuilder.Append(NewLine);
            strFunctionBuilder.Append(Tab + Tab + Tab + Tab + Tab + Tab + String.Format("iResult{0}=iGENumberingStart;", strPrimaryColumn));

            #endregion

            #region else if (iGENumberingStart > iMaxID)
            strFunctionBuilder.Append(NewLine);
            strFunctionBuilder.Append(Tab + Tab + Tab + Tab + Tab + "else");

            strFunctionBuilder.Append(NewLine);
            strFunctionBuilder.Append(Tab + Tab + Tab + Tab + Tab + "{");

            strFunctionBuilder.Append(NewLine);
            strFunctionBuilder.Append(Tab + Tab + Tab + Tab + Tab + Tab + String.Format("iResult{0}=iGENumberingStart;", strPrimaryColumn));

            strFunctionBuilder.Append(NewLine);
            strFunctionBuilder.Append(Tab + Tab + Tab + Tab + Tab + Tab + "bool isOk=false;");

            #region while (iResult{0} < iMaxID && !isOk)
            strFunctionBuilder.Append(NewLine);
            strFunctionBuilder.Append(Tab + Tab + Tab + Tab + Tab + Tab + String.Format("while (iResult{0} < iMaxID && !isOk)", strPrimaryColumn));
            
            //Begin While loop
            strFunctionBuilder.Append(NewLine);
            strFunctionBuilder.Append(Tab + Tab + Tab + Tab + Tab + Tab + "{");

            strFunctionBuilder.Append(NewLine);
            strFunctionBuilder.Append(Tab + Tab + Tab + Tab + Tab + Tab + String.Format("if (!IsExist(iResult{0}))", strPrimaryColumn));

            strFunctionBuilder.Append(NewLine);
            strFunctionBuilder.Append(Tab + Tab + Tab + Tab + Tab + Tab + Tab+"isOk=true;");

            strFunctionBuilder.Append(NewLine);
            strFunctionBuilder.Append(Tab + Tab + Tab + Tab + Tab + Tab + String.Format("iResult{0}++;", strPrimaryColumn));

            strFunctionBuilder.Append(NewLine);
            strFunctionBuilder.Append(Tab + Tab + Tab + Tab + Tab + Tab + "}");
            //End while loop
            #endregion

            strFunctionBuilder.Append(NewLine);
            strFunctionBuilder.Append(Tab + Tab + Tab + Tab + Tab + Tab + "if(isOk)");

            strFunctionBuilder.Append(NewLine);
            strFunctionBuilder.Append(Tab + Tab + Tab + Tab + Tab + Tab + Tab + String.Format("iResult{0}--;", strPrimaryColumn));

            strFunctionBuilder.Append(NewLine);
            strFunctionBuilder.Append(Tab + Tab + Tab + Tab + Tab + Tab + "else");

            strFunctionBuilder.Append(NewLine);
            strFunctionBuilder.Append(Tab + Tab + Tab + Tab + Tab + Tab + Tab + String.Format("iResult{0} = iMaxID + 1;", strPrimaryColumn));

            strFunctionBuilder.Append(NewLine);
            strFunctionBuilder.Append(Tab + Tab + Tab + Tab + Tab + "}");
            #endregion


            //End if objGENumberingInfo.GENumberingModus==0
            strFunctionBuilder.Append(NewLine);
            strFunctionBuilder.Append(Tab + Tab + Tab + Tab + "}");
            #endregion

            #region else if (objGENumberingInfo.GENumberingModus == 0)
            strFunctionBuilder.Append(NewLine);
            strFunctionBuilder.Append(Tab + Tab + Tab + Tab + "else");

            strFunctionBuilder.Append(NewLine);
            strFunctionBuilder.Append(Tab + Tab + Tab + Tab + "{");

            strFunctionBuilder.Append(NewLine);
            strFunctionBuilder.Append(Tab + Tab + Tab + Tab + Tab+String.Format("iResult{0} = i{0};",strPrimaryColumn));

            strFunctionBuilder.Append(NewLine);
            strFunctionBuilder.Append(Tab + Tab + Tab + Tab + "}");
            #endregion

            //End if objGENumberingInfo!=null
            strFunctionBuilder.Append(NewLine);
            strFunctionBuilder.Append(Tab+Tab+Tab+"}");
            #endregion

            #region else if(objGENumberingInfo!=null)
            strFunctionBuilder.Append(NewLine);
            strFunctionBuilder.Append(Tab + Tab + Tab + "else");

            strFunctionBuilder.Append(NewLine);
            strFunctionBuilder.Append(Tab + Tab + Tab + "{");

            strFunctionBuilder.Append(NewLine);
            strFunctionBuilder.Append(Tab + Tab + Tab + String.Format("iResult{0} = iMaxID + 1;",strPrimaryColumn));

            strFunctionBuilder.Append(NewLine);
            strFunctionBuilder.Append(Tab + Tab + Tab + "}");
            #endregion

            strFunctionBuilder.Append(NewLine);
            strFunctionBuilder.Append(Tab + Tab + Tab + String.Format("return iResult{0};", strPrimaryColumn));
            

            //End Function
            strFunctionBuilder.Append(NewLine);
            strFunctionBuilder.Append(Tab + Tab + "}");
            #endregion
            strFunctionBuilder.Append(NewLine);

            return strFunctionBuilder.ToString();
        }

        private String GenerateCreateObjectFunction(String strTableName)
        {
            StringBuilder strFunctionBuilder = new StringBuilder();
            String strPrimaryColumn = SQLConnect.GetTablePrimaryColumn(strTableName);
            strFunctionBuilder.Append(Tab+Tab+String.Format("public override int CreateObject(BusinessObject obj{0}Info)",strTableName));

            //Begin Function
            strFunctionBuilder.Append(Tab + Tab + NewLine);
            strFunctionBuilder.Append(Tab + Tab + "{");

            strFunctionBuilder.Append(Tab + Tab + Tab + NewLine);
            strFunctionBuilder.Append(Tab+Tab+Tab+String.Format("dal.SetValueToPrimaryColumn(obj{0}Info, Get{1}(Convert.ToInt32(dal.GetPrimaryColumnValue(obj{0}Info))));",strTableName,strPrimaryColumn));

            strFunctionBuilder.Append(Tab + Tab + Tab + NewLine);
            strFunctionBuilder.Append(Tab + Tab + Tab + String.Format("dal.SetValueToIDStringColumn(obj{0}Info, Get{1}(Convert.ToInt32(dal.GetPrimaryColumnValue(obj{0}Info))));",strTableName, strPrimaryColumn));

            strFunctionBuilder.Append(Tab + Tab + Tab + NewLine);
            strFunctionBuilder.Append(Tab + Tab + Tab + String.Format("return dal.CreateObject(obj{0}Info);", strTableName));

            //End Function
            strFunctionBuilder.Append(Tab + Tab + NewLine);
            strFunctionBuilder.Append(Tab + Tab + "}");

            strFunctionBuilder.Append(NewLine);

            return strFunctionBuilder.ToString();
        }
        #endregion

        #region Functions for Generate Module Class
        public String GenerateModuleClass()
        {
            StringBuilder strModuleClassBuilder = new StringBuilder();
            String strModuleName = TableName.Substring(2, TableName.Length - 3);

            //Generate Class Header
            strModuleClassBuilder.Append(GenerateModuleClassHeader(strModuleName));

            //Generate Constructor
            strModuleClassBuilder.Append(GenerateModuleClassConstructor(strModuleName,TableName));

            //Generate Class Footer
            strModuleClassBuilder.Append(GenerateClassFooter());

            return strModuleClassBuilder.ToString();
        }

        private String GenerateModuleClassHeader(String strModuleName)
        {
            StringBuilder strClassHeaderBuilder = new StringBuilder();
            strClassHeaderBuilder.Append("using System;");
            strClassHeaderBuilder.Append(NewLine);
            strClassHeaderBuilder.Append("using System.Text;");
            strClassHeaderBuilder.Append(NewLine);
            strClassHeaderBuilder.Append("using System.Collections;");
            strClassHeaderBuilder.Append(NewLine);
            strClassHeaderBuilder.Append("using System.Collections.Generic;");
            strClassHeaderBuilder.Append(NewLine);
            strClassHeaderBuilder.Append("using System.Data;");
            //hieu nguyen change append ExpertLib,using ExpertERP.BusinessEntities; using ExpertERP.BaseProvider;
            strClassHeaderBuilder.Append(NewLine);
            strClassHeaderBuilder.Append("");
            strClassHeaderBuilder.Append(NewLine);
            strClassHeaderBuilder.Append("using ExpertERP.BusinessEntities;");
            strClassHeaderBuilder.Append(NewLine);
            strClassHeaderBuilder.Append("using ExpertERP.BaseProvider;");
            strClassHeaderBuilder.Append(NewLine);
            strClassHeaderBuilder.Append("using ExpertLib;");
            //end
            strClassHeaderBuilder.Append(NewLine);
            strClassHeaderBuilder.Append("");
            strClassHeaderBuilder.Append(NewLine);
            strClassHeaderBuilder.Append("");
            strClassHeaderBuilder.Append(NewLine);
            strClassHeaderBuilder.Append("");

            strClassHeaderBuilder.Append(NewLine);
            strClassHeaderBuilder.Append(String.Format("namespace ExpertERP.Modules.{0}",strModuleName));
            strClassHeaderBuilder.Append(NewLine + "{" + NewLine);
            strClassHeaderBuilder.Append(Tab + String.Format("#region {0}", strModuleName+"Module") + NewLine);
            strClassHeaderBuilder.Append(Tab + "//-----------------------------------------------------------" + NewLine);
            strClassHeaderBuilder.Append(Tab + "//Generated By: Expert Studio" + NewLine);
            strClassHeaderBuilder.Append(Tab + String.Format("//Class:{0}Module", strModuleName) + NewLine);
            strClassHeaderBuilder.Append(Tab + String.Format("//Created Date:{0}", DateTime.Today.ToLongDateString()) + NewLine);
            strClassHeaderBuilder.Append(Tab + "//-----------------------------------------------------------" + NewLine);
            strClassHeaderBuilder.Append(Tab + NewLine);

            strClassHeaderBuilder.Append(Tab + String.Format("public class {0}Module:BaseModuleERP", strModuleName) + NewLine);

            strClassHeaderBuilder.Append(Tab + "{" + NewLine);

            return strClassHeaderBuilder.ToString();
        }

        private String GenerateModuleClassConstructor(String strModuleName, String strTableName)
        {
            StringBuilder strClassConstructorBuilder = new StringBuilder();
            strClassConstructorBuilder.Append(NewLine);
            strClassConstructorBuilder.Append(GenerateModuleClassDefaultConstructor(strModuleName));            

            strClassConstructorBuilder.Append(NewLine);

            return strClassConstructorBuilder.ToString();

        }

        private String GenerateModuleClassDefaultConstructor(String strModuleName)
        {
            StringBuilder strClassConstructorBuilder = new StringBuilder();
            strClassConstructorBuilder.Append(Tab + Tab + String.Format("public {0}Module()", strModuleName));

            strClassConstructorBuilder.Append(NewLine);
            strClassConstructorBuilder.Append(Tab + Tab + "{");

            strClassConstructorBuilder.Append(NewLine);
            strClassConstructorBuilder.Append(Tab + Tab + Tab + "Name=");
            strClassConstructorBuilder.Append("\"" + strModuleName + "\"" + ";");

            strClassConstructorBuilder.Append(NewLine);
            strClassConstructorBuilder.Append(Tab + Tab + Tab + "CurrentModuleEntity= new " + strModuleName + "Entities();");

            strClassConstructorBuilder.Append(NewLine);
            strClassConstructorBuilder.Append(Tab + Tab + Tab + "CurrentModuleEntity.Module=this;");

            strClassConstructorBuilder.Append(NewLine);
            strClassConstructorBuilder.Append(Tab + Tab + Tab + "InitializeModule();");

            strClassConstructorBuilder.Append(NewLine);
            strClassConstructorBuilder.Append(Tab + Tab + "}");
            strClassConstructorBuilder.Append(NewLine);

            return strClassConstructorBuilder.ToString();

        }        

        #endregion

        #region Utilities
        //Add Hieu Nguyen, Check Exists SortOrder Column
        private String IsExistSortOrderColumn(String strTableName)
        {
            String strPrimaryColumn = SQLConnect.GetTablePrimaryColumn(strTableName);
            String strNameColumn = strPrimaryColumn.Substring(0, strPrimaryColumn.Length - 2) + "SortOrder";
            if (SQLConnect.ColumnIsExist(strTableName, strNameColumn))
                return strNameColumn;
            return String.Empty;
        }

        private bool IsExistNameColumn(String strTableName)
        {
            String strPrimaryColumn = SQLConnect.GetTablePrimaryColumn(strTableName);
            String strNameColumn = strPrimaryColumn.Substring(0, strPrimaryColumn.Length - 2) + "Name";
            return SQLConnect.ColumnIsExist(strTableName, strNameColumn);
        }

        private bool IsExistColumnNo(String strTableName)
        {
            String strPrimaryColumn = SQLConnect.GetTablePrimaryColumn(strTableName);
            String strColumnNo = strPrimaryColumn.Substring(0, strPrimaryColumn.Length - 2) + "No";
            return SQLConnect.ColumnIsExist(strTableName, strColumnNo);
        }
        private bool IsExistsAAStatusColumn()
        {
            try
            {
                return SQLConnect.ColumnIsExist(TableName, AAStatusColumn); ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        #endregion


    }
}
