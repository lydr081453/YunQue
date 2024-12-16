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
public partial class ShortMsg_ShortMsgAddAndEdit : ESP.Web.UI.PageBase
{
    static Hashtable ht =null;

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
        
        
        if (Request["Sid"] == null)//插入
        {
            ret = ESP.Media.BusinessLogic.ShortmsgManager.Add(GetObject(),CurrentUserID,out errmeg);
            if (ret > 0)
            {
                if (Request["Source"]!=null)
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location='SendShortMsgList.aspx';alert('保存成功！');", true);
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location='ShortMsgList.aspx';alert('保存成功！');", true);
                }
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("alert('{0}');", errmeg), true);
            }
        }
        else//修改
        {
            ret = ESP.Media.BusinessLogic.ShortmsgManager.Update(GetObject(), CurrentUserID,out errmeg);
            if (ret > 0)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location='ShortMsgList.aspx';alert('保存成功！');", true);
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
            this.Title = "添加短信息";
            this.btnOk.Text = "保存";
            this.labHeading.Text = "添加短信息";
            this.btnOk.Enabled = true;
         
        }
        else if (operate == "DEL")
        {
            int sid = Convert.ToInt32(Request["Sid"]);
            string errmeg;
            int ret = ESP.Media.BusinessLogic.ShortmsgManager.Delete(GetObject(), out errmeg);
            if (ret > 0)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location='ShortMsgList.aspx';alert('删除成功！');", true);
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("window.location='ShortMsgList.aspx';alert('{0}');", errmeg), true);
            }
        }
    }

    /// <summary>
    /// Inits the edit.
    /// </summary>
    private void InitEdit()
    {
        this.Title = "编辑短信息";
        this.btnOk.Text = "保存";
        this.labHeading.Text = "编辑短信息";
        if (Request["Sid"] != null)
        {
            int id = Convert.ToInt32(Request["Sid"]);
            ShortmsgInfo mlShortmsg = ESP.Media.BusinessLogic.ShortmsgManager.GetModel(id);
            if (mlShortmsg != null)
            {
                 txtSubject.Text=mlShortmsg.Subject.Trim();
                txtBody.Text= mlShortmsg.Body.Trim();
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
    #endregion

    /// <summary>
    /// Handles the Click event of the btnBack control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("/ShortMsg/ShortMsgList.aspx");
    }
}
