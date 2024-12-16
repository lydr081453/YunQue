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
using System.Collections.Specialized;

public partial class System_NoUploadReporterWarning : ESP.Web.UI.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ListBind();
    }

    #region 绑定列表
    private void ListBind()
    {
        //phNoUploadReporterList绑定        

        //DataTable dtNoUploadReporterList = ESP.Media.BusinessLogic.UsersManager.GetNoUploadReporterList(CurrentUserID);
        //if (dtNoUploadReporterList == null)
        //{
        //    dtNoUploadReporterList = new DataTable();
        //}
        //if (dtNoUploadReporterList.Rows.Count > 0)
        //{           
        //    Table tbp = new Table();
        //    tbp.Attributes["style"] = "width: 100%; border:0; padding:0";
        //    tbp.Attributes["align"] = "center";
        //    tbp.BorderWidth = 0;
        //    tbp.CellPadding = 0;
        //    TableRow trp = null;
        //    for (int i = 0; i < dtNoUploadReporterList.Rows.Count; i++)
        //    {

        //        trp = new TableRow();
        //        trp.CssClass = "align:left";
        //        tbp.Controls.Add(trp);


        //        TableCell tcpname = new TableCell();
        //        tcpname.Text = string.Format(" <li>用户名：</li><span class='span1'>{0}</span>", dtNoUploadReporterList.Rows[i]["reportername"]);
        //        TableCell tcppn = new TableCell();
        //        tcppn.Text = string.Format("<span class='span1'>项目名称：{0}</span>", dtNoUploadReporterList.Rows[i]["ProjectName"]);
        //        TableCell tcpdn = new TableCell();
        //        tcpdn.Text = string.Format("<span class='span1'>所属日常传播：{0}</span>", dtNoUploadReporterList.Rows[i]["DailyName"]);
        //        TableCell tcpdst = new TableCell();
        //        tcpdst.Text = string.Format("<span class='span1'>日常传播开始时间：{0}</span>", dtNoUploadReporterList.Rows[i]["DailyStartTime"]);
        //        TableCell tcpm = new TableCell();
        //        tcpm.Text = string.Format("<span class='span1'> 联系电话：{0}</span>", dtNoUploadReporterList.Rows[i]["Mobile"]);
        //        TableCell tcpe = new TableCell();
        //        tcpe.Text = string.Format("<span class='span1'>Email：{0}</span>", dtNoUploadReporterList.Rows[i]["Email"]);
        //        TableCell tcpu = new TableCell();
        //        tcpu.Text = string.Format("<asp:Button ID='btnUpload' Text='现在上传' runat='server' OnClick='btnUpload_Click'  class='widebuttons' />");

        //        trp.Controls.Add(tcpname);
        //        trp.Controls.Add(tcppn);
        //        trp.Controls.Add(tcpdn);
        //        trp.Controls.Add(tcpdst);
        //        trp.Controls.Add(tcpm);
        //        trp.Controls.Add(tcpe);
        //        trp.Controls.Add(tcpu);
        //    }
        //    phNoUploadReporterList.Controls.Add(tbp);

        //}
    }
    #endregion

    #region 上传
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        string pjid = "";
      //  Response.Redirect(string.Format("Bill/WritingFeeBillList.aspx?Pjid={0}", pjid));
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("window.lication='Bill/WritingFeeBillList.aspx?{0}={1}');window.close();",RequestName.ProjectID, pjid), true);
    }
    #endregion



}
