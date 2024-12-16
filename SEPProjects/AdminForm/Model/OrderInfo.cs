using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminForm.Model
{
    [Serializable]
    public class OrderInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderInfo"/> class.
        /// </summary>
        public OrderInfo()
        { }

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

        private int _orderStatus;

        /// <summary>
        /// Gets or sets the content of the order.
        /// </summary>
        /// <value>The content of the order.</value>
        public byte[] OrderContent
        {
            get { return ordercontent; }
            set { ordercontent = value; }
        }

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
            set { _id = value; }
            get { return _id; }
        }

        /// <summary>
        /// 基础信息表ID
        /// </summary>
        /// <value>The general_id.</value>
        public int general_id
        {
            set { _general_id = value; }
            get { return _general_id; }
        }

        /// <summary>
        /// 项目
        /// </summary>
        /// <value>The item_ no.</value>
        public string Item_No
        {
            set { _item_no = value; }
            get { return _item_no; }
        }

        /// <summary>
        /// 描述
        /// </summary>
        /// <value>The desctiprtion.</value>
        public string desctiprtion
        {
            set { _desctiprtion = value; }
            get { return _desctiprtion; }
        }

        /// <summary>
        /// 预计收货时间
        /// </summary>
        /// <value>The intend_receipt_date.</value>
        public string intend_receipt_date
        {
            set { _intend_receipt_date = value; }
            get { return _intend_receipt_date; }
        }

        /// <summary>
        /// 单价
        /// </summary>
        /// <value>The price.</value>
        public decimal price
        {
            set { _price = value; }
            get { return _price; }
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
            set { _uom = value; }
            get { return _uom; }
        }
        /// <summary>
        /// 数量
        /// </summary>
        /// <value>The quantity.</value>
        public decimal quantity
        {
            set { _quantity = value; }
            get { return _quantity; }
        }
        /// <summary>
        /// Gets or sets the total.
        /// </summary>
        /// <value>The total.</value>
        public decimal total
        {
            set { _total = value; }
            get { return _total; }
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

        private int _filiale_auditor_sh;
        /// <summary>
        /// 上海分公司审核人
        /// </summary>
        /// <value>The filiale_auditor_sh.</value>
        public int filiale_auditor_sh
        {
            get { return _filiale_auditor_sh; }
            set { _filiale_auditor_sh = value; }
        }

        private int _filiale_auditor_gz;
        /// <summary>
        /// 广州分公司审核人
        /// </summary>
        /// <value>The filiale_auditor_gz.</value>
        public int filiale_auditor_gz
        {
            get { return _filiale_auditor_gz; }
            set { _filiale_auditor_gz = value; }
        }

        /// <summary>
        /// Gets or sets the supplier id.
        /// </summary>
        /// <value>The supplier id.</value>
        public int supplierId
        {
            get { return _supplierId; }
            set { _supplierId = value; }
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

    }

}
