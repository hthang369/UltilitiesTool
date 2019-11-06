using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SQLTool.ViewModels
{
    public class BasePopupViewModel : BaseViewModel
    {
        private string _header;
        public string Header
        {
            get => _header;
            set => SetProperty(ref _header, value);
        }
        private Visibility _isTabControl;
        public Visibility isTabControl
        {
            get => _isTabControl;
            set => SetProperty(ref _isTabControl, value);
        }
        private Visibility _isNoTabControl;
        public Visibility isNoTabControl
        {
            get => _isNoTabControl;
            set => SetProperty(ref _isNoTabControl, value);
        }
    }
}
