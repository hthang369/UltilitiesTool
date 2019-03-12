using System;
using System.Collections.Generic;
using System.Text;

namespace ConnectDatabase
{
    public class AllDataSources
    {
        Boolean _selected;
        String _table_Name;
        Boolean _isExist;
        Boolean _isMoreColumn;
        List<Columns> _listColumns;
        List<References> _listReferences;

        public AllDataSources()
        {

        }

        public AllDataSources(Boolean selected, String table_name, Boolean isExist)
        {
            _selected = selected;
            _table_Name = table_name;
            _isExist = isExist;
            _listColumns = new List<Columns>();
            _listReferences = new List<References>();
            _isMoreColumn = false;
        }

        #region Property

        public Boolean Selected
        {
            get { return _selected; }
            set { _selected = value; }
        }
        public String Table_Name
        {
            get { return _table_Name; }
            set { _table_Name = value; }
        }
        public Boolean IsExist
        {
            get { return _isExist; }
            set { _isExist = value; }
        }
        public Boolean IsMoreColumn
        {
            get { return _isMoreColumn; }
            set { _isMoreColumn = value; }
        }
        public List<Columns> ListColumns
        {
            get { return _listColumns; }
            set { _listColumns = value; }
        }
        public List<References> ListReferences
        {
            get { return _listReferences; }
            set { _listReferences = value; }
        }
        #endregion
    }
}
