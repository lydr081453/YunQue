using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Portal.Common
{
    public class Util
    {
        public static string DateTime2String(DateTime d)
        {
            return d.ToShortDateString() + " " + d.ToShortTimeString();
        }

        /// <summary>
        /// 去除字符串中包含的HTML标签
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RemoveHTMLCode(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }

            Match m = Regex.Match(str, "</(.*)>");
            if (m.Success)
            {
                str = str.Replace(m.ToString(), "");
                m = Regex.Match(str, "<(.*)>");
                if (m.Success)
                {
                    str = str.Replace(m.ToString(), "");
                }
                return str;
            }
            else
            {
                m = Regex.Match(str, "<(.*)>");
                if (m.Success)
                {
                    str = str.Replace(m.ToString(), "");
                }
                return str;
            }
        }

        /// <summary>
        /// 从字符串中获取URL信息
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetURL(string str)
        {
            string pattern = @"http(s)?://([\w-]+.)+[\w-]+(/[\w-./?%&=]*)?";　//匹配URL的模式
            Match mc = Regex.Match(str, pattern);                             //满足pattern的匹配结果
            if (mc.Success)
            {
                str = mc.ToString();
            }
            return str;
        }

        /// <summary>
        /// 获得某个时间相对于现在过去了多久的中文描述
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string GetPassTimeString(DateTime time)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            TimeSpan t = new TimeSpan(DateTime.Now.Ticks - time.Ticks);
            int h = 0, m = 0, s = 0;
            if (t.TotalDays >= 1)
            {
                sb.Append(time.ToShortDateString());
                sb.Append(" ");
                sb.Append(time.ToShortTimeString());
                sb.Append(GetCNWeekDay(time.DayOfWeek));
            }
            else if ((int)t.TotalHours > 0)
            {
                h = (int)t.TotalHours;
                sb.Append(h);
                sb.Append("小时前");
            }
            else if ((int)t.TotalMinutes > 0)
            {
                m = (int)t.TotalMinutes;
                sb.Append(m);
                sb.Append("分钟前");
            }
            else if ((int)t.TotalSeconds > 0)
            {
                s = (int)t.TotalSeconds;
                sb.Append(s);
                sb.Append("秒前");
            }
            else
            {
                return "[刚刚]";
            }
            sb.Append("]");
            return sb.ToString();
        }

        public static string GetCNWeekDay(DayOfWeek d)
        {
            switch (d)
            {
                case DayOfWeek.Friday:
                    return "周五";
                case DayOfWeek.Monday:
                    return "周一";
                case DayOfWeek.Saturday:
                    return "周六";
                case DayOfWeek.Sunday:
                    return "周日";
                case DayOfWeek.Thursday:
                    return "周四";
                case DayOfWeek.Tuesday:
                    return "周二";
                case DayOfWeek.Wednesday:
                    return "周三";
                default:
                    return "";
            }
        }

        public static void SwapNumberToASC(ref int a, ref int b)
        {
            if (a <= b)
            {
                return;
            }
            else
            {
                int c = a;
                a = b;
                b = c;
            }
        }

        /// <summary>
        /// 获取论坛帖子的URL
        /// </summary>
        /// <param name="topicid">帖子ID</param>
        /// <param name="pageIndex">要显示第几页</param>
        /// <returns>帖子的URL</returns>
        /// <remarks>
        /// 如果在Web.config文件中的 sep 节配置了 forumWebSiteID，则返回
        /// 完整的 url，否则返回相对 url。
        /// </remarks>
        public static string ShowTopicAspxRewrite(int topicid, int pageIndex)
        {
            bool isAspxrewrite = true;
            string extname = ".aspx";
            //GeneralConfigInfo config = GeneralConfigs.GetConfig();

            string url;

            if (isAspxrewrite)
            {
                if (pageIndex > 0)
                {
                    url = "showtopic-" + topicid + "-" + pageIndex + extname;
                }
                else
                {
                    url = "showtopic-" + topicid + extname;
                }
            }
            else
            {
                if (pageIndex > 0)
                {
                    url = "showtopic.aspx?topicid=" + topicid + "&page=" + pageIndex;
                }
                else
                {
                    url = "showtopic.aspx?topicid=" + topicid;
                }
            }

            int forumWebSiteID;
            string forumWebSiteIDString = ESP.Configuration.ConfigurationManager.Items["forumWebSiteID"];
            if (int.TryParse(forumWebSiteIDString, out forumWebSiteID))
            {
                ESP.Framework.Entity.WebSiteInfo webSite = ESP.Framework.BusinessLogic.WebSiteManager.Get(forumWebSiteID);
                if(webSite != null)
                    return ESP.Utilities.UrlUtility.ConcatUrl("http://" + webSite.UrlPrefix, url);
            }

            return url;
        }

        /// <summary>
        /// 获得一个用户信息的表格
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string CreateUserTable(List<string[]> list)
        {
            System.Text.StringBuilder strbulider = new System.Text.StringBuilder();
            strbulider.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" class=\"com\">");
            if (list != null && list.Count > 0)
            {
                int k = 0;
                if (list.Count % 4 == 0)
                {
                    k = list.Count / 4;
                }
                else
                {
                    k = (list.Count / 4) + 1;
                }
                for (int j = 0; j < k; j++)
                {
                    strbulider.Append("<tr>");
                    for (int i = 0; i < 4; i++)
                    {
                        // 已经到了最后一条数据的时候跳出当前循环
                        if ((j * 4) + i + 1 > list.Count)
                        {
                            break;
                        }
                        string[] userinfo = list[(j * 4) + i];
                        strbulider.Append("<td><div><a href=\"/User.aspx?userid=" + userinfo[0] + "\" title=\"" + userinfo[1] + "\" rel=\"contact\"><img icon=\"34994\" class=\"buddy_icon\" src=\"" + userinfo[2] + "\" title=\"" + userinfo[1] + "\" border=\"0\" />" + userinfo[1] + "</a></div></td>");
                    }
                    strbulider.Append("</tr>");
                }
            }
            strbulider.Append("</table>");

            return strbulider.ToString();
        }
        
    }
}
