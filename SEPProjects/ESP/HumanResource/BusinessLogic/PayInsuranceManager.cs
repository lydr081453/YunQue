using System;
using System.Data;
using System.Collections.Generic;
using ESP.HumanResource.Entity;
using ESP.HumanResource.Common;
using System.Data.SqlClient;
using ESP.HumanResource.DataAccess;

namespace ESP.HumanResource.BusinessLogic
{
    public class PayInsuranceManager
    {
        private readonly static PayInsuranceDataProvider dal=new PayInsuranceDataProvider();
		public PayInsuranceManager()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			return dal.Exists(ID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
        public static int Add(PayInsuranceInfo model)
		{
			return dal.Add(model);
		}

        /// <summary>
        /// 增加一组数据
        /// </summary>
        public static int Add(PayInsuranceInfo model, LogInfo logModel)
        {
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    if (model != null)
                    {
                        dal.Add(model, trans);
                        
                        LogManager.AddLog(logModel, trans);
                        trans.Commit();
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }

                }
                catch (Exception ex)
                {
                    ESP.Logging.Logger.Add("添加社保福利补发信息错误","HR",ESP.Logging.LogLevel.Error,ex);
                    trans.Rollback();
                    return 0;
                }
            }
        }
		/// <summary>
		/// 更新一条数据
		/// </summary>
        public static void Update(PayInsuranceInfo model)
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
        public static PayInsuranceInfo GetModel(int ID)
		{
			return dal.GetModel(ID);
		}

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static PayInsuranceInfo GetModel(string strWhere)
        {
            return dal.GetModel(strWhere);
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
        public static List<PayInsuranceInfo> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            List<PayInsuranceInfo> modelList = new List<PayInsuranceInfo>();
            int rowsCount = ds.Tables[0].Rows.Count;
            if (rowsCount > 0)
            {
                PayInsuranceInfo model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new PayInsuranceInfo();
                    if (ds.Tables[0].Rows[n]["ID"].ToString() != "")
                    {
                        model.ID = int.Parse(ds.Tables[0].Rows[n]["ID"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["UserID"].ToString() != "")
                    {
                        model.UserID = int.Parse(ds.Tables[0].Rows[n]["UserID"].ToString());
                    }
                    model.FullNameCn = ds.Tables[0].Rows[n]["FullNameCn"].ToString();
                    if (ds.Tables[0].Rows[n]["EndowmentInsurance"].ToString() != "")
                    {
                        model.EndowmentInsurance = (ds.Tables[0].Rows[n]["EndowmentInsurance"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["UnemploymentInsurance"].ToString() != "")
                    {
                        model.UnemploymentInsurance = (ds.Tables[0].Rows[n]["UnemploymentInsurance"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["MedicalInsurance"].ToString() != "")
                    {
                        model.MedicalInsurance = (ds.Tables[0].Rows[n]["MedicalInsurance"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["PublicReserveFunds"].ToString() != "")
                    {
                        model.PublicReserveFunds = (ds.Tables[0].Rows[n]["PublicReserveFunds"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["PayYear"].ToString() != "")
                    {
                        model.PayYear = int.Parse(ds.Tables[0].Rows[n]["PayYear"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["PayMonth"].ToString() != "")
                    {
                        model.PayMonth = int.Parse(ds.Tables[0].Rows[n]["PayMonth"].ToString());
                    }
                    model.Remark = ds.Tables[0].Rows[n]["Remark"].ToString();
                    if (ds.Tables[0].Rows[n]["Creator"].ToString() != "")
                    {
                        model.Creator = int.Parse(ds.Tables[0].Rows[n]["Creator"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["CreateTime"].ToString() != "")
                    {
                        model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[n]["CreateTime"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["LastUpdateMan"].ToString() != "")
                    {
                        model.LastUpdateMan = int.Parse(ds.Tables[0].Rows[n]["LastUpdateMan"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["LastUpdateTime"].ToString() != "")
                    {
                        model.LastUpdateTime = DateTime.Parse(ds.Tables[0].Rows[n]["LastUpdateTime"].ToString());
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得当前用户相同公司下的人员列表
        /// </summary>
        /// <param name="UserInfo">当前用户</param>
        /// <param name="strWhere">查询条件</param>
        /// <returns></returns>
        public static List<PayInsuranceInfo> GetUserModelList(ESP.Framework.Entity.UserInfo UserInfo, string strWhere)
        {
            if (UserInfo != null)
            {
                //取当先用户的所有部门
                List<ESP.HumanResource.Entity.EmployeesInPositionsInfo> empinpos = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetModelList(" a.userid=" + UserInfo.UserID);
                string empid = "";
                string strSql = "";
                for (int i = 0; i < empinpos.Count; i++)
                {
                    empid += empinpos[i].CompanyID.ToString() + ",";
                }
                if (empid.Length > 0)
                {
                    empid = empid.Substring(0, empid.Length - 1);
                    strSql += string.Format(" and c.level1ID in ({0})", empid);
                }

                //取出所有人
                List<PayInsuranceInfo> list = GetModelList(strWhere);

                //给多部门的人付部门
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    //获得和登陆用户相同公司的人员部门
                    List<ESP.HumanResource.Entity.EmployeesInPositionsInfo> empinposlist = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetModelList(string.Format(" a.userid={0} " + strSql, list[i].UserID));
                    if (empinposlist.Count > 0)
                    {
                       // list[i].EmployeesInPositionsList = empinposlist;
                    }
                    else
                    {
                        //移除与当前用户不同公司的人
                        list.RemoveAt(i);
                    }

                }

                return list;
            }
            else
            {
                return null;
            }
        }

		#endregion  成员方法
    }
}
