using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace SEPAdmin.HR.Join
{
    /// <summary>
    /// Handler1 的摘要说明
    /// </summary>
    public class UploadAvater : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            if (context.Request.Files.Count > 0)
            {
                HttpPostedFile file = context.Request.Files[0];

                string filename = context.Request["name"] + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
                string savePath = Portal.Common.Global.USER_ICON_PATH + Portal.Common.Global.USER_ICON_FOLDER + filename;
                //string savePath = context.Server.MapPath("~/UserImage/UserHeadImage/") + filename;
                try
                {
                    file.SaveAs(savePath);
                    ESP.HumanResource.BusinessLogic.EmployeeBaseManager.updateUserPhoto(int.Parse(context.Request["uid"]), filename);
                    context.Response.Write("File uploaded successfully");
                }
                catch (Exception ex)
                {
                    context.Response.Write("File upload failed: " + ex.Message);
                }
            }
            else
            {
                context.Response.Write("No file received");
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