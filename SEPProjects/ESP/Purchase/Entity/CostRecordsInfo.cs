using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Purchase.Entity
{
    [Serializable]
    public class CostRecordsInfo
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string ProjectCode { get; set; }
        public int SupporterId { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int MaterialId { get; set; }
        public string MaterialName { get; set; }
        public decimal PreAmount { get; set; }
        public decimal FactAmount { get; set; }
        public int FormId { get; set; }
        public int RefFormId { get; set; }
        public int ParentId { get; set; }
        public int FormType { get; set; }
        public int Status { get; set; }
        public string Remark { get; set; }
        public DateTime CreateDate { get; set; }

        public CostRecord_Remark RemarkModel { get; set; }

    }

    public class CostRecord_Remark
    {
        public string Role { get; set; }
        public string FormCode { get; set; }
        public string Operation { get; set; }
        public string Log { get; set; }
    }
}
