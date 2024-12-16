using System;

public partial class Purchase_Requisition_SupplierInfoView : ESP.Web.UI.PageBase
{
    int supplierId = 0;
    /// <summary>
    /// 页面装载
    /// </summary>
    /// <param name="sender">发送者</param>
    /// <param name="e">事件对象</param>
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
                btnBack.Visible = false;
                Page.Master.FindControl("SiteMapPath1").Visible = false;
            }
        }
    }

    /// <summary>
    /// Handles the Click event of the btnBack control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("SupplierInfoList.aspx");
    }
}