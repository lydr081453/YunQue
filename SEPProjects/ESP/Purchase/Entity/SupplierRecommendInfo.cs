using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Purchase.Entity
{
    public class SupplierRecommendInfo
    {
        public SupplierRecommendInfo() { }

        public int Id { get; set; }
        public int RecommendId { get; set; }
        public int SupplierId { get; set; }
        public int SupplyId { get; set; }
        public string SupplierName { get; set; }
        public string Contacter { get; set; }
        public string Tel { get; set; }
        public string Mobile { get; set; }
        public string Position { get; set; }
        public string EMail { get; set; }
        public string DeptName { get; set; }
        public string Remark { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }
        public int Status { get; set; }
        public DateTime CreateDate { get; set; }
        public int UserId { get; set; }

        public int Orders { get; set; }
        public decimal TotalPrice { get; set; }

        public string BuildTime { get; set; }
        public string CompanyType { get; set; }
        public string RegCapital { get; set; }
        public string CompanyScale { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string BizModel { get; set; }
        public string MainProduct { get; set; }

        public int Sort { get; set; }

    }
}
