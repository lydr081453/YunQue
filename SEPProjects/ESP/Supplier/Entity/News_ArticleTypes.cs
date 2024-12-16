using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Supply.Entity
{
    public class ArticleTypes
    {
        public int ID { get; set; }

        public string TypeName { get; set; }

        public string TypeCode { get; set; }

        public bool IsDel { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
