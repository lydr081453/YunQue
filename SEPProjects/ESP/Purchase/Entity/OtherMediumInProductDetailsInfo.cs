using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Purchase.Entity
{
    public class OtherMediumInProductDetailsInfo
    {
        public OtherMediumInProductDetailsInfo()
        { }

        private int id;
        private string area;
        private int manuscriptType;
        private string newsPrice;
        private string description;
        private string layout;
        private string hopePrice;
        private string shunYaDescription;
        private DateTime createdDate;
        private int createdUserID;
        private DateTime modifiedDate;
        private int modifiedUserID;
        private int mediaProductID;
        private string unit;
        private string titlePrice;
        private string discount;
        private bool isHavePic;

        public bool IsHavePic
        {
            set { isHavePic = value; }
            get { return isHavePic; }
        }

        public string Discount
        {
            set { discount = value; }
            get { return discount; }
        }
        public string TitlePrice
        {
            set { titlePrice = value; }
            get { return titlePrice; }
        }
        public int MediaProductID
        {
            set { mediaProductID = value; }
            get { return mediaProductID; }
        }

        public int ID
        {
            set { id = value; }
            get { return id; }
        }

        public string Area
        {
            set { area = value; }
            get { return area; }
        }
        
        public int ManuscriptType
        {
            set { manuscriptType = value; }
            get { return manuscriptType; }
        }

        public string   NewsPrice
        {
            set { newsPrice = value; }
            get { return newsPrice; }
        }
        public string Unit
        {
            set { unit = value; }
            get { return unit; }
        }

        public string Description
        {
            set { description = value; }
            get { return description; }
        }

        public string Layout
        {
            set { layout = value; }
            get { return layout; }
        }

        public string HopePrice
        {
            set { hopePrice = value; }
            get { return hopePrice; }
        }

        public string ShunYaDescription
        {
            set { shunYaDescription = value; }
            get { return shunYaDescription; }
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
