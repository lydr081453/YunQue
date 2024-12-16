using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Administrative.BusinessLogic;
using ESP.Administrative.Entity;
using ESP.Framework.Entity;

namespace AdministrativeWeb.Attendance
{
    public partial class AddedClockInList : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request["clockinid"] != null && Request["flag"] != null)
                {
                    if (Request["flag"] == "1") 
                        DeletedClockIn(int.Parse(Request["clockinid"]));
                }
                BindInfo();
            }
        }

        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            BindInfo();
        }

        /// <summary>
        /// 查询当前用户所添加的打卡记录信息
        /// </summary>
        protected void BindInfo()
        {
            string dt = DateTime.Now.AddMonths(-3).ToString("yyyy-MM-dd");

            ClockInManager clockinManager = new ClockInManager();
            string term = " deleted=0 and readtime>'" + dt + "' ";
            string hrIds = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetHRId();
            if (hrIds.IndexOf(CurrentUserID.ToString()) < 0)
                term += " and  operatorid='" + UserID + "'";
            DataSet ds = clockinManager.GetList(term);
            Grid1.DataSource = ds;
            Grid1.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("/attendance/AddedClockInEdit.aspx");
        }
        
        /// <summary>
        /// 删除考勤打卡记录信息
        /// </summary>
        protected void DeletedClockIn(int id)
        {
            ClockInManager clockinManager = new ClockInManager();
            clockinManager.Delete(id);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "btnSave", "alert('数据删除成功！');", true);
        }

        /// <summary>
        /// 获得员工姓名
        /// </summary>
        /// <param name="userCode">员工编号</param>
        /// <returns>员工姓名</returns>
        protected string GetUserName(string userCode)
        {
            string username = "";
            if (!string.IsNullOrEmpty(userCode))
            {
                ClockInManager clockinManager = new ClockInManager();
                EmployeeInfo employeeModel = clockinManager.GetEmployeeInfoByCode(userCode);
                // ESP.Framework.BusinessLogic.EmployeeManager.GetByCode(userCode);
                if (employeeModel != null && !string.IsNullOrEmpty(employeeModel.FullNameCN))
                {
                    username = employeeModel.FullNameCN;
                }
            }
            return username;
        }

        protected string GetInOrOut(string inorout)
        {
            if (inorout == "True")
                return "上班时间";
            else
                return "下班时间";
        }

        /// <summary>
        /// 获得删除操作的URL
        /// </summary>
        /// <param name="formID">编号</param>
        /// <returns>返回URL</returns>
        protected string GetDeleteUrl(int formID)
        {
            string url = "";
            url = "<a href=\"#\" onclick=\"return DeleteInfo(this," + formID + ", 1);\"><img src=\"../images/disable.gif\" /></a>";
            return url;
        }
    }
}
