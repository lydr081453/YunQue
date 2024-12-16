using System;
namespace ESP.Media.Entity
{
	public class FacetofaceInfo
	{
		#region 构造函数
        public FacetofaceInfo()
		{
			id = 0;//id
			cycle = 0;//cycle
			createuserid = 0;//createuserid
			createdate = string.Empty;//createdate
			path = string.Empty;//path
			subject = string.Empty;//subject
			remark = string.Empty;//Remark
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
		/// cycle
		/// </summary>
		private int cycle;
		public int Cycle
		{
			get
			{
				return cycle;
			}
			set
			{
				cycle = value ;
			}
		}


		/// <summary>
		/// createuserid
		/// </summary>
		private int createuserid;
		public int Createuserid
		{
			get
			{
				return createuserid;
			}
			set
			{
				createuserid = value ;
			}
		}


		/// <summary>
		/// createdate
		/// </summary>
		private string createdate;
		public string Createdate
		{
			get
			{
				return createdate;
			}
			set
			{
				createdate = value ;
			}
		}


		/// <summary>
		/// path
		/// </summary>
		private string path;
		public string Path
		{
			get
			{
				return path;
			}
			set
			{
				path = value ;
			}
		}


		/// <summary>
		/// subject
		/// </summary>
		private string subject;
		public string Subject
		{
			get
			{
				return subject;
			}
			set
			{
				subject = value ;
			}
		}


		/// <summary>
		/// Remark
		/// </summary>
		private string remark;
		public string Remark
		{
			get
			{
				return remark;
			}
			set
			{
				remark = value ;
			}
		}


		#endregion
	}
}
