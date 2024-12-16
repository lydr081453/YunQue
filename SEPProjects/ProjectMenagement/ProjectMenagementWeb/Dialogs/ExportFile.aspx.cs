using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace FinanceWeb.Dialogs
{
    public partial class ExportFile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request["returnID"]) && !string.IsNullOrEmpty(Request["commandName"]))
                {
                    ExportCommand(int.Parse(Request["returnID"]), Request["commandName"]);
                }
                else if (!string.IsNullOrEmpty(Request["returnID"]) && !string.IsNullOrEmpty(Request["Page"]) && Request["Page"] == "ReturnTabEdit.aspx")
                {
                    ExportReturnTabEdit(int.Parse(Request["returnID"]));
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="returnID"></param>
        public void ExportReturnTabEdit(int returnID)
        {
            ESP.Finance.Entity.ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(returnID);
            if (!string.IsNullOrEmpty(returnModel.MediaOrderIDs))
            {
                string filename;
                string serverpath = Server.MapPath(ESP.Finance.Configuration.ConfigurationManager.MediaOrderPath);
                ESP.Finance.BusinessLogic.ReturnManager.ExportMediaOrderExcel(ESP.Finance.Utility.Common.CurrentUserSysID, returnModel, serverpath, out filename, false);
                if (!string.IsNullOrEmpty(filename))
                {
                    outExcel(serverpath + filename, filename, true);
                }
            }
        }

        /// <summary>
        /// 导出全部记者、导出未付款记者
        /// </summary>
        /// <param name="returnID"></param>
        /// <param name="commandName"></param>
        public void ExportCommand(int returnID, string commandName)
        {
            if (commandName == "Export")
            {
                ESP.Finance.Entity.ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(returnID);
                if (!string.IsNullOrEmpty(returnModel.MediaOrderIDs))
                {
                    string filename;
                    string serverpath = Server.MapPath(ESP.Finance.Configuration.ConfigurationManager.MediaOrderPath);
                    ESP.Finance.BusinessLogic.ReturnManager.ExportMediaOrderExcel(0, returnModel, serverpath, out filename, false);
                    if (!string.IsNullOrEmpty(filename))
                    {
                        outExcel(serverpath + filename, filename, true);
                    }
                }
                else
                {
                    return;
                }
            }
            else if (commandName == "Journalist")
            {
                ESP.Finance.Entity.ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(returnID);
                if (!string.IsNullOrEmpty(returnModel.MediaOrderIDs))
                {
                    string filename;
                    string serverpath = Server.MapPath(ESP.Finance.Configuration.ConfigurationManager.MediaOrderPath);
                    ESP.Finance.BusinessLogic.ReturnManager.ExportMediaOrderExcel(0, returnModel, serverpath, out filename, true);
                    if (!string.IsNullOrEmpty(filename))
                    {
                        outExcel(serverpath + filename, filename, true);
                    }
                }
                else
                {
                    return;
                }
            }
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.close();", true);
        }

        private void outExcel(string pathandname, string filename, bool isDelete)
        {
            if (!File.Exists(pathandname))
                return;
            FileStream fin = File.OpenRead(pathandname);
            Response.AddHeader("Content-Disposition", "attachment;   filename=" + filename);
            Response.AddHeader("Connection", "Close");
            Response.AddHeader("Content-Transfer-Encoding", "binary");
            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Length", fin.Length.ToString());
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
    }
}
