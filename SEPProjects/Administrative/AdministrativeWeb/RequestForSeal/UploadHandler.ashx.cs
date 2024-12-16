using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using ESP.Administrative.Entity;
using ESP.Administrative.BusinessLogic;

namespace AdministrativeWeb.RequestForSeal
{
    /// <summary>
    /// UploadHandler 的摘要说明
    /// </summary>
    public class UploadHandler : IHttpHandler
    {
        //RequestForSealManager manager = new RequestForSealManager();
        public void ProcessRequest(HttpContext context)
        {
            HttpFileCollection uploadedFiles = context.Request.Files;
            int maxSize = 10 * 1024 * 1024; // 10MB

            string[] allowedTypes = { "image/jpeg", "image/png", "application/pdf","application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                                      "application/vnd.openxmlformats-officedocument.wordprocessingml.document","application/msword","application/octet-stream",
                                      "application/vnd.ms-excel","message/rfc822","application/zip","application/x-rar-compressed"};
            if (uploadedFiles.Count > 0)
            {
                //int RfsId = int.Parse(context.Request["rfsId"]);
                //RequestForSealInfo model = manager.GetModel(RfsId);
                string fileName = "";
                for (int i = 0; i < uploadedFiles.Count; i++)
                {
                    HttpPostedFile uploadedFile = uploadedFiles[i];
                    if (uploadedFile.ContentLength > 0)
                    {
                        // 检查文件大小
                        if (uploadedFile.ContentLength > maxSize)
                        {
                            context.Response.StatusCode = 400;
                            context.Response.Write("文件大小超过10MB: " + uploadedFile.FileName);
                            return;
                        }

                        // 检查文件类型
                        if (Array.IndexOf(allowedTypes, uploadedFile.ContentType) < 0)
                        {
                            context.Response.StatusCode = 400;
                            context.Response.Write("不允许的文件类型: " + uploadedFile.FileName);
                            return;
                        }
                        fileName = Guid.NewGuid().ToString() + "_" + uploadedFile.FileName;
                        var path = ESP.Configuration.ConfigurationManager.SafeAppSettings["RequestForSealPath"];
                        string filePath = Path.Combine(path, Path.GetFileName(fileName));
                        uploadedFile.SaveAs(filePath);
                    }
                }
                //if (model != null && !string.IsNullOrEmpty(Files))
                //{
                //    model.Files = Files;
                //    manager.Update(model);
                //}
                context.Response.StatusCode = 200;
                context.Response.Write(fileName);
            }
            else
            {
                context.Response.StatusCode = 400;
                context.Response.Write("没有文件被上传。");
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}