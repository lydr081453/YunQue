using System;
namespace ESP.Administrative.Entity
{
	/// <summary>
	/// 实体类AreaInfo 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
    /// 
    [Serializable]
    public partial class MatterReasonInfo
	{
        public MatterReasonInfo()
		{}
		#region Model
		private int _id;
        private int _SingleOverTimeID;
		private string _details;
		private DateTime _createddate;
		private DateTime _todate;
		private DateTime _startdate;
		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
            set { _id = value; }
            get { return _id; }
		}
		/// <summary>
		/// 
		/// </summary>
        public int SingleOverTimeID
		{
            set { _SingleOverTimeID = value; }
            get { return _SingleOverTimeID; }
		}
		/// <summary>
		/// 
		/// </summary>
		public string Details
		{
            set { _details = value; }
            get { return _details; }
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime CreatedDate
		{
            set { _createddate = value; }
            get { return _createddate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime EndDate
        {
            set { _todate = value; }
            get { return _todate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime StartDate
        {
            set { _startdate = value; }
            get { return _startdate; }
        }
		#endregion Model



        /// <summary>
        /// 格式化数据对象
        /// </summary>
        /// <param name="r"></param>
        public void PopupData(System.Data.IDataReader r)
        {
            if (r["ID"].ToString() != "")
            {
                _id = int.Parse(r["ID"].ToString());
            }
            _details = r["Details"].ToString();
            _SingleOverTimeID = Convert.ToInt32(r["SingleOverTimeID"].ToString());
            var objTime = r["CreatedDate"];
            if (!(objTime is DBNull))
            {
                _createddate = (DateTime)objTime;
            }
            var endTime = r["EndDate"];
            if (!(endTime is DBNull))
            {
                _todate = (DateTime)objTime;
            }
            var startTime = r["StartDate"];
            if (!(startTime is DBNull))
            {
                _startdate = (DateTime)objTime;
            }
        }

        /// <summary>
        /// 格式化数据对象
        /// </summary>
        /// <param name="r"></param>
        public void PopupData(System.Data.DataRow r)
        {
            if (r["ID"].ToString() != "")
            {
                _id = int.Parse(r["ID"].ToString());
            }
            _details = r["Details"].ToString();
            _SingleOverTimeID = Convert.ToInt32(r["SingleOverTimeID"].ToString());
            var objTime = r["CreatedDate"];
            if (!(objTime is DBNull))
            {
                _createddate = (DateTime)objTime;
            }
            var endTime = r["EndDate"];
            if (!(endTime is DBNull))
            {
                _todate = (DateTime)objTime;
            }
            var startTime = r["StartDate"];
            if (!(startTime is DBNull))
            {
                _startdate = (DateTime)objTime;
            }
        }
	}
}

