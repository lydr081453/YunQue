using System;
using System.Data;
using System.Collections.Generic;
using ESP.HumanResource.Entity;
using ESP.HumanResource.Common;
using System.Data.SqlClient;
using ESP.HumanResource.DataAccess;

namespace ESP.HumanResource.BusinessLogic
{
    public class SocialSecurityModelManager
    {
        private readonly static PayInsuranceDataProvider dal = new PayInsuranceDataProvider();        

        public static SocialSecurityModelInfo GetPayments(int userid, int year, int month)
        {
            
            string sqlWhere = "";

            
            if (month == 12)
            {
                sqlWhere += " and (CreatedTime between '" + year.ToString() +"-" + month.ToString() + "-01' and '" + (year+1).ToString() + "-01-01')";
            }
            else
            {
                sqlWhere += " and (CreatedTime between '" + year.ToString() + "-" + month.ToString() + "-01' and '" + (year).ToString() + "-" + (month + 1).ToString() + "-01')";
            }
                        
            SnapshotsInfo snap = ESP.HumanResource.BusinessLogic.SnapshotsManager.GetTopModel(userid,sqlWhere);
            if (snap == null)
            {
                sqlWhere = "";
                if (month > 4)
                {
                    sqlWhere += " and (CreatedTime between '" + year.ToString() + "-4-01' and '" + (year + 1).ToString() + "-3-31')";
                }
                else
                {
                    sqlWhere += " and (CreatedTime between '" + (year - 1).ToString() + "-4-01' and '" + (year).ToString() + "-3-31')";
                }
                snap = ESP.HumanResource.BusinessLogic.SnapshotsManager.GetTopModel(userid, sqlWhere);
            }
            List<PayInsuranceInfo> pay = PayInsuranceManager.GetModelList(" p.userid="+userid.ToString()+" and p.payyear="+year.ToString()+" and p.paymonth="+month.ToString());

            List<ESP.HumanResource.Entity.EmployeesInPositionsInfo> list = EmployeesInPositionsManager.GetModelList(" a.userID = " + userid.ToString());
            DataSet ds = ESP.HumanResource.BusinessLogic.SocialSecurityManager.GetList(" (begintime <= '" + year.ToString() + "-" + month.ToString() + "-01' and endtime >= '" + year.ToString() + "-" + month.ToString()  +"-01') and SocialInsuranceCompany=" + list[0].CompanyID);
            //养老底线值
            ESP.HumanResource.Entity.ProtectionLineInfo prot = ProtectionLineManager.GetModel(1);

            //养老/失业/工伤/生育缴费基数
            decimal sbase = 0;
            try
            {
                sbase = Convert.ToDecimal(ESP.Salary.Utility.DESEncrypt.Decode(snap.socialInsuranceBase));
            }
            catch (Exception ex) { }
            //医疗基数
            decimal mbase = 0;
            try
            {
                mbase = Convert.ToDecimal(ESP.Salary.Utility.DESEncrypt.Decode(snap.medicalInsuranceBase));
            }
            catch (Exception ex) { }

            //公积金基数
            decimal pbase = 0;
            try
            {
                pbase = Convert.ToDecimal(ESP.Salary.Utility.DESEncrypt.Decode(snap.publicReserveFundsBase));
            }
            catch (Exception ex) { }

            //养老保险公司比例
            decimal eif = 0;
            //养老保险个人比例
            decimal eii = 0;
            //失业保险公司比例
            decimal UIF = 0;
            //失业保险个人比例
            decimal UII = 0;
            //生育保险公司比例
            decimal BIF = 0;
            //工伤险公司比例
            decimal CIF = 0;
            //医疗保险公司比例
            decimal MIF = 0;
            //医疗保险个人比例
            decimal MII = 0;
            //医疗保险大额医疗个人支付额
            decimal MIBI = 0;
            //公积金比例
            decimal PRF = 0;
            if (ds.Tables[0].Rows.Count > 0)
            {
                //养老保险公司比例
                 eif = decimal.Parse(!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["EIProportionOfFirms"].ToString()) ? ds.Tables[0].Rows[0]["EIProportionOfFirms"].ToString() : "0");
                //养老保险个人比例
                 eii = decimal.Parse(!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["EIProportionOfIndividuals"].ToString()) ? ds.Tables[0].Rows[0]["EIProportionOfIndividuals"].ToString() : "0");
                //失业保险公司比例
                 UIF = decimal.Parse(!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["UIProportionOfFirms"].ToString()) ? ds.Tables[0].Rows[0]["UIProportionOfFirms"].ToString() : "0");
                //失业保险个人比例
                 UII = decimal.Parse(!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["UIProportionOfIndividuals"].ToString()) ? ds.Tables[0].Rows[0]["UIProportionOfIndividuals"].ToString() : "0");
                //生育保险公司比例
                 BIF = decimal.Parse(!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["BIProportionOfFirms"].ToString()) ? ds.Tables[0].Rows[0]["BIProportionOfFirms"].ToString() : "0");
                //工伤险公司比例
                 CIF = decimal.Parse(!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["CIProportionOfFirms"].ToString()) ? ds.Tables[0].Rows[0]["CIProportionOfFirms"].ToString() : "0");
                //医疗保险公司比例
                 MIF = decimal.Parse(!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["MIProportionOfFirms"].ToString()) ? ds.Tables[0].Rows[0]["MIProportionOfFirms"].ToString() : "0");
                //医疗保险个人比例
                 MII = decimal.Parse(!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["MIProportionOfIndividuals"].ToString()) ? ds.Tables[0].Rows[0]["MIProportionOfIndividuals"].ToString() : "0");
                //医疗保险大额医疗个人支付额
                 MIBI = decimal.Parse(!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["MIBigProportionOfIndividuals"].ToString()) ? ds.Tables[0].Rows[0]["MIBigProportionOfIndividuals"].ToString() : "0");
                //公积金比例
                 PRF = decimal.Parse(!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["PRFProportionOfFirms"].ToString()) ? ds.Tables[0].Rows[0]["PRFProportionOfFirms"].ToString() : "0");

               
            }

            SocialSecurityModelInfo ssmi = new SocialSecurityModelInfo();
            if (snap != null)
            {
                ssmi.BIFirmsCosts = (snap.InsurancePlace == "外阜农业户口" || snap.InsurancePlace == "外阜城镇户口") ? ESP.Salary.Utility.DESEncrypt.Encode("0") : ESP.Salary.Utility.DESEncrypt.Encode(((sbase > prot.ProtectionLineNameAmount ? sbase : prot.ProtectionLineNameAmount) * (BIF / 100)).ToString("0.00"));
                ssmi.BIIndividualsCosts = ESP.Salary.Utility.DESEncrypt.Encode("0");
                ssmi.CIFirmsCosts = ESP.Salary.Utility.DESEncrypt.Encode((sbase * (CIF / 100)).ToString("0.00"));
                ssmi.CIIndividualsCosts = ESP.Salary.Utility.DESEncrypt.Encode((sbase * (CIF / 100)).ToString("0.00"));
                ssmi.EIFirmsCosts = ESP.Salary.Utility.DESEncrypt.Encode((sbase * (eif / 100)).ToString("0.00"));
                ssmi.EIIndividualsCosts = ESP.Salary.Utility.DESEncrypt.Encode((sbase * (eii / 100)).ToString("0.00"));
                ssmi.MIFirmsCosts = ESP.Salary.Utility.DESEncrypt.Encode((mbase * (MIF / 100)).ToString("0.00"));
                ssmi.MIIndividualsCosts = ESP.Salary.Utility.DESEncrypt.Encode((mbase * (MII / 100)).ToString("0.00"));
                ssmi.PRFirmsCosts = ESP.Salary.Utility.DESEncrypt.Encode((pbase * (PRF / 100)).ToString("0.00"));
                ssmi.PRIIndividualsCosts = ESP.Salary.Utility.DESEncrypt.Encode((pbase * (PRF / 100)).ToString("0.00"));
                ssmi.UIFirmsCosts = ESP.Salary.Utility.DESEncrypt.Encode((sbase * (UIF / 100)).ToString("0.00"));
                ssmi.UIIndividualsCosts = snap.InsurancePlace == "外阜农业户口" ? ESP.Salary.Utility.DESEncrypt.Encode("0"):ESP.Salary.Utility.DESEncrypt.Encode((sbase * (UII / 100)).ToString("0.00")) ;
            }
            else
            {
                ssmi.BIFirmsCosts = ESP.Salary.Utility.DESEncrypt.Encode("0");
                ssmi.BIIndividualsCosts = ESP.Salary.Utility.DESEncrypt.Encode("0");
                ssmi.CIFirmsCosts = ESP.Salary.Utility.DESEncrypt.Encode("0");
                ssmi.CIIndividualsCosts = ESP.Salary.Utility.DESEncrypt.Encode("0");
                ssmi.EIFirmsCosts = ESP.Salary.Utility.DESEncrypt.Encode("0");
                ssmi.EIIndividualsCosts = ESP.Salary.Utility.DESEncrypt.Encode("0");
                ssmi.MIFirmsCosts = ESP.Salary.Utility.DESEncrypt.Encode("0");
                ssmi.MIIndividualsCosts = ESP.Salary.Utility.DESEncrypt.Encode("0");
                ssmi.PRFirmsCosts = ESP.Salary.Utility.DESEncrypt.Encode("0");
                ssmi.PRIIndividualsCosts = ESP.Salary.Utility.DESEncrypt.Encode("0");
                ssmi.UIFirmsCosts = ESP.Salary.Utility.DESEncrypt.Encode("0");
                ssmi.UIIndividualsCosts = ESP.Salary.Utility.DESEncrypt.Encode("0");
 
            }
            if (pay.Count > 0)
            {
                decimal ei = 0;
                decimal mi = 0;
                decimal ui = 0;
                decimal prf = 0;
                foreach (PayInsuranceInfo i in pay)
                {
                    ei += decimal.Parse(ESP.Salary.Utility.DESEncrypt.Decode(i.EndowmentInsurance));
                    mi += decimal.Parse(ESP.Salary.Utility.DESEncrypt.Decode(i.MedicalInsurance));
                    ui += decimal.Parse(ESP.Salary.Utility.DESEncrypt.Decode(i.UnemploymentInsurance));
                    prf += decimal.Parse(ESP.Salary.Utility.DESEncrypt.Decode(i.PublicReserveFunds));
                }
                ssmi.EndowmentInsurance = ESP.Salary.Utility.DESEncrypt.Encode(ei.ToString());
                ssmi.MedicalInsurance = ESP.Salary.Utility.DESEncrypt.Encode(mi.ToString());
                ssmi.PublicReserveFunds = ESP.Salary.Utility.DESEncrypt.Encode(prf.ToString());
                ssmi.UnemploymentInsurance = ESP.Salary.Utility.DESEncrypt.Encode(ui.ToString());
                ssmi.Remarks = pay[0].Remark;
            }
            else
            {
                ssmi.EndowmentInsurance = ESP.Salary.Utility.DESEncrypt.Encode("0");
                ssmi.MedicalInsurance = ESP.Salary.Utility.DESEncrypt.Encode("0");
                ssmi.PublicReserveFunds = ESP.Salary.Utility.DESEncrypt.Encode("0");
                ssmi.UnemploymentInsurance = ESP.Salary.Utility.DESEncrypt.Encode("0");
                ssmi.Remarks = "";
            }

            return ssmi;
        }
    }
}
