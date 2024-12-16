using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.HumanResource.Entity
{
    public class ProtectionLineInfo
    {
        public ProtectionLineInfo()
		{}
		#region Model
		private int _id;
		private string _protectionlinename;
		private decimal _protectionlinenameamount = decimal.Parse("0.0");
		private int _creator;
		private DateTime _createtime = DateTime.Parse("1900-01-01");
		private int _lastupdaterid;
        private DateTime _lastupdatetime = DateTime.Parse("1900-01-01");
		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
		set{ _id=value;}
		get{return _id;}
		}
		/// <summary>
		/// 底线名字
		/// </summary>
		public string ProtectionLineName
		{
		set{ _protectionlinename=value;}
		get{return _protectionlinename;}
		}
		/// <summary>
		/// 底线金额
		/// </summary>
		public decimal ProtectionLineNameAmount
		{
		set{ _protectionlinenameamount=value;}
		get{return _protectionlinenameamount;}
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
		/// 创建时间
		/// </summary>
		public DateTime CreateTime
		{
		set{ _createtime=value;}
		get{return _createtime;}
		}
		/// <summary>
		/// 最后修改人
		/// </summary>
		public int LastUpdaterID
		{
		set{ _lastupdaterid=value;}
		get{return _lastupdaterid;}
		}
		/// <summary>
		/// 最后修改时间
		/// </summary>
		public DateTime LastUpdateTime
		{
		set{ _lastupdatetime=value;}
		get{return _lastupdatetime;}
		}
		#endregion Model

    }
}
