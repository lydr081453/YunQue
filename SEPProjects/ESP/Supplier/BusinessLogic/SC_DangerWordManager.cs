using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Supplier.Entity;
using ESP.Supplier.DataAccess;
using System.Data;
using System.Data.SqlClient;

namespace ESP.Supplier.BusinessLogic
{
    public class SC_DangerWordManager
    {
        private static readonly SC_DangerWordDataProvider dal = new SC_DangerWordDataProvider();
        public SC_DangerWordManager()
        { }

        #region  成员方法

        /// <summary>
        /// 添加一条数据
        /// </summary>
        public static void Add(SC_DangerWord model)
        {
            dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static void Update(SC_DangerWord model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static void Delete(int Id)
        {
            dal.Delete(Id);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static SC_DangerWord GetModel(int Id)
        {
            return dal.GetModel(Id);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }

        public static IList<SC_DangerWord> GetList(string strWhere, SqlParameter[] parameters)
        {
            return dal.GetList(strWhere, parameters);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static DataSet GetAllList()
        {
            return dal.GetList("");
        }

        public static IList<SC_DangerWord> GetAllLists()
        {
            return ESP.ConfigCommon.CBO.FillCollection<SC_DangerWord>(GetAllList());
        }



        public static bool IsDanger(string body)
        {
            IList<SC_DangerWord> list = GetAllLists();
            if (list != null && list.Count > 0)
            {
                foreach (SC_DangerWord dword in list)
                {
                    if (body.ToLower().IndexOf(dword.Word.ToLower()) > -1)
                    {
                        return true;
                    }
                }
                return false;
            }
            return false;
        }
        #endregion  成员方法
    }
}
