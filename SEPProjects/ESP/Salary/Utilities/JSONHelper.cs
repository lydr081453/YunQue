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

namespace ESP.Salary.Utility
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
}
