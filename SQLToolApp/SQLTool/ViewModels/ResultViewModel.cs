using DevExpress.Xpf.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SQLTool.ViewModels
{
    public class ResultViewModel : BasePopupViewModel
    {
        private List<DataResults> _dataResults;
        public List<DataResults> DataResults
        {
            get => _dataResults;
            set => SetProperty(ref _dataResults, value);
        }
        
        public void KeyBindingActionCommand(object x)
        {
            
        }
    }
    public class DataResults
    {
        public string Title { get; set; }
        public DataTable DataSource { get; set; }
        public TabPinMode PinMode { get; set; }
        public ICommand KeyBindingCommand { get; set; }
    }
}
