using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SEPAdmin.IT
{
    public partial class OMView : ESP.Web.UI.PageBase
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
                string status = string.Empty;
                if (log.Status == 1)
                    status = "审批通过";
                else
                    status = "审批驳回";

                this.lblLog.Text += log.UserName + status + "[" + log.remark + "  " + log.LogTime.ToString() + "]</br>";
            }

        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("OMList.aspx");
        }

    }
}