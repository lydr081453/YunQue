using System;
using System.Data;

namespace ESP.Purchase.Entity
{
    /// <summary>
    /// 实体类T_Message 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class MessageInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageInfo"/> class.
        /// </summary>
        public MessageInfo()
        { }
        
        private int _id;
        private string _subject;
        private string _body;
        private int _createrid;
        private DateTime _lasttime;
        private DateTime _createtime;
        private string _creatername;
        private int _areaid;
        private string _attFile;
        /// <summary>
        /// 附件
        /// </summary>
        public string attFile
        {
            get { return _attFile; }
            set { _attFile = value; }
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }

        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        /// <value>The subject.</value>
        public string subject
        {
            set { _subject = value; }
            get { return _subject; }
        }

        /// <summary>
        /// Gets or sets the body.
        /// </summary>
        /// <value>The body.</value>
        public string body
        {
            set { _body = value; }
            get { return _body; }
        }

        /// <summary>
        /// Gets or sets the createrid.
        /// </summary>
        /// <value>The createrid.</value>
        public int createrid
        {
            set { _createrid = value; }
            get { return _createrid; }
        }

        /// <summary>
        /// Gets or sets the lasttime.
        /// </summary>
        /// <value>The lasttime.</value>
        public DateTime lasttime
        {
            set { _lasttime = value; }
            get { return _lasttime; }
        }

        /// <summary>
        /// Gets or sets the createtime.
        /// </summary>
        /// <value>The createtime.</value>
        public DateTime createtime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }

        /// <summary>
        /// Gets or sets the creatername.
        /// </summary>
        /// <value>The creatername.</value>
        public string creatername
        {
            set { _creatername = value; }
            get { return _creatername; }
        }

        /// <summary>
        /// Gets or sets the areaid.
        /// </summary>
        /// <value>The id.</value>
        public int areaid
        {
            set { _areaid = value; }
            get { return _areaid; }
        }

        /// <summary>
        /// Popups the data.
        /// </summary>
        /// <param name="r">The r.</param>
        public void PopupData(IDataReader r)
        {
            if (null != r["id"] && r["id"].ToString() != "")
            {
                id = int.Parse(r["id"].ToString());
            }
            if (null != r["createrid"] && r["createrid"].ToString() != "")
            {
                createrid = int.Parse(r["createrid"].ToString());
                creatername = new ESP.Compatible.Employee(int.Parse(r["createrid"].ToString())).Name;
            }
            subject = r["subject"].ToString();
            body = r["body"].ToString();
            createtime = DateTime.Parse(r["createtime"].ToString());
            lasttime = DateTime.Parse(r["lasttime"].ToString());
            if (null != r["areaid"] && r["areaid"].ToString() != "")
            {
                areaid = int.Parse(r["areaid"].ToString());
            }
            attFile = r["attFile"] == DBNull.Value ? "" : r["attFile"].ToString();
        }
    }
}