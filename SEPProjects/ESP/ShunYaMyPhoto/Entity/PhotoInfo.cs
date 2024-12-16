using System;
using System.Collections.Generic;
using System.Text;
namespace MyPhotoInfo
{
    [Serializable]
    public class PhotoInfo
    {

        #region "Private Members"

        private int id;
        private int userid;
        private string fileName;
        private string smallfileName;
        private string middelFileName;
        private string photoName;
        private string description;
        private int systemAmount;
        private int browseAmount;
        private DateTime createdDate;
        private DateTime modifiedDate;
        private bool isValidate;
        private int modifiedUserID;

        #endregion

        #region "Constructors"
        public void UsersEntity()
        {
        }

        public void UsersEntity(int id, int userid, string fileName, string smallfileName, string photoName, string description
            , DateTime createdDate, int browseAmount, int systemAmount, bool isValidate, DateTime modifiedDate, int modifiedUserID, string middelFileName)
        {
            this.id = id;
            this.userid = userid;
            this.fileName = fileName;
            this.smallfileName = smallfileName;
            this.photoName = photoName;
            this.description = description;
            this.createdDate = createdDate;
            this.browseAmount = browseAmount;
            this.systemAmount = systemAmount;
            this.isValidate = isValidate;
            this.modifiedDate = modifiedDate;
            this.modifiedUserID = modifiedUserID;
            this.middelFileName = middelFileName;
        }
        #endregion

        #region "Public Properties"
        public bool IsValidate
        {
            get { return isValidate; }
            set { isValidate = value; }
        }
        public int SystemAmount
        {
            get { return systemAmount; }
            set { systemAmount = value; }
        }
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
        public int ModifiedUserID
        {
            get { return modifiedUserID; }
            set { modifiedUserID = value; }
        }

        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        public string SmallfileName
        {
            get { return smallfileName; }
            set { smallfileName = value; }
        }

        public string MiddelFileName
        {
            get { return middelFileName; }
            set { middelFileName = value; }
        }

        public string PhotoName
        {
            get { return photoName; }
            set { photoName = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public DateTime CreatedDate
        {
            get { return createdDate; }
            set { createdDate = value; }
        }

        public DateTime ModifiedDate
        {
            get { return modifiedDate; }
            set { modifiedDate = value; }
        }
        public int BrowseAmount
        {
            get { return browseAmount; }
            set { browseAmount = value; }
        }
        #endregion
    }
}