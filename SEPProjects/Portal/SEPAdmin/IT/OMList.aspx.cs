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
    public partial class OMList : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindFlow();
                search();
            }
        }

        private void bindFlow()
        {
            var flowList = ESP.HumanResource.BusinessLogic.ITFlowManager.GetList(" and status =1",null);
            ESP.HumanResource.Entity.ITFlowInfo flow =new ITFlowInfo();
            flow.Id =-1;
            flow.FlowName ="请选择..";

            flowList.Insert(0, flow);

            ddlStatus.DataSource = flowList;
            ddlStatus.DataTextField = "FlowName";
            ddlStatus.DataValueField = "Id";
            ddlStatus.DataBind();

             
         }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            search();
        }

        private void search()
        {
            string strwhere = string.Empty;

            if (!string.IsNullOrEmpty(txtCode.Text))
            {
                strwhere += " and (title like '%" + txtCode.Text + "%' or description like '%" + txtCode.Text + "%')";
            }
            if (ddlStatus.SelectedIndex > 0)
            {
                strwhere += " and FlowId = "+ ddlStatus.SelectedValue.ToString();
            }

            

            SqlParameter[] parameters = {					
					new SqlParameter("@userid", SqlDbType.Int)         
                                        };
            parameters[0].Value = UserID;
            var flow = ESP.HumanResource.BusinessLogic.ITFlowManager.GetList(" and(directorId=@userid or testId=@userid or publisherId=@userid)", parameters.ToList());

            string flowIds = string.Empty;

            foreach (var f in flow)
            {
                flowIds += f.Id.ToString() + ",";
            }

            flowIds = flowIds.TrimEnd(',');

            if (!string.IsNullOrEmpty(flowIds))
            {
                strwhere += " and (UserId=" + UserID + "or FlowId in(" + flowIds + "))";
            }
            else
            {
                strwhere += " and UserId=" + UserID;
            }

            var itlist = ESP.HumanResource.BusinessLogic.ITItemsManager.GetList(strwhere, null);

            this.gvOM.DataSource = itlist;
            this.gvOM.DataBind();

        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            Response.Redirect("OMEdit.aspx");
        }

        protected void gvOM_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ESP.HumanResource.Entity.ITItemsInfo model = (ITItemsInfo)e.Row.DataItem;
                Label lblStatus = (Label)e.Row.FindControl("lblStatus");
                HyperLink hyperEdit = (HyperLink)e.Row.FindControl("hyperEdit");

                if (model.Status == 1)
                {
                    lblStatus.Text = "待审批";
                    hyperEdit.Visible = false;
                    
                }
                else if (model.Status == 2)
                {
                    lblStatus.Text = "待测试";
                    hyperEdit.Visible = false;
                }
                else if (model.Status == 3)
                {
                    lblStatus.Text = "待发布";
                    hyperEdit.Visible = false;
                }
                else if (model.Status == 4)
                {
                    lblStatus.Text = "已完成";
                    hyperEdit.Visible = false;
                }
                else if (model.Status == -1)
                {
                    lblStatus.Text = "驳回";
                    hyperEdit.NavigateUrl = "OMEdit.aspx?id="+model.Id;
                }
            }
        }

        protected void gvOM_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
    }
}