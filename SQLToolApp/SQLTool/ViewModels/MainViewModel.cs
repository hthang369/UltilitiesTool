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
            btnAddCommand = new RelayCommand<object>((x) => CanExecute(), (x) => AddCommand(x));
            btnEditCommand = new RelayCommand<object>((x) => CanExecute(), (x) => AddCommand(x));
            btnDelCommand = new RelayCommand<object>((x) => CanExecute(), (x) => AddCommand(x));
            btnVerCommand = new RelayCommand<object>((x) => CanExecute(), (x) => AddCommand(x));
        }

        private bool CanExecute()
        {
            return true;
        }

        private void AddCommand(object item)
        {
            string val = string.Empty;
            DialogResult result = Util.PromptForm.ShowText("abdgdd", "", ref val);
        }
    }
}
