using System;
using System.Data;
using System.Collections.Generic;
using ESP.HumanResource.Entity;
using ESP.HumanResource.Common;
using System.Data.SqlClient;
using ESP.HumanResource.DataAccess;

namespace ESP.HumanResource.BusinessLogic
{   
    public class EmployeeWelfareManager
    {
        private static EmployeeWelfareDataProvider dal = new EmployeeWelfareDataProvider();
        public EmployeeWelfareManager()
        { }
        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(EmployeeWelfareInfo model, SqlTransaction trans)
        {
            return dal.Add(model, trans);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static void Update(EmployeeWelfareInfo model, SqlTransaction trans)
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
        /// 删除数据
        /// </summary>
        public static void DeleteBySysUserID(int sysid, SqlTransaction trans)
        {

            dal.Delete(sysid, trans);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static EmployeeWelfareInfo GetModel(int id)
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
        public static List<EmployeeWelfareInfo> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            List<EmployeeWelfareInfo> modelList = new List<EmployeeWelfareInfo>();
            int rowsCount = ds.Tables[0].Rows.Count;
            if (rowsCount > 0)
            {
                EmployeeWelfareInfo model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new EmployeeWelfareInfo();
                    if (ds.Tables[0].Rows[n]["id"].ToString() != "")
                    {
                        model.id = int.Parse(ds.Tables[0].Rows[n]["id"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["sysid"].ToString() != "")
                    {
                        model.sysid = int.Parse(ds.Tables[0].Rows[n]["sysid"].ToString());
                    }
                    model.contractType = ds.Tables[0].Rows[n]["contractType"].ToString();
                    model.contractTerm = ds.Tables[0].Rows[n]["contractTerm"].ToString();
                    model.contractCompany = ds.Tables[0].Rows[n]["contractCompany"].ToString();
                    if (ds.Tables[0].Rows[n]["contractStartDate"].ToString() != "")
                    {
                        model.contractStartDate = DateTime.Parse(ds.Tables[0].Rows[n]["contractStartDate"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["contractEndDate"].ToString() != "")
                    {
                        model.contractEndDate = DateTime.Parse(ds.Tables[0].Rows[n]["contractEndDate"].ToString());
                    }
                    model.probationPeriod = ds.Tables[0].Rows[n]["probationPeriod"].ToString();
                    if (ds.Tables[0].Rows[n]["probationPeriodDeadLine"].ToString() != "")
                    {
                        model.probationPeriodDeadLine = DateTime.Parse(ds.Tables[0].Rows[n]["probationPeriodDeadLine"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["probationEndDate"].ToString() != "")
                    {
                        model.probationEndDate = DateTime.Parse(ds.Tables[0].Rows[n]["probationEndDate"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["endowmentInsurance"].ToString() != "")
                    {
                        if ((ds.Tables[0].Rows[n]["endowmentInsurance"].ToString() == "1") || (ds.Tables[0].Rows[n]["endowmentInsurance"].ToString().ToLower() == "true"))
                        {
                            model.endowmentInsurance = true;
                        }
                        else
                        {
                            model.endowmentInsurance = false;
                        }
                    }
                    if (ds.Tables[0].Rows[n]["unemploymentInsurance"].ToString() != "")
                    {
                        if ((ds.Tables[0].Rows[n]["unemploymentInsurance"].ToString() == "1") || (ds.Tables[0].Rows[n]["unemploymentInsurance"].ToString().ToLower() == "true"))
                        {
                            model.unemploymentInsurance = true;
                        }
                        else
                        {
                            model.unemploymentInsurance = false;
                        }
                    }
                    if (ds.Tables[0].Rows[n]["birthInsurance"].ToString() != "")
                    {
                        if ((ds.Tables[0].Rows[n]["birthInsurance"].ToString() == "1") || (ds.Tables[0].Rows[n]["birthInsurance"].ToString().ToLower() == "true"))
                        {
                            model.birthInsurance = true;
                        }
                        else
                        {
                            model.birthInsurance = false;
                        }
                    }
                    if (ds.Tables[0].Rows[n]["compoInsurance"].ToString() != "")
                    {
                        if ((ds.Tables[0].Rows[n]["compoInsurance"].ToString() == "1") || (ds.Tables[0].Rows[n]["compoInsurance"].ToString().ToLower() == "true"))
                        {
                            model.compoInsurance = true;
                        }
                        else
                        {
                            model.compoInsurance = false;
                        }
                    }
                    if (ds.Tables[0].Rows[n]["medicalInsurance"].ToString() != "")
                    {
                        if ((ds.Tables[0].Rows[n]["medicalInsurance"].ToString() == "1") || (ds.Tables[0].Rows[n]["medicalInsurance"].ToString().ToLower() == "true"))
                        {
                            model.medicalInsurance = true;
                        }
                        else
                        {
                            model.medicalInsurance = false;
                        }
                    }
                    model.socialInsuranceCompany = ds.Tables[0].Rows[n]["socialInsuranceCompany"].ToString();
                    model.socialInsuranceAddress = ds.Tables[0].Rows[n]["socialInsuranceAddress"].ToString();
                    model.socialInsuranceCode = ds.Tables[0].Rows[n]["socialInsuranceCode"].ToString();
                    model.medicalInsuranceCode = ds.Tables[0].Rows[n]["medicalInsuranceCode"].ToString();
                    model.socialInsuranceBase = ds.Tables[0].Rows[n]["socialInsuranceBase"].ToString();
                    model.medicalInsuranceBase = ds.Tables[0].Rows[n]["medicalInsuranceBase"].ToString();
                    model.publicReserveFundsCompany = ds.Tables[0].Rows[n]["publicReserveFundsCompany"].ToString();
                    model.publicReserveFundsAddress = ds.Tables[0].Rows[n]["publicReserveFundsAddress"].ToString();
                    model.publicReserveFundsBase = ds.Tables[0].Rows[n]["publicReserveFundsBase"].ToString();
                    model.publicReserveFundsCode = ds.Tables[0].Rows[n]["publicReserveFundsCode"].ToString();
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
                    model.ArchiveCode = ds.Tables[0].Rows[n]["ArchiveCode"].ToString();
                    model.ArchiveDate = ds.Tables[0].Rows[n]["ArchiveDate"].ToString();
                    model.memo = ds.Tables[0].Rows[n]["memo"].ToString();
                    if (ds.Tables[0].Rows[n]["publicReserveFunds"].ToString() != "")
                    {
                        if ((ds.Tables[0].Rows[n]["publicReserveFunds"].ToString() == "1") || (ds.Tables[0].Rows[n]["publicReserveFunds"].ToString().ToLower() == "true"))
                        {
                            model.publicReserveFunds = true;
                        }
                        else
                        {
                            model.publicReserveFunds = false;
                        }
                    }
                    if (ds.Tables[0].Rows[n]["contractRenewalCount"].ToString() != "")
                    {
                        model.contractRenewalCount = int.Parse(ds.Tables[0].Rows[n]["contractRenewalCount"].ToString());
                    }
                    model.contractSignInfo = ds.Tables[0].Rows[n]["contractSignInfo"].ToString();
                    model.ArchivePlace = ds.Tables[0].Rows[n]["ArchivePlace"].ToString();
                    model.InsurancePlace = ds.Tables[0].Rows[n]["InsurancePlace"].ToString();
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
                    if (ds.Tables[0].Rows[n]["ComplementaryMedicalCosts"].ToString() != "")
                    {
                        model.ComplementaryMedicalCosts = decimal.Parse(ds.Tables[0].Rows[n]["ComplementaryMedicalCosts"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["ComplementaryMedical"].ToString() != "")
                    {
                        if ((ds.Tables[0].Rows[n]["ComplementaryMedical"].ToString() == "1") || (ds.Tables[0].Rows[n]["ComplementaryMedical"].ToString().ToLower() == "true"))
                        {
                            model.ComplementaryMedical = true;
                        }
                        else
                        {
                            model.ComplementaryMedical = false;
                        }
                    }
                    if (ds.Tables[0].Rows[n]["AccidentInsurance"].ToString() != "")
                    {
                        if ((ds.Tables[0].Rows[n]["AccidentInsurance"].ToString() == "1") || (ds.Tables[0].Rows[n]["AccidentInsurance"].ToString().ToLower() == "true"))
                        {
                            model.AccidentInsurance = true;
                        }
                        else
                        {
                            model.AccidentInsurance = false;
                        }
                    }
                    if (ds.Tables[0].Rows[n]["AccidentInsuranceBeginTime"].ToString() != "")
                    {
                        model.AccidentInsuranceBeginTime = DateTime.Parse(ds.Tables[0].Rows[n]["AccidentInsuranceBeginTime"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["AccidentInsuranceEndTime"].ToString() != "")
                    {
                        model.AccidentInsuranceEndTime = DateTime.Parse(ds.Tables[0].Rows[n]["AccidentInsuranceEndTime"].ToString());
                    }
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
        /// 通过SysId获得福利信息
        /// </summary>
        /// <param name="sysid"></param>
        /// <returns></returns>
        public static EmployeeWelfareInfo getModelBySysId(int sysid)
        {
            return dal.getModelBySysId(sysid);
        }

        #endregion  成员方法
    }
}
