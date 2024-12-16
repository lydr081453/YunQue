using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SEPAdmin.IT
{
    public partial class OMEdit : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindFlow();

                if (!string.IsNullOrEmpty(Request["id"]))
                {
                    bindData(int.Parse(Request["id"]));
                }
            }
        }

        private void bindFlow()
        {
            var flowList = ESP.HumanResource.BusinessLogic.ITFlowManager.GetList(" and status =1", null);
            ESP.HumanResource.Entity.ITFlowInfo flow = new ESP.HumanResource.Entity.ITFlowInfo();
            flow.Id = -1;
            flow.FlowName = "请选择..";

            flowList.Insert(0, flow);

            ddlType.DataSource = flowList;
            ddlType.DataTextField = "FlowName";
            ddlType.DataValueField = "Id";
            ddlType.DataBind();


        }

        private void bindData(int id)
        {
            var model = ESP.HumanResource.BusinessLogic.ITItemsManager.GetModel(id);
            this.txtDesc.Text = model.Description;
            this.txtTitle.Text = model.Title;
            this.ddlType.SelectedValue = model.FlowId.ToString();
        }

        protected void btnCommit_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(this.txtTitle.Text) || string.IsNullOrEmpty(this.txtDesc.Text))
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('请填写标题和运维内容！');", true);
                return;
            }
            if (ddlType.SelectedIndex==0)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('请填写运维类型！');", true);
                return;
            }
            ESP.HumanResource.Entity.ITItemsInfo model =null;
            if (!string.IsNullOrEmpty(Request["id"]))
            {
                int id = int.Parse(Request["id"]);
                model = ESP.HumanResource.BusinessLogic.ITItemsManager.GetModel(id);
            }
            else
            {
                model = new ESP.HumanResource.Entity.ITItemsInfo();
            }

            model.Title = txtTitle.Text;
            model.Description = txtDesc.Text;
            model.CreateTime = DateTime.Now;
            model.FlowId =int.Parse( ddlType.SelectedValue);
            model.FlowName = ddlType.SelectedItem.Text;
            model.Status = 1;
            model.UserId = UserID;
            model.UserName = CurrentUserName;

            var itflow = ESP.HumanResource.BusinessLogic.ITFlowManager.GetModel(model.FlowId);

            model.AuditorId = itflow.DirectorId;
            model.Auditor = itflow.DirectorName;

            if (!string.IsNullOrEmpty(Request["id"]))
            {
                ESP.HumanResource.BusinessLogic.ITItemsManager.Update(model);
            }
            else
            {
                ESP.HumanResource.BusinessLogic.ITItemsManager.Add(model);
            }

            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('提交成功，请等待审批！');window.location.href='OMList.aspx';", true);


        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("OMList.aspx");
        }
    }
}