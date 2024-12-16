using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using ESP.Purchase.Common;

public partial class Purchase_Requisition_MediaDlg : ESP.Web.UI.PageBase
{
    private string clientId = "ctl00_ContentPlaceHolder1_";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string mname=string.Empty;
            string mtype = string.Empty ; 
            if (!string.IsNullOrEmpty(Request[RequestName.MediaName]))
            {
                mname = Request[RequestName.MediaName];
            }
            if (!string.IsNullOrEmpty(Request[RequestName.MediaType]))
            {
                mtype = Request[RequestName.MediaType];
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
        List<System.Data.SqlClient.SqlParameter> parameters ;
        System.Data.SqlClient.SqlParameter param1;
        System.Data.SqlClient.SqlParameter param2;
        string term = string.Empty;
        if (!string.IsNullOrEmpty(medianame) && string.IsNullOrEmpty(mediatype))
        {
            term = " and a.mediacname like '%'+@mediacname+'%' ";
            parameters = new List<System.Data.SqlClient.SqlParameter>();
            param1 = new System.Data.SqlClient.SqlParameter("@mediacname",System.Data.SqlDbType.NVarChar,50);
            parameters.Add(param1);
        }
        else if (string.IsNullOrEmpty(medianame) && !string.IsNullOrEmpty(mediatype))
        {
            term = " and a.MediaItemType =@MediaItemType  ";
            parameters = new List<System.Data.SqlClient.SqlParameter>();
            param1 = new System.Data.SqlClient.SqlParameter( "@MediaItemType ",System.Data.SqlDbType.Int,4);
            param1.SqlValue = mediatype;
            parameters.Add(param1);
        }
        else if (!string.IsNullOrEmpty(medianame) && !string.IsNullOrEmpty(mediatype))
        {
            term = " and a.mediacname like '%'+@mediacname+'%'  and a.MediaItemType =@MediaItemType ";
            parameters = new List<System.Data.SqlClient.SqlParameter>();
            param1 = new System.Data.SqlClient.SqlParameter("@mediacname",System.Data.SqlDbType.NVarChar,50);
            param1.SqlValue = medianame;
            parameters.Add( param1);
            param2 = new System.Data.SqlClient.SqlParameter("@MediaItemType",System.Data.SqlDbType.Int,4);
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
            mtype=this.ddlOption.SelectedIndex.ToString();           
        }
        MediaBind(mtype, mname);
    }
    protected void btnClean_Click(object sender, EventArgs e)
    {
        this.txtName.Text = "";
        this.ddlOption.SelectedIndex = 0;
        this.gv.DataSource = ESP.Media.BusinessLogic.MediaitemsManager.GetAllObjectList(null,null);
        this.gv.DataBind();
    }
    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Add")
        {
            // this.gv.EditRowStyle.BackColor = Color.FromName("#F7CE90");
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gv.Rows[index];

            Response.Write("<script>opener.document.all." + clientId + "txtMediaSelect.value= '" + row.Cells[2].Text + "'</script>");
            Response.Write("<script>opener.document.all." + clientId + "lblMedia.value= '" + row.Cells[2].Text + "'</script>");
            Response.Write("<script>opener.document.all." + clientId + "hidMediaID.value= '" + gv.DataKeys[index].Value.ToString() + "'</script>");
            Response.Write("<script>opener.document.all." + clientId + "lblReporter.value= ''</script>");
            Response.Write("<script>opener.document.all." + clientId + "txtReporterSelect.value= ''</script>");
            Response.Write("<script>opener.document.all." + clientId + "ddlSex.selectedIndex=0;</script>");
            Response.Write("<script>opener.document.all." + clientId + "txtReporterPhone.value= ''</script>");
            Response.Write("<script>opener.document.all." + clientId + "txtIC.value= ''</script>");
            Response.Write("<script>opener.document.all." + clientId + "txtCity.value= ''</script>");
            Response.Write("<script>opener.document.all." + clientId + "txtPhone.value= ''</script>");
            Response.Write("<script>opener.document.all." + clientId + "txtBank.value= ''</script>");
            Response.Write("<script>opener.document.all." + clientId + "txtAccount.value= ''</script>");
            Response.Write("<script>opener.document.all." + clientId + "hidReporterID.value= ''</script>");
            Response.Write(@"<script>window.close();</script>");
        }
    }
}
