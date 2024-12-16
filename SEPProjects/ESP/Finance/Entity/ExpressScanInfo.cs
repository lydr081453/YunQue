using System;
namespace ESP.Finance.Entity
{
    [Serializable]
    public partial class ExpressScanInfo
    {
        public ExpressScanInfo()
        { }
        public int Id { get; set; }
        public string ExpressNo { get; set; }
        public int ExpYear { get; set; }
        public int ExpMonth { get; set; }
        public string Company { get; set; }
        public int Creater { get; set; }
        public DateTime CreateDate { get; set; }
        public int Status { get; set; }

    }
}
