using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using ExpertLib;
using ExpertLib.Utilities;

namespace ConnectDatabase
{
    public class CompareController
    {
        public DataSet GetDataSet(String strQuery)
        {
            return SQLConnect.RunQuery(strQuery);
        }

        public void CreateTable(List<String> dsTables)
        {
            GMCWaitingDialog.HideDialog();
            GMCWaitingDialog.Title = "Vui lòng chờ ";
            GMCWaitingDialog.Caption = "Đang tạo table...";
            GMCWaitingDialog.Show();

            foreach (String tb in dsTables)
            {
                String str = "CREATE TABLE " + tb + "("+tb.Substring(0,tb.Length-1)+"ID INT PRIMARY KEY)";
                try
                {
                    GetDataSet(str);
                    GMCLogging.LogNewMessage("", "", "Tạo Table", "Tạo Table " + tb + " thành công.", "SUCCESS");
                }
                catch
                {
                    GMCLogging.LogNewMessage("", "", "Tạo Table", "Tạo Table " + tb + " không thành công.", "FAIL");
                    continue;
                }
            }
        }
        
        public void CreateColumn(List<String> dsTables, List<Columns> dsColumns)
        {
            GMCWaitingDialog.HideDialog();
            GMCWaitingDialog.Title = "Vui lòng chờ ";
            GMCWaitingDialog.Caption = "Đang tạo column...";
            GMCWaitingDialog.Show();

            foreach (String tb in dsTables)
            {
                String str = "ALTER TABLE [dbo].[" + tb + "] ADD \n";
                int start = 0;
                foreach (Columns cl in dsColumns)
                {
                    if (cl.Selected)
                    {
                        if (cl.Table_Name==tb && cl.Column_Name!=(tb.Substring(0,tb.Length-1)+"ID"))
                        {
                            if (start == 0)
                            {
                                str += "" + cl.Column_Name + " " + cl.Data_Type + " ";
                                start = 1;
                            }
                            else
                            {
                                str += ", \n" + cl.Column_Name + " " + cl.Data_Type + " ";
                            }
                            if (cl.Data_Type.Contains("char") || cl.Data_Type == "text")
                            {
                                str += " (" + cl.Character_Maximum_Length + ") ";
                            }
                            if (cl.Is_NullLable == "NO")
                            {
                                str += " NOT NULL";
                                start = 1;

                            }
                            else
                            {
                                str += " NULL";
                            }
                        }
                    }
                }
                try
                {
                    if (str != "ALTER TABLE [dbo].[" + tb + "] ADD \n")
                    {
                        GetDataSet(str);
                        GMCLogging.LogNewMessage("", "", "Tạo Column", "Tạo Column cho Table " + tb + " thành công.", "SUCCESS");
                    }
                }
                catch
                {
                    GMCLogging.LogNewMessage("", "", "Tạo Column", "Tạo Column cho Table " + tb + " không thành công.", "FAIL");
                    continue;
                }

            }
            GMCWaitingDialog.HideDialog();
        }

        public void CreateReference(List<References> dsReferences)
        {
            GMCWaitingDialog.HideDialog();
            GMCWaitingDialog.Title = "Vui lòng chờ ";
            GMCWaitingDialog.Caption = "Đang tạo foreign key...";
            GMCWaitingDialog.Show();
            
            foreach (References rf in dsReferences)
            {
                if (rf.Selected)
                {
                    String str = "ALTER TABLE " + rf.TableNameForeignKey + " \n";
                    str += "ADD CONSTRAINT " + rf.ForeignKey + " \n";
                    str += "FOREIGN KEY (" + rf.ColumnNameForeignKey + ") REFERENCES " + rf.TableNamePrimaryKey + " (" + rf.ColumnNamePrimaryKey + ")";
                    try
                    {
                        GetDataSet(str);
                        GMCLogging.LogNewMessage("", "", "Tạo Reference", "Tạo Reference cho Table " + rf.TableNameForeignKey + " thành công.", "SUCCESS");
                    }
                    catch
                    {
                        GMCLogging.LogNewMessage("", "", "Tạo Reference", "Tạo Reference cho Table " + rf.TableNameForeignKey + " không thành công.", "FAIL");
                        continue;
                    }
                }
            }
            GMCWaitingDialog.HideDialog();
        }

        public List<String> GetTableNotDuplicate(List<Tables> dsTables)
        {
            Dictionary<String,String> lsttb = new Dictionary<String,String>();
            foreach (Tables tb in dsTables)
            {
                if (!lsttb.ContainsKey(tb.Table_Name) && tb.Selected)
                {
                    lsttb.Add(tb.Table_Name, tb.Table_Name);
                }
            }
            List<String> dstb = new List<String>();
            foreach (String key in lsttb.Keys)
            {
                dstb.Add(key);
            }
            return dstb;
        }

        public void GenStoreTable(List<String> dsTable)
        {
            GMCWaitingDialog.HideDialog();
            GMCWaitingDialog.Title = "Gen StoreProcedure Table";
            GMCWaitingDialog.Caption = "Đang Generation ...";
            GMCWaitingDialog.Show();

            String str="";
            foreach (String tb in dsTable)
            {
                if (tb!="")
                {
                    GMCWaitingDialog.SetCaption(String.Format("Đang Generation {0}", tb));
                    try
                    {
                        GMCGeneration codeGenerator = new GMCGeneration();
                        codeGenerator.TableName = tb;
                        codeGenerator.Database = GMCGeneration.MSSQL;
                        codeGenerator.GenerateTableStoredProcedures().Trim();
                        codeGenerator.ExecuteScriptTableStoredProcedures();

                        String classInfo = codeGenerator.GenerateObjectInfoClass();
                        String classController = codeGenerator.GenerateObjectControllerClass();

                        String strPath = System.Windows.Forms.Application.StartupPath + "\\" + DateTime.Now.ToString("yyyyMMdd");
                        String strInfo = strPath + "\\Info";
                        String strController = strPath + "\\Controller";

                        if (!System.IO.Directory.Exists(strInfo))
                            System.IO.Directory.CreateDirectory(strInfo);
                        if (!System.IO.Directory.Exists(strController))
                            System.IO.Directory.CreateDirectory(strController);

                        String linkfile = String.Format(@"{0}\\" + tb + "Info.cs", strInfo);
                        System.IO.File.WriteAllText(linkfile, classInfo);

                        linkfile = String.Format(@"{0}\\" + tb + "Controller.cs", strController);
                        System.IO.File.WriteAllText(linkfile, classController);
                    }
                    catch (System.Exception ex)
                    {
                    	str += tb+"\r\n";
                    }
                }
            }
            if (str != "")
            {
                String strPath = System.Windows.Forms.Application.StartupPath + "\\" + DateTime.Now.ToString("yyyyMMdd");

                if (!System.IO.Directory.Exists(strPath))
                    System.IO.Directory.CreateDirectory(strPath);

                String linkfile = String.Format(@"{0}\\DSTableErrorGenarate.txt", strPath);
                System.IO.File.WriteAllText(linkfile, str);

                GMCLogging.LogNewMessage("", "", "Kết thúc", "Genarate Error. Check file: " + linkfile, "FAIL");
            }
            else
            {
                GMCLogging.LogNewMessage("", "", "Kết thúc", "Quá trình thực thi thành công.", "SUCCESS");
            }
            GMCWaitingDialog.HideDialog();
        }

        public void CreateStoreProcedure(Dictionary<String, String> lstStoreProcedure)
        {
            GMCWaitingDialog.HideDialog();
            GMCWaitingDialog.Title = "Vui lòng chờ ";
            GMCWaitingDialog.Caption = "Đang tạo StoreProcedure...";
            GMCWaitingDialog.Show();
            foreach (String key in lstStoreProcedure.Keys)
            {
                try
                {
                    GetDataSet(lstStoreProcedure[key]);
                    GMCLogging.LogNewMessage("", "", "Tạo StoreProcedure", "Tạo StoreProcedure " + key + " thành công.", "SUCCESS");
                }
                catch
                {
                    GMCLogging.LogNewMessage("", "", "Tạo StoreProcedure", "Tạo StoreProcedure " + key + " không thành công.", "FAIL");
                    continue;
                }
            }
            GMCWaitingDialog.HideDialog();
        }

        public DataSet GetDataFromTableName(String TableName, List<String> field)
        {
            #region ketnoi1

            String fields = "";
            bool flag = false;
            foreach (string s in field)
            {
                if (flag == true)
                    fields += string.Format(",{0}", s);
                if (flag == false)
                {
                    flag = true;
                    fields += string.Format("{0}", s);
                }
            }
            String str = string.Format("Select {0} FROM {1}", fields, TableName);
            try
            {
                DataSet ds = GetDataSet(str);
                return ds;
            }
            catch (Exception ex)
            {
                return null;
            }
            #endregion
        }
    }
}
