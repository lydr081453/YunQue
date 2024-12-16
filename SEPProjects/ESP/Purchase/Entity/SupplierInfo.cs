using System;
using System.Data;
using ESP.Purchase.BusinessLogic;

namespace ESP.Purchase.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class SupplierInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SupplierInfo"/> class.
        /// </summary>
        public SupplierInfo()
        { }

        #region Model
        private int _id;
        private string _supplier_name;
        private string _supplier_area;
        private string _supplier_industry;
        private string _supplier_scale;
        private string _supplier_principal;
        private string _supplier_builttime;
        private string _supplier_website;
        private string _supplier_source;
        private string _supplier_frameno;
        private string _contact_name;
        private string _contact_tel;
        private string _contact_mobile;
        private string _contact_email;
        private string _contact_fax;
        private string _contact_address;
        private string _service_content;
        private string _service_area;
        private string _service_workamount;
        private string _service_customization;
        private string _service_ohter;
        private string _business_price;
        private string _business_paytime;
        private string _business_prepay;
        private string _evaluation_department;
        private string _evaluation_level;
        private string _evaluation_feedback;
        private string _evaluation_note;
        private string _account_name;
        private string _account_bank;
        private string _account_number;
        private string _service_forshunya;
        private decimal? _payment_tax;
        private int? _general_id;
        /// <summary>
        /// 对应的PR单ID
        /// </summary>
        public int? general_id
        {
            get { return _general_id; }
            set { _general_id = value; }
        }
        public decimal? Payment_Tax
        {
            get { return _payment_tax; }
            set { _payment_tax = value; }
        }
        private int _supplier_type;
        /// <summary>
        /// 供应商类型 1协议供应商 2推荐供应商
        /// </summary>
        /// <value>The supplier_type.</value>
        public int supplier_type
        {
            get { return _supplier_type; }
            set { _supplier_type = value; }
        }

        private int _supplier_status;
        /// <summary>
        /// 供应商状态 0为停用,1为可用
        /// </summary>
        /// <value>The supplier_type.</value>
        public int supplier_status
        {
            get { return _supplier_status; }
            set { _supplier_status = value; }
        }
        /// <summary>
        /// 流水号
        /// </summary>
        /// <value>The id.</value>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }

        /// <summary>
        /// 基础信息-供应商全称
        /// </summary>
        /// <value>The supplier_name.</value>
        public string supplier_name
        {
            set { _supplier_name = value; }
            get { return _supplier_name; }
        }

        /// <summary>
        /// 基础信息-所在地区
        /// </summary>
        /// <value>The supplier_area.</value>
        public string supplier_area
        {
            set { _supplier_area = value; }
            get { return _supplier_area; }
        }

        /// <summary>
        /// 基础信息-行业
        /// </summary>
        /// <value>The supplier_industry.</value>
        public string supplier_industry
        {
            set { _supplier_industry = value; }
            get { return _supplier_industry; }
        }

        /// <summary>
        /// 基础信息-规模
        /// </summary>
        /// <value>The supplier_scale.</value>
        public string supplier_scale
        {
            set { _supplier_scale = value; }
            get { return _supplier_scale; }
        }

        /// <summary>
        /// 基础信息-注册资本
        /// </summary>
        /// <value>The supplier_principal.</value>
        public string supplier_principal
        {
            set { _supplier_principal = value; }
            get { return _supplier_principal; }
        }

        /// <summary>
        /// 基础信息-成立年限
        /// </summary>
        /// <value>The supplier_builttime.</value>
        public string supplier_builttime
        {
            set { _supplier_builttime = value; }
            get { return _supplier_builttime; }
        }

        /// <summary>
        /// 基础信息-网址
        /// </summary>
        /// <value>The supplier_website.</value>
        public string supplier_website
        {
            set { _supplier_website = value; }
            get { return _supplier_website; }
        }

        /// <summary>
        /// 基础信息-供应商来源
        /// </summary>
        /// <value>The supplier_source.</value>
        public string supplier_source
        {
            set { _supplier_source = value; }
            get { return _supplier_source; }
        }

        /// <summary>
        /// 基础信息-协议框架号
        /// </summary>
        /// <value>The supplier_frame NO.</value>
        public string supplier_frameNO
        {
            set { _supplier_frameno = value; }
            get { return _supplier_frameno; }
        }

        /// <summary>
        /// 联系信息-联系人
        /// </summary>
        /// <value>The contact_name.</value>
        public string contact_name
        {
            set { _contact_name = value; }
            get { return _contact_name; }
        }

        /// <summary>
        /// 联系信息-固话
        /// </summary>
        /// <value>The contact_tel.</value>
        public string contact_tel
        {
            set { _contact_tel = value; }
            get { return _contact_tel; }
        }

        /// <summary>
        /// 联系信息-移动电话
        /// </summary>
        /// <value>The contact_mobile.</value>
        public string contact_mobile
        {
            set { _contact_mobile = value; }
            get { return _contact_mobile; }
        }

        /// <summary>
        /// 联系信息-Email
        /// </summary>
        /// <value>The contact_email.</value>
        public string contact_email
        {
            set { _contact_email = value; }
            get { return _contact_email; }
        }

        /// <summary>
        /// 联系信息-传真
        /// </summary>
        /// <value>The contact_fax.</value>
        public string contact_fax
        {
            set { _contact_fax = value; }
            get { return _contact_fax; }
        }

        /// <summary>
        /// 联系信息-地址
        /// </summary>
        /// <value>The contact_address.</value>
        public string contact_address
        {
            set { _contact_address = value; }
            get { return _contact_address; }
        }

        /// <summary>
        /// 产品服务信息-主要服务内容
        /// </summary>
        /// <value>The service_content.</value>
        public string service_content
        {
            set { _service_content = value; }
            get { return _service_content; }
        }

        /// <summary>
        /// 产品服务信息-服务覆盖区域
        /// </summary>
        /// <value>The service_area.</value>
        public string service_area
        {
            set { _service_area = value; }
            get { return _service_area; }
        }

        /// <summary>
        /// 产品服务信息-可同时承接的工作量
        /// </summary>
        /// <value>The service_workamount.</value>
        public string service_workamount
        {
            set { _service_workamount = value; }
            get { return _service_workamount; }
        }

        /// <summary>
        /// 产品服务信息-定制服务
        /// </summary>
        /// <value>The service_customization.</value>
        public string service_customization
        {
            set { _service_customization = value; }
            get { return _service_customization; }
        }

        /// <summary>
        /// 产品服务信息-其他
        /// </summary>
        /// <value>The service_ohter.</value>
        public string service_ohter
        {
            set { _service_ohter = value; }
            get { return _service_ohter; }
        }

        /// <summary>
        /// 商务条款-参考报价（附件形式）
        /// </summary>
        /// <value>The business_price.</value>
        public string business_price
        {
            set { _business_price = value; }
            get { return _business_price; }
        }

        /// <summary>
        /// 商务条款-账期
        /// </summary>
        /// <value>The business_paytime.</value>
        public string business_paytime
        {
            set { _business_paytime = value; }
            get { return _business_paytime; }
        }

        /// <summary>
        /// 商务条款-预付款
        /// </summary>
        /// <value>The business_prepay.</value>
        public string business_prepay
        {
            set { _business_prepay = value; }
            get { return _business_prepay; }
        }

        /// <summary>
        /// 评估信息-采购部评价
        /// </summary>
        /// <value>The evaluation_department.</value>
        public string evaluation_department
        {
            set { _evaluation_department = value; }
            get { return _evaluation_department; }
        }

        /// <summary>
        /// 评估信息-推荐等级
        /// </summary>
        /// <value>The evaluation_level.</value>
        public string evaluation_level
        {
            set { _evaluation_level = value; }
            get { return _evaluation_level; }
        }

        /// <summary>
        /// 评估信息-业务反馈
        /// </summary>
        /// <value>The evaluation_feedback.</value>
        public string evaluation_feedback
        {
            set { _evaluation_feedback = value; }
            get { return _evaluation_feedback; }
        }

        /// <summary>
        /// 评估信息-备注
        /// </summary>
        /// <value>The evaluation_note.</value>
        public string evaluation_note
        {
            set { _evaluation_note = value; }
            get { return _evaluation_note; }
        }

        /// <summary>
        /// 帐户信息-开户公司名称
        /// </summary>
        /// <value>The account_name.</value>
        public string account_name
        {
            set { _account_name = value; }
            get { return _account_name; }
        }

        /// <summary>
        /// 帐户信息-开户银行
        /// </summary>
        /// <value>The account_bank.</value>
        public string account_bank
        {
            set { _account_bank = value; }
            get { return _account_bank; }
        }

        /// <summary>
        /// 帐户信息-帐号
        /// </summary>
        /// <value>The account_number.</value>
        public string account_number
        {
            set { _account_number = value; }
            get { return _account_number; }
        }

        /// <summary>
        /// 产品服务信息_服务案例
        /// </summary>
        /// <value>The service_forshunya.</value>
        public string service_forshunya
        {
            set { _service_forshunya = value; }
            get { return _service_forshunya; }
        }

        private string _productTypes;
        /// <summary>
        /// 物料类别
        /// </summary>
        /// <value>The product types.</value>
        public string productTypes
        {
            get { return _productTypes; }
            set { _productTypes = value; }
        }

        /// <summary>
        /// 预估媒体成本比例
        /// </summary>
        public decimal? CostRate { get; set; }
        #endregion Model

        /// <summary>
        /// Popups the data.
        /// </summary>
        /// <param name="r">The r.</param>
        public void PopupData(IDataReader r)
        {
            if (r["id"] != DBNull.Value && r["id"].ToString() != "")
            {
                _id = int.Parse(r["id"].ToString());
            }
            _supplier_name = r["supplier_name"].ToString();
            _supplier_area = r["supplier_area"].ToString();
            _supplier_industry = r["supplier_industry"].ToString();
            _supplier_scale = r["supplier_scale"].ToString();
            _supplier_principal = r["supplier_principal"].ToString();
            _supplier_builttime = r["supplier_builttime"].ToString();
            _supplier_website = r["supplier_website"].ToString();
            _supplier_source = r["supplier_source"].ToString();
            _supplier_frameno = r["supplier_frameNO"].ToString();
            _contact_name = r["contact_name"].ToString();
            _contact_tel = r["contact_tel"].ToString();
            _contact_mobile = r["contact_mobile"].ToString();
            _contact_email = r["contact_email"].ToString();
            _contact_fax = r["contact_fax"].ToString();
            _contact_address = r["contact_address"].ToString();
            _service_content = r["service_content"].ToString();
            _service_area = r["service_area"].ToString();
            _service_workamount = r["service_workamount"].ToString();
            _service_customization = r["service_customization"].ToString();
            _service_ohter = r["service_ohter"].ToString();
            _business_price = r["business_price"].ToString();
            _business_paytime = r["business_paytime"].ToString();
            _business_prepay = r["business_prepay"].ToString();
            _evaluation_department = r["evaluation_department"].ToString();
            _evaluation_level = r["evaluation_level"].ToString();
            _evaluation_feedback = r["evaluation_feedback"].ToString();
            _evaluation_note = r["evaluation_note"].ToString();
            _account_name = r["account_name"].ToString();
            _account_bank = r["account_bank"].ToString();
            _account_number = r["account_number"].ToString();
            _service_forshunya = r["service_forshunya"].ToString();
            _productTypes = ProductManager.GetTypeNameBySupplierId(int.Parse(r["id"].ToString()));
            _supplier_type = r["supplier_type"] == DBNull.Value ? 1 : int.Parse(r["supplier_type"].ToString());
            _supplier_status = r["supplier_status"] == DBNull.Value ? 1 : int.Parse(r["supplier_status"].ToString());
            _payment_tax = r["payment_tax"] == DBNull.Value ? 0 : decimal.Parse(r["payment_tax"].ToString());
            CostRate = r["CostRate"] == DBNull.Value ? 0 : decimal.Parse(r["CostRate"].ToString());
        }
    }
}