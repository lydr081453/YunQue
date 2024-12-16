using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Media.Common
{
    public class Config
    {
        public static readonly long PHOTO_CONTENT_LENGTH = 1024 * 1024;
        public static readonly long USER_ICON_CONTENT_LENGTH = 1024 * 1024;
        public static readonly int USER_ICON_SIZE_SMALL = 48;
        public static readonly int USER_ICON_SIZE = 64;
        public static readonly int USER_ICON_SIZE_LARGE = 200;
        // public static readonly string USER_ICON_DIR
        public static readonly long THUMBNAIL_CONTENT_LENGTH_LIMIT = 0;
        public static readonly string[] PHOTO_CONTENT_TYPES = { "image/bmp", "image/jpeg", "image/pjpeg", "image/gif", "image/x-png", "application/octet-stream", "application/x-shockwave-flash" };
        public static readonly string[] PHOTO_EXTENSIONS = { ".bmp", ".jpg", ".jpg", ".gif", ".png" };
        public static readonly string PHOTO_DIR = ESP.Configuration.ConfigurationManager.SafeAppSettings["UpFile"];
        public static readonly string USER_ICON_DIR = ESP.Configuration.ConfigurationManager.SafeAppSettings["UserIcon"];
        public static readonly string FLASH_DIR = ESP.Configuration.ConfigurationManager.SafeAppSettings["FlashDir"];
        private static string _wf;

        //过滤敏感词
        public static string WORD_FILTER
        {
            get
            {
                if (_wf == null || _wf.Trim() == "")
                {
                    TextReader tr = System.IO.File.OpenText(ESP.Configuration.ConfigurationManager.SafeAppSettings["WordFilter"]);
                    string str = tr.ReadToEnd();
                    tr.Close();
                    _wf = str.Replace("\r\n", "|");
                }
                return _wf;
            }
            set { _wf = value; }
        }

        //用户头像尺寸
        public struct IconSize
        {
            public const int VERYSMALL = 24;
            public const int SMALL = 48;
            public const int NORMAL = 64;
            public const int LARGE = 200;
        }

        public struct PhotoSizeSettings
        {
            public const int WITHOUTLIMIT = 2048;
            public const int MAXSIZE = 1600;
            public const int LARGESIZE = 1280;
            public const int NORMALSIZE = 1024;
            public const int SMALLSIZE = 800;
            public const int MINSIZE = 640;
        }
    }
}
