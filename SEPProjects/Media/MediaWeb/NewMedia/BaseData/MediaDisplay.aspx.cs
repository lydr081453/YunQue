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
using System.Web.UI.MobileControls;
using System.Collections.Generic;
using ESP.MediaLinq.Entity;
using MediaWeb.NewMedia.BaseData.skins;

namespace MediaWeb.NewMedia.BaseData
{
    public partial class MediaDisplay : ESP.Web.UI.PageBase
    {
        int alertvalue = 0;
        DataTable dtreport = null;
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["Mid"] != null)
            {
                hidMediaId.Value = Request["Mid"];
                media_MediaItemsInfo media = ESP.MediaLinq.BusinessLogic.MediaItemManager.GetModel(Convert.ToInt32(Request["Mid"]));
                UserControl uc;

                if (media != null)
                {
                    if (!string.IsNullOrEmpty(Request["enablededit"]) && Request["enablededit"] == "0")
                    {
                        this.btnEdit1.Visible = false;
                        this.btnEdit.Visible = false;
                    }
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
                    Watch();
                }
            }
            if (string.IsNullOrEmpty(Request["alert"]))
            {
                btnClose.Visible = false;
                btnClose1.Visible = false;
                btnBack.Visible = true;
                btnBack1.Visible = true;
            }
            else
            {
                btnClose.Visible = true;
                btnClose1.Visible = true;
                btnBack.Visible = false;
                btnBack1.Visible = false;
            }
            if (!string.IsNullOrEmpty(Request["alert"]))
            {
                alertvalue = int.Parse(Request["alert"]);
                if (alertvalue > 1)
                {
                    btnBack.Visible = true;
                    btnBack1.Visible = true;
                }
            }

            //string filename = string.Format("媒体签到表 {0}.xls", DateTime.Now).Replace(':', '.');
            //btnReporterSign.Attributes.Add("onclick", string.Format("return btnReporterSign_ClientClick('{0}');", filename));


            //filename = string.Format("联络表 {0}.xls", DateTime.Now).Replace(':', '.');
            //btnReporterContact.Attributes.Add("onclick", string.Format("return btnReporterContact_ClientClick('{0}');", filename));
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
            //string strColumn = "ReporterID#ReporterName#Sex#Birthday#UsualMobile#Tel_O#QQ#MSN#Experience#ReporterID";
            //string strHeader = "选择#姓名#性别#出生日期#手机#固话#QQ#MSN#工作单位#查看";
            //string sort = "#ReporterName#Sex#Birthday#######";
            //MyControls.GridViewOperate.GridViewHelper.DrawGridView(false, strColumn, strHeader, sort, this.dgList);

            string strColumn = "reportername#mediacname#sex#ReporterPosition#responsibledomain#mobile#tel#email";
            string strHeader = "姓名#所属媒体#性别#职务#负责领域#手机#固话#邮箱";
            string sort = "ReporterName#mediacname#sex#ReporterPosition#responsibledomain###";
            MyControls.GridViewOperate.GridViewHelper.DrawGridView(false, strColumn, strHeader, sort, this.dgList);
        }
        #endregion

        #region 绑定列表
        private void ListBind(int mid)
        {
            dtreport = ESP.MediaLinq.BusinessLogic.ReporterManager.GetListByMediaID(mid);
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
            for (int i = 0; i < dgList.Columns.Count; i++)
            {
                dgList.Columns[i].HeaderStyle.Wrap = false;
            }
        }

        /// <summary>
        /// Handles the RowDataBound event of the dgList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewRowEventArgs"/> instance containing the event data.</param>
        protected void dgList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                int id = int.Parse(dgList.DataKeys[e.Row.RowIndex].Value.ToString());
                e.Row.Cells[0].Text = string.Format("<a onclick=\"window.open('ReporterDisplay.aspx?alert=1&Rid={0}','','{2}');\">{1}</a>", id, e.Row.Cells[0].Text, ESP.MediaLinq.Utilities.Global.OpenClass.Common);
            }
        }

        /// <summary>
        /// Gets the work string.
        /// </summary>
        /// <param name="xml">The XML.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Watches this instance.
        /// </summary>
        private void Watch()
        {
            if (Request["Mid"] != null)
            {
                media_MediaItemsInfo media = ESP.MediaLinq.BusinessLogic.MediaItemManager.GetModel(Convert.ToInt32(Request["Mid"]));

                if (media != null)
                {
                    string ctrl = string.Empty;

                    if (media.MediaItemType == 1)
                    {
                        //Response.Redirect(string.Format("PlaneMediaContentsList.aspx?Mid={0}", Request["Mid"]));
                        if (string.IsNullOrEmpty(Request["alert"]))
                        {
                            ctrl = string.Format("<input type='button' id='btnWatch' onclick=\"window.open('PlaneMediaContentsList.aspx?alert=1&Mid={0}','历史列表','{2}');\" value={1} class='widebuttons'/>", Request["Mid"], "历史查看", ESP.MediaLinq.Utilities.Global.OpenClass.Common);
                        }
                        else
                        {
                            ctrl = string.Format("<input type='button' id='btnWatch' onclick=\"window.location = 'PlaneMediaContentsList.aspx?alert=" + (alertvalue) + "&Mid={0}';\" value={1} class='widebuttons'/>", Request["Mid"], "历史查看");
                        }


                    }
                    else if (media.MediaItemType == 3)
                    {
                        //Response.Redirect(string.Format("TvMediaContentsList.aspx?Mid={0}", Request["Mid"]));
                        if (string.IsNullOrEmpty(Request["alert"]))
                        {
                            ctrl = string.Format("<input type='button' id='btnWatch' onclick=\"window.open('TvMediaContentsList.aspx?alert=1&Mid={0}','历史列表','{2}');\" value={1} class='widebuttons'/>", Request["Mid"], "历史查看", ESP.MediaLinq.Utilities.Global.OpenClass.Common);
                        }
                        else
                        {
                            ctrl = string.Format("<input type='button' id='btnWatch' onclick=\"window.location='TvMediaContentsList.aspx?alert=" + (alertvalue) + "&Mid={0}';\" value={1} class='widebuttons'/>", Request["Mid"], "历史查看");
                        }


                    }
                    else if (media.MediaItemType == 2)
                    {
                        //Response.Redirect(string.Format("WebMediaContentsList.aspx?Mid={0}", Request["Mid"]));
                        if (string.IsNullOrEmpty(Request["alert"]))
                        {
                            ctrl = string.Format("<input type='button' id='btnWatch' onclick=\"window.open('WebMediaContentsList.aspx?alert=1&Mid={0}','历史列表','{2}');\" value={1} class='widebuttons'/>", Request["Mid"], "历史查看", ESP.MediaLinq.Utilities.Global.OpenClass.Common);
                        }
                        else
                        {
                            ctrl = string.Format("<input type='button' id='btnWatch' onclick=\"window.location='WebMediaContentsList.aspx?alert=" + (alertvalue) + "&Mid={0}';\" value={1} class='widebuttons'/>", Request["Mid"], "历史查看");
                        }

                    }
                    else if (media.MediaItemType == 4)
                    {
                        //Response.Redirect(string.Format("DABMediaContentsList.aspx?Mid={0}", Request["Mid"]));
                        if (string.IsNullOrEmpty(Request["alert"]))
                        {
                            ctrl = string.Format("<input type='button' id='btnWatch' onclick=\"window.open('DABMediaContentsList.aspx?alert=1&Mid={0}','历史列表','{2}');\" value={1} class='widebuttons'/>", Request["Mid"], "历史查看", ESP.MediaLinq.Utilities.Global.OpenClass.Common);
                        }
                        else
                        {
                            ctrl = string.Format("<input type='button' id='btnWatch' onclick=\"window.location='DABMediaContentsList.aspx?alert=" + (alertvalue) + "&Mid={0}';\" value={1} class='widebuttons'/>", Request["Mid"], "历史查看");
                        }


                    }
                    ltOperate.Text = ctrl;
                }
            }
            if (!string.IsNullOrEmpty(Request["visible"]) && Request["visible"] == "false")
            {
                tbReporter.Visible = false;
                ltOperate.Visible = false;
            }
        }

        /// <summary>
        /// Handles the Click event of the btnAdd control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            int mid = 0;
            if (Request["Mid"] != null)
                mid = Convert.ToInt32(Request["Mid"]);
            Response.Redirect("ReporterAddAndEdit.aspx?Operate=ADD&Mid=" + mid.ToString());
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            string param = Request.Url.Query.Substring(Request.Url.Query.IndexOf('?') + 1);
            //int mid = 0;
            if (Request["Mid"] != null)
                param = ESP.MediaLinq.Utilities.Global.AddParam(param, "Mid", Request["Mid"]);
            if (!string.IsNullOrEmpty(Request["alert"]))
                param = ESP.MediaLinq.Utilities.Global.AddParam(param, "alert", (Convert.ToInt32(Request["alert"]) + 1).ToString());
            string sname = Guid.NewGuid().ToString();//DateTime.Now.ToShortTimeString();
            List<string> trunto = new List<string>();
            trunto.Add("MediaDisplay.aspx");

            Session[sname] = trunto;

            if (Request["backurl"] != null)
                param = ESP.MediaLinq.Utilities.Global.AddParam(param, "backurl", Request["backurl"]);

            param = ESP.MediaLinq.Utilities.Global.AddParam(param, "Operate", "EDIT");
            param = ESP.MediaLinq.Utilities.Global.AddParam(param, "sname", sname);
            param = ESP.MediaLinq.Utilities.Global.AddParam(param, "truntocount", "0");
            Response.Redirect(string.Format("MediaAddAndEdit.aspx?{0}", param));
        }

        /// <summary>
        /// Handles the Click event of the btnBack control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnBack_Click(object sender, EventArgs e)
        {
            if (alertvalue == 0)
                Response.Redirect(string.IsNullOrEmpty(Request["backurl"]) ? "AuditedMediaList.aspx" : Request["backurl"].ToString());
            else
            {
                if (!string.IsNullOrEmpty(Request["backurl"]))
                {
                    string param = Request.Url.Query.Substring(Request.Url.Query.IndexOf('?') + 1);
                    param = ESP.MediaLinq.Utilities.Global.AddParam(param, "Mid", Request["Mid"]);
                    if (alertvalue > 0)
                    {
                        param = ESP.MediaLinq.Utilities.Global.AddParam(param, "alert", (alertvalue - 1).ToString());
                    }
                    Response.Redirect(string.Format("/Project/ProjectSelectMediaList.aspx?{0}", param));
                }
            }
        }
        /////
        //protected void btnReporterSign_Click(object sender, EventArgs e)
        //{
        //    string filename = string.Format("媒体签到表 {0}.xls", DateTime.Now).Replace(':', '.');
        //    filename = Server.MapPath(ESP.MediaLinq.Utilities.ConfigManager.BillPath + filename);
        //    string errmsg = string.Empty;

        //    //if (ESP.Media.BusinessLogic.ExcelExportManager.SaveSignExcel(Response, dtreport, filename, out errmsg, CurrentUserID))
        //    //{
        //    //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("alert('生成媒体签到表{0}');", errmsg), true);
        //    //}
        //    //else
        //    //{
        //    //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("alert('{0}');", errmsg), true);

        //    //}
        //}
        //protected void btnReporterContact_Click(object sender, EventArgs e)
        //{
        //    string filename = string.Format("联络表 {0}.xls", DateTime.Now).Replace(':', '.');
        //    filename = Server.MapPath(ESP.MediaLinq.Utilities.ConfigManager.BillPath + filename);
        //    string errmsg = string.Empty;

        //    //if (ESP.Media.BusinessLogic.ExcelExportManager.SaveCommunicateExcel(Response, dtreport, filename, out errmsg, CurrentUserID))
        //    //{
        //    //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("alert('生成联络表{0}');", errmsg), true);
        //    //}
        //    //else
        //    //{
        //    //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("alert('{0}');", errmsg), true);

        //    //}
        //}
    }
}
