using System;
using System.Collections.Generic;
using System.Text;
namespace MyPhotoInfo
{
    [Serializable]
    public class LotteryUserInfo
    {

        #region "Private Members"

        private int index;
        private int userid;
        private string username;
        private string name;

        #endregion

        #region "Constructors"
        public void LotteryUserEntity()
        {
        }

        public void LotteryUserEntity(int index, int userid, string username, string name)
        {
            this.index = index;
            this.userid = userid;
            this.username = username;
            this.name = name;
        }
        #endregion

        #region "Public Properties"
        public int Index
        {
            get { return index; }
            set { index = value; }
        }
        public int UserID
        {
            get { return userid; }
            set { userid = value; }
        }
        public string Username
        {
            get { return username; }
            set { username = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        #endregion
    }
}
