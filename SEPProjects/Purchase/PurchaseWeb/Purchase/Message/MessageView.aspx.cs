using System;
using ESP.Purchase.Common;


public partial class Purchase_Message_MessageView : ESP.Web.UI.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ListBind();

    }
    public void ListBind()
    {
        if (!IsPostBack)
        {            
            if (Request["action"] == "show")
            {
                ESP.Purchase.Entity.MessageInfo message = ESP.Purchase.DataAccess.MessageDataProvider.GetModel(int.Parse(Request["id"].ToString()));
                labBody.Text = message.body.Trim();
                labSubject.Text = message.subject.Trim();
                labArea.Text = State.Message_Area[message.areaid];
                labdown.Text = message.attFile == "" ? "" : "<a target='_blank' href='../../" + message.attFile + "'><img src='/images/ico_04.gif' border='0' /></a>";
            }
            if (Request["isback"] == "1")
            {
                btnBack.Visible = false;
            }
        }
    }
    
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("MessageList.aspx");
    }
}
