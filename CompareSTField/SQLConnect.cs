using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CreateCompany;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.Repository;
using System.Data.SqlClient;

namespace CreateCompany
{	
	public static class SQLConnect
	{
        public static SqlConnection dbConn = null;
        
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
        
        public static String GetStringConnect(String strServer, String strDatabase, String strUsername, String strPassword)
        {
            return String.Format(@"Server={0};Database={1};User Id={2};Password={3};"
                                 , strServer, strDatabase, strUsername, strPassword);
        }

        public static SqlCommand GetQuery(string strQueryCommand)
        {
            try
            {
                return new SqlCommand(strQueryCommand, dbConn);// { CommandType = CommandType.TableDirect };
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
                adapter.SelectCommand = new SqlCommand(cmdText, dbConn);// { CommandType = CommandType.TableDirect };
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

            // Add the input parameter and set value
            for (int iIndex = 1; iIndex < command.Parameters.Count; iIndex++)
            {
                if (values.Length >= iIndex)
                {
                    command.Parameters[iIndex].Value = values[iIndex - 1];
                }
            }

            // Open the connection and execute the reader.
            try
            {
                DataSet ds = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
	}
}