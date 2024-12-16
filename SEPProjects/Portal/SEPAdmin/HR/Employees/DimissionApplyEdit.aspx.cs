using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using ESP.HumanResource.Common;
using ESP.HumanResource.BusinessLogic;

public partial class Employees_DimissionApplyEdit : ESP.Web.UI.PageBase
{
    int dimissionApplyId = 0;
    int sysId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        //离职申请ID
        if (!string.IsNullOrEmpty(Request["dimissionApplyId"]))
        {
            dimissionApplyId = int.Parse(Request["dimissionApplyId"]);
            ESP.HumanResource.Entity.DimissionInfo model = GetModel();
            sysId = model.userId;
        }

        if (!string.IsNullOrEmpty(Request["userid"]))
            sysId = int.Parse(Request["userid"]);
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
            ESP.HumanResource.Entity.DimissionInfo model = GetModel();
            ESP.HumanResource.Entity.EmployeeBaseInfo info = EmployeeBaseManager.GetModel(model.userId);
            //离职申请信息
            txtuserName.Text = model.userName;
            sysId = model.userId;
            if (!string.IsNullOrEmpty(model.userCode))
            {
                labCode.Text = model.userCode;
            }
            else
            {

                labCode.Text = info.Code;
            }
            try
            {
                txtjoinJobDate.Text = model.joinJobDate.ToString("yyyy-MM-dd") == "1900-01-01" ? "" : model.joinJobDate.ToString("yyyy-MM-dd");
            }
            catch { }
            txtcompanyName.Text = model.companyName;
            hidcompanyId.Value = model.companyID.ToString();
            txtgroupName.Text = model.groupName;
            hidgroupId.Value = model.groupID.ToString();
            txtdepartmentName.Text = model.departmentName;
            hiddepartmentId.Value = model.departmentID.ToString();
            txtdimissionDate.Text = model.dimissionDate.ToString("yyyy-MM-dd");
            txtdimissionCause.Text = model.dimissionCause;

            ESP.HumanResource.Entity.SnapshotsInfo snap = SnapshotsManager.GetModel(model.snapshotsId);
            int year = DateTime.Now.Year - 10;
            for (int i = 0; i < 20; i++)
            {
                drpEndowmentEndTimeY.Items.Insert(i, new ListItem((year + i).ToString(), (year + i).ToString()));
                drpPublicReserveFundsEndTimeY.Items.Insert(i, new ListItem((year + i).ToString(), (year + i).ToString()));
                drpAccidentInsuranceEndTimeY.Items.Insert(i, new ListItem((year + i).ToString(), (year + i).ToString()));
            }
            try
            {
                drpEndowmentEndTimeY.SelectedValue = snap.endowmentInsuranceEndTime.ToString("yyyy");
            }
            catch
            {
                drpEndowmentEndTimeY.SelectedValue = DateTime.Now.Year.ToString();
            }
            try
            {
                drpPublicReserveFundsEndTimeY.SelectedValue = snap.publicReserveFundsEndTime.ToString("yyyy");
            }
            catch
            {
                drpPublicReserveFundsEndTimeY.SelectedValue = DateTime.Now.Year.ToString();
            }
            try
            {
                drpAccidentInsuranceEndTimeY.SelectedValue = info.EmployeeWelfareInfo.AccidentInsuranceEndTime.Year.ToString();
            }
            catch
            {
                drpAccidentInsuranceEndTimeY.SelectedValue = DateTime.Now.Year.ToString();
            }

            for (int i = 1; i <= 12; i++)
            {
                drpEndowmentEndTimeM.Items.Insert(i - 1, new ListItem((i).ToString("00"), (i).ToString("00")));
                drpPublicReserveFundsEndTimeM.Items.Insert(i - 1, new ListItem((i).ToString("00"), (i).ToString("00")));
                drpAccidentInsuranceEndTimeM.Items.Insert(i - 1, new ListItem((i).ToString("00"), (i).ToString("00")));
            }
            try
            {
                drpEndowmentEndTimeM.SelectedValue = snap.endowmentInsuranceEndTime.ToString("MM");
            }
            catch
            {
                drpEndowmentEndTimeM.SelectedValue = DateTime.Now.Month.ToString();
            }
            try
            {
                drpPublicReserveFundsEndTimeM.SelectedValue = snap.publicReserveFundsEndTime.ToString("MM");
            }
            catch
            {
                drpPublicReserveFundsEndTimeM.SelectedValue = DateTime.Now.Month.ToString();
            }
            try
            {
                drpAccidentInsuranceEndTimeM.SelectedValue = info.EmployeeWelfareInfo.AccidentInsuranceEndTime.Month.ToString("00");
            }
            catch
            {
                drpAccidentInsuranceEndTimeM.SelectedValue = DateTime.Now.Month.ToString();
            }

            chkFinish.Checked = model.isFinish;
        }
        else
        {
            //部门信息
            List<ESP.HumanResource.Entity.EmployeesInPositionsInfo> departments = EmployeesInPositionsManager.GetModelList(" a.UserID=" + sysId);
            ESP.HumanResource.Entity.EmployeeBaseInfo model = EmployeeBaseManager.GetModel(sysId);
            labCode.Text = model.Code;
            if (departments != null && departments.Count > 0)
            {
                List<ESP.Framework.Entity.DepartmentInfo> deps = new List<ESP.Framework.Entity.DepartmentInfo>();
                //////
                ESP.Framework.BusinessLogic.DepartmentManager.GetDepartmentListByID(departments[0].GroupID, deps);
                if (deps.Count == 3)
                {
                    txtcompanyName.Text = deps[0].DepartmentName;
                    hidcompanyId.Value = deps[0].DepartmentID.ToString();
                    txtdepartmentName.Text = deps[1].DepartmentName;
                    hiddepartmentId.Value = deps[1].DepartmentID.ToString();
                    txtgroupName.Text = deps[2].DepartmentName;
                    hidgroupId.Value = deps[2].DepartmentID.ToString();
                }
                else if (deps.Count == 2)
                {
                    txtdepartmentName.Text = deps[1].DepartmentName;
                    hiddepartmentId.Value = deps[1].DepartmentID.ToString();
                }
            }

            //入职日期
            ESP.HumanResource.Entity.EmployeeJobInfo employeeModel = EmployeeJobManager.getModelBySysId(sysId);
            txtjoinJobDate.Text = employeeModel == null ? "1900-01-01" : employeeModel.joinDate.ToString("yyyy-MM-dd");

            ESP.Framework.Entity.UserInfo userinfo = ESP.Framework.BusinessLogic.UserManager.Get(sysId);
            this.txtuserName.Text = userinfo.FullNameCN;

            int year = DateTime.Now.Year - 10;
            for (int i = 0; i < 20; i++)
            {
                drpEndowmentEndTimeY.Items.Insert(i, new ListItem((year + i).ToString(), (year + i).ToString()));
                drpPublicReserveFundsEndTimeY.Items.Insert(i, new ListItem((year + i).ToString(), (year + i).ToString()));
                drpAccidentInsuranceEndTimeY.Items.Insert(i, new ListItem((year + i).ToString(), (year + i).ToString()));
            }

            drpEndowmentEndTimeY.SelectedValue = DateTime.Now.Year.ToString();
            drpPublicReserveFundsEndTimeY.SelectedValue = DateTime.Now.Year.ToString();
            drpAccidentInsuranceEndTimeY.SelectedValue = DateTime.Now.Year.ToString();

            for (int i = 1; i <= 12; i++)
            {
                drpEndowmentEndTimeM.Items.Insert(i - 1, new ListItem((i).ToString("00"), (i).ToString("00")));
                drpPublicReserveFundsEndTimeM.Items.Insert(i - 1, new ListItem((i).ToString("00"), (i).ToString("00")));
                drpAccidentInsuranceEndTimeM.Items.Insert(i - 1, new ListItem((i).ToString("00"), (i).ToString("00")));
            }

            drpEndowmentEndTimeM.SelectedValue = DateTime.Now.Month.ToString();
            drpPublicReserveFundsEndTimeM.SelectedValue = DateTime.Now.Month.ToString();
            drpAccidentInsuranceEndTimeM.SelectedValue = DateTime.Now.Month.ToString();
        }
    }

    /// <summary>
    /// 获得离职申请的对象
    /// </summary>
    /// <returns></returns>
    private ESP.HumanResource.Entity.DimissionInfo GetModel()
    {
        ESP.HumanResource.Entity.DimissionInfo model = new ESP.HumanResource.Entity.DimissionInfo();
        if (dimissionApplyId > 0)
            model = DimissionManager.GetModel(dimissionApplyId);
        return model;
    }

    /// <summary>
    /// 获得离职申请的编辑后的对象
    /// </summary>
    /// <returns></returns>
    private ESP.HumanResource.Entity.DimissionInfo GetNewModel(ref ESP.HumanResource.Entity.SnapshotsInfo snapshots, ref ESP.HumanResource.Entity.EmployeeBaseInfo employeeBase)
    {
        ESP.HumanResource.Entity.DimissionInfo model = GetModel();
        //离职申请信息

        model.userCode = labCode.Text;
        model.userId = sysId; //申请人sysid
        model.userName = txtuserName.Text.Trim();
        model.joinJobDate = DateTime.Parse(txtjoinJobDate.Text.Trim());
        model.groupID = int.Parse(hidgroupId.Value == "" ? "0" : hidgroupId.Value);
        model.groupName = txtgroupName.Text.Trim();
        model.departmentID = int.Parse(hiddepartmentId.Value == "" ? "0" : hiddepartmentId.Value);
        model.departmentName = txtdepartmentName.Text.Trim();
        model.companyID = int.Parse(hidcompanyId.Value == "" ? "0" : hidcompanyId.Value);
        model.companyName = txtcompanyName.Text;
        model.dimissionDate = DateTime.Parse(txtdimissionDate.Text.Trim());
        model.dimissionCause = txtdimissionCause.Text.Trim();

        if (!string.IsNullOrEmpty(Request["userid"]))
        {
            snapshots = SnapshotsManager.GetTopModel(int.Parse(Request["userid"].ToString()));
        }
        else
        {
            snapshots = SnapshotsManager.GetTopModel(sysId);
        }

        if (snapshots == null || snapshots.UserID < 0)
        {
            snapshots.CreatedTime = DateTime.Now;
            snapshots.Creator = UserInfo.UserID;
            snapshots.CreatorName = UserInfo.Username;

            snapshots = new ESP.HumanResource.Entity.SnapshotsInfo();
            //养老/失业/工伤/生育缴费基数
            snapshots.socialInsuranceBase = ESP.Salary.Utility.DESEncrypt.Encode("0");
            //医疗基数
            snapshots.medicalInsuranceBase = ESP.Salary.Utility.DESEncrypt.Encode("0");
            //公积金基数
            snapshots.publicReserveFundsBase = ESP.Salary.Utility.DESEncrypt.Encode("0");

            try
            {
                //养老保险公司比例
                snapshots.EIProportionOfFirms = decimal.Parse("0");
                //养老保险个人比例
                snapshots.EIProportionOfIndividuals = decimal.Parse("0");
                //失业保险公司比例
                snapshots.UIProportionOfFirms = decimal.Parse("0");
                //失业保险个人比例
                snapshots.UIProportionOfIndividuals = decimal.Parse("0");
                //生育保险公司比例
                snapshots.BIProportionOfFirms = decimal.Parse("0");
                //生育保险个人比例
                snapshots.BIProportionOfIndividuals = decimal.Parse("0");
                //工伤险公司比例
                snapshots.CIProportionOfFirms = decimal.Parse("0");
                //工伤险个人比例
                snapshots.CIProportionOfIndividuals = decimal.Parse("0");
                //医疗保险公司比例
                snapshots.MIProportionOfFirms = decimal.Parse("0");
                //医疗保险个人比例
                snapshots.MIProportionOfIndividuals = decimal.Parse("0");
                //医疗保险大额医疗个人支付额
                snapshots.MIBigProportionOfIndividuals = decimal.Parse("0");
                //公积金比例
                snapshots.PRFProportionOfFirms = snapshots.PRFProportionOfIndividuals = decimal.Parse("0");
            }
            catch { }
        }

        if (chkPRF.Checked)
        {
            //公积金结束时间
            snapshots.publicReserveFundsEndTime = employeeBase.EmployeeWelfareInfo.publicReserveFundsEndTime = DateTime.Parse("1900-01-01");
        }
        else
        {
            try
            {
                //公积金结束时间
                snapshots.publicReserveFundsEndTime = employeeBase.EmployeeWelfareInfo.publicReserveFundsEndTime = DateTime.Parse(drpPublicReserveFundsEndTimeY.SelectedItem.Value + "-" + drpPublicReserveFundsEndTimeM.SelectedItem.Value + "-01");
            }
            catch { }
        }
        if (chkEndowment.Checked)
        {
            //养老保险结束时间            
            snapshots.endowmentInsuranceEndTime = employeeBase.EmployeeWelfareInfo.endowmentInsuranceEndTime = DateTime.Parse("1900-01-01");
            //失业保险结束时间 
            snapshots.unemploymentInsuranceEndTime = employeeBase.EmployeeWelfareInfo.unemploymentInsuranceEndTime = DateTime.Parse("1900-01-01");
            //生育险结束时间
            snapshots.birthInsuranceEndTime = employeeBase.EmployeeWelfareInfo.birthInsuranceEndTime = DateTime.Parse("1900-01-01");
            //工伤险结束时间
            snapshots.compoInsuranceEndTime = employeeBase.EmployeeWelfareInfo.compoInsuranceEndTime = DateTime.Parse("1900-01-01");
            //医疗保险结束时间
            snapshots.medicalInsuranceEndTime = employeeBase.EmployeeWelfareInfo.medicalInsuranceEndTime = DateTime.Parse("1900-01-01");
        }
        else
        {
            try
            {
                //养老保险结束时间            
                snapshots.endowmentInsuranceEndTime = employeeBase.EmployeeWelfareInfo.endowmentInsuranceEndTime = DateTime.Parse(drpEndowmentEndTimeY.SelectedItem.Value + "-" + drpEndowmentEndTimeM.SelectedItem.Value + "-01");
            }
            catch { }
            try
            {
                //失业保险结束时间 
                snapshots.unemploymentInsuranceEndTime = employeeBase.EmployeeWelfareInfo.unemploymentInsuranceEndTime = DateTime.Parse(drpEndowmentEndTimeY.SelectedItem.Value + "-" + drpEndowmentEndTimeM.SelectedItem.Value + "-01");
            }
            catch { }
            try
            {
                //生育险结束时间
                snapshots.birthInsuranceEndTime = employeeBase.EmployeeWelfareInfo.birthInsuranceEndTime = DateTime.Parse(drpEndowmentEndTimeY.SelectedValue + "-" + drpEndowmentEndTimeM.SelectedValue + "-01");
            }
            catch { }
            try
            {
                //工伤险结束时间
                snapshots.compoInsuranceEndTime = employeeBase.EmployeeWelfareInfo.compoInsuranceEndTime = DateTime.Parse(drpEndowmentEndTimeY.SelectedValue + "-" + drpEndowmentEndTimeM.SelectedValue + "-01");
            }
            catch { }
            try
            {
                //医疗保险结束时间
                snapshots.medicalInsuranceEndTime = employeeBase.EmployeeWelfareInfo.medicalInsuranceEndTime = DateTime.Parse(drpEndowmentEndTimeY.SelectedValue + "-" + drpEndowmentEndTimeM.SelectedValue + "-01");
            }
            catch { }
        }

        employeeBase.EmployeeWelfareInfo.AccidentInsuranceEndTime = DateTime.Parse(drpAccidentInsuranceEndTimeY.SelectedItem.Value + "-" + drpAccidentInsuranceEndTimeM.SelectedItem.Value + "-01");

        model.isFinish = chkFinish.Checked;

        return model;
    }

    protected void btnCommit_Click(object sender, EventArgs e)
    {
        ESP.HumanResource.Entity.SnapshotsInfo snapshots = null;
        ESP.HumanResource.Entity.EmployeeBaseInfo employeeBase = EmployeeBaseManager.GetModel(sysId);
        ESP.HumanResource.Entity.DimissionInfo model = GetNewModel(ref snapshots, ref employeeBase);
        int returnValue = 0;
        if (dimissionApplyId > 0)
        {
            returnValue = DimissionManager.Update(model, snapshots, employeeBase, LogManager.GetLogModel(UserInfo.Username + "修改了" + model.userName + "的离职申请", UserInfo.UserID, UserInfo.Username, model.userId, model.userName, Status.Log), true);
            if (returnValue > 0)
            {
                if (model.isFinish)
                {
                    SendMail(model.userId.ToString(), model.companyID);
                }
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location='UnfinishedDimissionList.aspx';alert('离职申请成功！');", true);
            }
            else
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('离职申请失败！');", true);
        }
        else
        {
            model.creater = UserInfo.UserID; //创建人
            model.createDate = DateTime.Now;
            returnValue = DimissionManager.Add(model, snapshots, employeeBase, LogManager.GetLogModel(UserInfo.Username + "添加了" + model.userName + "的离职申请", UserInfo.UserID, UserInfo.Username, model.userId, model.userName, Status.Log), true);
            if (returnValue > 0)
            {
                if (model.isFinish)
                {
                    SendMail(model.userId.ToString(), model.companyID);
                }

                if (!string.IsNullOrEmpty(Request["type"]))
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location='FieldworkList.aspx';alert('离职申请成功！');", true);
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location='EmployeesAllList.aspx';alert('离职申请成功！');", true);
                }
            }
            else
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('离职申请失败！');", true);
        }
    }

    /// <summary>
    /// 返回按钮的Click事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["type"]))
        {
            Response.Redirect("/HR/Employees/FieldworkList.aspx");
        }
        else
        {
            Response.Redirect("/HR/Employees/EmployeesAllList.aspx");
        }
    }

    private void SendMail(string userid, int companyID)
    {
        try
        {
            string recipientAddress = "";

            List<ESP.HumanResource.Entity.UsersInfo> list = UsersManager.GetUserList(companyID, ESP.HumanResource.Common.Status.DimissionSendMail);
            foreach (ESP.HumanResource.Entity.UsersInfo info in list)
            {
                if (!string.IsNullOrEmpty(info.Email))
                {
                    recipientAddress += info.Email.Trim() + " ,";
                }
            }
            if (recipientAddress.Trim().Length > 1)
            {
                recipientAddress = recipientAddress.Substring(0, recipientAddress.Length - 1);
            }

            string url = "http://" + Request.Url.Authority + "/HR/Print/DimissionMail2.aspx?uid=" + userid;

            string body = ESP.HumanResource.Common.SendMailHelper.ScreenScrapeHtml(url);

            SendMailHelper.SendMail("离职员工信息", recipientAddress, body, null);
        }
        catch (Exception ex)
        {
            ESP.Logging.Logger.Add(ex.ToString());
        }
    }
}
