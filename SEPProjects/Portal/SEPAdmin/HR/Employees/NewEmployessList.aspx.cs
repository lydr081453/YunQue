using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.HumanResource.BusinessLogic;
using ESP.HumanResource.Common;

public partial class Employees_NewEmployessList : ESP.Web.UI.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            listBind();
        }
    }

    protected void SearchBtn_Click(object sender, EventArgs e)
    {
        listBind();
    }

    protected void listBind()
    {
        string strCondition = string.Empty;
        strCondition += string.Format(" and a.status = {0} ", Status.WaitEntry);
        if (!string.IsNullOrEmpty(txtName.Text.Trim()))
        {
            strCondition += string.Format(" and (b.lastnamecn+b.firstnamecn like '%{0}%' or b.username like '%{0}%') ", txtName.Text.Trim());
        }
        List<ESP.HumanResource.Entity.EmployeeBaseInfo> list = null;
        //if (ESP.Framework.BusinessLogic.PermissionManager.HasModulePermission("001", this.ModuleInfo.ModuleID, this.UserID))
        //{
        int userid = CurrentUserID;
        int hradmin = int.Parse(System.Configuration.ConfigurationManager.AppSettings["HRAdminID"]);
        string hrIds = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetHRId();

        if (hradmin == CurrentUserID || hrIds.IndexOf(CurrentUserID.ToString()) >= 0)
            userid = 0;
        list = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetWaitEntryUserModelList(userid, strCondition);


        gvE.DataSource = list;
        gvE.DataBind();

        if (gvE.PageCount > 1)
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
        labPageCount.Text = labPageCountT.Text = (gvE.PageIndex + 1).ToString() + "/" + gvE.PageCount.ToString();
        if (gvE.PageCount > 0)
        {
            if (gvE.PageIndex + 1 == gvE.PageCount)
                disButton("last");
            else if (gvE.PageIndex == 0)
                disButton("first");
            else
                disButton("");
        }
    }

    protected void gvE_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblPrint = (Label)e.Row.FindControl("lblPrint");
            ESP.HumanResource.Entity.EmployeeBaseInfo model = (ESP.HumanResource.Entity.EmployeeBaseInfo)e.Row.DataItem;
            if (lblPrint != null)
            {
                if (model.OfferLetterTemplate != 3)//intern
                {
                    lblPrint.Text = " <a href='/HR/Join/InternConfirmPrint.aspx?UserId=" + model.UserID.ToString() + "'";
                    lblPrint.Text += " target='_blank'>";
                    lblPrint.Text += "<img src='/Images/printno2.gif' border='0px;' alt='打印聘用通知' /></a>";
                }
                else
                {
                    lblPrint.Text = " <a href='/HR/Join/ConfirmPrint.aspx?UserId=" + model.UserID.ToString() + "'";
                    lblPrint.Text += " target='_blank'>";
                    lblPrint.Text += "<img src='/Images/printno2.gif' border='0px;' alt='打印聘用通知' /></a>";
                }
            }

            if ((bool)gvE.DataKeys[e.Row.RowIndex].Values[1])
            {
                e.Row.Cells[1].Text = "已发邮件";
            }
            else
            {
                e.Row.Cells[1].Text = "未发邮件";
            }

            //if (ESP.Framework.BusinessLogic.PermissionManager.HasModulePermission("005", this.ModuleInfo.ModuleID, this.UserID))
            //{
            //    e.Row.Cells[9].Text = "";
            //}
        }
    }

    #region 翻页相关
    protected void gvE_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvE.PageIndex = e.NewPageIndex;
        listBind();
    }


    protected void btnLast_Click(object sender, EventArgs e)
    {
        Paging(gvE.PageCount);
    }
    protected void btnFirst_Click(object sender, EventArgs e)
    {
        Paging(0);
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        Paging((gvE.PageIndex + 2) >= gvE.PageCount ? gvE.PageCount : (gvE.PageIndex + 1));
    }
    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        Paging((gvE.PageIndex - 1) < 1 ? 0 : (gvE.PageIndex - 1));
    }
    /// <summary>
    /// 翻页
    /// </summary>
    /// <param name="pageIndex">页码</param>
    private void Paging(int pageIndex)
    {
        GridViewPageEventArgs e = new GridViewPageEventArgs(pageIndex);
        gvE_PageIndexChanging(new object(), e);
    }

    //翻页判断
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

    protected void gvE_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //if (e.CommandName == "Del")
        //{
        //    int sysid = int.Parse(e.CommandArgument.ToString());
        //    #region 日志信息
        //    ESP.HumanResource.Entity.LogInfo logModel = new ESP.HumanResource.Entity.LogInfo();
        //    logModel.LogMedifiedTeme = DateTime.Now;
        //    logModel.Des = "[" + UserInfo.Username + "]删除待入职人员信息";
        //    logModel.LogUserId = UserInfo.UserID;
        //    logModel.LogUserName = UserInfo.Username;
        //    #endregion
        //    if (EmployeeBaseManager.Delete(sysid, logModel))
        //    {
        //        ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('删除成功！');", true);
        //    }
        //    else
        //    {
        //        ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('删除失败！');", true);
        //    }
        //    listBind();
        //}
        //else 
        if (e.CommandName == "Update")
        {
            ////修改员工状态 
            int userId = int.Parse(e.CommandArgument.ToString());
            //ESP.HumanResource.Entity.EmployeeBaseInfo info = EmployeeBaseManager.GetModel(userId);
            //info.Status = Status.Reject;
            //EmployeeBaseManager.Update(info);
            //ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('用户已归入Reject人员库！');", true);
            //listBind();
            Response.Redirect("RejectEdit.aspx?userid=" + userId + "&backUrl=NewEmployessList.aspx");
        }
    }

    protected void gvE_RowUpdating(object sender, GridViewUpdateEventArgs e)
    { }

    protected void btnSendMail_Click(object sender, EventArgs e)
    {
        string userid = "";
        for (int i = 0; i <= gvE.Rows.Count - 1; i++)
        {
            CheckBox cbox = (CheckBox)gvE.Rows[i].FindControl("chkMail");
            if (cbox.Checked == true)
            {
                userid += gvE.DataKeys[i].Value.ToString() + ",";
            }
        }
        if (userid.Length > 0)
        {
            userid = userid.Substring(0, userid.LastIndexOf(","));
            try
            {
                SendMail(userid);
                ShowCompleteMessage("发送邮件成功", "NewEmployessList.aspx");
            }
            catch (Exception ex)
            {
                ESP.Logging.Logger.Add(ex.ToString());
                ShowCompleteMessage("发送邮件失败", "NewEmployessList.aspx");
            }
        }
    }

    #region 发送邮件
    private void SendOfferMail(ESP.HumanResource.Entity.EmployeeBaseInfo empModel)
    {
        string url = "http://" + Request.Url.Authority + "/HR/Join/InternConfirmPrint.aspx?UserId=" + empModel.UserID.ToString();

        string body = ESP.HumanResource.Common.SendMailHelper.ScreenScrapeHtml(url);

        SendMailHelper.SendMail("新员工入职通知", empModel.InternalEmail, body, null);
    }

    //private void SendMail(string userid)
    //{
    //    string recipientAddress = "";
    //    int compID = 0;
    //    int depID = 0;
    //    int gropID = 0;
    //    string userids = "";
    //    string userid2s = "";
    //    List<ESP.HumanResource.Entity.EmployeeBaseInfo> userlist2 = new List<ESP.HumanResource.Entity.EmployeeBaseInfo>();
    //    List<ESP.HumanResource.Entity.EmployeeBaseInfo> userlist = EmployeeBaseManager.GetModelList(" and a.userid in (" + userid + ")");

    //    foreach (ESP.HumanResource.Entity.EmployeeBaseInfo einfo in userlist)
    //    {
    //        einfo.IsSendMail = true;
    //        if (einfo.Code.IndexOf("I") >= 0)
    //        {
    //            SendOfferMail(einfo);
    //        }
    //        userlist2.Add(einfo);
    //    }

    //    List<ESP.HumanResource.Entity.EmployeesInPositionsInfo> lists = EmployeesInPositionsManager.GetModelList(" a.userid in (" + userid + ") order by c.level2ID");

    //    int[] deps = EmployeesInPositionsManager.GetUserDepartmentID(userid);
    //    if (deps.Length > 0)
    //    {
    //        for (int i = 0; i < deps.Length; i++)
    //        {
    //            if (lists.Count > 0)
    //            {
    //                foreach (ESP.HumanResource.Entity.EmployeesInPositionsInfo eip in lists)
    //                {
    //                    if (eip.DepartmentID == deps[i])
    //                    {
    //                        userids += eip.UserID + ",";
    //                        compID = eip.CompanyID;
    //                        depID = eip.DepartmentID;
    //                        gropID = eip.GroupID;
    //                        //调组打破北上广限制，用Description记录北上广对应的组别权限设置
    //                        ESP.Framework.Entity.DepartmentInfo deptModel = ESP.Framework.BusinessLogic.DepartmentManager.Get(gropID);
    //                        if (deptModel != null && !string.IsNullOrEmpty(deptModel.Description))
    //                        {
    //                            compID = int.Parse(deptModel.Description);
    //                        }
    //                    }
    //                }
    //                if (userids.Trim().Length > 1)
    //                {
    //                    userids = userids.Substring(0, userids.Length - 1);

    //                    List<ESP.HumanResource.Entity.UsersInfo> list = UsersManager.GetUserList(compID, ESP.HumanResource.Common.Status.WaitEntrySendMail);
    //                    foreach (ESP.HumanResource.Entity.UsersInfo info in list)
    //                    {
    //                        if (!string.IsNullOrEmpty(info.Email))
    //                        {
    //                            recipientAddress += info.Email.Trim() + " ,";
    //                        }
    //                    }

    //                    if (recipientAddress.Trim().Length > 1)
    //                    {
    //                        recipientAddress = recipientAddress.Substring(0, recipientAddress.Length - 1);
    //                    }

    //                    if (userids.Length > 0)
    //                    {
    //                        string url = "http://" + Request.Url.Authority + "/HR/Print/NewEmployeeMail.aspx?userid=" + userids;

    //                        string body = ESP.HumanResource.Common.SendMailHelper.ScreenScrapeHtml(url);

    //                        SendMailHelper.SendMail("新员工入职通知", recipientAddress, body, null);

    //                        recipientAddress = "";
    //                        userids = "";
    //                    }
    //                }
    //            }
    //        }
    //        if (userlist2.Count > 0)
    //        {
    //            foreach (ESP.HumanResource.Entity.EmployeeBaseInfo einfo in userlist2)
    //            {
    //                EmployeeBaseManager.Update(einfo);
    //            }
    //        }
    //    }
    //}

    private void SendMail(string userid)
    {
        string recipientAddress = "";
        int compID = 0;

        List<ESP.HumanResource.Entity.EmployeeBaseInfo> userlist = EmployeeBaseManager.GetModelList(" and a.userid in (" + userid + ")");

        foreach (ESP.HumanResource.Entity.EmployeeBaseInfo einfo in userlist)
        {
            einfo.IsSendMail = true;
            if (einfo.Code.IndexOf("I") >= 0)
            {
                SendOfferMail(einfo);
            }
            else
            {
                ESP.HumanResource.Entity.EmployeesInPositionsInfo userPosition = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetModel(einfo.UserID);
                ESP.Framework.Entity.DepartmentInfo userDept = ESP.Framework.BusinessLogic.DepartmentManager.Get(userPosition.DepartmentID);
                if (userDept != null && !string.IsNullOrEmpty(userDept.Description))
                {
                    compID = int.Parse(userDept.Description);
                }

                List<ESP.HumanResource.Entity.UsersInfo> list = UsersManager.GetUserList(compID, ESP.HumanResource.Common.Status.WaitEntrySendMail);
                foreach (ESP.HumanResource.Entity.UsersInfo info in list)
                {
                    if (!string.IsNullOrEmpty(info.Email))
                    {
                        recipientAddress += info.Email.Trim() + ",";
                    }
                }

                if (recipientAddress.Trim().Length > 1)
                {
                    recipientAddress = recipientAddress.Substring(0, recipientAddress.Length - 1);
                }
                string url = "http://" + Request.Url.Authority + "/HR/Print/NewEmployeeMail.aspx?userid=" + einfo.UserID;

                string body = ESP.HumanResource.Common.SendMailHelper.ScreenScrapeHtml(url);

                SendMailHelper.SendMail("新员工入职通知", recipientAddress, body, null);

                recipientAddress = "";

                 EmployeeBaseManager.Update(einfo);

            }
        }

               

        }

    
    #endregion

    #region 获得楼层前台的邮箱
    private string GetInformationMail(int groupid)
    {
        string mail = "";
        try
        {
            List<ESP.HumanResource.Entity.UsersInfo> userlist = UsersManager.GetUserListByGroupID(groupid);

            foreach (ESP.HumanResource.Entity.UsersInfo info in userlist)
            {
                if (!string.IsNullOrEmpty(info.Email.Trim()))
                {
                    mail += info.Email.Trim() + " ,";
                }
            }
        }
        catch (Exception ex) { ESP.Logging.Logger.Add(ex.ToString()); }
        return mail;
    }
    #endregion

    #region 获得团队Admin的邮箱
    private string GetDepartmentAdminMail(int departmentid)
    {
        string mail = "";
        try
        {
            List<ESP.HumanResource.Entity.UsersInfo> userlist = UsersManager.GetUserListByDepartmentID(departmentid);
            foreach (ESP.HumanResource.Entity.UsersInfo info in userlist)
            {
                if (!string.IsNullOrEmpty(info.Email.Trim()))
                {
                    mail += info.Email.Trim() + " ,";
                }
            }
        }
        catch (Exception ex) { ESP.Logging.Logger.Add(ex.ToString()); }
        return mail;
    }
    #endregion
}
