using ESP.HumanResource.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SEPAdmin.IT
{
    public partial class OMAuditList : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindFlow();
                bindData();
            }
        }

        private void bindFlow()
        {
            var flowList = ESP.HumanResource.BusinessLogic.ITFlowManager.GetList(" and status =1", null);
            ESP.HumanResource.Entity.ITFlowInfo flow = new ESP.HumanResource.Entity.ITFlowInfo();
            flow.Id = -1;
            flow.FlowName = "请选择..";

            flowList.Insert(0, flow);

            ddlStatus.DataSource = flowList;
            ddlStatus.DataTextField = "FlowName";
            ddlStatus.DataValueField = "Id";
            ddlStatus.DataBind();


        }

        private void bindData()
        {
            var list = ESP.HumanResource.BusinessLogic.ITItemsManager.GetList(" and auditorId ="+UserID+" and status not in(-1,4)",null);
            this.gvOM.DataSource = list;
            this.gvOM.DataBind();
        }

        protected void gvOM_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ESP.HumanResource.Entity.ITItemsInfo model = (ITItemsInfo)e.Row.DataItem;
                Label lblStatus = (Label)e.Row.FindControl("lblStatus");

                if (model.Status == 1)
                    lblStatus.Text = "待审批";
                else if (model.Status == 2)
                {
                    lblStatus.Text = "待测试";
                }
                else if (model.Status == 3)
                {
                    lblStatus.Text = "待发布";
                }
                else if (model.Status == 4)
                {
                    lblStatus.Text = "已完成";
                }
                else if (model.Status == -1)
                {
                    lblStatus.Text = "驳回";
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

        }
    }
}