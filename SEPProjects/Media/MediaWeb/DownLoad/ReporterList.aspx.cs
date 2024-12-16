using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using ESP.Media.BusinessLogic;

public partial class DownLoad_ReporterList : ESP.Web.UI.PageBase
{
    private void outExcel(string pathandname, string filename, bool isDelete)
    {
        if (!File.Exists(pathandname))
            return;
        FileStream fin = File.OpenRead(pathandname);
        Response.AddHeader("Content-Disposition", "attachment;   filename=" + filename);
        Response.AddHeader("Connection", "Close");
        Response.AddHeader("Content-Transfer-Encoding", "binary");
        Response.ContentType = "application/octet-stream";

        byte[] buf = new byte[1024];
        while (true)
        {
            int length = fin.Read(buf, 0, buf.Length);
            if (length > 0)
                Response.OutputStream.Write(buf, 0, length);
            if (length < buf.Length)
                break;
        }
        fin.Close();
        Response.Flush();
        Response.Close();
        if (isDelete)
        {
            FileInfo finfo = new FileInfo(pathandname);
            finfo.Delete();
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

        string filename = string.Empty;
        bool isSigned = false;
        if (!string.IsNullOrEmpty(Request["ExportType"]))
        {
            if (Request["ExportType"].ToLower() == "sign")
            {
                isSigned = true;
                filename = string.Format("Sign{0}.xls", DateTime.Now.Ticks.ToString());
            }
            else
            {
                filename = string.Format("Contract{0}.xls", DateTime.Now.Ticks.ToString());
            }
        }
        string term = string.Empty;
        if (!string.IsNullOrEmpty(Request["Term"]))
        {
            term = Request["Term"];
            if (term.Length > 0)
            {
                term = " and Reporterid in (" + term + ")";
            }
        }
        DataTable dt = ReportersManager.GetList(term, null);

        string fname = string.Empty;
        string errmsg = string.Empty;
        bool isdel = false;
        string filepath = string.Empty;
        filename = Server.MapPath(ESP.Media.Access.Utilities.ConfigManager.BillPath + filename);
        if (isSigned)
        {

            filepath = ExcelExportManager.SaveSignExcel(dt, filename, out fname, out isdel, out errmsg, Convert.ToInt32(CurrentUser.SysID));
        }
        else
        {

            filepath = ExcelExportManager.SaveCommunicateExcel(dt, filename, out fname, out isdel, out errmsg, Convert.ToInt32(CurrentUser.SysID));
        }
        if (filepath != null && filepath.Length > 0)
        {
            outExcel(filepath, fname, isdel);
        }
    }
}