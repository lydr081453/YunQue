using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Supply.Entity
{
    public class ArticlesComment
    {
        public int ID { get; set; }

        public string CommentBody { get; set; }

        public string CommentTitle { get; set; }

        public DateTime CreatedDate { get; set; }

        public int CreatedUserID { get; set; }

        public int ArticleID { get; set; }
    }
}
