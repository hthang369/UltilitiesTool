using DevExpress.Xpf.Core;
using SQLTool.Util;
using System;
using System.Collections.Generic;
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
        
        public void KeyBindingActionCommand(object[] x)
        {
            DevExpress.Xpf.Grid.TableView objTableView = x.FirstOrDefault() as DevExpress.Xpf.Grid.TableView;
            if (objTableView == null) return;
            HotKeyInfo hotKey = HotKeyGenerate.GenerateHotKeyByString(Convert.ToString(x.LastOrDefault()));
            object focusVal = objTableView.Grid.GetCellValue(objTableView.FocusedRowHandle, objTableView.FocusedColumn);
            switch (hotKey.modifierKey)
            {
                case ModifierKeys.Alt:
                    switch (hotKey.key)
                    {
                        case Key.C:
                            Clipboard.SetDataObject(focusVal);
                            break;
                    }
                    break;
                case ModifierKeys.Control:
                    switch (hotKey.key)
                    {
                        case Key.M:
                            string strColumn = string.Empty;
                            PromptForm.ShowText("Find Column", "Find Column", ref strColumn);
                            if (!string.IsNullOrEmpty(strColumn))
                            {
                                objTableView.Grid.Columns.ToList().ForEach(col =>
                                {
                                    if (!col.FieldName.Equals("id"))
                                        col.Visible = false;
                                    strColumn.Split(',').ToList().ForEach(c =>
                                    {
                                        if (col.FieldName.Equals(c))
                                            col.Visible = true;
                                    });
                                });
                            }
                            break;
                        case Key.H:
                            int idx = objTableView.Grid.Columns.ToList().FindIndex(c => c.FieldName.Equals(objTableView.FocusedColumn.FieldName));
                            DevExpress.Xpf.Grid.GridColumn column = objTableView.Grid.Columns.ToList().ElementAtOrDefault(idx + 1);
                            objTableView.FocusedColumn.Visible = false;
                            if (column != null)
                                objTableView.FocusedColumn = column;
                            break;
                    }
                    break;
                case ModifierKeys.Shift:
                    break;
                case ModifierKeys.None:
                    switch (hotKey.key)
                    {
                        case Key.F:
                            objTableView.FocusedColumn.AutoFilterValue = focusVal;
                            break;
                    }
                    break;
            }
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
