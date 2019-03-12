using System;
using System.Collections.Generic;
using System.Text;

namespace ConnectDatabase
{
    public class Columns
    {
        Boolean _selected;
        String _table_Name;
        String _column_Name;
        String _is_NullLable;
        String _data_Type;
        String _character_Maximum_Length;
        int _ordinal_Position;

        public Columns()
        {

        }

        public Columns(Boolean selected, String table_name, String column_name, String is_nulllable, String data_type, String character_maximum_length, int ordinal_position)
        {
            _selected=selected;
            _table_Name=table_name;
            _column_Name=column_name;
            _is_NullLable=is_nulllable;
            _data_Type=data_type;
            _character_Maximum_Length=character_maximum_length;
            _ordinal_Position = ordinal_position;
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

        public String Column_Name
        {
            get { return _column_Name; }
            set { _column_Name = value; }
        }

        public String Is_NullLable
        {
            get { return _is_NullLable; }
            set { _is_NullLable = value; }
        }

        public String Data_Type
        {
            get { return _data_Type; }
            set { _data_Type = value; }
        }

        public String Character_Maximum_Length
        {
            get { return _character_Maximum_Length; }
            set { _character_Maximum_Length = value; }
        }
        public int Ordinal_Position
        {
            get { return _ordinal_Position; }
            set { _ordinal_Position = value; }
        }
        #endregion
    }
}
