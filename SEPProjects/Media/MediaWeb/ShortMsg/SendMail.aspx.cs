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
using System.Net.Mail;
using System.Net;
using ESP.Compatible;
using ESP.Media.Entity;
using ESP.Media.BusinessLogic;
public partial class ShortMsg_SendMail : ESP.Web.UI.PageBase
{
    int rowsNo = 0;
    int emailId = 0;
    string annexpath = string.Empty;
    DataTable dtreport = new DataTable();

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        
       
            this.updateAnnex.Visible = false;
           
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
                else if (Request["action"] == "Select")
                {
                    MailBind();
                    ListBind();
                }
                else if (!string.IsNullOrEmpty(txtMedia.Text) || !string.IsNullOrEmpty(txtReporter.Text))
                {
                    SelectReporter();
                }
                else
                {
                    ListBind();
                }
            
        

     //   string filename = string.Format("媒体签到表 {0}.xls", DateTime.Now).Replace(':', '.');
        string filename = "1";
        btnReporterSign.Attributes.Add("onclick", string.Format("return btnReporterSign_ClientClick('{0}');", filename));


     //   filename = string.Format("联络表 {0}.xls", DateTime.Now).Replace(':', '.');
        filename = "1";
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
            string strH = "center########";
            MyControls.GridViewOperate.GridViewHelper.DrawGridView(false, strColumn, strHeader, sort,strH, this.dgList);
        }
        else if (Request["Daid"] != null && Request["Daid"].Trim().Length > 0)
        {
            string strColumn = "reporterid#reportername#medianame#sex#ReporterPosition#responsibledomain#mobile#tel#email";
            string strHeader = "选择#姓名#所属媒体#性别#职务#负责领域#手机#固话#邮箱";
            string sort = "#ReporterName#medianame#sex#ReporterPosition#responsibledomain#####";
            string strH = "center########";
            MyControls.GridViewOperate.GridViewHelper.DrawGridView(false, strColumn, strHeader, sort,strH, this.dgList);
        }
        else
        {
            string strColumn = "reporterid#reportername#medianame#sex#ReporterPosition#responsibledomain#mobile#tel#email";
            string strHeader = "选择#姓名#所属媒体#性别#职务#负责领域#手机#固话#邮箱";
            string sort = "#ReporterName#medianame#sex#ReporterPosition#responsibledomain#####";
            string strH = "center########";
            MyControls.GridViewOperate.GridViewHelper.DrawGridView(false, strColumn, strHeader, sort,strH, this.dgList);
        }
    }
    #endregion

    #region 绑定列表
    /// <summary>
    /// Lists the bind.
    /// </summary>
    private void ListBind()
    {
        Hashtable ht = new Hashtable();

      //  this.dgList.HideColumns(new int[] { 0 });      

        

        if (Request["Evid"] != null && Request["Evid"].Trim().Length>0)
        {
            dtreport = ESP.Media.BusinessLogic.EventsManager.GetRelationReporters(Convert.ToInt32(Request["Evid"]), null,null);
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
                ht.Add("@"+i+"",int.Parse(strs[i].Trim()));
                str += "@" + i + ",";
            
            }
           
         }
        str = str.Substring(0,str.Length -1);
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

         dtreport = ESP.Media.BusinessLogic.ReportersManager.GetList(tems.ToString(),ht);

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

    //绑定邮件内容
    /// <summary>
    /// Mails the bind.
    /// </summary>
    private void MailBind()
    {
        MailmsgInfo mailmsg = ESP.Media.BusinessLogic.MailmsgManager.GetModel(int.Parse(Request["MailID"].ToString().Trim()));
        txtSubject.Text = mailmsg.Subject.ToString();
        wtpNew.XMLNText = Server.HtmlDecode(mailmsg.Body.ToString());
        hidMailID.Value = mailmsg.Id.ToString();       
        hidPJID.Value = Request[RequestName.ProjectID].ToString();
 
    }
    #endregion


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
        Response.Redirect("SendMailList.aspx");
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

    //发送邮件
    /// <summary>
    /// Handles the Click event of the btnSend control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnSend_Click(object sender, EventArgs e)
    {
        string[] emailAll = null;
        string reportmail = string.Empty;
        if (hidChkID.Value.Equals("selall"))
        {
            emailAll = new string[dgList.Rows.Count];
            for (int i = 0; i < dgList.Rows.Count; i++)
            {
                emailAll[i] = dgList.Rows[i].Cells[8].Text;
            }
        }
        else
        {
            string[] selvalue = hidChkID.Value.Split(',');
            emailAll = new string[selvalue.Length ];
            for (int i = 0; i < selvalue.Length; i++)
            {
                
                 //   emailAll[i] = dgList.Rows[Convert.ToInt32(selvalue[i])].Cells[8].Text;
              //  emailAll[i] = dgList.Rows[i].Cells[8].Text;
                ReportersInfo mr = ESP.Media.BusinessLogic.ReportersManager.GetModel(Convert.ToInt32(selvalue[i]));
                emailAll[i] = mr.Emailone.Trim();
            }
        }
        if (emailAll != null && emailAll.Length > 0)
        {
            sendmail(emailAll);
        }
    }

    /// <summary>
    /// Saves the mail.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void SaveMail(object sender, EventArgs e) 
    {
        string errmeg;
        ESP.Media.BusinessLogic.MailAnnexManager annex = new ESP.Media.BusinessLogic.MailAnnexManager();

        if (updateAnnex.HasFile)
        {
            annex.AnnexFileName = Server.MapPath(ESP.Media.Access.Utilities.ConfigManager.MailAttachmentPath + updateAnnex.FileName);
            annex.AnnexFileData = updateAnnex.FileBytes;
        }
        int ret = ESP.Media.BusinessLogic.MailmsgManager.Add(GetObject(), annex, CurrentUserID, out errmeg);
    }

    /// <summary>
    /// Sendmails the specified recvmail.
    /// </summary>
    /// <param name="recvmail">The recvmail.</param>
    void sendmail(string[] recvmail)
    {
        string errmsg = string.Empty;
        if (recvmail == null || recvmail.Length <= 0) return;
        MailAddress from = new MailAddress(ESP.Media.Access.Utilities.ConfigManager.SendMailAddress);
        MailMessage msg = new MailMessage();
        ESP.Media.BusinessLogic.MailAnnexManager annex = new ESP.Media.BusinessLogic.MailAnnexManager();
        msg.From = from;
        //msg.Subject = txtSubject.Text.Trim();
        //msg.Body = txtEmailBody.Text.Trim();
       
        int mailId = Convert.ToInt32(hidMailID.Value);
        string sub = string.Empty;
        string body = string.Empty;
        
        if (mailId > 0)
        {
            MailmsgInfo mailmsg = ESP.Media.BusinessLogic.MailmsgManager.GetModel(mailId);
            sub = mailmsg.Body;
            body = mailmsg.Subject;
            annexpath = mailmsg.Attachmentspath;
        }
        else 
        {
            sub = txtSubject.Text.Trim();            
            body = Server.HtmlEncode(this.wtpNew.XMLNText);
            
            if (updateAnnex.HasFile)
            {
                annex.AnnexFileName = Server.MapPath(ESP.Media.Access.Utilities.ConfigManager.MailAttachmentPath + updateAnnex.FileName);
                annex.AnnexFileData = updateAnnex.FileBytes;
                annexpath = CommonManager.SaveFile(ESP.Media.Access.Utilities.ConfigManager.MailAttachmentPath, annex.AnnexFileName, annex.AnnexFileData, true);
            }
        }
        msg.Subject = sub;
        msg.Body = body;
        if(annexpath.Length >0)
        {
            msg.Attachments.Add(new Attachment(Server.MapPath(annexpath)));
        }
        SmtpClient mailclient = new SmtpClient(ESP.Media.Access.Utilities.ConfigManager.SmtpHostAddress);
        mailclient.Credentials = new System.Net.NetworkCredential(ESP.Media.Access.Utilities.ConfigManager.MailUserName, ESP.Media.Access.Utilities.ConfigManager.MailPassWord);

        string sus_msg_mail = string.Empty;
        string err_msg_mail = string.Empty;
        for (int i = 0; i < recvmail.Length; i++)
        {
            if (!string.IsNullOrEmpty(recvmail[i]))
            {
                
            msg.To.Add(new MailAddress(recvmail[i]));
        
            try
            {
                mailclient.Send(msg);
                int ret = 0;
                if (Request["Daid"] != null && Request["Daid"].Trim().Length > 0)
                {
                    ret = ESP.Media.BusinessLogic.EmailsendManager.Add_UpdateDailyStatus(GetObject(recvmail[i]),CurrentUserID,Convert.ToInt32(Request["Daid"]),out errmsg);
                }
                else if (Request["Evid"] != null && Request["Evid"].Trim().Length > 0)
                {
                    ret = ESP.Media.BusinessLogic.EmailsendManager.Add_UpdateEventStatus(GetObject(recvmail[i]), CurrentUserID, Convert.ToInt32(Request["Evid"]),out errmsg);
                }
                else
                {
                    ret = ESP.Media.BusinessLogic.EmailsendManager.Add(GetObject(recvmail[i]),CurrentUserID, out errmsg) ;
                }
                if (ret> 0)
                {
                  
                    sus_msg_mail += recvmail[i] + "发送成功并保存，";
                   
                }
                else 
                { 
                
                    sus_msg_mail += recvmail[i] + "发送成功，但保存失败；";
                }
               
            }
            catch (Exception ex)
            {
                sus_msg_mail += recvmail[i] + "失败原因：" + ex.Message;               
            }
           
        }
        else
        {
            sus_msg_mail = "记者的邮箱不能为空";
        }
        }
        ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("alert('{0}');window.close();", sus_msg_mail), true);
        //ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("alert('{0}');",sus_msg_mail  ), true);
    }

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

    #region 获得对象
    /// <summary>
    /// Gets the object.
    /// </summary>
    /// <param name="emailaddress">The emailaddress.</param>
    /// <returns></returns>
    public EmailsendInfo GetObject(string emailaddress)
    {

        EmailsendInfo emailSend = new EmailsendInfo();
        emailSend.Recvaddress=emailaddress;
        emailSend.Recvuserid = 1;
        emailSend.Recvusertype = 1;
        emailSend.Senddate = DateTime.Now.ToString();
        emailSend.Senduserid = 1;
        emailSend.Status = 1;
        emailSend.Emailid = emailId;
      //  emailSend.Sendbody = txtEmailBody.Text.Trim();
        emailSend.Sendbody = Server.HtmlEncode(this.wtpNew.XMLNText);
        emailSend.Sendsubject = txtSubject.Text.Trim();
        emailSend.Sendattachmentspath = annexpath;
        return emailSend;
    }

    /// <summary>
    /// Gets the object.
    /// </summary>
    /// <returns></returns>
    private MailmsgInfo GetObject()
    {
        MailmsgInfo mlMailmsg = null;

        if (Request["Sid"] != null)
        {
            mlMailmsg = ESP.Media.BusinessLogic.MailmsgManager.GetModel(Convert.ToInt32(Request["Sid"]));
        }
        else
        {
            mlMailmsg = new MailmsgInfo();
        }
        if (Request["Sid"] != null)
        {
            mlMailmsg.Id = Convert.ToInt32(Request["Sid"]);
        }

        mlMailmsg.Subject = txtSubject.Text;
        mlMailmsg.Body = Server.HtmlEncode(wtpNew.XMLNText);
        mlMailmsg.Createdate = mlMailmsg.Createdate == string.Empty ? DateTime.Now.ToString() : mlMailmsg.Createdate;
        mlMailmsg.Createid = mlMailmsg.Createid == 0 ? CurrentUserID : mlMailmsg.Createid;



        return mlMailmsg;
    }
    #endregion

    /// <summary>
    /// Handles the Click event of the btnReporterSign control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnReporterSign_Click(object sender, EventArgs e)
    {
        //string filename = string.Format("媒体签到表 {0}.xls", DateTime.Now).Replace(':', '.');
        //filename = Server.MapPath(ESP.Media.Access.Utilities.ConfigManager.BillPath + filename);
        //string errmsg = string.Empty;

        //if (ESP.Media.BusinessLogic.ExcelExportManager.SaveSignExcel(Response, dtreport,filename, out errmsg, CurrentUserID))
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
        //string filename = string.Format("联络表 {0}.xls", DateTime.Now).Replace(':', '.');
        //filename = Server.MapPath(ESP.Media.Access.Utilities.ConfigManager.BillPath + filename);
        //string errmsg = string.Empty;

        //if(ESP.Media.BusinessLogic.ExcelExportManager.SaveCommunicateExcel(Response,dtreport,filename,out errmsg,CurrentUserID))
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


    protected void showAnnex(object sender, EventArgs e)
    {
        this.updateAnnex.Visible = true;
    }
}
