using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Purchase.Common;
using ESP.Purchase.DataAccess;
using ESP.Purchase.Entity;
using ESP.Compatible;

public partial class Purchase_Message_NewPost : ESP.Web.UI.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ListBind();
        if (!IsPostBack)
        {
            DropDownListBind();
        }

    }
    public void ListBind()
    {
        if (!IsPostBack)
        {
            if (Request["action"] == "edit")
            {
                MessageInfo message = MessageDataProvider.GetModel(int.Parse(Request["id"].ToString()));
                txtBody.Text = message.body.Trim();
                txtSubject.Text = message.subject.Trim();
                drpArea.SelectedValue = message.areaid.ToString();
                labdown.Text = message.attFile == "" ? "" : "<a target='_blank' href='../../" + message.attFile + "'><img src='/images/ico_04.gif' border='0' /></a>";
            }
            if (Request["action"] == "show")
            {

                drpArea.Enabled = false;
                txtBody.ReadOnly = true;
                txtSubject.ReadOnly = true;
                btnSend.Visible = false;
                fil.Visible = false;
            }
            if (Request["isback"] == "1")
            {
                btnBack.Visible = false;
            }
        }
    }

    private void DropDownListBind()
    {

        drpArea.Items.Clear();
        for (int i = 0; i < State.Message_Area.Length; i++)
        {
            drpArea.Items.Insert(i, new ListItem(State.Message_Area[i], i.ToString()));
        }
        drpArea.DataBind();

        
    }
    protected void btnSend_Click(object sender, EventArgs e)
    {
        if (Request["action"] == "edit")
        {
            MessageInfo tm = MessageDataProvider.GetModel(int.Parse(Request["id"].ToString()));
            tm.body = txtBody.Text.Trim();
            tm.subject = txtSubject.Text.Trim();
            tm.createrid = CurrentUserID;
            tm.lasttime = DateTime.Now;
            tm.areaid = int.Parse(drpArea.SelectedValue);

            if (null != fil.PostedFile && fil.PostedFile.FileName != "")
            {
                string fileName = "Message_" + tm.id + "_" + DateTime.Now.Ticks.ToString();
                tm.attFile = upFile(fileName, ESP.Purchase.Common.ServiceURL.UpFilePath);
            }

            MessageDataProvider.Update(tm);

            //记录操作日志
            ESP.Logging.Logger.Add(string.Format("{0}对公告中的id={1}的数据完成 {2} 的操作", CurrentUser.Name, tm.id.ToString(), "新建保存"), "采购公告信息");
        }
        else
        {
            MessageInfo tm = new MessageInfo();
            tm.body = txtBody.Text.Trim();
            tm.subject = txtSubject.Text.Trim();
            tm.createrid = CurrentUserID;
            tm.lasttime = DateTime.Now;
            tm.createtime = DateTime.Now;
            tm.areaid = int.Parse(drpArea.SelectedValue);

            if (null != fil.PostedFile && fil.PostedFile.FileName != "")
            {
                string fileName = "Message_" + tm.id + "_" + DateTime.Now.Ticks.ToString();
                tm.attFile = upFile(fileName, ESP.Purchase.Common.ServiceURL.UpFilePath);
            }

            MessageDataProvider.Add(tm);

            //记录操作日志
            ESP.Logging.Logger.Add(string.Format("{0}对公告中的id={1}的数据完成 {2} 的操作", CurrentUser.Name, tm.id.ToString(), "新建保存"), "采购公告信息");
        }
        Response.Redirect("MessageList.aspx");
    }

    private string upFile(string fileName, string mapPath)
    {
        string savePath = "";
        string extension = System.IO.Path.GetExtension(fil.PostedFile.FileName).ToLower();
        if (!System.IO.Directory.Exists(mapPath + @"upFile\MessageFile"))
        {
            System.IO.Directory.CreateDirectory(mapPath + @"upFile\MessageFile");
        }
        savePath = mapPath + @"upFile\MessageFile\" + fileName + extension;
        fil.PostedFile.SaveAs(savePath);
        return "upFile\\MessageFile\\" + fileName + extension;
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("MessageList.aspx");
    }
}
