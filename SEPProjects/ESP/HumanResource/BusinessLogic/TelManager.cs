using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.HumanResource.Entity;
using System.Data.SqlClient;
using System.Data;

namespace ESP.HumanResource.BusinessLogic
{
    /// <summary>
    /// 分机
    /// </summary>
    public class TelManager
    {
         private static readonly ESP.HumanResource.DataAccess.TelDataProvider dal = new ESP.HumanResource.DataAccess.TelDataProvider();
         public TelManager()
        { }

        public static bool Exists(int id)
        {
            return dal.Exists(id);
        }
        public static int Add(TELInfo model, SqlConnection conn, SqlTransaction trans)
        {
            return dal.Add(model, conn, trans);
        }
        public static int Update(TELInfo model, SqlConnection conn, SqlTransaction trans)
        {
            return dal.Update(model, conn, trans);
        }

        public  static int Update(TELInfo model, SqlTransaction trans)
        {
            return dal.Update(model, trans);
        }

        public static int Update(TELInfo model)
        {
            return dal.Update(model, null);
        }

        public void Delete(int id, SqlConnection conn, SqlTransaction trans)
        {
             dal.Delete(id, conn, trans);
        }


        public static TELInfo GetModel(int id)
        {
            return dal.GetModel(id);
        }

        public static TELInfo GetModel(string tel)
        {
            return dal.GetModel(tel);
        }

        public static  DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }

        public static  List<TELInfo> GetModelList(string strWhere, List<SqlParameter> parms)
        {
            return dal.GetModelList(strWhere, parms);
        }

        public static List<TELInfo> GetModelList(string strWhere)
        {
            return dal.GetModelList(strWhere);
        }
    }
}
