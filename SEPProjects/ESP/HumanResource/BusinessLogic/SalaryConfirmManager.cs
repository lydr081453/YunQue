using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.HumanResource.Entity;

namespace ESP.HumanResource.BusinessLogic
{
   public  class SalaryConfirmManager
    {
        private ESP.HumanResource.DataAccess.SalaryConfirmDataProvider dataProvider = new ESP.HumanResource.DataAccess.SalaryConfirmDataProvider();

        public int Add(SalaryConfirmInfo model)
        {
            return dataProvider.Add(model);
        }

        public bool Exists(int userid, int year,int month)
        {
            return dataProvider.Exists(userid, year,month);
        }

        public bool SalaryDataExists(int userid, int year,int month)
        {
            return dataProvider.SalaryDataExists(userid, year, month);
        }

        public bool PaymentReportExists(int userid, DateTime dt1, DateTime dt2)
        {
            return dataProvider.PaymentReportExists(userid, dt1, dt2);
        }
    }
}
