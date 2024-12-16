using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.UserPoint.Entity
{
    /// <summary>
    /// 
    /// </summary>
   public class UserPointInfo
    {
       /// <summary>
       /// 
       /// </summary>
        public UserPointInfo()
		{}
		#region Model
		private int _userid;
		private int _sp;
		private byte[] _timestamp;
       /// <summary>
       /// 
       /// </summary>
        public int ID { get; set; }
		/// <summary>
		/// 用户编号
		/// </summary>
		public int UserID
		{
		set{ _userid=value;}
		get{return _userid;}
		}
		/// <summary>
		/// 供应链积分
		/// </summary>
		public int SP
		{
		set{ _sp=value;}
		get{return _sp;}
		}
		/// <summary>
		/// 时间戳（避免出现积分修改脏数据）
		/// </summary>
        public byte[] TimeStamp
		{
		set{ _timestamp=value;}
		get{return _timestamp;}
        }
        #endregion
    }
}
