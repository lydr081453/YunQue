using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.HumanResource.BusinessLogic;
using ESP.HumanResource.Common;

namespace SEPAdmin.HR.Employees
{
    public partial class InsuranceCompensationEdit : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                initForm();
            }

        }

        protected void initForm()
        {
            int year = DateTime.Now.Year - 10;
            for (int i = 0; i < 20; i++)
            {
                drpYear.Items.Insert(i, new ListItem((year + i).ToString(), (year + i).ToString()));
            }
            for (int i = 1; i <= 12; i++)
            {
                drpMonth.Items.Insert(i - 1, new ListItem((i).ToString(), (i).ToString()));
            }
            if (!string.IsNullOrEmpty(Request["id"]))
            {

                ESP.HumanResource.Entity.PayInsuranceInfo payinfo = PayInsuranceManager.GetModel(" p.id=" + Request["id"].ToString());

                if (payinfo != null)
                {
                    labUsers.Text = payinfo.FullNameCn;
                    hidUserID.Value = payinfo.UserID.ToString();

                    drpYear.SelectedValue = payinfo.PayYear.ToString();
                    drpMonth.SelectedValue = payinfo.PayMonth.ToString();

                    txtEndowmentInsurance.Text = !string.IsNullOrEmpty(payinfo.EndowmentInsurance) ? ESP.Salary.Utility.DESEncrypt.Decode(payinfo.EndowmentInsurance) : "0";
                    txtMedicalInsurance.Text = !string.IsNullOrEmpty(payinfo.MedicalInsurance) ? ESP.Salary.Utility.DESEncrypt.Decode(payinfo.MedicalInsurance) : "0";
                    txtPublicReserveFunds.Text = !string.IsNullOrEmpty(payinfo.PublicReserveFunds) ? ESP.Salary.Utility.DESEncrypt.Decode(payinfo.PublicReserveFunds) : "0";
                    txtUnemploymentInsurance.Text = !string.IsNullOrEmpty(payinfo.UnemploymentInsurance) ? ESP.Salary.Utility.DESEncrypt.Decode(payinfo.UnemploymentInsurance) : "0";

                    txtRemarks.Text = payinfo.Remark;
                }


            }
            else
            {
                drpYear.SelectedValue = DateTime.Now.Year.ToString();
                drpMonth.SelectedValue = DateTime.Now.Month.ToString();
                txtEndowmentInsurance.Text = ("0");
                txtMedicalInsurance.Text = ("0");
                txtPublicReserveFunds.Text = ("0");
                txtUnemploymentInsurance.Text = ("0");
            }                        
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            ESP.HumanResource.Entity.PayInsuranceInfo info = new ESP.HumanResource.Entity.PayInsuranceInfo();
            info.UserID = int.Parse(hidUserID.Value.Trim());
            info.EndowmentInsurance = ESP.Salary.Utility.DESEncrypt.Encode(txtEndowmentInsurance.Text.Trim());
            info.MedicalInsurance = ESP.Salary.Utility.DESEncrypt.Encode(txtMedicalInsurance.Text.Trim());
            info.PublicReserveFunds = ESP.Salary.Utility.DESEncrypt.Encode(txtPublicReserveFunds.Text.Trim());
            info.UnemploymentInsurance = ESP.Salary.Utility.DESEncrypt.Encode(txtUnemploymentInsurance.Text.Trim());
            info.PayYear = int.Parse(drpYear.SelectedItem.Value);
            info.PayMonth = int.Parse(drpMonth.SelectedItem.Value);
            info.Creator = UserInfo.UserID;
            info.LastUpdateMan = UserInfo.UserID;
            info.CreateTime = info.LastUpdateTime = DateTime.Now;

            ESP.HumanResource.Entity.LogInfo logModel = new ESP.HumanResource.Entity.LogInfo();
                logModel.Des = UserInfo.Username + "添加了" + labUsers.Text + "的社保福利补发信息。";
                logModel.LogMedifiedTeme = DateTime.Now;
                logModel.LogUserId = UserInfo.UserID;
                logModel.LogUserName = UserInfo.Username;

            int rel = PayInsuranceManager.Add(info,logModel);
            if (rel > 0)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location='InsuranceCompensationList.aspx';alert('添加成功！');", true);
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('添加失败！');", true);
            }
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("InsuranceCompensationList.aspx");
        }
    }
}
