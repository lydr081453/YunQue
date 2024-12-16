using System;
using System.Text;
using System.Collections;
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
using ESP.Compatible;
using ESP.Media.BusinessLogic;

public partial class NewMedia_System_MediaFaceToFaceAdd : ESP.Web.UI.PageBase
{
    private static ESP.MediaLinq.Entity.Media_FaceToFace editface;

    protected void Page_Load(object sender, EventArgs e)
    {
        ListBind();
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request[RequestName.Operate]) && Request[RequestName.Operate] == "edit")
            {
                editface = ESP.MediaLinq.BusinessLogic.FaceToFaceManager.GetModel(Convert.ToInt32(Request[RequestName.FaceID]));
                this.txtCycle.Text = editface.cycle.ToString();
                this.txtSubject.Text = editface.subject;
                this.txtRemark.Text = editface.Remark;
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
        string clientclick2 = "return confirm('确定要删除该信息吗?');";
        MyControls.GridViewOperate.ImageButtonItem itmDelete = new MyControls.GridViewOperate.ImageButtonItem(ESP.Media.Access.Utilities.ConfigManager.DelIconPath, clientclick2, "id", true);
        itmDelete.DoSomething += new MyControls.GridViewOperate.DoSomethingHandler(itmDelete_DoSomething);
        tfdelete.ItemTemplate = itmDelete;
        tfdelete.HeaderText = "删除";
        dgList.Columns.Add(tfdelete);


    }
    #endregion

    void itmDelete_DoSomething(object sender, string value)
    {
        string errmsg = string.Empty;
        if (ESP.MediaLinq.BusinessLogic.FaceToFaceManager.Delete(Convert.ToInt32(value)))
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
        Response.Redirect("/System/MediaFaceToFaceAdd.aspx?" + RequestName.Operate + "=edit&" + RequestName.FaceID + "=" + value);
    }

    private void ListBind()
    {
        StringBuilder str = new StringBuilder();
        Hashtable ht = new Hashtable();
        DataTable dt = ESP.MediaLinq.BusinessLogic.FaceToFaceManager.GetAll();
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
        string errmsg = string.Empty;
        int ret = 0;
        string logoname = string.Empty;
        if (!string.IsNullOrEmpty(Request[RequestName.Operate]) && Request[RequestName.Operate] == "edit")
        {
            editface.cycle = Convert.ToInt32(this.txtCycle.Text);
            editface.Remark = this.txtRemark.Text;
            editface.subject = this.txtSubject.Text;
            editface.createdate = DateTime.Now;
            editface.createuserid = UserInfo.UserID;

            if (ESP.MediaLinq.BusinessLogic.FaceToFaceManager.Update(editface))
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
            if (fplTitle.HasFile)
            {
                byte[] filedata = fplTitle.FileBytes;


                if (fplTitle.FileName != null && fplTitle.FileName.Length > 0)
                {
                    ESP.MediaLinq.Entity.Media_FaceToFace face = new ESP.MediaLinq.Entity.Media_FaceToFace();
                    face.cycle = Convert.ToInt32(this.txtCycle.Text);
                    face.Remark = this.txtRemark.Text;
                    face.subject = this.txtSubject.Text;
                    face.createdate = DateTime.Now;
                    face.createuserid = UserInfo.UserID;
                    //meeting.Path = myFile.FileName;

                    string filename = Server.MapPath(ESP.Media.Access.Utilities.ConfigManager.MediaFaceToFacePath + fplTitle.FileName);
                    if (filedata != null && filename != string.Empty)
                    {
                        face.path = CommonManager.SaveFile(ESP.Media.Access.Utilities.ConfigManager.MediaFaceToFacePath, filename, filedata, true);
                    }

                    ret = ESP.MediaLinq.BusinessLogic.FaceToFaceManager.Add(face);

                }
            }
        }

        if (ret > 0)
        {
            // Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("alert('{0}');", "上传成功"), true);
            Response.Redirect("/System/MediaFaceToFaceAdd.aspx?" + RequestName.Operate + "=add");
            this.txtRemark.Text = string.Empty;
            this.txtSubject.Text = string.Empty;
        }
        else
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("alert('{0}');", errmsg), true);
        }
    }
}
