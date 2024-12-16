using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ESP.HumanResource.Entity;
using ESP.HumanResource.Common;

namespace SEPAdmin.HR.Join
{
    public partial class HeadAccountEdit : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        private void BindData()
        {
            if (string.IsNullOrEmpty(this.hidGroupId.Value))
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('请选择部门！');", true);
                return;
            }
            int groupid = int.Parse(this.hidGroupId.Value);
            int[] goupids = new int[1];
            goupids[0] = groupid;

            var empList = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetUsersByDeparmtnetID(goupids, " and a.status in(0,1,2,3,10,12,13)");
            gvList.DataSource = empList;
            gvList.DataBind();

            var deptlist = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelList(" DirectorId=" + UserID.ToString() + " or ManagerId=" + UserID.ToString() + " or CEOId=" + UserID.ToString());
            string depts = string.Empty;
            foreach (var dep in deptlist)
            {
                depts += dep.DepId.ToString() + ",";
            }

            depts = depts.TrimEnd(',');
            string strwhere = string.Empty;
            strwhere = "(CreatorId=" + UserID.ToString();

            if (!string.IsNullOrEmpty(depts))
            {
                strwhere += " or groupid in(" + depts + "))";
            }
            else
            { strwhere += ")"; }

            strwhere += " and replaceuserid not in(select userid from sep_employees where status in(0,1,2,3,10,12,13))";

            var headaccountList = new ESP.HumanResource.BusinessLogic.HeadAccountManager().GetList(" and  (" + strwhere + ")");

            this.gvHeadAccount.DataSource = headaccountList;
            this.gvHeadAccount.DataBind();

        }

        protected void btnCheck_Click(object sender, EventArgs e)
        {
            BindData();
            trNew.Visible = true;
        }
        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            EmployeeBaseInfo model = (EmployeeBaseInfo)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int groupid = int.Parse(this.hidGroupId.Value);
                Label lblReplace = (Label)e.Row.FindControl("lblReplace");

                if (Request["talentId"] == null)
                {

                    lblReplace.Text = "<a href='HeadAccountCreate.aspx?deptid=" + groupid.ToString() + "&replaceid=" + model.UserID.ToString() + "' >替换</a>";

                }
                else
                {
                    string talentId = Request["talentId"].ToString();
                    lblReplace.Text = "<a href='HeadAccountCreate.aspx?deptid=" + groupid.ToString() + "&replaceid=" + model.UserID.ToString() + "&talentId=" + talentId + "' >替换</a>";
                }
            }
        }

        protected void gvHeadAccount_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            HeadAccountInfo model = (HeadAccountInfo)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                PositionLevelsInfo pb = ESP.HumanResource.BusinessLogic.PositionLevelsManager.GetModel(model.LevelId);
                if (model.OfferLetterUserId != 0)
                {
                    Label lblUsername = (Label)e.Row.FindControl("lblUsername");
                    ESP.HumanResource.Entity.UsersInfo empModel = ESP.HumanResource.BusinessLogic.UsersManager.GetModel(model.OfferLetterUserId);
                    lblUsername.Text = empModel.LastNameCN + empModel.FirstNameCN; ;
                }
                Label lblSalary = (Label)e.Row.FindControl("lblSalary");
                lblSalary.Text = pb.SalaryLow.ToString("#,##0.00") + " - " + pb.SalaryHigh.ToString("#,##0.00");
            }
        }
    }
}
