using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.HumanResource.Entity
{
    public class InsuranceBaseInfo
    {
        public InsuranceBaseInfo()
        { }

        private int _id;
        private string _cityname;
        private string _yanglao_c;
        private string _yanglao_c_fbd;
        private string _yanglao_p;
        private string _shiye_c;
        private string _shiye_p;
        private string _gongshang_c;
        private string _gongshang_p;
        private string _shengyu_c;
        private string _shengyu_p;
        private string _yiliao_c;
        private string _yiliao_p;
        private string _zhufang_c;
        private string _zhufang_p;
        private string _zhongda_c;
        private string _zhongda_p;
        private string _heji_c;
        private string _heji_p;
        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CityName
        {
            set { _cityname = value; }
            get { return _cityname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string YangLao_C
        {
            set { _yanglao_c = value; }
            get { return _yanglao_c; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string YangLao_C_FBD
        {
            set { _yanglao_c_fbd = value; }
            get { return _yanglao_c_fbd; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string YangLao_P
        {
            set { _yanglao_p = value; }
            get { return _yanglao_p; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ShiYe_C
        {
            set { _shiye_c = value; }
            get { return _shiye_c; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ShiYe_P
        {
            set { _shiye_p = value; }
            get { return _shiye_p; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string GongShang_C
        {
            set { _gongshang_c = value; }
            get { return _gongshang_c; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string GongShang_P
        {
            set { _gongshang_p = value; }
            get { return _gongshang_p; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ShengYu_C
        {
            set { _shengyu_c = value; }
            get { return _shengyu_c; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ShengYu_P
        {
            set { _shengyu_p = value; }
            get { return _shengyu_p; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string YiLiao_C
        {
            set { _yiliao_c = value; }
            get { return _yiliao_c; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string YiLiao_P
        {
            set { _yiliao_p = value; }
            get { return _yiliao_p; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ZhuFang_C
        {
            set { _zhufang_c = value; }
            get { return _zhufang_c; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ZhuFang_P
        {
            set { _zhufang_p = value; }
            get { return _zhufang_p; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ZhongDa_C
        {
            set { _zhongda_c = value; }
            get { return _zhongda_c; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ZhongDa_P
        {
            set { _zhongda_p = value; }
            get { return _zhongda_p; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string HeJi_C
        {
            set { _heji_c = value; }
            get { return _heji_c; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string HeJi_P
        {
            set { _heji_p = value; }
            get { return _heji_p; }
        }
    }


    /// <summary>
    /// 社保缴费上下线信息
    /// </summary>
    public class InsuranceBase
    {
        public int Id { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public string InsuranceType { get; set; }
        public string CityName { get; set; }
        public decimal MaxPrice { get; set; }
        /// <summary>
        /// 无下线，该值为0
        /// </summary>
        public decimal MinPrice { get; set; }
    }
}
