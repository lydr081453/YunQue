using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.Entity
{
    [Serializable]
    public partial class FinanceObjectInfo
    {
        public int Id { get; set; }
        public string ObjectType { get; set; }
        public string ObjectCode { get; set; }
        public string ObjectName { get; set; }
        public int ObjectId { get; set; }
        public string Code0 { get; set; }
        public string code1 { get; set; }
        public string code2 { get; set; }
        public string code3 { get; set; }
        public string code4 { get; set; }
        public string code5 { get; set; }
        public string code6 { get; set; }
        public string CredenceTypeCode { get; set; }
        public int RowLevel { get; set; }
        public string RowDesc { get; set; }

    }
}
