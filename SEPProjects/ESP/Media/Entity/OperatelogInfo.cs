using System;
namespace ESP.Media.Entity
{
	public class OperatelogInfo
	{
		#region 构造函数
        public OperatelogInfo()
		{
			id = 0;//ID
			userid = 0;//UserID
			operatetypeid = 0;//OperateTypeID
			operatetableid = 0;//OperateTableID
			operatetime = string.Empty;//OperateTime
			operatedes = string.Empty;//OperateDes
			integralid = 0;//IntegralID
			integral = 0;//Integral
			del = 0;//Del
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
		/// UserID
		/// </summary>
		private int userid;
		public int Userid
		{
			get
			{
				return userid;
			}
			set
			{
				userid = value ;
			}
		}


		/// <summary>
		/// OperateTypeID
		/// </summary>
		private int operatetypeid;
		public int Operatetypeid
		{
			get
			{
				return operatetypeid;
			}
			set
			{
				operatetypeid = value ;
			}
		}


		/// <summary>
		/// OperateTableID
		/// </summary>
		private int operatetableid;
		public int Operatetableid
		{
			get
			{
				return operatetableid;
			}
			set
			{
				operatetableid = value ;
			}
		}


		/// <summary>
		/// OperateTime
		/// </summary>
		private string operatetime;
		public string Operatetime
		{
			get
			{
				return operatetime;
			}
			set
			{
				operatetime = value ;
			}
		}


		/// <summary>
		/// OperateDes
		/// </summary>
		private string operatedes;
		public string Operatedes
		{
			get
			{
				return operatedes;
			}
			set
			{
				operatedes = value ;
			}
		}


		/// <summary>
		/// IntegralID
		/// </summary>
		private int integralid;
		public int Integralid
		{
			get
			{
				return integralid;
			}
			set
			{
				integralid = value ;
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
		/// Del
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
