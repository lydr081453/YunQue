using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.Entity
{
    [Serializable]
    public class AssetInfo
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserCode { get; set; }
        public string UserName { get; set; }
        public string AssetCode { get; set; }
        public string Asset { get; set; }
        public string Model { get; set; }
        public string Amount { get; set; }
        public string BranchCode { get; set; }
        public string appDate { get; set; }
        public string StopAllowanceDate { get; set; }

    }
}
