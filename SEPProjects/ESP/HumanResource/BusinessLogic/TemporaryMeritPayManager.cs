using System;
using System.Data;
using System.Collections.Generic;
using ESP.HumanResource.Entity;
using ESP.HumanResource.Common;
using System.Data.SqlClient;
using ESP.HumanResource.DataAccess;

namespace ESP.HumanResource.BusinessLogic
{
    public class TemporaryMeritPayManager
    {
        private readonly static TemporaryMeritPayDataProvider dal = new TemporaryMeritPayDataProvider();
		public TemporaryMeritPayManager()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public static bool Exists(int ID)
		{
			return dal.Exists(ID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
        public static int Add(TemporaryMeritPayInfo model)
		{
			return dal.Add(model);
		}

        /// <summary>
        /// 添加导入的批量数据
        /// </summary>
        public static int AddandUpdate(List<TemporaryMeritPayInfo> list1, List<TemporaryMeritPayInfo> list2, LogInfo logModel)
        {
            int returnValue = 0;
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    foreach (TemporaryMeritPayInfo tp2 in list2)
                    {
                        returnValue = dal.Add(tp2, conn, trans);
                    }
                    foreach (TemporaryMeritPayInfo tp1 in list1)
                    {
                        
                        returnValue = dal.Update(tp1, conn, trans);
                    }
                    LogManager.AddLog(logModel, trans);
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    ESP.Logging.Logger.Add(ex.ToString());
                    returnValue = 0;
                }
            }
            return returnValue;
        }


		/// <summary>
		/// 更新一条数据
		/// </summary>
        public static void Update(TemporaryMeritPayInfo model)
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
        public static TemporaryMeritPayInfo GetModel(int ID)
		{
			return dal.GetModel(ID);
		}

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static TemporaryMeritPayInfo GetModel(string Code)
        {
            return dal.GetModel(Code);
        }

        /// <summary>
        /// 得到绩效变动
        /// </summary>
        public static string GetMeritPay(int UserID, int Year, int Month)
        {
            return dal.GetMeritPay(UserID,Year,Month);
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
        public static DataSet GetAllList()
		{
			return dal.GetList("");
		}

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static List<TemporaryMeritPayInfo> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            List<TemporaryMeritPayInfo> modelList = new List<TemporaryMeritPayInfo>();
            int rowsCount = ds.Tables[0].Rows.Count;
            if (rowsCount > 0)
            {
                TemporaryMeritPayInfo model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new TemporaryMeritPayInfo();
                    if (ds.Tables[0].Rows[n]["ID"].ToString() != "")
                    {
                        model.ID = int.Parse(ds.Tables[0].Rows[n]["ID"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["UserID"].ToString() != "")
                    {
                        model.UserID = int.Parse(ds.Tables[0].Rows[n]["UserID"].ToString());
                    }
                    model.Code = ds.Tables[0].Rows[n]["Code"].ToString();
                    model.MeritPay = ds.Tables[0].Rows[n]["MeritPay"].ToString();
                    if (ds.Tables[0].Rows[n]["ImplementYear"].ToString() != "")
                    {
                        model.ImplementYear = int.Parse(ds.Tables[0].Rows[n]["ImplementYear"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["ImplementMonth"].ToString() != "")
                    {
                        model.ImplementMonth = int.Parse(ds.Tables[0].Rows[n]["ImplementMonth"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["Creator"].ToString() != "")
                    {
                        model.Creator = int.Parse(ds.Tables[0].Rows[n]["Creator"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["CreateDate"].ToString() != "")
                    {
                        model.CreateDate = DateTime.Parse(ds.Tables[0].Rows[n]["CreateDate"].ToString());
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }




		#endregion  成员方法
    }
}
