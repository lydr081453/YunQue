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

public partial class Purchase_Requisition_ProposedSupplierView :ESP.Web.UI.PageBase
{
    int supplierId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["supplierId"]))
        {
            supplierId = int.Parse(Request["supplierId"]);
        }
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request["isback"]) && Request["isback"] != "0")
            {
                this.btnBack.Visible = false;
                //SiteMapPath1
                Page.Master.FindControl("SiteMapPath1").Visible = false;
            }
        }
        //记录操作日志
        ESP.Logging.Logger.Add(string.Format("查看id为{0}的推荐供应商信息",supplierId), "页面访问");
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("SupplierInfoList.aspx");
    }
}
