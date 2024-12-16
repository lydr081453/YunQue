using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
namespace Portal.Common
{
    public class ImageHelper
    {
        /// <summary>
        /// 保存图片
        /// 保存缩略图，JPEG格式
        /// 如果图片尺寸超过最大尺寸则生成最大尺寸缩略图
        /// 如果图片本身尺寸没有超过最大尺寸，但是大小超过限制，则生成压缩图片
        /// </summary>
        /// <param name="input"></param>
        /// <param name="filename"></param>
        /// <param name="ext"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static string SaveIcon(Stream input, int userid, string oldfn)
        {
            Bitmap image = new Bitmap(input);
            int left = 0, top = 0, width = 0;
            if (image.Width > image.Height)
            {
                left = (image.Width - image.Height) / 2;
                top = 0;
                width = image.Height;
            }
            else if (image.Width < image.Height)
            {
                left = 0;
                top = (image.Height - image.Width) / 2;
                width = image.Width;
            }
            else
            {
                left = top = 0;
                width = image.Width;
            }

            //清除之前的图标
            if (oldfn != "")
            {
                DeleteIcon(oldfn);
            }

            string fn = DateTime.Now.Ticks.ToString();
            Random random = new Random(userid);
            fn += random.Next().ToString();

            //保存原图尺寸或最大尺寸图形，图片将被转换为JPEG格式
            Rectangle r = new Rectangle(left, top, width, width);
            Rectangle destOri = new Rectangle(0, 0, image.Width, image.Height);
            Rectangle destSmall = new Rectangle(0, 0, (int)Portal.Common.Global.UserIconSize.Small, (int)Portal.Common.Global.UserIconSize.Small);
            Rectangle destNormal = new Rectangle(0, 0, (int)Portal.Common.Global.UserIconSize.Normal, (int)Portal.Common.Global.UserIconSize.Normal);
            Rectangle destLarge = new Rectangle(0, 0, (int)Portal.Common.Global.UserIconSize.Large, (int)Portal.Common.Global.UserIconSize.Large);
            SaveThumbnailImage(image, destOri, Portal.Common.Global.USER_ICON_FOLDER + fn);//images/upload/
            //SaveThumbnailImage(image, destSmall, r, Portal.Common.Global.USER_ICON_FOLDER + fn + "_" + (int)Portal.Common.Global.UserIconSize.Small);
            //SaveThumbnailImage(image, destNormal, r, Portal.Common.Global.USER_ICON_FOLDER + fn + "_" + (int)Portal.Common.Global.UserIconSize.Normal);
            //if (image.Width < (int)Portal.Common.Global.UserIconSize.Large || image.Height < (int)Portal.Common.Global.UserIconSize.Large)
            //    SaveThumbnailImage(image, destOri, r, Portal.Common.Global.USER_ICON_FOLDER + fn + "_" + (int)Portal.Common.Global.UserIconSize.Large);
            //else
            //    SaveThumbnailImage(image, destLarge, r, Portal.Common.Global.USER_ICON_FOLDER + fn + "_" + (int)Portal.Common.Global.UserIconSize.Large);
            return fn;
        }

        public static string SaveIcon(Stream input, int userid, string oldfn,bool hrWebSave)
        {
            Bitmap image = new Bitmap(input);
            int left = 0, top = 0, width = 0;
            if (image.Width > image.Height)
            {
                left = (image.Width - image.Height) / 2;
                top = 0;
                width = image.Height;
            }
            else if (image.Width < image.Height)
            {
                left = 0;
                top = (image.Height - image.Width) / 2;
                width = image.Width;
            }
            else
            {
                left = top = 0;
                width = image.Width;
            }

            //清除之前的图标
            if (oldfn != "")
            {
                DeleteIcon(oldfn);
            }

            string fn = DateTime.Now.Ticks.ToString();
            Random random = new Random(userid);
            fn += random.Next().ToString();

            //保存原图尺寸或最大尺寸图形，图片将被转换为JPEG格式
            Rectangle r = new Rectangle(left, top, width, width);
            Rectangle destOri = new Rectangle(0, 0, image.Width, image.Height);
            Rectangle destSmall = new Rectangle(0, 0, (int)Portal.Common.Global.UserIconSize.Small, (int)Portal.Common.Global.UserIconSize.Small);
            Rectangle destNormal = new Rectangle(0, 0, (int)Portal.Common.Global.UserIconSize.Normal, (int)Portal.Common.Global.UserIconSize.Normal);
            Rectangle destLarge = new Rectangle(0, 0, (int)Portal.Common.Global.UserIconSize.Large, (int)Portal.Common.Global.UserIconSize.Large);
            SaveThumbnailImage(image, destOri, Portal.Common.Global.USER_ICON_FOLDER + fn,hrWebSave);//images/upload/
            
            return fn;
        }

        /// <summary>
        /// 保存缩略图
        /// 图像固定为JPEG格式，后缀为.jpg
        /// </summary>
        /// <param name="img"></param>
        /// <param name="r"></param>
        /// <param name="filename"></param>
        public static void SaveThumbnailImage(Image img, Rectangle r, string filename ,bool hrWebSave)
        {
            using (Bitmap thumbnail = new Bitmap(r.Width, r.Height))
            {
                Graphics g = Graphics.FromImage(thumbnail);
                filename = filename.ToLower().IndexOf(".jpg") > 0 ? filename : filename + ".jpg";
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(img, r);
                thumbnail.Save(Portal.Common.Global.USER_ICON_PATH +filename, ImageFormat.Jpeg);
                thumbnail.Dispose();
                g.Dispose();
            }
        }


        /// <summary>
        /// 保存缩略图
        /// 图像固定为JPEG格式，后缀为.jpg
        /// </summary>
        /// <param name="img"></param>
        /// <param name="r"></param>
        /// <param name="filename"></param>
        public static void SaveThumbnailImage(Image img, Rectangle r, string filename)
        {
            using (Bitmap thumbnail = new Bitmap(r.Width, r.Height))
            {
                Graphics g = Graphics.FromImage(thumbnail);
                filename = filename.ToLower().IndexOf(".jpg") > 0 ? filename : filename + ".jpg";
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(img, r);
                thumbnail.Save(System.Web.HttpContext.Current.Server.MapPath(filename), ImageFormat.Jpeg);
                thumbnail.Dispose();
                g.Dispose();
            }
        }

        public static void SaveThumbnailImage(Image img, Rectangle dest, Rectangle src, string filename)
        {
            using (Bitmap thumbnail = new Bitmap(dest.Width, dest.Height))
            {
                Graphics g = Graphics.FromImage(thumbnail);
                filename = filename.ToLower().IndexOf(".jpg") > 0 ? filename : filename + ".jpg";
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(img, dest, src, GraphicsUnit.Pixel);
                thumbnail.Save(System.Web.HttpContext.Current.Server.MapPath(filename), ImageFormat.Jpeg);
                thumbnail.Dispose();
                g.Dispose();
            }
        }

        /// <summary>
        /// 生成缩略图所需的矩形结构
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="widthSource"></param>
        /// <param name="heightSource"></param>
        /// <returns></returns>
        public static Rectangle ZoomRectangle(int width, int height, int widthSource, int heightSource)
        {
            Rectangle r = new Rectangle();
            if (widthSource < width && heightSource < height)
            {
                r = new Rectangle(0, 0, widthSource, heightSource);
            }
            else
            {
                if (widthSource / width <= heightSource / height)
                {
                    r = new Rectangle(0, 0, (int)(widthSource * height / heightSource), height);
                }
                else
                {
                    r = new Rectangle(0, 0, width, (int)(heightSource * width / widthSource));
                }
            }
            return r;
        }

        public static string GetIconVirtualPath(string filename, int size)
        {
            if (filename == null || filename.Trim() == "")
            {
                return "/images/user.jpg";
            }
            if (filename.IndexOf(Portal.Common.Global.USER_ICON_FOLDER) < 0)
            {
                filename = Portal.Common.Global.USER_ICON_FOLDER + filename;
            }
            string fn = System.Web.HttpContext.Current.Server.MapPath(filename);
            fn = fn.ToLower().Replace(".jpg", "") + "_" + size + ".jpg";
            if (File.Exists(fn))
            {
                return filename.ToLower().Replace(".jpg", "") + "_" + size + ".jpg";
            }
            else
            {
                return filename;
            }
        }

        public static void DeleteIcon(string oldfn)
        {
            if (oldfn.IndexOf(Portal.Common.Global.USER_ICON_FOLDER) < 0)
            {
                oldfn += Portal.Common.Global.USER_ICON_FOLDER;
            }
            string s = System.Web.HttpContext.Current.Server.MapPath(oldfn.Replace(".jpg", (int)Portal.Common.Global.UserIconSize.Small + ".jpg"));
            if (File.Exists(s))
            {
                File.Delete(s);
            }
            s = System.Web.HttpContext.Current.Server.MapPath(oldfn.Replace(".jpg", (int)Portal.Common.Global.UserIconSize.Normal + ".jpg"));
            if (File.Exists(s))
            {
                File.Delete(s);
            }
            s = System.Web.HttpContext.Current.Server.MapPath(oldfn.Replace(".jpg", (int)Portal.Common.Global.UserIconSize.Large + ".jpg"));
            if (File.Exists(s))
            {
                File.Delete(s);
            }
            s = System.Web.HttpContext.Current.Server.MapPath(oldfn);
            if (File.Exists(s))
            {
                File.Delete(s);
            }
        }

        public static string UserIconFullPath(int userid)
        {
            ESP.Framework.Entity.EmployeeInfo info = ESP.Framework.BusinessLogic.EmployeeManager.Get(userid);
            if (info != null && info.Photo.Trim().Length > 0)
            {
                return ESP.Framework.BusinessLogic.WebSiteManager.Get(ESP.Framework.BusinessLogic.SettingManager.Get().PortalWebSite).HttpRootUrl + Portal.Common.Global.USER_ICON_FOLDER + info.Photo.Trim() + ".jpg";
            }
            else
            {
                return "";
            }
        }

        public static string UserIconFullPath(int userid, Portal.Common.Global.UserIconSize size)
        {
            ESP.Framework.Entity.EmployeeInfo info = ESP.Framework.BusinessLogic.EmployeeManager.Get(userid);
            if (info != null && info.Photo.Trim().Length > 0)
            {
                return ESP.Framework.BusinessLogic.WebSiteManager.Get(ESP.Framework.BusinessLogic.SettingManager.Get().PortalWebSite).HttpRootUrl + Portal.Common.Global.USER_ICON_FOLDER + info.Photo.Trim() + "_" + (int)size + ".jpg";
            }
            else
            {
                return "";
            }
        }
    }
}
