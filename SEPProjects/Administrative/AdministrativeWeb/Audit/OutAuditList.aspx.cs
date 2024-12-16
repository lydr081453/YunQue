using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ESP.Administrative.Common;
using ESP.Administrative.BusinessLogic;
using ESP.HumanResource.BusinessLogic;
using ESP.Administrative.Entity;

namespace AdministrativeWeb.Audit
{
    public partial class OutAuditList : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                drpDate();
                BindInfo();
            }
        }

        protected void drpDate()
        {
            int year = DateTime.Now.Year - 10;
            for (int i = 0; i <= 10; i++)
            {
                drpYear.Items.Insert(i, new ListItem((year + i).ToString(), (year + i).ToString()));
            }

            drpYear.SelectedValue = DateTime.Now.Year.ToString();

            for (int i = 1; i <= 12; i++)
            {
                drpMonth.Items.Insert(i - 1, new ListItem((i).ToString("00"), (i).ToString("00")));
            }

            if (10 < DateTime.Now.Day)
            {
                drpMonth.SelectedValue = DateTime.Now.Month.ToString("00");
            }
            else if (1 != DateTime.Now.Month)
            {
                drpMonth.SelectedValue = (DateTime.Now.Month - 1).ToString("00");
            }
            else
            {
                drpMonth.SelectedValue = (DateTime.Now.Month - 1).ToString("00");
                drpYear.SelectedValue = (DateTime.Now.Year - 1).ToString();
            } 
        }

        protected void BindInfo()
        {
            string year = drpYear.SelectedItem.Value;
            string month = drpMonth.SelectedItem.Value;

            OperationAuditManageManager manager = new OperationAuditManageManager();
            List<OperationAuditManageInfo> list = manager.GetUnderlingsInfo(UserID);
            string userids = "";
            if (list != null && list.Count > 0)
            {
                foreach (OperationAuditManageInfo model in list)
                {
                    userids += model.UserID + ",";
                }
            }
            userids = userids.TrimEnd(',');                

            string str = @" (mattertype={0} or mattertype={6}) and matterstate={1} and deleted='false' and userid in ({2}) and (begintime between '{3}-{4}-01 00:00:00' and '{3}-{4}-{5} 23:59:59') ";
            DataSet dsNoNeed = new MattersManager().GetMatterInfosList(string.Format(str, Status.MattersType_Out, Status.MattersState_NoSubmit, userids, year, month, DateTime.DaysInMonth(int.Parse(year), int.Parse(month)).ToString(), Status.MattersType_Other));
            DataSet dsPassed = new MattersManager().GetMatterInfosList(string.Format(str, Status.MattersType_Out, Status.MattersState_Passed, userids, year, month, DateTime.DaysInMonth(int.Parse(year), int.Parse(month)).ToString(), Status.MattersType_Other));
            GridNoNeed.DataSource = dsNoNeed;
            GridNoNeed.DataBind();

            GridPassed.DataSource = dsPassed;
            GridPassed.DataBind();
        }

        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            BindInfo();
        }

        protected void btnAudit_Click(object sender, EventArgs e)
        {
            string hid = hidMatter.Value;
            List<ESP.Administrative.Entity.MattersInfo> list = new MattersManager().GetMattersList(hid);
            if (list.Count > 0)
            {
                List<ESP.Administrative.Entity.MattersInfo> listmatter = new List<ESP.Administrative.Entity.MattersInfo>();
                foreach (ESP.Administrative.Entity.MattersInfo info in list)
                {
                    info.MatterState = Status.MattersState_Passed;
                    info.UpdateTime = DateTime.Now;
                    info.OperateorID = UserInfo.UserID;
                    listmatter.Add(info);
                }
                if (0 < new MattersManager().Update(listmatter))
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location='OutAuditList.aspx?userid=" + list[0].UserID + "';alert('审批成功！');", true);
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location='OutAuditList.aspx?userid=" + list[0].UserID + "';alert('审批失败！');", true);
                }
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location='OutAuditList.aspx?userid=" + list[0].UserID + "';alert('审批失败！');", true);
            }

        }

        /// <summary>
        /// 获得用户信息
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public string GetUserName(string UserID)
        { 
            return ESP.Framework.BusinessLogic.UserManager.Get(int.Parse(UserID)).FullNameCN;
        }
    }
}
