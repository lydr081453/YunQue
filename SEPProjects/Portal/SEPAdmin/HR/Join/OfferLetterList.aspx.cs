using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.HumanResource.Common;
using ESP.HumanResource.BusinessLogic;
using System.Net.Mail;

/// <summary>
/// 已发送Offer Letter，但未确认；已保存但未发送Offer Letter，用户列表
/// </summary>
public partial class OfferLetterList : ESP.Web.UI.PageBase
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

    /// <summary>
    /// 查询已保存和Offer Letter未确认的用户列表。
    /// </summary>
    protected void listBind()
    {
        string strCondition = string.Empty;
        // 查询未发送Offer Letter和Offer Letter未确认的员工列表。
        strCondition += string.Format(" and a.status in ({0},{1},{2},{3},{4}) ", Status.IsSaved, Status.IsSendOfferLetter, Status.WaitEntry, Status.OfferHRAudit, Status.OfferFinanceAudit);
        if (!string.IsNullOrEmpty(txtName.Text.Trim()))
        {
            strCondition += string.Format(" and (b.lastnamecn+b.firstnamecn like '%{0}%' or b.username like '%{0}%') ", txtName.Text.Trim());
        }

        List<ESP.HumanResource.Entity.EmployeeBaseInfo> list = null;
        if (!ESP.Framework.BusinessLogic.PermissionManager.HasModulePermission("001", this.ModuleInfo.ModuleID, this.UserID))
        {
            strCondition += string.Format(" and a.OfferLetterSender={0} ", UserID);
        }
        list = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetUserModelList(strCondition);
        list = list.OrderByDescending(x => x.UserID).ToList<ESP.HumanResource.Entity.EmployeeBaseInfo>();
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
            Label lblAuditor = (Label)e.Row.FindControl("lblAuditor");
            ESP.HumanResource.Entity.EmployeeBaseInfo model = (ESP.HumanResource.Entity.EmployeeBaseInfo)e.Row.DataItem;
            if (lblPrint != null)
            {
                if (model.OfferLetterTemplate != 3)//intern
                {
                    lblPrint.Text = " <a href='InternConfirmPrint.aspx?UserId=" + model.UserID.ToString() + "'";
                    lblPrint.Text += " target='_blank'>";
                    lblPrint.Text += "<img src='/Images/printno2.gif' border='0px;' alt='打印聘用通知' /></a>";
                }
                else
                {
                    lblPrint.Text = " <a href='ConfirmPrint.aspx?UserId=" + model.UserID.ToString() + "'";
                    lblPrint.Text += " target='_blank'>";
                    lblPrint.Text += "<img src='/Images/printno2.gif' border='0px;' alt='打印聘用通知' /></a>";
                }
            }
            if ((int)gvE.DataKeys[e.Row.RowIndex].Values[1] == ESP.HumanResource.Common.Status.OfferHRAudit)
            {
                e.Row.Cells[3].Text = "待HR总监审核";
                e.Row.Cells[8].Text = "";
                e.Row.Cells[10].Text = "";
                e.Row.Cells[12].Text = "";
            }
            else if ((int)gvE.DataKeys[e.Row.RowIndex].Values[1] == ESP.HumanResource.Common.Status.OfferFinanceAudit)
            {
                e.Row.Cells[3].Text = "待财务审批";
                e.Row.Cells[8].Text = "";
                e.Row.Cells[10].Text = "";
                e.Row.Cells[12].Text = "";
            }
            else if ((int)gvE.DataKeys[e.Row.RowIndex].Values[1] == ESP.HumanResource.Common.Status.IsSendOfferLetter)
            {
                e.Row.Cells[3].Text = "已审核";
                //e.Row.Cells[8].Text = "";
                e.Row.Cells[10].Text = "";
                //e.Row.Cells[12].Text = "";
            }
            else if ((int)gvE.DataKeys[e.Row.RowIndex].Values[1] == ESP.HumanResource.Common.Status.WaitEntry)
            {
                e.Row.Cells[3].Text = "已确认入职";
                e.Row.Cells[8].Text = "";
                e.Row.Cells[10].Text = "";
                //e.Row.Cells[11].Text = "";
                e.Row.Cells[12].Text = "";
            }
            else
            {
                e.Row.Cells[3].Text = "未审核";
                e.Row.Cells[12].Text = "";
            }

        }
    }

    protected void gvE_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Del")
        {
            int sysid = int.Parse(e.CommandArgument.ToString());
            ESP.HumanResource.Entity.EmployeeBaseInfo info = EmployeeBaseManager.GetModel(sysid);
            if (info.Status == ESP.HumanResource.Common.Status.WaitEntry || info.Status == ESP.HumanResource.Common.Status.OfferHRAudit || info.Status == ESP.HumanResource.Common.Status.IsSendOfferLetter)
            {
                ShowCompleteMessage("信息已提交，不能进行删除！", "OfferLetterList.aspx");
            }
            else
            {

                #region 日志信息
                ESP.HumanResource.Entity.LogInfo logModel = new ESP.HumanResource.Entity.LogInfo();
                logModel.LogMedifiedTeme = DateTime.Now;
                logModel.Des = "[" + UserInfo.Username + "]删除待入职人员信息";
                logModel.LogUserId = UserInfo.UserID;
                logModel.LogUserName = UserInfo.Username;
                #endregion
                if (EmployeeBaseManager.Delete(sysid, logModel))
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('删除成功！');", true);
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('删除失败！');", true);
                }
                listBind();
            }
        }
        else if (e.CommandName == "Update")
        {
            ////修改员工状态 
            int userId = int.Parse(e.CommandArgument.ToString());
            //ESP.HumanResource.Entity.EmployeeBaseInfo info = EmployeeBaseManager.GetModel(userId);
            //info.Status = Status.Reject;
            //EmployeeBaseManager.Update(info);
            //ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('用户已归入Reject人员库！');", true);
            //listBind();
            Response.Redirect("/hr/Employees/RejectEdit.aspx?userid=" + userId + "&backUrl=/hr/join/OfferletterList.aspx");
        }
        else if (e.CommandName == "Mail")
        {
            int userId = int.Parse(e.CommandArgument.ToString());
            ESP.HumanResource.Entity.EmployeeBaseInfo model = EmployeeBaseManager.GetModel(userId);
            decimal pay = model.Pay;
            decimal performance = model.Performance;

            if (!string.IsNullOrEmpty(model.PrivateEmail))
            {
                string url = "";
                //if (model.OfferLetterTemplate == 3)
                //    url = "http://" + Request.Url.Authority + "/HR/Join/InternOfferLetter.aspx?userid=" + model.UserID + "&nowBasePay=" + pay.ToString("#,##0.00") + "&nowMeritPay=" + performance.ToString("#,##0.00") + "&showSalary=1";
                //else if (model.OfferLetterTemplate == 4)
                //    url = "http://" + Request.Url.Authority + "/HR/Join/SalesOfferLetter.aspx?userid=" + model.UserID + "&nowBasePay=" + pay.ToString("#,##0.00") + "&nowMeritPay=" + performance.ToString("#,##0.00") + "&showSalary=1";
                //else if (model.OfferLetterTemplate == 5)
                //    url = "http://" + Request.Url.Authority + "/HR/Join/OfferLetter12.aspx?userid=" + model.UserID + "&nowBasePay=" + pay.ToString("#,##0.00") + "&nowMeritPay=" + performance.ToString("#,##0.00") + "&showSalary=1";
                //else if (model.OfferLetterTemplate == 6)
                //    url = "http://" + Request.Url.Authority + "/HR/Join/SalesOfferLetter.aspx?userid=" + model.UserID + "&nowBasePay=" + pay.ToString("#,##0.00") + "&nowMeritPay=" + performance.ToString("#,##0.00") + "&showSalary=1";
                //else
                    url = "http://" + Request.Url.Authority + "/HR/Join/OfferLetter.aspx?userid=" + model.UserID + "&nowBasePay=" + pay.ToString("#,##0.00") + "&nowMeritPay=" + performance.ToString("#,##0.00") + "&showSalary=1";
                string body = ESP.HumanResource.Common.SendMailHelper.ScreenScrapeHtml(url);
                ESP.Mail.MailManager.Send("聘用任职信", body, true, new MailAddress[] { new MailAddress(model.PrivateEmail) });
                ShowCompleteMessage("聘用任职信发送成功！", "OfferLetterList.aspx");
            }
            else
            {
                ESP.Logging.Logger.Add("没有填写" + model.FullNameCN + "个人邮箱。");
                ShowCompleteMessage("聘用任职信发送失败！", "OfferLetterList.aspx");
            }
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
                //SendMail(userid);
                ShowCompleteMessage("发送邮件成功", "NewEmployessList.aspx");
            }
            catch (Exception ex)
            {
                ESP.Logging.Logger.Add(ex.ToString());
                ShowCompleteMessage("发送邮件失败", "NewEmployessList.aspx");
            }
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

    #region 发送邮件
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
    //    userlist2 = new List<ESP.HumanResource.Entity.EmployeeBaseInfo>();
    //    foreach (ESP.HumanResource.Entity.EmployeeBaseInfo einfo in userlist)
    //    {
    //        einfo.IsSendMail = true;
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
    //                        if (eip.WorkCountry == deps[i].ToString() || string.IsNullOrEmpty(eip.WorkCountry))
    //                        {
    //                            userids += eip.UserID + ",";
    //                            compID = eip.CompanyID;
    //                            depID = eip.DepartmentID;
    //                            gropID = eip.GroupID;
    //                        }
    //                        else
    //                        {
    //                            userid2s += eip.UserID + ",";
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

    //                    recipientAddress += GetInformationMail(gropID);
    //                    recipientAddress += GetDepartmentAdminMail(depID);

    //                    if (recipientAddress.Trim().Length > 1)
    //                    {
    //                        recipientAddress = recipientAddress.Substring(0, recipientAddress.Length - 1);
    //                    }

    //                    if (userids.Length > 0)
    //                    {
    //                        string url = "http://" + Request.Url.Authority + "/HR/Print/NewEmployeeMail.aspx?userid=" + userids;

    //                        string body = ESP.HumanResource.Common.SendMailHelper.ScreenScrapeHtml(url);

    //                        SendMailHelper.SendMail("新员工入职通知", recipientAddress, body, null);

    //                        // SendMailHelper.SendMail("新员工入职通知", "rich.chow@163.com", body, null);

    //                        recipientAddress = "";
    //                        userids = "";
    //                    }
    //                }
    //                if (userid2s.Trim().Length > 1)
    //                {
    //                    int workcompid = 0;
    //                    int workdepid = 0;
    //                    int workgroupid = 0;
    //                    string workuserid = "";
    //                    string recipientAddress2 = "";

    //                    userid2s = userid2s.Substring(0, userid2s.Length - 1);
    //                    string[] dep2s = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetUserWorkDepartmentID(userid2s);
    //                    if (dep2s.Length > 0)
    //                    {
    //                        List<ESP.HumanResource.Entity.EmployeeBaseInfo> userlist3 = EmployeeBaseManager.GetModelList(" and a.userid in (" + userid2s + ")");
    //                        for (int j = 0; j < dep2s.Length; j++)
    //                        {
    //                            foreach (ESP.HumanResource.Entity.EmployeeBaseInfo ei in userlist3)
    //                            {
    //                                if (ei.WorkCountry == dep2s[j])
    //                                {
    //                                    workuserid += ei.UserID.ToString() + ",";
    //                                    workcompid = int.Parse(ei.WorkCity);
    //                                    workdepid = int.Parse(ei.WorkCountry);
    //                                    workgroupid = int.Parse(ei.WorkAddress);
    //                                }
    //                            }
    //                            if (workuserid.Length > 1)
    //                            {
    //                                workuserid = workuserid.Substring(0, workuserid.Length - 1);
    //                                List<ESP.HumanResource.Entity.UsersInfo> list2 = UsersManager.GetUserList(workcompid, ESP.HumanResource.Common.Status.WaitEntrySendMail);
    //                                foreach (ESP.HumanResource.Entity.UsersInfo info in list2)
    //                                {
    //                                    if (!string.IsNullOrEmpty(info.Email))
    //                                    {
    //                                        recipientAddress2 += info.Email.Trim() + " ,";
    //                                    }
    //                                }
    //                                //有异地工作行为，回给总部带入职抄送人发信
    //                                List<ESP.HumanResource.Entity.UsersInfo> list3 = UsersManager.GetUserList();
    //                                foreach (ESP.HumanResource.Entity.UsersInfo info in list3)
    //                                {
    //                                    if (!string.IsNullOrEmpty(info.Email))
    //                                    {
    //                                        recipientAddress2 += info.Email.Trim() + " ,";
    //                                    }
    //                                }
    //                                recipientAddress2 += GetInformationMail(workgroupid);
    //                                //给原单位的团队HRAdmin发信
    //                                recipientAddress2 += GetDepartmentAdminMail(depID);
    //                                //给工作地的团队HRAdmin发信
    //                                recipientAddress2 += GetDepartmentAdminMail(workdepid);

    //                                if (recipientAddress2.Trim().Length > 1)
    //                                {
    //                                    recipientAddress2 = recipientAddress2.Substring(0, recipientAddress2.Length - 1);
    //                                }
    //                                string url = "http://" + Request.Url.Authority + "/HR/Print/NewEmployeeMail.aspx?userid=" + workuserid;

    //                                string body = ESP.HumanResource.Common.SendMailHelper.ScreenScrapeHtml(url);

    //                                SendMailHelper.SendMail("新员工入职通知", recipientAddress2, body, null);

    //                                //SendMailHelper.SendMail("新员工入职通知", "rich.chow@163.com", body, null);

    //                                recipientAddress2 = "";
    //                                workuserid = "";
    //                            }
    //                        }
    //                    }
    //                    userid2s = "";
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
    #endregion
}