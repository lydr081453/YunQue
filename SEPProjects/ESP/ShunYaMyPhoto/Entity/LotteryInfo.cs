using System;
using System.Collections.Generic;
using System.Text;
namespace MyPhotoInfo
{
    [Serializable]
    public class LotteryInfo
    {

        #region "Private Members"

        private int id;
        private int userid;
        private DateTime createdDate;
        private string lotteryLevel;
        private string ip;

        #endregion

        #region "Constructors"
        public void LotteryEntity()
        {
        }

        public void LotteryEntity(int id, int userid, DateTime createdDate, string lotteryLevel, string ip)
        {
            this.id = id;
            this.userid = userid;
            this.createdDate = createdDate;
            this.lotteryLevel = lotteryLevel;
            this.ip = ip;
        }
        #endregion

        #region "Public Properties"
        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        public int UserID
        {
            get { return userid; }
            set { userid = value; }
        }
        public DateTime CreatedDate
        {
            get { return createdDate; }
            set { createdDate = value; }
        }
        public string LotteryLevel
        {
            get { return lotteryLevel; }
            set { lotteryLevel = value; }
        }
        public string IP
        {
            get { return ip; }
            set { ip = value; }
        }
        #endregion
    }
}
