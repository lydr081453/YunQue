using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Goheer.EXIF;

namespace ESP.Supplier.Common
{
    public class ImageHelper
    {
        public const string WATERMARK = "LogLife.CN 版权所有";
        public static void DrawWatermark(Stream input)
        {
            Bitmap image = new Bitmap(input);
            Graphics g = Graphics.FromImage(image);
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            g.DrawString(WATERMARK, new Font(new FontFamily("宋体"), 12f, FontStyle.Bold), new SolidBrush(Color.WhiteSmoke), new PointF(image.Width - 100, image.Height - 100));
            image.Save("C:/test.jpg");
        }

        /*        public static PhotoInfo SaveIcon(Stream input, int size, int length, int userid, string oldfn)
                {
                    Bitmap image = new Bitmap(input);

                    //计算不重复的文件名，此文件名包含用户编号，乐活志编号。

                    PhotoInfo info = new PhotoInfo();
                    info.exif = new EXIFextractor(ref image, "");
                    int width = 0, height = 0;
                    if (image.Width <= size && image.Height <= size)
                    {
                        width = image.Width;
                        height = image.Height;

                    }
                    else
                    {
                        //如果图片超过最大限度，则保存为最大限度的图片
                        if (image.Width > image.Height)
                        {
                            width = size;
                            height = (int)((double)image.Height / (double)((double)image.Width / (double)size));
                        }
                        else if (image.Width < image.Height)
                        {
                            height = size;
                            width = (int)((double)image.Width / (double)((double)image.Height / (double)size));
                        }
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
                    Rectangle r = ZoomRectangle(width, height, image.Width, image.Height);
                    SaveThumbnailImage(image, r, Config.USER_ICON_DIR + fn);
                    info.filename = fn + ".jpg";
                    info.size.width = width;
                    info.size.height = height;
                    //保存缩略图，包括——Config.USER_ICON_SIZE_SMALL | Config.USER_ICON_SIZE | Config.USER_ICON_SIZE_LARGE。
                    if (width > Config.IconSize.SMALL || height > Config.IconSize.SMALL)
                    {
                        r = ZoomRectangle(Config.IconSize.SMALL, Config.IconSize.SMALL, image.Width, image.Height);
                        SaveThumbnailImage(image, r,  Config.USER_ICON_DIR + fn + "_" + Config.IconSize.SMALL);
                    }

                    if (width > Config.IconSize.NORMAL || height > Config.IconSize.NORMAL)
                    {
                        r = ZoomRectangle(Config.IconSize.NORMAL, Config.IconSize.NORMAL, image.Width, image.Height);
                        SaveThumbnailImage(image, r, Config.USER_ICON_DIR + fn + "_" + Config.IconSize.NORMAL);
                    }

                    if (width > Config.IconSize.LARGE || height > Config.IconSize.LARGE)
                    {
                        r = ZoomRectangle(Config.IconSize.LARGE, Config.IconSize.LARGE, image.Width, image.Height);
                        SaveThumbnailImage(image, r, Config.USER_ICON_DIR + fn + "_" + Config.IconSize.LARGE);
                    }
                    return info;
                }*/

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

            //计算不重复的文件名，此文件名包含用户编号，乐活志编号。

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

        public static PhotoInfo SavePhoto(Stream input, int size, long length, int userid, int blogid)
        {
            Bitmap image = new Bitmap(input);
            PhotoInfo info = new PhotoInfo();

            //计算不重复的文件名，此文件名包含用户编号，乐活志编号。
            Random random = new Random((int)DateTime.Now.Ticks);
            string filename = userid.ToString() + "_" + blogid.ToString() + "_" + DateTime.Now.Ticks.ToString() + random.Next(100, 999).ToString();

            info.exif = new EXIFextractor(ref image, "");
            int width = 0, height = 0;
            if (image.Width <= size && image.Height <= size)
            {
                width = image.Width;
                height = image.Height;

            }
            else
            {
                //如果图片超过最大限度，则保存为最大限度的图片
                if (image.Width > image.Height)
                {
                    width = size;
                    height = (int)((double)image.Height / (double)((double)image.Width / (double)size));
                }
                else if (image.Width < image.Height)
                {
                    height = size;
                    width = (int)((double)image.Width / (double)((double)image.Height / (double)size));
                }
            }
            //保存原图尺寸或最大尺寸图形，图片将被转换为JPEG格式
            Rectangle r = ZoomRectangle(width, height, image.Width, image.Height);
            SaveThumbnailImage(image, r, Config.PHOTO_DIR + filename);
            info.filename = filename + ".jpg";

            info.size.width = width;
            info.size.height = height;
            //保存缩略图，分别是120和640两种。
            if (width > 120 || height > 120 || length > Config.THUMBNAIL_CONTENT_LENGTH_LIMIT)
            {
                r = ZoomRectangle(120, 120, image.Width, image.Height);
                SaveThumbnailImage(image, r, Config.PHOTO_DIR + filename + "_120");
            }
            if (width > 640 || height > 640 || length > Config.THUMBNAIL_CONTENT_LENGTH_LIMIT)
            {
                r = ZoomRectangle(640, 640, image.Width, image.Height);
                SaveThumbnailImage(image, r, Config.PHOTO_DIR + filename + "_640");
            }
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

        public static string SaveFlashFile(System.Web.HttpPostedFile file, int userid, int blogid)
        {
            Random random = new Random((int)DateTime.Now.Ticks);
            string filename = userid.ToString() + "_" + blogid.ToString() + "_" + DateTime.Now.Ticks.ToString() + random.Next(100, 999).ToString() + ".swf";
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
        public static string GetPhotoVirtualPath(string filename, int size)
        {
            if (filename.IndexOf(Config.PHOTO_DIR) < 0)
            {
                filename = Config.PHOTO_DIR + filename;
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
        public static string GetPhotoPhysicalPath(string filename, int size)
        {
            if (filename.IndexOf(Config.PHOTO_DIR) < 0)
            {
                filename = Config.PHOTO_DIR + filename;
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

        public static string GetPhoto_120(string filename)
        {
            return GetPhotoVirtualPath(filename, 120);
        }

        public static string GetPhoto_640(string filename)
        {
            return GetPhotoVirtualPath(filename, 640);
        }

        public static void DeletePhoto(string filename)
        {
            if (filename.IndexOf(Config.PHOTO_DIR) < 0)
            {
                filename = Config.PHOTO_DIR + filename;
            }
            string fn = GetPhotoPhysicalPath(filename, 120);
            if (fn != null && fn != "")
            {
                File.Delete(fn);
            }
            fn = GetPhotoPhysicalPath(filename, 640);
            if (fn != null && fn != "")
            {
                File.Delete(fn);
            }
        }

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
