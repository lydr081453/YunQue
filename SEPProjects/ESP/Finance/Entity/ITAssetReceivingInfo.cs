using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.Entity
{
    [Serializable]
    public class ITAssetReceivingInfo
    {
        public int Id { get; set; }
        public int AssetId { get; set; }
        public int UserId { get; set; }
        public DateTime ReceiveDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public string ReceiveDesc { get; set; }
        public int OperatorId { get; set; }
        public int Status { get; set; }

        public string UserCode{ get; set; }
        public string UserName	{ get; set; }
        public string Email{ get; set; }
        public string Mobile { get; set; }
        public string DataServer { get; set; }

        public string AssetName { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string SerialCode { get; set; }
        public decimal Price { get; set; }

    }
}
