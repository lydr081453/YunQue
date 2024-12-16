using System;
namespace ESP.Finance.Entity
{
	/// <summary>
	/// 实体类AreaInfo 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
    /// 
    [Serializable]
    public partial class AreaInfo
	{
        public AreaInfo()
		{}
		#region Model
		private int _areaid;
		private string _areacode;
		private string _areaname;
		private string _searchcode;
		private string _description;
		private string _others;
		/// <summary>
		/// 
		/// </summary>
		public int AreaID
		{
			set{ _areaid=value;}
			get{return _areaid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AreaCode
		{
			set{ _areacode=value;}
			get{return _areacode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AreaName
		{
			set{ _areaname=value;}
			get{return _areaname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SearchCode
		{
			set{ _searchcode=value;}
			get{return _searchcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Description
		{
			set{ _description=value;}
			get{return _description;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Others
		{
			set{ _others=value;}
			get{return _others;}
		}
		#endregion Model

	}
}

