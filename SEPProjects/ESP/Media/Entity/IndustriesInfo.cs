using System;
namespace ESP.Media.Entity
{
	public class IndustriesInfo
	{
		#region ���캯��
        public IndustriesInfo()
		{
			industryid = 0;//IndustryID
			industryname = string.Empty;//IndustryName
			del = 0;//ɾ�����
		}
		#endregion


		#region ����
		/// <summary>
		/// IndustryID
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
		/// IndustryName
		/// </summary>
		private string industryname;
		public string Industryname
		{
			get
			{
				return industryname;
			}
			set
			{
				industryname = value ;
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
