using System;
namespace ESP.Media.Entity
{
	public class ProjectwritingfeebillrelationInfo
	{
		#region ���캯��
        public ProjectwritingfeebillrelationInfo()
		{
			id = 0;//id
			writingfeebillid = 0;//WritingFeeBillID
			projectid = 0;//projectid
			del = 0;//del
		}
		#endregion


		#region ����
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
		/// WritingFeeBillID
		/// </summary>
		private int writingfeebillid;
		public int Writingfeebillid
		{
			get
			{
				return writingfeebillid;
			}
			set
			{
				writingfeebillid = value ;
			}
		}


		/// <summary>
		/// projectid
		/// </summary>
		private int projectid;
		public int Projectid
		{
			get
			{
				return projectid;
			}
			set
			{
				projectid = value ;
			}
		}


		/// <summary>
		/// del
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
