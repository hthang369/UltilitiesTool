using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompareSTFields
{
    class CSTFields
    {
        private int _sTFieldID;
        private String _sTFieldName;
        private bool _IsExitSource;
        private bool _IsDestination;


        public CSTFields()
        {
            _sTFieldID = 0;
            _sTFieldName = "";
            _IsExitSource = false;
            _IsDestination = false;
        }

        public CSTFields(int mSTFieldID, string mSTFieldName, bool mIsExitSource, bool mIsDestination)
        {
            _sTFieldID = mSTFieldID;
            _sTFieldName = mSTFieldName;
            _IsExitSource = mIsExitSource;
            _IsDestination = mIsDestination;
        }

        public int STFieldID
        {
            get { return _sTFieldID; }
            set { _sTFieldID = value; }
        }

        public String STFieldName
        {
            get { return _sTFieldName; }
            set { _sTFieldName = value; }
        }


        public bool IsExitSource
        {
            get { return _IsExitSource; }
            set { _IsExitSource = value; }
        }

        public bool IsDestination
        {
            get { return _IsDestination; }
            set { _IsDestination = value; }
        }

    }

    
}
