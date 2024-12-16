using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class BankInfo_BankList : ESP.Web.UI.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ListBind();
        }
    }

    private void ListBind()
    {
        string terms = "";
        List<SqlParameter> parms = new List<SqlParameter>();
        if (txtBankName.Text.Trim() != "")
        {
            terms += " and bankname like '%'+@bankname+'%'";
            parms.Add(new SqlParameter("@bankname", txtBankName.Text.Trim()));
        }
        if (txtBankAccountName.Text.Trim() != "")
        {
            terms += " and bankaccountname like '%'+@bankaccountname+'%'";
            parms.Add(new SqlParameter("@bankaccountname", txtBankAccountName.Text.Trim()));
        }
        if (txtBranchName.Text.Trim() != "")
        {
            terms += " and branchname like '%'+@branchname+'%'";
            parms.Add(new SqlParameter("@branchname", txtBranchName.Text.Trim()));
        }
        if (txtAddress.Text.Trim() != "")
        {
            terms += " and address like '%'+@address+'%'";
            parms.Add(new SqlParameter("@address", txtAddress.Text.Trim()));
        }
        IList<ESP.Finance.Entity.BankInfo> list = ESP.Finance.BusinessLogic.BankManager.GetList(terms, parms);
        gvList.DataSource = list;
        gvList.DataBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ListBind();
    }

    protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvList.PageIndex = e.NewPageIndex;
        ListBind();
    }

    protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Del")
        {
            int bankId = int.Parse(e.CommandArgument.ToString());
            if (ESP.Finance.BusinessLogic.BankManager.Delete(bankId) == ESP.Finance.Utility.DeleteResult.Succeed)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('删除成功！');", true);
                ListBind();
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('删除失败！');", true);
            }
        }
    }
}
