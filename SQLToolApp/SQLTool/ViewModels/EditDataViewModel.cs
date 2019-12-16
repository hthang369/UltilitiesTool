using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;

namespace SQLTool.ViewModels
{
    [POCOViewModel]
    public class EditDataViewModel : BasePopupViewModel
    {
        private object _strQuery;
        public object strQuery
        {
            get => _strQuery;
            set => SetProperty(ref _strQuery, value);
        }
        Views.EditDataView view;
        public EditDataViewModel(Views.EditDataView _view)
        {
            view = _view;
        }
        protected override void KeyBindingActionCommand(object x)
        {
            
            Util.FunctionList.ShowResultDataView(view.reditData.Text);
        }
    }
}