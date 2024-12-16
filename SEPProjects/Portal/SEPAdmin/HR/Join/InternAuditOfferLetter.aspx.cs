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

public partial class InternAuditOfferLetter : ESP.Web.UI.PageBase
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
                if (!string.IsNullOrEmpty(Request["nowBasePay"]))
                    nowBasePay = Request["nowBasePay"];
                InitPage(curUserId, nowBasePay);
                imgShunya.Src = "http://" + ESP.Framework.BusinessLogic.WebSiteManager.Get(1).UrlPrefix + "/Images/xingyan.png";
            }
        }
    }

    protected void InitPage(int userid, string nowBasePay)
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
                if (depPositionList != null && depPositionList.Count > 0)
                {
                    EmployeePositionInfo empPositionInfo = depPositionList[0];
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
                        }
                    }
                }

                DateTime joinDate = employeeBaseInfo.EmployeeJobInfo.joinDate;
                labJoinDate.Text = joinDate.ToString("yyyy 年 MM 月 dd 日");
                labDateTime.Text = DateTime.Now.ToString("yyyy 年 MM 月 dd 日");
                if (Request["showSalary"] == "1")
                {
                    labNowBasePay.Text = nowBasePay;
                }
                //labNowMeritPay.Text = nowMeritPay;
                if (!string.IsNullOrEmpty(employeeBaseInfo.Memo))
                {
                    labMemo.Text = "备注：" + employeeBaseInfo.Memo + "<br />";
                }
            }
        }
    }
}
