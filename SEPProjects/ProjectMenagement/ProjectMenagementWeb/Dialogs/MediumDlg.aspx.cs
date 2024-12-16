using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace FinanceWeb.Dialogs
{
    public partial class MediumDlg : ESP.Web.UI.PageBase
    {
        private string clientId = "ctl00_ContentPlaceHolder1_";
        private int projectID = 0;
        private int releaseID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                string mname = string.Empty;
                string mtype = string.Empty;
                if (!string.IsNullOrEmpty(Request["MediaName"]))
                {
                    mname = Request["MediaName"];
                }
                if (!string.IsNullOrEmpty(Request["MediaType"]))
                {
                    mtype = Request["MediaType"];
                }
                MediaBind(mtype, mname);

            }
        }

        private void MediaBind(string mediatype, string medianame)
        {
            if (mediatype == "0")
                mediatype = string.Empty;
            this.txtName.Text = medianame;
            this.ddlOption.SelectedValue = mediatype;
            List<System.Data.SqlClient.SqlParameter> parameters;
            System.Data.SqlClient.SqlParameter param1;
            System.Data.SqlClient.SqlParameter param2;
            string term = string.Empty;
            if (!string.IsNullOrEmpty(medianame) && string.IsNullOrEmpty(mediatype))
            {
                term = " and a.mediacname like '%'+@mediacname+'%' ";
                parameters = new List<System.Data.SqlClient.SqlParameter>();
                param1 = new System.Data.SqlClient.SqlParameter("@mediacname", System.Data.SqlDbType.NVarChar, 50);
                parameters.Add(param1);
            }
            else if (string.IsNullOrEmpty(medianame) && !string.IsNullOrEmpty(mediatype))
            {
                term = " and a.MediaItemType =@MediaItemType  ";
                parameters = new List<System.Data.SqlClient.SqlParameter>();
                param1 = new System.Data.SqlClient.SqlParameter("@MediaItemType ", System.Data.SqlDbType.Int, 4);
                param1.SqlValue = mediatype;
                parameters.Add(param1);
            }
            else if (!string.IsNullOrEmpty(medianame) && !string.IsNullOrEmpty(mediatype))
            {
                term = " and a.mediacname like '%'+@mediacname+'%'  and a.MediaItemType =@MediaItemType ";
                parameters = new List<System.Data.SqlClient.SqlParameter>();
                param1 = new System.Data.SqlClient.SqlParameter("@mediacname", System.Data.SqlDbType.NVarChar, 50);
                param1.SqlValue = medianame;
                parameters.Add(param1);
                param2 = new System.Data.SqlClient.SqlParameter("@MediaItemType", System.Data.SqlDbType.Int, 4);
                param2.SqlValue = mediatype;
                parameters.Add(param2);
            }
            else
            {
                term = null;
                parameters = null;
            }
            List<ESP.Media.Entity.QueryMediaItemInfo> items = ESP.Media.BusinessLogic.MediaitemsManager.GetAllObjectList(term, parameters);
            this.gv.DataSource = items;
            this.gv.DataBind();
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            string mname = string.Empty;
            string mtype = string.Empty;
            if (!string.IsNullOrEmpty(this.txtName.Text.Trim()))
            {
                mname = this.txtName.Text.Trim();
            }
            if (this.ddlOption.SelectedIndex > 0)
            {
                mtype = this.ddlOption.SelectedIndex.ToString();
            }
            MediaBind(mtype, mname);
        }
        protected void btnClean_Click(object sender, EventArgs e)
        {
            this.txtName.Text = "";
            this.ddlOption.SelectedIndex = 0;
            this.gv.DataSource = ESP.Media.BusinessLogic.MediaitemsManager.GetAllObjectList(null, null);
            this.gv.DataBind();
        }

        protected void btnSelected_Click(object sender, EventArgs e)
        {
            if (projectID > 0)
            {
                string strReportIDs = string.Empty;
                foreach (GridViewRow item in this.gv.Rows)
                {
                    CheckBox chkSelected = (CheckBox)item.FindControl("chkSelected");
                    HiddenField hidMediaID = (HiddenField)item.FindControl("hidMediaID");
                    if (chkSelected != null && chkSelected.Checked && hidMediaID != null && !string.IsNullOrEmpty(hidMediaID.Value))
                    {
                        ESP.Finance.Entity.MediumForProjectInfo newInfo = new ESP.Finance.Entity.MediumForProjectInfo();
                        newInfo.MediaID = Convert.ToInt32(hidMediaID.Value);
                        newInfo.CreatedUserID = UserInfo.UserID;
                        newInfo.IsDel = false;
                        newInfo.ModifiedUserID = UserInfo.UserID;
                        newInfo.ProjectID = Convert.ToInt32(Request["projectID"]);
                        ESP.Finance.BusinessLogic.MediumForProjectManager.Add(newInfo);
                        //hidMediaID.Value
                    }
                }
                    Response.Write("<script>opener.__doPostBack('ctl00$ContentPlaceHolder1$linkbtnMediaForProject', '');</script>");
                    Response.Write(@"<script>window.close();</script>");
            }
            else
            {
                Response.Write("<script>alert(\"发生错误\");</script>");
            }

        }

        protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gv.Rows[index];

                Response.Write("<script>opener.document.all." + clientId + "txtMediaSelect.value= '" + row.Cells[2].Text + "'</script>");
                Response.Write("<script>opener.document.all." + clientId + "hidMediaID.value= '" + gv.DataKeys[index].Value.ToString() + "'</script>");
                Response.Write(@"<script>window.close();</script>");
            }
        }
    }
}
