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
public partial class Dialogs_ContractFileDownLoad : ESP.Web.UI.PageBase
{
    private string GetFileName()
    {
        string attachment = null;
        ContractInfo item = null;
         PaymentDetailInfo detailModel=null;
         PNBatchInfo batchModel = null;

        string pContractID = Request[ESP.Finance.Utility.RequestName.ContractID];
        ESP.Purchase.Entity.DataInfo permissionModel=null;
        if (!string.IsNullOrEmpty(pContractID))
        {
            int contractId;
            if (int.TryParse(pContractID, out contractId))
            {
                if (!string.IsNullOrEmpty(Request["FileType"]))
                {
                    detailModel = ESP.Finance.BusinessLogic.PaymentDetailManager.GetModel(contractId);
                    PaymentInfo paymentModel = ESP.Finance.BusinessLogic.PaymentManager.GetModel(detailModel.PaymentID);
                    permissionModel = ESP.Purchase.BusinessLogic.DataPermissionManager.GetDataInfoModel(2, paymentModel.ProjectID);
                    if (permissionModel != null && CurrentUser != null)
                    {
                        bool edit = ESP.Purchase.BusinessLogic.DataPermissionManager.CheckUserEditPermission(2, item.ProjectID, int.Parse(CurrentUser.SysID));
                        bool view = ESP.Purchase.BusinessLogic.DataPermissionManager.CheckUserViewPermission(2, item.ProjectID, int.Parse(CurrentUser.SysID));
                        bool max = ESP.Purchase.BusinessLogic.DataPermissionManager.isMaxPermissionUser(int.Parse(CurrentUser.SysID));
                        if (edit == false && view == false && max == false)
                        {
                            return null;
                        }
                    }
                    else
                    { return null; }
                }
                if (!string.IsNullOrEmpty(Request["consumption"]) || !string.IsNullOrEmpty(Request["RebateRegistration"]))
                {
                    batchModel = ESP.Finance.BusinessLogic.PNBatchManager.GetModel(contractId);
                }
                else
                {
                    item = ESP.Finance.BusinessLogic.ContractManager.GetModel(contractId);
                    permissionModel = ESP.Purchase.BusinessLogic.DataPermissionManager.GetDataInfoModel(2, item.ProjectID);
                    if (permissionModel != null && CurrentUser != null)
                    {
                        bool edit = ESP.Purchase.BusinessLogic.DataPermissionManager.CheckUserEditPermission(2, item.ProjectID, int.Parse(CurrentUser.SysID));
                        bool view = ESP.Purchase.BusinessLogic.DataPermissionManager.CheckUserViewPermission(2, item.ProjectID, int.Parse(CurrentUser.SysID));
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
        }

        if (item != null)
        {
            attachment = item.Attachment;
        }
        else if (detailModel != null)
        {
            attachment = detailModel.FileUrl;
        }
        else if (batchModel != null)
        {
            attachment = batchModel.ProveFile;
        }

        if (!string.IsNullOrEmpty(attachment))
        {
            string contractPath = ESP.Finance.Configuration.ConfigurationManager.ContractPath + attachment;

            return contractPath;
        }
        return null;
    }

    private void DownloadFile(string filename)
    {
        //打开要下载的文件
        System.IO.FileStream r = new System.IO.FileStream(filename, System.IO.FileMode.Open);
        //设置基本信息
        Response.Buffer = false;
        Response.AddHeader("Connection", "Keep-Alive");
        Response.ContentType = "application/octet-stream";
        Response.AddHeader("Content-Disposition", "attachment;filename=" + System.IO.Path.GetFileName(filename));
        Response.AddHeader("Content-Length", r.Length.ToString());

        try
        {
            while (true)
            {
                //开辟缓冲区空间
                byte[] buffer = new byte[1024];
                //读取文件的数据
                int leng = r.Read(buffer, 0, 1024);
                if (leng == 0)//到文件尾，结束
                    break;
                if (leng == 1024)//读出的文件数据长度等于缓冲区长度，直接将缓冲区数据写入
                    Response.BinaryWrite(buffer);
                else
                {
                    //读出文件数据比缓冲区小，重新定义缓冲区大小，只用于读取文件的最后一个数据块
                    byte[] b = new byte[leng];
                    for (int i = 0; i < leng; i++)
                        b[i] = buffer[i];
                    Response.BinaryWrite(b);
                }
            }
        }
        catch(Exception ex)
        {
            Response.ClearHeaders();
            Response.ClearContent();
            Response.Write(ex.Message);
        }
        r.Close();//关闭下载文件
        Response.End();//结束文件下载
    }
 
    protected void Page_Load(object sender, EventArgs e)
    {      
        #region "原来的代码"
        try
        {
            if (CurrentUser == null)
            {
                Response.Write("当前用户尚未登录！");
                return;
            }

            string file = GetFileName();

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
        #endregion
      
    }
}
