using System;
namespace ESP.Media.Entity
{
	public class OperatetypeInfo
	{
		#region 构造函数
        public OperatetypeInfo()
		{
			id = 0;//ID
			name = string.Empty;//Name
			altname = string.Empty;//AltName
		}
		#endregion


		#region 属性
		/// <summary>
		/// ID
		/// </summary>
		private int id;
		public int Id
		{
			get
			{
				return id;
			}
			set
			{
				id = value ;
			}
		}


		/// <summary>
		/// Name
		/// </summary>
		private string name;
		public string Name
		{
			get
			{
				return name;
			}
			set
			{
				name = value ;
			}
		}


		/// <summary>
		/// AltName
		/// </summary>
		private string altname;
		public string Altname
		{
			get
			{
				return altname;
			}
			set
			{
				altname = value ;
			}
		}


		#endregion
	}
}
