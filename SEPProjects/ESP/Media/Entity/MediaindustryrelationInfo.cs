using System;
namespace ESP.Media.Entity
{
	public class MediaindustryrelationInfo
	{
		#region ���캯��
        public MediaindustryrelationInfo()
		{
			id = 0;//id
			mediaitemid = 0;//ý��id
			industryid = 0;//��ҵid

			del = 0;//ɾ�����
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
		/// ý��id
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
		/// ��ҵid

		/// </summary>
		private int industryid;
		public int Industryid
		{
			get
			{
				return industryid;
			}
			set
			{
				industryid = value ;
			}
		}


		/// <summary>
		/// ɾ�����
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
