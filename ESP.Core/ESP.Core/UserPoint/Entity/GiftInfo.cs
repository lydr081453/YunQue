using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.UserPoint.Entity
{
    /// <summary>
    /// 
    /// </summary>
   public class GiftInfo
    {
       /// <summary>
       /// 
       /// </summary>
       public GiftInfo()
		{}
		#region Model
		private int _giftid;
		private string _code;
		private string _name;
		private string _description;
		private int _points;
		private int _count;
		private int _state;
		private DateTime _createtime;
		private int _creator;
		private byte[] _timestamp;
        private string _images;
		/// <summary>
		/// 礼品编号
		/// </summary>
		public int GiftID
		{
		set{ _giftid=value;}
		get{return _giftid;}
		}
		/// <summary>
		/// 礼品代码
		/// </summary>
		public string Code
		{
		set{ _code=value;}
		get{return _code;}
		}
		/// <summary>
		/// 礼品名称
		/// </summary>
		public string Name
		{
		set{ _name=value;}
		get{return _name;}
		}
		/// <summary>
		/// 礼品描述
		/// </summary>
		public string Description
		{
		set{ _description=value;}
		get{return _description;}
		}
		/// <summary>
		/// 所需积分
		/// </summary>
		public int Points
		{
		set{ _points=value;}
		get{return _points;}
		}
		/// <summary>
		/// 剩余数量
		/// </summary>
		public int Count
		{
		set{ _count=value;}
		get{return _count;}
		}
		/// <summary>
		/// 当前状态
		/// </summary>
		public int State
		{
		set{ _state=value;}
		get{return _state;}
		}
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreateTime
		{
		set{ _createtime=value;}
		get{return _createtime;}
		}
		/// <summary>
		/// 创建人
		/// </summary>
		public int Creator
		{
		set{ _creator=value;}
		get{return _creator;}
		}
		/// <summary>
		/// 时间戳，为了避免商品购买并发产生的脏数据
		/// </summary>
        public byte[] TimeStamp
		{
		set{ _timestamp=value;}
		get{return _timestamp;}
		}
       /// <summary>
       /// 
       /// </summary>
        public string Images
        {
            set { _images = value; }
            get { return _images; }
        }
		#endregion Model
    }
}
