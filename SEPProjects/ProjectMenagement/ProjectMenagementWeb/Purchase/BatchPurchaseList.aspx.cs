using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using ESP.Framework.BusinessLogic;

public partial class Purchase_BatchPurchaseList : ESP.Web.UI.PageBase
{
    private Dictionary<int, string> UserNames;
   
   // private int AuditorId;
   // private int AuditorId2;

    private static void Log(int id) { Log(id, ""); }
    private static void Log(int id, string s)
    {
        int uid = UserManager.GetCurrentUserID();
        using (System.IO.StreamWriter log = new System.IO.StreamWriter(HttpRuntime.AppDomainAppPath + "\\timelog.txt", true))
        {
            log.WriteLine("{0:000000} {1:000} {2:hh:mm:ss:ffffff} {3}", uid, id, DateTime.Now, s); 
            log.Flush();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Purchase_BatchPurchaseList));
        this.ddlBranch.Attributes.Add("onchange", "selectBranch(this.options[this.selectedIndex].value,this.options[this.selectedIndex].text);");
        if (!Page.IsPostBack)
        {
           // CurrentUserId = UserManager.GetCurrentUserID();

           // int.TryParse(ESP.Configuration.ConfigurationManager.SafeAppSettings["AuditorId"], out AuditorId);
           // int.TryParse(ESP.Configuration.ConfigurationManager.SafeAppSettings["AuditorId2"], out AuditorId2);

            SearchWait();
           // SearchAuditing();
            //SearchFinance();
            //SearchComplete();
        }
    }

    /// <summary>
    /// 待审批
    /// </summary>
    private void SearchWait()
    {
        List<SqlParameter> paramlist = new List<SqlParameter>();
        string term = string.Empty;
        string DelegateUsers = string.Empty;
        IList<ESP.Framework.Entity.AuditBackUpInfo> delegates = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(CurrentUserID);
        foreach (ESP.Framework.Entity.AuditBackUpInfo model in delegates)
        {
            DelegateUsers += model.UserID.ToString() + ",";
        }
        DelegateUsers = DelegateUsers.TrimEnd(',');
        term = " (Status=@status1 or  Status=@status2 or Status is null) and BatchType in(1,3) ";
        SqlParameter p1 = new SqlParameter("@status1", System.Data.SqlDbType.Int, 4);
        p1.SqlValue = (int)PaymentStatus.PurchaseFirst;
        paramlist.Add(p1);
        SqlParameter p2 = new SqlParameter("@status2", System.Data.SqlDbType.Int, 4);
        p2.SqlValue = (int)PaymentStatus.PurchaseMajor1;
        paramlist.Add(p2);

        //if (CurrentUserId != AuditorId)
        //{
            if (!string.IsNullOrEmpty(DelegateUsers))
                term += " AND (PaymentUserID=@sysID or PaymentUserID in(" + DelegateUsers + ")) ";
            else
                term += " AND PaymentUserID=@sysID  and BatchType in(1,3) ";

            SqlParameter p6 = new SqlParameter("@sysID", System.Data.SqlDbType.Int, 4);
            p6.SqlValue = CurrentUserID;
            paramlist.Add(p6);
        //}
        //else
        //{
        //    term += " and Status<>@status1 and Status is not null ";
        //}
        if (!string.IsNullOrEmpty(term))
        {
            if (this.txtKey.Text.Trim() != string.Empty)
            {
                term += "  and ( amounts like '%'+@prno+'%' or suppliername like '%'+@prno+'%' or batchcode like '%'+@prno+'%' or purchasebatchcode like '%'+@prno+'%')";
                SqlParameter sp1 = new SqlParameter("@prno", System.Data.SqlDbType.NVarChar, 50);
                sp1.SqlValue = this.txtKey.Text.Trim();
                paramlist.Add(sp1);

            }
            if (!string.IsNullOrEmpty(this.hidBranchID.Value) && !string.IsNullOrEmpty(this.hidBranchName.Value))
            {
                term += " and Branchcode = @BranchCode ";
                System.Data.SqlClient.SqlParameter pBrach = new System.Data.SqlClient.SqlParameter("@BranchCode", System.Data.SqlDbType.NVarChar, 50);
                pBrach.SqlValue = this.hidBranchName.Value;
                paramlist.Add(pBrach);
            }
            if (!string.IsNullOrEmpty(txtBeginDate.Text.Trim()) && !string.IsNullOrEmpty(txtEndDate.Text.Trim()))
            {
                if (Convert.ToDateTime(txtBeginDate.Text) <= Convert.ToDateTime(txtEndDate.Text))
                {
                    term += " and LastUpdateDateTime between @beginDate and @endDate";
                    System.Data.SqlClient.SqlParameter sp3 = new System.Data.SqlClient.SqlParameter("@beginDate", System.Data.SqlDbType.DateTime, 8);
                    sp3.SqlValue = this.txtBeginDate.Text;
                    paramlist.Add(sp3);
                    System.Data.SqlClient.SqlParameter sp4 = new System.Data.SqlClient.SqlParameter("@endDate", System.Data.SqlDbType.DateTime, 8);
                    sp4.SqlValue = this.txtEndDate.Text;
                    paramlist.Add(sp4);

                }
            }
            IList<ESP.Finance.Entity.PNBatchInfo> returnList = ESP.Finance.BusinessLogic.PNBatchManager.GetList(term, paramlist);
            var tmplist = returnList.OrderBy(N => N.PaymentDate);
            IList<ESP.Finance.Entity.PNBatchInfo> returnlist = tmplist.ToList();
            grWait.DataSource = returnlist;
            grWait.DataBind();

            decimal total = 0;
            string batchIds = "";
            foreach (ESP.Finance.Entity.PNBatchInfo model in returnlist)
            {
                total += model.Amounts == null ? 0 : model.Amounts.Value;
                batchIds += model.BatchID + ",";
            }
            litPZ1.Text = total.ToString("#,##0.00");
            litPN1.Text = batchIds.Length == 0 ? "0" : ESP.Finance.BusinessLogic.PNBatchRelationManager.GetList(" batchId in (" + batchIds.TrimEnd(',') + ")", new List<SqlParameter>()).Count.ToString();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        SearchWait();
    }

    protected void btnSearchAll_Click(object sender, EventArgs e)
    {
        this.txtKey.Text = string.Empty;
        this.hidBranchID.Value = string.Empty;
        this.hidBranchName.Value = string.Empty;
        this.txtBeginDate.Text = string.Empty;
        this.txtEndDate.Text = string.Empty;
        SearchWait();
    }

    public string getUser(int userId)
    {
        ESP.Compatible.Employee emp = new ESP.Compatible.Employee(userId);
        return "<a style='cursor:pointer;color:Black' onclick=\"ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(userId) + "');\">" + emp.Name + "</a>";
    }

    [AjaxPro.AjaxMethod]
    public static List<List<string>> GetBranchList()
    {
        IList<ESP.Finance.Entity.BranchInfo> blist = ESP.Finance.BusinessLogic.BranchManager.GetList(null, null);
        List<List<string>> list = new List<List<string>>();
        List<string> item = null;
        foreach (ESP.Finance.Entity.BranchInfo branch in blist)
        {
            item = new List<string>();
            item.Add(branch.BranchID.ToString());
            item.Add(branch.BranchCode);
            list.Add(item);
        }
        List<string> c = new List<string>();
        c.Add("-1");
        c.Add("请选择...");
        list.Insert(0, c);
        return list;
    }


  
}
