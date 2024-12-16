using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ESP.Administrative.Entity;
using ESP.Administrative.DataAccess;
using System.Data.SqlClient;

namespace ESP.Administrative.BusinessLogic
{
    public class TalentManager
    {
         private readonly TalentDataProvider dal = new TalentDataProvider();
         public TalentManager()
        { }

        public TalentInfo GetModel(int ID)
        {
            return dal.GetModel(ID);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<TalentInfo> GetList(string strWhere ,string orderBy)
        {
            return dal.GetList(strWhere,orderBy);
        }
    }
}
