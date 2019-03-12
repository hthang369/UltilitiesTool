using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using ExpertLib;
using System.IO;
using System.Data;

namespace SetTabIndex
{
    public class TreeFieldNode
    {
        private STFieldsInfo _stfield;
        private List<TreeFieldNode> _listTreeFieldNodeChild;

        public STFieldsInfo STField
        {
            get { return _stfield; }
            set { _stfield = value; }
        }
        public List<TreeFieldNode> ListTreeFieldNodeChild
        {
            get { return _listTreeFieldNodeChild; }
            set { _listTreeFieldNodeChild = value; }
        }

        public TreeFieldNode()
        {
            _listTreeFieldNodeChild = new List<TreeFieldNode>();
        }
        public TreeFieldNode(STFieldsInfo STFieldInfo)
        {
            _stfield = STFieldInfo;
            _listTreeFieldNodeChild = new List<TreeFieldNode>();
        }

    }
}
