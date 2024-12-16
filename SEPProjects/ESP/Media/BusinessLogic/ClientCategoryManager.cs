using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ESP.Media.Access;
using ESP.Media.Entity;
using ESP.Media.DataAccess;
using ESP.Media.Access.Utilities;
namespace ESP.Media.BusinessLogic
{
    public class ClientCategoryManager
    {
       public static ClientCategoryInfo GetModel(int categoryid)
       {
           if (categoryid <= 0)
           {
               ClientCategoryInfo category = new ClientCategoryInfo();
               category.CategoryName = string.Empty;
               category.SortID = 0;
               category.CategoryID = 0;

               return category;
           }
           return ClientCategoryDataProvider.Load(categoryid);
       }

       public static DataTable getAll()
       {
           DataTable dt = ClientCategoryDataProvider.QueryInfo("", null);
           return dt;
       }

       [AjaxPro.AjaxMethod]
       public static List<List<string>> getAllList()
       {
           List<List<string>> list = new List<List<string>>();

           List<string> c = new List<string>();
           c.Add("0");
           c.Add("请选择");
           list.Add(c);

           DataTable dt = ClientCategoryDataProvider.QueryInfo("", null);

           if (dt != null && dt.Rows.Count > 0)
           {
               for (int i = 0; i < dt.Rows.Count; i++)
               {
                   List<string> s = new List<string>();
                   s.Add(dt.Rows[i]["categoryid"].ToString());
                   s.Add(dt.Rows[i]["categoryname"].ToString());
                   list.Add(s);
               }
           }
           return list;
       }

    }
}
