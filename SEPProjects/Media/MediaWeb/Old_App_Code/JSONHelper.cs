
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Script.Serialization;
using System.Data;
using System.Linq;
namespace Web.Components
{
    public static class JSONHelper
    {
        public static string ToJSON(this object obj)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(obj);
        }

        public static string ToJSON(this object obj, int recursionDepth)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.RecursionLimit = recursionDepth;
            return serializer.Serialize(obj);
        }
    }

    public static class GetDataSource
    {
        public static DataTable GetPagingSource(this DataTable obj, int start, int size, string sort, string dir)
        {
            var query = from row in obj.AsEnumerable() select row;
            int iCount = query.Count();   //所要记录数
            int PageNum = start / size;  //共有页数 
            int PageSize = size;
            System.Collections.Generic.IEnumerable<DataRow> newquery;
            newquery = query.Skip(PageSize * PageNum).Take(PageSize);
            DataTable dt = obj.Clone();
            foreach (DataRow row in newquery)
            {
                dt.ImportRow(row);
            }
            return dt;
        }
    }
}
