using DevExpress.Xpf.Core;
using SQLTool.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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

        private ObservableCollection<DXTabItem> _lstTabItems;
        public ObservableCollection<DXTabItem> lstTabItems
        {
            get => _lstTabItems;
            set => SetProperty(ref _lstTabItems, value);
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
