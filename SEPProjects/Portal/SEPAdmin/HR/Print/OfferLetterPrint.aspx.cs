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

public partial class OfferLetterPrint : ESP.Web.UI.PageBase
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
            int deptid = 0;
            if (employeeBaseInfo != null)
            {
                labUserName1.Text = labUserName2.Text = usersInfo.FullNameCN;
                labIDNumber1.Text = labIDNumber2.Text = employeeBaseInfo.IDNumber;
                labFullUserNameEN1.Text = labFullUserNameEN2.Text = usersInfo.FullNameEN;
                if (depPositionList != null && depPositionList.Count > 0)
                {
                    EmployeePositionInfo empPositionInfo = depPositionList[0];
                    labPosition.Text = empPositionInfo.DepartmentPositionName;
                    labGroupInfo.Text = "";
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
                                deptid = dep.DepartmentID;
                            }
                            if (dep.DepartmentLevel == 2)
                            {
                                level2 = dep.DepartmentName;
                            }
                            if (dep.DepartmentLevel == 3)
                            {
                                level3 = dep.DepartmentName;
                            }
                            labGroupInfo.Text = level1 + "--" + level2 + "--" + level3;
                        }

                       
                    }
                }

                DateTime joinDate = employeeBaseInfo.EmployeeJobInfo.joinDate;
                labJoinDate.Text = joinDate.ToString("yyyy 年 MM 月 dd 日");
                labDateTime.Text = DateTime.Now.ToString("yyyy 年 MM 月 dd 日");
                labNowBasePay.Text = string.IsNullOrEmpty(nowBasePay) ? "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" : nowBasePay;
                labNowMeritPay.Text = string.IsNullOrEmpty(nowMeritPay) ? "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" : nowMeritPay;
                if (!string.IsNullOrEmpty(employeeBaseInfo.Memo))
                {
                    labMemo1.Text = labMemo2.Text = "备注：" + employeeBaseInfo.Memo + "<br />";
                }
                if (employeeBaseInfo.OfferLetterTemplate == 3)
                {
                    DefaultOfferLetter.Visible = false;
                    InternOfferLetter.Visible = true;
                }
                else
                {
                    DefaultOfferLetter.Visible = true;
                    InternOfferLetter.Visible = false;
                    if (employeeBaseInfo.OfferLetterTemplate == 2 || employeeBaseInfo.OfferLetterTemplate == 6)
                    {
                        labPreBasePay.Text = "60";
                        labPreMeritPay.Text = "40";
                    }
                    else
                    {
                        labPreBasePay.Text = "70";
                        labPreMeritPay.Text = "30";
                    }
                }
            }
        }
    }
}
