using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using System.Text;
using System.Data.SqlClient;

namespace SEPAdmin.HR.Dimission
{
    public partial class UnfinishedDimissionList : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ListBind();
            }
        }

        /// <summary>
        /// 绑定待审批的离职单信息
        /// </summary>
        private void ListBind()
        {
            int auditUserid = UserID;
            StringBuilder strWhere = new StringBuilder();
            List<SqlParameter> parms = new List<System.Data.SqlClient.SqlParameter>();

            if (txtUserCode.Text.Trim() != "")
            {
                strWhere.Append(" and userCode like '%'+@userCode+'%'");
                parms.Add(new SqlParameter("@userCode", txtUserCode.Text.Trim()));
            }
            if (txtuserName.Text.Trim() != "")
            {
                strWhere.Append(" and username like '%'+@username+'%'");
                parms.Add(new SqlParameter("@username", txtuserName.Text.Trim()));
            }
            if (txtDepartments.Text.Trim() != "")
            {
                strWhere.Append(" and departmentName like '%'+@departmentname+'%'");
                parms.Add(new SqlParameter("@departmentname", txtDepartments.Text.Trim()));
            }

            if (txtBeginTime.Text.Trim() != "")
            {
                strWhere.Append(" and datediff(dd, cast('").Append(txtBeginTime.Text.Trim()).Append("' as smalldatetime), lastday ) >= 0 ");
            }
            if (txtEndTime.Text.Trim() != "")
            {
                strWhere.Append(" and datediff(dd, cast('").Append(txtEndTime.Text.Trim()).Append("' as smalldatetime), lastday ) <= 0");
            }

            DataSet ds = ESP.HumanResource.BusinessLogic.DimissionFormManager.GetUnfinishedDimissionList(strWhere.ToString(), parms);

            gvList.DataSource = ds;
            gvList.DataBind();

            if (gvList.PageCount > 1)
            {
                PageBottom.Visible = true;
                PageTop.Visible = true;
            }
            else
            {
                PageBottom.Visible = false;
                PageTop.Visible = false;
            }
            if (gvList.Rows.Count > 0)
            {
                tabTop.Visible = true;
                tabBottom.Visible = true;
            }
            else
            {
                tabTop.Visible = false;
                tabBottom.Visible = false;
            }

            labAllNum.Text = labAllNumT.Text = gvList.Rows.Count.ToString();
            labPageCount.Text = labPageCountT.Text = (gvList.PageIndex + 1).ToString() + "/" + gvList.PageCount.ToString();
        }

        /// <summary>
        /// 绑定离职单据信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
            {
                string status = e.Row.Cells[5].Text;
                DataRowView dr = e.Row.DataItem as DataRowView;

                DateTime? lastDay = null;
                if (dr["LastDay"] != null && !string.IsNullOrEmpty(dr["LastDay"].ToString()))
                {
                    lastDay = DateTime.Parse(dr["LastDay"].ToString()); // 离职日期
                }
                int statusInt = 1;
                if (!int.TryParse(status, out statusInt))
                {
                    statusInt = 1;
                }

                string strStatus = "未提交";
                switch (statusInt)
                {
                    case (int)ESP.HumanResource.Common.DimissionFormStatus.NotSubmit:
                        strStatus = "未提交";
                        break;
                    case (int)ESP.HumanResource.Common.DimissionFormStatus.WaitDirector:
                        strStatus = "待总监审批";
                        break;
                    case (int)ESP.HumanResource.Common.DimissionFormStatus.WaitReceiver:
                        strStatus = "待交接人确认";
                        break;
                    case (int)ESP.HumanResource.Common.DimissionFormStatus.WaitManager:
                        strStatus = "待总经理审批";
                        break;
                    case (int)ESP.HumanResource.Common.DimissionFormStatus.WaitGroupHR:
                        strStatus = "待团队行政审批";
                        break;
                    case (int)ESP.HumanResource.Common.DimissionFormStatus.WaitHR1:
                        strStatus = "待集团人力资源审批";
                        break;
                    case (int)ESP.HumanResource.Common.DimissionFormStatus.WaitHRIT:
                        strStatus = "待集团人力资源、IT部审批";
                        break;
                    case (int)ESP.HumanResource.Common.DimissionFormStatus.WaitFinance:
                        strStatus = "待财务审批";
                        break;
                    case (int)ESP.HumanResource.Common.DimissionFormStatus.WaitIT:
                        strStatus = "待IT确认";
                        break;
                    case (int)ESP.HumanResource.Common.DimissionFormStatus.WaitAdministration:
                        strStatus = "待集团行政审批";
                        break;
                    case (int)ESP.HumanResource.Common.DimissionFormStatus.WaitHR2:
                        strStatus = "待集团人力资源审批";
                        break;
                    case (int)ESP.HumanResource.Common.DimissionFormStatus.AuditComplete:
                        strStatus = "审批通过";
                        break;
                    case (int)ESP.HumanResource.Common.DimissionFormStatus.Overrule:
                        strStatus = "审批驳回";
                        break;
                    default:
                        strStatus = "未提交";
                        break;
                }

                // 判断该离职单是否是处于当前登录人审批
                bool b = ESP.HumanResource.BusinessLogic.DimissionDetailsManager.CheckDimissionData(UserID, int.Parse(dr["DimissionId"].ToString()));

                if (!b)
                {
                    e.Row.Cells[7].Text = "";
                }
                if (statusInt == (int)ESP.HumanResource.Common.DimissionFormStatus.WaitGroupHR && lastDay.Value.Date > DateTime.Now)
                {
                    e.Row.Cells[7].Text = "";
                }

                if (ESP.Framework.BusinessLogic.PermissionManager.HasModulePermission("003", this.ModuleInfo.ModuleID, this.UserID) &&
                    statusInt == (int)ESP.HumanResource.Common.DimissionFormStatus.AuditComplete)  // 集团人力审批权限
                {
                    e.Row.Cells[7].Text = "<a href=\"DimissionFormPrint.aspx?dimissionid=" + int.Parse(dr["DimissionId"].ToString()) + "\" title=\"打印离职单\" target=\"_blank\"><img src=\"../../images/printno.gif\" /></a>"
                        + "&nbsp;&nbsp;<a href=\"DimissionCertification.aspx?dimissionid=" + int.Parse(dr["DimissionId"].ToString()) + "\" title=\"打印离职证明\" target=\"_blank\"><img src=\"../../images/printno2.gif\" /></a>";
                }

                e.Row.Cells[5].Text = strStatus;
            }
        }

        /// <summary>
        /// 检索按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ListBind();
        }

        #region 分页设置
        protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvList.PageIndex = e.NewPageIndex;
            ListBind();
        }

        protected void btnLast_Click(object sender, EventArgs e)
        {
            Paging(gvList.PageCount);
        }
        protected void btnFirst_Click(object sender, EventArgs e)
        {
            Paging(0);
        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
            Paging((gvList.PageIndex + 1) > gvList.PageCount ? gvList.PageCount : (gvList.PageIndex + 1));
        }
        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            Paging((gvList.PageIndex - 1) < 0 ? 0 : (gvList.PageIndex - 1));
        }

        /// <summary>
        /// 翻页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        private void Paging(int pageIndex)
        {
            GridViewPageEventArgs e = new GridViewPageEventArgs(pageIndex);
            gvList_PageIndexChanging(new object(), e);
        }

        /// <summary>
        /// 分页按钮的显示设置
        /// </summary>
        /// <param name="page"></param>
        private void disButton(string page)
        {
            switch (page)
            {
                case "first":
                    btnFirst.Enabled = false;
                    btnPrevious.Enabled = false;
                    btnNext.Enabled = true;
                    btnLast.Enabled = true;

                    btnFirst2.Enabled = false;
                    btnPrevious2.Enabled = false;
                    btnNext2.Enabled = true;
                    btnLast2.Enabled = true;
                    break;
                case "last":
                    btnFirst.Enabled = true;
                    btnPrevious.Enabled = true;
                    btnNext.Enabled = false;
                    btnLast.Enabled = false;

                    btnFirst2.Enabled = true;
                    btnPrevious2.Enabled = true;
                    btnNext2.Enabled = false;
                    btnLast2.Enabled = false;
                    break;
                default:
                    btnFirst.Enabled = true;
                    btnPrevious.Enabled = true;
                    btnNext.Enabled = true;
                    btnLast.Enabled = true;

                    btnFirst2.Enabled = true;
                    btnPrevious2.Enabled = true;
                    btnNext2.Enabled = true;
                    btnLast2.Enabled = true;
                    break;
            }
        }
        #endregion
    }
}