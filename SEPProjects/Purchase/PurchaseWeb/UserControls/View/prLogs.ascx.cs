using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;
using System.Data;
using System.Data.SqlClient;

namespace PurchaseWeb.UserControls.View
{
    public partial class prLogs : System.Web.UI.UserControl
    {
        int generalid = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request[RequestName.GeneralID]))
            {
                generalid = int.Parse(Request[RequestName.GeneralID]);
            }
            if (!IsPostBack)
            {
                repLog.DataSource = GetLogTable();
                repLog.DataBind();
            }
        }

        private DataView GetLogTable()
        {
            GeneralInfo g = GeneralInfoManager.GetModel(generalid);
            string strwhere = string.Format(@"  and gid={0}", generalid);
            List<LogInfo> list = LogManager.GetLoglist(strwhere, new List<SqlParameter>());

            DataTable dt = new DataTable();
            DataColumn dc1 = new DataColumn("Des");
            DataColumn dc2 = new DataColumn("Time", typeof(DateTime));
            dt.Columns.Add(dc1);
            dt.Columns.Add(dc2);

            DataTable auditLogs = AuditLogManager.GetauditLog(generalid, 2);
            if (auditLogs != null)
            {
                foreach (DataRow auditLog in auditLogs.Rows)
                {
                    DataRow dr = dt.NewRow();
                    dr["Des"] = auditLog["auditUserName"] + State.operationAudit_statusName[int.Parse(auditLog["auditType"].ToString())] + " " + auditLog["remarkDate"].ToString() + " " + auditLog["remark"].ToString();
                    if (g.PrNo != auditLog["prno"].ToString().Trim())
                    {
                        dr["Des"] += "  单号变更[<font color='Red'>" + auditLog["prno"] + "</font>]";
                    }
                    dr["Time"] = auditLog["remarkDate"];
                    dt.Rows.Add(dr);
                }
            }
            for (int i = 0; i < list.Count; i++)
            {
                bool isAdd = true;
                if (auditLogs != null)
                {
                    foreach (DataRow auditLog in auditLogs.Rows)
                    {
                        if (list[i].LogUserId == int.Parse(auditLog["audituserid"].ToString()) && list[i].LogMedifiedTeme.ToString("yyyy-MM-dd hh:mm:ss") == DateTime.Parse(auditLog["remarkDate"].ToString()).ToString("yyyy-MM-dd hh:mm:ss"))
                        {
                            isAdd = false;
                        }
                    }
                }
                if (isAdd)
                {
                    DataRow dr = dt.NewRow();
                    dr["Des"] = list[i].Des;
                    dr["Time"] = list[i].LogMedifiedTeme;
                    dt.Rows.Add(dr);
                }
            }

            DataView dv = dt.DefaultView;
            dv.Sort = "Time ASC";
            return dv;
        }
    }
}