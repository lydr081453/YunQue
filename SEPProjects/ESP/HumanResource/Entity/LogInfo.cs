using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace ESP.HumanResource.Entity
{
    public class LogInfo
    {
        public LogInfo()
        { }

        private int logId;
        private int logUserId;
        private DateTime logMedifiedTeme;
        private string des;
        private int status;

        public string Des
        {
            get { return des; }
            set { des = value; }
        }
        public int LogId
        {
            get { return logId; }
            set { logId = value; }
        }
        public int LogUserId
        {
            get { return logUserId; }
            set { logUserId = value; }
        }
        public DateTime LogMedifiedTeme
        {
            get { return logMedifiedTeme; }
            set { logMedifiedTeme = value; }
        }
        public int Status
        {
            get { return status; }
            set { status = value; }
        }

        private string logUserName;
        public string LogUserName
        {
            set { logUserName = value; }
            get { return logUserName; }
        }

        private int sysId;
        public int SysId
        {
            set { sysId = value; }
            get { return sysId; }
        }

        private string sysUserName;
        public string SysUserName
        {
            set { sysUserName = value; }
            get { return sysUserName; }
        }


        public void PopupData(IDataReader r)
        {
            if (r["LogId"].ToString() != "")
            {
                this.logId = int.Parse(r["LogId"].ToString());
            }
            if (r["LogUserId"].ToString() != "")
            {
                this.logUserId = int.Parse(r["LogUserId"].ToString());
            }
            if (r["LogModifiedTime"].ToString() != "")
            {
                this.logMedifiedTeme = DateTime.Parse(r["LogModifiedTime"].ToString());
            }
            des = r["des"].ToString();
            if (r["Status"].ToString() != "")
            {
                this.Status = int.Parse(r["Status"].ToString());
            }
            this.logUserName = r["LogUserName"].ToString();
            this.sysUserName = r["sysUserName"].ToString();
            if (r["sysId"].ToString() != "")
                this.sysId = int.Parse(r["sysId"].ToString());
        }
    }
}
