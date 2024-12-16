using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Purchase.Entity
{
    public class OtherMediumForOrderInfo
    {
        public OtherMediumForOrderInfo()
        { }

        private int id;
        private int orderID;
        private int mediaID;
        private string mediaName;
        private string customerName;
        private string title;
        private string ofSpace;
        private DateTime startDate;
        private decimal wordsCount;
        private string picSize;
        private string layoutSize;
        private string color;
        private string price;
        private string unit;
        private string amount;
        private string discount;
        private DateTime createdDate;
        private int createdUserID;
        private DateTime modifiedDate;
        private int modifiedUserID;
        private string otherFees;
        private bool isAccessories;
        private string description;

        private string mediaArea;
        private string mediaDescription;
        private string mediaShunYaDescription;

        public string MediaDescription
        {
            set { mediaDescription = value; }
            get { return mediaDescription; }
        }
        public string MediaShunYaDescription
        {
            set { mediaShunYaDescription = value; }
            get { return mediaShunYaDescription; }
        }
        public string MediaArea
        {
            set { mediaArea = value; }
            get { return mediaArea; }
        }
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

        public int MediaID
        {
            set { mediaID = value; }
            get { return mediaID; }
        }

        public string MediaName
        {
            set { mediaName = value; }
            get { return mediaName; }
        }

        public string CustomerName
        {
            set { customerName = value; }
            get { return customerName; }
        }

        public string Title
        {
            set { title = value; }
            get { return title; }
        }

        public string OfSpace
        {
            set { ofSpace = value; }
            get { return ofSpace; }
        }

        public DateTime StartDate
        {
            set { startDate = value; }
            get { return startDate; }
        }

        public decimal WordsCount
        {
            set { wordsCount = value; }
            get { return wordsCount; }
        }

        public string PicSize
        {
            set { picSize = value; }
            get { return picSize; }
        }

        public string LayoutSize
        {
            set { layoutSize = value; }
            get { return layoutSize; }
        }

        public string Color
        {
            set { color = value; }
            get { return color; }
        }

        public string Price
        {
            set { price = value; }
            get { return price; }
        }

        public string Unit
        {
            set { unit = value; }
            get { return unit; }
        }

        public string Amount
        {
            set { amount = value; }
            get { return amount; }
        }

        public string Discount
        {
            set { discount = value; }
            get { return discount; }
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

        public string OtherFees
        {
            set { otherFees = value; }
            get { return otherFees; }
        }

        public bool IsAccessories
        {
            set { isAccessories = value; }
            get { return isAccessories; }
        }

        public string Description
        {
            set { description = value; }
            get { return description; }
        }
    }
}