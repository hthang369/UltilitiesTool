using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using System.Data;
using System.Threading.Tasks;

namespace SQLTool.ViewModels
{
    [POCOViewModel]
    public class ResultDataViewModel : BasePopupViewModel
    {
        private DataTable _dataSource;
        public DataTable dataSource
        {
            get => _dataSource;
            set => SetProperty(ref _dataSource, value);
        }

        public ResultDataViewModel()
        {
            //dataSource = SQLAppLib.SQLDBUtil.GetDataTable("SHOW VARIABLES LIKE '%version%'");
        }

        public void RunQuery(string strQuery)
        {
            Task.Factory.StartNew(() =>
            {
                dataSource = SQLAppLib.SQLDBUtil.GetDataTable(strQuery);
            }).Wait();
        }
    }
}