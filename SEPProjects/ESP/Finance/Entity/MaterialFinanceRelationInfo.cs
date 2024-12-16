using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.Entity
{
    [Serializable]
    public partial class MaterialFinanceRelationInfo
    {
        public int Id{get;set;}
        public int MaterialId{get;set;}
        public string FinanceObjectName{get;set;}
        public string MaterialType { get; set; }
    }
}
