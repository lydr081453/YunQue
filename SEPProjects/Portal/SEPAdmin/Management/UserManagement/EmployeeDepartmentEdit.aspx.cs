using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.HumanResource.BusinessLogic;
using ESP.HumanResource.Entity;

namespace SEPAdmin.Management.UserManagement
{
    public partial class EmployeeDepartmentEdit : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            #region AjaxProRegister
            AjaxPro.Utility.RegisterTypeForAjax(typeof(EmployeeBaseManager));
            #endregion
            //if (!IsPostBack)
            //{
            //    drpJobBind();
            //}
        }

        //protected void drpJobBind()
        //{
        //    drpJob_JoinJob.Items.Insert(0, new ListItem("请选择...", "-1"));
        //}

        protected void btnSave_Click(object sender, EventArgs e)
        {
            ESP.HumanResource.Entity.EmployeesInPositionsInfo model = new ESP.HumanResource.Entity.EmployeesInPositionsInfo();
            if (!string.IsNullOrEmpty(txtJob_CompanyName.Text.Trim()) && !string.IsNullOrEmpty(hidCompanyId.Value))
            {
                model.DepartmentID = int.Parse(hidCompanyId.Value);

            }
            if (!string.IsNullOrEmpty(txtJob_DepartmentName.Text.Trim()) && !string.IsNullOrEmpty(hidDepartmentID.Value))
            {
                model.DepartmentID = int.Parse(hidDepartmentID.Value);

            }
            if (!string.IsNullOrEmpty(txtJob_GroupName.Text.Trim()) && !string.IsNullOrEmpty(hidGroupId.Value))
            {
                model.DepartmentID = int.Parse(hidGroupId.Value);
            }
            model.DepartmentPositionID = int.Parse(txtJob_JoinJob.Value);
            model.UserID = int.Parse(Request["userid"].ToString());
            model.IsManager = false;
            model.IsActing = false;

            ESP.HumanResource.Entity.LogInfo logModel = new ESP.HumanResource.Entity.LogInfo();
            logModel.LogMedifiedTeme = DateTime.Now;
            logModel.Des = "为" + ESP.Framework.BusinessLogic.EmployeeManager.Get(int.Parse(Request["userid"].ToString())).FullNameCN + "新增部门职务";//+txtJob_CompanyName+txtJob_DepartmentName+txtJob_GroupName+",职务为" + drpJob_JoinJob.SelectedItem.Text;
            logModel.LogUserId = UserInfo.UserID;
            logModel.LogUserName = UserInfo.Username;

            PositionLogInfo posLogInfo = new PositionLogInfo();

            posLogInfo.UserId = model.UserID;
            posLogInfo.UserName = model.UserName;
            posLogInfo.UserCode = model.UserCode;
            posLogInfo.DepartmentId = model.DepartmentID;
            posLogInfo.DepartmentName = model.DepartmentName;

            ESP.Framework.Entity.DepartmentPositionInfo dPInfo = ESP.Framework.BusinessLogic.DepartmentPositionManager.Get(int.Parse(txtJob_JoinJob.Value));
            if (dPInfo != null)
            {
                posLogInfo.PositionId = dPInfo.DepartmentPositionID;
                posLogInfo.PositionName = dPInfo.DepartmentPositionName;
                PositionBaseInfo pBInfo = PositionBaseManager.GetModel(dPInfo.PositionBaseId);
                if (pBInfo != null)
                {
                    posLogInfo.PositionBaseId = pBInfo.Id;
                    posLogInfo.PositionBaseName = pBInfo.PositionName;

                    posLogInfo.LevelId = pBInfo.LeveId;
                    posLogInfo.LevelName = pBInfo.LevelName;

                }

            }

            posLogInfo.BeginDate = posLogInfo.CreateDate = DateTime.Now;

          
            if (EmployeesInPositionsManager.Add(model, logModel,posLogInfo))
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location='EmployeesEdit.aspx?userid=" + Request["userid"] + "';alert('添加成功！');", true);
            }
            else {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location='EmployeesEdit.aspx?userid=" + Request["userid"] + "';alert('添加失败！');", true);
            }
            
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("EmployeesEdit.aspx?userid=" + Request["userid"]);
        }
    }
    
}
