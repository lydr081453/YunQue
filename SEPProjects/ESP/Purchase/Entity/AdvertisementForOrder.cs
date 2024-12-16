using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Purchase.Entity
{
    public class AdvertisementForOrderInfo
    {
        public AdvertisementForOrderInfo()
        { }

        private int id;
        private int orderID;
        private int advertisementID;
        private int advertisementDetailsID;
        private string mediaType;
        private string mediaName;
        private string advertisementExemplar;
        private decimal priceTotal;
        private decimal discount;
        private decimal total;
        private decimal distributionPercent;
        private decimal distributionPrice;
        private decimal returnPoint;
        private string accountPeriod;
        private DateTime createdDate;
        private int createdUserID;
        private DateTime modifiedDate;
        private int modifiedUserID;
        private bool isDel;

        public int ID
        {
            set { id = value; }
            get { return id; }
        }

        public int OrderID
        {
            set { orderID = value; }
            get { return orderID; }
        }

        public int AdvertisementID
        {
            set { advertisementID = value; }
            get { return advertisementID; }
        }

        public int AdvertisementDetailsID
        {
            set { advertisementDetailsID = value; }
            get { return advertisementDetailsID; }
        }

        public string MediaType
        {
            set { mediaType = value; }
            get { return mediaType; }
        }

        public string MediaName
        {
            set { mediaName = value; }
            get { return mediaName; }
        }

        public string AdvertisementExemplar
        {
            set { advertisementExemplar = value; }
            get { return advertisementExemplar; }
        }

        public decimal PriceTotal
        {
            set { priceTotal = value; }
            get { return priceTotal; }
        }

        public decimal Discount
        {
            set { discount = value; }
            get { return discount; }
        }

        public decimal Total
        {
            set { total = value; }
            get { return total; }
        }

        public decimal DistributionPercent
        {
            set { distributionPercent = value; }
            get { return distributionPercent; }
        }

        public decimal DistributionPrice
        {
            set { distributionPrice = value; }
            get { return distributionPrice; }
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

        public bool IsDel
        {
            set { isDel = value; }
            get { return isDel; }
        }
    }
}