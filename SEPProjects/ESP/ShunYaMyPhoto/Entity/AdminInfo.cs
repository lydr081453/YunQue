using System;
using System.Collections.Generic;
using System.Text;
namespace MyPhotoInfo
{
    [Serializable]
    public class AdminInfo
    {

        #region "Private Members"

        private int id;
        private int sysuserid;

        #endregion

        #region "Constructors"
        public void UsersEntity()
        {
        }

        public void UsersEntity(int id, int sysuserid)
        {
            this.id = id;
            this.sysuserid = sysuserid;
        }
        #endregion

        #region "Public Properties"
        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        public int SysUserID
        {
            get { return sysuserid; }
            set { sysuserid = value; }
        }
        #endregion
    }
}
