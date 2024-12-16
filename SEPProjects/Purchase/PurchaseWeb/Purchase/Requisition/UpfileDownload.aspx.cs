using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Microsoft.Win32;
using ESP.Finance.Entity;
using ESP.Finance.BusinessLogic;
using System.Configuration;
using System.Text;
using ESP.Finance.Utility;

namespace PurchaseWeb.Purchase.Requisition
{
    public partial class UpfileDownload : ESP.Web.UI.PageBase
    {
        private string GetFileName()
        {
            string attachment = null;
            ESP.Purchase.Entity.OrderInfo item = null;
            string orderId = Request["OrderId"];
            string[] links = null;
            ESP.Purchase.Entity.DataInfo permissionModel = null;
            int index = 0;
            if (!string.IsNullOrEmpty(Request["RecipientId"]))
            {
                int rid = int.Parse(Request["RecipientId"]);
                ESP.Purchase.Entity.RecipientInfo rmodel = ESP.Purchase.BusinessLogic.RecipientManager.GetModel(rid);
                attachment =rmodel.FileUrl;
                links = new string[] { attachment };
            }
            else if (!string.IsNullOrEmpty(orderId))
            {
                index = int.Parse(Request["Index"]); 
                int contractId;
                if (int.TryParse(orderId, out contractId))
                {
                    item = ESP.Purchase.BusinessLogic.OrderInfoManager.GetModel(contractId);
                    permissionModel = ESP.Purchase.BusinessLogic.DataPermissionManager.GetDataInfoModel(0, item.general_id);
                    if (permissionModel != null && CurrentUser != null)
                    {
                        bool edit = ESP.Purchase.BusinessLogic.DataPermissionManager.CheckUserEditPermission(0, item.general_id, int.Parse(CurrentUser.SysID));
                        bool view = ESP.Purchase.BusinessLogic.DataPermissionManager.CheckUserViewPermission(0, item.general_id, int.Parse(CurrentUser.SysID));
                        bool max = ESP.Purchase.BusinessLogic.DataPermissionManager.isMaxPermissionUser(int.Parse(CurrentUser.SysID));
                        if (edit == false && view == false && max == false)
                        {
                            return null;
                        }
                    }
                    else
                    { return null; }
                }
            }

            if (item != null)
            {
                attachment = item.upfile;
                links = attachment.TrimEnd('#').Split('#');
            }

            if (!string.IsNullOrEmpty(attachment))
            {
                // string upfilePath = "/";
                // upfilePath = ESP.Utilities.UrlUtility.ConcatUrl(HttpRuntime.AppDomainAppVirtualPath, upfilePath);

                attachment = ESP.Purchase.Common.ServiceURL.UpFilePath + links[index];

                return attachment;
            }
            return null;
        }

        private string GetPolicyFileName()
        {
            string attachment = null;
            int orderId = int.Parse(Request["OrderId"]);
            ESP.Purchase.Entity.PolicyFlowInfo item = ESP.Purchase.BusinessLogic.PolicyFlowManager.GetModel(orderId);
            attachment = ESP.Purchase.Common.ServiceURL.UpFilePath + item.contents;
            return attachment;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (CurrentUser == null)
                {
                    Response.Write("当前用户尚未登录！");
                    return;
                }
                string file = string.Empty;
                if (!string.IsNullOrEmpty(Request["policy"]))
                    file = GetPolicyFileName();
                else
                    file = GetFileName();
                if (file != null && file.Length > 0)
                {
                    string filename = file;
                    string saveFileName = System.IO.Path.GetFileName(file);
                    string fileextname = System.IO.Path.GetExtension(file);
                    string DEFAULT_CONTENT_TYPE = "application/unknown";
                    string filecontenttype;
                    try
                    {
                        RegistryKey regkey = Registry.ClassesRoot;
                        RegistryKey fileextkey = regkey.OpenSubKey(fileextname);
                        object obj = fileextkey.GetValue("Content Type", DEFAULT_CONTENT_TYPE);
                        filecontenttype = obj == null ? DEFAULT_CONTENT_TYPE : obj.ToString();
                    }
                    catch
                    {
                        filecontenttype = DEFAULT_CONTENT_TYPE;
                    }

                    string downLoadName = HttpUtility.UrlEncode(saveFileName, Encoding.UTF8).Replace("+", "%20");
                    int key = downLoadName.IndexOf("__");
                    if (key > -1)
                    {
                        downLoadName = downLoadName.Substring(key + 2);
                    }

                    Response.Clear();
                    //Response.Charset = "utf-8";
                    Response.Buffer = true;
                    //this.EnableViewState = false;
                    Response.ContentEncoding = System.Text.Encoding.UTF8;


                    Response.AppendHeader("Content-Disposition", "attachment;filename=" + downLoadName);
                    Response.ContentType = filecontenttype;

                    Response.WriteFile(filename);
                    Response.Flush();
                    //Response.Close();

                }
                else
                {
                    throw new FileNotFoundException(null, file);
                }
            }
            catch (FileNotFoundException ex)
            {
                Response.ClearHeaders();
                Response.ClearContent();

                string f = ex.FileName;
                if (f == null)
                    f = "";
                int index = f.LastIndexOf(System.IO.Path.DirectorySeparatorChar);
                if (index >= 0)
                    f = f.Substring(index + 1);
                Response.Write("系统找不到指定文件 " + f + " ，该文件可以已被删除或移动。由此带来的不便，敬请谅解！");
            }
            catch (System.Exception ex)
            {
                Response.ClearHeaders();
                Response.ClearContent();
                Response.Write(ex.Message);
            }
            Response.End();
        }
    }
}
