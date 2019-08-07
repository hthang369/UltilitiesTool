using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using System.Collections.Generic;

namespace SQLTool.ViewModels
{
    [POCOViewModel]
    public class SqlControlViewModel : BaseViewModel
    {
        private List<string> _lstSqlType;
        public List<string> lstSqlType
        {
            get => _lstSqlType;
            set => SetProperty<List<string>>(ref _lstSqlType, value);
        }
        private List<string> _lstServers;
        public List<string> lstServers
        {
            get => _lstServers;
            set => SetProperty<List<string>>(ref _lstServers, value);
        }

        public SqlControlViewModel()
        {
            lstSqlType = new List<string>()
            {
                SQLAppLib.SqlDbConnectionType.MySql.ToString(),
                SQLAppLib.SqlDbConnectionType.Sql.ToString(),
            };
        }
    }
}