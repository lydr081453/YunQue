using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Purchase.Entity
{
    public class TypeRecommendInfo
    {
        public TypeRecommendInfo()
        { }

        public int Id { get; set; }
        public int Level1Id { get; set; }
        public int Level2Id { get; set; }
        public int Level3Id { get; set; }
        public string Level1 { get; set; }
        public string Level2 { get; set; }
        public string Level3 { get; set; }
        public int Orders { get; set; }
        public decimal TotalPrice { get; set; }
        public int RecYear { get; set; }
        public int RecMonth { get; set; }
        public DateTime CreateDate { get; set; }
        public int UserId { get; set; }
        public int Status { get; set; }

    }
}
