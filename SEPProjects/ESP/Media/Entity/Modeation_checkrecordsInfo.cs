using System;
namespace ESP.Media.Entity
{
	public class Modeation_checkrecordsInfo
	{
		#region 构造函数
        public Modeation_checkrecordsInfo()
		{
			mediaitemid = 0;//MediaItemID
			userid = 0;//UserID
			username = string.Empty;//UserName
			checkaction = 0;//CheckAction
			checkdate = string.Empty;//CheckDate
		}
		#endregion


		#region 属性
		/// <summary>
		/// MediaItemID
		/// </summary>
		private int mediaitemid;
		public int Mediaitemid
		{
			get
			{
				return mediaitemid;
			}
			set
			{
				mediaitemid = value ;
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
		/// UserName
		/// </summary>
		private string username;
		public string Username
		{
			get
			{
				return username;
			}
			set
			{
				username = value ;
			}
		}


		/// <summary>
		/// CheckAction
		/// </summary>
		private int checkaction;
		public int Checkaction
		{
			get
			{
				return checkaction;
			}
			set
			{
				checkaction = value ;
			}
		}


		/// <summary>
		/// CheckDate
		/// </summary>
		private string checkdate;
		public string Checkdate
		{
			get
			{
				return checkdate;
			}
			set
			{
				checkdate = value ;
			}
		}


		#endregion
	}
}
