using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using ExpertLib;
using System.IO;
using System.Data;
using ConnectDatabase;

namespace SetTabIndex
{
    public class SetTabIndexController
    {
        public DataSet getChildControl(int stfieldparentID, int screenid,string aligntype)
        {
            string sqlquery="";
            if(aligntype=="TopLeft")
                sqlquery = string.Format(@"select * from STFields where STFieldParentID={0} and STScreenID={1} order by STFieldLocationY, STFieldLocationX", stfieldparentID, screenid);
            else if(aligntype=="LeftTop")
                sqlquery = string.Format(@"select * from STFields where STFieldParentID={0} and STScreenID={1} order by STFieldLocationX, STFieldLocationY", stfieldparentID, screenid);
            return SQLConnect.RunQuery(sqlquery);
        }
        public void UpdateTabIndex(int STFieldID, int index)
        {
            CompareDatabasesController c = new CompareDatabasesController();

            string qerry = String.Format(@"update STFields set STFieldTabIndex={0} where STFieldID={1}", index, STFieldID);
            c.GetDataSet(qerry);
        }
        public DataSet getScreenOfModule(int STModuleID)
        {
            CompareDatabasesController c = new CompareDatabasesController();
            string query = string.Format(@"select * from STScreens where STModuleID={0}", STModuleID);
            return c.GetDataSet(query);
        }
        public void setAllTabIndexToDefaut(int stscreenid)
        {
            CompareDatabasesController c = new CompareDatabasesController();
            string query = string.Format(@"update STFields set STFieldTabIndex=1000 where STScreenID={0}", stscreenid);
            c.GetDataSet(query);
        }     
        public DataSet GetModules()
        {
            string sqlquery = "select * from STModules";
            return SQLConnect.RunQuery(sqlquery);
        }
        public DataSet GetUserGroups()
        {
            string sqlquery = "select * from ADUserGroups where AAStatus='Alive'";
            return SQLConnect.RunQuery(sqlquery);
        }
        public DataSet GetGroupControls(int screenID)
        {
            string sqlquery = string.Format(@"select * from STFields where STScreenID = '{0}' and STFieldType in ('{1}','{2}','{3}')", screenID, "GMCGroupControl", "GMCPanelControl", "GMCTabPageControl");
            return SQLConnect.RunQuery(sqlquery);
        }
        public DataSet GetScreenByModuleAndUserGroup(int moduleID, int UserGroupID)
        {
            if (UserGroupID == 0) return getScreenOfModule(moduleID);
            string sqlquery = string.Format(@"select * from STScreens where STModuleID={0} and STUserGroupID={1}", moduleID, UserGroupID);
            return SQLConnect.RunQuery(sqlquery);
        }
        public DataSet GetFieldsByScreen(int screenID)
        {
            string sqlquery = string.Format(@"select * from STFields where STScreenID={0}", screenID);
            return SQLConnect.RunQuery(sqlquery);
        }
    }
}
