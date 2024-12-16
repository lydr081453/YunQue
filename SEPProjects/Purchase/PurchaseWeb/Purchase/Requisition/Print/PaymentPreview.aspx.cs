using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using ESP.Purchase.BusinessLogic;

public partial class Purchase_Requisition_Print_PaymentPreview : ESP.Web.UI.PageBase
{
    int num = 1;
    int listCount = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string where = "";
            if (!string.IsNullOrEmpty(Request["ppid"]))
                where = string.Format(" and a.id={0}", Request["ppid"]);
            else if (!string.IsNullOrEmpty(Request["gid"]))
                where = string.Format(" and a.gid in ({0})", Request["gid"]);

            DataSet ds = PaymentPeriodManager.GetGeneralPaymentList(where);
            listCount = ds.Tables[0].Rows.Count;
            repList.DataSource = ds;
            repList.DataBind();

            ESP.Logging.Logger.Add(string.Format("查看/打印 查询条件为 {0} PN单", where));
        }
        if (!string.IsNullOrEmpty(Request["viewButton"]) && Request["viewButton"] == "no")
        {
            btnClose.Visible = false;
            btnPrint.Visible = false;
        }
    }

    protected void repList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Image logoImg = (Image)e.Item.FindControl("logoImg");
            
            DataRowView dr = (DataRowView)e.Item.DataItem;
            Label labProjCode = (Label)e.Item.FindControl("labProjCode");
            if (!string.IsNullOrEmpty(dr["project_code"].ToString()))
            {
                labProjCode.Text = dr["project_code"].ToString().Substring(0, 1);
            }
            Label labProjectCode = (Label)e.Item.FindControl("labProjectCode");
            labProjectCode.Text = dr["project_code"].ToString();
            Label labRequestorUserName = (Label)e.Item.FindControl("labRequestorUserName");
            labRequestorUserName.Text = dr["requestorname"].ToString();
            Label labRequestorID = (Label)e.Item.FindControl("labRequestorID");
            labRequestorID.Text = new ESP.Compatible.Employee(int.Parse(dr["requestor"].ToString())).ID.ToString();
            Label labDepartment = (Label)e.Item.FindControl("labDepartment");
            labDepartment.Text = dr["Department"].ToString();
            Label labReturnContent = (Label)e.Item.FindControl("labReturnContent");

            labReturnContent.Text = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetPNDes(int.Parse(dr["gid"].ToString()));
            Label labPreFee = (Label)e.Item.FindControl("labPreFee");
            labPreFee.Text = dr["inceptPrice"] == DBNull.Value ? "" : decimal.Parse(dr["inceptPrice"].ToString()).ToString("#,##0.00");
            Label labOrderid = (Label)e.Item.FindControl("labOrderid");
            labOrderid.Text = dr["orderid"].ToString();

            Label lab_TotalPrice = (Label)e.Item.FindControl("lab_TotalPrice");
            lab_TotalPrice.Text = dr["inceptPrice"] == DBNull.Value ? "" : decimal.Parse(dr["inceptPrice"].ToString()).ToString("#,##0.00");
            Label labAccountName = (Label)e.Item.FindControl("labAccountName");
            labAccountName.Text = dr["account_name"].ToString();
            Label labAccountBankName = (Label)e.Item.FindControl("labAccountBankName");
            labAccountBankName.Text = dr["account_bank"].ToString();
            Label labAccountBankNo = (Label)e.Item.FindControl("labAccountBankNo");
            labAccountBankNo.Text = dr["account_number"].ToString();
            Label lblPN = (Label)e.Item.FindControl("lblPN");
            lblPN.Text = dr["returnCode"].ToString();
            Label lblAuditList = (Label)e.Item.FindControl("lblAuditList");
            if (dr["returnID"] != System.DBNull.Value)
            {
                ESP.Finance.Entity.ReturnInfo ReturnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(Convert.ToInt32(dr["returnID"]));
                IList<ESP.Finance.Entity.AuditLogInfo> histList = ESP.Finance.BusinessLogic.AuditLogManager.GetReturnList( ReturnModel.ReturnID);
                ESP.Finance.Entity.BranchInfo branchModel = ESP.Finance.BusinessLogic.BranchManager.GetModelByCode(ReturnModel.ProjectCode.Substring(0, 1));
                logoImg.ImageUrl = "/images/" + branchModel.Logo;

                string rethist = string.Empty;
                string auditDate = string.Empty;
                foreach (ESP.Finance.Entity.AuditLogInfo item in histList)
                {
                    auditDate = item.AuditDate == null ? "" : item.AuditDate.Value.ToString("yyyy-MM-dd HH:mm:ss");

                    if (string.IsNullOrEmpty(item.Suggestion))
                    {
                        if (item.AuditStatus == (int)ESP.Finance.Utility.AuditHistoryStatus.PassAuditing)
                        {
                            item.Suggestion = "审批通过";
                        }
                        else
                        {
                            item.Suggestion = "审批驳回";
                        }
                    }
                    rethist += "审批人:  " + item.AuditorEmployeeName + "  [" + auditDate + "]  " + item.Suggestion + "<br/>";
                }
                lblAuditList.Text = rethist;
            }
            if (listCount > 1 && e.Item.ItemIndex != (listCount - 1))
            {
                Label labafter = (Label)e.Item.FindControl("labafter");
                labafter.Text = "<p style=\"page-break-after:always\">&nbsp;</p>";
            }
        }
    }

}
