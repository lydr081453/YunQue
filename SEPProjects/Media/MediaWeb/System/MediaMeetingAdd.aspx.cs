﻿using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using ESP.Media.Access.Utilities;
using ESP.Media.Entity;
using ESP.Compatible;
using ESP.Media.Entity;

public partial class System_MediaMeetingAdd : ESP.Web.UI.PageBase
{
   private static MeetingsInfo editmeeting;
    protected void Page_Load(object sender, EventArgs e)
    {
        ListBind();
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request[RequestName.Operate]) && Request[RequestName.Operate] == "edit")
            {
                editmeeting = ESP.Media.BusinessLogic.MeetingsManager.GetModel(Convert.ToInt32(Request[RequestName.meetingID]));
                this.txtCycle.Text = editmeeting.Cycle.ToString();
                this.txtSubject.Text = editmeeting.Subject;
                this.txtRemark.Text = editmeeting.Remark;
            }
        }
    }
    override protected void OnInit(EventArgs e)
    {
        InitDataGridColumn();
        base.OnInit(e);
    }
    #region 绑定列头
    /// <summary>
    /// 初始化表格
    /// </summary>
    private void InitDataGridColumn()
    {
        string strColumn = "Cycle#Subject#Remark#Createdate";
        string strHeader = "期号#标题#备注#上传时间";
        string sort = "Cycle###Createdate";
        string strH = "center###";
        MyControls.GridViewOperate.GridViewHelper.DrawGridView(false, strColumn, strHeader, sort, strH, this.dgList);
        
        TemplateField tf = new TemplateField();
        string clientclick = "";
        MyControls.GridViewOperate.ImageButtonItem itmEdit = new MyControls.GridViewOperate.ImageButtonItem(ESP.Media.Access.Utilities.ConfigManager.EditIconPath, clientclick, "id", true);
        itmEdit.DoSomething += new MyControls.GridViewOperate.DoSomethingHandler(itmEdit_DoSomething);
        tf.ItemTemplate = itmEdit;
        tf.HeaderText = "编辑";
        dgList.Columns.Add(tf);

        TemplateField tfdelete = new TemplateField();
        string clientclick2 = "return confirm('确定要删除该例会信息吗?');";
        MyControls.GridViewOperate.ImageButtonItem itmDelete = new MyControls.GridViewOperate.ImageButtonItem(ESP.Media.Access.Utilities.ConfigManager.DelIconPath, clientclick2, "id", true);
        itmDelete.DoSomething += new MyControls.GridViewOperate.DoSomethingHandler(itmDelete_DoSomething);
        tfdelete.ItemTemplate = itmDelete;
        tfdelete.HeaderText = "删除";
        dgList.Columns.Add(tfdelete);


    }

    void itmDelete_DoSomething(object sender, string value)
    {
        string errmsg=string.Empty;
        if (ESP.Media.BusinessLogic.MeetingsManager.delete(Convert.ToInt32(value)))
        {
            errmsg = "删除成功.";
            ListBind();
        }
        else
            errmsg = "删除失败.";

        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("alert('{0}');", errmsg), true);

    }

    void itmEdit_DoSomething(object sender, string value)
    {
        Response.Redirect("/System/MediaMeetingAdd.aspx?" + RequestName.Operate + "=edit&" + RequestName.meetingID + "=" + value);
    }
    #endregion

    private void ListBind()
    {
        StringBuilder str = new StringBuilder();
        Hashtable ht = new Hashtable();
        DataTable dt = ESP.Media.BusinessLogic.MeetingsManager.GetAllList();
        this.dgList.DataSource = dt.DefaultView;
    }

    protected void dgList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string filename = dgList.DataKeys[e.Row.RowIndex].Value.ToString();
            e.Row.Cells[1].Text = string.Format("<a href='#' onclick=\"window.location='" + filename.Substring(filename.IndexOf('~') + 1) + "'\">{0}</a>", e.Row.Cells[1].Text);
        }
    }

    protected void btnOk_Click(object sender, System.EventArgs e)
    {
        string errmsg=string.Empty;
        int ret=0;
        string logoname = string.Empty;

               if (!string.IsNullOrEmpty(Request[RequestName.Operate]) && Request[RequestName.Operate]=="edit")
               {
                   editmeeting.Cycle = Convert.ToInt32(this.txtCycle.Text);
                   editmeeting.Remark = this.txtRemark.Text;
                   editmeeting.Subject = this.txtSubject.Text;
                   editmeeting.Createdate = DateTime.Now.ToString();
                   editmeeting.Createuserid = Convert.ToInt32(CurrentUser.SysID);

                   if (ESP.Media.BusinessLogic.MeetingsManager.modify(editmeeting, CurrentUserID))
                   {
                       ret = 1;
                       errmsg = "修改成功.";
                   }
                   else
                   {
                       ret = 0;
                       errmsg = "修改失败.";
                   }
                   
               }
               else
               {
                   MeetingsInfo meeting = new MeetingsInfo();
                   meeting.Cycle = Convert.ToInt32(this.txtCycle.Text);
                   meeting.Remark = this.txtRemark.Text;
                   meeting.Subject = this.txtSubject.Text;
                   meeting.Createdate = DateTime.Now.ToString();
                   meeting.Createuserid = Convert.ToInt32(CurrentUser.SysID);
                   //meeting.Path = myFile.FileName;

                   if (fplTitle.HasFile)
                   {
                       byte[] filedata = fplTitle.FileBytes;
                   if (fplTitle.FileName != null && fplTitle.FileName.Length > 0)
                   {
                   ret = ESP.Media.BusinessLogic.MeetingsManager.add(meeting, Server.MapPath(ESP.Media.Access.Utilities.ConfigManager.MediaMeetings + fplTitle.FileName), filedata, Convert.ToInt32(CurrentUser.SysID), out errmsg);
                   }
                   }
               }
               if (ret > 0)
               {
                  
                   //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("alert('{0}');", "上传成功"), true);
                   Response.Redirect("/System/MediaMeetingAdd.aspx?" + RequestName.Operate + "=add");
               }
               else
               {
                   Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("alert('{0}');", errmsg), true);
               }

    }
}