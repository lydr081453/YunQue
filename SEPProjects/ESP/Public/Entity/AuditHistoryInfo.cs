using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Public.Entity
{
    public class AuditHistoryInfo
    {
        private int _id;
        private string _auditorName;
        private string _auditorNameEN;
        private int _auditorUserID;
        private int _auditorResult;
        private string _auditDesc;
        private string _auditProjectNum;
        private int _auditType;
        private int _auditRuleID;
        private DateTime _createdDate;
        private int _createdUserID;
        private string _ruleUserPosition;

        public int ID
        {
            get
            { return _id; }
            set
            { _id = value; }
        }

        public string AuditorName
        {
            get
            { return _auditorName; }
            set
            { _auditorName = value; }
        }

        public string AuditorNameEN
        {
            get
            { return _auditorNameEN; }
            set
            { _auditorNameEN = value; }
        }

        public int AuditorUserID
        {
            get
            { return _auditorUserID; }
            set
            { _auditorUserID = value; }
        }

        public int AuditorResult
        {
            get
            { return _auditorResult; }
            set
            { _auditorResult = value; }
        }

        public string AuditDesc
        {
            get
            { return _auditDesc; }
            set
            { _auditDesc = value; }
        }

        public string AuditProjectNum
        {
            get
            { return _auditProjectNum; }
            set
            { _auditProjectNum = value; }
        }

        public int AuditType
        {
            get
            { return _auditType; }
            set
            { _auditType = value; }
        }

        public int AuditRuleID
        {
            get
            { return _auditRuleID; }
            set
            { _auditRuleID = value; }
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

        public string RuleUserPosition
        {
            get
            { return _ruleUserPosition; }
            set
            { _ruleUserPosition = value; }
        }
    }
}