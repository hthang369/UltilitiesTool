using DevExpress.Xpf.Core;
using DevExpress.Xpf.Grid;
using SQLAppLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace SQLTool.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        Window frmMain;
        private List<FunctionListObject> _lstFunctions;
        public List<FunctionListObject> lstFunctions
        {
            get => _lstFunctions;
            set => SetProperty(ref _lstFunctions, value);
        }
        private double _iLvsWidth;
        public double iLvsWidth
        {
            get => _iLvsWidth;
            set => SetProperty(ref _iLvsWidth, value);
        }
        public ICommand btnAddCommand { get; set; }
        public ICommand btnEditCommand { get; set; }
        public ICommand btnDelCommand { get; set; }
        public ICommand btnVerCommand { get; set; }
        public ICommand btnCompareCommand { get; set; }
        public ICommand btnRefeshCommand { get; set; }
        public ICommand KeyBindingCommand { get; set; }

        public MainViewModel(Window window)
        {
            frmMain = window;
            iLvsWidth = window.Width - 30;
            lstFunctions = new List<FunctionListObject>();
            string strPath = System.Windows.Forms.Application.StartupPath + "\\Scripts\\config.ini";
            string strSectionCaption = "Captions";
            string strSectionFunc = "Funcs";
            string[] arrFunctions = Directory.GetFiles(System.Windows.Forms.Application.StartupPath + "\\Scripts", "*.sql");
            List<string> funcCaptions = SQLApp.GetKeysIniFile(strPath, strSectionCaption);
            Parallel.ForEach(arrFunctions, (item) =>
            {
                lock (lstFunctions)
                {
                    string strName = Path.GetFileNameWithoutExtension(item);
                    string strKey = funcCaptions.Find(x => x.Equals(strName));
                    string strText = string.IsNullOrEmpty(strKey) ? strName : SQLApp.GetIniFile(strPath, strSectionCaption, strKey);
                    lstFunctions.Add(new FunctionListObject { Name = strName, Text = strText, Path = item });
                }
            });
            List<string> funcKeysIni = SQLApp.GetKeysIniFile(strPath, strSectionFunc);
            Parallel.ForEach(funcKeysIni, (item) =>
            {
                lock (lstFunctions)
                {
                    string strName = item;
                    string strKey = funcCaptions.Find(x => x.Equals(strName));
                    string strText = SQLApp.GetIniFile(strPath, strSectionCaption, strKey);
                    string strFuncName = SQLApp.GetIniFile(strPath, strSectionFunc, strKey);
                    lstFunctions.Add(new FunctionListObject { Name = strName, Text = strText, FuncName = strFuncName });
                }
            });

            btnAddCommand = new RelayCommand<object>((x) => CanExecute(), (x) => ActionCommand(x));
            btnEditCommand = new RelayCommand<object>((x) => CanExecute(), (x) => ActionCommand(x));
            btnDelCommand = new RelayCommand<object>((x) => CanExecute(), (x) => ActionCommand(x));
            btnVerCommand = new RelayCommand<object>((x) => CanExecute(), (x) => ActionCommand(x));

            KeyBindingCommand = new RelayCommand<object>((x) => CanExecute(), (x) => KeyBindingActionCommand(x));
        }

        private void ActionCommand(object item)
        {
            SimpleButton btn = item as SimpleButton;
            FrameworkElement window = GetFrameworkElement(btn);
            MainWindow mainWindow = (window as MainWindow);
            string strType = Convert.ToString(mainWindow.ctrlFrom.cboSqlType.SelectedItem);
            //ResultViewModel resultView = new ResultViewModel();
            //DataResults data = new DataResults();
            //data.Title = "APPOs";
            //data.PinMode = TabPinMode.Left;
            //Task.Run(() =>
            //{
            //    data.DataSource = SQLAppLib.SQLDBUtil.GetDataTable("select * from APPOs");
            //});
            //Views.ResultView result = new Views.ResultView();
            //result.DataContext = data;
            //result.ShowDialog();
            if (string.IsNullOrEmpty(strType))
            {
                DXMessageBox.Show(mainWindow, "Vui lòng chọn Loại Sql", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            switch (Convert.ToString(btn.Content).ToLower())
            {
                case "add":
                case "edit":
                    int idx = mainWindow.ctrlFrom.cboServer.SelectedIndex;
                    if (idx == -1) idx = 0;
                    Util.FunctionList.section = strType;
                    Util.FunctionList.GetConfigConnectSQL(Convert.ToString(btn.Content), idx);
                    break;
                case "ver":
                    //SQLDBUtil.
                    break;
            }

        }

        internal void KeyActionCommand(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.F9:
                    Util.FunctionList.ShowEditDataView();
                    break;
                case Key.System:
                    if (e.KeyboardDevice.Modifiers == ModifierKeys.Alt)
                    {

                    }
                    break;
                case Key.Enter:
                    if(sender is TableView)
                    {
                        FunctionListObject func = (sender as TableView).SelectedRows.Cast<FunctionListObject>().FirstOrDefault();
                        if (!string.IsNullOrEmpty(func.FuncName))
                            Util.FunctionList.CallMethodName(func.FuncName, this.frmMain);
                        else if (!string.IsNullOrEmpty(func.Path))
                            Util.FunctionList.LoadQueryPath(func, this.frmMain);
                    }
                    break;
            }
        }

        private void ShowEditDataView()
        {
            ViewModels.PopupViewModel popupView = new PopupViewModel();
            Views.BasePopupWindow popupWindow = new Views.BasePopupWindow() { DataContext = popupView };
            popupView.isNoTabControl = Visibility.Visible;
            popupView.isTabControl = Visibility.Hidden;
            popupWindow.waitLoadView.LoadingChild = new Views.EditDataView();
            popupWindow.Show();
            //popupWindow.tabResults.AddNewTabItem();
        }

        private void KeyBindingActionCommand(object x)
        {
            string[] arr = Convert.ToString(x).Split('+');
            Key key = (Key)Enum.Parse(typeof(Key), arr.LastOrDefault());
            ModifierKeys modifier = (ModifierKeys)Enum.Parse(typeof(ModifierKeys), arr.FirstOrDefault());
            switch (modifier)
            {
                case ModifierKeys.Alt:
                    switch (key)
                    {
                        case Key.D1:
                            Util.FunctionList.FindModule(frmMain);
                            break;
                    }
                    break;
                case ModifierKeys.Control:
                    switch (key)
                    {
                        case Key.D1:
                            Util.FunctionList.FindModule(frmMain);
                            break;
                    }
                    break;
                case ModifierKeys.Shift:
                    break;
                case ModifierKeys.None:
                    if (key == Key.Enter)
                    {
                        //Util.FunctionList.FindAllProcessLockedFile(frmMain);
                        Util.FunctionList.ShowFlashDealView();
                    }
                    break;
            }
            //DevExpress.Xpf.Editors.SearchControl
        }

        private void KeyModifierCommand(ModifierKeys modifier, Key key)
        {

        }
    }

    public class FunctionListObject
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public string Path { get; set; }
        public string FuncName { get; set; }
    }
}
