using DevExpress.Xpf.Core;
using SQLAppLib;
using SQLTool.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace SQLTool.Util
{
    public class FunctionList
    {
        public static string section = "SqlServer";
        private static string serverCnt = "ServerCount";
        private static string _serverDesc = "Descreiption";
        private static string _serverName = "Server";
        private static string _serverUID = "UID";
        private static string _serverPWD = "PWD";
        private static string _serverDBOld = "DB_Old";
        private static string strFileName = System.Windows.Forms.Application.StartupPath + "\\config.ini";
        private static string strServer;
        private static string strDatabase;
        private static string strUserName;
        private static string strPassWord;
        private static string strDBOld;
        public static string strPath = System.Windows.Forms.Application.StartupPath + "\\Scripts\\";
        public static string strFileCfgScript = strPath + "scripts.ini";
        public static string strCfgScriptName = strPath + "config.ini";
        public static Dictionary<string, string> lstFuncLst;
        public static string strDynPara = "DynPara";
        private static ViewModels.ResultViewModel popupView;
        private static ViewModels.CompareResultViewModel comparePopupView;
        private static Views.BasePopupWindow popupWindow;
        private static string _strThemeApp = "ThemeApp";
        private static string _strThemeName = "ThemeName";
        private static string _strFontApp = "FontApp";
        private static string _ctrlFrom = "ctrlFrom";
        private static string _ctrlTo = "ctrlTo";
        private static MainWindow mainWindow;

        public static void SetWindowParent(Window frm)
        {
            mainWindow = frm as MainWindow;
        }

        public static string GetSourceCodePath()
        {
            return SQLApp.GetIniFile(strFileName, "SourceCode", "SourceUrl");
        }

        public static bool CheckSelectedDB()
        {
            if (mainWindow != null && string.IsNullOrEmpty(Convert.ToString(mainWindow.ctrlFrom.cboDatabase.SelectedItem)))
            {
                ShowMessenge("Vui lòng chọn DB", "Thông báo", MessageBoxImage.Warning);
                return false;
            }
            return true;
        }

        #region Theme
        public static string GetThemeName()
        {
            string _themeName = SQLApp.GetIniFile(strFileName, _strThemeApp, _strThemeName);
            if (string.IsNullOrEmpty(_themeName)) _themeName = Properties.Resources.ThemeName;
            return _themeName;
        }

        public static void SetThemeName(string themeName)
        {
            SQLApp.SetIniFile(strFileName, _strThemeApp, _strThemeName, themeName);
        }

        public static void ShowChangeTheme()
        {
            string _themeName = ApplicationThemeHelper.ApplicationThemeName;
            if (PromptForm.ShowCombobox("Change Theme", "Theme Name", Theme.Themes.Select(x => x.Name).ToArray(), ref _themeName) == MessageBoxResult.Cancel)
                return;

            SetThemeName(_themeName);


            //System.Diagnostics.Process.Start(System.Windows.Forms.Application.ExecutablePath);
            //Environment.Exit(Environment.ExitCode);

            ApplicationThemeHelper.ApplicationThemeName = _themeName;
        }
        #endregion

        #region CallMethod
        public static void CallMethodName(string strFuncName, Window frmParent)
        {
            MethodInfo method = method = typeof(FunctionList).GetMethod(strFuncName, (((BindingFlags)BindingFlags.Public) | ((BindingFlags)BindingFlags.Static)));
            if (method != null)
            {
                if (method.GetParameters().Length == 0)
                    method.Invoke(null, (object[])null);
                else
                    method.Invoke(null, new Window[] { frmParent });
            }
        }
        public static void LoadQueryPath(FunctionListObject obj, Window frmParent)
        {
            string strQuery = SQLApp.GetFile(obj.Path);
            strQuery = GenerateScriptWithParameters(obj, strQuery, frmParent);
            if (string.IsNullOrEmpty(strQuery)) return;
            DataTable dt = SQLDBUtil.GetDataTable(strQuery);
            if (dt == null) return;
            ShowResultData(frmParent, dt, strQuery);
        }
        public static void ShowFunctionList(string strFuncName, Window frmParent)
        {
            string sourceUrl = SQLApp.GetIniFile(strFileName, "SourceCode", "SourceUrl");
            bool isReturn = false;
            if(string.IsNullOrEmpty(sourceUrl) || !Directory.Exists(sourceUrl))
            {
                isReturn = true;
                FolderBrowserDialog folder = new FolderBrowserDialog();
                if (folder.ShowDialog() == DialogResult.OK)
                {
                    SQLApp.SetIniFile(strFileName, "SourceCode", "SourceUrl", folder.SelectedPath);
                    isReturn = false;
                }
            }
            if (isReturn) return;
            List<string> lstFuncs = SQLApp.GetKeysIniFile(strCfgScriptName, strFuncName, 3000);
            List<FunctionListObject> lstObjectFuncs = new List<FunctionListObject>();
            lstFuncs.ForEach(x =>
            {
                string caption = SQLApp.GetIniFile(strCfgScriptName, "Captions", x);
                if (string.IsNullOrEmpty(caption)) caption = string.Join(" ", x.ToUpper().Split('_'));
                lstObjectFuncs.Add(new FunctionListObject { Name = x, Text = caption});
            });
            PromptForm._frmParent = frmParent;
            string value = string.Empty;
            MessageBoxResult messageResult = PromptForm.ShowCombobox("Function List In Source", "Function Name", lstObjectFuncs.Select(x => x.Text).ToArray(), ref value);
            if(messageResult == MessageBoxResult.OK)
            {
                FunctionListObject functionObj = lstObjectFuncs.Find(x => x.Text.Equals(value));
                string strKey = (functionObj != null) ? functionObj.Name : string.Empty;
                string functionName = SQLApp.GetIniFile(strCfgScriptName, strFuncName, strKey);
                if (functionName.StartsWith("Cmd"))
                {
                    functionObj.FuncName = functionName;
                    ExecutedScriptCommand(functionObj, frmParent);
                }
                else
                {
                    CallMethodName(functionName, frmParent);
                }
            }
        }
        private static string GenerateScriptWithParameters(FunctionListObject functionObj, string strScript, Window frmParent)
        {
            string strCnt = SQLApp.GetIniFile(strFileCfgScript, strDynPara, string.Concat(functionObj.Name, "Cnt"));
            int iCnt = string.IsNullOrEmpty(strCnt) ? 0 : Convert.ToInt32(strCnt);
            if (iCnt > 0)
            {
                PromptForm._frmParent = frmParent;
                string strValue = "";
                for (int i = 1; i <= iCnt; i++)
                {
                    string param = Convert.ToString(SQLApp.GetIniFile(strFileCfgScript, strDynPara, string.Concat(functionObj.Name, "Name", i)));
                    strValue = Convert.ToString(SQLApp.GetIniFile(strFileCfgScript, strDynPara, string.Concat(functionObj.Name, "Val", i)));
                    string strValList = Convert.ToString(SQLApp.GetIniFile(strFileCfgScript, strDynPara, string.Concat(functionObj.Name, "ValList", i)));
                    string strListFolder = Convert.ToString(SQLApp.GetIniFile(strFileCfgScript, strDynPara, string.Concat(functionObj.Name, "ListFolder", i)));
                    MessageBoxResult result;
                    if (!string.IsNullOrEmpty(strListFolder))
                    {
                        string sourceUrl = SQLApp.GetIniFile(strFileName, "SourceCode", "SourceUrl");
                        string[] lstModules = Directory.GetDirectories(string.Concat(sourceUrl,"\\",strListFolder));
                        string[] lstModuleNames = lstModules.ToList().Select(x => new DirectoryInfo(x).Name).ToArray();
                        result = PromptForm.ShowCombobox("Dynamic parameter for script: " + functionObj.Text, param, lstModuleNames, ref strValue);
                    }
                    else if (!string.IsNullOrEmpty(strValList))
                        result = PromptForm.ShowCombobox("Dynamic parameter for script: " + functionObj.Text, param, strValList.Split('|'), ref strValue);
                    else
                        result = PromptForm.ShowText("Dynamic parameter for script: " + functionObj.Text, param, ref strValue);
                    if (result == MessageBoxResult.Cancel)
                    {
                        return (string.IsNullOrEmpty(param)) ? strScript : string.Empty;
                    }
                    if(!string.IsNullOrEmpty(param))
                        strScript = strScript.Replace(param, strValue);
                    if(!string.IsNullOrEmpty(strValList) && !string.IsNullOrEmpty(strValue))
                        strScript = string.Concat(strScript, " ", strValue);
                }
            }
            return strScript;
        }
        private static ViewModels.ResultViewModel GetResultPopupView()
        {
            if (popupView == null)
                popupView = new ResultViewModel();
            return popupView;
        }
        private static Views.BasePopupWindow GetPopupWindow()
        {
            if (popupWindow == null)
                popupWindow = new Views.BasePopupWindow();
            return popupWindow;
        }
        private static ViewModels.CompareResultViewModel GetCompareResultPopupView()
        {
            if (comparePopupView == null)
                comparePopupView = new CompareResultViewModel();
            return comparePopupView;
        }
        private static void ShowResultData(Window frmParent, DataTable dtSource, string strQuery = "")
        {
            if (dtSource != null)
            {
                ViewModels.ResultViewModel popupView = GetResultPopupView();
                popupView.Title = "T-SQL";
                popupView.Header = "T-SQL Result";
                Task.Factory.StartNew(() =>
                {
                    return dtSource;
                }).ContinueWith(r => AddControlsToGrid(popupView, r.Result, strQuery), TaskScheduler.FromCurrentSynchronizationContext());
                ShowPopupViewModal(popupView, new Views.ResultView());
            }
            else if (!string.IsNullOrEmpty(strQuery))
            {
                ShowResultDataView(strQuery);
            }
        }
        public static void ShowResultDataView(string strQuery)
        {
            ViewModels.ResultViewModel popupView = GetResultPopupView();
            popupView.Title = "T-SQL";
            popupView.Header = "T-SQL Result";

            Task.Factory.StartNew(() =>
            {
                return SQLAppLib.SQLDBUtil.GetDataTable(strQuery);
            }).ContinueWith(r => AddControlsToGrid(popupView, r.Result, strQuery), TaskScheduler.FromCurrentSynchronizationContext());
            ShowPopupViewModal(popupView, new Views.ResultView());
        }
        private static void AddControlsToGrid(BasePopupViewModel viewModel, DataTable dtSource, string strQuery)
        {
            if (viewModel is ResultViewModel)
            {
                ResultViewModel result = (viewModel as ResultViewModel);
                if (result.lstTabItems == null)
                    result.lstTabItems = new System.Collections.ObjectModel.ObservableCollection<DXTabItem>();
                if (result.DataResults == null)
                    result.DataResults = new Dictionary<string, string>();
                DXTabItem tabItem = new DXTabItem();
                tabItem.FontSize = Convert.ToDouble(SQLApp.GetIniFile(strFileName, _strFontApp, "FontSize"));
                BaseGridControl gridControl = new BaseGridControl();
                BaseTableView tableView = new BaseTableView();
                gridControl.View = tableView;
                gridControl.ItemsSource = dtSource;
                tabItem.Header = dtSource.TableName;
                tabItem.Content = gridControl;
                result.lstTabItems.Add(tabItem);
                result.DataResults.Add(dtSource.TableName, strQuery);
            }
        }
        private static void ShowPopupViewModal(BasePopupViewModel viewModel, System.Windows.Controls.UserControl view)
        {
            Views.BasePopupWindow popupWindow = GetPopupWindow();
            popupWindow.DataContext = viewModel;
            viewModel.isNoTabControl = Visibility.Visible;
            viewModel.isTabControl = Visibility.Hidden;
            popupWindow.waitLoadView.LoadingChild = view;
            popupWindow.Closed += PopupWindow_Closed;
            //(view as Views.ResultView).tabControl.KeyUp += TabControl_KeyUp;
            //ICommand commandKey = new RelayCommand<object>((x) => true, (x) => KeyBindingActionCommand(x));
            //InputBinding input = new KeyBinding(commandKey, Key.V, ModifierKeys.Alt);
            //input.CommandParameter = "Alt+V";
            //(view as Views.ResultView).tabControl.InputBindings.Add(input);
            if (view is Views.ResultView)
            {
                (view as Views.ResultView).tabControl.TabContentCacheMode = DevExpress.Xpf.Core.TabContentCacheMode.None;
                (view as Views.ResultView).tabControl.TabRemoved += TabControl_TabRemoved;
                (view as Views.ResultView).tabControl.TabIndex = (view as Views.ResultView).tabControl.Items.Count - 1;
            }
            popupWindow.Show();
        }
        private static void ShowCompareResultView(DataTable dtSource)
        {
            if (dtSource != null)
            {
                ViewModels.CompareResultViewModel popupView = GetCompareResultPopupView();
                popupView.Title = "Compare";
                popupView.Header = "Compare Result";
                Task.Factory.StartNew(() =>
                {
                    return dtSource;
                }).ContinueWith(r => AddControlsToGrid(popupView, r.Result, ""), TaskScheduler.FromCurrentSynchronizationContext());
                ShowPopupViewModal(popupView, new Views.CompareResult());
            }
        }
        private static void PopupWindow_Closed(object sender, EventArgs e)
        {
            popupWindow = null;
            popupView = null;
        }

        private static void TabControl_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(e.SystemKey == Key.LeftAlt || e.SystemKey == Key.RightAlt)
            {
                if(e.Key == Key.V)
                {
                    DevExpress.Xpf.Core.DXTabItem tabItem = ((sender as DevExpress.Xpf.Core.DXTabControl).SelectedTabItem as DevExpress.Xpf.Core.DXTabItem);

                }
            }
        }

        private static void TabControl_TabRemoved(object sender, DevExpress.Xpf.Core.TabControlTabRemovedEventArgs e)
        {
            if ((sender as DevExpress.Xpf.Core.DXTabControl).Items.Count == 0)
            {
                popupWindow.Close();
                popupWindow = null;
            }
        }
        #endregion

        #region function list
        public static void ChangeDbLaravel(Window frmParent)
        {
            if (!CheckSelectedDB()) return;
            
            string sourceUrl = GetSourceCodePath();
            string filePath = Directory.GetFiles(sourceUrl, "*.env").FirstOrDefault();
            string[] lines = File.ReadAllLines(filePath);
            string[] lstDBs = SQLDBUtil.GetDataTableByDataSet(SQLDBUtil.GetAllDatabases()).Select().Select(x => x[0].ToString()).ToArray();
            string DBVal = lines.ToList().Find(x => x.StartsWith("DB_DATABASE")).Split('=').LastOrDefault();
            if (PromptForm.ShowCombobox("Change Database Name", "Database Name", lstDBs, ref DBVal) == MessageBoxResult.OK)
            {
                SQLAppWaitingDialog.ShowDialog();
                int idx = lines.ToList().FindIndex(x => x.StartsWith("DB_DATABASE"));
                lines[idx] = string.Format("{0}={1}", "DB_DATABASE", DBVal);
                File.WriteAllLines(filePath, lines);
                FunctionListObject functionObj = new FunctionListObject();
                functionObj.FuncName = "CmdLaravelConfigCache";
                ExecutedScriptCommand(functionObj, frmParent);
                SQLAppWaitingDialog.HideDialog();
            }
        }
        public static void LoadDataByTable(Window frmParent)
        {
            PromptForm._frmParent = frmParent;
            string moduleName = "";
            MessageBoxResult result = PromptForm.ShowText("Find Module", "ModuleName", ref moduleName);
            if (result == MessageBoxResult.Cancel) return;
            string strQuery = SQLApp.GetFile(strPath + "FindModule.sql");
            strQuery = strQuery.Replace("@ModuleName@", moduleName);
            DataTable dt = SQLDBUtil.GetDataTable(strQuery);
            if (dt == null) return;
            dt.TableName = "STModules";
            ShowResultData(frmParent, dt, strQuery);
        }
        //Ctrl + 1 tìm module
        public static void FindModule(Window frmParent)
        {
            PromptForm._frmParent = frmParent;
            string moduleName = "";
            MessageBoxResult result = PromptForm.ShowText("Find Module", "ModuleName", ref moduleName);
            if (result == MessageBoxResult.Cancel) return;
            string strQuery = SQLApp.GetFile(strPath + "FindModule.sql");
            strQuery = strQuery.Replace("@ModuleName@", moduleName);
            DataTable dt = SQLDBUtil.GetDataTable(strQuery);
            if (dt == null) return;
            dt.TableName = "STModules";
            ShowResultData(frmParent, dt, strQuery);
        }
        //Ctrl + F Find Column
        public static void FindColumn(Window frmParent)
        {
            PromptForm._frmParent = frmParent;
            string tableName = "";
            MessageBoxResult result = PromptForm.ShowCombobox("FindColumn", "Table Name", ref tableName);
            if (result == MessageBoxResult.Cancel) return;
            string colName = "";
            result = PromptForm.ShowText("FindColumn", "Column Name", ref colName);
            if (result == MessageBoxResult.Cancel) return;
            DataSet ds = SQLDBUtil.GetAllTableColumns(tableName, colName);
            DataTable dtData = SQLDBUtil.GetDataTableByDataSet(ds);
            if (dtData == null) return;
            dtData.TableName = tableName;
            ShowResultData(frmParent, dtData, "");
        }
        //Alt + 1 View Data by No
        public static void GetViewDataByNo(Window frmParent)
        {
            PromptForm._frmParent = frmParent;
            string tableName = "";

            MessageBoxResult result = PromptForm.ShowCombobox("ViewDataByNo", "Table Name", ref tableName);
            if (result == MessageBoxResult.Cancel) return;
            string colName = "";
            result = PromptForm.ShowText("ViewDataByNo", "Column Name", ref colName);
            if (result == MessageBoxResult.Cancel) return;
            if (string.IsNullOrEmpty(colName)) colName = "*";
            string strWhere = string.Empty;
            if (SQLDBUtil.ColumnIsExistInTable(tableName, "AAStatus")) strWhere = "AAStatus = 'Alive'";
            string strQuery = SQLDBUtil.GenerateQuery(tableName, strWhere, colName);
            //DataTable dtData = SQLDBUtil.GetDataByTable(tableName, strWhere, colName);
            //if (dtData == null) return;
            //dtData.TableName = tableName;
            ShowResultData(frmParent, null, strQuery);
        }
        
        //Ctrl + 0 View Connect Sql
        public static void GetViewConnectToSQL(Window frmParent)
        {
            string strQuery = SQLApp.GetFile(strPath + "ViewConnectSql.sql");
            DataTable dtSource = SQLDBUtil.GetDataTable(strQuery);
            ShowResultData(frmParent, dtSource, strQuery);
        }
        //Ctrl + Shift + A : Create Module
        public static void CreateMoudle()
        {
            string str = MethodInfo.GetCurrentMethod().Name;
            string strModuleName = "";
            MessageBoxResult result = PromptForm.ShowText(str, "Module Name", ref strModuleName);
            if (result == MessageBoxResult.Cancel) return;
            string strModuleDesc = "";
            result = PromptForm.ShowText(str, "Module Descreiption", ref strModuleDesc);
            if (result == MessageBoxResult.Cancel) return;
            string strModuleCode = "";
            result = PromptForm.ShowText(str, "Module Code", ref strModuleCode);
            if (result == MessageBoxResult.Cancel) return;
            if (!string.IsNullOrEmpty(strModuleName) && !string.IsNullOrEmpty(strModuleDesc) && !string.IsNullOrEmpty(strModuleCode))
            {
                string strQuery = SQLApp.GetFile(strPath + "CreateModule.sql");
                strQuery = strQuery.Replace("@ModuleName@", strModuleName);
                strQuery = strQuery.Replace("@ModuleCode@", strModuleCode);
                strQuery = strQuery.Replace("@ModuleDesc@", strModuleDesc);
                int iResult = SQLDBUtil.ExecuteNonQuery(strQuery);
                ShowMessenger(iResult);
            }
        }
        //Ctrl + Alt + T : Gen Script Create Table
        public static void GenScriptCreateTable(Window frmParent)
        {
            string str = MethodInfo.GetCurrentMethod().Name;
            string strTableName = "";
            MessageBoxResult result = PromptForm.ShowCombobox(str, "Table Name", ref strTableName);
            if (result == MessageBoxResult.Cancel) return;
            string strQuery = SQLApp.GetFile(strPath + "GenCreateTable.sql");
            strQuery = strQuery.Replace("@TableName@", strTableName);
            DataTable dt = SQLDBUtil.GetDataTable(strQuery);
            ShowResultData(frmParent, dt, strQuery);
        }
        //Ctrl + Alt + T : Gen Script Create Table
        public static void GenScriptCreateColumn(Window frmParent)
        {
            string str = MethodInfo.GetCurrentMethod().Name;
            string strTableName = "";
            MessageBoxResult result = PromptForm.ShowCombobox(str, "Table Name", ref strTableName);
            if (result == MessageBoxResult.Cancel) return;
            string strColumnName = "";
            result = PromptForm.ShowCombobox(str, "Column Name", ref strTableName);
            if (result == MessageBoxResult.Cancel) return;
            string strQuery = SQLApp.GetFile(strPath + "GenCreateTable.sql");
            strQuery = strQuery.Replace("@TableName@", strTableName);
            DataTable dt = SQLDBUtil.GetDataTable(strQuery);
            ShowResultData(frmParent, dt, strQuery);
        }
        //Ctrl + 6: Gen Info / Controller
        public static void GenInfoController()
        {
            string str = MethodInfo.GetCurrentMethod().Name;
            string strTableName = "";
            MessageBoxResult result = PromptForm.ShowCombobox(str, "Table Name", ref strTableName);
            if (result == MessageBoxResult.Cancel) return;
            string strType = string.Empty;
            result = PromptForm.ShowCombobox(str, "Gen Controller", new string[] { "YES", "NO" }, ref strType);
            if (result == MessageBoxResult.Cancel) return;
            string strQuery = SQLApp.GetFile(strPath + "GenInfo.sql");
            strQuery = strQuery.Replace("@TableName@", strTableName);
            strQuery = strQuery.Replace("@Version@", System.Windows.Forms.Application.ProductName + " - " + System.Windows.Forms.Application.ProductVersion);
            strQuery = strQuery.Replace("@CreatedDate@", DateTime.Now.ToShortDateString());
            DataTable dt = SQLDBUtil.GetDataTable(strQuery);
            if (dt != null)
            {
                string strContent = Convert.ToString(dt.Rows[0][0]);
                SQLApp.WriteFile("D:\\" + strTableName + "Info.cs", strContent);
                //NotifycationAler aler = new NotifycationAler();
                //aler.ShowDialog();
            }
            if (strType == "YES")
            {
                strQuery = SQLApp.GetFile(strPath + "GenController.sql");
                strQuery = strQuery.Replace("@TableName@", strTableName);
                strQuery = strQuery.Replace("@Version@", System.Windows.Forms.Application.ProductName + " - " + System.Windows.Forms.Application.ProductVersion);
                strQuery = strQuery.Replace("@CreatedDate@", DateTime.Now.ToShortDateString());
                dt = SQLDBUtil.GetDataTable(strQuery);
                if (dt != null)
                {
                    string strContent = Convert.ToString(dt.Rows[0][0]);
                    SQLApp.WriteFile("D:\\" + strTableName + "Controller.cs", strContent);
                }
            }
        }
        //public static void LoadFunction(frmMain frmParent)
        //{
        //    ListViewItem obj = frmParent.lstFunction.SelectedItems.Cast<ListViewItem>().FirstOrDefault();
        //    if (obj == null) return;
        //    string strCnt = SQLApp.GetIniFile(strFileCfgScript, strDynPara, obj.Text + "Cnt");
        //    if (string.IsNullOrEmpty(strCnt)) return;
        //    int idxCnt = Convert.ToInt32(strCnt);
        //    Dictionary<string, string> dicPara = new Dictionary<string, string>();
        //    for (int i = 1; i <= idxCnt; i++)
        //    {
        //        string strName = SQLApp.GetIniFile(strFileCfgScript, strDynPara, obj.Text + "Name" + i);
        //        string strVal = SQLApp.GetIniFile(strFileCfgScript, strDynPara, obj.Text + "Val" + i);
        //        string strValList = SQLApp.GetIniFile(strFileCfgScript, strDynPara, obj.Text + "ValList" + i);
        //        MessageBoxResult result = MessageBoxResult.Cancel;
        //        if (strName.Contains("TableName"))
        //            result = PromptForm.ShowCombobox(obj.Text, strName, ref strVal);
        //        else if(!string.IsNullOrEmpty(strValList))
        //            result = PromptForm.ShowCombobox(obj.Text, strName, strValList.Split('|'), ref strVal);
        //        else
        //            result = PromptForm.ShowText(obj.Text, strName, ref strVal);
        //        if (result == MessageBoxResult.Cancel) return;
        //        dicPara.AddItem(strName, strVal);
        //    }
        //    string strQuery = SQLApp.GetFile(strPath + obj.Text + ".sql");
        //    foreach (KeyValuePair<string,string> item in dicPara)
        //    {
        //        strQuery = strQuery.Replace(item.Key, item.Value);
        //    }
        //    SQLDBUtil.GetDataSet(strQuery);
        //}
        #region Sync DB
        public static DataSet SynchronizeTable(int indexFrom, int indexTo, string strDBFrom, string strDBTo)
        {
            int iResult = CreateLinkServer(indexFrom, indexTo, strDBFrom, strDBTo);
            string strSrvName = GetDescriptionConfig(indexFrom);
            string strSrvAdd = GetServerConfig(indexFrom);
            string strUser = GetUserNameConfig(indexFrom);
            string strPassWord = GetPassWordConfig(indexFrom);

            string strSrvNameTo = GetDescriptionConfig(indexTo);
            string strSrvAddTo = GetServerConfig(indexFrom);
            string strUserTo = GetUserNameConfig(indexTo);
            string strPassWordTo = GetPassWordConfig(indexTo);

            string strQuery = SQLApp.GetFile(strPath + "SyncDB.sql");
            strQuery = strQuery.Replace("@serverName@", strSrvName);
            strQuery = strQuery.Replace("@serverAddress@", strSrvAdd);
            strQuery = strQuery.Replace("@serverUser@", strUser);
            strQuery = strQuery.Replace("@serverPass@", strPassWord);
            strQuery = strQuery.Replace("@serverDB@", strDBFrom);

            strQuery = strQuery.Replace("@serverNameTo@", strSrvNameTo);
            strQuery = strQuery.Replace("@serverAddressTo@", strSrvAddTo);
            strQuery = strQuery.Replace("@serverUserTo@", strUserTo);
            strQuery = strQuery.Replace("@serverPassTo@", strPassWordTo);
            strQuery = strQuery.Replace("@serverDBTo@", strDBTo);

            return SQLDBUtil.GetDataSet(strQuery);
        }
        public static int CreateLinkServer(int indexFrom, int indexTo, string strDBFrom, string strDBTo)
        {
            string strSrvName = GetDescriptionConfig(indexFrom);
            string strSrvAdd = GetServerConfig(indexFrom);
            string strUser = GetUserNameConfig(indexFrom);
            string strPassWord = GetPassWordConfig(indexFrom);

            string strSrvNameTo = GetDescriptionConfig(indexTo);
            string strSrvAddTo = GetServerConfig(indexFrom);
            string strUserTo = GetUserNameConfig(indexTo);
            string strPassWordTo = GetPassWordConfig(indexTo);

            string strQuery = SQLApp.GetFile(strPath + "LinkServer.sql");
            strQuery = strQuery.Replace("@serverName@", strSrvName);
            strQuery = strQuery.Replace("@serverAddress@", strSrvAdd);
            strQuery = strQuery.Replace("@serverUser@", strUser);
            strQuery = strQuery.Replace("@serverPass@", strPassWord);
            strQuery = strQuery.Replace("@serverDB@", strDBFrom);

            strQuery = strQuery.Replace("@serverNameTo@", strSrvNameTo);
            strQuery = strQuery.Replace("@serverAddressTo@", strSrvAddTo);
            strQuery = strQuery.Replace("@serverUserTo@", strUserTo);
            strQuery = strQuery.Replace("@serverPassTo@", strPassWordTo);
            strQuery = strQuery.Replace("@serverDBTo@", strDBTo);

            return SQLDBUtil.ExecuteNonQuery(strQuery);
        }
        public static string GetScriptDropColumn(string strTable, string strColumn)
        {
            return string.Format("ALTER TABLE [{0}] DROP COLUMN [{1}]", strTable, strColumn);
        }
        public static string GetScriptDropTable(string strTable)
        {
            return string.Format("DROP TABLE [{0}]", strTable);
        }
        public static string GetScriptCreateTable(string strDBName, string strTable)
        {
            string strQuery = SQLApp.GetFile(strPath + "CreateTable.sql");
            strQuery = strQuery.Replace("@tablename@", strTable);
            strQuery = strQuery.Replace("@schemaname@", strDBName);
            DataTable dt = SQLDBUtil.GetDataTable(strQuery);
            if (dt != null && dt.Rows.Count > 0)
            {
                return string.Format("{0}", dt.Rows[0][0]);
            }
            return "";
        }
        public static string GetScriptCreateColumn(string strDBName, string strTable, string strColName)
        {
            string strQuery = SQLApp.GetFile(strPath + "CreateColumn.sql");
            strQuery = strQuery.Replace("@TableName@", strTable);
            strQuery = strQuery.Replace("@ColumnName@", strColName);
            strQuery = strQuery.Replace("@schemaname@", strDBName);
            DataTable dt = SQLDBUtil.GetDataTable(strQuery);
            if (dt != null && dt.Rows.Count > 0)
            {
                return string.Format("{0}", dt.Rows[0][0]);
            }
            return "";
        }
        public static void CompareDatabase(SqlDbConnectionType consType, Dictionary<string, SqlDbConnection> lstCons)
        {
            if (!CheckSelectedDB()) return;
            switch (consType)
            {
                case SqlDbConnectionType.MySql:
                    CompareDBMySql(lstCons);
                    break;
                case SqlDbConnectionType.SqlServer:
                    //(new TSQL.Tokens.
                    break;
            }
        }
        protected static void CompareDBMySql(Dictionary<string, SqlDbConnection> lstCons)
        {
            string strQuery = SQLDBUtil.GenerateQuery("information_schema.TABLES", "TABLE_SCHEMA = '{0}'", "TABLE_NAME");
            CompareDifferrentData(lstCons, strQuery, "Compare Same Tables");

            strQuery = SQLDBUtil.GenerateQuery("information_schema.COLUMNS", "TABLE_SCHEMA = '{0}'", "TABLE_NAME,COLUMN_NAME,DATA_TYPE");
            CompareDifferrentData(lstCons, strQuery, "Compare Same Columns");

            strQuery = SQLDBUtil.GenerateQuery("information_schema.ROUTINES", "ROUTINE_SCHEMA = '{0}'", "ROUTINE_NAME,ROUTINE_TYPE,DATA_TYPE");
            CompareDifferrentData(lstCons, strQuery, "Compare Same Routines");

            strQuery = SQLDBUtil.GenerateQuery("information_schema.TRIGGERS", "TRIGGER_SCHEMA = '{0}'", "TRIGGER_NAME,EVENT_MANIPULATION,EVENT_OBJECT_TABLE,ACTION_TIMING");
            CompareDifferrentData(lstCons, strQuery, "Compare Same Triggers");

            strQuery = SQLDBUtil.GenerateQuery("information_schema.STATISTICS", "TABLE_SCHEMA = '{0}'", "TABLE_NAME,COLUMN_NAME");
            CompareDifferrentData(lstCons, strQuery, "Compare Same User key");
        }
        protected static void CompareDifferrentData(Dictionary<string, SqlDbConnection> lstCons, string strQuery, string tblName)
        {
            SQLDBUtil.CurrentDatabase = lstCons[_ctrlFrom];
            DataTable dtSource = SQLDBUtil.GetDataTable(string.Format(strQuery, SQLDBUtil.CurrentDatabase.Connection.Database));
            SQLDBUtil.CurrentDatabase = lstCons[_ctrlTo];
            DataTable dtTarget = SQLDBUtil.GetDataTable(string.Format(strQuery, SQLDBUtil.CurrentDatabase.Connection.Database));
            IEnumerable<DataRow> lstSameTable = dtSource.AsEnumerable().Except(dtTarget.AsEnumerable(), DataRowComparer.Default);
            DataTable dtSame = ConvertDataRowToTable(lstSameTable, tblName);
            ShowCompareResultView(dtSame);
        }
        protected static DataTable ConvertDataRowToTable(IEnumerable<DataRow> lstRows, string strTableName)
        {
            if(lstRows.Count() > 0)
            {
                using(DataTable dtSame = lstRows.CopyToDataTable())
                {
                    dtSame.TableName = strTableName;
                    return dtSame;
                }
            }
            else
            {
                using (DataTable dtSame = new DataTable())
                {
                    dtSame.TableName = strTableName;
                    return dtSame;
                }
            }
        }
        private static void AddReaderToGrid(BasePopupViewModel viewModel, System.Data.Common.DbDataReader dataReader)
        {
            DataTable dtSource = new DataTable();
            dtSource.TableName = "Data";

            for (int i = 0; i < dataReader.FieldCount; i++)
            {
                DataColumn col = new DataColumn(dataReader.GetName(i), dataReader.GetFieldType(i));
                dtSource.Columns.Add(col);
            }
            while (dataReader.Read())
            {
                object[] values = new object[dataReader.FieldCount];
                dataReader.GetValues(values);
                dtSource.LoadDataRow(values, false);
            }
            dataReader.Close();
            
            AddControlsToGrid(viewModel, dtSource, "");
        }
        #endregion

        #region func list F10
        public static void GetFunctionList()
        {
            lstFuncLst = new Dictionary<string, string>()
            {
                { "COL RENAME","RenameColumn" }, { "COL REPLACENAME","replacename" }, { "COL ADD","AddColumn" }, { "COL ALTER","AlterColumn" }, { "COL DROP","DropColumn" },
                { "INDEX ADD","AddIndex" }, { "INDEX DROP","DropIndex" }, { "PK ADD","AddPrimaryKey" }, { "PK DROP","DropPrimaryKey"}, { "FK ADD","AddForeignKey" }, { "FK DROP","DropForeignKey" },
                { "TABLE RENAME","RenameTable" }, { "TABLE ADD","CreateTable" }, { "TABLE DROP","DropTable" }, { "TABLE ZAP","ZapTable" }, {"DB CREATE","CreateDatabase" }, {"DB DROP","DropDatabase" },
                { "DB BACKUP","BackupDatabase" }, { "DB SHRINK","ShrinkDatabase" }, {"DB RESTORE","RestoreDatabase" }, {"DB RESTART","" }
            };
        }
        public static void ShowFunctionList()
        {
            GetFunctionList();
            string[] lstSource = lstFuncLst.Keys.ToArray();
            string value = "";
            MessageBoxResult result = PromptForm.ShowCombobox("Action", "Action", lstSource, ref value);
            if (result == MessageBoxResult.Cancel) return;
            //switch (value)
            //{
            FunctionList lstThis = new FunctionList();
            MethodInfo mi = lstThis.GetType().GetMethod(lstFuncLst[value]);
            mi.Invoke(lstThis, null);
            //MethodInfo miConstructed = mi.MakeGenericMethod(type[0]);
            //}
        }
        public static void RenameColumn()
        {
            string str = MethodInfo.GetCurrentMethod().Name;
            string strTblName = "";
            MessageBoxResult result = PromptForm.ShowText(str, "Table Name", ref strTblName);
            if (result == MessageBoxResult.Cancel) return;
            string strColOld = "";
            result = PromptForm.ShowText(str, "Column Name", ref strColOld);
            if (result == MessageBoxResult.Cancel) return;
            string strColNew = "";
            result = PromptForm.ShowText(str, "New Column Name", ref strColNew);
            if (result == MessageBoxResult.Cancel) return;
            int iResult = SQLDBUtil.ExecuteNonQuery(string.Format("ALTER TABLE {0} RENAME COLUMN {1} TO {2}", strTblName, strColOld, strColNew));
            ShowMessenger(iResult);
        }
        public static void DropColumn()
        {
            string str = MethodInfo.GetCurrentMethod().Name;
            string strTblName = "";
            MessageBoxResult result = PromptForm.ShowText(str, "Table Name", ref strTblName);
            if (result == MessageBoxResult.Cancel) return;
            string strColName = "";
            result = PromptForm.ShowText(str, "Column Name", ref strColName);
            if (result == MessageBoxResult.Cancel) return;
            int iResult = SQLDBUtil.ExecuteNonQuery(string.Format("ALTER TABLE {0} DROP COLUMN {1}", strTblName, strColName));
            ShowMessenger(iResult);
        }
        public static void AlterColumn()
        {
            string str = MethodInfo.GetCurrentMethod().Name;
            string strTblName = "";
            MessageBoxResult result = PromptForm.ShowText("Table Name", "Table Name", ref strTblName);
            if (result == MessageBoxResult.Cancel) return;
            string strColName = "";
            result = PromptForm.ShowText("Column Name", "Column Name", ref strColName);
            if (result == MessageBoxResult.Cancel) return;
            string strColType = SQLDBUtil.GetColumnDBType(strTblName, strColName);
            result = PromptForm.ShowText("Column Type", "Column Type", ref strColType);
            if (result == MessageBoxResult.Cancel) return;
            int iResult = SQLDBUtil.ExecuteNonQuery(string.Format("ALTER TABLE {0} ALTER COLUMN {1} {2}", strTblName, strColName, strColType));
            ShowMessenger(iResult);
        }
        public static void AddColumn()
        {
            string str = MethodInfo.GetCurrentMethod().Name;
            string strTblName = "";
            MessageBoxResult result = PromptForm.ShowCombobox(str, "Table Name", ref strTblName);
            if (result == MessageBoxResult.Cancel) return;
            string strColName = "";
            result = PromptForm.ShowText(str, "Column Name", ref strColName);
            if (result == MessageBoxResult.Cancel) return;
            int iResult = SQLDBUtil.ExecuteNonQuery(string.Format("ALTER TABLE {0} ADD {1}", strTblName, strColName));
            ShowMessenger(iResult);
        }
        public static void AddPrimaryKey()
        {
            string str = MethodInfo.GetCurrentMethod().Name;
            string strTblName = "";
            MessageBoxResult result = PromptForm.ShowText("Table Name", "Table Name", ref strTblName);
            if (result == MessageBoxResult.Cancel) return;
            string strColName = "";
            result = PromptForm.ShowText("Column Name", "Column Name", ref strColName);
            if (result == MessageBoxResult.Cancel) return;
            int iResult = SQLDBUtil.ExecuteNonQuery(string.Format("ALTER TABLE {0} ADD CONSTRAINT PK_{0} PRIMARY KEY({1})", strTblName, strColName));
            ShowMessenger(iResult);
        }
        public static void AddIndex()
        {
            string str = MethodInfo.GetCurrentMethod().Name;
            string strTblName = "";
            MessageBoxResult result = PromptForm.ShowCombobox(str, "Table Name", ref strTblName);
            if (result == MessageBoxResult.Cancel) return;
            string strColName = "";
            result = PromptForm.ShowText(str, "Column Name", ref strColName);
            if (result == MessageBoxResult.Cancel) return;
            string strIdxName = "";
            result = PromptForm.ShowText(str, "Index Name", ref strIdxName);
            if (result == MessageBoxResult.Cancel) return;
            int iResult = SQLDBUtil.ExecuteNonQuery(string.Format("CREATE INDEX {0} ON {1}({2})", strIdxName, strTblName, strColName));
            ShowMessenger(iResult);
        }
        public static void DropPrimaryKey()
        {
            string str = MethodInfo.GetCurrentMethod().Name;
            string strTblName = "";
            MessageBoxResult result = PromptForm.ShowText("Table Name", "Table Name", ref strTblName);
            if (result == MessageBoxResult.Cancel) return;
            string strColName = "";
            result = PromptForm.ShowText("Column Name", "Column Name", ref strColName);
            if (result == MessageBoxResult.Cancel) return;
            int iResult = SQLDBUtil.ExecuteNonQuery(string.Format("ALTER TABLE {0} DROP CONSTRAINT PK_{1}", strTblName, strColName));
            ShowMessenger(iResult);
        }
        public static void DropIndex()
        {
            string str = MethodInfo.GetCurrentMethod().Name;
            string strTblName = "";
            MessageBoxResult result = PromptForm.ShowText("Table Name", "Table Name", ref strTblName);
            if (result == MessageBoxResult.Cancel) return;
            string strIdxName = "";
            result = PromptForm.ShowText("Index Name", "Index Name", ref strIdxName);
            if (result == MessageBoxResult.Cancel) return;
            int iResult = SQLDBUtil.ExecuteNonQuery(string.Format("DROP INDEX {0}.{1}", strTblName, strIdxName));
            ShowMessenger(iResult);
        }
        public static void DropTable()
        {
            string str = MethodInfo.GetCurrentMethod().Name;
            string strTblName = "";
            MessageBoxResult result = PromptForm.ShowText("Table Name", "Table Name", ref strTblName);
            if (result == MessageBoxResult.Cancel) return;
            int iResult = SQLDBUtil.ExecuteNonQuery(string.Format("DROP TABLE {0}", strTblName));
            ShowMessenger(iResult);
        }
        public static void DropDatabase()
        {
            string str = MethodInfo.GetCurrentMethod().Name;
            string strDBName = "";
            MessageBoxResult result = PromptForm.ShowText("Database Name", "Database Name", ref strDBName);
            if (result == MessageBoxResult.Cancel) return;
            int iResult = SQLDBUtil.ExecuteNonQuery(string.Format("DROP DATABASE {0}", strDBName));
            ShowMessenger(iResult);
        }
        public static void AddForeignKey(string strTblName, string strColName, string strReferenTblName, string strReferenColName)
        {
            string str = MethodInfo.GetCurrentMethod().Name;
            int iResult = SQLDBUtil.ExecuteNonQuery(string.Format("ALTER TABLE {0} ADD CONSTRAINT FK_{0}_{2}_{1} FOREIGN KEY {1} REFERENCES {2} ({3})", strTblName, strColName, strReferenTblName, strReferenColName));
            ShowMessenger(iResult);
        }
        public static void DropForeignKey()
        {
            string str = MethodInfo.GetCurrentMethod().Name;
            string strTblName = "";
            MessageBoxResult result = PromptForm.ShowText("Table Name", "Table Name", ref strTblName);
            if (result == MessageBoxResult.Cancel) return;
            int iResult = SQLDBUtil.ExecuteNonQuery(string.Format("ALTER TABLE {0} DROP CONSTRAINT FK_{1}", strTblName));
            ShowMessenger(iResult);
        }
        public static void RenameTable()
        {
            string str = MethodInfo.GetCurrentMethod().Name;
            string strTblName = "";
            MessageBoxResult result = PromptForm.ShowText(str, "Table Name", ref strTblName);
            if (result == MessageBoxResult.Cancel) return;
            string strToTblName = "";
            result = PromptForm.ShowText(str, "New Table Name", ref strToTblName);
            if (result == MessageBoxResult.Cancel) return;
            int iResult = SQLDBUtil.ExecuteNonQuery(string.Format("RENAME TABLE {0} TO {1}", strTblName, strToTblName));
            ShowMessenger(iResult);
        }
        public static void CreateTable()
        {

        }
        public static void CreateDatabase()
        {

        }
        public static void ZapTable()
        {

        }
        public static void BackupDatabase()
        {

        }
        public static void ShrinkDatabase()
        {

        }
        public static void RestoreDatabase()
        {

        }
        public static void ShowMessenger(int idx)
        {
            if (idx != 0)
                System.Windows.MessageBox.Show("Thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            else
                System.Windows.MessageBox.Show("Thất bại!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        public static void ShowMessenge(string strContent, string strTitle, MessageBoxImage messageImage)
        {
            DXMessageBox.Show(strContent, strTitle, MessageBoxButton.OK, messageImage);
        }
        public static void ShowMessengeInfo(string strContent, string strTitle = "Thông báo")
        {
            ShowMessenge(strContent, strTitle, MessageBoxImage.Information);
        }
        #endregion

        #region Function Extra
        public static void FindAllProcessLockedFile(Window frmParent)
        {
            string strFilePath = "";
            using (OpenFileDialog open = new OpenFileDialog())
            {
                if(open.ShowDialog() == DialogResult.OK)
                    strFilePath = open.FileName;
            }
            List<Process> lstProcess = Win32Processes.GetProcessesLockingFile(strFilePath);
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("name"));
            foreach (Process item in lstProcess)
            {
                DataRow dr = dt.NewRow();
                dr["name"] = item.ProcessName;
                dt.Rows.Add(dr);
                item.Kill();
            }
            ShowResultData(frmParent, dt, "");
        }
        
        private static void AddEventByView(System.Windows.Controls.Control view, BasePopupViewModel viewModel, Key _key, ModifierKeys _modifierKeys)
        {
            InputBinding inputBinding = new KeyBinding(viewModel.KeyBindingCommand, _key, _modifierKeys);
            inputBinding.CommandParameter = string.Format("{0}+{1}",_modifierKeys.ToString(), _key.ToString());
            view.InputBindings.Add(inputBinding);
        }
        public static void ShowYoutubeView()
        {
            ViewModels.YoutubeViewModel popupView = new YoutubeViewModel();
            ShowPopupViewModal(popupView, new Views.YoutubeView());
        }
        public static void ShowFlashDealView()
        {
            Views.FlashDealView view = new Views.FlashDealView();
            ViewModels.FlashDealViewModel popupView = new FlashDealViewModel(view);
            popupView.Header = "Flash sale sendo";
            ShowPopupViewModal(popupView, view);
        }
        public static void ShowEditDataView()
        {
            Views.EditDataView view = new Views.EditDataView();
            ViewModels.EditDataViewModel popupView = new EditDataViewModel(view);
            popupView.Title = "T-SQL";
            popupView.Header = "T-SQL";
            AddEventByView(view.reditData, popupView, Key.F9, ModifierKeys.None);
            AddEventByView(view.reditData, popupView, Key.G, ModifierKeys.Control);
            ShowPopupViewModal(popupView, view);
        }
        
        #endregion
        #endregion

        #region Function list cmd
        private static void ExecutedScriptCommand(FunctionListObject functionObj, Window frmParent)
        {
            //string funcName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            //Lấy tên function hiện tại
            string strScript = GetScriptCommandByFuncName(functionObj.FuncName);
            strScript = GenerateScriptWithParameters(functionObj, strScript, frmParent);
            if (string.IsNullOrEmpty(strScript))
            {
                ShowMessengeInfo("Không có mã thực thi");
                return;
            }
            SQLAppWaitingDialog.ShowDialog();
            string sourceUrl = GetSourceCodePath();
            string output = SQLApp.ExecutedPowerShell(string.Format("cd {0} {1} {2}", sourceUrl, Environment.NewLine, strScript));
            string strType = SQLApp.GetIniFile(strFileCfgScript, strDynPara, functionObj.Name + "Show");
            if(!string.IsNullOrEmpty(strType))
            {
                ShowScriptCommand(output, strType);
                SQLAppWaitingDialog.HideDialog();
                return;
            }
            SQLAppWaitingDialog.HideDialog();
            ShowMessengeInfo(output);
        }
        public static string GetScriptCommandByFuncName(string strFuncName)
        {
            return SQLApp.GetIniFile(string.Concat(strPath, "ScriptCommand.ini"), "Laravel", strFuncName);
        } 
        protected static void ShowScriptCommand(string output, string strType)
        {
            if (strType.Equals("table"))
            {
                string[] arr = output.Split('\n');
                DataTable dtSoure = new DataTable();
                dtSoure.TableName = "Route List";
                int idx = 0;
                arr.ToList().ForEach(r =>
                {
                    string[] row = r.Split('|');
                    if (row.Length > 1)
                    {
                        if (idx == 0)
                        {
                            row.Where(c => !string.IsNullOrEmpty(c.Trim())).ToList().ForEach(c =>
                            {
                                DataColumn col = new DataColumn(c.Trim(), typeof(string));
                                dtSoure.Columns.Add(col);
                            });
                        }
                        else
                            dtSoure.LoadDataRow(row.Where(c => !string.IsNullOrEmpty(c.Trim())).Select(c => c.Trim()).ToArray(), false);
                        idx++;
                    }
                });
                ShowResultData(null, dtSoure);
            }
            else
            {
                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.Filter = "*." + strType.Replace("file", "");
                if(saveFile.ShowDialog() == DialogResult.OK)
                {
                    SQLApp.WriteFile(saveFile.FileName, output);
                }
            }
        }
        #endregion

        #region config Connect
        public static void GetConfigConnectSQL(string status, int idx = 0)
        {
            string cnt = SQLApp.GetIniFile(strFileName, section, serverCnt);
            int index = Convert.ToInt32(cnt);
            string strDesc = SQLApp.GetIniFile(strFileName, section, _serverDesc + idx);
            MessageBoxResult result = PromptForm.ShowText("Description", "Description", ref strDesc);
            if (result == MessageBoxResult.Cancel) return;
            string strServer = SQLApp.GetIniFile(strFileName, section, _serverName + idx);
            result = PromptForm.ShowText("Server", "Server", ref strServer);
            if (result == MessageBoxResult.Cancel) return;
            string strUser = SQLApp.GetIniFile(strFileName, section, _serverUID + idx);
            result = PromptForm.ShowText("User", "User", ref strUser);
            if (result == MessageBoxResult.Cancel) return;
            string strPass = SQLApp.GetIniFile(strFileName, section, _serverPWD + idx);
            result = PromptForm.ShowText("Pass", "Pass", ref strPass);
            if (result == MessageBoxResult.Cancel) return;
            if (status == "Add")
            {
                if (!string.IsNullOrEmpty(strDesc) && !string.IsNullOrEmpty(strServer) && !string.IsNullOrEmpty(strUser) && !string.IsNullOrEmpty(strPass))
                {
                    index += 1;
                    SQLApp.SetIniFile(strFileName, section, serverCnt, index.ToString());
                    SQLApp.SetIniFile(strFileName, section, _serverDesc + index, strDesc);
                    SQLApp.SetIniFile(strFileName, section, _serverName + index, strServer);
                    SQLApp.SetIniFile(strFileName, section, _serverUID + index, strUser);
                    SQLApp.SetIniFile(strFileName, section, _serverPWD + index, strPass);
                    SQLApp.SetIniFile(strFileName, section, _serverDBOld + index, "");
                }
            }
            else if (status == "edit")
            {
                if (!string.IsNullOrEmpty(strDesc) && !string.IsNullOrEmpty(strServer) && !string.IsNullOrEmpty(strUser) && !string.IsNullOrEmpty(strPass))
                {
                    SQLApp.SetIniFile(strFileName, section, _serverDesc + idx, strDesc);
                    SQLApp.SetIniFile(strFileName, section, _serverName + idx, strServer);
                    SQLApp.SetIniFile(strFileName, section, _serverUID + idx, strUser);
                    SQLApp.SetIniFile(strFileName, section, _serverPWD + idx, strPass);
                }
            }
            else
            {
                SQLApp.SetIniFile(strFileName, section, serverCnt, (index - 1).ToString());
                SQLApp.SetIniFile(strFileName, section, _serverDesc + idx, null);
                SQLApp.SetIniFile(strFileName, section, _serverName + idx, null);
                SQLApp.SetIniFile(strFileName, section, _serverUID + idx, null);
                SQLApp.SetIniFile(strFileName, section, _serverPWD + idx, null);
                SQLApp.SetIniFile(strFileName, section, _serverDBOld + idx, null);
            }
        }
        #endregion

        #region Load config
        public static void GetDatabaseVersion(SqlDbConnectionType connectionType)
        {
            string strVersion = SQLDBUtil.GetDatabaseVersion(connectionType);
            ShowMessengeInfo(strVersion);
        }
        public static List<string> LoadConfigInitToList(string keySection = null)
        {
            List<string> lst = new List<string>();
            if (string.IsNullOrEmpty(keySection)) keySection = section;
            string count = SQLApp.GetIniFile(strFileName, keySection, serverCnt);
            if (!string.IsNullOrEmpty(count))
            {
                for (int i = 0; i < Convert.ToInt32(count); i++)
                {
                    lst.Add(SQLApp.GetIniFile(strFileName, keySection, _serverDesc + (i + 1)));
                }
            }
            return lst;
        }
        public static void LoadConfigIniFromServer(ComboBox cbbData)
        {
            try
            {
                #region Code mới lấy thông tin từ file config
                cbbData.Items.Clear();
                cbbData.Items.AddRange(LoadConfigInitToList().ToArray());
                #endregion
            }
            catch
            {
            }
        }
        public static void LoadDatabaseByServer(ComboBox cboServer, ComboBox cboDataBase, int idx = -1)
        {
            try
            {
                if (idx != -1)
                    cboServer.SelectedIndex = idx;
                strServer = SQLApp.GetIniFile(strFileName, section, _serverName + (cboServer.SelectedIndex + 1));
                strUserName = SQLApp.GetIniFile(strFileName, section, _serverUID + (cboServer.SelectedIndex + 1));
                strPassWord = SQLApp.GetIniFile(strFileName, section, _serverPWD + (cboServer.SelectedIndex + 1));
                strDBOld = SQLApp.GetIniFile(strFileName, section, _serverDBOld + (cboServer.SelectedIndex + 1));

                //SQLDBUtil.SwitchConnection(strServer, strDBOld, strUserName, strPassWord);
                cboDataBase.DisplayMember = "name";
                cboDataBase.ValueMember = "name";
                DataTable dtSource = SQLDBUtil.GetDataTableByDataSet(SQLDBUtil.GetAllDatabases());
                if (dtSource != null)
                {
                    cboDataBase.DataSource = dtSource;
                }
            }
            catch
            {
            }
        }
        public static DataTable LoadDatabaseByServer(string keySection, int idx)
        {
            if (idx == -1) return new DataTable();
            strServer = GetServerConfig(keySection, idx);
            strUserName = GetUserNameConfig(keySection, idx);
            strPassWord = GetPassWordConfig(keySection, idx);
            //strDBOld = SQLApp.GetIniFile(strFileName, section, _serverDBOld + (cboServer.SelectedIndex + 1));

            SQLDBUtil.ChangeConnection((SqlDbConnectionType)Enum.Parse(typeof(SqlDbConnectionType), keySection), strServer, strUserName, strPassWord);
            return SQLDBUtil.GetDataTableByDataSet(SQLDBUtil.GetAllDatabases());
        }
        public static string GetItemConfig(string keySection, string keyPrefix, int idx)
        {
            if (string.IsNullOrEmpty(keySection)) keySection = section;
            return SQLApp.GetIniFile(strFileName, keySection, keyPrefix + (idx + 1));
        }
        public static string GetServerConfig(string keySection, int idx)
        {
            return GetItemConfig(keySection, _serverName, idx);
        }
        public static string GetUserNameConfig(string keySection, int idx)
        {
            return GetItemConfig(keySection, _serverUID, idx);
        }
        public static string GetPassWordConfig(string keySection, int idx)
        {
            return GetItemConfig(keySection, _serverPWD, idx);
        }
        public static string GetDescriptionConfig(string keySection, int idx)
        {
            return GetItemConfig(keySection, _serverDesc, idx);
        }
        public static string GetServerConfig(int idx)
        {
            return SQLApp.GetIniFile(strFileName, section, _serverName + (idx + 1));
        }
        public static string GetUserNameConfig(int idx)
        {
            return SQLApp.GetIniFile(strFileName, section, _serverUID + (idx + 1));
        }
        public static string GetPassWordConfig(int idx)
        {
            return SQLApp.GetIniFile(strFileName, section, _serverPWD + (idx + 1));
        }
        public static string GetDescriptionConfig(int idx)
        {
            return SQLApp.GetIniFile(strFileName, section, _serverDesc + (idx + 1));
        }
        public static string GetDBHistoryConfig(int idx)
        {
            return SQLApp.GetIniFile(strFileName, section, _serverDBOld + (idx + 1));
        }
        public static void SetDBHistoryConfig(int idx, string strDBOld)
        {
            SQLApp.SetIniFile(strFileName, section, _serverDBOld + (idx + 1), strDBOld);
        }
        public static void SetServerHistoryConfig(string strSVOld)
        {
            SQLApp.SetIniFile(strFileName, "LoginHistory", "ServerOld", strSVOld);
        }
        public static string GetServerHistoryConfig()
        {
            return SQLApp.GetIniFile(strFileName, "LoginHistory", "ServerOld");
        }
        public static int GetAnimateWindowTime()
        {
            int dwTime = 5000;
            dwTime = Convert.ToInt32(SQLApp.GetIniFile(strFileName, "AnimateWindow", "AnimateTime"));
            return dwTime;
        }
        #endregion
    }

    public class PromptForm
    {
        public static Window _frmParent;
        public static MessageBoxResult Show(string title, string promptText, ref string value, bool bIsText, bool bIsCombobox, bool isShowLstTbl, string[] lstFunctionList,
                                        InputBoxValidation validation)
        {
            ViewModels.PopupViewModel popupView = new ViewModels.PopupViewModel();
            Views.BasePopupWindow popup = new Views.BasePopupWindow() { DataContext = popupView, Height = 150, Width = 600 };
            popupView.Header = title;
            popupView.Title = promptText;
            popupView.valueReturn = value;
            popupView.isText = bIsText;
            if (isShowLstTbl)
            {
                DataTable dt = SQLDBUtil.GetDataTableByDataSet(SQLDBUtil.GetAllTables());
                popupView.dataSource = dt.Select().Select(x => Convert.ToString(x[0])).ToList();
            }
            else
                popupView.dataSource = lstFunctionList;
            Views.PopupView view = popup.waitLoadView.LoadingChild as Views.PopupView;
            if (bIsText)
                view.txtInput.Focus();
            else
                view.cboInput.Focus();
            //view.txtInpu
            //frmSearch frmInput = new frmSearch(_frmParent, bIsText, bIsCombobox, isShowLstTbl);
            //frmInput.SetCaption(promptText);
            //frmInput.Text = title;
            //frmInput.SetDataSourceCombobox(lstFunctionList);
            //if (bIsText)
            //    frmInput.SetText(value);
            //else
            //    frmInput.SetSelectedText(value);
            //frmInput.GetControlFocus();
            //frmInput.StartPosition = FormStartPosition.CenterScreen;
            //frmInput.ResumeLayout(false);
            //frmInput.PerformLayout();
            //SQLApp.SetFormTitle(frmInput);
            //string text = (bIsText) ? frmInput.GetText() : frmInput.GetSelectedText();
            //if (validation != null)
            //{
            //    frmInput.FormClosing += delegate (object sender, FormClosingEventArgs e)
            //    {
            //        if (frmInput.MessageBoxResult == MessageBoxResult.OK)
            //        {
            //            string errorText = validation(text);
            //            if (e.Cancel = (errorText != ""))
            //            {
            //                MessageBox.Show(frmInput, errorText, "Validation Error",
            //                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            //                frmInput.GetControlFocus();
            //            }
            //        }
            //    };
            //}
            DevExpress.Mvvm.UICommand uIOkCommand = new DevExpress.Mvvm.UICommand(MessageBoxResult.OK, "OK", popupView.okCommand, true, false, null, true, System.Windows.Controls.Dock.Left);
            DevExpress.Mvvm.UICommand uICancelCommand = new DevExpress.Mvvm.UICommand(MessageBoxResult.Cancel, "Cancel", null, false, true, null, true, System.Windows.Controls.Dock.Left);
            List<DevExpress.Mvvm.UICommand> lst = new List<DevExpress.Mvvm.UICommand>() { uIOkCommand, uICancelCommand };
            popup.ShowDialog(lst);
            value = Convert.ToString(popupView.valueReturn);
            //value = (bIsText) ? frmInput.GetText() : frmInput.GetSelectedText();
            MessageBoxResult dialogResult = popup.DialogButtonResult;
            return dialogResult;
        }
        public static MessageBoxResult ShowText(string title, string promptText, ref string value)
        {
            return Show(title, promptText, ref value, true, false, false, null, null);
        }
        public static MessageBoxResult ShowCombobox(string title, string promptText, ref string value)
        {
            return Show(title, promptText, ref value, false, true, true, null, null);
        }
        public static MessageBoxResult ShowCombobox(string title, string promptText, string[] lstSource, ref string value)
        {
            return Show(title, promptText, ref value, false, true, false, lstSource, null);
        }
        public delegate string InputBoxValidation(string errorMessage);
    }
    public class TextBoxUtil : TextBox
    {
        public TextBoxUtil()
        {
            this.KeyDown += textBox_KeyDown;
        }

        private void textBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
                this.SelectAll();
            if (e.Control && e.Shift && e.KeyCode == Keys.S)
            {
                string str = System.Windows.Forms.Clipboard.GetText();
                str = str.TrimEnd(Environment.NewLine.ToCharArray());
                string[] lst = str.Split('\n');
                this.ResetText();
                this.Text = string.Join(",", lst);
            }
        }
    }
}
