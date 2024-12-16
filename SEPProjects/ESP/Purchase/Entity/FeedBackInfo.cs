using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Purchase.Entity
{
    [Serializable]
    public class FeedBackInfo
    {
        public int id { get; set; }
        public int supplierId { get; set; }
        public string supplierName { get; set; }
        public string feedback { get; set; }
        public string creator { get; set; }
        public string creatorName { get; set; }
        public DateTime createTime { get; set; }
        public string createIp { get; set; }
        public int status { get; set; }
        public string PriceScore { get; set; }
        public string ServiceScore { get; set; }
        public string QualityScore { get; set; }
        public string TimelinessScore { get; set; }
        public string Score { get; set; }
        public string ManagerFeedBack { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int ModifiedManagerId { get; set; }

    }
}
