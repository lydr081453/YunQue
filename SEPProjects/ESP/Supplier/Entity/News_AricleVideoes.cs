using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Supply.Entity
{
    public class AricleVideoes
    {
        private int _id;
        public int ID 
        {
            get { return _id; }
            set { _id = value; }
        }
        private int _articleid;
        public int ArticleID
        {
            get { return _articleid; }
            set { _articleid = value; }
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

        private string _LocalVideoPath;
        public string LocalVideoPath
        {
            get { return _LocalVideoPath; }
            set { _LocalVideoPath = value; }
        }
    }
}
