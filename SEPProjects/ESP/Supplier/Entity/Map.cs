using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Supplier.Entity
{
    public class Map
    {
        private string _questionnum;
        private int _qid;

        public string QuestionNum
        {
            set { _questionnum = value; }
            get { return _questionnum; }
        }

        public int QId
        {
            set { _qid = value; }
            get { return _qid; }
        }

    }
}
