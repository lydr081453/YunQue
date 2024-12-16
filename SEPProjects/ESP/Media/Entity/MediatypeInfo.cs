using System;
namespace ESP.Media.Entity
{
	public class MediatypeInfo
	{
		#region 构造函数
        public MediatypeInfo()
		{
			id = 0;//id
			name = string.Empty;//名称
			des = string.Empty;//描述

		}
		#endregion


		#region 属性
		/// <summary>
		/// id
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
		/// 名称
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
		/// 描述

		/// </summary>
		private string des;
		public string Des
		{
			get
			{
				return des;
			}
			set
			{
				des = value ;
			}
		}


		#endregion
	}
}
