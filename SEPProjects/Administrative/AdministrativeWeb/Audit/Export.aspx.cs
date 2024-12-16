using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Administrative.Common;

namespace AdministrativeWeb.Audit
{
    public partial class Export : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                drpDate();
            }
        }

        /// <summary>
        /// 绑定下拉列表中的日期和时间内容
        /// </summary>
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

        /// <summary>
        /// 导出考勤
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExport_Click(object sender, EventArgs e)
        {
            string type = "4";
            string userids = txtUserIds.Text.Trim();
            string content = drpYear.SelectedItem.Text + "|" + drpMonth.SelectedItem.Value;
            FileHelper.ExportTemp(content, type, UserID, Server.MapPath("~"), Response, userids);
        }
    }
}
