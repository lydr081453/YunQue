using System;
namespace ESP.Finance.Entity
{
    /// <summary>
    /// 实体类AreaInfo 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    /// 
    [Serializable]
    public partial class MediumForProjectInfo
	{
        public MediumForProjectInfo()
		{}
		#region Model
        private int _id;
        private int _projectid;
        private int _mediaid;
        private int _createduserid;
        private int _modifieduserid;

        private DateTime _modifiedDate;
        private DateTime _createdDate;
        private bool _isDel;
		/// <summary>
		/// ID
		/// </summary>
        public int ID
		{
            set { _id = value; }
            get { return _id; }
		}
		/// <summary>
        /// ProjectID
		/// </summary>
        public int ProjectID
		{
            set { _projectid = value; }
            get { return _projectid; }
		}
		/// <summary>
		/// 
        /// </summary>
        public int MediaID
        {
            set { _mediaid = value; }
            get { return _mediaid; }
        }
		/// <summary>
		/// 
        /// </summary>
        public int CreatedUserID
        {
            set { _createduserid = value; }
            get { return _createduserid; }
        }
		/// <summary>
		/// 
		/// </summary>
        public int ModifiedUserID
		{
            set { _modifieduserid = value; }
            get { return _modifieduserid; }
		}
		/// <summary>
		/// 
		/// </summary>
        public DateTime ModifiedDate
		{
            set { _modifiedDate = value; }
            get { return _modifiedDate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreatedDate
        {
            set { _createdDate = value; }
            get { return _createdDate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsDel
        {
            set { _isDel = value; }
            get { return _isDel; }
        }
		#endregion Model

	}
}
