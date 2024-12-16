using System.Data;
using System.Collections.Generic;
using ESP.Purchase.DataAccess;
using ESP.Purchase.Entity;
using System.Text;

namespace ESP.Purchase.BusinessLogic
{
   public static  class SupplierRecommendManager
    {
       private static SupplierRecommendProvider dal = new SupplierRecommendProvider();

        public static int Add(SupplierRecommendInfo model)
        {
            return dal.Add(model);
        }

        public static int Update(SupplierRecommendInfo model)
        {
            return dal.Update(model);
        }

        public static SupplierRecommendInfo GetModel(int id)
        {
            return dal.GetModel(id);
        }

        public static List<SupplierRecommendInfo> GetModelList(string strWhere)
        {
            return dal.GetModelList(strWhere);
        }

        public static List<SupplierRecommendInfo> SearchData(int typeId,string dtBegin, string dtEnd)
        {
            return dal.SearchData(typeId,dtBegin, dtEnd);
        }

    }
}
