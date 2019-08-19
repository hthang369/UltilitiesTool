using DevExpress.Xpf.Core;
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
        private List<string> _lstFunctions;
        public List<string> lstFunctions
        {
            get => _lstFunctions;
            set => SetProperty(ref _lstFunctions, value);
        }
        public ICommand btnAddCommand { get; set; }
        public ICommand btnEditCommand { get; set; }
        public ICommand btnDelCommand { get; set; }
        public ICommand btnVerCommand { get; set; }
        public ICommand btnCompareCommand { get; set; }
        public ICommand btnRefeshCommand { get; set; }

        public MainViewModel()
        {
            lstFunctions = new List<string>();
            string[] arrFunctions = Directory.GetFiles(System.Windows.Forms.Application.StartupPath + "\\Scripts", "*.sql");
            Parallel.ForEach(arrFunctions, (item) =>
            {
                lstFunctions.Add(Path.GetFileNameWithoutExtension(item));
            });
            btnAddCommand = new RelayCommand<object>((x) => CanExecute(), (x) => ActionCommand(x));
            btnEditCommand = new RelayCommand<object>((x) => CanExecute(), (x) => ActionCommand(x));
            btnDelCommand = new RelayCommand<object>((x) => CanExecute(), (x) => ActionCommand(x));
            btnVerCommand = new RelayCommand<object>((x) => CanExecute(), (x) => ActionCommand(x));
        }

        private bool CanExecute()
        {
            return true;
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
            EditDataViewModel editData = new EditDataViewModel();
            editData.Header = "T-SQL";
            Views.BasePopupWindow popup = new Views.BasePopupWindow() { DataContext = editData };
            popup.waitLoadView.LoadingChild = new Views.EditDataView();
            popup.ShowDialog();
            if(string.IsNullOrEmpty(strType))
            {
                DXMessageBox.Show(mainWindow, "Vui lòng chọn Loại Sql", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            int idx = mainWindow.ctrlFrom.cboServer.SelectedIndex;
            if (idx == -1) idx = 0;
            Util.FunctionList.section = strType;
            Util.FunctionList.GetConfigConnectSQL(Convert.ToString(btn.Content), idx);
        }
    }
}
