using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Purchase.Entity
{
    public class AdvertisementTypeInfo
    {
        public AdvertisementTypeInfo()
        { }

        private int id;
        private string typeName;
        private DateTime createdDate;
        private int createdUserID;
        private DateTime modifiedDate;
        private int modifiedUserID;
        private bool isDel;

        public bool IsDel
        {
            set { isDel = value; }
            get { return isDel; }
        }
        public int ID
        {
            set { id = value; }
            get { return id; }
        }

        public string TypeName
        {
            set { typeName = value; }
            get { return typeName; }
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