using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SEPAdmin.IT
{
    public partial class OMAudit : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request["id"]))
                {
                    bindData(int.Parse(Request["id"]));
                }
            }
        }

        private void bindData(int id)
        {
            var model = ESP.HumanResource.BusinessLogic.ITItemsManager.GetModel(id);
            lblCreater.Text = model.UserName;
            lblCreateTime.Text = model.CreateTime.ToString("yyyy-MM-dd");
            lblTitile.Text = model.Title;
            txtDesc.Text = model.Description;
            lblType.Text = model.FlowName;

            bindLog(model.Id);
        }

        private void bindLog(int id)
        {
            var loglist = ESP.HumanResource.BusinessLogic.ItLogManager.GetList(id);
            foreach (var log in loglist)
            { 
                string status =string.Empty;
                if(log.Status==1)
                    status="审批通过";
                else
                    status ="审批驳回";

                this.lblLog.Text += log.UserName + status + "["+log.remark+"  "+log.LogTime.ToString()+"]</br>";
            }

        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            int id = int.Parse(Request["id"]);

            if (string.IsNullOrEmpty(this.txtAudit.Text))
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('请填写审核信息！');", true);
                return;
            }

            var model = ESP.HumanResource.BusinessLogic.ITItemsManager.GetModel(id);
            model.Status = -1;
            model.AuditorId = 0;
            model.Auditor = "";
            ESP.HumanResource.BusinessLogic.ITItemsManager.Update(model);

            ESP.HumanResource.Entity.ITLogInfo log = new ESP.HumanResource.Entity.ITLogInfo();
            log.ITItemId = model.Id;
            log.LogTime = DateTime.Now;
            log.remark = this.txtAudit.Text;
            log.Status = 2;
            log.UserId = UserID;
            log.UserName = CurrentUserName;
            ESP.HumanResource.BusinessLogic.ItLogManager.Add(log);

            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('已经驳回申请！');window.location.href='OMAuditList.aspx';", true);

        }

        protected void btnCommit_Click(object sender, EventArgs e)
        {
            int id = int.Parse(Request["id"]);

            if (string.IsNullOrEmpty(this.txtAudit.Text))
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('请填写审核信息！');", true);
                return;
            }

            var model = ESP.HumanResource.BusinessLogic.ITItemsManager.GetModel(id);
            var flow = ESP.HumanResource.BusinessLogic.ITFlowManager.GetModel(model.FlowId);
            if (flow.DirectorId == UserID )
            {
                if (flow.TestId == 0 && flow.PublisherId != 0)
                {
                    model.Status = 3;
                    model.AuditorId = flow.PublisherId;
                    model.Auditor = flow.PublisherName;
                }
                else if (flow.TestId == 0 && flow.PublisherId == 0)
                {
                    model.Status = 4;
                    model.AuditorId = 0;
                    model.Auditor = "";
                }
                else
                {
                    model.Status = 2;
                    model.AuditorId = flow.TestId;
                    model.Auditor = flow.TestName;
                }
            }
            else if (flow.TestId == UserID && flow.PublisherId!=0)
            {
                model.Status = 3;
                model.AuditorId = flow.PublisherId;
                model.Auditor = flow.PublisherName;
            }
            else if (flow.PublisherId == UserID)
            {
                model.Status = 4;
                model.AuditorId = 0;
                model.Auditor = "";
            }
            
            ESP.HumanResource.BusinessLogic.ITItemsManager.Update(model);

            ESP.HumanResource.Entity.ITLogInfo log = new ESP.HumanResource.Entity.ITLogInfo();
            log.ITItemId = model.Id;
            log.LogTime = DateTime.Now;
            log.remark = this.txtAudit.Text;
            log.Status = 1;
            log.UserId = UserID;
            log.UserName = CurrentUserName;
            ESP.HumanResource.BusinessLogic.ItLogManager.Add(log);

            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('审核通过申请！');window.location.href='OMAuditList.aspx';", true);

        } 

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("OMAuditList.aspx");
        }
    }
}