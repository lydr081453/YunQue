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
using System.Text;
using ESP.Compatible;
using ESP.Media.Entity;

public partial class ShortMsg_SendShortMsg : ESP.Web.UI.PageBase
{
    int shortMsgId = 0;
    int rowNo = 0;
    DataTable dtreport = null;

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request[RequestName.ProjectID] != null && Request[RequestName.ProjectID].Trim().Length > 0)
        {
            hidPJID.Value = Request[RequestName.ProjectID].ToString().Trim();
        }
        if (Request["action"] == "Reporter")
        {
            RepeaterListBind();
        }
        else if (Request["action"] == "Media")
        {
            MediaListBind();
        }
        else if (!string.IsNullOrEmpty(txtMedia.Text) || !string.IsNullOrEmpty(txtReporter.Text))
        {
            SelectReporter();
        }
        else
        {
            ListBind();
        }

        string filename = string.Format("媒体签到表 {0}.xls", DateTime.Now).Replace(':', '.');
        btnReporterSign.Attributes.Add("onclick", string.Format("return btnReporterSign_ClientClick('{0}');", filename));


        filename = string.Format("联络表 {0}.xls", DateTime.Now).Replace(':', '.');
        btnReporterContact.Attributes.Add("onclick", string.Format("return btnReporterContact_ClientClick('{0}');", filename));
    }

    /// <summary>
    /// Raises the <see cref="E:Init"/> event.
    /// </summary>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    override protected void OnInit(EventArgs e)
    {
        InitDataGridColumn();
        base.OnInit(e);
        int userid = CurrentUserID;
    }

    #region 绑定列头
    /// <summary>
    /// 初始化表格
    /// </summary>
    private void InitDataGridColumn()
    {
        if (Request["Evid"] != null && Request["Evid"].Trim().Length > 0)
        {
            string strColumn = "reporterid#reportername#medianame#sex#ReporterPosition#responsibledomain#mobile#tel#email";
            string strHeader = "选择#姓名#所属媒体#性别#职务#负责领域#手机#固话#邮箱";
            string sort = "#ReporterName#medianame#sex#ReporterPosition#responsibledomain#####";
            string strH = "center################";
            MyControls.GridViewOperate.GridViewHelper.DrawGridView(false, strColumn, strHeader, sort, this.dgList);
        }
        else if (Request["Daid"] != null && Request["Daid"].Trim().Length > 0)
        {
            string strColumn = "reporterid#reportername#medianame#sex#ReporterPosition#responsibledomain#mobile#tel#email";
            string strHeader = "选择#姓名#所属媒体#性别#职务#负责领域#手机#固话#邮箱";
            string sort = "#ReporterName#medianame#sex#ReporterPosition#responsibledomain#####";
            string strH = "center################";
            MyControls.GridViewOperate.GridViewHelper.DrawGridView(false, strColumn, strHeader, sort, this.dgList);
        }
        else
        {
            string strColumn = "reporterid#reportername#medianame#sex#ReporterPosition#responsibledomain#mobile#tel#email";
            string strHeader = "选择#姓名#所属媒体#性别#职务#负责领域#手机#固话#邮箱";
            string sort = "#ReporterName#medianame#sex#ReporterPosition#responsibledomain#####";
            string strH = "center################";
            MyControls.GridViewOperate.GridViewHelper.DrawGridView(false, strColumn, strHeader, sort, this.dgList);
        }
    }
    #endregion

    /// <summary>
    /// Gets the work string.
    /// </summary>
    /// <param name="xml">The XML.</param>
    /// <returns></returns>
    private string GetWorkString(string xml)
    {
        xml = Server.HtmlDecode(xml);
        Media_skins_Experience.InitExperienceTable();
        DataTable dt = Media_skins_Experience.ExperienceTable.Clone();
        System.IO.StringReader sr = new System.IO.StringReader(xml);
        dt.ReadXml(sr);
        if (dt.Rows.Count > 0)
            return dt.Rows[0]["单位名称"].ToString();
        else
            return xml;
    }

    #region 绑定列表
    /// <summary>
    /// Lists the bind.
    /// </summary>
    private void ListBind()
    {
        Hashtable ht = new Hashtable();

       // this.dgList.HideColumns(new int[] { 0 });
        
        if (Request["Evid"] != null && Request["Evid"].Trim().Length>0)
        {
            dtreport = ESP.Media.BusinessLogic.EventsManager.GetRelationReporters(Convert.ToInt32(Request["Evid"]), null, null);
        }
        else if (Request["Daid"] != null && Request["Daid"].Trim().Length > 0)
        {
            dtreport = ESP.Media.BusinessLogic.DailysManager.GetRelationReporters(Convert.ToInt32(Request["Daid"]), null, null);
        }
        else if (Request[RequestName.ProjectID] != null && Request[RequestName.ProjectID].Trim().Length > 0 && Request[RequestName.ProjectID].Trim() != "0")
        {
            dtreport = ESP.Media.BusinessLogic.ProjectsManager.GetRelationReporters(Convert.ToInt32(Request[RequestName.ProjectID]), null, null);
        }
        else
        {
            dtreport = ESP.Media.BusinessLogic.ReportersManager.GetList("", ht);
        }
        
            this.dgList.DataSource = dtreport.DefaultView;
            if (dgList.Rows.Count > 0)
            {
                this.btnReporterSign.Visible = true;
                this.btnReporterContact.Visible = true;
            }
            else
            {
                this.btnReporterSign.Visible = false;
                this.btnReporterContact.Visible = false;
            }
        for (int i = 0; i < dgList.Columns.Count; i++)
        {
            dgList.Columns[i].HeaderStyle.Wrap = false;
        }
    }

    /// <summary>
    /// Repeaters the list bind.
    /// </summary>
    private void RepeaterListBind()
    {
        string reporter = Request["ChkID"].ToString();
        hidChkID.Value = reporter.Trim();
        StringBuilder tems = new StringBuilder();
        
        Hashtable ht = new Hashtable();
        string str = " and a.reporterid in (";
        if (reporter != string.Empty)
        {
            string[] strs = reporter.Split(',');
            for (int i = 0; i < strs.Length; i++)
            {
                ht.Add("@" + i + "", int.Parse(strs[i].Trim()));
                str += "@" + i + ",";

            }

        }
        str = str.Substring(0, str.Length - 1);
        str = str + ")";
        tems.Append(str);

        dtreport = ESP.Media.BusinessLogic.ReportersManager.GetList(tems.ToString(), ht);
       
            this.dgList.DataSource = dtreport.DefaultView;
            if (dgList.Rows.Count > 0)
            {
                this.btnReporterSign.Visible = true;
                this.btnReporterContact.Visible = true;
            }
            else
            {
                this.btnReporterSign.Visible = false;
                this.btnReporterContact.Visible = false;
            }
        for (int i = 0; i < dgList.Columns.Count; i++)
        {
            dgList.Columns[i].HeaderStyle.Wrap = false;
        }

    }

    /// <summary>
    /// Media the list bind.
    /// </summary>
    private void MediaListBind()
    {
        string media = Request["MID"].ToString();
        StringBuilder tems = new StringBuilder();
        Hashtable ht = new Hashtable();
        string mediastr = " and Media_ID in (";
        if (media != string.Empty)
        {
            string[] strs = media.Split(',');
            for (int i = 0; i < strs.Length; i++)
            {
                ht.Add("@" + i + "", int.Parse(strs[i].Trim()));
                mediastr += "@" + i + ",";

            }

        }
        mediastr = mediastr.Substring(0, mediastr.Length - 1);
        mediastr = mediastr + ")";
        tems.Append(mediastr);

         dtreport = ESP.Media.BusinessLogic.ReportersManager.GetList(tems.ToString(), ht);

        this.dgList.DataSource = dtreport.DefaultView;
        if (dgList.Rows.Count > 0)
        {
            this.btnReporterSign.Visible = true;
            this.btnReporterContact.Visible = true;
        }
        else
        {
            this.btnReporterSign.Visible = false;
            this.btnReporterContact.Visible = false;
        }
        for (int i = 0; i < dgList.Columns.Count; i++)
        {
            dgList.Columns[i].HeaderStyle.Wrap = false;
        }


    }
    #endregion

    /// <summary>
    /// Handles the RowDataBound event of the dgList control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewRowEventArgs"/> instance containing the event data.</param>
    protected void dgList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = "<input type='checkbox' id='chkHeader' onclick=selectedcheck('Header','Rep'); value='" + e.Row.Cells[0].Text + "' />选择";
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = string.Format("<input type='checkbox' id='chkRep' name='chkRep' value={0} />", e.Row.Cells[0].Text);

        }

    }

    /// <summary>
    /// Handles the Click event of the btnSelect control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnSelect_Click(object sender, EventArgs e)
    {
        Response.Redirect("SendShortMsgList.aspx");
    }

    //搜索记者
    /// <summary>
    /// Handles the Click event of the btnSearch control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        SelectReporter();
    }

    private void SelectReporter()
    {
        string reporter = txtReporter.Text.Trim();
        string media = txtMedia.Text.Trim();
        StringBuilder tems = new StringBuilder();

        Hashtable ht = new Hashtable();

        if (txtReporter.Text.Trim() != string.Empty)
        {
            tems.Append(" and a.reportername like '%'+@reporter+'%'");
            ht.Add("@reporter", txtReporter.Text.Trim());
        }
        if (txtMedia.Text.Trim() != string.Empty)
        {
            tems.Append(" and (media.mediacname like '%'+@media+'%' or media.ChannelName like '%'+@media+'%' or media.TopicName like '%'+@media+'%') ");
            ht.Add("@media", txtMedia.Text.Trim());
        }
        dtreport = ESP.Media.BusinessLogic.ReportersManager.GetList(tems.ToString(), ht);

        this.dgList.DataSource = dtreport.DefaultView;
        if (dgList.Rows.Count > 0)
        {
            this.btnReporterSign.Visible = true;
            this.btnReporterContact.Visible = true;
        }
        else
        {
            this.btnReporterSign.Visible = false;
            this.btnReporterContact.Visible = false;
        }
        for (int i = 0; i < dgList.Columns.Count; i++)
        {
            dgList.Columns[i].HeaderStyle.Wrap = false;
        }
    }

    //发送短消息
    /// <summary>
    /// Handles the Click event of the btnSend control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnSend_Click(object sender, EventArgs e)
    {
        string[] phonenoAll = null;
        if (hidChkID.Value.Equals("selall"))
        {
            phonenoAll = new string[dgList.Rows.Count];
            for (int i = 0; i < dgList.Rows.Count; i++)
            {
                phonenoAll[i] = dgList.Rows[i].Cells[6].Text;
            }
        }
        else
        {
            string[] selvalue = hidChkID.Value.Split(',');
            phonenoAll = new string[selvalue.Length];
            for (int i = 0; i < selvalue.Length; i++)
            {
              //  phonenoAll[i] = dgList.Rows[Convert.ToInt32(selvalue[i])].Cells[6].Text;
                ReportersInfo mr = ESP.Media.BusinessLogic.ReportersManager.GetModel(Convert.ToInt32(selvalue[i]));
                phonenoAll[i] = mr.Usualmobile;
            }
        }
        if (phonenoAll != null && phonenoAll.Length > 0)
        {
            sendphoneno(phonenoAll);
        }
    }

    /// <summary>
    /// Sendphonenoes the specified recvphoneno.
    /// </summary>
    /// <param name="recvphoneno">The recvphoneno.</param>
    void sendphoneno(string[] recvphoneno)
    {
        string errmsg = string.Empty;


        string sus_msg_phoneno = string.Empty;

        string sub = string.Empty;
        string body = string.Empty;
      
        int shortMsgId = Convert.ToInt32(hidShortMsgID.Value);
        if (shortMsgId > 0)
        {
            ShortmsgInfo shortmsg = ESP.Media.BusinessLogic.ShortmsgManager.GetModel(shortMsgId);
             body= shortmsg.Body;
             sub = shortmsg.Subject;
        }

        for (int i = 0; i < recvphoneno.Length; i++)
        {
            if (!recvphoneno[i].Equals("&nbsp;"))
            {
                try
                {
                    //**************发送短消息代码***********
                    int ret = 0;
                    if (Request["Daid"] != null && Request["Daid"].Trim().Length > 0)
                    {
                        ret = ESP.Media.BusinessLogic.ShortmsgsendManager.Add_UpdateDailyStatus(GetObject(recvphoneno[i]), CurrentUserID, Convert.ToInt32(Request["Daid"]), out errmsg);
                    }
                    else if (Request["Evid"] != null && Request["Evid"].Trim().Length > 0)
                    {
                        ret = ESP.Media.BusinessLogic.ShortmsgsendManager.Add_UpdateEventStatus(GetObject(recvphoneno[i]), CurrentUserID, Convert.ToInt32(Request["Evid"]), out errmsg);
                    }
                    else
                    {
                        ret = ESP.Media.BusinessLogic.ShortmsgsendManager.Add(GetObject(recvphoneno[i]), CurrentUserID, out errmsg);
                    }
                    if (ret > 0)
                    {
                        sus_msg_phoneno += recvphoneno[i] + "发送成功并保存，";
                    }
                    else
                    {
                        sus_msg_phoneno += recvphoneno[i] + "发送成功，但保存失败；";
                    }
                }
                catch (Exception ex)
                {
                    sus_msg_phoneno += recvphoneno[i] + "失败原因：" + ex.Message;
                }
            }
            else 
            {
                sus_msg_phoneno = "手机号码不能为空";
            }
        }
        if (recvphoneno.Length < 0)
        {
            sus_msg_phoneno = "请选择手机号码";
        }

        ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("opener.location = opener.location;alert('{0}');window.close();", sus_msg_phoneno), true);

    }

    /// <summary>
    /// Saves the MSG.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void SaveMsg(object sender, EventArgs e)
    {
        string errmeg;

       int ret = ESP.Media.BusinessLogic.ShortmsgManager.Add(GetObject(), CurrentUserID, out errmeg);

    }

    #region 获得对象
    /// <summary>
    /// Gets the object.
    /// </summary>
    /// <param name="Recvphoneno">The recvphoneno.</param>
    /// <returns></returns>
    public ShortmsgsendInfo GetObject(string Recvphoneno)
    {

        ShortmsgsendInfo shortmsgsend = new ShortmsgsendInfo();
        shortmsgsend.Recvphoneno = Recvphoneno;
        shortmsgsend.Senddate = DateTime.Now.ToString();
        shortmsgsend.Senduserid=1;
        shortmsgsend.Recvuserid=1;
        shortmsgsend.Shortmsgid = shortMsgId;
        shortmsgsend.Status = 1;
        shortmsgsend.Recvusertype = 1;
        shortmsgsend.Sendbody= txtBody.Text.Trim();
        shortmsgsend.Sendsubject = txtSubject.Text.Trim(); 
        return shortmsgsend;
    }
    #endregion

    /// <summary>
    /// Adds the status.
    /// </summary>
    private void AddStatus()
    {
        if (Request["Daid"] != null && Request["Daid"].Trim().Length > 0)
        {
            DailysInfo evt = ESP.Media.BusinessLogic.DailysManager.GetModel(Convert.ToInt32(Request["Daid"]));
            if (evt != null)
            {
                int status = evt.Dailystatus > 0 ? evt.Dailystatus + 1 : 0;
                ESP.Media.BusinessLogic.DailysManager.SetStatus(Convert.ToInt32(Request["Daid"]), status, null);
            }
        }
        else if (Request["Evid"] != null && Request["Evid"].Trim().Length > 0)
        {
            ESP.Media.Entity.EventsInfo evt = ESP.Media.BusinessLogic.EventsManager.GetModel(Convert.ToInt32(Request["Evid"]));
            if (evt != null)
            {
                int status = evt.Eventstatus > 0 ? evt.Eventstatus + 1 : 0;
                ESP.Media.BusinessLogic.EventsManager.SetStatus(Convert.ToInt32(Request["Evid"]), status, null);
            }
        }
        Response.Redirect(string.Format("../Project/ProjectMediaReporter.aspx?{0}={1}",RequestName.ProjectID, Request[RequestName.ProjectID]));
    }

    /// <summary>
    /// Gets the object.
    /// </summary>
    /// <returns></returns>
    private ShortmsgInfo GetObject()
    {
        ShortmsgInfo mlShortmsg = null;
        if (Request["Sid"] != null)
        {
            mlShortmsg = ESP.Media.BusinessLogic.ShortmsgManager.GetModel(Convert.ToInt32(Request["Sid"]));
        }
        else
        {
            mlShortmsg = new ShortmsgInfo();
        }
        if (Request["Sid"] != null)
        {
            mlShortmsg.Id = Convert.ToInt32(Request["Sid"]);
        }
        mlShortmsg.Subject = txtSubject.Text.Trim();
        mlShortmsg.Body = txtBody.Text.Trim();
        mlShortmsg.Createdate = mlShortmsg.Createdate == string.Empty ? DateTime.Now.ToString() : mlShortmsg.Createdate;
        mlShortmsg.Createid = mlShortmsg.Createid == 0 ? CurrentUserID : mlShortmsg.Createid;
        mlShortmsg.Senddate = mlShortmsg.Senddate == string.Empty ? DateTime.Now.ToString() : mlShortmsg.Senddate;

        return mlShortmsg;
    }

    /// <summary>
    /// Handles the Click event of the btnReporterSign control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnReporterSign_Click(object sender, EventArgs e)
    {
        string filename = string.Format("媒体签到表 {0}.xls", DateTime.Now).Replace(':', '.');
        filename = Server.MapPath(ESP.Media.Access.Utilities.ConfigManager.BillPath + filename);
        string errmsg = string.Empty;

        //if (ESP.Media.BusinessLogic.ExcelExportManager.SaveSignExcel(Response, dtreport, filename, out errmsg, CurrentUserID))
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("alert('生成媒体签到表{0}');", errmsg), true);
        //}
        //else
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("alert('{0}');", errmsg), true);

        //}
    }

    /// <summary>
    /// Handles the Click event of the btnReporterContact control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnReporterContact_Click(object sender, EventArgs e)
    {
        string filename = string.Format("联络表 {0}.xls", DateTime.Now).Replace(':', '.');
        filename = Server.MapPath(ESP.Media.Access.Utilities.ConfigManager.BillPath + filename);
        string errmsg = string.Empty;

        //if (ESP.Media.BusinessLogic.ExcelExportManager.SaveCommunicateExcel(Response, dtreport, filename, out errmsg, CurrentUserID))
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("alert('生成联络表{0}');", errmsg), true);
        //}
        //else
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("alert('{0}');", errmsg), true);
        //}
    }

    #region 返回总库
    /// <summary>
    /// Handles the OnClick event of the btnClear control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnClear_OnClick(object sender, EventArgs e)
    {
        this.txtReporter.Text = string.Empty;
        ListBind();
    }
    #endregion
}
