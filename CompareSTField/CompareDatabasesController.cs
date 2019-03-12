using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
//using ExpertLib;
using System.Windows.Forms;
using CreateCompany;

namespace CreateCompany
{	
	public class CompareDatabasesController
	{
        public CompareDatabasesController()
		{
		}

        public DataSet GetDataSet(String strQuery)
        {
            return SQLConnect.RunQuery(strQuery);
        }

        public DataSet DetailField_IsExitSource(string Module, string Group)
        {
            String strcom = string.Format(@"select STFieldID,STFieldName,'FALSE' as STFieldKhac  from STFields where STScreenID in (select STScreenID from STScreens where STModuleID in (select STModuleID from STModules where STModuleName='{0}') and STUserGroupID = ( select ADUserGroupID from ADUserGroups where ADUserGroupID = {1}))", Module, Group);
            return GetDataSet(strcom);
            //return DataSet;
           // return SQLConnect.RunQuery(strcom);
        }
	}
}