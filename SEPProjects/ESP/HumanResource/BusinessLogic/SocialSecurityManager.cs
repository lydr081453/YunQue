using System;
using System.Data;
using System.Collections.Generic;
using ESP.HumanResource.Entity;
using ESP.HumanResource.Common;
using System.Data.SqlClient;
using ESP.HumanResource.DataAccess;

namespace ESP.HumanResource.BusinessLogic
{
    public class SocialSecurityManager
    {
        private readonly static SocialSecurityDataProvider dal=new SocialSecurityDataProvider();
		public SocialSecurityManager()
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
        public static int Add(SocialSecurityInfo model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
        public static int Update(SocialSecurityInfo model)
		{
			return dal.Update(model);
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
        public static SocialSecurityInfo GetModel(int ID)
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
        public static DataSet GetAllList()
		{
			return dal.GetList("");
		}

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static List<SocialSecurityInfo> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            List<SocialSecurityInfo> modelList = new List<SocialSecurityInfo>();
            int rowsCount = ds.Tables[0].Rows.Count;
            if (rowsCount > 0)
            {
                SocialSecurityInfo model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new SocialSecurityInfo();
                    if (ds.Tables[0].Rows[n]["ID"].ToString() != "")
                    {
                        model.ID = int.Parse(ds.Tables[0].Rows[n]["ID"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["EIProportionOfFirms"].ToString() != "")
                    {
                        model.EIProportionOfFirms = decimal.Parse(ds.Tables[0].Rows[n]["EIProportionOfFirms"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["EIProportionOfIndividuals"].ToString() != "")
                    {
                        model.EIProportionOfIndividuals = decimal.Parse(ds.Tables[0].Rows[n]["EIProportionOfIndividuals"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["UIProportionOfFirms"].ToString() != "")
                    {
                        model.UIProportionOfFirms = decimal.Parse(ds.Tables[0].Rows[n]["UIProportionOfFirms"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["UIProportionOfIndividuals"].ToString() != "")
                    {
                        model.UIProportionOfIndividuals = decimal.Parse(ds.Tables[0].Rows[n]["UIProportionOfIndividuals"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["BIProportionOfFirms"].ToString() != "")
                    {
                        model.BIProportionOfFirms = decimal.Parse(ds.Tables[0].Rows[n]["BIProportionOfFirms"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["BIProportionOfIndividuals"].ToString() != "")
                    {
                        model.BIProportionOfIndividuals = decimal.Parse(ds.Tables[0].Rows[n]["BIProportionOfIndividuals"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["CIProportionOfFirms"].ToString() != "")
                    {
                        model.CIProportionOfFirms = decimal.Parse(ds.Tables[0].Rows[n]["CIProportionOfFirms"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["CIProportionOfIndividuals"].ToString() != "")
                    {
                        model.CIProportionOfIndividuals = decimal.Parse(ds.Tables[0].Rows[n]["CIProportionOfIndividuals"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["MIProportionOfFirms"].ToString() != "")
                    {
                        model.MIProportionOfFirms = decimal.Parse(ds.Tables[0].Rows[n]["MIProportionOfFirms"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["MIProportionOfIndividuals"].ToString() != "")
                    {
                        model.MIProportionOfIndividuals = decimal.Parse(ds.Tables[0].Rows[n]["MIProportionOfIndividuals"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["MIBigProportionOfIndividuals"].ToString() != "")
                    {
                        model.MIBigProportionOfIndividuals = decimal.Parse(ds.Tables[0].Rows[n]["MIBigProportionOfIndividuals"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["PRFProportionOfFirms"].ToString() != "")
                    {
                        model.PRFProportionOfFirms = decimal.Parse(ds.Tables[0].Rows[n]["PRFProportionOfFirms"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["PRFProportionOfIndividuals"].ToString() != "")
                    {
                        model.PRFProportionOfIndividuals = decimal.Parse(ds.Tables[0].Rows[n]["PRFProportionOfIndividuals"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["BeginTime"].ToString() != "")
                    {
                        model.BeginTime = DateTime.Parse(ds.Tables[0].Rows[n]["BeginTime"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["EndTime"].ToString() != "")
                    {
                        model.EndTime = DateTime.Parse(ds.Tables[0].Rows[n]["EndTime"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["SocialInsuranceCompany"].ToString() != "")
                    {
                        model.SocialInsuranceCompany = int.Parse(ds.Tables[0].Rows[n]["SocialInsuranceCompany"].ToString());
                    }
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
                    if (ds.Tables[0].Rows[n]["lastUpdateTime"].ToString() != "")
                    {
                        model.lastUpdateTime = DateTime.Parse(ds.Tables[0].Rows[n]["lastUpdateTime"].ToString());
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }


		#endregion  成员方法
    }
}
