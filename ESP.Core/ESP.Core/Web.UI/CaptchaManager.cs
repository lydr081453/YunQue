using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Web.Caching;
using System.Web;
using System.Globalization;
using ESP.Logging;

namespace ESP.Web
{
    /// <summary>
    /// 验证码管理类。
    /// </summary>
    public static class CaptchaManager
    {
        private static int _expiredTime = 60;
        private const string CAPTCHA_COOKIE_NAME = "CaptchaSessionID";

        private static string GetCacheKey(string session)
        {
            return "CAPTCHA_" + session;
        }

        /// <summary>
        /// 生成验证码图片。
        /// </summary>
        /// <returns>验证码图片。</returns>
        public static Bitmap Generate()
        {
            char[] text;
            Bitmap bmp = new CaptchaImage().RenderImage(out text);
            string session = Guid.NewGuid().ToString("N");

            DateTime expire = DateTime.Now.AddSeconds(_expiredTime);

            HttpRuntime.Cache.Insert(GetCacheKey(session), new KeyValuePair<char[], DateTime>(text, expire), null, expire, Cache.NoSlidingExpiration);

            HttpCookie cookie = new HttpCookie(CAPTCHA_COOKIE_NAME, session);
            //cookie.HttpOnly = true;
            //cookie.Expires = DateTime.Now.AddSeconds(_expiredTime);
            HttpContext.Current.Response.Cookies.Remove(CAPTCHA_COOKIE_NAME);
            HttpContext.Current.Response.Cookies.Add(cookie);

            return bmp;
        }

        /// <summary>
        /// 重新生成验证码图片。
        /// </summary>
        /// <returns>验证码图片。</returns>
        public static Bitmap Regenerate()
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[CAPTCHA_COOKIE_NAME];
            if (cookie != null && cookie.Value != null)
            {
                string osession = cookie.Value;
                HttpRuntime.Cache.Remove(GetCacheKey(osession));
            }
            return Generate();
        }

        /// <summary>
        /// 检查验证是否正确。
        /// </summary>
        /// <param name="code">要检查的验证码。</param>
        /// <returns>验证码是否正确。</returns>
        public static bool Check(string code)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[CAPTCHA_COOKIE_NAME];

            if (cookie != null && cookie.Value != null)
            {
                HttpContext current = HttpContext.Current;
                string value = current.Request.Browser["supportsEmptyStringInCookieValue"] == "false" ? "NoCookie" : string.Empty;
                HttpCookie emptyCookie = new HttpCookie(CAPTCHA_COOKIE_NAME, value);
                cookie.Expires = new DateTime(1999, 10, 12);
                current.Response.Cookies.Remove(CAPTCHA_COOKIE_NAME);
                current.Response.Cookies.Add(cookie);

                string osession = cookie.Value;
                object obj   =  HttpRuntime.Cache.Get(GetCacheKey(osession));
                if(obj == null)
                    return false;

                KeyValuePair<char[], DateTime> cacheItem = (KeyValuePair<char[], DateTime>)obj;

                char[] text = cacheItem.Key;
                DateTime expire = cacheItem.Value;

                HttpRuntime.Cache.Remove(GetCacheKey(osession));

                if (string.Compare(code, new string(text), CultureInfo.InvariantCulture, CompareOptions.OrdinalIgnoreCase) == 0)
                    return true;
            }
            return false;
        }

    }
}
