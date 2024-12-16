using System;
using System.Data;

namespace ESP.Purchase.Entity
{
    /// <summary>
    ///T_Log 的摘要说明
    /// </summary>
    public class LogInfo
    {
        public LogInfo()
        { }

        private int logId;
        private int gid;
        private int logUserId;
        private DateTime logMedifiedTeme;
        private string des;
        private string prno;
        private int status;
        private string _glideno;
        private string _IpAddress;
        /// <summary>
        /// IP地址
        /// </summary>
        public string IpAddress
        {
            get { return _IpAddress; }
            set { _IpAddress = value; }
        }

        /// <summary>
        /// Gets or sets the DES.
        /// </summary>
        /// <value>The DES.</value>
        public string Des
        {
            get { return des; }
            set { des = value; }
        }

        /// <summary>
        /// Gets or sets the log id.
        /// </summary>
        /// <value>The log id.</value>
        public int LogId
        {
            get { return logId; }
            set { logId = value; }
        }

        /// <summary>
        /// Gets or sets the gid.
        /// </summary>
        /// <value>The gid.</value>
        public int Gid
        {
            get { return gid; }
            set { gid = value; }
        }

        /// <summary>
        /// Gets or sets the log user id.
        /// </summary>
        /// <value>The log user id.</value>
        public int LogUserId
        {
            get { return logUserId; }
            set { logUserId = value; }
        }

        /// <summary>
        /// Gets or sets the log medified teme.
        /// </summary>
        /// <value>The log medified teme.</value>
        public DateTime LogMedifiedTeme
        {
            get { return logMedifiedTeme; }
            set { logMedifiedTeme = value; }
        }

        /// <summary>
        /// Gets or sets the pr no.
        /// </summary>
        /// <value>The pr no.</value>
        public string PrNo
        {
            get { return prno; }
            set { prno = value; }
        }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public int Status
        {
            get { return status; }
            set { status = value; }
        }

        /// <summary>
        /// Gets the glideno.
        /// </summary>
        /// <value>The glideno.</value>
        public string glideno
        {
            get
            {
                _glideno = "";
                for (int i = 0; i < (7 - this.gid.ToString().Length); i++)
                {
                    _glideno += "0";
                }
                return _glideno + this.gid;
            }
        }

        /// <summary>
        /// Popups the data.
        /// </summary>
        /// <param name="r">The r.</param>
        public void PopupData(IDataReader r)
        {
            if (null != r["LogId"] && r["LogId"].ToString() != "")
            {
                logId = int.Parse(r["LogId"].ToString());
            }
            if (null != r["Gid"] && r["Gid"].ToString() != "")
            {
                gid = int.Parse(r["Gid"].ToString());
            }
            if (null != r["LogUserId"] && r["LogUserId"].ToString() != "")
            {
                logUserId = int.Parse(r["LogUserId"].ToString());
            }
            if (null != r["LogModifiedTime"] && r["LogModifiedTime"].ToString() != "")
            {
                logMedifiedTeme = DateTime.Parse(r["LogModifiedTime"].ToString());
            }
            des = r["des"].ToString();
            if (null != r["Status"] && r["Status"].ToString() != "")
            {
                Status = int.Parse(r["Status"].ToString());
            }
            IpAddress = r["IpAddress"] == DBNull.Value ? "" : r["IpAddress"].ToString();
        }
    }
}