using System.Data;
using System.Collections.Generic;
using ESP.Purchase.DataAccess;
using ESP.Purchase.Entity;
using System.Text;

namespace ESP.Purchase.BusinessLogic
{
    public static class TypeRecommendManager
    {
        private static TypeRecommendProvider dal = new TypeRecommendProvider();

        public static int Add(TypeRecommendInfo model)
        {
            return dal.Add(model);
        }

        public static int Update(TypeRecommendInfo model)
        {
            return dal.Update(model);
        }

        public static TypeRecommendInfo GetModel(int id)
        {
            return dal.GetModel(id);
        }

        public static List<TypeRecommendInfo> GetModelList(string strWhere)
        {
            return dal.GetModelList(strWhere);
        }

        public static List<TypeRecommendInfo> SearchData(string dtBegin ,string dtEnd)
        {
            return dal.SearchData(dtBegin,dtEnd);
        }

        
    }
}
