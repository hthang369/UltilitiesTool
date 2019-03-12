using System;
using System.Collections.Generic;
using System.Text;

namespace ConnectDatabase
{
    public class Tables
    {
        Boolean _selected;
        String _table_Name;
        Boolean _isExist;
        Boolean _isMoreColumn;
        string _type;

        public Tables()
        {

        }

        public Tables(Boolean selected, String table_name, Boolean isExist)
        {
            _selected = selected;
            _table_Name = table_name;
            _isExist = isExist;
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
        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }
        #endregion
    }
}
