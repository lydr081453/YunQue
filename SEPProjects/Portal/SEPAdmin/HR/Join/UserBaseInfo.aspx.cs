using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.HumanResource.BusinessLogic;

/// <summary>
/// 员工编辑个人信息
/// </summary>
public partial class UserBaseInfo : ESP.Web.UI.PageBase
{
    private const string savepath = "/UserImage/UserHeadImage" + "/";
    protected int ContractCount = 0;
    protected bool isAccept = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        #region AjaxProRegister
        AjaxPro.Utility.RegisterTypeForAjax(typeof(EmployeeBaseManager));
        #endregion

        if (!IsPostBack)
        {
            initForm();  // 初始化页面信息
            ListBind();  // 获得用户职位信息
        }
    }

    /// <summary>
    /// 初始化页面信息
    /// </summary>
    protected void initForm()
    {
        ESP.HumanResource.Entity.EmployeeBaseInfo model = EmployeeBaseManager.GetModel(UserID);
        ESP.HumanResource.Entity.UsersInfo userModel = UsersManager.GetModel(UserID);

        //照片
        if (!string.IsNullOrEmpty(model.Photo))
            imgBase_Photo.ImageUrl = savepath + model.Photo;
        else
            imgBase_Photo.ImageUrl = "../../public/CutImage/image/man.GIF";

        #region 个人基本情况
        txtUserCode.Text = model.Code;  // 员工编号
        txtJob_JoinDate.Text = model.EmployeeJobInfo.joinDate.Year < 1901 ? "" : model.EmployeeJobInfo.joinDate.ToString("yyyy-MM-dd");  // 入职日期
        txtBase_Email.Text = userModel.Email;  // 公司邮箱
        txtBase_FitstNameCn.Text = userModel.FirstNameCN;  // 中文名
        txtBase_LastNameCn.Text = userModel.LastNameCN;
        txtBase_FirstNameEn.Text = userModel.FirstNameEN;  // 英文姓名
        txtBase_LastNameEn.Text = userModel.LastNameEN;
        txtCommonName.Text = model.CommonName;  // 员工昵称（公司常用名）
        // 性别
        if (ESP.HumanResource.Common.Status.Gender.Male.Equals(model.Gender))
            radBase_Sex1.Checked = true;
        else if (ESP.HumanResource.Common.Status.Gender.Female.Equals(model.Gender))
            radBase_Sex2.Checked = true;
        txtBase_Birthday.Text = model.Birthday.Year < 1901 ? "" : model.Birthday.ToString("yyyy-MM-dd");  // 出生日期
        txtBase_PlaceOfBirth.Text = model.BirthPlace;  // 籍贯
        txtBase_IdNo.Text = model.IDNumber;  // 身份证号码
        txtBase_DomicilePlace.Text = model.DomicilePlace;  // 户口所在地
        // 婚否
                txtBase_Marriage.SelectedValue = model.MaritalStatus.ToString();
             

        txtBase_Health.Text = model.Health;  // 健康状况
        #endregion

        #region 个人专业情况
        txtBase_FinishSchool.Text = model.GraduateFrom;  // 毕业院校
        txtBase_FinishSchoolDate.Text = model.GraduatedDate.ToString("yyyy-MM-dd") == "1754-01-01" ? "" : model.GraduatedDate.ToString("yyyy-MM-dd");  // 毕业时间
        txtBase_Speciality.Text = model.Major;  // 专业
        txtBase_Education.SelectedValue = model.Education;  // 学历
        txtBase_WorkSpecialty.Text = model.WorkSpecialty;  // 个人特长
        #endregion

        #region 其他个人情况
        txtBase_HomePhone.Text = model.HomePhone;  // 家庭联系电话
        txtBase_MobilePhone.Text = model.MobilePhone;  // 手机
        txtBase_PrivateEmail.Text = model.PrivateEmail;  // 个人邮箱
        txtBase_Address1.Text = model.Address;  // 家庭通讯地址
        txtBase_PostCode.Text = model.PostCode;  // 邮政编码
        txtBase_EmergencyLinkman.Text = model.EmergencyContact;  // 紧急联系人
        txtBase_EmergencyPhone.Text = model.EmergencyContactPhone;  // 紧急联系人电话
        #endregion

        // 简历附件
        if (!string.IsNullOrEmpty(model.Resume))
        {
            labResume.Text = "<a target='_blank' href='" + model.Resume + "'><img src='/Images/dc.gif' border='0' /></a>";
            labResume.Visible = true;
        }
        else
            labResume.Visible = false;
        txtBase_WorkExperience.Text = model.WorkExperience;  // 工作履历
        // 在六个月内是否有严重的疾病或意外的事故，无/有，请详细说明
        txtBase_DiseaseInSixMonths.Text = model.DiseaseInSixMonthsInfo;
        txtJob_Memo.Text = model.Memo;  // 备注

        //户口性质
        if (!string.IsNullOrEmpty(model.Residence))
            ddlHuji.SelectedValue = model.Residence;
        //配偶姓名
        txtMate.Text = model.MateName;
        //本人现住址
        txtAddressNow.Text = model.AdrressNow;
        //邮编
        txtPostCodeNow.Text = model.PostCodeNow;
        //家庭成员
        txtFamilly.Text = model.FamillyDesc;
        radAccept.Checked = isAccept ? true : false;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        ESP.HumanResource.Entity.EmployeeBaseInfo model = EmployeeBaseManager.GetModel(UserID);
        ESP.HumanResource.Entity.SnapshotsInfo snapshots = SnapshotsManager.GetTopModel(UserID);
        ESP.HumanResource.Entity.UsersInfo userModel = UsersManager.GetModel(model.UserID);
        if (model != null)
        {
            initModel(ref model, ref snapshots, ref userModel);
            ESP.HumanResource.Entity.LogInfo logModel = new ESP.HumanResource.Entity.LogInfo();
            logModel.LogUserId = UserInfo.UserID;
            logModel.LogMedifiedTeme = DateTime.Now;
            logModel.Des =CurrentUser.Name+ "修改了人员[" + userModel.LastNameCN + userModel.FirstNameCN + "]信息"+model.IDNumber;
            logModel.LogUserName = UserInfo.Username;

            if (EmployeeBaseManager.Update(model, userModel, snapshots, logModel))
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('个人信息修改成功！');", true);
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('个人信息修改失败，请重试。');", true);
            }
            if (!string.IsNullOrEmpty(hidReadCount.Value))
            {
                ContractCount = int.Parse(hidReadCount.Value);
            }
            isAccept = radAccept.Checked ? true : false;
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        ESP.HumanResource.Entity.EmployeeBaseInfo model = EmployeeBaseManager.GetModel(UserID);
        ESP.HumanResource.Entity.SnapshotsInfo snapshots = SnapshotsManager.GetTopModel(UserID);
        ESP.HumanResource.Entity.UsersInfo userModel = UsersManager.GetModel(model.UserID);
        if (model != null)
        {
            initModel(ref model, ref snapshots, ref userModel);
            ESP.HumanResource.Entity.LogInfo logModel = new ESP.HumanResource.Entity.LogInfo();
            logModel.LogUserId = UserInfo.UserID;
            logModel.LogMedifiedTeme = DateTime.Now;
            logModel.Des = "修改人员[" + userModel.LastNameCN + userModel.FirstNameCN + "]信息";
            logModel.LogUserName = UserInfo.Username;

            if (EmployeeBaseManager.Update(model, userModel, snapshots, logModel))
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.open('JoinFormPrint.aspx');", true);
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('个人信息修改失败，请重试。');", true);
            }
            if (!string.IsNullOrEmpty(hidReadCount.Value))
            {
                ContractCount = int.Parse(hidReadCount.Value);
            }
            isAccept = radAccept.Checked ? true : false;
        }
    }

    /// <summary>
    /// 将页面信息保存到相对应的Model中
    /// </summary>
    /// <param name="model">用户基本信息</param>
    /// <param name="snapshots">用户快照信息</param>
    /// <param name="userModel">用户内网账号信息</param>
    protected void initModel(ref ESP.HumanResource.Entity.EmployeeBaseInfo model, ref ESP.HumanResource.Entity.SnapshotsInfo snapshots,
        ref ESP.HumanResource.Entity.UsersInfo userModel)
    {
        if (snapshots == null)
            snapshots = new ESP.HumanResource.Entity.SnapshotsInfo();

        #region 个人基本情况
        snapshots.Code = model.Code = txtUserCode.Text.Trim();  // 员工编号
        model.EmployeeJobInfo.joinDate = DateTime.Parse(txtJob_JoinDate.Text.Trim());  // 入职日期
        userModel.FirstNameCN = txtBase_FitstNameCn.Text.Trim();  // 中文姓名
        userModel.LastNameCN = txtBase_LastNameCn.Text.Trim();
        userModel.FirstNameEN = txtBase_FirstNameEn.Text.Trim();  // 英文姓名
        userModel.LastNameEN = txtBase_LastNameEn.Text.Trim();
        model.CommonName = snapshots.CommonName = txtCommonName.Text.Trim();  // 个人昵称
        // 性别
        if (radBase_Sex1.Checked)
            model.Gender = (int)ESP.HumanResource.Common.Status.Gender.Male;
        else
            model.Gender = (int)ESP.HumanResource.Common.Status.Gender.Female;
        if (string.IsNullOrEmpty(txtBase_Birthday.Text))
            model.Birthday = DateTime.Now;
        else
            model.Birthday = DateTime.Parse(txtBase_Birthday.Text.Trim());  // 出生日期
        model.BirthPlace = txtBase_PlaceOfBirth.Text.Trim();  // 籍贯
        model.IDNumber = txtBase_IdNo.Text.Trim();  // 身份证号码
        model.DomicilePlace = txtBase_DomicilePlace.Text.Trim();  // 户口所在地
        snapshots.MaritalStatus = model.MaritalStatus = int.Parse(txtBase_Marriage.SelectedValue);  // 婚姻状况
        model.Health = txtBase_Health.Text.Trim();  // 健康状况
        #endregion

        #region 个人专业情况
        snapshots.GraduatedFrom = model.GraduateFrom = txtBase_FinishSchool.Text.Trim();  // 毕业院校
        if (string.IsNullOrEmpty(txtBase_FinishSchoolDate.Text))
            model.GraduatedDate = DateTime.Now;
        else
            model.GraduatedDate = DateTime.Parse(txtBase_FinishSchoolDate.Text.Trim());  // 毕业时间
        snapshots.Major = model.Major = txtBase_Speciality.Text.Trim();  // 专业
        snapshots.Education = model.Education = txtBase_Education.SelectedValue;  // 最高学历
        model.WorkSpecialty = txtBase_WorkSpecialty.Text.Trim();  // 个人特长
        #endregion

        #region 个人其他情况
        model.HomePhone = txtBase_HomePhone.Text.Trim();  // 家庭联系电话
        model.MobilePhone = txtBase_MobilePhone.Text.Trim();  // 手机
        model.PrivateEmail = txtBase_PrivateEmail.Text.Trim();
        model.Address = txtBase_Address1.Text.Trim();  // 家庭通讯地址
        model.PostCode = txtBase_PostCode.Text.Trim();  // 邮政编码
        model.EmergencyContact = txtBase_EmergencyLinkman.Text.Trim();  // 紧急联系人
        model.EmergencyContactPhone = txtBase_EmergencyPhone.Text.Trim();  //紧急联系人电话
        #endregion

        // 员工简历信息
        if (fileCV.PostedFile != null && fileCV.PostedFile.ContentLength > 0)
        {
            string filename = SaveFile(model.Code);
            model.Resume = filename;
        }
        model.WorkExperience = txtBase_WorkExperience.Text.Trim();  // 工作履历
        //在六个月内是否有严重的疾病或意外的事故，无/有，请详细说明
        model.DiseaseInSixMonthsInfo = txtBase_DiseaseInSixMonths.Text.Trim();
        model.Memo = txtJob_Memo.Text.Trim();  // 备注
        model.LastModifiedTime = DateTime.Now;
        model.LastModifier = UserID;
        model.LastModifierName = UserInfo.FullNameCN;

        //户口性质
        model.Residence = ddlHuji.SelectedValue;
        //配偶姓名
        model.MateName = txtMate.Text;
        //本人现住址
        model.AdrressNow = txtAddressNow.Text;
        //邮编
        model.PostCodeNow = txtPostCodeNow.Text;
        //家庭成员
        model.FamillyDesc = txtFamilly.Text;
    }

    /// <summary>
    /// 保存员工简历信息
    /// </summary>
    /// <param name="Code">员工编号</param>
    /// <returns>员工简历文件名</returns>
    private string SaveFile(string Code)
    {
        HttpPostedFile myFile = fileCV.PostedFile;
        if (myFile.FileName != null && myFile.ContentLength > 0)//&& theFile.ContentLength <= Config.PHOTO_CONTENT_LENGTH)
        {
            Random random = new Random((int)DateTime.Now.Ticks);
            string fn = "/HR/ResumeFiles/" + Code + "_" + DateTime.Now.ToString("yyyyMMdd") + random.Next(100, 999).ToString() + fileCV.FileName;
            myFile.SaveAs(System.Web.HttpContext.Current.Server.MapPath(fn));
            return fn;
        }
        else
        {
            return "";
        }
    }

    protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Del")
        {
            string ids = e.CommandArgument.ToString();
            ESP.HumanResource.Entity.LogInfo logModel = new ESP.HumanResource.Entity.LogInfo();
            logModel.LogMedifiedTeme = DateTime.Now;
            logModel.Des = "为" + ESP.Framework.BusinessLogic.EmployeeManager.Get(int.Parse(ids.Split(',')[0])).FullNameCN + "删除了部门职务";
            logModel.LogUserId = UserInfo.UserID;
            logModel.LogUserName = UserInfo.Username;
            EmployeesInPositionsManager.Delete(int.Parse(ids.Split(',')[0]), int.Parse(ids.Split(',')[1]), int.Parse(ids.Split(',')[2]), logModel);
            ListBind();
        }
    }

    protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }

    private void ListBind()
    {
        List<ESP.HumanResource.Entity.EmployeesInPositionsInfo> list = EmployeesInPositionsManager.GetModelList(" a.userID = " + UserID);
        gvList.DataSource = list;
        gvList.DataBind();

        if (gvList.PageCount > 0)
        {
            if (gvList.PageIndex + 1 == gvList.PageCount)
                disButton("last");
            else if (gvList.PageIndex == 0)
                disButton("first");
            else
                disButton("");
        }
    }

    #region 分页设置
    protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvList.PageIndex = e.NewPageIndex;
        ListBind();
    }

    protected void btnLast_Click(object sender, EventArgs e)
    {
        Paging(gvList.PageCount);
    }
    protected void btnFirst_Click(object sender, EventArgs e)
    {
        Paging(0);
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        Paging((gvList.PageIndex + 1) > gvList.PageCount ? gvList.PageCount : (gvList.PageIndex + 1));
    }
    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        Paging((gvList.PageIndex - 1) < 0 ? 0 : (gvList.PageIndex - 1));
    }

    /// <summary>
    /// 翻页
    /// </summary>
    /// <param name="pageIndex">页码</param>
    private void Paging(int pageIndex)
    {
        GridViewPageEventArgs e = new GridViewPageEventArgs(pageIndex);
        gvList_PageIndexChanging(new object(), e);
    }

    /// <summary>
    /// 分页按钮的显示设置
    /// </summary>
    /// <param name="page"></param>
    private void disButton(string page)
    {
        switch (page)
        {
            case "first":
                //btnFirst.Enabled = false;
                //btnPrevious.Enabled = false;
                //btnNext.Enabled = true;
                //btnLast.Enabled = true;
                break;
            case "last":
                //btnFirst.Enabled = true;
                //btnPrevious.Enabled = true;
                //btnNext.Enabled = false;
                //btnLast.Enabled = false;
                break;
            default:
                //btnFirst.Enabled = true;
                //btnPrevious.Enabled = true;
                //btnNext.Enabled = true;
                //btnLast.Enabled = true;
                break;
        }
    }

    #endregion
}