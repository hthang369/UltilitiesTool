using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Windows.Forms;

namespace ExpertERPProcedure.Utilities
{
    public enum AccessType
    {
        SELECT = 1,
        INSERT = 2,
        UPDATE = 3,
        DELETE = 4
    }
    public class ExcelClient
    {
        String _sourceFileName;
        public String SourceFileName
        {
            get { return _sourceFileName; }
            set 
            {
                if (_sourceFileName != value)
                {
                    isConnected = false;
                }
                _sourceFileName = value; 
            }
        }
        String _CurrentMsg;
        OleDbConnection _oleDBConn;
        OleDbDataAdapter _oleDBAdapter;
        bool isConnected = false;

        public ExcelClient(String SourceFileName)
        {
            this.SourceFileName = SourceFileName;
        }

        public bool OpenConnection()
        {
            try
            {
                if (isConnected == true)
                {
                    CloseConnection();
                }

                _oleDBConn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + this._sourceFileName + ";Extended Properties= " + "\"Excel 12.0 XML; IMEX=1;\"");
                try
                {
                    _oleDBConn.Open();
                    isConnected = true;
                }
                catch(Exception ex)
                {
                    _oleDBConn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + this._sourceFileName + @";Extended Properties= " + "\"Excel 8.0;HDR=Yes;IMEX=1\"");
                    _oleDBConn.Open();
                    isConnected = true;
                }
            }
            catch (System.Exception e)
            {
                _CurrentMsg = "Error " + e.Message;
                MessageBox.Show(e.Message, "Error Opening Source", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }

        public bool CloseConnection()
        {
            try
            {
                _oleDBConn.Close();
                _CurrentMsg = "Success : Connection Closed.";
                isConnected = false;
            }
            catch (System.Exception e)
            {
                _CurrentMsg = "Error :" + e.Message;
                MessageBox.Show(e.Message, "Error closing source", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }

        public DataTable GetDataTable(String strQuery, AccessType type)
        {

            try
            {
                DataTable dtTable = new DataTable();
                OleDbCommand command = new OleDbCommand(strQuery);

                if (isConnected == false)
                {
                    isConnected = OpenConnection();
                    if (isConnected == false)
                        return null;
                }
                command.Connection = this._oleDBConn;
                _oleDBAdapter = new OleDbDataAdapter();
                if (type == AccessType.SELECT)
                {
                    _oleDBAdapter.SelectCommand = command;
                }
                else if (type == AccessType.INSERT)
                {
                    _oleDBAdapter.InsertCommand = command;
                }
                else if (type == AccessType.UPDATE)
                {
                    _oleDBAdapter.UpdateCommand = command;
                }
                else if (type == AccessType.DELETE)
                {
                    _oleDBAdapter.DeleteCommand = command;
                }

                _oleDBAdapter.Fill(dtTable);
                return dtTable;
            }
            catch (System.Exception e)
            {
                this._CurrentMsg = "ERROR " + e.Message;
                MessageBox.Show(e.Message, "Error Reading Source", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public DataTable GetAllDataFromSheet(String SheetName)
        {
            DataTable dtTable = new DataTable();

            OleDbCommand command = new OleDbCommand("SELECT * FROM [" + SheetName + "]");

            try
            {
                if (isConnected == false)
                {
                    isConnected = OpenConnection();
                }
                if (isConnected == false)
                return null;

                command.Connection = this._oleDBConn;
                _oleDBAdapter = new OleDbDataAdapter();
                _oleDBAdapter.SelectCommand = command;
                _oleDBAdapter.Fill(dtTable);
            }
            catch (System.Exception e)
            {
                return null;
            }
            return dtTable;

        }

        public bool RunQuery(String strQuery)
        {
            try
            {
                OleDbCommand nonQueryCommand = new OleDbCommand(strQuery);
                nonQueryCommand.Connection = this._oleDBConn;
                nonQueryCommand.CommandText = strQuery;

                int rowsAffected = nonQueryCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool InserValue(String SheetName, object[] arrValue)
        {
            String strQuery = BuildInsertQuery(SheetName, arrValue);
            return RunQuery(strQuery);
        }

        public String BuildInsertQuery(String SheetName, object[] arrValue)
        {
            String strQuery = String.Empty;
            StringBuilder strBuild = new StringBuilder(String.Format("INSERT INTO [{0}] + VALUES("));

            strBuild.Append("'" + arrValue[0].ToString() + "'");
            for (int i = 1; i < arrValue.Length; i++)
            {
                strBuild.Append(",");
                strBuild.Append("'" + arrValue[i].ToString() + "'");
            }

            return strQuery;
        }

        public bool PopulateData(String SheetName, object[][] DataValue)
        {

            return true;
        }

        public String[] GetSheetName()
        {
            DataTable dtSheetnames = _oleDBConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

            if (dtSheetnames == null)
                return null;

            String[] ExcelSheets = new String[dtSheetnames.Rows.Count];

            int i = 0;
            foreach (DataRow row in dtSheetnames.Rows)
            {
                ExcelSheets[i] = row["TABLE_NAME"].ToString();
                i++;
            }

            return ExcelSheets;
        }

    }
}
