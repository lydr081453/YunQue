using System;
namespace ESP.Media.Entity
{
	public class Search_clickcountInfo
	{
		#region ���캯��
        public Search_clickcountInfo()
		{
			id = 0;//id
			dataid = 0;//����ID
			datatype = 0;//�������� 1 ���� 2ý�� 3�ͻ� 4��Ʒ��
			clickcount = 0;//�����
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
		/// ����ID
		/// </summary>
		private int dataid;
		public int Dataid
		{
			get
			{
				return dataid;
			}
			set
			{
				dataid = value ;
			}
		}


		/// <summary>
		/// �������� 1 ���� 2ý�� 3�ͻ� 4��Ʒ��
		/// </summary>
		private int datatype;
		public int Datatype
		{
			get
			{
				return datatype;
			}
			set
			{
				datatype = value ;
			}
		}


		/// <summary>
		/// �����
		/// </summary>
		private int clickcount;
		public int Clickcount
		{
			get
			{
				return clickcount;
			}
			set
			{
				clickcount = value ;
			}
		}


		#endregion
	}
}
