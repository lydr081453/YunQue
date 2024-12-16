using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.HumanResource.BusinessLogic;
using ESP.HumanResource.Entity;
using ESP.Framework.BusinessLogic;
using System.Net.Mail;
using System.IO;
using ESP.HumanResource.Common;
using SEPAdmin.Management.UserManagement;

namespace SEPAdmin.HR.Employees
{
    public partial class Settings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// 上传图片 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void uploadImage_Click(object sender, EventArgs e)
        {
            HttpContext context = new HttpContext(Request, Response);
            filePath = UploadFile(context);
            if (!string.IsNullOrEmpty(filePath))
            {
                ImageDrag.ImageUrl = filePath;
                ImageIcon.ImageUrl = filePath;
                btn_Image.Visible = true;
                btn_Image.Focus();
            }
        }

        private string filePath = string.Empty;

        /// <summary>
        /// 保存头像
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Image_Click(object sender, EventArgs e)
        {
            int curUserId = int.Parse(Request["UserId"]);
            if (this.ImageDrag.ImageUrl != "../../public/CutImage/image/blank.jpg" && this.ImageIcon.ImageUrl != "../../public/CutImage/image/blank.jpg")
            {
                int imageWidth = Int32.Parse(txt_width.Text);
                int imageHeight = Int32.Parse(txt_height.Text);
                int cutTop = Int32.Parse(txt_top.Text);
                int cutLeft = Int32.Parse(txt_left.Text);
                int dropWidth = Int32.Parse(txt_DropWidth.Text);
                int dropHeight = Int32.Parse(txt_DropHeight.Text);

                string filename = CutPhotoHelp.SaveCutPic(Server.MapPath(ImageIcon.ImageUrl), Server.MapPath(savepath), 0, 0, dropWidth,
                                        dropHeight, cutLeft, cutTop, imageWidth, imageHeight);

                this.imgphoto.ImageUrl = savepath + filename;

                System.IO.File.Copy(Server.MapPath(savepath) + filename, System.Configuration.ConfigurationManager.AppSettings["UserIconPath"] + filename);
                EmployeeBaseManager.updateUserPhoto(curUserId, filename);
                Page.ClientScript.RegisterStartupScript(typeof(UpLoadUserPhoto), "step3", "<script type='text/javascript'>Step3();</script>");
            }
            else
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('请先上传您的头像!');", true);
            }
        }

        private const string savepath = "/UserImage/UserHeadImage" + "/";
        public string UploadFile(HttpContext context)
        {
            string result = string.Empty;
            HttpFileCollection fileCollection = context.Request.Files;//获取上传文件的集合
            string dirpath = Server.MapPath(savepath);
            if (fileCollection.Count != 0)
            {
                for (int i = 0; i < fileCollection.Count; i++)
                {
                    HttpPostedFile file = fileCollection[i];//获取单个文件
                    if (file.ContentLength == 0) //如果该文件大小为0
                    {
                        continue;
                    }
                    string fileName = string.Empty;
                    string fileExtention = string.Empty;
                    fileExtention = System.IO.Path.GetExtension(file.FileName); //获取文件后缀
                    if (fileExtention.ToLower().Equals(".png") || fileExtention.ToLower().Equals(".bmp") || fileExtention.ToLower().Equals(".jpg") || fileExtention.ToLower().Equals(".gif"))
                    {
                        fileName = "xy_" + DateTime.Now.ToString("yyyyMMddHHmmss") + fileExtention;
                        dirpath = (dirpath + fileName).Replace(@"\\", @"\");
                        file.SaveAs(dirpath);
                        result = savepath + fileName;
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "", "alert('无效的图片格式!')", true);
                        return "";
                    }
                }
            }
            return result;
        }

    }
}
