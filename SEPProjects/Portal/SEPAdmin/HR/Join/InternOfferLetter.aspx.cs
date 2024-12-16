using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.HumanResource.Entity;
using ESP.HumanResource.BusinessLogic;
using ESP.Framework.BusinessLogic;
using ESP.Framework.Entity;

public partial class InternOfferLetter : ESP.Web.UI.PageBase
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
                string nowBasePay = "";
                string nowMeritPay = "";
                if (!string.IsNullOrEmpty(Request["nowBasePay"]))
                    nowBasePay = Request["nowBasePay"];
                if (!string.IsNullOrEmpty(Request["nowMeritPay"]))
                    nowMeritPay = Request["nowMeritPay"];

                InitPage(curUserId, nowBasePay, nowMeritPay);
                imgShunya.Src = "http://" + ESP.Framework.BusinessLogic.WebSiteManager.Get(1).UrlPrefix + "/Images/xingyan.png";
                linkOk.HRef = "http://" + ESP.Framework.BusinessLogic.WebSiteManager.Get(1).UrlPrefix + "/Hr/Join/OfferLetterConfirm.aspx?code=" + Server.UrlEncode(ESP.Salary.Utility.DESEncrypt.Encode(curUserId + ""));
            }
        }
    }

    protected void InitPage(int userid, string nowBasePay, string nowMeritPay)
    {
        if (userid > 0)
        {
            ESP.Framework.BusinessLogic.UserManager.Get(userid);
            // 用户信息
            EmployeeBaseInfo employeeBaseInfo = EmployeeBaseManager.GetModel(userid);
            ESP.Framework.Entity.UserInfo usersInfo = UserManager.Get(userid);
            IList<EmployeePositionInfo> depPositionList = DepartmentPositionManager.GetEmployeePositions(userid);

            if (employeeBaseInfo != null)
            {
                labUserName.Text = usersInfo.FullNameCN;
                //labIDNumber.Text = employeeBaseInfo.IDNumber;
                //labFullUserNameEN.Text = employeeBaseInfo.FullNameEN;
                if (depPositionList != null && depPositionList.Count > 0)
                {
                    EmployeePositionInfo empPositionInfo = depPositionList[0];
                    //labPosition.Text = empPositionInfo.DepartmentPositionName;
                    //labGroupInfo.Text = "";
                    List<DepartmentInfo> departmentList = new List<DepartmentInfo>();
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
                            }
                            //labGroupInfo.Text = level1 + "--" + level2 + "--" + level3;
                        }
                    }
                }

                DateTime joinDate = employeeBaseInfo.EmployeeJobInfo.joinDate;
                labJoinDate.Text = joinDate.ToString("yyyy 年 MM 月 dd 日");
                labDateTime.Text = DateTime.Now.ToString("yyyy 年 MM 月 dd 日");
                labNowBasePay.Text = nowBasePay;
                //labNowMeritPay.Text = nowMeritPay;
                if (!string.IsNullOrEmpty(employeeBaseInfo.Memo))
                {
                    labMemo.Text = "备注：" + employeeBaseInfo.Memo + "<br />";
                }
                //if (employeeBaseInfo.OfferLetterTemplate == 2)
                //{
                //    labPreBasePay.Text = "60";
                //    labPreMeritPay.Text = "40";
                //}
                //else
                //{
                //    labPreBasePay.Text = "70";
                //    labPreMeritPay.Text = "30";
                //}
            }
        }
    }

    protected void btnOKJoin_Click(object sender, EventArgs e)
    {
        Response.Redirect("OfferLetterConfirm.aspx?userid=" + curUserId);
    }
}
