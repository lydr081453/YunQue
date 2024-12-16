using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace FinanceWeb.project
{
    /// <summary>
    /// UploadHandler 的摘要说明
    /// </summary>
    public class UploadHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            HttpFileCollection uploadedFiles = context.Request.Files;
            int maxSize = 10 * 1024 * 1024; // 10MB

            string[] allowedTypes = { "image/jpeg", "image/png", "application/pdf","application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                                      "application/vnd.openxmlformats-officedocument.wordprocessingml.document","application/msword","application/octet-stream",
                                      "application/vnd.ms-excel","message/rfc822"};

            ESP.Framework.Entity.UserInfo current = ESP.Framework.BusinessLogic.UserManager.Get(int.Parse(context.User.Identity.Name.ToString()));
            if (uploadedFiles.Count > 0)
            {
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
                        string fileName ="Contract_" + Guid.NewGuid().ToString()+"_"+uploadedFile.FileName;
                        string filePath = Path.Combine(ESP.Finance.Configuration.ConfigurationManager.ContractPath, Path.GetFileName(fileName));
                        uploadedFile.SaveAs(filePath);

                        ESP.Finance.Entity.ContractInfo contract = new ESP.Finance.Entity.ContractInfo();
                        contract.Description = uploadedFile.FileName.Split('.')[0];
                        contract.ProjectID = int.Parse(context.Request["pid"]);
                        contract.Usable = true;
                        contract.CreateDate = DateTime.Now;
                        contract.CreatorUserId = current.UserID;
                        contract.CreatorUserName = current.LastNameCN + current.FirstNameCN;
                        contract.Status = (int)ESP.Finance.Utility.ContractStatus.Status.Wait_Submit;
                        contract.Attachment = fileName;
                        ESP.Finance.BusinessLogic.ContractManager.Add(contract);
                        
                    }
                }
                context.Response.StatusCode = 200;
                context.Response.Write("文件上传成功！");
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