using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Supplier.Entity
{
    public class SC_Advice
    {
        private int _ID;
        private string _AdviceType;
        private string _AdviceTitle;
        private string _AdviceContent;
        private int _CommitUser;
        private string _CommitUserName;
        private string _CommitEmail;
        private DateTime _CommitDate;
        private string _CommitIp;
        private int _CommitType;

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public string AdviceType
        {
            get { return _AdviceType; }
            set { _AdviceType = value; }
        }

        public string AdviceTitle
        {
            get { return _AdviceTitle; }
            set { _AdviceTitle = value; }
        }

        public string AdviceContent
        {
            get { return _AdviceContent; }
            set { _AdviceContent = value; }
        }

        public int CommitUser
        {
            get { return _CommitUser; }
            set { _CommitUser = value; }
        }

        public string CommitUserName
        {
            get { return _CommitUserName; }
            set { _CommitUserName = value; }
        }

        public string CommitEmail
        {
            get { return _CommitEmail; }
            set { _CommitEmail = value; }
        }

        public DateTime CommitDate
        {
            get { return _CommitDate; }
            set { _CommitDate = value; }
        }

        public string CommitIp
        {
            get { return _CommitIp; }
            set { _CommitIp = value; }
        }

        public int CommitType
        {
            get { return _CommitType; }
            set { _CommitType = value; }
        }
    }
}
