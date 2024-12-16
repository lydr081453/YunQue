using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.HumanResource.BusinessLogic;
using ESP.HumanResource.Entity;

namespace SEPAdmin.Management.UserManagement
{
    public partial class EmpPositionDelete : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string userid = Request["userId"];
                string deptid = Request["deptId"];
                string positionid = Request["positionId"];

                ESP.HumanResource.Entity.UsersInfo model = ESP.HumanResource.BusinessLogic.UsersManager.GetModel(int.Parse(userid));
                ESP.HumanResource.Entity.EmployeeBaseInfo emp =ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModel(model.UserID);
                ESP.HumanResource.Entity.EmployeesInPositionsInfo positionModel = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetModel(model.UserID,int.Parse(positionid),int.Parse(deptid));
                ESP.Framework.Entity.DepartmentInfo dept = ESP.Framework.BusinessLogic.DepartmentManager.Get(positionModel.DepartmentID);
                ESP.Framework.Entity.DepartmentPositionInfo position = ESP.Framework.BusinessLogic.DepartmentPositionManager.Get(positionModel.DepartmentPositionID);

                this.lblDept.Text = dept.DepartmentName;
                this.lblUserName.Text = model.FirstNameCN + model.LastNameCN;
                this.lblUserCode.Text = emp.Code;
                this.lblPosition.Text = position.DepartmentPositionName;

            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string userid = Request["userId"];
            string deptid = Request["deptId"];
            string positionid = Request["positionId"];

            ESP.HumanResource.Entity.UsersInfo userModel = ESP.HumanResource.BusinessLogic.UsersManager.GetModel(int.Parse(userid));

            ESP.HumanResource.Entity.LogInfo logModel = new ESP.HumanResource.Entity.LogInfo();
            logModel.LogMedifiedTeme = DateTime.Now;
            logModel.Des = "为" + ESP.Framework.BusinessLogic.EmployeeManager.Get(int.Parse(userid)).FullNameCN + "删除了部门职务";
            logModel.LogUserId =userModel.UserID;
            logModel.LogUserName = userModel.Username;
            logModel.SysId=CurrentUserID;
            logModel.SysUserName=CurrentUserName;

            EmployeesInPositionsManager.Delete(int.Parse(userid), int.Parse(positionid), int.Parse(deptid), logModel);

            //更新PositionLog信息
            PositionLogInfo pLInfo = PositionLogManager.GetModel(userModel.UserID, int.Parse(positionid));
            if (pLInfo != null)
            {
                pLInfo.EndDate = DateTime.Now;
                PositionLogManager.Update(pLInfo);
            }

            string str = string.Format("var win = art.dialog.open.origin;win.location='EmpMgt.aspx?userid=" + userid + "';");
            ClientScript.RegisterStartupScript(typeof(string), "", str, true);
        }
    }
}
