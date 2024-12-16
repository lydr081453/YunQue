using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.HumanResource.Entity
{
    public class SocialSecurityModelInfo
    {
        private string _endowmentinsurance;
        private string _unemploymentinsurance;
        private string _medicalinsurance;
        private string _publicreservefunds;
        private string _eifirmscosts;
        private string _eiindividualscosts;
        private string _uifirmscosts;
        private string _uiindividualscosts;
        private string _bifirmscosts;
        private string _biindividualscosts;
        private string _cifirmscosts;
        private string _ciindividualscosts;
        private string _mifirmscosts;
        private string _miindividualscosts;
        private string _prfirmscosts;
        private string _priindividualscosts;
        private string _remarks;
        /// <summary>
        /// 养老保险
        /// </summary>
        public string EndowmentInsurance
        {
            set { _endowmentinsurance = value; }
            get { return _endowmentinsurance; }
        }
        /// <summary>
        /// 失业保险
        /// </summary>
        public string UnemploymentInsurance
        {
            set { _unemploymentinsurance = value; }
            get { return _unemploymentinsurance; }
        }
        /// <summary>
        /// 医疗保险
        /// </summary>
        public string MedicalInsurance
        {
            set { _medicalinsurance = value; }
            get { return _medicalinsurance; }
        }
        /// <summary>
        /// 公积金
        /// </summary>
        public string PublicReserveFunds
        {
            set { _publicreservefunds = value; }
            get { return _publicreservefunds; }
        }
        /// <summary>
        /// 养老保险公司应缴费用
        /// </summary>
        public string EIFirmsCosts
        {
            set { _eifirmscosts = value; }
            get { return _eifirmscosts; }
        }
        /// <summary>
        /// 养老保险个人应缴费用
        /// </summary>
        public string EIIndividualsCosts
        {
            set { _eiindividualscosts = value; }
            get { return _eiindividualscosts; }
        }
        /// <summary>
        /// 失业保险公司应缴费用
        /// </summary>
        public string UIFirmsCosts
        {
            set { _uifirmscosts = value; }
            get { return _uifirmscosts; }
        }
        /// <summary>
        /// 失业保险个人应缴费用
        /// </summary>
        public string UIIndividualsCosts
        {
            set { _uiindividualscosts = value; }
            get { return _uiindividualscosts; }
        }
        /// <summary>
        /// 生育保险公司应缴费用
        /// </summary>
        public string BIFirmsCosts
        {
            set { _bifirmscosts = value; }
            get { return _bifirmscosts; }
        }
        /// <summary>
        /// 生育保险个人应缴费用
        /// </summary>
        public string BIIndividualsCosts
        {
            set { _biindividualscosts = value; }
            get { return _biindividualscosts; }
        }
        /// <summary>
        /// 工伤险公司应缴费用
        /// </summary>
        public string CIFirmsCosts
        {
            set { _cifirmscosts = value; }
            get { return _cifirmscosts; }
        }
        /// <summary>
        /// 工伤险个人应缴费用
        /// </summary>
        public string CIIndividualsCosts
        {
            set { _ciindividualscosts = value; }
            get { return _ciindividualscosts; }
        }
        /// <summary>
        /// 医疗保险公司应缴费用
        /// </summary>
        public string MIFirmsCosts
        {
            set { _mifirmscosts = value; }
            get { return _mifirmscosts; }
        }
        /// <summary>
        /// 医疗保险个人应缴费用
        /// </summary>
        public string MIIndividualsCosts
        {
            set { _miindividualscosts = value; }
            get { return _miindividualscosts; }
        }
        /// <summary>
        /// 公积金公司应缴费用
        /// </summary>
        public string PRFirmsCosts
        {
            set { _prfirmscosts = value; }
            get { return _prfirmscosts; }
        }
        /// <summary>
        /// 公积金个人应缴费用
        /// </summary>
        public string PRIIndividualsCosts
        {
            set { _priindividualscosts = value; }
            get { return _priindividualscosts; }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks
        {
            set { _remarks = value; }
            get { return _remarks; }
        }
    }
}
