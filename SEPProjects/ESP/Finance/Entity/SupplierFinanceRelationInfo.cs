using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.Entity
{
    [Serializable]
   public  class SupplierFinanceRelationInfo
    {
        public int Id{get;set;}
        public int SupplierId{get;set;}
        public string ShortName{get;set;}
        public string FianceObjectName {get;set;}
        public string Remark { get; set; }

    }
}
