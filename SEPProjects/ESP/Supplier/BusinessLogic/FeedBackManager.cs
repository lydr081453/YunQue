using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Supplier.Entity;
using ESP.Supplier.DataAccess;
using System.Data;
using System.Data.SqlClient;

namespace ESP.Supplier.BusinessLogic
{
    public class FeedBackManager
    {
        private static readonly FeedBackDataProvider dal = new FeedBackDataProvider();

        public static void Delete(int Id)
        {
            dal.Delete(Id);
        }

        public static FeedBackInfo GetModel(int Id)
        {
            return dal.GetModel(Id);
        }

        public static void Update(FeedBackInfo model)
        {
            dal.Update(model);
        }

        public static List<FeedBackInfo> GetListBySupplierId(int SupplierId)
        {
            return dal.GetListBySupplierId(SupplierId);
        }

        public static List<FeedBackInfo> GetList(string condition, string orderby)
        {
            return dal.GetList(condition,orderby);
        }
    }
}
