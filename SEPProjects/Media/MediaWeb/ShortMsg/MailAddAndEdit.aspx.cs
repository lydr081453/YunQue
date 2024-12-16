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
using ESP.Compatible;
using ESP.Media.Entity;
public partial class ShortMsg_MailAddAndEdit : ESP.Web.UI.PageBase
{
    static Hashtable ht = null;

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ht = new Hashtable();
            InitPage();
        }
    }

    /// <summary>
    /// Raises the <see cref="E:Init"/> event.
    /// </summary>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    override protected void OnInit(EventArgs e)
    {
        base.OnInit(e);
        int userid = CurrentUserID;
    }

    #region 确认
    /// <summary>
    /// Handles the Click event of the btnOk control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnOk_Click(object sender, EventArgs e)
    {
        int ret;
        int sid;
        string errmeg;

        ESP.Media.BusinessLogic.MailAnnexManager annex = new ESP.Media.BusinessLogic.MailAnnexManager();
     
        
        if (updateAnnex.HasFile)
        {
            annex.AnnexFileName = Server.MapPath(ESP.Media.Access.Utilities.ConfigManager.MailAttachmentPath + updateAnnex.FileName);
            annex.AnnexFileData = updateAnnex.FileBytes;
        }


        if (Request["Sid"] == null)//插入
        {
            //if (Adsprice.HasFile)
            //{
            //    attach.PriceFileData = Adsprice.FileBytes;
            //    attach.PriceFileName = Server.MapPath(ESP.Media.Access.Utilities.ConfigManager.MediaPricePath + Adsprice.FileName);
            //}
            ret = ESP.Media.BusinessLogic.MailmsgManager.Add(GetObject(),annex,CurrentUserID, out errmeg);
            if (ret > 0)
            {
                string url = "MailList.aspx";
                string param = Request.Url.Query.Substring(Request.Url.Query.IndexOf('?') + 1);
                if (!string.IsNullOrEmpty(Request["backurl"]))
                { url = Request["backurl"]; }

                if (Request["Source"]!=null)
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location='" + url + "?" + param + "';alert('保存成功！');", true);
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location='" + url + "?" + param + "';alert('保存成功！');", true);
                }
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("alert('{0}');", errmeg), true);
            }
        }
        else//修改
        {
            ret = ESP.Media.BusinessLogic.MailmsgManager.Update(GetObject(),annex, CurrentUserID,out errmeg);
            if (ret > 0)
            {
                string url = "MailList.aspx";
                if (!string.IsNullOrEmpty(Request["backurl"]))
                { url = Request["backurl"]; }
                string param = Request.Url.Query.Substring(Request.Url.Query.IndexOf('?') + 1);

                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location='" + url + "?" + param + "';alert('保存成功！');", true);
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("alert('{0}');", errmeg), true);
            }
        }
    }
    #endregion
   
    #region 初始化页面信息
    /// <summary>
    /// Inits the page.
    /// </summary>
    private void InitPage()
    {
        string operate = "null";
        this.btnOk.Enabled = false;
       
        if (Request["Operate"] != null)
        {
            operate = Request["Operate"];
        }
        if (operate == "EDIT")
        {
            InitEdit();
        }
        else if (operate == "ADD")
        {
            this.Title = "添加邮件";
            this.btnOk.Text = "保存";
            this.labHeading.Text = "添加邮件";
            this.btnOk.Enabled = true;

        }
        else if (operate == "DEL")
        {
            int sid = Convert.ToInt32(Request["Sid"]);
            string errmeg;
            int ret = ESP.Media.BusinessLogic.MailmsgManager.Delete(GetObject(), out errmeg);
            if (ret > 0)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location='MailList.aspx';alert('删除成功！');", true);
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("window.location='MailList.aspx';alert('{0}');", errmeg), true);
            }
        }
    }

    /// <summary>
    /// Inits the edit.
    /// </summary>
    private void InitEdit()
    {
        this.Title = "编辑邮件";
        this.btnOk.Text = "保存";
        this.labHeading.Text = "编辑邮件";
        if (Request["Sid"] != null)
        {
            int id = Convert.ToInt32(Request["Sid"]);
            MailmsgInfo mlMailmsg = ESP.Media.BusinessLogic.MailmsgManager.GetModel(id);
            if (mlMailmsg != null)
            {
                txtSubject.Text = mlMailmsg.Subject.Trim();
               // txtBody.Text = mlMailmsg.Body.Trim();
                wtpNew.XMLNText = Server.HtmlDecode(mlMailmsg.Body.Trim());
                this.btnOk.Enabled = true;
            }
        }
    }
    #endregion

    #region 获得对象
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

        mlMailmsg.Subject = this.txtSubject.Text;
        mlMailmsg.Body = Server.HtmlEncode(this.wtpNew.XMLNText);
        mlMailmsg.Createdate = mlMailmsg.Createdate == string.Empty ? DateTime.Now.ToString() : mlMailmsg.Createdate;
        mlMailmsg.Createid = mlMailmsg.Createid == 0 ? CurrentUserID : mlMailmsg.Createid;
        
       

        return mlMailmsg;
    }
    #endregion

    /// <summary>
    /// Handles the Click event of the btnBack control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnBack_Click(object sender, EventArgs e)
    {
        string url = "MailList.aspx";
        string param = Request.Url.Query.Substring(Request.Url.Query.IndexOf('?') + 1);
        if (!string.IsNullOrEmpty(Request["backurl"]))
        { url = Request["backurl"]; }
        Response.Redirect(url + "?" + param);
    }
}
