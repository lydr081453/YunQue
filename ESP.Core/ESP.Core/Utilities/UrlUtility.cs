
namespace ESP.Utilities
{
    /// <summary>
    /// URL操作辅助类
    /// </summary>
    public static class UrlUtility
    {
        /// <summary>
        /// 从Url字符串中移除指定的查询参数
        /// </summary>
        /// <param name="url">Url字符串</param>
        /// <param name="queryName">要移除的参数的名字</param>
        /// <returns>移除的指定的参数后的Url</returns>
        public static string RemoveQuery(string url, string queryName)
        {
            if (url == null || queryName == null)
                return url;

            int index1 = url.IndexOf("?" + queryName + "=");
            if (index1 < 0)
                index1 = url.IndexOf("&" + queryName + "=");

            if (index1 < 0)
                return url;

            int index2 = url.IndexOf("&", index1 + queryName.Length);
            if (index2 < 0)
                return url.Substring(0, index1);

            return url.Substring(0, index1 + 1) + url.Substring(index2 + 1);
        }

        /// <summary>
        /// 将Url从 http(s)://domain/path_and_query 转为 /path_and_query 形式
        /// </summary>
        /// <param name="url">原始Url</param>
        /// <returns>Path and Query</returns>
        public static string GetPathAndQuery(string url)
        {
            if (string.IsNullOrEmpty(url))
                return null;

            int index = url.IndexOf(':');
            if (index < 0)
                return null;

            index = url.IndexOf('/', index + 3);
            if (index < 0)
                return "/";

            return url.Substring(index);
        }

        /// <summary>
        /// 获取 path 相对于 basePath 的相对路径
        /// </summary>
        /// <param name="path">原始路径</param>
        /// <param name="basePath">基本路径</param>
        /// <param name="startWithSlash">结果路径是否以'/'开头</param>
        /// <returns>相对路径</returns>
        public static string GetRelativePath(string path, string basePath, bool startWithSlash)
        {
            if (path == null || path.Length == 0)
                return null;

            if (basePath == null || basePath.Length == 0)
                return path;

            int len = basePath.Length;
            char last = basePath[len - 1];
            if (last == '/')
                len--;

            if (path.Length < len)
                return null;

            if (path.Length == len)
            {
                return startWithSlash ? "/" : "";
            }

            if (!startWithSlash)
                len++;

            return path.Substring(len);
        }

        /// <summary>
        /// 将指定的查询参数添加到Url字符串中
        /// </summary>
        /// <param name="url">Url字符串</param>
        /// <param name="queryName">查询参数的名字</param>
        /// <param name="queryValue">查询参数的值（已经进行了Url编码）</param>
        /// <returns>添加了指定参数后的Url</returns>
        public static string AddQuery(string url, string queryName, string queryValue)
        {
            if (url == null || queryName == null || queryName.Length == 0 || queryValue == null)
                return url;

            string ESP = url.IndexOf("?") < 0 ? "?" : "&";
            return url = url + ESP + queryName + "=" + queryValue;
        }


        /// <summary>
        /// 将两个Url片段连接成一个Url，如果Left后边和right前边都有“/”，则删除其中一个，如果都没有，则添加一个。
        /// </summary>
        /// <param name="left">Url的左半部分</param>
        /// <param name="right">Url的右半部分</param>
        /// <returns>拼接后的Url</returns>
        public static string ConcatUrl(string left, string right)
        {
            if (left == null || left.Length == 0)
                return right;
            if (right == null || right.Length == 0)
                return left;

            bool f1 = (left[left.Length - 1] == '/');
            bool f2 = (right[0] == '/');

            if (f1 && f2)
                return left + right.Substring(1);

            if (f1 || f2)
                return left + right;

            return left + "/" + right;
        }
    }
}
