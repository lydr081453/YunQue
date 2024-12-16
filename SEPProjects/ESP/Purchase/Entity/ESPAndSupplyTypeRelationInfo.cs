using System;
using System.Data;

namespace ESP.Purchase.Entity
{
    /// <summary>
    /// 实体类T_Message 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class ESPAndSupplyTypeRelationInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ESPAndSupplyTypeRelationInfo"/> class.
        /// </summary>
        public ESPAndSupplyTypeRelationInfo()
        { }
        
        private int _id;
        private int _esptypeid;
        private int _supplytypeid;
        private int _createeid;
        private DateTime _createddate;
        private int _typelevel;

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }

        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        /// <value>The subject.</value>
        public int ESPTypeId
        {
            set { _esptypeid = value; }
            get { return _esptypeid; }
        }

        /// <summary>
        /// Gets or sets the body.
        /// </summary>
        /// <value>The body.</value>
        public int SupplyTypeId
        {
            set { _supplytypeid = value; }
            get { return _supplytypeid; }
        }

        /// <summary>
        /// Gets or sets the createrid.
        /// </summary>
        /// <value>The createrid.</value>
        public int CreatedUserId
        {
            set { _createeid = value; }
            get { return _createeid; }
        }

        /// <summary>
        /// Gets or sets the lasttime.
        /// </summary>
        /// <value>The lasttime.</value>
        public DateTime CreatedDate
        {
            set { _createddate = value; }
            get { return _createddate; }
        }

        /// <summary>
        /// Gets or sets the areaid.
        /// </summary>
        /// <value>The id.</value>
        public int TypeLevel
        {
            set { _typelevel = value; }
            get { return _typelevel; }
        }

        /// <summary>
        /// Popups the data.
        /// </summary>
        /// <param name="r">The r.</param>
        public void PopupData(IDataReader r)
        {
            if (null != r["id"] && r["id"].ToString() != "")
            {
                ID = int.Parse(r["id"].ToString());
            }
            if (null != r["CreatedUserId"] && r["CreatedUserId"].ToString() != "")
            {
                CreatedUserId = int.Parse(r["CreatedUserId"].ToString());
                //creatername = new ESP.Compatible.Employee(int.Parse(r["createrid"].ToString())).Name;
            }
            if (null != r["ESPTypeId"] && r["ESPTypeId"].ToString() != "")
            {
                ESPTypeId = int.Parse(r["ESPTypeId"].ToString());
            }
            if (null != r["SupplyTypeId"] && r["SupplyTypeId"].ToString() != "")
            {
                SupplyTypeId = int.Parse(r["SupplyTypeId"].ToString());
            }
            CreatedDate = DateTime.Parse(r["CreatedDate"].ToString());
            if (null != r["TypeLevel"] && r["TypeLevel"].ToString() != "")
            {
                TypeLevel = int.Parse(r["TypeLevel"].ToString());
            }
        }
    }
}