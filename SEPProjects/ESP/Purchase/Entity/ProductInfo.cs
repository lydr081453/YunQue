using System;
using System.Data;

namespace ESP.Purchase.Entity
{
    /// <summary>
    /// 实体类T_Product 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class ProductInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T_Product"/> class.
        /// </summary>
        public ProductInfo()
        { }

        #region Model
        private int _id;
        private int _producttype;
        private string _productname;
        private string _productdes;
        private string _productunit;
        private int _supplierId;
        private int _isShow;

        /// <summary>
        /// 自增序号
        /// </summary>
        /// <value>The id.</value>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }

        /// <summary>
        /// 物品类型
        /// </summary>
        /// <value>The type of the product.</value>
        public int productType
        {
            set { _producttype = value; }
            get { return _producttype; }
        }
        /// <summary>
        /// 物品名称
        /// </summary>
        /// <value>The name of the product.</value>
        public string productName
        {
            set { _productname = value; }
            get { return _productname; }
        }

        /// <summary>
        /// 物品描述
        /// </summary>
        /// <value>The product DES.</value>
        public string productDes
        {
            set { _productdes = value; }
            get { return _productdes; }
        }

        /// <summary>
        /// 物品单位
        /// </summary>
        /// <value>The product unit.</value>
        public string productUnit
        {
            set { _productunit = value; }
            get { return _productunit; }
        }

        /// <summary>
        /// 供应商ID
        /// </summary>
        /// <value>The supplier id.</value>
        public int supplierId
        {
            get { return _supplierId; }
            set { _supplierId = value; }
        }

        private string _supplierName;
        /// <summary>
        /// Gets or sets the name of the supplier.
        /// </summary>
        /// <value>The name of the supplier.</value>
        public string supplierName
        {
            get { return _supplierName; }
            set { _supplierName = value; }
        }

        private string _typename;
        /// <summary>
        /// Gets or sets the typename.
        /// </summary>
        /// <value>The typename.</value>
        public string typename
        {
            get { return _typename; }
            set { _typename = value; }
        }

        private string _productClass;
        /// <summary>
        /// Gets or sets the product class.
        /// </summary>
        /// <value>The product class.</value>
        public string productClass
        {
            get { return _productClass; }
            set { _productClass = value; }
        }

        private decimal _productprice;
        /// <summary>
        /// Gets or sets the product price.
        /// </summary>
        /// <value>The product price.</value>
        public decimal ProductPrice
        {
            get { return _productprice; }
            set { _productprice = value; }
        }

        /// <summary>
        /// Gets or sets the is show.
        /// </summary>
        /// <value>The is show.</value>
        public int IsShow
        {
            get { return _isShow; }
            set { _isShow = value; }
        }
        #endregion Model

        /// <summary>
        /// Popups the data.
        /// </summary>
        /// <param name="r">The r.</param>
        public void PopupData(IDataReader r)
        {
            _id = int.Parse(r["id"].ToString());
            if (r["producttype"] != DBNull.Value && r["producttype"] != "")
            {
                _producttype = int.Parse(r["producttype"].ToString());
            }
            _productname = r["productname"].ToString();
            _productdes = r["productdes"].ToString();
            _productunit = r["productunit"].ToString();
            if (r["supplierId"] != DBNull.Value && r["supplierId"] != "")
            {
                _supplierId = int.Parse(r["supplierId"].ToString());
            }
            _typename = r["typename"].ToString();
            _supplierName = r["suppliername"].ToString();
            _productClass = r["productClass"].ToString();
            if (r["productprice"] != DBNull.Value && r["productprice"] != "")
            {
                _productprice = decimal.Parse(r["productprice"].ToString());
            }
            if (r["IsShow"] != DBNull.Value && r["IsShow"] != "")
            {
                _isShow = int.Parse(r["IsShow"].ToString());
            }
        }
    }
}