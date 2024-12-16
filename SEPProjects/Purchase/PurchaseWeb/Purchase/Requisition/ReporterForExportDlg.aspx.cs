using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Purchase.Common;

public partial class Purchase_Requisition_ReporterForExportDlg : ESP.Web.UI.PageBase
{
    private string clientId = "ctl00_ContentPlaceHolder1_";
    string MediaID = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request[RequestName.MediaID]))
            {
                MediaID = Request[RequestName.MediaID];
                List<System.Data.SqlClient.SqlParameter> parameters = new List<System.Data.SqlClient.SqlParameter>();
               
                string term = " and media.mediaitemid = @mediaitemid";
                System.Data.SqlClient.SqlParameter param1 = new System.Data.SqlClient.SqlParameter("@mediaitemid",System.Data.SqlDbType.Int,4);
                param1.Value = MediaID;
                parameters.Add(param1);
                this.gv.DataSource = ESP.Media.BusinessLogic.ReportersManager.GetAllObjectList(term, parameters); ;
                this.gv.DataBind();
            }
        }
    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        string term = string.Empty;
        List<System.Data.SqlClient.SqlParameter> parameters = new List<System.Data.SqlClient.SqlParameter>();
        term += " and media.mediaitemid = @mediaitemid";
        System.Data.SqlClient.SqlParameter param1 = new System.Data.SqlClient.SqlParameter("@mediaitemid",System.Data.SqlDbType.Int,4);
        param1.Value = MediaID;
        parameters.Add(param1);

        if (!string.IsNullOrEmpty(this.txtName.Text.Trim()))
        {
            term += " and a.reportername like '%'+@reportername+'%' ";
            System.Data.SqlClient.SqlParameter param2 = new System.Data.SqlClient.SqlParameter("@reportername", System.Data.SqlDbType.NVarChar, 50);
            param2.Value = this.txtName.Text.Trim();
            parameters.Add(param2);
        }
        this.gv.DataSource = ESP.Media.BusinessLogic.ReportersManager.GetAllObjectList(term, parameters);
        this.gv.DataBind();

    }
    protected void btnClean_Click(object sender, EventArgs e)
    {
        this.gv.DataSource = ESP.Media.BusinessLogic.ReportersManager.GetAllObjectList(null, null);
        this.gv.DataBind();
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        //Response.Write("<script>opener.document.all." + clientId + "pnlReporter.style.display= 'block'</script>");
        //Response.Write("<script>opener.document.all." + clientId + "lblReporter.value= ''</script>");
        Response.Write("<script>opener.document.all." + clientId + "txtReporterSelect.value= ''</script>");
        //Response.Write("<script>opener.document.all." + clientId + "ddlSex.selectedIndex=0;</script>");
        //Response.Write("<script>opener.document.all." + clientId + "txtReporterPhone.value= ''</script>");
        //Response.Write("<script>opener.document.all." + clientId + "txtIC.value= ''</script>");
        //Response.Write("<script>opener.document.all." + clientId + "txtCity.value= ''</script>");
        //Response.Write("<script>opener.document.all." + clientId + "txtPhone.value= ''</script>");
        //Response.Write("<script>opener.document.all." + clientId + "txtBank.value= ''</script>");
        //Response.Write("<script>opener.document.all." + clientId + "txtAccount.value= ''</script>");
        Response.Write("<script>opener.document.all." + clientId + "hidReporterID.value= ''</script>");
        //Response.Write("<script>opener.document.all." + clientId + "txtBankAcountName.value= ''</script>");
        Response.Write("<script>opener.document.all." + clientId + "hidMediaOrderID.value= ''</script>");
        Response.Write(@"<script>window.close();</script>");
    }

    protected void btnSelected_Click(object sender, EventArgs e)
    {
        string strReportIDs = string.Empty;
        foreach (GridViewRow item in this.gv.Rows)
        {
            CheckBox chkSelected = (CheckBox)item.FindControl("chkSelected");
            HiddenField hidReporterID = (HiddenField)item.FindControl("hidReporterID");
            if (chkSelected != null && chkSelected.Checked && hidReporterID != null && !string.IsNullOrEmpty(hidReporterID.Value))
            {
                strReportIDs += hidReporterID.Value + ",";
            }
        }
        if (strReportIDs != string.Empty)
        {
            strReportIDs = strReportIDs.Substring(0, strReportIDs.Length - 1);
            Response.Write("<script>opener.document.all." + clientId + "hidReporterIDs.value= '" + strReportIDs + "'</script>");
            Response.Write("<script>opener.__doPostBack('ctl00$ContentPlaceHolder1$linkbtnReporter', '');</script>");
            Response.Write(@"<script>window.close();</script>");
        }

    }

    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Add")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gv.Rows[index];

            //Response.Write("<script>opener.document.all." + clientId + "pnlReporter.style.display= 'block'</script>");
            if (!string.IsNullOrEmpty(row.Cells[2].Text.Trim()) && row.Cells[2].Text.Trim() != "&nbsp;")
            {
                //Response.Write("<script>opener.document.all." + clientId + "lblReporter.value= '" + row.Cells[2].Text.Trim() + "'</script>");
                Response.Write("<script>opener.document.all." + clientId + "txtReporterSelect.value= '" + row.Cells[2].Text.Trim() + "'</script>");
            }
            else
            {
                //Response.Write("<script>opener.document.all." + clientId + "lblReporter.value= ''</script>");
                Response.Write("<script>opener.document.all." + clientId + "txtReporterSelect.value= ''</script>");

            }
            //if (!string.IsNullOrEmpty(row.Cells[4].Text.Trim()) && row.Cells[4].Text.Trim() != "&nbsp;")
            //    Response.Write("<script>for (var i = 0; i < opener.document.all." + clientId + "ddlSex.options.length; i ++){if (opener.document.all." + clientId + "ddlSex.options[i].innerText  == '" + row.Cells[4].Text.Trim() + "'){opener.document.all." + clientId + "ddlSex.options[i].selected=true;}} </script>");
            //else
            //    Response.Write("<script>opener.document.all." + clientId + "ddlSex.selectedIndex=0;</script>");

            //if (!string.IsNullOrEmpty(row.Cells[7].Text.Trim()) && row.Cells[7].Text.Trim() != "&nbsp;")
            //    Response.Write("<script>opener.document.all." + clientId + "txtReporterPhone.value= '" + row.Cells[7].Text.Trim() + "'</script>");
            //else
            //    Response.Write("<script>opener.document.all." + clientId + "txtReporterPhone.value= ''</script>");
           
            //if (!string.IsNullOrEmpty(row.Cells[9].Text.Trim()) && row.Cells[9].Text.Trim()!="&nbsp;")
            //     Response.Write("<script>opener.document.all." + clientId + "txtIC.value= '" + row.Cells[9].Text.Trim() + "'</script>");
            //else
            //    Response.Write("<script>opener.document.all." + clientId + "txtIC.value= ''</script>");

            //if (!string.IsNullOrEmpty(row.Cells[5].Text.Trim()) && row.Cells[5].Text.Trim() != "&nbsp;")
            //    Response.Write("<script>opener.document.all." + clientId + "txtCity.value= '" + row.Cells[5].Text.Trim() + "'</script>");
            //else
            //    Response.Write("<script>opener.document.all." + clientId + "txtCity.value= ''</script>");

            //if (!string.IsNullOrEmpty(row.Cells[6].Text.Trim()) && row.Cells[6].Text.Trim() != "&nbsp;")
            //    Response.Write("<script>opener.document.all." + clientId + "txtPhone.value= '" + row.Cells[6].Text.Trim() + "'</script>");
            //else
            //    Response.Write("<script>opener.document.all." + clientId + "txtPhone.value= ''</script>");

            //if (!string.IsNullOrEmpty(row.Cells[10].Text.Trim()) && row.Cells[10].Text.Trim() != "&nbsp;")
            //    Response.Write("<script>opener.document.all." + clientId + "txtBank.value= '" + row.Cells[10].Text.Trim() + "'</script>");
            //else
            //    Response.Write("<script>opener.document.all." + clientId + "txtBank.value= ''</script>");

            //if (!string.IsNullOrEmpty(row.Cells[12].Text.Trim()) && row.Cells[12].Text.Trim() != "&nbsp;")
            //Response.Write("<script>opener.document.all." + clientId + "txtAccount.value= '" + row.Cells[12].Text.Trim() + "'</script>");
            //else
            //    Response.Write("<script>opener.document.all." + clientId + "txtAccount.value= ''</script>");

            //if (!string.IsNullOrEmpty(row.Cells[13].Text.Trim()) && row.Cells[13].Text.Trim() != "&nbsp;")
            //    Response.Write("<script>opener.document.all." + clientId + "txtBankAcountName.value= '" + row.Cells[13].Text.Trim() + "'</script>");
            //else
            //    Response.Write("<script>opener.document.all." + clientId + "txtBankAcountName.value= ''</script>");
            Response.Write("<script>opener.document.all." + clientId + "hidMediaOrderID.value= ''</script>");
            Response.Write("<script>opener.document.all." + clientId + "hidReporterID.value= '" + gv.DataKeys[index].Value.ToString().Trim() + "'</script>");
            Response.Write(@"<script>window.close();</script>");
        }
    }
}
