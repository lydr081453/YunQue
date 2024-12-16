using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Entity;
using ESP.Finance.BusinessLogic;
using System.Data.SqlClient;
using System.Data;
using ESP.Finance.Utility;
using System.Configuration;

namespace FinanceWeb.Consumption
{
    public partial class ConsumptionList : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindList();
        }

        private void BindList()
        {
            string workflowUsers = ConfigurationManager.AppSettings["ConsumptionWorkFlow10W"];//审批流上的人员可以看所有
            string strWhere = " batchType =4 ";

            if (workflowUsers.IndexOf(CurrentUserID.ToString()) < 0)
            {
                strWhere += " and (CreatorId=@CreatorId) ";//当前登录人只能看自己创建
            }
            //else
            //{
            //    strWhere += " and (status not in(0,1) ) ";
            //}

            
              List<System.Data.SqlClient.SqlParameter> paramlist = new List<System.Data.SqlClient.SqlParameter>();
              SqlParameter CreatorId = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
                    CreatorId.Value = CurrentUserID;
                    paramlist.Add(CreatorId);
                    if (!string.IsNullOrEmpty(txtKey.Text))
                    {
                        strWhere += " and (batchCode like '%@kv%' or purchaseBatchCode like '%@kv%' or description like '%@kv%')";
                        SqlParameter kv = new SqlParameter("@kv", SqlDbType.Int, 4);
                        kv.Value = txtKey.Text.Trim();
                        paramlist.Add(kv);
                    }
                    var batchList = ESP.Finance.BusinessLogic.PNBatchManager.GetList(strWhere, paramlist);
                    this.GvImport.DataSource = batchList;
                    this.GvImport.DataBind();
          
        }

        protected void GvImport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            ESP.Finance.Entity.PNBatchInfo model = (ESP.Finance.Entity.PNBatchInfo)e.Row.DataItem;
             if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
            }
             if (e.Row.RowType == DataControlRowType.DataRow)
             {
                 Label lblStatus = (Label)e.Row.FindControl("lblStatus");
                 LinkButton btnDelete = (LinkButton)e.Row.FindControl("btnDelete");
                 lblStatus.Text = ReturnPaymentType.ReturnStatusString(model.Status.Value, 0, null);

                 Label lblAdj = (Label)e.Row.FindControl("lblAdj");
                 if (model.PeriodID != null && model.PeriodID == 1)
                 {
                     lblAdj.Text = "调整";
                 }
                 else
                     lblAdj.Text = "导入";

                 if (model.Status == (int)PaymentStatus.Created || model.Status == (int)PaymentStatus.Rejected || model.Status == (int)PaymentStatus.Save)
                 {
                     btnDelete.Visible = true;
                 }
                 else
                     btnDelete.Visible = false;

             }
        }

        protected void GvImport_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Del")
            {
                //撤销付款申请
                int ID = int.Parse(e.CommandArgument.ToString());
                if (ESP.Finance.BusinessLogic.PNBatchManager.DeleteConsumption(ID)>0)
                {
                    BindList();
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('删除成功！');", true);
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('删除失败！');", true);
                }
            }
        }

        protected void GvImport_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvImport.PageIndex = e.NewPageIndex;
            BindList();
        }

        protected void btnImport_Click(object sender, EventArgs e)
        {
            Response.Redirect("ConsumptionImp.aspx");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindList();
        }

        protected void btnSearchAll_Click(object sender, EventArgs e)
        {
            txtKey.Text = "";
            BindList();
        }

        protected void btnAdj_Click(object sender, EventArgs e)
        {
            Response.Redirect("ConsumptionImp.aspx?adj=1");
        }
    }
}