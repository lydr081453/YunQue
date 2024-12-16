using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Script.Serialization;
using System.Data;
using System.Linq;
using System.Reflection;
using ESP.Compatible;
using System.IO;
using System.Globalization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
namespace ESP.Salary.Utility
{

    public static class PagingClass
    {

        public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> list, string sortExpression)
        {
            sortExpression += "";
            string[] parts = sortExpression.Split(' ');
            bool descending = false;
            string property = "";

            if (parts.Length > 0 && parts[0].Length != 0)
            {
                property = parts[0];

                if (parts.Length > 1)
                {
                    descending = parts[1].ToLower().Contains("esc");
                }

                PropertyInfo prop = typeof(T).GetProperty(property);

                if (prop == null)
                {
                    throw new Exception("No property '" + property + "' in + " + typeof(T).Name + "'");
                }

                if (descending)
                    return list.OrderByDescending(x => prop.GetValue(x, null));
                else
                    return list.OrderBy(x => prop.GetValue(x, null));
            }

            return list;
        }

        public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> list, string sortCol, string sortDir)
        {
            string sortExpression = sortCol + " " + sortDir;
            string[] parts = sortExpression.Split(' ');
            bool descending = false;
            string property = "";

            if (parts.Length > 0 && parts[0].Length != 0)
            {
                property = parts[0];

                if (parts.Length > 1)
                {
                    descending = parts[1].ToLower().Contains("desc");
                }

                PropertyInfo prop = typeof(T).GetProperty(property);

                if (prop == null)
                {
                    throw new Exception("No property '" + property + "' in + " + typeof(T).Name + "'");
                }

                if (descending)
                    return list.OrderByDescending(x => prop.GetValue(x, null));
                else
                    return list.OrderBy(x => prop.GetValue(x, null));
            }

            return list;
        }

        public static IList<T> GetPagingList<T>(this IList<T> source, int offset, int page_size, string sortCol, string sortDir)
        {
            IList<T> pagingList = new List<T>();

            if (source == null || source.Count == 0 || source.Count <= page_size)
                return source;



            var list = source.OrderBy(sortCol, sortDir).Skip(offset).Take(page_size);

            foreach (T t in list)
            {
                pagingList.Add(t);
            }
            return pagingList;
        }


    }

}