using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using ESP.Finance.Utility;

public partial class ForeGift_killForeGiftDetail : ESP.Web.UI.PageBase
{
    int returnId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ReturnID]))
        {
            returnId = int.Parse(Request[ESP.Finance.Utility.RequestName.ReturnID]);
        }
        if (!IsPostBack)
        {
            ESP.Finance.Entity.ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(returnId);
            ViewForeGift.BindInfo(returnModel);
            logBind(returnModel);
        }
    }


    /// <summary>
    /// 绑定已抵押金列表
    /// </summary>
    private void logBind(ESP.Finance.Entity.ReturnInfo returnModel)
    {
        if (returnModel == null)
            returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(returnId);
        string terms = " and foregiftReturnId=@returnId";
        List<SqlParameter> parms = new List<SqlParameter>();
        parms.Add(new SqlParameter("@returnId", returnModel.ReturnID));
        gvLog.DataSource = ESP.Finance.BusinessLogic.ForeGiftLinkManager.GetKillList(terms, parms);
        gvLog.DataBind();
    }

    protected void gvLog_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvLog.PageIndex = e.NewPageIndex;
        logBind(null);
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("killForeGiftList.aspx");
    }
}
