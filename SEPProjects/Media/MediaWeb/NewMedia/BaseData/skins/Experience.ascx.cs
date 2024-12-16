using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace MediaWeb.NewMedia.BaseData.skins
{
    public partial class Experience : System.Web.UI.UserControl
    {
        private static DataTable experienceTable = null;



        public static DataTable ExperienceTable
        {
            get
            {
                if (experienceTable == null)
                    InitExperienceTable();
                return experienceTable;
            }
        }

        public static string ExperienceXml
        {
            get
            {
                System.IO.StringWriter sw = new System.IO.StringWriter();
                ExperienceTable.WriteXml(sw);
                return sw.ToString();
            }
        }

        public static void InitExperienceTable()
        {
            experienceTable = new DataTable("Experience");
            experienceTable.Columns.Add("单位类型");
            experienceTable.Columns.Add("单位名称");
            experienceTable.Columns.Add("职位");
            experienceTable.Columns.Add("工作起始时间");
            experienceTable.Columns.Add("工作截止时间");
            experienceTable.Columns.Add("专兼职");
            experienceTable.Columns.Add("单位所属行业");
            experienceTable.Columns.Add("单位描述");
        }

        public void SetProperties(int index)
        {
            if (index >= ExperienceTable.Rows.Count | index < 0)
                return;
            DataRow row = ExperienceTable.Rows[index];
            string worktype = row["单位类型"].ToString().Trim();
            if (worktype == "是媒体")
                worktype = "媒体";
            ddlType.Text = worktype;
            txtUnitName.Text = row["单位名称"].ToString().Trim();
            txtDuty.Text = row["职位"].ToString().Trim();
            dpWorkBeginTime.Text = row["工作起始时间"].ToString().Split(' ')[0];
            if (dpWorkBeginTime.Text.Equals("1900-1-1"))
            {
                dpWorkBeginTime.Text = "";
            }
            dpWorkEndTime.Text = row["工作截止时间"].ToString().Split(' ')[0];
            if (dpWorkEndTime.Text.Equals("1900-1-1"))
            {
                dpWorkEndTime.Text = "";
            }
            ddlAllTime.Text = row["专兼职"].ToString().Trim();
            txtUnitNature.Text = row["单位所属行业"].ToString().Trim();
            txtUnitDescription.Text = row["单位描述"].ToString().Trim();
        }

        public int UpdateProperties()
        {
            string msg = string.Empty;
            if (txtUnitName.Text.Trim() == string.Empty)
            {
                msg += "单位名称 ";
            }
            if (txtDuty.Text.Trim() == string.Empty)
            {
                msg += "职位 ";
            }
            if (msg.Length > 0)
            {
                msg = "请填写" + msg;
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("alert('{0}');", msg), true);
                return -1;
            }//添加必填项判断
            DataRow row = ExperienceTable.NewRow();
            string worktype = ddlType.Text.Trim();
            if (worktype == "媒体")
                worktype = "是媒体";
            row["单位类型"] = worktype;
            row["单位名称"] = txtUnitName.Text.Trim();
            row["职位"] = txtDuty.Text.Trim();
            row["工作起始时间"] = dpWorkBeginTime.Text.Trim().Split(' ')[0];
            if (row["工作起始时间"].Equals("1900-1-1"))
            {
                row["工作起始时间"] = "";
            }
            row["工作截止时间"] = dpWorkEndTime.Text.Trim().Split(' ')[0];
            if (row["工作截止时间"].Equals("1900-1-1"))
            {
                row["工作截止时间"] = "";
            }
            row["专兼职"] = ddlAllTime.Text.Trim();
            row["单位所属行业"] = txtUnitNature.Text.Trim();
            row["单位描述"] = txtUnitDescription.Text.Trim();
            ExperienceTable.Rows.Add(row);
            return 0;
        }
    }
}