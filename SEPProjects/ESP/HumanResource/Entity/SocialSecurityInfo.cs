using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Framework.Entity;
using System.Data;

namespace ESP.HumanResource.Entity
{
    public class SocialSecurityInfo
    {
        public SocialSecurityInfo()
		{}
		#region Model
		private int _id;
		private decimal _eiproportionoffirms;
		private decimal _eiproportionofindividuals;
		private decimal _uiproportionoffirms;
		private decimal _uiproportionofindividuals;
		private decimal _biproportionoffirms;
		private decimal _biproportionofindividuals;
		private decimal _ciproportionoffirms;
		private decimal _ciproportionofindividuals;
		private decimal _miproportionoffirms;
		private decimal _miproportionofindividuals;
		private decimal _mibigproportionofindividuals;
		private decimal _prfproportionoffirms;
		private decimal _prfproportionofindividuals;
		private DateTime _begintime = DateTime.Parse("1900-01-01");
		private DateTime _endtime = DateTime.Parse("1900-01-01");
		private int _socialinsurancecompany;
        private string _companyname;
		private int _creator;
		private DateTime _createtime;
		private int _lastupdateman;
		private DateTime _lastupdatetime;
		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
		set{ _id=value;}
		get{return _id;}
		}
		/// <summary>
		/// 养老保险公司比例
		/// </summary>
		public decimal EIProportionOfFirms
		{
		set{ _eiproportionoffirms=value;}
		get{return _eiproportionoffirms;}
		}
		/// <summary>
		/// 养老保险个人比例
		/// </summary>
		public decimal EIProportionOfIndividuals
		{
		set{ _eiproportionofindividuals=value;}
		get{return _eiproportionofindividuals;}
		}
		/// <summary>
		/// 失业保险公司比例
		/// </summary>
		public decimal UIProportionOfFirms
		{
		set{ _uiproportionoffirms=value;}
		get{return _uiproportionoffirms;}
		}
		/// <summary>
		/// 失业保险个人比例
		/// </summary>
		public decimal UIProportionOfIndividuals
		{
		set{ _uiproportionofindividuals=value;}
		get{return _uiproportionofindividuals;}
		}
		/// <summary>
		/// 生育保险公司比例
		/// </summary>
		public decimal BIProportionOfFirms
		{
		set{ _biproportionoffirms=value;}
		get{return _biproportionoffirms;}
		}
		/// <summary>
		/// 生育保险个人比例
		/// </summary>
		public decimal BIProportionOfIndividuals
		{
		set{ _biproportionofindividuals=value;}
		get{return _biproportionofindividuals;}
		}
		/// <summary>
		/// 工伤险公司比例
		/// </summary>
		public decimal CIProportionOfFirms
		{
		set{ _ciproportionoffirms=value;}
		get{return _ciproportionoffirms;}
		}
		/// <summary>
		/// 工伤险个人比例
		/// </summary>
		public decimal CIProportionOfIndividuals
		{
		set{ _ciproportionofindividuals=value;}
		get{return _ciproportionofindividuals;}
		}
		/// <summary>
		/// 医疗保险公司比例
		/// </summary>
		public decimal MIProportionOfFirms
		{
		set{ _miproportionoffirms=value;}
		get{return _miproportionoffirms;}
		}
		/// <summary>
		/// 医疗保险个人比例
		/// </summary>
		public decimal MIProportionOfIndividuals
		{
		set{ _miproportionofindividuals=value;}
		get{return _miproportionofindividuals;}
		}
		/// <summary>
		/// 医疗保险大额医疗个人支付额
		/// </summary>
		public decimal MIBigProportionOfIndividuals
		{
		set{ _mibigproportionofindividuals=value;}
		get{return _mibigproportionofindividuals;}
		}
		/// <summary>
		/// 公积金公司比例
		/// </summary>
		public decimal PRFProportionOfFirms
		{
		set{ _prfproportionoffirms=value;}
		get{return _prfproportionoffirms;}
		}
		/// <summary>
		/// 公积金个人比例
		/// </summary>
		public decimal PRFProportionOfIndividuals
		{
		set{ _prfproportionofindividuals=value;}
		get{return _prfproportionofindividuals;}
		}
		/// <summary>
		/// 社保生效时间
		/// </summary>
		public DateTime BeginTime
		{
		set{ _begintime=value;}
		get{return _begintime;}
		}
		/// <summary>
		/// 社保失效时间
		/// </summary>
		public DateTime EndTime
		{
		set{ _endtime=value;}
		get{return _endtime;}
		}
		/// <summary>
		/// 社保挂靠公司，对应公司ID
		/// </summary>
		public int SocialInsuranceCompany
		{
		set{ _socialinsurancecompany=value;}
		get{return _socialinsurancecompany;}
		}
        /// <summary>
        /// 社保挂靠公司名
        /// </summary>
        public string CompanyName
        {
            set { _companyname = value; }
            get { return _companyname; }
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
		public DateTime lastUpdateTime
		{
		set{ _lastupdatetime=value;}
		get{return _lastupdatetime;}
		}
		#endregion Model
    }
}
