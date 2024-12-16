using System.Text;

namespace ESP.Utilities
{
    /// <summary>
    /// Javascript辅助类
    /// </summary>
    public static class JavascriptUtility
    {
        /// <summary>
        /// 将字符串的内容置于引号中以便Javascript使用
        /// </summary>
        /// <param name="value">字符串值</param>
        /// <param name="forUrl">是否用于Url中</param>
        /// <returns>可以在Javascript中使用的字段串</returns>
        public static string QuoteJScriptString(string value, bool forUrl)
        {
            return QuoteJScriptString(value, forUrl, false);
        }

        /// <summary>
        /// 将字符串的内容置于引号中以便Javascript使用
        /// </summary>
        /// <param name="value">字符串值</param>
        /// <param name="forUrl">是否用于Url中</param>
        /// <param name="addQuotes">是否在字符串的两端添加双引号</param>
        /// <returns>可以在Javascript中使用的字段串</returns>
        /// <example>
        /// string s = "a string \" - ";
        /// Response.Write("var s = " + JavascriptHelper.QuoteJScriptString(s, false, true));
        /// 
        /// 输出结果： var s = "a string \" - "
        /// </example>
        public static string QuoteJScriptString(string value, bool forUrl, bool addQuotes)
        {
            StringBuilder builder = null;
            if (string.IsNullOrEmpty(value))
            {
                return addQuotes ? "\"\"" : string.Empty;
            }

            if (addQuotes)
            {
                builder = new StringBuilder(value.Length + 2);
                builder.Append('"');
            }

            int startIndex = 0;
            int count = 0;
            for (int i = 0; i < value.Length; i++)
            {
                switch (value[i])
                {
                    case '%':
                        {
                            if (!forUrl)
                            {
                                break;
                            }
                            if (builder == null)
                            {
                                builder = new StringBuilder(value.Length + 6);
                            }
                            if (count > 0)
                            {
                                builder.Append(value, startIndex, count);
                            }
                            builder.Append("%25");
                            startIndex = i + 1;
                            count = 0;
                            continue;
                        }
                    case '\'':
                        {
                            if (builder == null)
                            {
                                builder = new StringBuilder(value.Length + 5);
                            }
                            if (count > 0)
                            {
                                builder.Append(value, startIndex, count);
                            }
                            builder.Append(@"\'");
                            startIndex = i + 1;
                            count = 0;
                            continue;
                        }
                    case '\\':
                        {
                            if (builder == null)
                            {
                                builder = new StringBuilder(value.Length + 5);
                            }
                            if (count > 0)
                            {
                                builder.Append(value, startIndex, count);
                            }
                            builder.Append(@"\\");
                            startIndex = i + 1;
                            count = 0;
                            continue;
                        }
                    case '\t':
                        {
                            if (builder == null)
                            {
                                builder = new StringBuilder(value.Length + 5);
                            }
                            if (count > 0)
                            {
                                builder.Append(value, startIndex, count);
                            }
                            builder.Append(@"\t");
                            startIndex = i + 1;
                            count = 0;
                            continue;
                        }
                    case '\n':
                        {
                            if (builder == null)
                            {
                                builder = new StringBuilder(value.Length + 5);
                            }
                            if (count > 0)
                            {
                                builder.Append(value, startIndex, count);
                            }
                            builder.Append(@"\n");
                            startIndex = i + 1;
                            count = 0;
                            continue;
                        }
                    case '\r':
                        {
                            if (builder == null)
                            {
                                builder = new StringBuilder(value.Length + 5);
                            }
                            if (count > 0)
                            {
                                builder.Append(value, startIndex, count);
                            }
                            builder.Append(@"\r");
                            startIndex = i + 1;
                            count = 0;
                            continue;
                        }
                    case '"':
                        {
                            if (builder == null)
                            {
                                builder = new StringBuilder(value.Length + 5);
                            }
                            if (count > 0)
                            {
                                builder.Append(value, startIndex, count);
                            }
                            builder.Append("\\\"");
                            startIndex = i + 1;
                            count = 0;
                            continue;
                        }
                }
                count++;
            }
            if (builder == null)
            {
                return value;
            }
            if (count > 0)
            {
                builder.Append(value, startIndex, count);
            }

            if (addQuotes)
                builder.Append('"');
            return builder.ToString();
        }
    }
}
