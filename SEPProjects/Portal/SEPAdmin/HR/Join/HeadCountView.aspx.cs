using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.HumanResource.Entity;
using ESP.HumanResource.BusinessLogic;
using System.Net.Mail;
namespace SEPAdmin.HR.Join
{
    public partial class HeadCountView : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }

        private void BindData()
        {
            int haid = int.Parse(Request["haid"]);
            HeadAccountInfo model = new HeadAccountManager().GetModel(haid);
            var operation = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(model.GroupId);
            lblTeamLeader.Text = operation.HeadCountAuditor;
            lblVPAuditor.Text = operation.HeadCountDirector;
            var deptModel = ESP.Finance.BusinessLogic.DepartmentViewManager.GetModel(model.GroupId);

            PositionLevelsInfo pb = ESP.HumanResource.BusinessLogic.PositionLevelsManager.GetModel(model.LevelId);

            this.lblDept.Text = deptModel.level1 + "-" + deptModel.level2 + "-" + deptModel.level3;
            lblCreator.Text = model.Creator;
            lblAppDate.Text = model.CreateDate.ToString();
            this.lblRemark.Text = model.Remark;
            this.lblPosition.Text = model.Position;
            this.lblUserPosition.Text = model.Position;

            this.lblLevel.Text = model.LevelName;
            this.chkAAD.Checked = model.IsAAD;
            this.chkAAD.Enabled = false;

            this.lblCustomer.Text = model.CustomerName;
            this.lblReplaceReason.Text = model.ReplaceReason;
            if (model.ReplaceUserId != 0)
            {
                ESP.Framework.Entity.UserInfo replaceModel = ESP.Framework.BusinessLogic.UserManager.Get(model.ReplaceUserId);
                this.lblReplaceUser.Text = replaceModel.FullNameCN + "  " + model.ReplaceUserPosition;
            }
            this.lblRequestment.Text = model.Requestment;
            this.lblResponse.Text = model.Response;
            if (model.DimissionDate != null)
            {
                this.lblDimissionDate.Text = model.DimissionDate <= new DateTime(1900,1,1) ? "" : model.DimissionDate.Value.ToString("yyyy-MM-dd");
            }
            if (model.NewBiz == "立项")
            {
                chkCreate.Checked = true;
                chkUnCreate.Checked = false;
            }
            else if (model.NewBiz == "未立项")
            {
                chkCreate.Checked = false;
                chkUnCreate.Checked = true;
            }
            else
            {
                chkCreate.Checked = false;
                chkUnCreate.Checked = false;
            }

            if (!string.IsNullOrEmpty(model.CostUrl) && (CurrentUserID == operation.HRId || CurrentUserID == operation.DirectorId || CurrentUserID == operation.ManagerId || CurrentUserID == operation.CEOId || CurrentUserID == int.Parse(System.Configuration.ConfigurationManager.AppSettings["AdvanceID"])))
            {
                hypAttach.ToolTip = "下载附件：" + model.CostUrl;
                this.hypAttach.NavigateUrl = "/HR/Employees/FileDownLoad.aspx?ContractID=" + model.Id.ToString();
                hypAttach.Visible = true;
            }

            var loglist = new ESP.HumanResource.BusinessLogic.HeadAccountLogManager().GetList(" and HeadAccountId=" + model.Id);

            foreach (var log in loglist)
            {
                string auditstatus = "审批通过";
                if (log.Status == 2)
                    auditstatus = "审批驳回";
                else if (log.Status == 3)
                {
                    auditstatus = "审批留言";
                }
                string logstr = string.Format("[{0}] {1}{2}：{3}（{4}）", ESP.HumanResource.Common.Status.HeadAccountStatus_Names[log.AuditType], log.Auditor, auditstatus, log.Remark, log.AuditDate.ToString());
                lblLog.Text += logstr + "</br>";
            }

            var hrDesc = loglist.Where(x => x.AuditType == (int)ESP.HumanResource.Common.Status.HeadAccountStatus.InterviewHR).FirstOrDefault();
            var groupDesc = loglist.Where(x => x.AuditType == (int)ESP.HumanResource.Common.Status.HeadAccountStatus.InterviewGroup).FirstOrDefault();
            this.lblGroupDesc.Text = groupDesc == null ? "" : groupDesc.Remark;
            this.lblHRDesc.Text = hrDesc == null ? "" : hrDesc.Remark;

            this.lblSalary.Text = pb.SalaryLow.ToString("#,##0.00") + " - " + pb.SalaryHigh.ToString("#,##0.00");

            if (model.OfferLetterUserId != 0)
                initForm(model.OfferLetterUserId);
        }

        protected void initForm(int sysid)
        {
            try
            {
                ESP.HumanResource.Entity.EmployeeBaseInfo model = EmployeeBaseManager.GetModel(sysid);
                ESP.HumanResource.Entity.UsersInfo user = UsersManager.GetModel(sysid);
                ESP.Administrative.Entity.OperationAuditManageInfo audit = (new ESP.Administrative.BusinessLogic.OperationAuditManageManager()).GetOperationAuditModelByUserID(user.UserID);
                ESP.HumanResource.Entity.EmployeesInPositionsInfo position = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetModel(user.UserID);
                var operation = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(position.DepartmentID);
                lblLastNameCn.Text = user.LastNameCN;
                lblFirstNameCn.Text = user.FirstNameCN;
                lblPrivateEmail.Text = model.PrivateEmail;
                lblJoinDate.Text = model.EmployeeJobInfo.joinDate.ToString("yyyy-MM-dd");

                lblMobile.Text = model.MobilePhone;
                lblUserRemark.Text = model.Memo;
                lblIDCard.Text = model.IDNumber;
                lblNowBasePay.Text = model.Pay.ToString();
                lblNowMeritPay.Text = model.Performance.ToString();
                lblNowAttendance.Text = model.Attendance.ToString();

                if ( model.Pay > 0 && (CurrentUserID == operation.FinanceUserId || CurrentUserID == operation.RiskControlAccounter))
                {
                    //财务可清空工资
                    btnClearPrice.Visible = true;
                }

                if (operation != null)
                {
                    lblTeamLeader.Text = operation.HeadCountAuditor;
                    this.lblVPAuditor.Text = operation.HeadCountDirector;
                }

                if (audit != null)
                {
                    this.lblKaoqin.Text = audit.TeamLeaderName;
                }


                this.lblUserType.Text = ESP.Framework.BusinessLogic.EmployeeManager.GetTypeName(model.TypeID);

                

                List<ESP.HumanResource.Entity.EmployeesInPositionsInfo> eiplist = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetModelList(" a.userid=" + model.UserID);
                ESP.HumanResource.Entity.EmployeesInPositionsInfo deps = eiplist[0];


                if (!string.IsNullOrEmpty(model.WorkCity))
                {
                   var  dept = ESP.Framework.BusinessLogic.DepartmentManager.Get(int.Parse(model.WorkCity));
                    lblLocation.Text =dept==null?"": dept.DepartmentName;
                }

                //lblTmplate.Text = model.OfferLetterTemplate.ToString();  // Offer Letter模板
                chkExamen.Checked = model.IsExamen;   // 是否是应届毕业生。
                lblSeniority.Text = model.Seniority.ToString();
            }
            catch (Exception ex)
            {
                ESP.Logging.Logger.Add(ex.ToString());
            }
        }

        protected void btnClearPrice_Click(object sender, EventArgs e)
        {
            int haid = int.Parse(Request["haid"]);
            HeadAccountInfo hcInfo = new HeadAccountManager().GetModel(haid);
            ESP.HumanResource.Entity.EmployeeBaseInfo model = EmployeeBaseManager.GetModel(hcInfo.OfferLetterUserId);
            model.Pay = 0;
            model.Performance = 0;
            model.Attendance = 0;
            EmployeeBaseManager.Update(model);

            ESP.HumanResource.Entity.HeadAccountLogInfo logModel = new ESP.HumanResource.Entity.HeadAccountLogInfo();
            logModel.AuditDate = DateTime.Now;
            logModel.Auditor = UserInfo.FullNameCN;
            logModel.AuditorId = UserID;
            logModel.AuditType = (int)ESP.HumanResource.Common.Status.HeadAccountStatus.FinancialAudit;
            logModel.HeadAccountId = haid;
            logModel.Status = 0;

            new ESP.HumanResource.BusinessLogic.HeadAccountLogManager().Add(logModel);

            
            Response.Redirect("HeadCountView.aspx?haid="+haid);
        }


    }
}
