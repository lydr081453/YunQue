using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Purchase.Entity
{
    public class AdvertisementDetailsInfo
    {
        public AdvertisementDetailsInfo()
        { }

        private int id;
        private int advertisementID;
        private decimal discount;
        private string discountDescription;
        private decimal distributionMin;
        private decimal distributionMax;
        private decimal distributionPercent;
        private string distributionDescription;
        private decimal returnPoint;
        private string accountPeriod;
        private DateTime createdDate;
        private int createdUserID;
        private DateTime modifiedDate;
        private int modifiedUserID;

        public int ID
        {
            set { id = value; }
            get { return id; }
        }

        public int AdvertisementID
        {
            set { advertisementID = value; }
            get { return advertisementID; }
        }

        public decimal Discount
        {
            set { discount = value; }
            get { return discount; }
        }

        public string DiscountDescription
        {
            set { discountDescription = value; }
            get { return discountDescription; }
        }

        public decimal DistributionMin
        {
            set { distributionMin = value; }
            get { return distributionMin; }
        }

        public decimal DistributionMax
        {
            set { distributionMax = value; }
            get { return distributionMax; }
        }

        public decimal DistributionPercent
        {
            set { distributionPercent = value; }
            get { return distributionPercent; }
        }

        public string DistributionDescription
        {
            set { distributionDescription = value; }
            get { return distributionDescription; }
        }

        public decimal ReturnPoint
        {
            set { returnPoint = value; }
            get { return returnPoint; }
        }

        public string AccountPeriod
        {
            set { accountPeriod = value; }
            get { return accountPeriod; }
        }

        public DateTime CreatedDate
        {
            set { createdDate = value; }
            get { return createdDate; }
        }

        public int CreatedUserID
        {
            set { createdUserID = value; }
            get { return createdUserID; }
        }

        public DateTime ModifiedDate
        {
            set { modifiedDate = value; }
            get { return modifiedDate; }
        }

        public int ModifiedUserID
        {
            set { modifiedUserID = value; }
            get { return modifiedUserID; }
        }
    }
}