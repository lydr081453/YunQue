using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.ConfigCommon;
using ESP.Supplier.BusinessLogic;
using ESP.Supplier.Entity;

namespace SupplierWeb.UserInfo
{
    public partial class UserInfoUpdateLinkMan : System.Web.UI.Page
    {
        private int _supplierId = 0;
        private int _linkmanId = 0;

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

            if(null !=Request["lid"] && !string.IsNullOrEmpty(Request["lid"]))
            {
                _linkmanId = int.Parse(Request["lid"].ToString());
            }
            if (!IsPostBack)
            {
                if (_linkmanId > 0)
                {
                    BindInfo((new SC_LinkManManager()).GetModel(_linkmanId));
                }
            }
        }

        private void BindInfo(SC_LinkMan model)
        {
            if (null != model)
            {
                txtName.Text = model.Name;
                rblSex.SelectedValue = model.Sex.ToString();
                txtTitle.Text = model.Title;
                txtTel.Text = model.Tel;
                txtMobile.Text = model.Mobile;
                txtFax.Text = model.Fax;
                txtAddress.Text = model.Address;
                txtZIP.Text = model.ZIP;
                txtEmail.Text = model.Email;
                txtQQ.Text = model.QQ;
                txtMSN.Text = model.MSN;
                txtNote.Text = model.Note;
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
                //if (ext.Length == 0)
                //{
                //    Page.ClientScript.RegisterStartupScript(typeof(string), "ExtNotAllow", "alert('封面图片格式不对，需要：GIF、JPEG或BMP文件格式。');", true);
                //    return false;
                //}
                //else
                //{
                //    b.bytes = fileCover.PostedFile.ContentLength;
                //    b.photoname = txtCoverName.Text.Trim();
                //    PhotoInfo pi = ImageHelper.SavePhoto(fileCover.PostedFile.InputStream, Config.PhotoSizeSettings.LARGESIZE, fileCover.PostedFile.ContentLength, LoginUser.id, b.id);
                //    b.filename = pi.filename;
                //    b.width = pi.size.width;
                //    b.height = pi.size.height;
                //    foreach (Pair pr in pi.exif)
                //    {
                //        b.exif += pr.First + ":" + pr.Second + "\r\n";
                //    }
                //}
            }

            SC_LinkManManager slm = new SC_LinkManManager();
            SC_LinkMan model;
            if(_linkmanId >0)
            {
                model = slm.GetModel(_linkmanId);
            }
            else
            {
                model = new SC_LinkMan();
                model.SupplierId = _supplierId;
                model.CreatedIP = Page.Request.UserHostAddress;
            }

            model.Name = txtName.Text.Trim();
            model.Sex = int.Parse(rblSex.SelectedValue);
            model.Title = txtTitle.Text.Trim();
            model.Tel = txtTel.Text.Trim();
            model.Mobile = txtMobile.Text.Trim();
            model.Fax = txtFax.Text.Trim();
            model.Address = txtAddress.Text.Trim();
            model.ZIP = txtZIP.Text.Trim();
            model.Email = txtEmail.Text.Trim();
            model.QQ = txtQQ.Text.Trim();
            model.MSN = txtMSN.Text.Trim();
            model.Note = txtNote.Text.Trim();
            model.LastModifiedIP = Page.Request.UserHostAddress;

            if(_linkmanId >0)
            {
                slm.Update(model);
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('联系人信息更新成功!');window.location='UserInfo.aspx';", true);
            }
            else
            {
                if (slm.Add(model) > 0)
                {
                    ClientScript.RegisterStartupScript(typeof(string), "", "alert('联系人信息添加成功!');window.location='UserInfo.aspx';", true);
                }
                else
                    ClientScript.RegisterStartupScript(typeof(string), "", "alert('联系人信息添加失败!');window.location='UserInfo.aspx';", true);
            }

        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserInfo.aspx");
        }
    }
}
