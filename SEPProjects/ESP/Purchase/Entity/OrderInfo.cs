using System;
using System.Data;
using System.Collections.Generic;

namespace ESP.Purchase.Entity
{
    /// <summary>
    /// 实体类T_OrderInfo 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class OrderInfo
	{
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderInfo"/> class.
        /// </summary>
		public OrderInfo()
		{}

		#region Model
		private int _id;
		private int _general_id;
		private string _item_no;
		private string _desctiprtion;
		private string _intend_receipt_date;
		private decimal _price;
        private string _upfile;
		private string _uom;
		private decimal _quantity;
		private decimal _total;
        private int _supplierId;
        private string _moneytype;
        private int _billid;
        private byte[] ordercontent;
        public decimal FactTotal { get; set; }
      //  private List<PRMediaBLL.Reporter.Media_ReporterList> reporter;
        private int _orderStatus;

        /// <summary>
        /// Gets or sets the reporter list.
        /// </summary>
        /// <value>The reporter list.</value>
        //public List<PRMediaBLL.Reporter.Media_ReporterList> ReporterList
        //{
        //    get
        //    {
        //        if (reporter == null)
        //        {
        //            reporter = new List<PRMediaBLL.Reporter.Media_ReporterList>();
        //        }
        //        return reporter;
        //    }
        //    set { reporter = value; }
        //}

        /// <summary>
        /// Gets or sets the content of the order.
        /// </summary>
        /// <value>The content of the order.</value>
        public byte[] OrderContent
        {
            get { return ordercontent; }
            set { ordercontent = value; }
        }

       // private PRMediaBLL.PRWrittingFee.Media_writingfeebill _writtingFeeBill;
        /// <summary>
        /// Gets or sets the writting fee bill.
        /// </summary>
        /// <value>The writting fee bill.</value>
        //public PRMediaBLL.PRWrittingFee.Media_writingfeebill WrittingFeeBill
        //{
        //    get 
        //    {
        //        if (_writtingFeeBill == null)
        //        {
        //            _writtingFeeBill = new PRMediaBLL.PRWrittingFee.Media_writingfeebill();
        //        }
        //        return _writtingFeeBill; 
        //    }
        //    set { _writtingFeeBill = value; }
        //}

        /// <summary>
        /// Gets or sets the bill ID.
        /// </summary>
        /// <value>The bill ID.</value>
        public int BillID
        {
            get { return _billid; }
            set { _billid = value; }
        }

        private int _billtype;
        /// <summary>
        /// Gets or sets the type of the bill.
        /// </summary>
        /// <value>The type of the bill.</value>
        public int BillType
        {
            get { return _billtype; }
            set { _billtype = value; }
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}

        /// <summary>
        /// 基础信息表ID
        /// </summary>
        /// <value>The general_id.</value>
		public int general_id
		{
			set{ _general_id=value;}
			get{return _general_id;}
		}

        /// <summary>
        /// 项目
        /// </summary>
        /// <value>The item_ no.</value>
		public string Item_No
		{
			set{ _item_no=value;}
			get{return _item_no;}
		}

        /// <summary>
        /// 描述
        /// </summary>
        /// <value>The desctiprtion.</value>
		public string desctiprtion
		{
			set{ _desctiprtion=value;}
			get{return _desctiprtion;}
		}

        /// <summary>
        /// 预计收货时间
        /// </summary>
        /// <value>The intend_receipt_date.</value>
		public string intend_receipt_date
		{
			set{ _intend_receipt_date=value;}
			get{return _intend_receipt_date;}
		}

        /// <summary>
        /// 单价
        /// </summary>
        /// <value>The price.</value>
		public decimal price
		{
			set{ _price=value;}
			get{return _price;}
		}

        /// <summary>
        /// 附件
        /// </summary>
        /// <value>The upfile.</value>
        public string upfile
        {
            get { return _upfile; }
            set { _upfile = value; }
        }
        /// <summary>
        /// 单位
        /// </summary>
        /// <value>The uom.</value>
		public string uom
		{
			set{ _uom=value;}
			get{return _uom;}
		}
        /// <summary>
        /// 数量
        /// </summary>
        /// <value>The quantity.</value>
        public decimal quantity
		{
			set{ _quantity=value;}
			get{return _quantity;}
		}
        /// <summary>
        /// Gets or sets the total.
        /// </summary>
        /// <value>The total.</value>
		public decimal total
		{
			set{ _total=value;}
			get{return _total;}
		}

        private int _producttype;
        /// <summary>
        /// 物料类别
        /// </summary>
        /// <value>The producttype.</value>
        public int producttype
        {
            get { return _producttype; }
            set { _producttype = value; }
        }

        private string _producttypename;
        /// <summary>
        /// 物料类别名称
        /// </summary>
        /// <value>The producttypename.</value>
        public string producttypename
        {
            get { return _producttypename; }
            set { _producttypename = value; }
        }

        private int _auditor;
        /// <summary>
        /// 初审人ID
        /// </summary>
        /// <value>The auditor.</value>
        public int auditor
        {
            get { return _auditor; }
            set { _auditor = value; }
        }

        private int _filiale_auditor_cq;
        /// <summary>
        /// 重庆分公司审核人
        /// </summary>
        /// <value>The filiale_auditor_sh.</value>
        public int filiale_auditor_cq
        {
            get { return _filiale_auditor_cq; }
            set { _filiale_auditor_cq = value; }
        }


        /// <summary>
        /// Gets or sets the supplier id.
        /// </summary>
        /// <value>The supplier id.</value>
        public int supplierId
        {
            get { return _supplierId;}
            set { _supplierId = value;}
        }

        private string _supplierName;
        /// <summary>
        /// 供应商名称
        /// </summary>
        /// <value>The name of the supplier.</value>
        public string supplierName
        {
            get { return _supplierName; }
            set { _supplierName = value; }
        }

        private int _productAttribute;
        /// <summary>
        /// 物料属性
        /// 1：目录物品，2：非目录物品
        /// </summary>
        /// <value>The product attribute.</value>
        public int productAttribute
        {
            get { return _productAttribute; }
            set { _productAttribute = value; }
        }

        /// <summary>
        /// 货币符合
        /// </summary>
        /// <value>The moneytype.</value>
        public string moneytype
        {
            get { return _moneytype; }
            set { _moneytype = value; }
        }

       

        /// <summary>
        /// 采购物品状态.
        /// </summary>
        /// <value>The order status.</value>
        public int OrderStatus
        {
            get { return _orderStatus; }
            set { _orderStatus = value; }
        }

        private decimal _oldPrice;
        /// <summary>
        /// 原始单价
        /// </summary>
        public decimal oldPrice
        {
            get { return _oldPrice; }
            set { _oldPrice = value; }
        }

        private decimal _oldQuantity;
        /// <summary>
        /// 原始数量
        /// </summary>
        public decimal oldQuantity
        {
            get { return _oldQuantity; }
            set { _oldQuantity = value; }
        }

		#endregion Model

        #region method
        /// <summary>
        /// Popups the data.
        /// </summary>
        /// <param name="r">The r.</param>
        public void PopupData(IDataReader r)
        {
            if (null != r["id"] && r["id"].ToString() != "")
            {
                this.id = int.Parse(r["id"].ToString());
            }
            if (null != r["general_id"] && r["general_id"].ToString() != "")
            {
                this.general_id = int.Parse(r["general_id"].ToString());
            }
            Item_No = r["item_no"].ToString();
            desctiprtion = r["desctiprtion"].ToString();
            intend_receipt_date = r["intend_receipt_date"].ToString();
            if (null != r["price"] && r["price"].ToString() != "")
            {
                price = decimal.Parse(r["price"].ToString());
            }
            upfile = r["upfile"].ToString().TrimEnd('#');
            uom = r["uom"].ToString();
            if (null != r["quantity"] && r["quantity"].ToString() != "")
            {
                quantity = decimal.Parse(r["quantity"].ToString());
            }
            if (null != r["total"] && r["total"].ToString() != "")
            {
                total = decimal.Parse(r["total"].ToString());
            }
            if (null != r["FactTotal"] && r["FactTotal"].ToString() != "")
            {
                FactTotal = decimal.Parse(r["FactTotal"].ToString());
            }
            if (null != r["producttype"] && r["producttype"] != DBNull.Value && r["producttype"].ToString() != "")
            {
                _producttype = int.Parse(r["producttype"].ToString());
            }
            _producttypename = r["typename"].ToString();
            if (null != r["auditorid"] && r["auditorid"].ToString() != "")
            {
                _auditor = int.Parse(r["auditorid"].ToString());
            }
            if (null != r["supplierId"] && r["supplierId"].ToString() != "")
            {
                _supplierId = int.Parse(r["supplierId"].ToString());
            }
            if (null != r["productAttribute"] && r["productAttribute"] != DBNull.Value && r["productAttribute"].ToString() != "")
            {
                productAttribute = int.Parse(r["productAttribute"].ToString());
            }
            supplierName = r["supplierName"].ToString();
            if (null != r["shauditorid"] && r["shauditorid"].ToString() != "")
            {
                _filiale_auditor_cq = int.Parse(r["shauditorid"].ToString());
            }

            if (null != r["OrderStatus"] && r["OrderStatus"].ToString() != "")
            {
                _orderStatus = int.Parse(r["OrderStatus"].ToString());
            }
            _oldPrice = r["oldPrice"] == DBNull.Value ? 0m : decimal.Parse(r["oldPrice"].ToString());
            _oldQuantity = r["oldQuantity"] == DBNull.Value ? 0m : decimal.Parse(r["oldQuantity"].ToString());
        }
        #endregion
	}
}