using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Public.Entity
{
    public class AuditRulesInfo
    {
        private int _id;
        private int _auditorUserSysID;
        private string _auditorUserName;
        private string _auditorUserCode;
        private string _auditorEmployeeName;
        private string _suggenstion;
        private DateTime _auditedDate;
        private int _status;
        private int _formType;
        private int _formID;

        public int ID
        {
            get 
            {   return _id;}
            set
            {   _id = value;}
        }

        public int AuditorUserSysID
        {
            get
            { return _auditorUserSysID; }
            set
            { _auditorUserSysID = value; }
        }

        public int Status
        {
            get
            { return _status; }
            set
            { _status = value; }
        }

        public int FormType
        {
            get
            { return _formType; }
            set
            { _formType = value; }
        }

        public int FormID
        {
            get
            { return _formID; }
            set
            { _formID = value; }
        }

        public string AuditorUserName
        {
            get
            { return _auditorUserName; }
            set
            { _auditorUserName = value; }
        }

        public string AuditorUserCode
        {
            get
            { return _auditorUserCode; }
            set
            { _auditorUserCode = value; }
        }

        public string AuditorEmployeeName
        {
            get
            { return _auditorEmployeeName; }
            set
            { _auditorEmployeeName = value; }
        }

        public string Suggenstion
        {
            get
            { return _suggenstion; }
            set
            { _suggenstion = value; }
        }

        public DateTime AuditedDate
        {
            get
            { return _auditedDate; }
            set
            { _auditedDate = value; }
        }
    }
}