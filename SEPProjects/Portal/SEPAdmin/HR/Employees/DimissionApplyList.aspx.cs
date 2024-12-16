using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;
using ESP.HumanResource.Common;
using ESP.HumanResource.BusinessLogic;

public partial class Employees_DimissionApplyList : ESP.Web.UI.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ListBind();
        }
    }

    /// <summary>
    /// 绑定列表
    /// </summary>
    private void ListBind()
    {
        List<SqlParameter> parms = new List<SqlParameter>();
        StringBuilder strWhere = new StringBuilder();
        strWhere.Append(" and isFinish!=@isFinish");
        parms.Add(new SqlParameter("@isFinish", false));
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
            strWhere.Append(" and datediff(dd, cast('").Append(txtBeginTime.Text.Trim()).Append("' as smalldatetime), dimissionDate ) >= 0 ");
        }
        if (txtEndTime.Text.Trim() != "")
        {
            strWhere.Append(" and datediff(dd, cast('").Append(txtEndTime.Text.Trim()).Append("' as smalldatetime), dimissionDate ) <= 0");
        }

        List<ESP.HumanResource.Entity.EmployeesInPositionsInfo> empinpos = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetModelList(" a.userid=" + UserInfo.UserID);
        string empid = "";

        int IsBeijing = 0;
        for (int i = 0; i < empinpos.Count; i++)
        {
            if (empinpos[i].CompanyID == 19)
            {
                IsBeijing = 1;
                break;
            }
            empid += empinpos[i].CompanyID.ToString() + ",";
        }
        if (empid.Length > 0 && IsBeijing == 0)
        {
            empid = empid.Substring(0, empid.Length - 1);
            strWhere.Append(string.Format(" and companyID in ({0})", empid));
        }
        List<ESP.HumanResource.Entity.DimissionInfo> list = DimissionManager.GetModelList(strWhere.ToString(), parms);

        gvList.DataSource = list;
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
        if (list.Count > 0)
        {
            tabTop.Visible = true;
            tabBottom.Visible = true;
        }
        else
        {
            tabTop.Visible = false;
            tabBottom.Visible = false;
        }

        labAllNum.Text = labAllNumT.Text = list.Count.ToString();
        labPageCount.Text = labPageCountT.Text = (gvList.PageIndex + 1).ToString() + "/" + gvList.PageCount.ToString();
        if (gvList.PageCount > 0)
        {
            if (gvList.PageIndex + 1 == gvList.PageCount)
                disButton("last");
            else if (gvList.PageIndex == 0)
                disButton("first");
            else
                disButton("");
        }

        int year = DateTime.Now.Year - 10;
        for (int i = 0; i < 20; i++)
        {
            drpYear.Items.Insert(i, new ListItem((year + i).ToString(), (year + i).ToString()));
        }

        drpYear.SelectedValue = DateTime.Now.Year.ToString();

        for (int i = 1; i <= 12; i++)
        {
            drpMonth.Items.Insert(i - 1, new ListItem((i).ToString("00"), (i).ToString("00")));
        }

        drpMonth.SelectedValue = DateTime.Now.Month.ToString("00");
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

    protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (!string.IsNullOrEmpty(gvList.DataKeys[e.Row.RowIndex].Values[1].ToString()))
            {
                if ("1" == gvList.DataKeys[e.Row.RowIndex].Values[1].ToString())
                {
                    e.Row.Cells[4].Text = "已复职";
                }
            }
        }
    }
    protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //if (e.CommandName == "Del")
        //{
        //    int id = int.Parse(e.CommandArgument.ToString());
        //    ESP.HumanResource.Entity.DimissionInfo model = DimissionManager.GetModel(id);
        //    DimissionManager.Delete(id, LogManager.GetLogModel(UserInfo.Username + "删除了" + model.userName + "的离职申请", UserInfo.UserID,UserInfo.Username,model.userId,model.userName,Status.Log));
        //    ListBind();
        //}

        //重新入职
        if (e.CommandName == "Up")
        {
            int id = int.Parse(e.CommandArgument.ToString());
            ESP.HumanResource.Entity.DimissionInfo model = DimissionManager.GetModel(id);
            if (model.status == 0)
            {
                ESP.HumanResource.Entity.EmployeeBaseInfo baseinfo = EmployeeBaseManager.GetModel(model.userId);
                ESP.HumanResource.Entity.UsersInfo user = UsersManager.GetModel(baseinfo.UserID);
                ESP.HumanResource.Entity.UsersInfo olduser = UsersManager.GetModel(baseinfo.UserID);
                List<ESP.HumanResource.Entity.EmployeesInPositionsInfo> depslist = EmployeesInPositionsManager.GetModelList(" a.userid=" + baseinfo.UserID);
                ESP.HumanResource.Entity.EmployeesInPositionsInfo deps = new ESP.HumanResource.Entity.EmployeesInPositionsInfo();
                if (depslist.Count > 0)
                {
                    deps.UserID = depslist[0].UserID;
                    deps.DepartmentID = depslist[0].GroupID;
                    deps.DepartmentPositionID = depslist[0].DepartmentPositionID;
                    deps.IsActing = false;
                    deps.IsManager = false;
                    baseinfo.EmployeeJobInfo.companyid = depslist[0].CompanyID;
                    baseinfo.EmployeeJobInfo.companyName = depslist[0].CompanyName;
                    baseinfo.EmployeeJobInfo.departmentid = depslist[0].DepartmentID;
                    baseinfo.EmployeeJobInfo.departmentName = depslist[0].DepartmentName;
                    baseinfo.EmployeeJobInfo.groupid = depslist[0].GroupID;
                    baseinfo.EmployeeJobInfo.groupName = depslist[0].GroupName;
                    baseinfo.EmployeeJobInfo.joinjobID = depslist[0].DepartmentPositionID;
                    baseinfo.EmployeeJobInfo.joinJob = depslist[0].DepartmentPositionName;
                }
                ESP.HumanResource.Entity.SnapshotsInfo snap = SnapshotsManager.GetTopModel(baseinfo.UserID);
                //用户信息
                user.IsDeleted = false;
                user.Status = Status.WaitEntry;
                user.LastActivityDate = DateTime.Now;
                user.CreatedDate = DateTime.Now;
                user.IsApproved = false;
                user.IsLockedOut = false;
                //旧用户信息
                olduser.Username = "$" + olduser.Username;
                //员工信息
                baseinfo.EmployeeJobInfo.joinDate = DateTime.Now;
                baseinfo.CreatedTime = DateTime.Now;
                baseinfo.Creator = UserInfo.UserID;
                baseinfo.CreatorName = UserInfo.Username;
                baseinfo.Status = Status.WaitEntry;
                baseinfo.IsSendMail = false;

                //离职信息
                model.status = 1;

                #region 日志信息
                ESP.HumanResource.Entity.LogInfo logModel = new ESP.HumanResource.Entity.LogInfo();
                logModel.LogMedifiedTeme = DateTime.Now;
                logModel.Des = "重新入职员工[" + user.LastNameCN + user.FirstNameCN + "]";
                logModel.LogUserId = UserInfo.UserID;
                logModel.LogUserName = UserInfo.Username;
                #endregion

                if (EmployeeBaseManager.Add(baseinfo, user, olduser, deps, snap, model, logModel))
                {
                    try
                    {
                        ESP.Framework.BusinessLogic.UserManager.ResetPassword(user.Username, "password");
                    }
                    catch (System.Exception)
                    {

                    }
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('重新启用成功！请到待入职中确认复职人员的新员工信息');", true);
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('重新启用失败！');", true);
                }
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('此人已复职，无法再次启动复职！');", true);
            }
            ListBind();
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

    protected void btnSendMail_Click(object sender, EventArgs e)
    {
        try
        {
            string year = drpYear.SelectedItem.Value;
            string month = drpMonth.SelectedItem.Value;
            string date = year + "-" + month;
            string recipientAddress = "";

            List<ESP.HumanResource.Entity.UsersInfo> list = UsersManager.GetUserList(19, ESP.HumanResource.Common.Status.DimissionSendMail);
            foreach (ESP.HumanResource.Entity.UsersInfo info in list)
            {
                if (!string.IsNullOrEmpty(info.Email))
                {
                    recipientAddress += info.Email.Trim() + " ,";
                }
            }
            if (recipientAddress.Trim().Length > 1)
            {
                recipientAddress = recipientAddress.Substring(0, recipientAddress.Length - 1);
            }

            string url = "http://" + Request.Url.Authority + "/HR/Print/DimissionMail.aspx?year=" + year + "&month=" + month;

            string body = ESP.HumanResource.Common.SendMailHelper.ScreenScrapeHtml(url);

            SendMailHelper.SendMail(date + "离职员工信息", recipientAddress, body, null);

            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location='DimissionApplyList.aspx';alert('邮件发送成功！');", true);
        }
        catch (Exception ex)
        {
            ESP.Logging.Logger.Add(ex.ToString());
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location='DimissionApplyList.aspx';alert('邮件发送失败！');", true);
        }
    }
}
