using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Purchase.Entity;
using ESP.Purchase.BusinessLogic;
using System.Data.SqlClient;

namespace PurchaseWeb.Purchase.PRAuthorization
{
    public partial class PRAuthorizationEdit : ESP.Web.UI.PageBase
    {
        int Id = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["id"]))
                Id = int.Parse(Request["id"]);

            if (!IsPostBack)
            {
                BindInfo();
            }
        }

        private void BindInfo()
        {
            if (Id != 0)
            {
                palDetail.Visible = true;
                btnSelect.Visible = false;
                btnSave.Visible = false;
                btnClose.Visible = true;

                PRAuthorizationInfo model = PRAuthorizationManager.GetModel(Id);
                txtUser.Text = model.UserName;
                hidUser.Value = model.UserId.ToString();
                hidTypeId.Value = model.TypeId;
                txtType.Text = model.TypeName;
                txtRemark.Text = model.Remark;
                litCreateUser.Text = model.CreateUserName;
                litCreateTime.Text = model.CreateDate.ToString("yyyy-MM-dd hh:mm:ss");
                litStatus.Text = ESP.Purchase.Common.State.PRAuthorizationStatus_Names[model.Status];
                litCloseTime.Text = model.CloseDate == null ? "未关闭" : model.CloseDate.Value.ToString("yyyy-MM-dd hh:mm:ss");

                List<SqlParameter> parms = new List<SqlParameter>();
                string term = " and requestor=" + model.UserId + " and (b.supplierid is null or b.supplierid=0) and PRAuthorizationId="+Id;


                List<GeneralInfo> list = GeneralInfoManager.GetStatusList(term, parms);
                gvList.DataSource = list;
                gvList.DataBind();
                if (model.Status == (int)ESP.Purchase.Common.State.PRAuthorizationStatus.PRAuthorizationStatus_Close)
                    btnClose.Visible = false;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            PRAuthorizationInfo model = new PRAuthorizationInfo();
            model.CreateDate = DateTime.Now;
            model.CreateUserId = int.Parse(CurrentUser.SysID);
            model.CreateUserName = CurrentUser.Name;
            model.Remark = txtRemark.Text.Trim();
            model.Status = (int)ESP.Purchase.Common.State.PRAuthorizationStatus.PRAuthorizationStatus_Use;
            model.UserId = int.Parse(hidUser.Value);
            model.UserName = txtUser.Text.Trim();
            model.TypeId = this.hidTypeId.Value;
            model.TypeName = this.txtType.Text;

            if (PRAuthorizationManager.Add(model) > 0)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('保存成功！');window.location.href='PRAuthorizationList.aspx';", true);
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('保存失败！');", true);
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            PRAuthorizationInfo model = PRAuthorizationManager.GetModel(Id);
            model.Remark = txtRemark.Text.Trim();
            model.Status = (int)ESP.Purchase.Common.State.PRAuthorizationStatus.PRAuthorizationStatus_Close;
            model.CloseDate = DateTime.Now;
            PRAuthorizationManager.Update(model);
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('关闭成功！');window.location.href='PRAuthorizationList.aspx';", true);
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("PRAuthorizationList.aspx");
        }
    }
}
