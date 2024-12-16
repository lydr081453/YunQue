using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Framework.BusinessLogic;
using ESP.Framework.Entity;
using ESP.HumanResource.BusinessLogic;
using ESP.HumanResource.Entity;

public partial class OfferLetter : ESP.Web.UI.PageBase
{
    protected int curUserId
    {
        get
        {
            return ViewState["curUserId"] == null ? 0 : (int)ViewState["curUserId"];
        }
        set
        {
            ViewState["curUserId"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request["userid"]))
            {
                int userid = 0;
                if (int.TryParse(Request["userid"], out userid))
                    curUserId = userid;
                decimal nowBasePay = 0;
                decimal nowMeritPay = 0;
                decimal nowAttendance = 0;
                if (!string.IsNullOrEmpty(Request["nowBasePay"]))
                    nowBasePay = decimal.Parse(Request["nowBasePay"]);
                if (!string.IsNullOrEmpty(Request["nowMeritPay"]))
                    nowMeritPay = decimal.Parse(Request["nowMeritPay"]);
                if (!string.IsNullOrEmpty(Request["nowAttendance"]))
                    nowAttendance = decimal.Parse(Request["nowAttendance"]);

                InitPage(curUserId, nowBasePay, nowMeritPay, nowAttendance);
                imgShunya.Src = "http://" + ESP.Framework.BusinessLogic.WebSiteManager.Get(1).UrlPrefix + "/Images/xingyan.png";
                linkOk.HRef = "http://" + ESP.Framework.BusinessLogic.WebSiteManager.Get(1).UrlPrefix + "/Hr/Join/InternOfferLetterConfirm.aspx?code=" + Server.UrlEncode(ESP.Salary.Utility.DESEncrypt.Encode(curUserId + ""));
            }
        }
    }

    protected void InitPage(int userid, decimal nowBasePay, decimal nowMeritPay, decimal nowAttendance)
    {
        if (userid > 0)
        {
            ESP.Framework.BusinessLogic.UserManager.Get(userid);
            // 用户信息
            EmployeeBaseInfo employeeBaseInfo = EmployeeBaseManager.GetModel(userid);
            ESP.Framework.Entity.UserInfo usersInfo = UserManager.Get(userid);
            IList<EmployeePositionInfo> depPositionList = DepartmentPositionManager.GetEmployeePositions(userid);
            HeadAccountInfo hcInfo = new HeadAccountManager().GetModelByUserid(userid);
            EmployeeBaseInfo hcCreator = EmployeeBaseManager.GetModel(hcInfo.CreatorId);
           
            if (employeeBaseInfo != null)
            {
                labUserName.Text = usersInfo.FullNameCN;
                //labIDNumber.Text = employeeBaseInfo.IDNumber;
                //labFullUserNameEN.Text = employeeBaseInfo.FullNameEN;
                if (depPositionList != null && depPositionList.Count > 0)
                {
                    EmployeePositionInfo empPositionInfo = depPositionList[0];
                    labPosition.Text = empPositionInfo.DepartmentPositionName;
                    labGroupInfo.Text = "";
                    List<DepartmentInfo> departmentList = new List<DepartmentInfo>();
                    int deptid = 0;
                    departmentList = DepartmentManager.GetDepartmentListByID(empPositionInfo.DepartmentID, departmentList);
                    if (departmentList != null && departmentList.Count > 0)
                    {
                        string level1 = "";
                        string level2 = "";
                        string level3 = "";
                        foreach (DepartmentInfo dep in departmentList)
                        {
                            if (dep.DepartmentLevel == 1)
                            {
                                level1 = dep.DepartmentName;
                               
                            }
                            if (dep.DepartmentLevel == 2)
                            {
                                level2 = dep.DepartmentName;
                            }
                            if (dep.DepartmentLevel == 3)
                            {
                                level3 = dep.DepartmentName;
                                deptid = dep.DepartmentID;
                            }
                            labGroupInfo.Text = level1 + "--" + level2 + "--" + level3;
                        }
                        
                    }

                    ESP.Administrative.Entity.OperationAuditManageInfo adOperation = new ESP.Administrative.BusinessLogic.OperationAuditManageManager().GetOperationAuditModelByUserID(userid);
                    if (adOperation != null)
                        lblLeader.Text = adOperation.TeamLeaderName;
                    else
                    {
                        ESP.Framework.Entity.OperationAuditManageInfo operaitonDept = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(deptid);
                        lblLeader.Text = operaitonDept.DirectorName;
                    }
                }

                DateTime joinDate = employeeBaseInfo.EmployeeJobInfo.joinDate;
                labJoinDate.Text = labJoinDate1.Text = joinDate.ToString("yyyy 年 MM 月 dd 日");
                labDateTime.Text = DateTime.Now.ToString("yyyy 年 MM 月 dd 日");
                if (!string.IsNullOrEmpty(employeeBaseInfo.Memo))
                {
                    labMemo.Text = "备注：" + employeeBaseInfo.Memo + "<br />";
                }

                labZSze.Text = (nowBasePay + nowMeritPay + nowAttendance).ToString("#,##0.00");
                labZSjb.Text = nowBasePay.ToString("#,##0.00");
                labZSgw.Text = nowMeritPay.ToString("#,##0.00");
                labZSkq.Text = nowAttendance.ToString("#,##0.00");

                decimal discount = 0.8m;
                if (employeeBaseInfo.OfferLetterTemplate == 2)
                    discount = 1;

                labSYze.Text = ((nowBasePay + nowMeritPay + nowAttendance) * discount).ToString("#,##0.00");
                labSYjb.Text = (nowBasePay * discount).ToString("#,##0.00");
                labSYgw.Text = (nowMeritPay * discount).ToString("#,##0.00");
                labSYkq.Text = (nowAttendance * discount).ToString("#,##0.00");

                labMobile.Text = hcCreator.MobilePhone;
                labEmail.Text = hcCreator.Email;
                labAddress.Text = ESP.HumanResource.Common.Status.WorkAddress[employeeBaseInfo.WorkCity];

            }
        }
    }

    protected void btnOKJoin_Click(object sender, EventArgs e)
    {
        Response.Redirect("OfferLetterConfirm.aspx?userid=" + curUserId);
    }
}