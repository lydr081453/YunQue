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
using System.Collections.Generic;
using ESP.MediaLinq.Entity;
using MediaWeb.NewMedia.BaseData.skins;

namespace MediaWeb.NewMedia.BaseData
{
    public partial class AuditMediaDisplay : ESP.Web.UI.PageBase
    {
        DataTable dtreport = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["Mid"] != null)
            {
                hidMediaId.Value = Request["Mid"];
                media_MediaItemsInfo media = ESP.MediaLinq.BusinessLogic.MediaItemManager.GetModel(Convert.ToInt32(Request["Mid"]));
                UserControl uc;

                if (media != null)
                {
                    if (media.MediaItemType == 1)
                    {
                        uc = (UserControl)this.LoadControl("skins/PlaneMediaDisplay.ascx");
                        panelMediaDisplay.Controls.Add(uc);
                        ((PlaneMediaDisplay)uc).InitPage(media);
                    }
                    else if (media.MediaItemType == 3)
                    {
                        uc = (UserControl)this.LoadControl("skins/TvMediaDisplay.ascx");
                        panelMediaDisplay.Controls.Add(uc);
                        ((TvMediaDisplay)uc).InitPage(media);
                    }
                    else if (media.MediaItemType == 2)
                    {
                        uc = (UserControl)this.LoadControl("skins/WebMediaDisplay.ascx");
                        panelMediaDisplay.Controls.Add(uc);
                        ((WebMediaDisplay)uc).InitPage(media);
                    }
                    else if (media.MediaItemType == 4)
                    {
                        uc = (UserControl)this.LoadControl("skins/DABMediaDisplay.ascx");
                        panelMediaDisplay.Controls.Add(uc);
                        ((DABMediaDisplay)uc).InitPage(media);
                    }
                    ListBind(media.MediaitemID);
                }
            }
        }

        override protected void OnInit(EventArgs e)
        {
            InitDataGridColumn();
            base.OnInit(e);
            int userid = CurrentUserID;
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            //string param = Request.Url.Query.Substring(Request.Url.Query.IndexOf('?') + 1);
            //param = ESP.Media.Access.Utilities.Global.AddParam(param, "Mid", Request["Mid"]);
            //param = ESP.Media.Access.Utilities.Global.AddParam(param, "Operate", "EDIT");
            //param = ESP.Media.Access.Utilities.Global.AddParam(param, "PbackUrl", "AuditMediaDisplay.aspx");
            //param = ESP.Media.Access.Utilities.Global.AddParam(param, "backUrl", "AuditedMediaList.aspx");

            //string strMid = string.Empty;
            //string url = string.Format("MediaAddAndEdit.aspx?{0}",param);
            //Response.Redirect(url);


            int mid = 0;
            if (Request["Mid"] != null)
                mid = Convert.ToInt32(Request["Mid"]);
            string sname = Guid.NewGuid().ToString();//DateTime.Now.ToShortTimeString();
            List<string> trunto = new List<string>();
            trunto.Add("AuditMediaDisplay.aspx");

            Session[sname] = trunto;

            string strBackUrl = string.Empty;
            if (Request["backurl"] != null)
                strBackUrl = "&backurl=" + Request["backurl"];

            Response.Redirect("MediaAddAndEdit.aspx?Operate=EDIT&Mid=" + mid.ToString() + strBackUrl + "&sname=" + sname + "&truntocount=0");
        }

        #region 绑定列头
        /// <summary>
        /// 初始化表格
        /// </summary>
        private void InitDataGridColumn()
        {
            //string strColumn = "reportername#medianame#sex#ReporterPosition#responsibledomain#mobile#tel#email";
            //string strHeader = "姓名#所属媒体#性别#职务#负责领域#手机#固话#邮箱";
            //string sort = "ReporterName#medianame#sex#ReporterPosition#responsibledomain###";
            //MyControls.GridViewOperate.GridViewHelper.DrawGridView(false, strColumn, strHeader, sort, this.dgList);
        }
        #endregion

        #region 绑定列表
        private void ListBind(int mid)
        {
           // dtreport = ESP.Media.BusinessLogic.ReportersManager.GetList("and Media_ID=" + mid.ToString(), null);
           // this.dgList.DataSource = dtreport.DefaultView;

            //for (int i = 0; i < dgList.Columns.Count; i++)
            //{
            //    dgList.Columns[i].HeaderStyle.Wrap = false;
            //}
        }

        //protected void dgList_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        int id = int.Parse(dgList.DataKeys[e.Row.RowIndex].Value.ToString());
        //        e.Row.Cells[0].Text = string.Format("<a onclick=\"window.open('ReporterDisplay.aspx?alert=1&Rid={0}','','{2}');\">{1}</a>", id, e.Row.Cells[0].Text, ESP.Media.Access.Utilities.Global.OpenClass.Common);

        //    }
        //}

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
        #endregion

        //protected void btnWatch_Click(object sender, EventArgs e)
        //{
        //    if (Request["Mid"] != null)
        //    {
        //        MediaitemsInfo media = ESP.Media.BusinessLogic.MediaitemsManager.GetModel(Convert.ToInt32(Request["Mid"]));

        //        if (media != null)
        //        {
        //            if (media.Mediaitemtype == 1)
        //            {
        //                Response.Redirect(string.Format("PlaneMediaContentsList.aspx?Mid={0}", Request["Mid"]));

        //            }
        //            else if (media.Mediaitemtype == 3)
        //            {
        //                Response.Redirect(string.Format("TvMediaContentsList.aspx?Mid={0}", Request["Mid"]));

        //            }
        //            else if (media.Mediaitemtype == 2)
        //            {
        //                Response.Redirect(string.Format("WebMediaContentsList.aspx?Mid={0}", Request["Mid"]));

        //            }
        //            else if (media.Mediaitemtype == 4)
        //            {
        //                Response.Redirect(string.Format("DABMediaContentsList.aspx?Mid={0}", Request["Mid"]));

        //            }
        //        }
        //    }
        //}
        protected void btnAudit_Click(object sender, EventArgs e)
        {
            string errmsg = string.Empty;
            if (Request["Mid"] != null)
            {
                media_MediaItemsInfo media = ESP.MediaLinq.BusinessLogic.MediaItemManager.GetModel(Convert.ToInt32(Request["Mid"]));

                int ret = ESP.MediaLinq.BusinessLogic.MediaItemManager.AuditMedia(media, CurrentUserID, out errmsg);
                if (ret > 0)
                {

                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location = 'AuditMediaList.aspx';alert('审核成功！');window.close();", true);

                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("alert('{0}');", errmsg), true);
                }

            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("AuditMediaList.aspx");
        }
    }
}
