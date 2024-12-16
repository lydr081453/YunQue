using System;
using System.Collections.Generic;
using ESP.HumanResource.BusinessLogic;
using System.Data;

namespace SEPAdmin.HR.Print
{
    public partial class NewDimissionMail : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["dimissionId"]))
            {
                int dimissionId = 0;
                if (!int.TryParse(Request["dimissionId"], out dimissionId))
                    dimissionId = 0;
                string type = "";
                
                if (!string.IsNullOrEmpty(Request["type"]))
                {
                    if (Request["type"] == "2")  // 邮件提醒
                    {
                        type = "2";
                    }
                    else
                    {
                        type = "1";
                    }
                }

                InitPage(dimissionId, type);
            }
        }

        /// <summary>
        /// 初始化页面信息
        /// </summary>
        private void InitPage(int dimissionId, string type)
        {
            ESP.HumanResource.Entity.DimissionFormInfo dimissionFormInfo = ESP.HumanResource.BusinessLogic.DimissionFormManager.GetModel(dimissionId);  // 获得离职单信息
            ESP.HumanResource.Entity.DimissionITDetailsInfo itDetailInfo = ESP.HumanResource.BusinessLogic.DimissionITDetailsManager.GetITDetailInfo(dimissionId);
            List<ESP.HumanResource.Entity.EmployeesInPositionsInfo> departments = EmployeesInPositionsManager.GetModelList(" a.UserID=" + dimissionFormInfo.UserId);  // 用户部门信息
            ESP.HumanResource.Entity.EmployeeBaseInfo employeeModel = EmployeeBaseManager.GetModel(dimissionFormInfo.UserId);  // 用户基本信息

            labCode.Text = employeeModel.Code;
            txtPhone.Text = employeeModel.Phone1;
            if (departments != null && departments.Count > 0)
            {
                List<ESP.Framework.Entity.DepartmentInfo> deps = new List<ESP.Framework.Entity.DepartmentInfo>();
                ESP.Framework.BusinessLogic.DepartmentManager.GetDepartmentListByID(departments[0].GroupID, deps);
                if (deps.Count == 3)
                {
                    txtcompanyName.Text = deps[0].DepartmentName;
                    hidcompanyId.Value = deps[0].DepartmentID.ToString();
                    txtdepartmentName.Text = deps[1].DepartmentName;
                    hiddepartmentId.Value = deps[1].DepartmentID.ToString();
                    txtgroupName.Text = deps[2].DepartmentName+"("+employeeModel.WorkCity+")";
                    hidgroupId.Value = deps[2].DepartmentID.ToString();
                    //depId = deps[2].DepartmentID;
                }
                else if (deps.Count == 2)
                {
                    txtdepartmentName.Text = deps[1].DepartmentName;
                    hiddepartmentId.Value = deps[1].DepartmentID.ToString();
                }
                txtPosition.Text = departments[0].DepartmentPositionName;
            }

            //入职日期
            ESP.HumanResource.Entity.EmployeeJobInfo employeeJobModel = EmployeeJobManager.getModelBySysId(dimissionFormInfo.UserId);
            txtjoinJobDate.Text = employeeModel == null ? "1900-01-01" : employeeJobModel.joinDate.ToString("yyyy-MM-dd");

            ESP.Framework.Entity.UserInfo userinfo = ESP.Framework.BusinessLogic.UserManager.Get(dimissionFormInfo.UserId);
            this.txtuserName.Text = userinfo.FullNameCN;
            this.txtComEmail.Text = userinfo.Email;
            if (itDetailInfo != null)
            {
                if (itDetailInfo.EmailIsDelete)
                {
                    this.txtComEmail.Text = userinfo.Email+"（删除）";
                }
                else
                {
                    this.txtComEmail.Text = userinfo.Email + "（保留："+ itDetailInfo.EmailSaveLastDay.Value.ToString("yyyy-MM-dd")+"）";
                }
            }

            DataSet trafficFeeDs = ESP.HumanResource.BusinessLogic.DimissionDetailsManager.GetDimissionTrafficFee(UserID);
            if (trafficFeeDs != null && trafficFeeDs.Tables[0].Rows.Count > 0)
            {
                tbCash.Visible = true;
            }
            gvCashList.DataSource = trafficFeeDs;
            gvCashList.DataBind();

            #region 根据离职单状态判断页面显示内容
            if (dimissionFormInfo != null)
            {
                hidDimissionFormID.Value = dimissionFormInfo.DimissionId.ToString();
                //DimissionFormId = dimissionFormInfo.DimissionId;
                txtdimissionCause.Text = dimissionFormInfo.Reason;
                txtMobilePhone.Text = dimissionFormInfo.MobilePhone;
                txtEmail.Text = dimissionFormInfo.PrivateMail;
                txtdimissionDate2.Text = dimissionFormInfo.HopeLastDay == null ? "" : dimissionFormInfo.HopeLastDay.Value.ToString("yyyy-MM-dd");
                txtdimissionDate.Text = dimissionFormInfo.LastDay == null ? "" : dimissionFormInfo.LastDay.Value.ToString("yyyy-MM-dd");
            }
            #endregion

            if (!string.IsNullOrEmpty(type))
            {
                if (type == "2")  // 邮件提醒
                {
                    labMailTip.Visible = true;
                    labMailTip.Text = "总经理已确认并批准" + dimissionFormInfo.UserName + "的离职，请协助办理相关手续。";
                }
                else if (type == "1")
                {
                    labNextAudit.Visible = true;
                    labNextAudit.Text = dimissionFormInfo.UserName + "提交了离职申请，等待您的审批。";
                }
                else
                {
                    labNextAudit.Visible = true;
                    labNextAudit.Text = dimissionFormInfo.UserName + "提交了离职申请。";
                }
            }
            else
            {

                labNextAudit.Visible = true;
                labNextAudit.Text = dimissionFormInfo.UserName + "提交了离职申请。";
            }
            if (dimissionFormInfo.Status == (int)ESP.HumanResource.Common.DimissionFormStatus.WaitDirector)
                pnlSubmit.Visible = true;
            else
                pnlOtherStatus.Visible = true;
        }
    }
}
