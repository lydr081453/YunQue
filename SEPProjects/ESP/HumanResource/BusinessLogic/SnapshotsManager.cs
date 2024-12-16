using System;
using System.Data;
using System.Collections.Generic;
using ESP.HumanResource.Entity;
using ESP.HumanResource.Common;
using System.Data.SqlClient;
using ESP.HumanResource.DataAccess;

namespace ESP.HumanResource.BusinessLogic
{    
    public class SnapshotsManager
    {
        private static SnapshotsDataProvider dal = new SnapshotsDataProvider();
        public SnapshotsManager()
        { }
        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(SnapshotsInfo model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 增加一组数据
        /// </summary>
        public static bool Add(List<SnapshotsInfo> snap, LogInfo logModel)
        {
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    if (snap != null)
                    {
                        foreach (SnapshotsInfo model in snap)
                        {
                            dal.Add(model, trans);
                        }
                        LogManager.AddLog(logModel, trans);
                        trans.Commit();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    
                }
                catch (Exception ex)
                {
                    ESP.Logging.Logger.Add(ex.ToString());
                    trans.Rollback();
                    return false;
                }
            }
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static void Update(SnapshotsInfo model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static void Delete(int id)
        {

            dal.Delete(id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(SnapshotsInfo model, SqlTransaction trans)
        {
            return dal.Add(model, trans);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static void Update(SnapshotsInfo model, SqlTransaction trans)
        {
            dal.Update(model, trans);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static void Delete(int id, SqlTransaction trans)
        {

            dal.Delete(id, trans);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static SnapshotsInfo GetModel(int id)
        {

            return dal.GetModel(id);
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
        public static List<SnapshotsInfo> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            List<SnapshotsInfo> modelList = new List<SnapshotsInfo>();
            int rowsCount = ds.Tables[0].Rows.Count;
            if (rowsCount > 0)
            {
                SnapshotsInfo model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new SnapshotsInfo();
                    if (ds.Tables[0].Rows[n]["id"].ToString() != "")
                    {
                        model.id = int.Parse(ds.Tables[0].Rows[n]["id"].ToString());
                    }
                    model.Education = ds.Tables[0].Rows[n]["Education"].ToString();
                    model.GraduatedFrom = ds.Tables[0].Rows[n]["GraduatedFrom"].ToString();
                    model.Major = ds.Tables[0].Rows[n]["Major"].ToString();
                    if (ds.Tables[0].Rows[n]["ThisYearSalary"].ToString() != "")
                    {
                        model.ThisYearSalary = ds.Tables[0].Rows[n]["ThisYearSalary"].ToString();
                    }
                    if (ds.Tables[0].Rows[n]["Status"].ToString() != "")
                    {
                        model.Status = int.Parse(ds.Tables[0].Rows[n]["Status"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["UserID"].ToString() != "")
                    {
                        model.UserID = int.Parse(ds.Tables[0].Rows[n]["UserID"].ToString());
                    }
                    
                        model.nowBasePay = ds.Tables[0].Rows[n]["nowBasePay"].ToString();
                    
                        model.nowMeritPay = ds.Tables[0].Rows[n]["nowMeritPay"].ToString();
                    
                        model.newBasePay = ds.Tables[0].Rows[n]["newBasePay"].ToString();
                    
                    model.Code = ds.Tables[0].Rows[n]["Code"].ToString();
                    
                        model.newMeritPay = ds.Tables[0].Rows[n]["newMeritPay"].ToString();
                    
                    if (ds.Tables[0].Rows[n]["Creator"].ToString() != "")
                    {
                        model.Creator = int.Parse(ds.Tables[0].Rows[n]["Creator"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["CreatedTime"].ToString() != "")
                    {
                        model.CreatedTime = DateTime.Parse(ds.Tables[0].Rows[n]["CreatedTime"].ToString());
                    }
                    model.UserName = ds.Tables[0].Rows[n]["UserName"].ToString();
                    model.CreatorName = ds.Tables[0].Rows[n]["CreatorName"].ToString();
                    if (ds.Tables[0].Rows[n]["endowmentInsuranceStarTime"].ToString() != "")
                    {
                        model.endowmentInsuranceStarTime = DateTime.Parse(ds.Tables[0].Rows[n]["endowmentInsuranceStarTime"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["endowmentInsuranceEndTime"].ToString() != "")
                    {
                        model.endowmentInsuranceEndTime = DateTime.Parse(ds.Tables[0].Rows[n]["endowmentInsuranceEndTime"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["unemploymentInsuranceStarTime"].ToString() != "")
                    {
                        model.unemploymentInsuranceStarTime = DateTime.Parse(ds.Tables[0].Rows[n]["unemploymentInsuranceStarTime"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["unemploymentInsuranceEndTime"].ToString() != "")
                    {
                        model.unemploymentInsuranceEndTime = DateTime.Parse(ds.Tables[0].Rows[n]["unemploymentInsuranceEndTime"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["birthInsuranceStarTime"].ToString() != "")
                    {
                        model.birthInsuranceStarTime = DateTime.Parse(ds.Tables[0].Rows[n]["birthInsuranceStarTime"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["TypeID"].ToString() != "")
                    {
                        model.TypeID = int.Parse(ds.Tables[0].Rows[n]["TypeID"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["birthInsuranceEndTime"].ToString() != "")
                    {
                        model.birthInsuranceEndTime = DateTime.Parse(ds.Tables[0].Rows[n]["birthInsuranceEndTime"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["compoInsuranceStarTime"].ToString() != "")
                    {
                        model.compoInsuranceStarTime = DateTime.Parse(ds.Tables[0].Rows[n]["compoInsuranceStarTime"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["compoInsuranceEndTime"].ToString() != "")
                    {
                        model.compoInsuranceEndTime = DateTime.Parse(ds.Tables[0].Rows[n]["compoInsuranceEndTime"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["medicalInsuranceStarTime"].ToString() != "")
                    {
                        model.medicalInsuranceStarTime = DateTime.Parse(ds.Tables[0].Rows[n]["medicalInsuranceStarTime"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["medicalInsuranceEndTime"].ToString() != "")
                    {
                        model.medicalInsuranceEndTime = DateTime.Parse(ds.Tables[0].Rows[n]["medicalInsuranceEndTime"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["publicReserveFundsStarTime"].ToString() != "")
                    {
                        model.publicReserveFundsStarTime = DateTime.Parse(ds.Tables[0].Rows[n]["publicReserveFundsStarTime"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["publicReserveFundsEndTime"].ToString() != "")
                    {
                        model.publicReserveFundsEndTime = DateTime.Parse(ds.Tables[0].Rows[n]["publicReserveFundsEndTime"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["contractStartDate"].ToString() != "")
                    {
                        model.contractStartDate = DateTime.Parse(ds.Tables[0].Rows[n]["contractStartDate"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["contractEndDate"].ToString() != "")
                    {
                        model.contractEndDate = DateTime.Parse(ds.Tables[0].Rows[n]["contractEndDate"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["probationEndDate"].ToString() != "")
                    {
                        model.probationEndDate = DateTime.Parse(ds.Tables[0].Rows[n]["probationEndDate"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["MaritalStatus"].ToString() != "")
                    {
                        model.MaritalStatus = int.Parse(ds.Tables[0].Rows[n]["MaritalStatus"].ToString());
                    }
                    
                        model.socialInsuranceBase = (ds.Tables[0].Rows[n]["socialInsuranceBase"].ToString());
                    
                        model.medicalInsuranceBase =(ds.Tables[0].Rows[n]["medicalInsuranceBase"].ToString());
                    
                        model.publicReserveFundsBase = (ds.Tables[0].Rows[n]["publicReserveFundsBase"].ToString());
                    
                    if (ds.Tables[0].Rows[n]["isArchive"].ToString() != "")
                    {
                        if ((ds.Tables[0].Rows[n]["isArchive"].ToString() == "1") || (ds.Tables[0].Rows[n]["isArchive"].ToString().ToLower() == "true"))
                        {
                            model.isArchive = true;
                        }
                        else
                        {
                            model.isArchive = false;
                        }
                    }
                    model.contractSignInfo = ds.Tables[0].Rows[n]["contractSignInfo"].ToString();
                    model.InsurancePlace = ds.Tables[0].Rows[n]["InsurancePlace"].ToString();
                    if (ds.Tables[0].Rows[n]["Gender"].ToString() != "")
                    {
                        model.Gender =  int.Parse(ds.Tables[0].Rows[n]["Gender"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["Birthday"].ToString() != "")
                    {
                        model.Birthday = DateTime.Parse(ds.Tables[0].Rows[n]["Birthday"].ToString());
                    }
                    model.Degree = ds.Tables[0].Rows[n]["Degree"].ToString();
                    if (ds.Tables[0].Rows[n]["IsForeign"].ToString() != "")
                    {
                        if ((ds.Tables[0].Rows[n]["IsForeign"].ToString() == "1") || (ds.Tables[0].Rows[n]["IsForeign"].ToString().ToLower() == "true"))
                        {
                            model.IsForeign = true;
                        }
                        else
                        {
                            model.IsForeign = false;
                        }
                    }
                    if (ds.Tables[0].Rows[n]["IsCertification"].ToString() != "")
                    {
                        if ((ds.Tables[0].Rows[n]["IsCertification"].ToString() == "1") || (ds.Tables[0].Rows[n]["IsCertification"].ToString().ToLower() == "true"))
                        {
                            model.IsCertification = true;
                        }
                        else
                        {
                            model.IsCertification = false;
                        }
                    }
                    if (ds.Tables[0].Rows[n]["WageMonths"].ToString() != "")
                    {
                        model.WageMonths = int.Parse(ds.Tables[0].Rows[n]["WageMonths"].ToString());
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
                    if (ds.Tables[0].Rows[n]["EIFirmsCosts"].ToString() != "")
                    {
                        model.EIFirmsCosts = (ds.Tables[0].Rows[n]["EIFirmsCosts"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["EIIndividualsCosts"].ToString() != "")
                    {
                        model.EIIndividualsCosts = (ds.Tables[0].Rows[n]["EIIndividualsCosts"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["UIFirmsCosts"].ToString() != "")
                    {
                        model.UIFirmsCosts = (ds.Tables[0].Rows[n]["UIFirmsCosts"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["UIIndividualsCosts"].ToString() != "")
                    {
                        model.UIIndividualsCosts = (ds.Tables[0].Rows[n]["UIIndividualsCosts"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["BIFirmsCosts"].ToString() != "")
                    {
                        model.BIFirmsCosts = (ds.Tables[0].Rows[n]["BIFirmsCosts"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["BIIndividualsCosts"].ToString() != "")
                    {
                        model.BIIndividualsCosts = (ds.Tables[0].Rows[n]["BIIndividualsCosts"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["CIFirmsCosts"].ToString() != "")
                    {
                        model.CIFirmsCosts = (ds.Tables[0].Rows[n]["CIFirmsCosts"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["CIIndividualsCosts"].ToString() != "")
                    {
                        model.CIIndividualsCosts = (ds.Tables[0].Rows[n]["CIIndividualsCosts"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["MIFirmsCosts"].ToString() != "")
                    {
                        model.MIFirmsCosts = (ds.Tables[0].Rows[n]["MIFirmsCosts"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["MIIndividualsCosts"].ToString() != "")
                    {
                        model.MIIndividualsCosts = (ds.Tables[0].Rows[n]["MIIndividualsCosts"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["PRFirmsCosts"].ToString() != "")
                    {
                        model.PRFirmsCosts = (ds.Tables[0].Rows[n]["PRFirmsCosts"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["PRIIndividualsCosts"].ToString() != "")
                    {
                        model.PRIIndividualsCosts = (ds.Tables[0].Rows[n]["PRIIndividualsCosts"].ToString());
                    }
                    model.socialInsuranceCompany = ds.Tables[0].Rows[n]["socialInsuranceCompany"].ToString();
                    model.CommonName = ds.Tables[0].Rows[n]["CommonName"].ToString();
                    modelList.Add(model);
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// 获得最新的薪资对象
        /// </summary>
        /// <returns></returns>
        public static SnapshotsInfo GetTopModel(int sysId)
        {
            return dal.GetTopModel(sysId);
        }

        /// <summary>
        /// 获得最新的薪资对象
        /// </summary>
        /// <returns></returns>
        public static SnapshotsInfo GetTopModel(int sysId,string sqlWhere)
        {
            return dal.GetTopModel(sysId,sqlWhere);
        }

        /// <summary>
        /// 获得最新的薪资对象
        /// </summary>
        /// <returns></returns>
        public static SnapshotsInfo GetTopModel(string UserCode)
        {
            return dal.GetTopModel(UserCode);
        }

        public static List<SnapshotsInfo> GetPayments(int[] userids, int year, int month)
        {  
            if (userids != null)
            {
                return null;
            }
            string sqlWhere = "";

            List<SnapshotsInfo> list = new List<SnapshotsInfo>();
            if (month < 4)
            {
                sqlWhere += " and (CreatedTime between '" + year.ToString() + "-4-01' and '" + (year + 1).ToString() + "-3-31')";
            }
            else
            {
                sqlWhere += " and (CreatedTime between '" + (year-1).ToString() + "-4-01' and '" + (year).ToString() + "-3-31')";
            }

            if (userids.Length > 0)
            {
                foreach (int j in userids)
                {
                    ESP.HumanResource.Entity.SnapshotsInfo snap = ESP.HumanResource.BusinessLogic.SnapshotsManager.GetTopModel(j);
                    list.Add(snap);
                    
                }
            }
            
            
            return list;
        }

        #endregion  成员方法
    }
}
