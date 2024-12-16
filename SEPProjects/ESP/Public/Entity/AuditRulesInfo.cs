using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Public.Entity
{
    public class AuditLogsInfo
    {
        private int _id;
        private string _ruleUserName;
        private string _ruleUserNameEN;
        private int _ruleUserSysID;
        private string _ruleUserPosition;
        private DateTime _createdDate;
        private int _createdUserID;
        private int _auditorNumber;
        private int _nextAuditorNumber;

        public int ID
        {
            get
            { return _id; }
            set
            { _id = value; }
        }

        public string RuleUserName
        {
            get
            { return _ruleUserName; }
            set
            { _ruleUserName = value; }
        }

        public string RuleUserNameEN
        {
            get
            { return _ruleUserNameEN; }
            set
            { _ruleUserNameEN = value; }
        }

        public int RuleUserSysID
        {
            get
            { return _ruleUserSysID; }
            set
            { _ruleUserSysID = value; }
        }

        public string RuleUserPosition
        {
            get
            { return _ruleUserPosition; }
            set
            { _ruleUserPosition = value; }
        }

        public DateTime CreatedDate
        {
            get
            { return _createdDate; }
            set
            { _createdDate = value; }
        }

        public int CreatedUserID
        {
            get
            { return _createdUserID; }
            set
            { _createdUserID = value; }
        }

        public int AuditorNumber
        {
            get
            { return _auditorNumber; }
            set
            { _auditorNumber = value; }
        }

        public int NextAuditorNumber
        {
            get
            { return _nextAuditorNumber; }
            set
            { _nextAuditorNumber = value; }
        }
    }
}