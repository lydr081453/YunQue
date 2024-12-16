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

namespace MediaWeb.NewMedia.BaseData
{
    public partial class ReporterSelectAgencyList : ESP.Web.UI.PageBase
    {
         int alertvalue = 0;
        override protected void OnInit(EventArgs e)
        {
            InitDataGridColumn();
            InitializeComponent();
            base.OnInit(e);
            int userid = CurrentUserID;
        }

        private void InitializeComponent()
        {

        }

        #region 绑定列头
        /// <summary>
        /// 初始化表格
        /// </summary>
        private void InitDataGridColumn()
        {
            string strColumn = "AgencyID#AgencyCName#AgencyEName";
            string strHeader = "选择#机构中文名#机构英文名";
            string strSort = "#AgencyCName####";
            string strH = "center######";
            MyControls.GridViewOperate.GridViewHelper.DrawGridView(false, strColumn, strHeader, strSort, strH, this.dgList);
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["alert"]))
            {
                alertvalue = int.Parse(Request["alert"]);
            }
            if (!IsPostBack)
            {
                              

            }
            if (alertvalue > 0)
            {
                this.btnClose.Visible = true;
                this.btnBack.Visible = false;
            }
            else
            {
                this.btnClose.Visible = false;
                this.btnBack.Visible = true;

            }

            ListBind();
            Session[ESP.MediaLinq.Utilities.Global.SessionKey.CurrentRootPage] = ESP.MediaLinq.Utilities.Global.Url.ReporterSelectAgencyList;
        }
        protected void btnClear_OnClick(object sender, EventArgs e)
        {
           
            this.txtCnName.Text = string.Empty;
            
            ListBind();
        }

        #region 绑定列表
        private void ListBind()
        {
                       
            string cname = "";
            if (txtCnName.Text.Length > 0)
            {
                cname = txtCnName.Text;                
            }
          
            DataSet dt = null;
            if(!string.IsNullOrEmpty(Request["Mid"]))
            dt = ESP.MediaLinq.BusinessLogic.AgencyManager.GetDataSet(cname,Request["Mid"]);
            
            if (dt == null)
            {
                dt = new DataSet();
                this.dgList.DataSource = null;
            }
            else
                this.dgList.DataSource = dt.Tables[0].DefaultView;
        }
        #endregion

        #region 绑定下拉框
        private void ddlBind()
        {

        }
        #endregion

        #region 查找
        protected void btnSearch_Click(object sender, System.EventArgs e)
        {

        }
        #endregion


        #region 选择
        protected void btnSelect_Click(object sender, System.EventArgs e)
        {
            int aid = Convert.ToInt32(hidChecked.Value.Trim());
            string js = "";
            if (!string.IsNullOrEmpty(Request["Operate"]) && Request["Operate"] == "New")
            {
                //新添加记者ctl00_contentMain_txtAgencyName
                media_AgencyInfo agency = ESP.MediaLinq.BusinessLogic.AgencyManager.GetModel(aid);
                js += "<script>opener.document.all.ctl00_contentMain_txtAgencyName.value = '" + agency.AgencyCName + " " + agency.AgencyEName + "';</script>";
                js += "<script>opener.document.all.ctl00_contentMain_hidAgency.value = '" + agency.AgencyID + "';</script>";
                js += "<script>window.close();</script>";
                Response.Write(js);
            }
            if (Request["Rid"] != null && aid != 0)
            {
                int rid = Convert.ToInt32(Request["Rid"]);
                media_ReportersInfo reporter = ESP.MediaLinq.BusinessLogic.ReporterManager.GetModel(rid);
                if (reporter != null)
                {
                    string errmsg;
                    reporter.AgencyID = aid;
                    int ret = ESP.MediaLinq.BusinessLogic.ReporterManager.Update(reporter, null, UserInfo.UserID, out errmsg);
                    string backurl = "";
                    if (string.IsNullOrEmpty(Request["backurl"]))
                        backurl = "ReporterDisplay.aspx";
                    else
                        backurl = Request["backurl"];
                    if (alertvalue > 0)
                        backurl += "?alert=" + alertvalue;
                    else
                        backurl += "?";
                    if (ret > 0)
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("window.location='{0}&Rid={1}&Aid={2}&Operate=EDIT';alert('保存成功！');", backurl, rid, aid), true);
                    }
                    else
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("alert('{0}');", errmsg), true);

                    }

                }
            }
            else if (Request["Rid"] != null)
            {
                int rid = Convert.ToInt32(Request["Rid"]);
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("window.location='ReporterDisplay.aspx?Rid={0}';alert('你没有进行修改操作！');", rid), true);
            }
        }
        #endregion

        //#region 添加
        //protected void btnAdd_Click(object sender, System.EventArgs e)
        //{
        //    if (Request["Rid"] != "")
        //    {
        //        string ss = ESP.MediaLinq.Utilities.Global.Url.ReporterSelectMediaList + "?Rid=" + Request["Rid"];
        //        Response.Redirect("NewMedia.aspx?Source=" + ss);
        //    }
        //    else
        //    {
        //        Response.Redirect("NewMedia.aspx?Source=" + ESP.MediaLinq.Utilities.Global.Url.ReporterSelectMediaList);
        //    }
        //}
        //#endregion



        protected void dgList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int MediaId = int.Parse(dgList.DataKeys[e.Row.RowIndex].Value.ToString());
                e.Row.Cells[0].Text = string.Format("<input type=radio id=radNo onclick='selected(this)' name=radNo value={0}>", e.Row.Cells[0].Text);
                //e.Row.Cells[1].Text = string.Format("<a href='#' onclick=\"window.open('../Media/MediaDisplay.aspx?visible=false&alert=1&Mid={0}','','{2}');\" >{1}</a>", MediaId, e.Row.Cells[1].Text, ESP.Media.Access.Utilities.Global.OpenClass.Common);

            }
        }


        protected void btnBack_Click(object sender, EventArgs e)
        {
            string backurl = "";
            if (string.IsNullOrEmpty(Request["backurl"]))
                backurl = "ReporterDisplay.aspx?Rid=" + Request["Rid"];
            else
                backurl = Request["backurl"] + "?Rid=" + Request["Rid"] + "&Operate=EDIT";
            if (alertvalue > 0)
                backurl += "&alert=" + alertvalue;
            Response.Redirect(backurl);
        }
    }
    
}
