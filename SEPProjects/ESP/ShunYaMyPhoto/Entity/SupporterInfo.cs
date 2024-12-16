using System;
using System.Collections.Generic;
using System.Text;
namespace MyPhotoInfo
{
    [Serializable]
    public class SupporterInfo
    {

        #region "Private Members"

        private int id;
        private int photoID;
        private int supporterUserID;
        private DateTime createdDate;
        private string message;
        private string ip;
        #endregion

        #region "Constructors"
        public void SupportLogEntity()
        {
        }

        public void SupportLogEntity(int id, int photoID, int supporterUserID, DateTime createdDate, string message,string ip)
        {
            this.id = id;
            this.photoID = photoID;
            this.supporterUserID = supporterUserID;
            this.createdDate = createdDate;
            this.message = message;
            this.ip = ip;
        }
        #endregion

        #region "Public Properties"
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public string IP
        {
            get { return ip; }
            set { ip = value; }
        }

        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        public int PhotoID
        {
            get { return photoID; }
            set { photoID = value; }
        }

        public int SupporterUserID
        {
            get { return supporterUserID; }
            set { supporterUserID = value; }
        }

        public DateTime CreatedDate
        {
            get { return createdDate; }
            set { createdDate = value; }
        }
        #endregion
    }
}
