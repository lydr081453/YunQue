using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Purchase.Entity
{
    public class OtherMediumInProductInfo
    {
        public OtherMediumInProductInfo()
        { }

        private int id;
        private int productID;
        private string mediaName;
        private bool isDel;
        private DateTime createdDate;
        private int createdUserID;
        private DateTime modifiedDate;
        private int modifiedUserID;


        public int ID
        {
            set { id = value; }
            get { return id; }
        }

        public int ProductID
        {
            set { productID = value; }
            get { return productID; }
        }

        public string MediaName
        {
            set { mediaName = value; }
            get { return mediaName; }
        }

        public bool IsDel
        {
            set { isDel = value; }
            get { return isDel; }
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