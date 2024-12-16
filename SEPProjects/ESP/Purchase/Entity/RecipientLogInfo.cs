using System;
using System.Data;

namespace ESP.Purchase.Entity
{
    /// <summary>
    ///RecipientLogInfo 的摘要说明
    /// </summary>
    public class RecipientLogInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecipientLogInfo"/> class.
        /// </summary>
        public RecipientLogInfo()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        #region Model
        private int logId;
        private int rid;
        private int logUserId;
        private DateTime logMedifiedTeme;
        private string des;
        private string prno;
        private int status;
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
        /// Gets or sets the rid.
        /// </summary>
        /// <value>The rid.</value>
        public int Rid
        {
            get { return rid; }
            set { rid = value; }
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

        private string _rlideno;
        /// <summary>
        /// Gets the rlideno.
        /// </summary>
        /// <value>The rlideno.</value>
        public string rlideno
        {
            get
            {
                _rlideno = "";
                for (int i = 0; i < (7 - this.rid.ToString().Length); i++)
                {
                    _rlideno += "0";
                }
                return _rlideno + this.rid;
            }
        }
        #endregion Model

        #region Method
        /// <summary>
        /// Popups the data.
        /// </summary>
        /// <param name="r">The r.</param>
        public void PopupData(IDataReader r)
        {
            if (r["LogId"] != DBNull.Value && r["LogId"].ToString() != "")
            {
                logId = int.Parse(r["LogId"].ToString());
            }
            if (r["RId"] != DBNull.Value && r["RId"].ToString() != "")
            {
                rid = int.Parse(r["RId"].ToString());
            }
            if (r["LogUserId"] != DBNull.Value && r["LogUserId"].ToString() != "")
            {
                logUserId = int.Parse(r["LogUserId"].ToString());
            }
            if (r["LogModifiedTime"] != DBNull.Value && r["LogModifiedTime"].ToString() != "")
            {
                logMedifiedTeme = DateTime.Parse(r["LogModifiedTime"].ToString());
            }
            des = r["des"].ToString();
            if (r["Status"] != DBNull.Value && r["Status"].ToString() != "")
            {
                Status = int.Parse(r["Status"].ToString());
            }
            IpAddress = r["IpAddress"] == DBNull.Value ? "" : r["IpAddress"].ToString();
        }
        #endregion Method
    }
}