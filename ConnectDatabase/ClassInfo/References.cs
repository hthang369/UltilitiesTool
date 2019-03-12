using System;
using System.Collections.Generic;
using System.Text;

namespace ConnectDatabase
{
    public class References
    {
        Boolean _selected;
        String _foreignKey;
        String _tableNamePrimaryKey;
        String _columnNamePrimaryKey;
        String _tableNameForeignKey;
        String _columnNameForeignKey;

        public References()
        {

        }

        public References(Boolean selected, String foreignkey, String tablenameprimarykey, String columnnameprimarykey, String tablenameforeignkey, String columnnameforeignkey)
        {
            _selected = selected;
            _foreignKey = foreignkey;
            _tableNamePrimaryKey = tablenameprimarykey;
            _columnNamePrimaryKey = columnnameprimarykey;
            _tableNameForeignKey = tablenameforeignkey;
            _columnNameForeignKey = columnnameforeignkey;
        }

        #region Property

        public Boolean Selected
        {
            get { return _selected; }
            set { _selected = value; }
        }
        public String ForeignKey
        {
            get { return _foreignKey; }
            set { _foreignKey = value; }
        }
        public String TableNamePrimaryKey
        {
            get { return _tableNamePrimaryKey; }
            set { _tableNamePrimaryKey = value; }
        }
        public String ColumnNamePrimaryKey
        {
            get { return _columnNamePrimaryKey; }
            set { _columnNamePrimaryKey = value; }
        }
        public String TableNameForeignKey
        {
            get { return _tableNameForeignKey; }
            set { _tableNameForeignKey = value; }
        }
        public String ColumnNameForeignKey
        {
            get { return _columnNameForeignKey; }
            set { _columnNameForeignKey = value; }
        }

        #endregion
    }
}
