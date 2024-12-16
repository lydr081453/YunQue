using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.Entity
{
    [Serializable]
    public class ITAssetsInfo
    {
        public int Id { get; set; }
        public string SerialCode { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string AssetName { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Configuration { get; set; }
        public decimal Price { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string AssetDesc { get; set; }
        public int Status { get; set; }
        public DateTime ScrapDate { get; set; }

        public int ScrapUserId { get; set; }
        public string ScrapUserName { get; set; }
        public string ScrapDesc { get; set; }
        public int ScrapAuditorId { get; set; }
        public string ScrapAuditor { get; set; }
        public DateTime ScrapAuditDate { get; set; }
        public string ScrapAuditDesc { get; set; }
        public string UpFile { get; set; }
        public string RelationPO { get; set; }

        public int ScrapLeaderId { get; set; }
        public string ScrapLeader { get; set; }
        public DateTime ScrapLeaderDate { get; set; }
        public string ScrapLeaderDesc { get; set; }
        public string Photo { get; set; }
        public DateTime EditDate { get; set; }
    }
}
