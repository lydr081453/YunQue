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

namespace SEPAdmin.Talents
{
    public partial class UpfileDownload : ESP.Web.UI.PageBase
    {
        private string GetFileName()
        {
            string attachment = null;
            
            string id = Request["id"];

            ESP.HumanResource.Entity.TalentInfo model = (new ESP.HumanResource.BusinessLogic.TalentManager()).GetModel(int.Parse(id));

            if (model != null)
            {
                attachment = model.ResumeFiles;
            }

            if (!string.IsNullOrEmpty(attachment))
            {
                attachment = ESP.Purchase.Common.ServiceURL.UpFilePath + attachment;

                return attachment;
            }
            return null;
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
                string file =  GetFileName();

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
