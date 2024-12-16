using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.HumanResource.Entity
{
    public class PayInsuranceInfo
    {
        public PayInsuranceInfo()
		{}
		#region Model
		private int _id;
		private int _userid;
		private string _endowmentinsurance;
		private string _unemploymentinsurance;
		private string _medicalinsurance;
		private string _publicreservefunds;
		private int _payyear;
		private int _paymonth;
		private string _remark;
		private int _creator;
		private DateTime _createtime = DateTime.Parse("1900-01-01");
		private int _lastupdateman;
		private DateTime _lastupdatetime = DateTime.Parse("1900-01-01");
        private string _fullnamecn;
		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
		set{ _id=value;}
		get{return _id;}
		}
		/// <summary>
		/// 用户ID
		/// </summary>
		public int UserID
		{
		set{ _userid=value;}
		get{return _userid;}
		}
		/// <summary>
		/// 养老保险
		/// </summary>
		public string EndowmentInsurance
		{
		set{ _endowmentinsurance=value;}
		get{return _endowmentinsurance;}
		}
		/// <summary>
		/// 失业保险
		/// </summary>
		public string UnemploymentInsurance
		{
		set{ _unemploymentinsurance=value;}
		get{return _unemploymentinsurance;}
		}
		/// <summary>
		/// 医疗保险
		/// </summary>
		public string MedicalInsurance
		{
		set{ _medicalinsurance=value;}
		get{return _medicalinsurance;}
		}
		/// <summary>
		/// 公积金
		/// </summary>
		public string PublicReserveFunds
		{
		set{ _publicreservefunds=value;}
		get{return _publicreservefunds;}
		}
		/// <summary>
		/// 支付年份
		/// </summary>
		public int PayYear
		{
		set{ _payyear=value;}
		get{return _payyear;}
		}
		/// <summary>
		/// 支付月份
		/// </summary>
		public int PayMonth
		{
		set{ _paymonth=value;}
		get{return _paymonth;}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string Remark
		{
		set{ _remark=value;}
		get{return _remark;}
		}
		/// <summary>
		/// 创建人
		/// </summary>
		public int Creator
		{
		set{ _creator=value;}
		get{return _creator;}
		}
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreateTime
		{
		set{ _createtime=value;}
		get{return _createtime;}
		}
		/// <summary>
		/// 最后修改人
		/// </summary>
		public int LastUpdateMan
		{
		set{ _lastupdateman=value;}
		get{return _lastupdateman;}
		}
		/// <summary>
		/// 最后修改时间
		/// </summary>
		public DateTime LastUpdateTime
		{
		set{ _lastupdatetime=value;}
		get{return _lastupdatetime;}
		}

        /// <summary>
        /// 用户中文姓名
        /// </summary>
        public string FullNameCn
        {
            set { _fullnamecn = value; }
            get { return _fullnamecn; }
        }
		#endregion Model
    }
}
