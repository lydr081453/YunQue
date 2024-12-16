using System;
using System.Data;

namespace ESP.Purchase.Entity
{
    /// <summary>
    /// 实体类T_Watch 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class sepArticleInfo
    {
        public sepArticleInfo()
        { }
        #region Model
        private int _id;
        private int _sysUserID;
        private DateTime? _createdDate;
        private bool _isRead;

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
        public DateTime? CreatedDate
        {
            set { _createdDate = value; }
            get { return _createdDate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int SysUserID
        {
            set { _sysUserID = value; }
            get { return _sysUserID; }
        }

        public bool IsRead
        {
            get { return _isRead; }
            set { _isRead = value; }
        }
        #endregion Model


        /// <summary>
        /// Popups the data.
        /// </summary>
        /// <param name="r">The r.</param>
        public void PopupData(IDataReader r)
        {
            if (null != r["ID"] && r["ID"].ToString() != "")
            {
                ID = int.Parse(r["ID"].ToString());
            }
            if (null != r["SysUserID"] && r["SysUserID"].ToString() != "")
            {
                SysUserID = int.Parse(r["SysUserID"].ToString());
            }
            if (null != r["CreatedDate"] && r["CreatedDate"].ToString() != "")
            {
                CreatedDate = Convert.ToDateTime(r["CreatedDate"].ToString());
            }
            if (null != r["IsRead"] && r["IsRead"].ToString() != "")
            {
                IsRead = Convert.ToBoolean(r["IsRead"].ToString());
            }
        }
    }
}
