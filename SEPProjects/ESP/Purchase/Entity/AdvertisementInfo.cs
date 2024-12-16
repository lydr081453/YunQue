using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Purchase.Entity
{
    public class AdvertisementInfo
    {
        public AdvertisementInfo()
        { }

        private int id;
        private int mediaTypeID;
        private string mediaName;
        private DateTime createdDate;
        private int createdUserID;
        private DateTime modifiedDate;
        private int modifiedUserID;
        private bool isDel;
        private string description;
        private int productTypeID;


        public int ProductTypeID
        {
            set { productTypeID = value; }
            get { return productTypeID; }
        }
        public string Description
        {
            set { description = value; }
            get { return description; }
        }

        public int ID
        {
            set { id = value; }
            get { return id; }
        }

        public int MediaTypeID
        {
            set { mediaTypeID = value; }
            get { return mediaTypeID; }
        }

        public string MediaName
        {
            set { mediaName = value; }
            get { return mediaName; }
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