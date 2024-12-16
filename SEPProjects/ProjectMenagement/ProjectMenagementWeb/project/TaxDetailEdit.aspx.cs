using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinanceWeb.project
{
    public partial class TaxDetailEdit : ESP.Web.UI.PageBase
    {
        protected ESP.Finance.Entity.ReturnInfo returnModel = null;
        protected ESP.Finance.Entity.ProjectInfo projectModel = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                dataBind();

        }

        private void dataBind()
        {
            int returnid = 0;
            if (!string.IsNullOrEmpty(Request["ReturnId"]))
            {
                returnid = int.Parse(Request["ReturnId"].ToString());
                returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(returnid);
                this.lblProjectCode.Text = returnModel.ProjectCode;
                this.lblReturnCode.Text = returnModel.ReturnCode;
                txtFactFee.Text = returnModel.FactFee == null ? returnModel.PreFee.Value.ToString("#,##0.00") : returnModel.FactFee.Value.ToString("#,##0.00");
                lblFactDate.Text = returnModel.ReturnFactDate != null ? returnModel.ReturnFactDate.Value.ToString("yyyy-MM-dd") : "";
                lblDepartment.Text = returnModel.DepartmentName;
                lblSupplier.Text = returnModel.SupplierName;
                txtFactFee.Enabled = false;
            }
            else if (!string.IsNullOrEmpty(Request["ProjectId"]))
            {
                int projectid = int.Parse(Request["ProjectId"].ToString());
                projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(projectid);
                this.lblProjectCode.Text = projectModel.ProjectCode;
                lblDepartment.Text = projectModel.GroupName;
                txtFactFee.Enabled = true;
            }
            else if (!string.IsNullOrEmpty(Request["DetailId"]))
            {
                int detailId = int.Parse(Request["DetailId"].ToString());
                ESP.Finance.Entity.TaxDetailInfo detailModel = ESP.Finance.BusinessLogic.TaxDetailManager.GetModel(detailId);
                returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(detailModel.ReturnId);
                this.lblDepartment.Text = detailModel.DepartmentName;
                if (returnModel != null)
                {
                    lblSupplier.Text = returnModel.SupplierName;
                    this.lblFactDate.Text = returnModel.ReturnFactDate == null ? "" : returnModel.ReturnFactDate.Value.ToString("yyyy-MM-dd");
                }
                this.lblProjectCode.Text = detailModel.ProjectCode;
                this.lblReturnCode.Text = detailModel.ReturnCode;
                this.txtFactFee.Text = detailModel.Total.ToString("#,##0.00");
                txtFactFee.Enabled = false;
                this.txtTax.Text = detailModel.Tax.ToString("#,##0.00");
                this.txtTaxDate.Text = detailModel.TaxDate.ToString("yyyy-MM-dd");
              
            }
        }
        protected void btnYes_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(this.txtTax.Text) || string.IsNullOrEmpty(this.txtFactFee.Text) || string.IsNullOrEmpty(txtTaxDate.Text))
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('请输入税款及发生日期！');", true);
                return;
            }
            if (!string.IsNullOrEmpty(Request["DetailId"]))
            {
                int detailId = int.Parse(Request["DetailId"].ToString());
                ESP.Finance.Entity.TaxDetailInfo detailModel = ESP.Finance.BusinessLogic.TaxDetailManager.GetModel(detailId);
                detailModel.Tax = decimal.Parse(this.txtTax.Text);
                detailModel.TaxDate = DateTime.Parse(this.txtTaxDate.Text);
                int rett = ESP.Finance.BusinessLogic.TaxDetailManager.Update(detailModel);
                if (rett > 0)
                {
                    Response.Redirect("TaxDetailList.aspx");
                }
            }
            else if (!string.IsNullOrEmpty(Request["ReturnId"]))
            {
                int returnid = int.Parse(Request["ReturnId"].ToString());
                returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(returnid);
                ESP.Finance.Entity.TaxDetailInfo model = new ESP.Finance.Entity.TaxDetailInfo();
                model.ProjectId = returnModel.ProjectID.Value;
                model.ReturnId = returnModel.ReturnID;
                model.Status = 0;
                model.Tax = decimal.Parse(this.txtTax.Text);
                model.TaxDate = DateTime.Parse(this.txtTaxDate.Text);
                model.Total = returnModel.FactFee == null ? returnModel.PreFee.Value : returnModel.FactFee.Value;
                model.UserId = int.Parse(CurrentUser.SysID);
                model.UserName = CurrentUser.Name;
                model.CurrentDate = DateTime.Now;
                model.AuditerId = 0;
                model.Auditer = "";
                model.AuditDate = DateTime.Now;
                model.ProjectCode = returnModel.ProjectCode;
                model.ReturnCode = returnModel.ReturnCode;
                model.DepartmentId = returnModel.DepartmentID.Value;
                model.DepartmentName = returnModel.DepartmentName;

                int rett = ESP.Finance.BusinessLogic.TaxDetailManager.Add(model);
                if (rett > 0)
                {
                    Response.Redirect("TaxDetailList.aspx");
                }
            }
            else if (!string.IsNullOrEmpty(Request["ProjectId"]))
            {
                int projectid = int.Parse(Request["ProjectId"].ToString());
                projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(projectid);
                ESP.Finance.Entity.TaxDetailInfo model = new ESP.Finance.Entity.TaxDetailInfo();
                model.ProjectId = projectid;
                model.ReturnId = 0;
                model.Status = 0;
                model.Tax = decimal.Parse(this.txtTax.Text);
                model.TaxDate = DateTime.Parse(this.txtTaxDate.Text);
                model.Total = decimal.Parse(txtFactFee.Text);
                model.UserId = int.Parse(CurrentUser.SysID);
                model.UserName = CurrentUser.Name;
                model.CurrentDate = DateTime.Now;
                model.AuditerId = 0;
                model.Auditer = "";
                model.AuditDate = DateTime.Now;
                model.ProjectCode = projectModel.ProjectCode;
                model.ReturnCode = "";
                model.DepartmentId = projectModel.GroupID.Value;
                model.DepartmentName = projectModel.GroupName;

                int rett = ESP.Finance.BusinessLogic.TaxDetailManager.Add(model);
                if (rett > 0)
                {
                    Response.Redirect("TaxDetailList.aspx");
                }
            }

        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("TaxDetailList.aspx");
        }
    }
}
