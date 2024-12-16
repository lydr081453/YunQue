using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;

public partial class Download : ESP.Web.UI.PageBase
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
        DataTable dt = new DataTable();
        string filename = string.Empty;
        if(!string.IsNullOrEmpty(Request["FileName"]))
        {
            filename = Request["FileName"];
        }
        bool isSigned = false ;
        if (!string.IsNullOrEmpty(Request["ExportType"]))
        {
            if (Request["ExportType"].ToLower() == "sign")
            {
                isSigned = true;
            }
        }

        string fname = string.Empty;
        string errmsg = string.Empty;
        bool isdel = false;
        string filepath = string.Empty;
        if (isSigned)
        {
            
            filepath = ESP.Media.BusinessLogic.ExcelExportManager.SaveSignExcel(dt, filename, out fname, out isdel, out errmsg, Convert.ToInt32(CurrentUser.SysID));
        }
        else
        {
            filepath = ESP.Media.BusinessLogic.ExcelExportManager.SaveCommunicateExcel(dt, filename, out fname, out isdel, out errmsg, Convert.ToInt32(CurrentUser.SysID));
        }
        if (filepath != null && filepath.Length > 0)
        {
            //Response.Redirect(filepath);
            outExcel(filepath, fname, isdel);
        }
    }
}
