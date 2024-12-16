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
using ESP.MediaLinq.Entity;
using MediaWeb.NewMedia.BaseData.skins;

namespace MediaWeb.NewMedia.BaseData
{
    public partial class ReporterList : ESP.Web.UI.PageBase
    {
        DataTable dtreport = null;

        override protected void OnInit(EventArgs e)
        {
            InitDataGridColumn();
            base.OnInit(e);
            int userid = UserID;
        }

        #region 绑定列头
        /// <summary>
        /// 初始化表格
        /// </summary>
        private void InitDataGridColumn()
        {
            string strColumn = "reporterid#reportername#medianame#sex#ReporterPosition#responsibledomain#mobile#tel#email#ReporterID#ReporterID";
            string strHeader = "选择#姓名#所属媒体#性别#职务#负责领域#手机#固话#邮箱#编辑#删除";
            string sort = "#ReporterName#medianame#sex#ReporterPosition#responsibledomain######";
            string strH = "center#########center#center";
            MyControls.GridViewOperate.GridViewHelper.DrawGridView(false, strColumn, strHeader, sort, strH, this.dgList);
        }
        #endregion


        protected void Page_Load(object sender, EventArgs e)
        {

            Session[ESP.MediaLinq.Utilities.Global.SessionKey.CurrentRootPage] = ESP.MediaLinq.Utilities.Global.Url.ReporterList;
            ListBind();

            //string filename = string.Format("媒体签到表 {0}.xls", DateTime.Now).Replace(':', '.');
            //btnReporterSign.Attributes.Add("onclick", string.Format("return btnReporterSign_ClientClick('{0}');", filename));


            //filename = string.Format("联络表 {0}.xls", DateTime.Now).Replace(':', '.');
            //btnReporterContact.Attributes.Add("onclick", string.Format("return btnReporterContact_ClientClick('{0}');", filename));

            //ListBindBySession();
        }

        protected void btnClear_OnClick(object sender, EventArgs e)
        {
            this.txtReporterName.Text = string.Empty;
            this.txtMedia.Text = string.Empty;
            this.txtMobile.Text = string.Empty;
            this.txtIdCard.Text = string.Empty;
            this.txtEmail.Text = string.Empty;
            ListBind();
        }

        //private void ListBindBySession()
        //{
        //    string str;
        //    Hashtable ht;
        //    if (Session["reporterTerms"] == null)
        //        str = string.Empty;
        //    else
        //        str = Session["reporterTerms"].ToString();

        //    if (Session["reporterTerms"] == null)
        //        ht = null;
        //    else
        //        ht = (Hashtable)Session["reporterHash"];

        //    if (hidMediaId.Value != "0")
        //    {
        //        int mid = Convert.ToInt32(hidMediaId.Value);
        //        media_MediaItemsInfo media = ESP.MediaLinq.BusinessLogic.MediaItemManager.GetModel(mid);
        //        txtMedia.Text = media.MediaCName;
        //        dtreport = ESP.MediaLinq.BusinessLogic.ReporterManager.GetListByMedia(mid);
        //    }
        //    else
        //    {
        //        btnLink.Visible = false;
        //        dtreport = ESP.Media.BusinessLogic.ReportersManager.GetList(str, ht);
        //    }
        //    this.dgList.DataSource = dtreport.DefaultView;
        //    if (dgList.Rows.Count > 0)
        //    {
        //        this.btnReporterSign.Visible = true;
        //        this.btnReporterContact.Visible = true;
        //    }
        //    else
        //    {
        //        this.btnReporterSign.Visible = false;
        //        this.btnReporterContact.Visible = false;
        //    }
        //}


        #region 绑定列表
        private void ListBind()
        {
            StringBuilder str = new StringBuilder();
            Hashtable ht = new Hashtable();
            //记者名称
            string rname = "";
            if (txtReporterName.Text.Trim() != string.Empty)
            {
                str.Append(" and ReporterName like '%'+@rname+'%'");
                rname = txtReporterName.Text.Trim();
            }
            //手机号
            string mobile="";
            if (txtMobile.Text.Trim() != string.Empty)
            {
                str.Append(" and (UsualMobile like '%'+@mobile+'%' or BackupMobile like '%'+@mobile+'%' )");
                mobile = txtMobile.Text.Trim();                
            }

            //邮箱
            string email = "";
            if (txtEmail.Text.Trim() != string.Empty)
            {
                str.Append(" and (EmailOne like '%'+@email+'%' or EmailTwo like '%'+@email+'%'or EmailThree like '%'+@email+'%') ");
                email = txtEmail.Text.Trim();                
            }
            //身份证号
            string idcard="";
            if (txtIdCard.Text.Trim() != string.Empty)
            {
                str.Append(" and CardNumber like '%'+@idcard+'%'");
                idcard = txtIdCard.Text.Trim();                
            }
            //所属媒体
            string mediacname = "";
            if (txtMedia.Text.Trim() != "")
            {
                str.Append(" and (media.mediacname like '%'+@MediaCName+'%' or media.ChannelName like '%'+@MediaCName+'%' or media.TopicName like '%'+@MediaCName+'%') ");
                mediacname = txtMedia.Text.Trim();                
            }

            int mid = 0;


            //Session["reporterTerms"] = str.ToString();
            //Session["reporterHash"] = ht;

            if (hidMediaId.Value != "0")
            {
                mid = Convert.ToInt32(hidMediaId.Value);
                media_MediaItemsInfo media = ESP.MediaLinq.BusinessLogic.MediaItemManager.GetModel(mid);
                txtMedia.Text = media.MediaCName;
                dtreport = ESP.MediaLinq.BusinessLogic.ReporterManager.GetListByMedia(rname,mobile,email,idcard,mediacname,mid);
            }
            else
            {
                //btnLink.Visible = false;
                //  this.dgList.HideColumns(new int[] { 0 });
                dtreport = ESP.MediaLinq.BusinessLogic.ReporterManager.GetListByMedia(rname, mobile, email, idcard, mediacname, null);
            }
            this.dgList.DataSource = dtreport.DefaultView;
            //if (dgList.Rows.Count > 0)
            //{
            //    this.btnReporterSign.Visible = true;
            //    this.btnReporterContact.Visible = true;
            //}
            //else
            //{
            //    this.btnReporterSign.Visible = false;
            //    this.btnReporterContact.Visible = false;
            //}

            //for(int i=0;i<dgList.Columns.Count;i++)
            //{
            //    dgList.Columns[i].HeaderStyle.Wrap = false;
            //}
            //this.dgList.Columns[12].Visible = false;
        }
        #endregion

        #region 绑定下拉框
        private void ddlBind()
        {

        }
        #endregion

        #region 查找
        protected void btnFind_Click(object sender, EventArgs e)
        {
            ListBind();
        }
        #endregion

        #region 添加
        protected void btnAdd_Click(object sender, System.EventArgs e)
        {
            int mid = 0;
            int aid = 0;
            if (Request["Mid"] != null)
                mid = Convert.ToInt32(Request["Mid"]);
            if (!string.IsNullOrEmpty(Request["Aid"]))
                aid = Convert.ToInt32(Request["Aid"]);
            Response.Redirect("ReporterAddAndEdit.aspx?Operate=ADD&Mid=" + mid.ToString() + "&Aid=" + aid.ToString());
        }
        #endregion

        #region 关联到媒体
        //protected void btnLink_Click(object sender, EventArgs e)
        //{
        //    string[] ss = hidChecked.Value.Trim(',', ' ').Split(',');
        //    int[] ls = new int[ss.Length];
        //    for (int i = 0; i < ss.Length; i++)
        //    {
        //        ls[i] = Convert.ToInt32(ss[i]);
        //    }
        //    if (Request["Mid"] != null)
        //    {
        //        string errmsg;
        //        int ret = ESP.Media.BusinessLogic.ReportersManager.LinkToMedia(ls, Convert.ToInt32(Request["Mid"]), out errmsg);
        //        if (ret > 0)
        //        {
        //        }
        //        else
        //        {
        //            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("alert('{0}');", errmsg), true);
        //        }
        //    }
        //}
        #endregion

        protected void dgList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int mid = 0;
            if (Request["Mid"] != null)
                mid = Convert.ToInt32(Request["Mid"]);
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Text = "<input type='checkbox' id='chkHeader' onclick=selectedcheck('Header','Rep'); value='" + e.Row.Cells[0].Text + "' />选择";
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //e.Row.Cells[0].Text = string.Format("<input type='checkbox' onclick='selected(this)' value='{0}'/>", e.Row.Cells[0].Text);
                //e.Row.Cells[0].Width = 40;
                e.Row.Cells[0].Text = string.Format("<input type='checkbox' id='chkRep' name='chkRep' value={0} />", e.Row.Cells[0].Text);
                e.Row.Cells[1].Text = string.Format("<a href='ReporterDisplay.aspx?Rid={0}&Mid={1}';>" + e.Row.Cells[1].Text + "</a>", e.Row.Cells[9].Text, mid);

                //    e.Row.Cells[9].Text = string.Format("<a title='查看历史' onclick=\"window.open('ReporterContentsList.aspx?alert=1&Rid={0}','','{1}');\"><img src='{2}' /></a>", e.Row.Cells[10].Text, ESP.Media.Access.Utilities.Global.OpenClass.Common, ESP.Media.Access.Utilities.ConfigManager.DisplayIconPath); 
                e.Row.Cells[9].Text = string.Format("<a href='ReporterAddAndEdit.aspx?Operate=EDIT&Rid={0}&Mid={1}' ><img src='{2}' /></a>", e.Row.Cells[9].Text, mid, ESP.MediaLinq.Utilities.ConfigManager.EditIconPath);
                e.Row.Cells[10].Text = string.Format("<a href='ReporterAddAndEdit.aspx?Operate=DEL&Rid={0}&Mid={1}' onclick= \"return confirm( '真的要删除吗?');\" ><img src='{2}' /></a>", e.Row.Cells[10].Text, mid, ESP.MediaLinq.Utilities.ConfigManager.DelIconPath);
            }
        }

        protected void btnMedia_Click(object sender, EventArgs e)
        {
            Response.Redirect("ReporterSelectMediaList.aspx");
        }

        private string GetWorkString(string xml)
        {
            xml = Server.HtmlDecode(xml);
            Experience.InitExperienceTable();
            DataTable dt = Experience.ExperienceTable.Clone();
            System.IO.StringReader sr = new System.IO.StringReader(xml);
            dt.ReadXml(sr);
            if (dt.Rows.Count > 0)
                return dt.Rows[0]["单位名称"].ToString();
            else
                return xml;
        }

        protected void dgList_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (e.SortDirection == SortDirection.Ascending)
            {
                e.SortDirection = SortDirection.Descending;
            }
            else if (e.SortDirection == SortDirection.Descending)
            {
                e.SortDirection = SortDirection.Ascending;
            }
        }

        protected void btnReporterSign_Click(object sender, EventArgs e)
        {
            string filename = string.Format("媒体签到表 {0}.xls", DateTime.Now).Replace(':', '.');
            filename = Server.MapPath(ESP.MediaLinq.Utilities.ConfigManager.BillPath + filename);
            string errmsg = string.Empty;

            //if (ESP.Media.BusinessLogic.ExcelExport.SaveSignExcel(Response, dtreport, filename, out errmsg, CurrentUserID))
            //{
            //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("alert('生成媒体签到表{0}');", errmsg), true);
            //}
            //else
            //{
            //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("alert('{0}');", errmsg), true);

            //}
        }
        protected void btnReporterContact_Click(object sender, EventArgs e)
        {
            string filename = string.Format("联络表 {0}.xls", DateTime.Now).Replace(':', '.');
            filename = Server.MapPath(ESP.MediaLinq.Utilities.ConfigManager.BillPath + filename);
            string errmsg = string.Empty;

            //if (ESP.Media.BusinessLogic.ExcelExport.SaveCommunicateExcel(Response, dtreport, filename, out errmsg, CurrentUserID))
            //{
            //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("alert('生成联络表{0}');", errmsg), true);
            //}
            //else
            //{
            //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("alert('{0}');", errmsg), true);

            //}
        }

    }
}
