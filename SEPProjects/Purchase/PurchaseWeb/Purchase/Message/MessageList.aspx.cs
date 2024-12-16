using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using ESP.Purchase.DataAccess;
using ESP.Purchase.Entity;

public partial class Purchase_Message_MessageList : ESP.Web.UI.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ListBind();
        }
    }
    protected void gv_RowDataBound(object source, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
            if (e.Row.Cells[1].Text.Length > 50)
            {
                e.Row.Cells[1].Text = e.Row.Cells[1].Text.Substring(0, 50) + "...";
            }
            if (e.Row.Cells[2].Text.Length > 50)
            {
                e.Row.Cells[2].Text = e.Row.Cells[2].Text.Substring(0, 50) + "...";
            }
        }
       
    }
    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int id = int.Parse(e.CommandArgument.ToString());

        if (e.CommandName == "Del")
        {
            MessageDataProvider.Delete(id);

            //记录操作日志
            ESP.Logging.Logger.Add(string.Format("{0}对公告中的id={1}的数据完成 {2} 的操作", CurrentUser.Name, id.ToString(), "删除"), "采购公告信息");

            ListBind();
        }
        
    }
    private void ListBind()
    {
        List<MessageInfo> list = MessageDataProvider.GetList();
        gv.DataSource = list;
        gv.DataBind();
    }

}
