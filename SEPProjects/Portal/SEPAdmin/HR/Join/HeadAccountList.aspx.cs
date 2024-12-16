using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.HumanResource.Entity;
using ESP.HumanResource.BusinessLogic;
using System.Net.Mail;
using ESP.HumanResource.Common;

namespace SEPAdmin.HR.Join
{
    public partial class HeadAccountList : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            Response.Redirect("HeadAccountEdit.aspx");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        { BindData(); }

        private void BindData()
        {
            var deptlist = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelList(" DirectorId=" + UserID.ToString() + " or ManagerId=" + UserID.ToString() + " or CEOId=" + UserID.ToString() + " or financeUserId=" + UserID.ToString() + " or riskcontrolaccounter=" + UserID.ToString());
            //+ " or hrid=" + UserID.ToString() + " or hrAttendanceId=" + UserID.ToString() + " or receptionId=" + UserID.ToString()

            string depts = string.Empty;
            foreach (var dep in deptlist)
            {
                depts += dep.DepId.ToString() + ",";
            }

            depts = depts.TrimEnd(',');

            string strwhere = " and  createdate>='" + DateTime.Now.AddYears(-1).ToString("yyyy-MM-dd") + "' and  (CreatorId=" + UserID.ToString();

            if (!string.IsNullOrEmpty(depts))
            {
                strwhere += " or groupid in(" + depts + "))";
            }
            else
            {
                strwhere += ")";
            }

            int hrAdmin = int.Parse(System.Configuration.ConfigurationManager.AppSettings["HRAdminID"]);
            string hrIds = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetHRId();

            if (CurrentUserID == hrAdmin || hrIds.IndexOf(CurrentUserID.ToString())>=0)
            {
                strwhere = "  and  createdate>='" + DateTime.Now.AddYears(-1).ToString("yyyy-MM-dd") + "' ";
            }

            if (!string.IsNullOrEmpty(txtCode.Text))
            {
                strwhere += " and (id like '%" + txtCode.Text + "%' or creator like '%" + txtCode.Text + "%' or remark like '%" + txtCode.Text + "%' or position like '%" + txtCode.Text + "%' or customername like '%" + txtCode.Text + "%' or offerletteruserid in (select userid from sep_Users where LastNameCN+FirstNameCN like '%" + txtCode.Text + "%'))";

            }
            if (ddlStatus.SelectedValue == "-1")
            { }
            else if (ddlStatus.SelectedValue == "1")
            {
                strwhere += " and offerletteruserid in(select userid from sep_employees where status in(1,3))";
            }
            else if (ddlStatus.SelectedValue == "11")
            {
                strwhere += " and offerletteruserid in(select userid from sep_employees where status =11)";
            }
            else if (ddlStatus.SelectedValue == "99")
            {
                strwhere += "and (status =3 or offerletteruserid in(select userid from sep_employees where status not in(1,3,5)))";
            }
            else
            {
                strwhere += " and status in(" + ddlStatus.SelectedValue + ")";
            }
            //"驳回", "待VP审批", "待财务审批", "审批通过", "面谈完毕", "面谈审核通过", "提交Offer", "HR面谈意见", "业务团队面谈意见", "待团队预审" 

            var headaccountList = new ESP.HumanResource.BusinessLogic.HeadAccountManager().GetList(strwhere);

            this.gvHeadAccount.DataSource = headaccountList;
            this.gvHeadAccount.DataBind();
        }


        protected void gvHeadAccount_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int hdid = int.Parse(e.CommandArgument.ToString());
            ESP.HumanResource.Entity.HeadAccountInfo headModel = (new HeadAccountManager()).GetModel(hdid);
            if (e.CommandName == "Mail")
            {
                if (headModel.OfferLetterUserId != 0)
                {
                    ESP.Framework.Entity.OperationAuditManageInfo operation = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(headModel.GroupId);

                    ESP.HumanResource.Entity.EmployeeBaseInfo empHR = EmployeeBaseManager.GetModel(operation.HRId); 

                    ESP.HumanResource.Entity.EmployeeBaseInfo model = EmployeeBaseManager.GetModel(headModel.OfferLetterUserId);
                    decimal pay = model.Pay;
                    decimal performance = model.Performance;
                    decimal attendance = model.Attendance;

                    if (!string.IsNullOrEmpty(model.PrivateEmail))
                    {
                        string url = "";
                        url = "http://" + Request.Url.Authority + "/HR/Join/OfferLetter.aspx?userid=" + model.UserID + "&nowBasePay=" + pay.ToString("#,##0.00") + "&nowMeritPay=" + performance.ToString("#,##0.00") + "&nowAttendance=" + attendance.ToString("#,##0.00") + "&showSalary=1";
                        try
                        {
                            string body = ESP.HumanResource.Common.SendMailHelper.ScreenScrapeHtml(url);
                            ESP.Mail.MailManager.Send("聘用任职信", body, true, new MailAddress[] { new MailAddress(model.PrivateEmail), new MailAddress(empHR.InternalEmail) });
                            ShowCompleteMessage("聘用任职信发送成功！", "HeadAccountList.aspx");
                        }
                        catch
                        { }

                    }
                    else
                    {
                       // ESP.Logging.Logger.Add("没有填写" + model.FullNameCN + "个人邮箱。");
                        ShowCompleteMessage("聘用任职信发送失败！", "HeadAccountList.aspx");
                    }
                }
            }
            //重新约见
            else if (e.CommandName == "View")
            {
                if (headModel.OfferLetterUserId != 0)
                {
                    ESP.HumanResource.BusinessLogic.EmployeeBaseManager.Delete(headModel.OfferLetterUserId);
                    ESP.Framework.BusinessLogic.UserManager.Delete(headModel.OfferLetterUserId);

                    headModel.Status = (int)Status.HeadAccountStatus.FinanceApproved;
                    headModel.OfferLetterUserId = 0;

                    if (headModel.TalentId != 0)
                    {
                        ESP.HumanResource.Entity.TalentInfo talentModel = (new ESP.HumanResource.BusinessLogic.TalentManager()).GetModel(headModel.TalentId);
                        talentModel.Status = 0;
                        (new ESP.HumanResource.BusinessLogic.TalentManager()).Update(talentModel);
                    }
                    headModel.TalentId = 0;
                    (new ESP.HumanResource.BusinessLogic.HeadAccountManager()).Update(headModel);



                    ShowCompleteMessage("已经撤销上一次约见记录，请重新进行约见！", "HeadAccountList.aspx");
                }
            }

            else if (e.CommandName == "Delete")
            {
                (new HeadAccountManager()).Delete(headModel.Id);
                if (headModel.TalentId != 0)
                {
                    ESP.HumanResource.Entity.TalentInfo talentModel = (new ESP.HumanResource.BusinessLogic.TalentManager()).GetModel(headModel.TalentId);
                    talentModel.Status = 0;
                    (new ESP.HumanResource.BusinessLogic.TalentManager()).Update(talentModel);
                }
                ShowCompleteMessage("已经删除headcount！", "HeadAccountList.aspx");
            }
        }

        protected void gvHeadAccount_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HeadAccountInfo model = (HeadAccountInfo)e.Row.DataItem;
                // var operation = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(model.GroupId);
                //ESP.Framework.Entity.UserInfo replaceModel = null;
                //ESP.Framework.Entity.UserInfo userModel = null;
                ESP.HumanResource.Entity.EmployeeBaseInfo offerModel = null;
                //if (model.ReplaceUserId != 0)
                //    replaceModel = ESP.Framework.BusinessLogic.UserManager.Get(model.ReplaceUserId);
                if (model.OfferLetterUserId != 0)
                {
                    offerModel = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModel(model.OfferLetterUserId);
                    // userModel = ESP.Framework.BusinessLogic.UserManager.Get(model.OfferLetterUserId);
                }

                if (model.Status != (int)ESP.HumanResource.Common.Status.HeadAccountStatus.Reject)
                {
                    e.Row.Cells[10].Text = "";
                }

                ESP.Framework.Entity.OperationAuditManageInfo operationModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(model.GroupId);


                // PositionLevelsInfo pb = ESP.HumanResource.BusinessLogic.PositionLevelsManager.GetModel(model.LevelId);
                ESP.Finance.Entity.DepartmentViewInfo deptModel = ESP.Finance.BusinessLogic.DepartmentViewManager.GetModel(model.GroupId);
                //Label lblSalary = (Label)e.Row.FindControl("lblSalary");
                Label lblInterview = (Label)e.Row.FindControl("lblInterview");
                Label lblTransfer = (Label)e.Row.FindControl("lblTransfer");
                Label lblGroup = (Label)e.Row.FindControl("lblGroup");
                Label lblPrint = (Label)e.Row.FindControl("lblPrint");
                Label lblOffer = (Label)e.Row.FindControl("lblOffer");
                Label lblStatus = (Label)e.Row.FindControl("lblStatus");
                //Label lblReplace = (Label)e.Row.FindControl("lblReplace");

                ImageButton btnInterview = (ImageButton)e.Row.FindControl("btnInterview");
                ImageButton btnMail = (ImageButton)e.Row.FindControl("btnMail");
                ImageButton btnDelete = (ImageButton)e.Row.FindControl("btnDelete");

                Label lblId = (Label)e.Row.FindControl("lblId");
                Label lblAuditor = (Label)e.Row.FindControl("lblAuditor");
                int auditorId = 0;
                if (model.Status == (int)ESP.HumanResource.Common.Status.HeadAccountStatus.Commit)//待团队总监审核
                {
                    auditorId = operationModel.HeadCountDirectorId;
                }
                else if (model.Status == (int)ESP.HumanResource.Common.Status.HeadAccountStatus.WaitPreVPAudit)//待团队预审
                {
                    auditorId = operationModel.HeadCountAuditorId;
                }
                else if (model.Status == (int)ESP.HumanResource.Common.Status.HeadAccountStatus.WaitCFOAudit || model.Status == (int)ESP.HumanResource.Common.Status.HeadAccountStatus.VPApproved)//待风控审批
                {
                    auditorId = operationModel.HCFinalAuditor;
                }
                else if (model.Status == (int)ESP.HumanResource.Common.Status.HeadAccountStatus.InterView)
                {
                    auditorId = model.InterviewVPId;
                }
                if (auditorId != 0)
                {
                    ESP.HumanResource.Entity.EmployeeBaseInfo emp = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModel(auditorId);
                    lblAuditor.Text = emp.FullNameCN;
                }

                string strId = model.Id.ToString();

                while (strId.Length < 5)
                {
                    strId = "0" + strId;
                }
                lblId.Text = "HC" + strId;

                lblStatus.Text = ESP.HumanResource.Common.Status.HeadAccountStatus_Names[model.Status];
                if (offerModel != null)
                {
                    if (offerModel.Status == ESP.HumanResource.Common.Status.IsSendOfferLetter)
                    {
                        btnMail.Visible = true;
                    }

                }
                if (model.OfferLetterUserId == 0 || model.Status == (int)ESP.HumanResource.Common.Status.HeadAccountStatus.Reject)
                {
                    btnDelete.Visible = true;
                }
                else
                {
                    btnDelete.Visible = false;
                }

                lblGroup.Text = deptModel.level1 + "-" + deptModel.level2 + "-" + deptModel.level3;
                //lblSalary.Text = pb.SalaryLow.ToString("#,##0.00") + " - " + pb.SalaryHigh.ToString("#,##0.00");


                if ((model.CreatorId == CurrentUserID || operationModel.HRId == CurrentUserID || operationModel.Hrattendanceid == CurrentUserID || operationModel.ReceptionId == CurrentUserID) && model.Status == (int)ESP.HumanResource.Common.Status.HeadAccountStatus.FinanceApproved)
                {
                    lblInterview.Text = "<a href='HeadAccountInterview.aspx?haid=" + model.Id.ToString() + "'>约见</a>";
                }
                if ((model.CreatorId == CurrentUserID || operationModel.HRId == UserID) && model.Status == (int)ESP.HumanResource.Common.Status.HeadAccountStatus.FinanceApproved)
                {
                    lblTransfer.Text = "<a href='/HR/Transfer/TransferEdit.aspx?haid=" + model.Id.ToString() + "'>转组</a>";
                }


                if (offerModel != null)
                    lblOffer.Text = offerModel.FullNameCN;


                int hrAdmin = int.Parse(System.Configuration.ConfigurationManager.AppSettings["HRAdminID"]);
                string hrIds = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetHRId();

                if (model.CreatorId == CurrentUserID || CurrentUserID == hrAdmin || hrIds.IndexOf(CurrentUserID.ToString())>=0)
                {
                    lblPrint.Text = "<a href=\"/HR/Print/HeadcountPrint.aspx?haid=" + model.Id.ToString() + "\" target=\"_blank\">打印</a>";
                }
                //面谈后驳回可以重新面谈约见
                if (offerModel != null)
                {
                    if (offerModel.Status != (int)ESP.HumanResource.Common.Status.Entry && offerModel.Status != (int)ESP.HumanResource.Common.Status.Dimission)
                        btnInterview.Visible = true;
                }
                if (model.Status == (int)ESP.HumanResource.Common.Status.HeadAccountStatus.FinanceApproved)
                {
                    btnInterview.Visible = true;
                }

            }
        }

        protected void gvHeadAccount_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }
    }
}
