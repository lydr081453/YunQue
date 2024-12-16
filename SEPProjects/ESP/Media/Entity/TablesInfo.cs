using System;
namespace ESP.Media.Entity
{
	public class TablesInfo
	{
		#region 构造函数
        public TablesInfo()
		{
			tableid = 0;//TableID
			tablename = string.Empty;//TableName
			alttablename = string.Empty;//AltTableName
		}
		#endregion


		#region 属性
		/// <summary>
		/// TableID
		/// </summary>
		private int tableid;
		public int Tableid
		{
			get
			{
				return tableid;
			}
			set
			{
				tableid = value ;
			}
		}


		/// <summary>
		/// TableName
		/// </summary>
		private string tablename;
		public string Tablename
		{
			get
			{
				return tablename;
			}
			set
			{
				tablename = value ;
			}
		}


		/// <summary>
		/// AltTableName
		/// </summary>
		private string alttablename;
		public string Alttablename
		{
			get
			{
				return alttablename;
			}
			set
			{
				alttablename = value ;
			}
		}


		#endregion
	}
}
