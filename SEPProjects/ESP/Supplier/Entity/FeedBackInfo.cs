using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ESP.Supplier.Entity
{
    public class FeedBackInfo
    {
        private int id;
        private int supplierid;
        private string suppliername;
        private string feedback;
        private int creator;
        private string creatorname;
        private DateTime createtime;
        private string createip;
        private int status;

        private string pricescore;
        private string servicescore;
        private string qualityscore;
        private string timelinessscore;
        private string score;
        private string managerfeedback;
        private DateTime modifieddate;
        private int modifiedmanagerid;

        public int Id { get { return id; } set { id = value; } }
        public int SupplierId { get { return supplierid; } set { supplierid = value; } }
        public string SupplierName { get { return suppliername; } set { suppliername = value; } }
        public string FeedBack { get { return feedback; } set { feedback = value; } }
        public int Creator { get { return creator; } set { creator = value; } }
        public string CreatorName { get { return creatorname; } set { creatorname = value; } }
        public DateTime CreateTime { get { return createtime; } set { createtime = value; } }
        public string CreateIp { get { return createip; } set { createip = value; } }
        public int Status { get { return status; } set { status = value; } }

        public string PriceScore { get { return pricescore; } set { pricescore = value; } }
        public string ServiceScore { get { return servicescore; } set { servicescore = value; } }
        public string QualityScore { get { return qualityscore; } set { qualityscore = value; } }
        public string TimelinessScore { get { return timelinessscore; } set { timelinessscore = value; } }
        public string Score { get { return score; } set { score = value; } }

        public string ManagerFeedBack { get { return managerfeedback; } set { managerfeedback = value; } }
        public DateTime ModifiedDate { get { return modifieddate; } set { modifieddate = value; } }
        public int ModifiedManagerId { get { return modifiedmanagerid; } set { modifiedmanagerid = value; } }


        public void PopupData(IDataReader r)
        {
            if (null != r["id"] && r["id"].ToString() != "")
            {
                Id = int.Parse(r["id"].ToString());
            }
            if (null != r["supplierId"] && r["supplierId"].ToString() != "")
            {
                SupplierId = int.Parse(r["supplierId"].ToString());
            }

            SupplierName = r["supplierName"].ToString();
            FeedBack = r["feedBack"].ToString();
            CreatorName = r["creatorName"].ToString();
            PriceScore = r["PriceScore"].ToString();
            ServiceScore = r["ServiceScore"].ToString();
            QualityScore = r["QualityScore"].ToString();
            TimelinessScore = r["TimelinessScore"].ToString();
            Score = r["Score"].ToString();
            ManagerFeedBack = r["ManagerFeedBack"].ToString();

            if (null != r["createTime"] && r["createTime"].ToString() != "")
            {
                CreateTime = Convert.ToDateTime(r["createTime"]);
            }
            if (null != r["creator"] && r["creator"].ToString() != "")
            {
                Creator = int.Parse(r["creator"].ToString());
            }
            if (null != r["status"] && r["status"].ToString() != "")
            {
                Status = int.Parse(r["status"].ToString());
            }

            if (null != r["ModifiedDate"] && r["ModifiedDate"].ToString() != "")
            {
                ModifiedDate = DateTime.Parse(r["ModifiedDate"].ToString());
            }
            if (null != r["ModifiedManagerId"] && r["ModifiedManagerId"].ToString() != "")
            {
                ModifiedManagerId = int.Parse(r["ModifiedManagerId"].ToString());
            }
        }
    }
}
