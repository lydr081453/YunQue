using System;
namespace ESP.Media.Entity
{
	public class TrustedusersInfo
	{
		#region ���캯��
        public TrustedusersInfo()
		{
			userid = 0;//UserID
		}
		#endregion


		#region ����
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
