using DevExpress.Xpf.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            string[] arrFunctions = Directory.GetFiles(Application.StartupPath + "\\Scripts", "*.sql");
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
            Util.FunctionList.GetConfigConnectSQL(Convert.ToString(btn.Content));
        }
    }
}
