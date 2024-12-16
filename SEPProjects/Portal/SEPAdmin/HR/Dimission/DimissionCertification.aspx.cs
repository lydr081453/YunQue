using System;
using System.Collections.Generic;
using ESP.HumanResource.BusinessLogic;

namespace SEPAdmin.HR.Dimission
{
    public partial class DimissionCertification : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["dimissionid"]))
            {
                int dimissionid = 0;
                if (!int.TryParse(Request["dimissionid"], out dimissionid))
                    dimissionid = 0;
                InitPage(dimissionid);
            }
        }

        protected void InitPage(int dimissionid)
        {
            ESP.HumanResource.Entity.DimissionFormInfo dimissionFormInfo = ESP.HumanResource.BusinessLogic.DimissionFormManager.GetModel(dimissionid);
            List<ESP.HumanResource.Entity.EmployeesInPositionsInfo> departments = EmployeesInPositionsManager.GetModelList(" a.UserID=" + dimissionFormInfo.UserId);  // 用户部门信息
            ESP.HumanResource.Entity.EmployeeBaseInfo employeeModel = EmployeeBaseManager.GetModel(dimissionFormInfo.UserId);  // 用户基本信息

            labUserName.Text = labUserName2.Text = dimissionFormInfo.UserName;
            if (employeeModel.IDNumber.IndexOf("$") >= 0)
            {
                labIdCard.Text = labIdCard2.Text = employeeModel.IDNumber.Substring(1);
            }
            else
            {
                labIdCard.Text = labIdCard2.Text = employeeModel.IDNumber;
            }
            labJoinDay.Text = labJoinDay2.Text = ESP.HumanResource.BusinessLogic.EmployeeJobManager.getModelBySysId(dimissionFormInfo.UserId).joinDate.ToString("yyyy年MM月dd日");
            labLastDay.Text = labLastDay2.Text = dimissionFormInfo.LastDay.Value.ToString("yyyy年MM月dd日");
            
            labCurDay.Text = labCurDay2.Text = DateTime.Now.ToString("yyyy-MM-dd");
            ESP.HumanResource.Entity.DimissionHRDetailsInfo hrDetailInfo = ESP.HumanResource.BusinessLogic.DimissionHRDetailsManager.GetHRDetailInfo(dimissionid);
            if (hrDetailInfo.IsShowPosition)
            {
                if (departments != null && departments.Count > 0)
                {
                    labPosition.Text = labPosition2.Text =  departments[0].DepartmentPositionName ;
                }
            }
            else
            {
                labPositionTitle.Visible = false;
                labPositionTitle2.Visible = false;
                labPosition.Text = labPosition2.Text = "";
            }
            ESP.Finance.Entity.BranchInfo branchInfo = ESP.Finance.BusinessLogic.BranchManager.GetModel(hrDetailInfo.BranchId);
            if (branchInfo != null)
                labCompanyName1.Text = labCompanyName2.Text = branchInfo.BranchName;
        }
    }
}
