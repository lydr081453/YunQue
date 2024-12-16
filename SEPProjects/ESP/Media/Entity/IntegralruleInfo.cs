using System;
namespace ESP.Media.Entity
{
	public class IntegralruleInfo
	{
		#region 构造函数
        public IntegralruleInfo()
		{
			id = 0;//id
			operateid = 0;//OperateID
			tableid = 0;//TableID
			name = string.Empty;//英文名称
			altname = string.Empty;//中文名称
			integral = 0;//Integral
			del = 0;//删除标记
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
		/// OperateID
		/// </summary>
		private int operateid;
		public int Operateid
		{
			get
			{
				return operateid;
			}
			set
			{
				operateid = value ;
			}
		}


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
		/// 英文名称
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
		/// 中文名称
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


		/// <summary>
		/// Integral
		/// </summary>
		private int integral;
		public int Integral
		{
			get
			{
				return integral;
			}
			set
			{
				integral = value ;
			}
		}


		/// <summary>
		/// 删除标记
		/// </summary>
		private int del;
		public int Del
		{
			get
			{
				return del;
			}
			set
			{
				del = value ;
			}
		}


		#endregion
	}
}
