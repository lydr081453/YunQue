using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web;


namespace ESP.MediaLinq.Utilities
{
    public class ImageHelper
    {
        public const string WATERMARK = "";

        public static void DrawWatermark(Stream input)
        {
            Bitmap image = new Bitmap(input);
            Graphics g = Graphics.FromImage(image);
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            g.DrawString(WATERMARK, new Font(new FontFamily("宋体"), 12f, FontStyle.Bold), new SolidBrush(Color.WhiteSmoke), new PointF(image.Width - 100, image.Height - 100));
            image.Save("C:/test.jpg");
        }

        /// <summary>
        /// 保存图片
        /// 保存120和640大小的缩略图，JPEG格式
        /// 如果图片尺寸超过最大尺寸则生成最大尺寸缩略图
        /// 如果图片本身尺寸没有超过最大尺寸，但是大小超过限制，则生成压缩图片
        /// </summary>
        /// <param name="input"></param>
        /// <param name="filename"></param>
        /// <param name="ext"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static PhotoInfo SaveIcon(Stream input, int size, int length, int userid, string oldfn)
        {
            Bitmap image = new Bitmap(input);

            //计算不重复的文件名，此文件名包含id编号
            PhotoInfo info = new PhotoInfo();
            //info.exif = new EXIFextractor(ref image, "");
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
            Rectangle destSmall = new Rectangle(0, 0, Config.IconSize.SMALL, Config.IconSize.SMALL);
            Rectangle destNormal = new Rectangle(0, 0, Config.IconSize.NORMAL, Config.IconSize.NORMAL);
            Rectangle destLarge = new Rectangle(0, 0, Config.IconSize.LARGE, Config.IconSize.LARGE);
            SaveThumbnailImage(image, destOri, Config.USER_ICON_DIR + fn);
            SaveThumbnailImage(image, destSmall, r, Config.USER_ICON_DIR + fn + "_" + Config.IconSize.SMALL);
            SaveThumbnailImage(image, destNormal, r, Config.USER_ICON_DIR + fn + "_" + Config.IconSize.NORMAL);
            if (image.Width < Config.IconSize.LARGE || image.Height < Config.IconSize.LARGE)
                SaveThumbnailImage(image, destOri, r, Config.USER_ICON_DIR + fn + "_" + Config.IconSize.LARGE);
            else
                SaveThumbnailImage(image, destLarge, r, Config.USER_ICON_DIR + fn + "_" + Config.IconSize.LARGE);
            info.filename = fn + ".jpg";
            info.size.width = image.Width;
            info.size.height = image.Height;
            return info;
        }

        public static PhotoInfo SavePhoto(Stream input, int size, long length, string userid, string filepath)
        {
            Bitmap image = new Bitmap(input);
            PhotoInfo info = new PhotoInfo();

            //计算不重复的文件名，此文件名包含用户编号，乐活志编号。
            Random random = new Random((int)DateTime.Now.Ticks);
            string fn = userid.ToString() + "_" + DateTime.Now.Ticks.ToString() + random.Next(100, 999).ToString();

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

            //保存原图尺寸或最大尺寸图形，图片将被转换为JPEG格式
            Rectangle r = new Rectangle(left, top, width, width);
            Rectangle destOri = new Rectangle(0, 0, image.Width, image.Height);
            Rectangle destSmall = new Rectangle(0, 0, 87, 84);
            Rectangle destNormal = new Rectangle(0, 0, 110, 71);
            Rectangle destLarge = new Rectangle(0, 0, 282, 225);
            //SaveThumbnailImage(image, destOri, filepath + fn);
            SaveThumbnailImage(image, destOri, filepath + fn + "_full");
            //SaveThumbnailImage(image, destSmall, r, filepath + fn + "_87");
            SaveThumbnailImage(image, destSmall, r, filepath + fn);
            SaveThumbnailImage(image, destNormal, r, filepath + fn + "_110");
            if (image.Width < Config.IconSize.LARGE || image.Height < Config.IconSize.LARGE)
                SaveThumbnailImage(image, destOri, r, filepath + fn + "_225");
            else
                SaveThumbnailImage(image, destLarge, r, filepath + fn + "_225");
            info.filename = filepath + fn + ".jpg";
            info.size.width = image.Width;
            info.size.height = image.Height;
            return info;
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

        /// <summary>
        /// Saves the thumbnail image.
        /// </summary>
        /// <param name="img">The img.</param>
        /// <param name="dest">The dest.</param>
        /// <param name="src">The SRC.</param>
        /// <param name="filename">The filename.</param>
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
        /// Saves the flash file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="id">The id.</param>
        /// <param name="blogid">The blogid.</param>
        /// <returns></returns>
        public static string SaveFlashFile(System.Web.HttpPostedFile file, int id, int blogid)
        {
            Random random = new Random((int)DateTime.Now.Ticks);
            string filename = id.ToString() + "_" + blogid.ToString() + "_" + DateTime.Now.Ticks.ToString() + random.Next(100, 999).ToString() + ".swf";
            string fn = string.Empty;
            if (filename.IndexOf(Config.FLASH_DIR) < 0)
            {
                fn = System.Web.HttpContext.Current.Server.MapPath(Config.FLASH_DIR + filename);
            }
            file.SaveAs(fn);
            return filename;
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

        /// <summary>
        /// 返回图片的虚拟路径，返回值本身携带图片存储目录信息。
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static string GetPhotoVirtualPath(string filepath, string filename, int size)
        {
            if (filename.IndexOf(filepath) < 0)
            {
                filename = filepath + filename;
            }
            string fn = System.Web.HttpContext.Current.Server.MapPath(filename);
            if (!File.Exists(fn))
            {
                return null;
            }
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

        /// <summary>
        /// 返回图片的物理路径
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static string GetPhotoPhysicalPath(string filepath, string filename, int size)
        {
            if (filename.IndexOf(filepath) < 0)
            {
                filename = filepath + filename;
            }
            filename = System.Web.HttpContext.Current.Server.MapPath(filename);
            string fn = filename.ToLower().Replace(".jpg", "") + "_" + size + ".jpg";
            if (File.Exists(fn))
            {
                return fn;
            }
            else if (File.Exists(filename))
            {
                return filename;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the icon virtual path.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <param name="size">The size.</param>
        /// <returns></returns>
        public static string GetIconVirtualPath(string filename, int size)
        {
            if (filename == null || filename.Trim() == "")
            {
                return "/images/user.jpg";
            }
            if (filename.IndexOf(Config.USER_ICON_DIR) < 0)
            {
                filename = Config.USER_ICON_DIR + filename;
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

        /// <summary>
        /// Gets the photo_120.
        /// </summary>
        /// <param name="filepath">The filepath.</param>
        /// <param name="filename">The filename.</param>
        /// <returns></returns>
        public static string GetPhoto_120(string filepath, string filename)
        {
            return GetPhotoVirtualPath(filepath, filename, 120);
        }

        /// <summary>
        /// Gets the photo_640.
        /// </summary>
        /// <param name="filepath">The filepath.</param>
        /// <param name="filename">The filename.</param>
        /// <returns></returns>
        public static string GetPhoto_640(string filepath, string filename)
        {
            return GetPhotoVirtualPath(filepath, filename, 640);
        }

        /// <summary>
        /// Deletes the photo.
        /// </summary>
        /// <param name="filepath">The filepath.</param>
        /// <param name="filename">The filename.</param>
        public static void DeletePhoto(string filepath, string filename)
        {
            if (filename.IndexOf(filepath) < 0)
            {
                filename = filepath + filename;
            }
            string fn = GetPhotoPhysicalPath(filepath, filename, 120);
            if (!string.IsNullOrEmpty(fn))
            {
                File.Delete(fn);
            }
            fn = GetPhotoPhysicalPath(filepath, filename, 640);
            if (!string.IsNullOrEmpty(fn))
            {
                File.Delete(fn);
            }
        }

        /// <summary>
        /// Deletes the icon.
        /// </summary>
        /// <param name="oldfn">The oldfn.</param>
        public static void DeleteIcon(string oldfn)
        {
            if (oldfn.IndexOf(Config.USER_ICON_DIR) < 0)
            {
                oldfn += Config.USER_ICON_DIR;
            }
            string s = System.Web.HttpContext.Current.Server.MapPath(oldfn.Replace(".jpg", Config.IconSize.SMALL + ".jpg"));
            if (File.Exists(s))
            {
                File.Delete(s);
            }
            s = System.Web.HttpContext.Current.Server.MapPath(oldfn.Replace(".jpg", Config.IconSize.NORMAL + ".jpg"));
            if (File.Exists(s))
            {
                File.Delete(s);
            }
            s = System.Web.HttpContext.Current.Server.MapPath(oldfn.Replace(".jpg", Config.IconSize.LARGE + ".jpg"));
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
    }
}
