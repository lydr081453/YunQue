using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Purchase.DataAccess;
using ESP.Purchase.Entity;

namespace ESP.Purchase.BusinessLogic
{
    public static class RiskConsultationManager
    {
        private static RiskConsultationDataProvider dal = new RiskConsultationDataProvider();

        public static int Add(RiskConsultationInfo model)
        {
            return dal.Add(model);
        }

        public static IList<RiskConsultationInfo> GetList(int prid)
        {
            return dal.GetList(prid);
        }
    }
}
