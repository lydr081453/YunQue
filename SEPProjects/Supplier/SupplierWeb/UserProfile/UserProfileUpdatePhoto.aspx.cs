using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Supplier.Common;
using ESP.Supplier.DataAccess;
using ESP.Supplier.Entity;

namespace SupplierWeb.UserProfile
{
    public partial class UserProfileUpdatePhoto : System.Web.UI.Page
    {
        private int _supplierId = 0;
        private int _photoId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] != null && int.Parse(Session["user"].ToString()) > 0)
            {
                _supplierId = int.Parse(Session["user"].ToString());
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }
            if (null != Request["pid"] && !string.IsNullOrEmpty(Request["pid"]))
            {
                _photoId = int.Parse(Request["pid"].ToString());
            }
            if (!IsPostBack)
            {
                SC_SupplierPhoto model = (new SC_SupplierPhotoDataProvider()).GetModel(_photoId);

                if (null != model)
                {
                    BindInfo(model);
                }
            }

        }

        private void BindInfo(SC_SupplierPhoto model)
        {
            if (null != model)
            {
                txtShowTxt.Text = model.ShowTxt.ToString();
            }
        }


        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            //检查图片大小
            if (fileIcon.PostedFile.ContentLength > Config.PHOTO_CONTENT_LENGTH)
            {
                Page.ClientScript.RegisterStartupScript(typeof(string), "ContentLengthNotAllow", "alert('图片大小有误，请上传大小在1KB至2MB之间的图片。');", true);
                return;
            }

            SC_SupplierPhotoDataProvider dp = new SC_SupplierPhotoDataProvider();
            SC_SupplierPhoto model;
            if (_photoId > 0)
            {
                model = dp.GetModel(_photoId);
            }
            else
            {
                model = new SC_SupplierPhoto();
                model.SupplierId = _supplierId;
                model.CreatedIP = Page.Request.UserHostAddress;
                model.CreatTime = DateTime.Now;
            }

            model.ShowTxt = txtShowTxt.Text.Trim();

            //封面图片
            if (fileIcon.PostedFile.ContentLength > 0)
            {
                string ext = "";
                for (int i = 0; i < Config.PHOTO_CONTENT_TYPES.Length; ++i)
                {
                    if (fileIcon.PostedFile.ContentType == Config.PHOTO_CONTENT_TYPES[i])
                    {
                        ext = Config.PHOTO_EXTENSIONS[i];
                    }
                }
                if (ext.Length == 0)
                {
                    Page.ClientScript.RegisterStartupScript(typeof(string), "ExtNotAllow", "alert('封面图片格式不对，需要：GIF、JPEG或BMP文件格式。');", true);
                    return;
                }
                else
                {
                    model.PhotoUrl=ImageHelper.SaveIcon(fileIcon.PostedFile.InputStream, Config.PhotoSizeSettings.LARGESIZE, fileIcon.PostedFile.ContentLength, _supplierId, "").filename;
                }
            }

            //model.Content = txtContent.Text.Trim();
            model.LastUpdateTime = DateTime.Now;
            model.LastModifiedIP = Page.Request.UserHostAddress;

            if (_photoId > 0)
            {
                dp.Update(model);
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('信息更新成功!');window.location='UserProfile.aspx';", true);
            }
            else
            {
                if (dp.Add(model) > 0)
                {
                    ClientScript.RegisterStartupScript(typeof(string), "", "alert('信息添加成功!');window.location='UserProfile.aspx';", true);
                }
                else
                    ClientScript.RegisterStartupScript(typeof(string), "", "alert('信息添加失败!');window.location='UserProfile.aspx';", true);
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserProfile.aspx");
        }
    }
}
