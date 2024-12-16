using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PurchaseWeb.UserPointRule.view
{
    public partial class UserPointEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindList();
            }
        }

        private void bindList()
        {
            int userid = int.Parse(Request["UserId"]);
            ESP.Framework.Entity.UserInfo userModel = ESP.Framework.BusinessLogic.UserManager.Get(userid);
            ESP.UserPoint.Entity.UserPointInfo point =ESP.UserPoint.BusinessLogic.UserPointManager.GetModel(userid);
            IList<ESP.Framework.Entity.EmployeePositionInfo> positions = ESP.Framework.BusinessLogic.DepartmentPositionManager.GetEmployeePositions(userModel.UserID);
            ESP.Finance.Entity.DepartmentViewInfo dept = ESP.Finance.BusinessLogic.DepartmentViewManager.GetModel(positions[0].DepartmentID);

            lblDept.Text= dept.level1 + "-" + dept.level2 + "-" + dept.level3;
            lblEmail.Text = userModel.Email;
            lblUserName.Text = userModel.LastNameCN + userModel.FirstNameCN;
            lblPoint.Text = point.SP.ToString();

            IList<ESP.UserPoint.Entity.UserPointRecordInfo> records = ESP.UserPoint.BusinessLogic.UserPointRecordManager.GetList(" and userid="+userid.ToString());
            this.gvG.DataSource = records;
            this.gvG.DataBind();

            labAllNum.Text = labAllNumT.Text = records.Count.ToString();
            labPageCount.Text = labPageCountT.Text = (gvG.PageIndex + 1).ToString() + "/" + gvG.PageCount.ToString();
            labAllNumT.Text = labAllNum.Text;
            labPageCountT.Text = labPageCount.Text;
        }

        protected void btnPoint_Click(object sender ,EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPoint.Text))
            {
                try
                {
                    ESP.UserPoint.Entity.UserPointRecordInfo record = new ESP.UserPoint.Entity.UserPointRecordInfo();
                    record.GiftID = 0;
                    record.Memo = "后台调整";
                    record.RuleID = 0;

                    record.Points = int.Parse(txtPoint.Text);
                    record.UserID = int.Parse(Request["UserId"]);
                    record.OperationTime = DateTime.Now;
                    ESP.UserPoint.BusinessLogic.UserPointRecordManager.Add(record);
                }
                catch
                { 
                   
                }
                bindList();
            }
        }

        protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Visible = false;
                e.Row.Cells[1].Visible = false;
                e.Row.Cells[2].Visible = false;
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ESP.UserPoint.Entity.UserPointRecordInfo record = (ESP.UserPoint.Entity.UserPointRecordInfo)e.Row.DataItem;
                ESP.UserPoint.Entity.GiftInfo gift=null;
                ESP.UserPoint.Entity.UserPointRuleInfo rule = null;

                Label lblType = (Label)e.Row.FindControl("lblType");

                if (record.GiftID != 0)
                {
                    gift = ESP.UserPoint.BusinessLogic.GiftManager.GetModel(record.GiftID);
                    lblType.Text = gift.Name;
                }
                if (record.RuleID != 0)
                {
                    rule = ESP.UserPoint.BusinessLogic.UserPointRuleManager.GetModel(record.RuleID);
                    lblType.Text = rule.RuleName + "["+rule.RuleKey+"]";
                }
            }
        }

        #region "Page"
        protected void btnLast_Click(object sender, EventArgs e)
        {
            Paging(gvG.PageCount);
        }
        protected void btnFirst_Click(object sender, EventArgs e)
        {
            Paging(0);
        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
            Paging((gvG.PageIndex + 1) > gvG.PageCount ? gvG.PageCount : (gvG.PageIndex + 1));
        }
        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            Paging((gvG.PageIndex - 1) < 0 ? 0 : (gvG.PageIndex - 1));
        }

        private void Paging(int pageIndex)
        {
            GridViewPageEventArgs e = new GridViewPageEventArgs(pageIndex);
            gvG_PageIndexChanging(new object(), e);
        }


        protected void gvG_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvG.PageIndex = e.NewPageIndex;
            bindList();
        }

        #endregion

    }
}
