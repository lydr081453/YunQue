using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Supply.Entity
{
    public class Articles
    {
        private int _id;
        public int ID 
        {
            get { return _id; }
            set { _id = value; } 
        }

        private string _Title;
        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }

        private string _Body;
        public string Body
        {
            get { return _Body; }
            set { _Body = value; }
        }

        private DateTime _CreatedDate;
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }

        private DateTime _ModifiedDate;
        public DateTime ModifiedDate
        {
            get { return _ModifiedDate; }
            set { _ModifiedDate = value; }
        }

        private string _Author;
        public string Author
        {
            get { return _Author; }
            set { _Author = value; }
        }

        private DateTime? _RightDate;
        public DateTime? RightDate
        {
            get { return _RightDate; }
            set { _RightDate = value; }
        }

        private bool _IsCanComments;
        public bool IsCanComments
        {
            get { return _IsCanComments; }
            set { _IsCanComments = value; }
        }

        private int _ArticleTypeID;
        public int ArticleTypeID
        {
            get { return _ArticleTypeID; }
            set { _ArticleTypeID = value; }
        }

        private bool _IsHot;
        public bool IsHot
        {
            get { return _IsHot; }
            set { _IsHot = value; }
        }

        private bool _IsOnMainPage;
        public bool IsOnMainPage
        {
            get { return _IsOnMainPage; }
            set { _IsOnMainPage = value; }
        }

        private int _ViewCount;
        public int ViewCount
        {
            get { return _ViewCount; }
            set { _ViewCount = value; }
        }

        private string _Topics;
        public string Topics
        {
            get { return _Topics; }
            set { _Topics = value; }
        }

        private int _UserId;
        public int UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }

        private bool _IsRecommend;
        public bool IsRecommend
        {
            get { return _IsRecommend; }
            set { _IsRecommend = value; }
        }

        private int _Status;
        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        private string _PicPath;
        public string PicPath
        {
            get { return _PicPath; }
            set { _PicPath = value; }
        }
        private string _VideoPath;
        public string VideoPath
        {
            get { return _VideoPath; }
            set { _VideoPath = value; }
        }
        private string _VideoOldPath;
        public string VideoOldPath
        {
            get { return _VideoOldPath; }
            set { _VideoOldPath = value; }
        }

        private bool _IsUserPublish;
        public bool IsUserPublish
        {
            get { return _IsUserPublish; }
            set { _IsUserPublish = value; }
        }

        private string _Summery;
        public string Summery
        {
            get { return _Summery; }
            set { _Summery = value; }
        }

        private string _FLASHFilePath;
        public string FLASHFilePath
        {
            get { return _FLASHFilePath; }
            set { _FLASHFilePath = value; }
        }


        private int _CreatedUserId;
        public int CreatedUserId
        {
            get { return _CreatedUserId; }
            set { _CreatedUserId = value; }
        }

        private string _CreatedUserName;
        public string CreatedUserName
        {
            get { return _CreatedUserName; }
            set { _CreatedUserName = value; }
        }

        private int _CreatedUserType;
        public int CreatedUserType
        {
            get { return _CreatedUserType; }
            set { _CreatedUserType = value; }
        }

        private string _AttPath;
        public string AttPath
        {
            get { return _AttPath; }
            set { _AttPath = value; }
        }
    }
}
