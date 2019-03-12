using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace CreateCompany
{
    public class CompareFromTo
    {
        private List<DataRow> dataRowFrom;
        private List<DataRow> dataRowTo;
        public CompareFromTo()
        {
            dataRowFrom = new List<DataRow>();
            dataRowTo = new List<DataRow>();
        }
        public CompareFromTo(List<DataRow> listFrom,List<DataRow> listTo)
        {
            dataRowFrom = listFrom;
            dataRowTo = listTo;
        }
        public List<DataRow> DataRowFrom
        {
            get { return dataRowFrom; }
            set { dataRowFrom = value; }
        }
        public List<DataRow> DataRowTo
        {
            get { return dataRowTo; }
            set { dataRowTo = value; }
        }

    }
}
