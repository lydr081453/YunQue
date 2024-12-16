using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Data;
using System.Web.UI.WebControls;
using ESP.Purchase.Common;
using ESP.Purchase.BusinessLogic;
using ESP.Compatible;
using ESP.Purchase.Entity;

public partial class include_page_ModelTree : ESP.Web.UI.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //tb.InnerHtml = BindTree(int.Parse(ESP.Configuration.ConfigurationManager.SafeAppSettings["TreeRoot"]));
            tb.InnerHtml = BindTree(0);
        }
    }

    /// <summary>
    /// Binds the tree.
    /// </summary>
    /// <param name="parentId">The parent id.</param>
    /// <returns></returns>
    private string BindTree(int parentId)
    {
        StringBuilder strTABContent = new StringBuilder();
        int n = 0;

        DataTable dt = UserPrivilegeManager.GetNavigateTreeDT(this.UserID);
        TreeNode tn = null;
        foreach (DataRow dr in dt.Select("parentId=" + parentId))
        {
            if (dr["link"].ToString().Length == 0)
            {
                strTABContent.Append("<tr>");
                strTABContent.Append("<td height='25' align='left' background='../../images/blue_inside (55).gif' class='font' ");
                strTABContent.Append("style='padding-left: 10px; cursor: pointer' onclick=\"showdiv('div" + n.ToString() + "');\">");
                strTABContent.Append("<img src='../../images/l_ico.gif' width='12' height='8' />&nbsp;" + dr["name"].ToString() + "");
                if (dr["name"].ToString() == "审批中心" || dr["name"].ToString() == "收货中心")
                    strTABContent.Append("<font color='red'>！</font>");
                strTABContent.Append("</td></tr>");
                strTABContent.Append("<tr><td align='left' class='left_menu' style='padding: 5px 0 5px 5px;'>");
                strTABContent.Append("<div id='div" + n.ToString() + "' style=\"display:none\"><table width='100%' border='0' cellspacing='10' cellpadding='0'>");
                strTABContent.Append(BindChildTree(Convert.ToInt32(dr["id"]), dt));
                strTABContent.Append("</table></div></td></tr>");
            }
            else
            {
                continue;
            }

            n++;
        }
        return strTABContent.ToString();
    }

    /// <summary>
    /// Binds the child tree.
    /// </summary>
    /// <param name="parentId">The parent id.</param>
    /// <param name="dt">The dt.</param>
    /// <returns></returns>
    private string BindChildTree(int parentId, DataTable dt)
    {
        StringBuilder strTABContent = new StringBuilder();

        //DataTable dt = UserPrivilegeManager.GetNavigateTreeDT(this.UserID);
        foreach (DataRow dr in dt.Select("parentId=" + parentId))
        {
            if (dr["link"].ToString().EndsWith("/"))
            {
                continue;
            }
            else
            {
                string name = dr["name"].ToString() + "abc";
                switch (name)
                {
                    case "采购申请":
                        {
                            List<SqlParameter> parms = new List<SqlParameter>();
                            string term = " and (status = @status or status = @status1) and PRType not in (@Media,@Private) and requestor=@requestor";
                            parms.Add(new SqlParameter("@status", State.requisition_save));
                            parms.Add(new SqlParameter("@status1", State.requisition_return));
                            parms.Add(new SqlParameter("@requestor", CurrentUser.SysID));
                            parms.Add(new SqlParameter("@Media", (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA));
                            parms.Add(new SqlParameter("@Private", (int)ESP.Purchase.Common.PRTYpe.PR_PriFA));
                            int count = GeneralInfoManager.GetStatusList(term, parms).Count;
                            strTABContent.Append("<tr><td class='left_menu'>");
                            strTABContent.Append("<img src='../../images/ico_001.gif' width='8' height='8' />&nbsp;<a href='" + dr["link"].ToString() + "' target='modify'>" + dr["name"].ToString() + "(" + count.ToString() + ")</a>");
                            strTABContent.Append("</td></tr>");
                            break;
                        }
                    case "采购初审":
                        {
                            List<SqlParameter> parms = new List<SqlParameter>();
                            string term = " and (status = @status or status = @status1) ";
                            parms.Add(new SqlParameter("@status", State.requisition_commit));
                            parms.Add(new SqlParameter("@status1", State.order_return));
                            int count = GeneralInfoManager.GetStatusList(term, parms).Count;
                            strTABContent.Append("<tr><td class='left_menu'>");
                            strTABContent.Append("<img src='../../images/ico_001.gif' width='8' height='8' />&nbsp;<a href='" + dr["link"].ToString() + "' target='modify'>" + dr["name"].ToString() + "(" + count.ToString() + ")</a>");
                            if (count > 0)
                                strTABContent.Append("<font color='red'>！</font>");
                            strTABContent.Append("</td></tr>");
                            break;
                        }
                    case "采购总监审批":
                        {
                            List<SqlParameter> parms = new List<SqlParameter>();
                            string term = string.Empty;
                            term = " and (status = @status )";
                            parms.Add(new SqlParameter("@status", State.order_commit));
                            int count = GeneralInfoManager.GetStatusList(term, parms).Count;
                            strTABContent.Append("<tr><td class='left_menu'>");
                            strTABContent.Append("<img src='../../images/ico_001.gif' width='8' height='8' />&nbsp;<a href='" + dr["link"].ToString() + "' target='modify'>" + dr["name"].ToString() + "(" + count.ToString() + ")</a>");
                            if (count > 0)
                                strTABContent.Append("<font color='red'>！</font>");
                            strTABContent.Append("</td></tr>");
                            break;
                        }
                    case "已审批申请":
                        {
                            List<SqlParameter> parms = new List<SqlParameter>();
                            string term = string.Empty;
                            term = " and (status = @status or status=@statusRecipienting or status = @status_sended  or status = @confirm or status = @recipiented) ";
                            parms.Add(new SqlParameter("@status", State.order_ok));
                            parms.Add(new SqlParameter("@statusRecipienting", State.requisition_recipienting));
                            parms.Add(new SqlParameter("@status_sended", State.order_sended));
                            parms.Add(new SqlParameter("@confirm", State.order_confirm));
                            parms.Add(new SqlParameter("@recipiented", State.requisition_recipiented));
                            int count = GeneralInfoManager.GetStatusList(term, parms).Count;
                            strTABContent.Append("<tr><td class='left_menu'>");
                            strTABContent.Append("<img src='../../images/ico_001.gif' width='8' height='8' />&nbsp;<a href='" + dr["link"].ToString() + "' target='modify'>" + dr["name"].ToString() + "(" + count.ToString() + ")</a>");
                            strTABContent.Append("</td></tr>");
                            break;
                        }
                    case "已提交申请":
                        {
                            List<SqlParameter> parms = new List<SqlParameter>();
                            string term = " and status not in(@status,@status1,@status2)";
                            parms.Add(new SqlParameter("@status", State.requisition_save));
                            parms.Add(new SqlParameter("@status1", State.requisition_return));
                            parms.Add(new SqlParameter("@status2", State.requisition_del));
                            if (ESP.Configuration.ConfigurationManager.SafeAppSettings["AuditorName"].ToString().ToLower() != CurrentUser.ITCode.ToLower())
                            {
                                term += " and (requestor = @requestor or enduser = @requestor or goods_receiver = @requestor or Filiale_Auditor=@requestor)";
                                parms.Add(new SqlParameter("@requestor", CurrentUser.SysID));
                            }
                            int count = GeneralInfoManager.GetStatusList(term, parms).Count;
                            strTABContent.Append("<tr><td class='left_menu'>");
                            strTABContent.Append("<img src='../../images/ico_001.gif' width='8' height='8' />&nbsp;<a href='" + dr["link"].ToString() + "' target='modify'>" + dr["name"].ToString() + "(" + count.ToString() + ")</a>");
                            strTABContent.Append("</td></tr>");
                            break;
                        }
                    case "集团已审核":
                        {
                            List<SqlParameter> parms = new List<SqlParameter>();
                            string term = string.Empty;
                            term = " and (status = @status ) ";
                            parms.Add(new SqlParameter("@status", State.order_commit));
                            int count = GeneralInfoManager.GetStatusList(term, parms).Count;
                            strTABContent.Append("<tr><td class='left_menu'>");
                            strTABContent.Append("<img src='../../images/ico_001.gif' width='8' height='8' />&nbsp;<a href='" + dr["link"].ToString() + "' target='modify'>" + dr["name"].ToString() + "(" + count.ToString() + ")</a>");
                            strTABContent.Append("</td></tr>");
                            break;
                        }
                    case "分公司已审核":
                        {
                            List<SqlParameter> parms = new List<SqlParameter>();
                            string term = string.Empty;
                            term = " and status = @status and Filiale_Auditor > @filiale";
                            parms.Add(new SqlParameter("@status", State.requisition_commit));
                            parms.Add(new SqlParameter("@filiale", '0'));
                            int count = GeneralInfoManager.GetStatusList(term, parms).Count;
                            strTABContent.Append("<tr><td class='left_menu'>");
                            strTABContent.Append("<img src='../../images/ico_001.gif' width='8' height='8' />&nbsp;<a href='" + dr["link"].ToString() + "' target='modify'>" + dr["name"].ToString() + "(" + count.ToString() + ")</a>");
                            strTABContent.Append("</td></tr>");
                            break;
                        }
                    case "分公司审核":
                        {
                            List<SqlParameter> parms = new List<SqlParameter>();
                            string term = " and status = @status ";
                            parms.Add(new SqlParameter("@status", State.requisition_temporary_commit));
                            if (ESP.Configuration.ConfigurationManager.SafeAppSettings["AuditorName"].ToString().ToLower() != CurrentUser.ITCode.ToLower())
                            {
                                term += " and (Filiale_Auditor=@filiale";
                                parms.Add(new SqlParameter("@filiale", CurrentUser.SysID));
                                DataSet ds = FilialeAuditBackUpManager.GetList(" isBackupUser = 1");
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    term += " or Filiale_Auditor in (";
                                    string uid = "";
                                    foreach (DataRow dr1 in ds.Tables[0].Select())
                                    {
                                        uid += dr1["userId"].ToString() + ",";
                                    }
                                    uid = uid.Substring(0, uid.LastIndexOf(","));
                                    term += uid + ") ";
                                }
                                term += ")";
                            }
                            int count = GeneralInfoManager.GetStatusList(term, parms).Count;
                            strTABContent.Append("<tr><td class='left_menu'>");
                            strTABContent.Append("<img src='../../images/ico_001.gif' width='8' height='8' />&nbsp;<a href='" + dr["link"].ToString() + "' target='modify'>" + dr["name"].ToString() + "(" + count.ToString() + ")</a>");
                            if (count > 0)
                                strTABContent.Append("<font color='red'>！</font>");
                            strTABContent.Append("</td></tr>");
                            break;
                        }
                    case "收货":
                        {
                            List<SqlParameter> parms = new List<SqlParameter>();
                            string term = string.Empty;
                            //term = " and status = @status";
                            term = " and ( status = @status or status = @statusRecipienting or  ( status = @statusFlow and requisitionflow = @requisitionflow ) )";
                            //parms.Add(new SqlParameter("@status", State.order_confirm));
                            parms.Add(new SqlParameter("@status", State.order_confirm));
                            parms.Add(new SqlParameter("@statusRecipienting", State.requisition_recipienting));
                            parms.Add(new SqlParameter("@statusFlow", State.order_ok));
                            parms.Add(new SqlParameter("@requisitionflow", State.requisitionflow_toR));
                            term += " and (goods_receiver=@user or requestor=@user or enduser=@user or first_assessor=@user or Filiale_Auditor=@user)";
                            parms.Add(new SqlParameter("@user", CurrentUser.SysID));
                            int count = GeneralInfoManager.GetStatusList(term, parms).Count;
                            strTABContent.Append("<tr><td class='left_menu'>");
                            strTABContent.Append("<img src='../../images/ico_001.gif' width='8' height='8' />&nbsp;<a href='" + dr["link"].ToString() + "' target='modify'>" + dr["name"].ToString() + "(" + count.ToString() + ")</a>");
                            strTABContent.Append("</td></tr>");
                            break;
                        }
                    case "业务审核":
                        {
                            WorkFlowImpl.WorkItemData workitemdata = new WorkFlowImpl.WorkItemData();
                            List<WorkFlowModel.WorkItemData> list1 = workitemdata.getProcessDataList(CurrentUser.SysID, "PR单");
                            List<GeneralInfo> list = new List<GeneralInfo>();
                            List<WorkFlowModel.WorkItemData> list2 = null;
                            //取得授权审核人的数据 begin
                            IList<ESP.Framework.Entity.AuditBackUpInfo> delegates = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(Convert.ToInt32(CurrentUser.SysID));
                            foreach (ESP.Framework.Entity.AuditBackUpInfo backUp in delegates)
                            {
                                list2 = workitemdata.getProcessDataList(backUp.UserID.ToString(), "PR单");
                                foreach (WorkFlowModel.WorkItemData o in list2)
                                {
                                    GeneralInfo model = (GeneralInfo)o.ItemData;
                                    model.WorkitemID = o.WorkItemID;
                                    model.InstanceID = o.InstanceID;
                                    model.WorkItemName = o.WorkItemName;
                                    model.ProcessID = o.ProcessID;

                                    list.Add(model);
                                }
                            }
                            //取得授权审核人的数据 end
                            foreach (WorkFlowModel.WorkItemData o in list1)
                            {
                                GeneralInfo model = (GeneralInfo)o.ItemData;
                                model.WorkitemID = o.WorkItemID;
                                model.InstanceID = o.InstanceID;
                                model.WorkItemName = o.WorkItemName;
                                model.ProcessID = o.ProcessID;

                                list.Add(model);
                            }
                            int count = list.Count;
                            strTABContent.Append("<tr><td class='left_menu'>");
                            strTABContent.Append("<img src='../../images/ico_001.gif' width='8' height='8' />&nbsp;<a href='" + dr["link"].ToString() + "' target='modify'>" + dr["name"].ToString() + "(" + count.ToString() + ")</a>");
                            strTABContent.Append("</td></tr>");
                            break;
                        }
                    case "媒介审批":
                        {
                            List<SqlParameter> parms = new List<SqlParameter>();
                            string term = string.Empty;
                            term = " and (status = @status )";
                            parms.Add(new SqlParameter("@status", State.order_mediaAuditWait));
                            int count = GeneralInfoManager.GetStatusList(term, parms).Count;
                            strTABContent.Append("<tr><td class='left_menu'>");
                            strTABContent.Append("<img src='../../images/ico_001.gif' width='8' height='8' />&nbsp;<a href='" + dr["link"].ToString() + "' target='modify'>" + dr["name"].ToString() + "(" + count.ToString() + ")</a>");
                            strTABContent.Append("</td></tr>");
                            break;
                        }
                    case "广告购买审批":
                        {
                            List<SqlParameter> parms = new List<SqlParameter>();
                            string term = string.Empty;
                            term = " and (status = @status)  and adAuditor=@adAuditor";
                            parms.Add(new SqlParameter("@status", State.order_ADAuditWait));
                            parms.Add(new SqlParameter("@adAuditor", CurrentUserID));
                            int count =  GeneralInfoManager.GetStatusList(term, parms).Count;
                            strTABContent.Append("<tr><td class='left_menu'>");
                            strTABContent.Append("<img src='../../images/ico_001.gif' width='8' height='8' />&nbsp;<a href='" + dr["link"].ToString() + "' target='modify'>" + dr["name"].ToString() + "(" + count.ToString() + ")</a>");
                            strTABContent.Append("</td></tr>");
                            break;
                        }
                    default:
                        {
                            strTABContent.Append("<tr><td class='left_menu'>");
                            strTABContent.Append("<img src='../../images/ico_001.gif' width='8' height='8' />&nbsp;<a href='" + dr["link"].ToString() + "' target='modify'>" + dr["name"].ToString() + "</a>");
                            strTABContent.Append("</td></tr>");
                            break;
                        }
                }
            }
        }
        return strTABContent.ToString();
    }
}