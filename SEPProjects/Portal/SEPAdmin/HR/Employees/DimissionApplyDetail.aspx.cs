using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Employees_DimissionApplyDetail : ESP.Web.UI.PageBase
{
    int dimissionApplyId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        //离职申请ID
        if (!string.IsNullOrEmpty(Request["dimissionApplyId"]))
            dimissionApplyId = int.Parse(Request["dimissionApplyId"]);
        if (!IsPostBack)
        {
            InitPage();
        }
    }

    /// <summary>
    /// 初始化页面信息
    /// </summary>
    private void InitPage()
    {
        if (dimissionApplyId > 0)
        {
            ESP.HumanResource.Entity.DimissionInfo model = ESP.HumanResource.BusinessLogic.DimissionManager.GetModel(dimissionApplyId);

            //离职申请信息
            txtuserName.Text = model.userName;
            txtjoinJobDate.Text = model.joinJobDate.ToString("yyyy-MM-dd");
            txtgroupName.Text = model.groupName;
            txtdepartmentName.Text = model.departmentName;
            txtdimissionDate.Text = model.dimissionDate.ToString("yyyy-MM-dd");
            txtdimissionCause.Text = model.dimissionCause;

            ESP.HumanResource.Entity.SnapshotsInfo model2 = ESP.HumanResource.BusinessLogic.SnapshotsManager.GetModel(model.snapshotsId);
            //养老保险结束时间
            if (model2.endowmentInsuranceEndTime.Year > 1910)
                labsEndowmentEndTime.Text = model2.endowmentInsuranceEndTime.ToString("yyyy-MM");
            //失业保险结束时间        
            //if (model2.unemploymentInsuranceEndTime.Year > 1910)
            //    labsUnemploymentEndTime.Text = model2.unemploymentInsuranceEndTime.ToString("yyyy-MM");
            ////生育险结束时间
            //if (model2.birthInsuranceEndTime.Year > 1910)
            //    labsBirthEndTime.Text = model2.birthInsuranceEndTime.ToString("yyyy-MM");
            ////工伤险结束时间
            //if (model2.compoInsuranceEndTime.Year > 1910)
            //    labsCompoEndTime.Text = model2.compoInsuranceEndTime.ToString("yyyy-MM");
            ////医疗保险结束时间
            //if (model2.medicalInsuranceEndTime.Year > 1910)
            //    labsMedicalEndTime.Text = model2.medicalInsuranceEndTime.ToString("yyyy-MM");
            //公积金结束时间
            if (model2.publicReserveFundsEndTime.Year > 1910)
                labsPublicReserveFundsEndTime.Text = model2.publicReserveFundsEndTime.ToString("yyyy-MM");
        }
    }

    /// <summary>
    /// 返回按钮的Click事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("DimissionApplyList.aspx");
    }
}
