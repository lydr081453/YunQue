using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.HumanResource.BusinessLogic;
using ESP.HumanResource.Common;

namespace SEPAdmin.Management.UserManagement
{
    public partial class UpLoadUserPhoto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;
            if (!string.IsNullOrEmpty(Request.QueryString["Picurl"]))
            {
                ImageDrag.ImageUrl = Request.QueryString["Picurl"];
                ImageIcon.ImageUrl = Request.QueryString["Picurl"];
                Page.ClientScript.RegisterStartupScript(typeof(UpLoadUserPhoto), "step2", "<script type='text/javascript'>Step2();</script>");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(typeof(UpLoadUserPhoto), "step1", "<script type='text/javascript'>Step1();</script>");
            }

            if (!string.IsNullOrEmpty(Request["userid"]))
            {
                ESP.Framework.Entity.EmployeeInfo emp = ESP.Framework.BusinessLogic.EmployeeManager.Get(int.Parse(Request["userid"].ToString()));
                if (!string.IsNullOrEmpty(emp.Photo))
                {
                    imgphoto.ImageUrl = savepath + emp.Photo;
                }
            }
        }

        private const string savepath = "F:/web/S-Portal/images/upload/";

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (fuPhoto.PostedFile != null && fuPhoto.PostedFile.ContentLength > 0)
            {

                string ext = System.IO.Path.GetExtension(fuPhoto.PostedFile.FileName).ToLower();
                if (ext != ".jpg" && ext != ".jepg" && ext != ".bmp" && ext != ".gif")
                {
                    return;
                }
                string filename = "xy_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ext;
                string path = savepath + filename;
                fuPhoto.PostedFile.SaveAs(path);
                Response.Redirect("UpLoadUserPhoto.aspx?Picurl=" + Server.UrlEncode(path) + "&userid=" + Request["userid"]);
            }
            else
            {
                //do some thing;
            }
        }

        protected void btn_Image_Click(object sender, EventArgs e)
        {
            int imageWidth = Int32.Parse(txt_width.Text);
            int imageHeight = Int32.Parse(txt_height.Text);
            int cutTop = Int32.Parse(txt_top.Text);
            int cutLeft = Int32.Parse(txt_left.Text);
            int dropWidth = Int32.Parse(txt_DropWidth.Text);
            int dropHeight = Int32.Parse(txt_DropHeight.Text);

            string filename = CutPhotoHelp.SaveCutPic(Server.MapPath(ImageIcon.ImageUrl), savepath, 0, 0, dropWidth,
                                    dropHeight, cutLeft, cutTop, imageWidth, imageHeight);

            this.imgphoto.ImageUrl = savepath + filename;
            EmployeeBaseManager.updateUserPhoto(int.Parse(Request["userid"].ToString()), filename);
            Page.ClientScript.RegisterStartupScript(typeof(UpLoadUserPhoto), "step3", "<script type='text/javascript'>Step3();</script>");
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["operation"]))
                Response.Redirect("EmpMgt.aspx?userid=" + Request["userid"]);
            else
                Response.Redirect("EmployeeEdit.aspx?userid=" + Request["userid"]);
        }
    }

}
