using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.UserPoint.Entity
{
    /// <summary>
    /// test
    /// </summary>
   public class UserPointRecordInfo
    {
       /// <summary>
       /// test
       /// </summary>
       public UserPointRecordInfo()
		{}
		#region Model
		private int _userid;
		private int _ruleid;
		private int _giftid;
		private int _points;
		private string _memo;
		private DateTime _operationtime;
        private byte[] _timestamp;
       /// <summary>
       /// 
       /// </summary>
        public int RecordId { get; set; }
       /// <summary>
       /// 
       /// </summary>
        public int RefrenceID { get; set; }
		/// <summary>
		/// 用户编号
		/// </summary>
		public int UserID
		{
		set{ _userid=value;}
		get{return _userid;}
		}
		/// <summary>
		/// 规则编号，根据哪个积分规则获取的积分
		/// </summary>
		public int RuleID
		{
		set{ _ruleid=value;}
		get{return _ruleid;}
		}
		/// <summary>
		/// 礼品编号，购买了哪个礼品消费了积分
		/// </summary>
		public int GiftID
		{
		set{ _giftid=value;}
		get{return _giftid;}
		}
		/// <summary>
		/// 获取或消费的积分，正整数则为获取，负整数则为消费
		/// </summary>
		public int Points
		{
		set{ _points=value;}
		get{return _points;}
		}
		/// <summary>
		/// 用户获取或消费积分的描述，与什么单据、什么操作或什么礼品有关
		/// </summary>
		public string Memo
		{
		set{ _memo=value;}
		get{return _memo;}
		}
		/// <summary>
		/// 操作时间
		/// </summary>
		public DateTime OperationTime
		{
		set{ _operationtime=value;}
		get{return _operationtime;}
		}
		/// <summary>
		/// 时间戳
		/// </summary>
        public byte[] TimeStamp
		{
		set{ _timestamp=value;}
		get{return _timestamp;}
		}
		#endregion Model
    }
}
