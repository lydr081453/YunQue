using System;
namespace ESP.Finance.Entity
{
	/// <summary>
	/// 实体类CustomerPOInfo 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
    [Serializable]
	public class CustomerPOInfo
	{
        public CustomerPOInfo()
		{}
		#region Model
		private int _customerpoid;
		private int? _customertmpid;
		private decimal? _poamount;
		private string _pocode;
		/// <summary>
		/// 
		/// </summary>
		public int CustomerPOID
		{
			set{ _customerpoid=value;}
			get{return _customerpoid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? CustomerTMPID
		{
			set{ _customertmpid=value;}
			get{return _customertmpid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? POAmount
		{
			set{ _poamount=value;}
			get{return _poamount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string POCode
		{
			set{ _pocode=value;}
			get{return _pocode;}
		}
		#endregion Model

	}
}

