using System;
namespace ESP.Media.Entity
{
	public class Search_clickcountInfo
	{
		#region 构造函数
        public Search_clickcountInfo()
		{
			id = 0;//id
			dataid = 0;//数据ID
			datatype = 0;//数据类型 1 记者 2媒体 3客户 4产品线
			clickcount = 0;//点击数
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
		/// 数据ID
		/// </summary>
		private int dataid;
		public int Dataid
		{
			get
			{
				return dataid;
			}
			set
			{
				dataid = value ;
			}
		}


		/// <summary>
		/// 数据类型 1 记者 2媒体 3客户 4产品线
		/// </summary>
		private int datatype;
		public int Datatype
		{
			get
			{
				return datatype;
			}
			set
			{
				datatype = value ;
			}
		}


		/// <summary>
		/// 点击数
		/// </summary>
		private int clickcount;
		public int Clickcount
		{
			get
			{
				return clickcount;
			}
			set
			{
				clickcount = value ;
			}
		}


		#endregion
	}
}
