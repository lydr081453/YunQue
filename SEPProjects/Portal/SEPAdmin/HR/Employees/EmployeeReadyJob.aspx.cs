using System;
using System.Collections.Generic;
using ESP.HumanResource.BusinessLogic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using ESP.HumanResource.Common;
using ESP.Administrative.Entity;
using ESP.HumanResource.Entity;
using System.Collections;

public partial class Employees_EmployeeReadyJob : ESP.Web.UI.PageBase
{

    private string userid = "";
    private bool bo = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Employees_EmployeeReadyJob));

        this.drpContract_Company.Attributes.Add("onChange", "changeContractCompany(this.options[this.selectedIndex].value,this.options[this.selectedIndex].text);");

        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request["userid"]))
            {
                userid = Request["userid"].Trim();
                initForm(int.Parse(Request["userid"].Trim().ToString()));
            }
        }
    }

    [AjaxPro.AjaxMethod]
    public static List<List<string>> getBranch()
    {
        IList<ESP.Finance.Entity.BranchInfo> branchList = ESP.Finance.BusinessLogic.BranchManager.GetAllList();
        List<List<string>> list = new List<List<string>>();
        List<string> item = null;
        foreach (ESP.Finance.Entity.BranchInfo branch in branchList)
        {
            item = new List<string>();
            item.Add(branch.BranchCode.ToString());
            item.Add(branch.BranchName);
            list.Add(item);
        }
        List<string> c = new List<string>();
        c.Add("-1");
        c.Add("请选择...");
        list.Insert(0, c);
        return list;
    }

    protected void btnReturn_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["type"]))
            Response.Redirect("FieldworkList.aspx");
        else
            Response.Redirect("NewEmployessList.aspx");
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

        if (!string.IsNullOrEmpty(Request["userid"]))
        {



            ESP.HumanResource.Entity.EmployeeBaseInfo model = EmployeeBaseManager.GetModel(int.Parse(Request["userid"].Trim().ToString()));
            ESP.HumanResource.Entity.UsersInfo user = UsersManager.GetModel(int.Parse(Request["userid"].Trim().ToString()));
            ESP.HumanResource.Entity.SnapshotsInfo snap = new ESP.HumanResource.Entity.SnapshotsInfo();

            ESP.HumanResource.Entity.EmployeeJobInfo jobModel = EmployeeJobManager.getModelBySysId(model.UserID);

            if (model != null)
            {
                snap.UserID = model.UserID;
                if (!string.IsNullOrEmpty(Request["type"]))
                {
                    if (model.TypeID == 4)
                    {
                        bo = true;
                        snap.TypeID = model.TypeID = 1;//正式员工

                    }
                    else
                    {
                        snap.TypeID = model.TypeID;
                    }
                }
                else
                {
                    snap.TypeID = model.TypeID;
                }
                snap.CommonName = model.CommonName;
                snap.MaritalStatus = model.MaritalStatus;
                snap.Gender = model.Gender;
                snap.Birthday = model.Birthday;
                snap.Degree = model.Degree;
                snap.Education = model.Education;
                snap.GraduatedFrom = model.GraduateFrom;
                snap.Major = model.Major;
                snap.ThisYearSalary = model.ThisYearSalary;
                //待入职联系电话变正式手机电话
                snap.MobilePhone = model.MobilePhone = model.Phone2;
                snap.Status = model.Status;
                //工资
                snap.nowBasePay = snap.newBasePay = model.EmployeeWelfareInfo.socialInsuranceBase = ESP.Salary.Utility.DESEncrypt.Encode("0");
                //绩效
                snap.nowMeritPay = snap.newMeritPay = model.EmployeeWelfareInfo.socialInsuranceBase = ESP.Salary.Utility.DESEncrypt.Encode("0");
                snap.Creator = UserInfo.UserID;
                snap.CreatedTime = DateTime.Now;
                snap.UserName = model.FullNameCN;
                snap.CreatorName = UserInfo.Username;
                snap.IsForeign = model.IsForeign;
                snap.IsCertification = model.IsCertification;

                //养老/失业/工伤/生育缴费基数
                snap.socialInsuranceBase = model.EmployeeWelfareInfo.socialInsuranceBase = ESP.Salary.Utility.DESEncrypt.Encode("0");
                //医疗基数
                snap.medicalInsuranceBase = model.EmployeeWelfareInfo.medicalInsuranceBase = ESP.Salary.Utility.DESEncrypt.Encode("0");
                //公积金基数
                snap.publicReserveFundsBase = model.EmployeeWelfareInfo.publicReserveFundsBase = ESP.Salary.Utility.DESEncrypt.Encode("0");

                //养老保险公司比例
                snap.EIProportionOfFirms = model.EmployeeWelfareInfo.EIProportionOfFirms = decimal.Parse("0");
                //养老保险个人比例
                snap.EIProportionOfIndividuals = model.EmployeeWelfareInfo.EIProportionOfIndividuals = decimal.Parse("0");
                //失业保险公司比例
                snap.UIProportionOfFirms = model.EmployeeWelfareInfo.UIProportionOfFirms = decimal.Parse("0");
                //失业保险个人比例
                snap.UIProportionOfIndividuals = model.EmployeeWelfareInfo.UIProportionOfIndividuals = decimal.Parse("0");
                //生育保险公司比例
                snap.BIProportionOfFirms = model.EmployeeWelfareInfo.BIProportionOfFirms = decimal.Parse("0");
                //生育保险个人比例
                snap.BIProportionOfIndividuals = model.EmployeeWelfareInfo.BIProportionOfIndividuals = decimal.Parse("0");
                //工伤险公司比例
                snap.CIProportionOfFirms = model.EmployeeWelfareInfo.CIProportionOfFirms = decimal.Parse("0");
                //工伤险个人比例
                snap.CIProportionOfIndividuals = model.EmployeeWelfareInfo.CIProportionOfIndividuals = decimal.Parse("0");
                //医疗保险公司比例
                snap.MIProportionOfFirms = model.EmployeeWelfareInfo.MIProportionOfFirms = decimal.Parse("0");
                //医疗保险个人比例
                snap.MIProportionOfIndividuals = model.EmployeeWelfareInfo.MIProportionOfIndividuals = decimal.Parse("0");
                //医疗保险大额医疗个人支付额
                snap.MIBigProportionOfIndividuals = model.EmployeeWelfareInfo.MIBigProportionOfIndividuals = decimal.Parse("0");
                //公积金比例                    
                snap.MIBigProportionOfIndividuals = decimal.Parse("0");
                //公积金比例 
                snap.PRFProportionOfFirms = snap.PRFProportionOfIndividuals =
                    model.EmployeeWelfareInfo.PRFProportionOfFirms = model.EmployeeWelfareInfo.PRFProportionOfIndividuals = decimal.Parse("0");


                if (model.TypeID == 4)
                {
                    snap.Code = model.Code = ESP.Framework.BusinessLogic.EmployeeManager.CreateEmployeeCode("I{autono:00000}", "internship");
                }
                else
                {
                    //年假基数
                    if (ddlAnnualType.SelectedValue == "-1")
                    {
                        ShowCompleteMessage("请输入年假基数", "EmployeeReadyJob.aspx?userid=" + Request["userid"].Trim().ToString());
                        return;
                    }
                    model.AnnualLeaveBase = int.Parse(ddlAnnualType.SelectedValue);


                    model.BranchCode = hidContactBranch.Value;
                    model.EmployeeWelfareInfo.contractCompany = hidContactBranch.Value;
                    model.EmployeeWelfareInfo.socialInsuranceCompany = hidContactBranch.Value;

                    if (model.JoinDate != null)
                    {
                        model.FirstContractBeginDate = model.JoinDate;
                        model.FirstContractEndDate = model.JoinDate.Value.AddYears(3).AddDays(-1);

                        model.ContractSignDate = DateTime.Now;
                        model.ProbationDate = model.JoinDate.Value.AddMonths(6).AddDays(-1);

                        model.ContractBeginDate = model.FirstContractBeginDate;
                        model.ContractEndDate = model.FirstContractEndDate;
                    }


                    //员工编号
                    if (string.IsNullOrEmpty(model.Code) || model.Code.IndexOf("I") >= 0)
                    {
                        IList<ESP.HumanResource.Entity.EmployeeBaseInfo> emplist = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModelList(" and b.FirstNameCN='" + user.FirstNameCN + "' and b.LastNameCN='" + user.LastNameCN + "' and a.IDNumber='" + model.IDNumber + "'");
                        if (emplist != null && emplist.Count > 0 && !string.IsNullOrEmpty(emplist[0].Code))
                        {
                            model.Code = emplist[0].Code;
                        }
                        else
                        {
                            model.Code = ESP.Framework.BusinessLogic.EmployeeManager.CreateEmployeeCode();
                        }
                    }

                    //分机号
                    if (jobModel.companyid == 19 && string.IsNullOrEmpty(model.Phone1))
                    {
                        var tellist = ESP.HumanResource.BusinessLogic.TelManager.GetModelList("");
                        ESP.HumanResource.Entity.TELInfo tel = null;
                        if (tellist != null && tellist.Count > 0)
                        {
                            tel = tellist[0];
                            model.Phone1 = tel.Tel;

                            tel.Status = 0;
                            ESP.HumanResource.BusinessLogic.TelManager.Update(tel);

                        }
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(txtTelPhone.Text.Trim()))
                        {
                            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('请输入分机号！');", true);
                            return;
                        }
                        model.Phone1 = txtTelPhone.Text.Trim();
                    }



                    snap.Code = model.Code;

                    if (!string.IsNullOrEmpty(txtUserId.Text))
                    {
                        ESP.Administrative.BusinessLogic.ClockInManager clockInManager = new ESP.Administrative.BusinessLogic.ClockInManager();
                        ESP.Administrative.BusinessLogic.UserAttBasicInfoManager userAttBasicManager = new ESP.Administrative.BusinessLogic.UserAttBasicInfoManager();
                        clockInManager.Update(snap.Code, txtUserId.Text);
                        userAttBasicManager.Update(snap.Code, model.UserID);
                    }
                }
                //}

                try
                {
                    //养老保险公司应缴费用
                    snap.EIFirmsCosts = model.EmployeeWelfareInfo.EIFirmsCosts = ESP.Salary.Utility.DESEncrypt.Encode("0");
                    //养老保险个人应缴费用
                    snap.EIIndividualsCosts = model.EmployeeWelfareInfo.EIIndividualsCosts = ESP.Salary.Utility.DESEncrypt.Encode("0");
                    //失业保险公司应缴费用
                    snap.UIFirmsCosts = model.EmployeeWelfareInfo.UIFirmsCosts = ESP.Salary.Utility.DESEncrypt.Encode("0");
                    //失业保险个人应缴费用
                    snap.UIIndividualsCosts = model.EmployeeWelfareInfo.UIIndividualsCosts = ESP.Salary.Utility.DESEncrypt.Encode("0");
                    //生育保险公司应缴费用
                    snap.BIFirmsCosts = model.EmployeeWelfareInfo.BIFirmsCosts = ESP.Salary.Utility.DESEncrypt.Encode("0");
                    //生育保险个人应缴费用
                    snap.BIIndividualsCosts = model.EmployeeWelfareInfo.BIIndividualsCosts = ESP.Salary.Utility.DESEncrypt.Encode("0");
                    //工伤险公司应缴费用
                    snap.CIFirmsCosts = model.EmployeeWelfareInfo.CIFirmsCosts = ESP.Salary.Utility.DESEncrypt.Encode("0");
                    //工伤险个人应缴费用
                    snap.CIIndividualsCosts = model.EmployeeWelfareInfo.CIIndividualsCosts = ESP.Salary.Utility.DESEncrypt.Encode("0");
                    //医疗保险公司应缴费用
                    snap.MIFirmsCosts = model.EmployeeWelfareInfo.MIFirmsCosts = ESP.Salary.Utility.DESEncrypt.Encode("0");
                    //医疗保险个人应缴费用
                    snap.MIIndividualsCosts = model.EmployeeWelfareInfo.MIIndividualsCosts = ESP.Salary.Utility.DESEncrypt.Encode("0");
                    //公积金应缴费用
                    snap.PRFirmsCosts = snap.PRIIndividualsCosts =
                        model.EmployeeWelfareInfo.PRFirmsCosts = model.EmployeeWelfareInfo.PRIIndividualsCosts = ESP.Salary.Utility.DESEncrypt.Encode("0");
                }
                catch { }

                ESP.HumanResource.Entity.LogInfo logModel = new ESP.HumanResource.Entity.LogInfo();
                logModel.Des = user.LastNameCN + user.FirstNameCN + "入职";
                logModel.LogMedifiedTeme = DateTime.Now;
                logModel.LogUserId = UserInfo.UserID;
                logModel.LogUserName = UserInfo.Username;

                if (EmployeeBaseManager.UpdateReadyJobStatus(model, user, snap, logModel))
                {
                    try
                    {
                        if (string.IsNullOrEmpty(Request["type"]) && model.TypeID == 1)
                        {
                            SendMail(model.UserID, model.Code, txtItCode.Text, labBase_NameCn.Text,model.Photo);
                        }
                    }
                    catch (Exception ex)
                    {
                        ESP.Logging.Logger.Add(ex.ToString(), "HR", ESP.Logging.LogLevel.Error, ex);
                    }
                    if (!string.IsNullOrEmpty(Request["type"]))
                    {
                        ShowCompleteMessage("入职操作成功", "FieldworkList.aspx");
                    }
                    else
                    {
                        ShowCompleteMessage("入职操作成功", "NewEmployessList.aspx");
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(Request["type"]))
                    {
                        ShowCompleteMessage("入职操作失败,请重试", "EmployeeReadyJob.aspx?type=1&userid=" + model.UserID);
                    }
                    else
                    {
                        ShowCompleteMessage("入职操作失败,请重试", "EmployeeReadyJob.aspx?userid=" + model.UserID);
                    }
                }
            }
        }
    }

    protected void initForm(int sysid)
    {
        ESP.HumanResource.Entity.EmployeeBaseInfo model = EmployeeBaseManager.GetModel(sysid);

        ESP.HumanResource.Entity.UsersInfo user = UsersManager.GetModel(sysid);

        EmployeesInPositionsInfo positionModel = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetModel(sysid);

        ESP.Finance.Entity.DepartmentViewInfo deptView = ESP.Finance.BusinessLogic.DepartmentViewManager.GetModel(positionModel.DepartmentID);


        txtEmail.Text = user.Email;
        txtItCode.Text = user.Username;
        txtTel.Text = model.Phone2;

        txtUserId.Text = model.Code;

        drpUserType.Text = !string.IsNullOrEmpty(ESP.Framework.BusinessLogic.EmployeeManager.GetTypeName(model.TypeID)) ? ESP.Framework.BusinessLogic.EmployeeManager.GetTypeName(model.TypeID) : "正式员工";

        labBase_NameCn.Text = user.LastNameCN + user.FirstNameCN;

        labBase_Sex.Text = ESP.HumanResource.Common.Status.Gender_Names[model.Gender];

        try
        {
            labJob_JoinDate.Text = model.EmployeeJobInfo.joinDate.ToString("yyyy-MM-dd");
        }
        catch { }
        labJob_JoinJob.Text = model.EmployeeJobInfo.joinJob;


        if (!string.IsNullOrEmpty(model.Resume))
        {
            labResume.Text = "<a target='_blank' href='" + model.Resume + "'><img src='/Images/dc.gif' border='0' /></a>";
        }
        else
        {
            labResume.Text = "未上传简历";
        }
        if (model.OwnedPC)
            labOwnedPC.Text = "是";
        else
            labOwnedPC.Text = "否";
        labSeniority.Text = model.Seniority.ToString();

        labJob_CompanyName.Text = model.EmployeeJobInfo.companyName;
        labJob_DepartmentName.Text = model.EmployeeJobInfo.departmentName;
        labJob_GroupName.Text = model.EmployeeJobInfo.groupName;
        labIDCard.Text = model.IDNumber;


        if (deptView.level1Id == 19)
        {
            lblTelPhone.Visible = true;

        }
        else
        {
            lblTelPhone.Visible = false;
            txtTelPhone.Visible = true;
        }

        lblTelPhone.Text = model.Phone1;
        txtTelPhone.Text = model.Phone1;

        try
        {
            labWorkCity.Text = model.WorkCity;
        }
        catch (Exception ex) { }

        hidContactBranch.Value = model.BranchCode;
        txtBranch.Text = model.BranchCode;

        labJob_Memo.Text = model.Memo;
    }

    #region 发送邮件
    private void SendMail(int userid, string usercode, string username, string employeename,string photo)
    {
        string recipientAddress = "";
        int compID = 0;

        ESP.HumanResource.Entity.EmployeesInPositionsInfo userPosition = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetModel(userid);
        ESP.Framework.Entity.DepartmentInfo userDept = ESP.Framework.BusinessLogic.DepartmentManager.Get(userPosition.DepartmentID);
        if (userDept != null && !string.IsNullOrEmpty(userDept.Description))
        {
            compID = int.Parse(userDept.Description);
        }
        Hashtable ht = new Hashtable();
        if(!string.IsNullOrEmpty(photo))
            ht.Add("头像.jpg", Portal.Common.Global.USER_ICON_PATH + Portal.Common.Global.USER_ICON_FOLDER + photo);


        //辅助人员列表
        List<ESP.HumanResource.Entity.UsersInfo> list = UsersManager.GetUserList(compID, ESP.HumanResource.Common.Status.EntrySendMail);


        foreach (ESP.HumanResource.Entity.UsersInfo info in list)
        {
            if (!string.IsNullOrEmpty(info.Email))
            {
                recipientAddress += info.Email.Trim() + " ,";
            }
        }

        string keyno = "";
        if (!bo)
        {
            // 启动入职自动关联考勤，返回员工的门卡信息
            keyno = new ESP.Administrative.BusinessLogic.UserAttBasicInfoManager().AssociateAttendance(userid, usercode, username, employeename);
        }
        if (string.IsNullOrEmpty(keyno))
        {
            keyno = "0";
        }

        string url = "http://" + Request.Url.Authority + "/HR/Print/NewEmployeeEntryMail.aspx?userid=" + userid + "&keyno=" + keyno;
        string body = ESP.HumanResource.Common.SendMailHelper.ScreenScrapeHtml(url);
        SendMailHelper.SendMail("员工入职确定", recipientAddress, body, ht);

    }
    #endregion
}
