using System;
namespace ESP.Media.Entity
{
	public class MediatypeInfo
	{
		#region ���캯��
        public MediatypeInfo()
		{
			id = 0;//id
			name = string.Empty;//����
			des = string.Empty;//����

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
		/// ����
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
		/// ����

		/// </summary>
		private string des;
		public string Des
		{
			get
			{
				return des;
			}
			set
			{
				des = value ;
			}
		}


		#endregion
	}
}
