using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.HumanResource.Entity;
using ESP.HumanResource.Common;

namespace SEPAdmin.HR.Join
{
    public partial class HeadAccountCreate : ESP.Web.UI.PageBase
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
            int headcountId = 0;
            
            int deptid = 0;
            int replaceid = 0;

           

            if (!string.IsNullOrEmpty(Request["hcid"]))
            {
                headcountId = int.Parse(Request["hcid"]);

                ESP.HumanResource.Entity.HeadAccountInfo headCountModel = (new ESP.HumanResource.BusinessLogic.HeadAccountManager()).GetModel(headcountId);
                ESP.Framework.Entity.DepartmentPositionInfo position = ESP.Framework.BusinessLogic.DepartmentPositionManager.Get(headCountModel.PositionId);
                ESP.HumanResource.Entity.PositionBaseInfo baseModel = ESP.HumanResource.BusinessLogic.PositionBaseManager.GetModel(position.PositionBaseId);
                ESP.HumanResource.Entity.PositionLevelsInfo levelModel = ESP.HumanResource.BusinessLogic.PositionLevelsManager.GetModel(baseModel.LeveId);

                var operation = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(position.DepartmentID);

                deptid = headCountModel.GroupId;
                replaceid = headCountModel.ReplaceUserId;
                txtJob_JoinJob.Value = headCountModel.PositionId.ToString();
                txtPosition.Text = headCountModel.Position;

                this.hidPosition.Value = headCountModel.ReplaceUserPosition;

                this.txtRemark.Text = headCountModel.Remark;

                chkAAD.Checked = headCountModel.IsAAD;
                this.txtCustomer.Text = headCountModel.CustomerName;
                this.txtDimissionDate.Text = headCountModel.DimissionDate.Value.ToString("yyyy-MM-dd");
                this.txtResponse.Text = headCountModel.Response;
                this.txtReplaceReason.Text = headCountModel.ReplaceReason;
                this.txtRequestment.Text = headCountModel.Requestment;
                this.hidPosition.Value = headCountModel.ReplaceUserPosition;

                lblLevel.Text = baseModel.LevelName;
                lblSalary.Text = levelModel.SalaryLow.ToString("#,##0.00") + " - " + levelModel.SalaryHigh.ToString("#,##0.00");
                chkAAD.Checked = headCountModel.IsAAD;

                if (headCountModel.NewBiz == "立项")
                {
                    chkCreate.Checked = true;
                }
                else
                {
                    chkUnCreate.Checked = true;
                }

            }

            else
            {
                deptid = int.Parse(Request["deptid"]);
                if (!string.IsNullOrEmpty(Request["replaceid"]))
                {
                    replaceid = int.Parse(Request["replaceid"]);
                }

                
            }

            hidDeptId.Value = deptid.ToString();

            var deptModel = ESP.Finance.BusinessLogic.DepartmentViewManager.GetModel(deptid);

            this.lblDept.Text = deptModel.level1 + "-" + deptModel.level2 + "-" + deptModel.level3;

            if (replaceid != 0)
            {
                ESP.Framework.Entity.UserInfo replaceModel = ESP.Framework.BusinessLogic.UserManager.Get(replaceid);
                ESP.HumanResource.Entity.EmployeesInPositionsInfo positionModel = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetModel(replaceid);
                this.lblReplaceUser.Text = replaceModel.FullNameCN + "    " + positionModel.DepartmentPositionName;
                this.hidPosition.Value = positionModel.DepartmentPositionName;

            }
            else
            {
                trReplace1.Visible = false;
                trReplace2.Visible = false;
                trReplace3.Visible = false;
            }
        }

        protected void btnCommit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtJob_JoinJob.Value))
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('请选择职务！');", true);
                return;
            }

            ESP.HumanResource.Entity.HeadAccountInfo model = null;
            int replaceid = 0;
            int talentId = 0;

            if (string.IsNullOrEmpty(Request["hcid"]))
            {
                model = new HeadAccountInfo();
                if (!string.IsNullOrEmpty(Request["replaceid"]))
                {
                    replaceid = int.Parse(Request["replaceid"]);
                }
            }
            else
            {
                model = (new ESP.HumanResource.BusinessLogic.HeadAccountManager()).GetModel(int.Parse(Request["hcid"]));
                replaceid = model.ReplaceUserId;
            }
            ESP.Framework.Entity.DepartmentPositionInfo position = ESP.Framework.BusinessLogic.DepartmentPositionManager.Get(int.Parse(this.txtJob_JoinJob.Value));
            ESP.HumanResource.Entity.PositionBaseInfo baseModel = ESP.HumanResource.BusinessLogic.PositionBaseManager.GetModel(position.PositionBaseId);
            var operation = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(position.DepartmentID);

            model.ReplaceUserId = replaceid;
            model.ReplaceUserPosition = this.hidPosition.Value;
            model.BaseId = baseModel.Id;
            model.BaseName = baseModel.PositionName;
            model.LevelName = baseModel.LevelName;
            model.LevelId = baseModel.LeveId;
            model.CreateDate = DateTime.Now;
            model.CreatorId = UserID;
            model.Creator = UserInfo.FullNameCN;
            model.GroupId = position.DepartmentID;
            model.Position = position.DepartmentPositionName;
            model.PositionId = position.DepartmentPositionID;
            model.Remark = this.txtRemark.Text;
            model.RCUserId = operation.HCFinalAuditor;

            if (operation.HeadCountAuditorId == operation.HeadCountDirectorId)
                model.Status = (int)Status.HeadAccountStatus.Commit;
            else
                model.Status = (int)Status.HeadAccountStatus.WaitPreVPAudit;
            model.IsAAD = chkAAD.Checked;

            model.CustomerName = this.txtCustomer.Text;
            model.DimissionDate = string.IsNullOrEmpty(this.txtDimissionDate.Text) ? new DateTime(1754, 1, 1) : DateTime.Parse(this.txtDimissionDate.Text);
            model.Response = this.txtResponse.Text;
            model.ReplaceReason = this.txtReplaceReason.Text;
            model.Requestment = this.txtRequestment.Text;
            model.ReplaceUserPosition = this.hidPosition.Value;
            if (chkCreate.Checked == true)
            {
                model.NewBiz = chkCreate.Text;
            }
            else
            {
                model.NewBiz = chkUnCreate.Text;
            }


            if (!string.IsNullOrEmpty(Request["talentId"]))
            {
                talentId = int.Parse(Request["talentId"]);
            }
            if (talentId != 0)
            {
                model.TalentId = talentId;
                ESP.HumanResource.Entity.TalentInfo talent = (new ESP.HumanResource.BusinessLogic.TalentManager()).GetModel(talentId);
                talent.Status = 1;
                (new ESP.HumanResource.BusinessLogic.TalentManager()).Update(talent);
            }

            int haid =0;
            if (model.Id == 0)
            {
                haid = new ESP.HumanResource.BusinessLogic.HeadAccountManager().Add(model);

            }
            else
            {
                haid = model.Id;
                new ESP.HumanResource.BusinessLogic.HeadAccountManager().Update(model);
            }
            SendMail(model);
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('提交成功，请等待业务团队审批！');window.location.href='HeadAccountList.aspx';", true);

        }

        private void SendMail(ESP.HumanResource.Entity.HeadAccountInfo model)
        {
            string recipientAddress = "";

            ESP.Framework.Entity.OperationAuditManageInfo operation = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(model.GroupId);
            ESP.HumanResource.Entity.EmployeeBaseInfo directorModel = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModel(operation.DirectorId);
            ESP.HumanResource.Entity.EmployeeBaseInfo hrModel = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModel(operation.HRId);
            //ESP.HumanResource.Entity.EmployeeBaseInfo financeModel = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModel(int.Parse(System.Configuration.ConfigurationManager.AppSettings["AdvanceID"]));
            recipientAddress += string.IsNullOrEmpty(directorModel.InternalEmail) ? "" : directorModel.InternalEmail + ",";
            recipientAddress += string.IsNullOrEmpty(hrModel.InternalEmail) ? "" : hrModel.InternalEmail + ",";
            //recipientAddress += string.IsNullOrEmpty(financeModel.InternalEmail) ? "" : financeModel.InternalEmail + ","; ;

            string url = "http://" + Request.Url.Authority + "/HR/Print/HeadCountMail.aspx?haid=" + model.Id.ToString();
            string body = ESP.HumanResource.Common.SendMailHelper.ScreenScrapeHtml(url);
            try
            {
                SendMailHelper.SendMail("HeadCount提交申请", recipientAddress, body, null);
            }
            catch { }
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("HeadAccountList.aspx");
        }

    }
}
