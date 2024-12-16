using System;
namespace ESP.Media.Entity
{
	public class TrustedusersInfo
	{
		#region 构造函数
        public TrustedusersInfo()
		{
			userid = 0;//UserID
		}
		#endregion


		#region 属性
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


		#endregion
	}
}
