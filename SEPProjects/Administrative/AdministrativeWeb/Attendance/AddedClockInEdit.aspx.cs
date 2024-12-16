using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Framework.Entity;
using ESP.Framework.BusinessLogic;
using ESP.Administrative.BusinessLogic;
using ESP.Administrative.Entity;
using ESP.Administrative.Common;

namespace AdministrativeWeb.Attendance
{
    public partial class AddedClockInEdit : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // 通过用户ID获得用户信息
                //if (Request["ApplicantID"] != null)
                //{
                   // string applicantID = Request["ApplicantID"];
                    //SelectUserID = applicantID;
                    //upUserInfo.Visible = true;
                   // getUserInfo(int.Parse(applicantID));
                    txtReadTime.SelectedDate = DateTime.Now;
               // }
               // GetDepartmentInfo();
            }
        }

        /// <summary>
        /// 补充用户上下班时间信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ESP.Administrative.BusinessLogic.UserAttBasicInfoManager b = new ESP.Administrative.BusinessLogic.UserAttBasicInfoManager();
                MonthStatManager monthStatManager = new MonthStatManager();
                if (!string.IsNullOrEmpty(hidUserCardID.Value) && hidUserCardID.Value != "0")
                {
                    int id = int.Parse(hidUserCardID.Value);
                    ESP.Administrative.Entity.UserAttBasicInfo usercardinfo = b.GetModel(id);
                    DateTime selectDate = txtReadTime.SelectedDate;

                    // 判断用户是否可以操作该考勤记录信息，如果月度考勤已经提交就不可以操作该考勤记录信息
                    if (!monthStatManager.TryOperateData(usercardinfo.Userid, selectDate))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "btnSave", "alert('月度考勤已经提交，您不能对该月考勤记录进行任何操作。');", true);
                        return;
                    }

                    if (txtGoWorkTime.Text != null && !string.IsNullOrEmpty(txtGoWorkTime.Text.Trim()))
                    {
                        DateTime readTime = DateTime.Parse(selectDate.ToString("yyyy-MM-dd ") + txtGoWorkTime.Text.Trim());
                        ClockInInfo clockInInfo = new ClockInInfo();
                        clockInInfo.CardNO = usercardinfo.CardNo;
                        clockInInfo.CreateTime = DateTime.Now;
                        clockInInfo.Deleted = false;
                        clockInInfo.DoorName = "";
                        clockInInfo.InOrOut = true;
                        
                        clockInInfo.OperatorID = UserID;
                        clockInInfo.OperatorName = UserInfo.FullNameCN;
                        clockInInfo.ReadTime = readTime;
                        clockInInfo.UpdateTime = DateTime.Now;
                        clockInInfo.UserCode = usercardinfo.UserCode;
                        clockInInfo.Remark = txtRemark.Text.Trim();
                        new ClockInManager().Add(clockInInfo);
                    }
                    if (txtOffWorkTime.Text != null && !string.IsNullOrEmpty(txtOffWorkTime.Text.Trim()))
                    {
                        DateTime readTime = DateTime.Parse(selectDate.ToString("yyyy-MM-dd ") + txtOffWorkTime.Text.Trim());
                        TimeSpan span = TimeSpan.Parse(txtOffWorkTime.Text.Trim());
                        if (span <= TimeSpan.Parse(Status.OffWorkTimePoint))
                        {
                            readTime = readTime.AddDays(1);
                        }
                        
                        ClockInInfo clockInInfo = new ClockInInfo();
                        clockInInfo.CardNO = usercardinfo.CardNo;
                        clockInInfo.CreateTime = DateTime.Now;
                        clockInInfo.Deleted = false;
                        clockInInfo.DoorName = "";
                        clockInInfo.InOrOut = false;

                        clockInInfo.OperatorID = UserID;
                        clockInInfo.OperatorName = UserInfo.FullNameCN;
                        clockInInfo.ReadTime = readTime;
                        clockInInfo.UpdateTime = DateTime.Now;
                        clockInInfo.UserCode = usercardinfo.UserCode;
                        clockInInfo.Remark = txtRemark.Text.Trim();
                        new ClockInManager().Add(clockInInfo);
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "btnSave", "alert('数据保存成功！');", true);

                }
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "btnSave", "alert('数据保存失败！');", true);
            }
        }

        /// <summary>
        /// 上下班时间类型变换后，自动变换时间内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpInOrOut_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserAttBasicInfoManager b = new UserAttBasicInfoManager();
            UserAttBasicInfo userAttBasicModel = b.GetModelByUserid(UserID);
            if (userAttBasicModel != null && userAttBasicModel.GoWorkTime != null && userAttBasicModel.OffWorkTime != null)
            {
                //if (drpInOrOut.SelectedValue == "1")
                //    txtReadTime.SelectedDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd ") + userAttBasicModel.GoWorkTime);
                //else
                //    txtReadTime.SelectedDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd ") + userAttBasicModel.OffWorkTime);
            }
            else
                txtReadTime.SelectedDate = DateTime.Now;
        }

        /// <summary>
        /// 返回到上一个页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddedClockInList.aspx");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string strWhere = string.Empty;
            if (!string.IsNullOrEmpty(txtCode.Text))
            {
                strWhere = " and (b.username like '%" + txtCode.Text + "%' or a.code like '%" + txtCode.Text + "%' or b.LastNameCN+b.FirstNameCN like '%" + txtCode.Text + "%')";


                ESP.HumanResource.Entity.EmployeeBaseInfo userModel = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModelList(strWhere).FirstOrDefault();
                if (userModel != null)
                {
                    string script = string.Empty;
                    ESP.Compatible.Employee emp = new ESP.Compatible.Employee(userModel.UserID);
                    IList<string> depts = emp.GetDepartmentNames();
                    string deptstr = string.Empty;
                    if (depts != null && depts.Count != 0)
                    {
                        for (int i = 0; i < depts.Count; i++)
                        {
                            deptstr += depts[i].ToString() + ",";
                        }
                    }
                    string empname = emp.Name == string.Empty ? "&nbsp;" : emp.Name;
                    string empITCode = emp.ITCode == string.Empty ? "&nbsp;" : emp.ITCode;
                    string empMobile = emp.Mobile == string.Empty ? "&nbsp;" : emp.Mobile;
                    string empEmail = emp.EMail == string.Empty ? "&nbsp;" : emp.EMail;
                    string empTel = emp.Telephone == string.Empty ? "&nbsp;" : emp.Telephone;
                    string strdept = deptstr.TrimEnd(',') == string.Empty ? "&nbsp;" : deptstr.TrimEnd(',');

                    UserAttBasicInfoManager userCarInfoManager = new UserAttBasicInfoManager();
                    UserAttBasicInfo userCardInfo = userCarInfoManager.GetModelByUserid(userModel.UserID);
                    if (userCardInfo != null)
                    {
                        txtCardno.Text = userCardInfo.CardNo;
                        hidUserCardID.Value = userCardInfo.ID.ToString();
                    }
                    labUserName.Text = empname;            // 员工姓名
                    labUserCode.Text = emp.ID;             // 用户编号
                    labUserITCode.Text = empITCode;        // 员工账号
                    labUserTel.Text = empMobile;              // 公司电话
                    labUserDept.Text = strdept;            // 所属部门
                    hidUserid.Value = userModel.UserID.ToString();   // 用户编号
                }
            }
        }
    }
}
