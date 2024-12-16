using System;
using System.Data;
using System.Collections.Generic;
using ESP.HumanResource.Entity;
using ESP.HumanResource.Common;
using System.Data.SqlClient;
using ESP.HumanResource.DataAccess;

namespace ESP.HumanResource.BusinessLogic
{
    public class StatisticsTypeManager
    {
        private static readonly StatisticsTypeDataProvider dal = new StatisticsTypeDataProvider();
        public StatisticsTypeManager()
        { }
        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(StatisticsTypeInfo model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static void Update(StatisticsTypeInfo model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static void Delete(int ID)
        {

            dal.Delete(ID);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static StatisticsTypeInfo GetModel(int ID)
        {

            return dal.GetModel(ID);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static List<StatisticsTypeInfo> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            List<StatisticsTypeInfo> modelList = new List<StatisticsTypeInfo>();
            int rowsCount = ds.Tables[0].Rows.Count;
            if (rowsCount > 0)
            {
                StatisticsTypeInfo model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new StatisticsTypeInfo();
                    if (ds.Tables[0].Rows[n]["ID"].ToString() != "")
                    {
                        model.ID = int.Parse(ds.Tables[0].Rows[n]["ID"].ToString());
                    }
                    model.StatisticsTypeName = ds.Tables[0].Rows[n]["StatisticsTypeName"].ToString();
                    model.TypeID = ds.Tables[0].Rows[n]["TypeID"].ToString();
                    if (ds.Tables[0].Rows[n]["IsDeleted"].ToString() != "")
                    {
                        if ((ds.Tables[0].Rows[n]["IsDeleted"].ToString() == "1") || (ds.Tables[0].Rows[n]["IsDeleted"].ToString().ToLower() == "true"))
                        {
                            model.IsDeleted = true;
                        }
                        else
                        {
                            model.IsDeleted = false;
                        }
                    }
                    model.StatisticsTypeValue = ds.Tables[0].Rows[n]["StatisticsTypeValue"].ToString();
                    modelList.Add(model);
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }


        #endregion  成员方法
    }
}
