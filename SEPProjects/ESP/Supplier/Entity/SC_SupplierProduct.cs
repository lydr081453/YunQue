using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Supplier.Entity
{
    public class SC_SupplierProduct
    {
    
		public SC_SupplierProduct()
		{}
		#region Model
		private int _id;
		private int? _version;
		private int? _supplierid;
		private string _sn;
		private string _name;
		private DateTime? _usedbegintime;
		private DateTime? _usedendtime;
		private int? _paydays;
		private int? _receivetype;
		private string _receiver;
		private string _description;
		private string _unit;
		private string _class;
		private decimal? _price;
		private int? _producttypeid;
		private string _producttypename;
		private string _productcontentsheet;
		private string _createdip;
		private DateTime? _creattime;
		private string _lastmodifiedip;
		private DateTime? _lastupdatetime;
		private int? _type;
		private int? _status;
		private string _productpic;
		private int? _productbatchid;
		/// <summary>
		/// 联系人流水号
		/// </summary>
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Version
		{
			set{ _version=value;}
			get{return _version;}
		}
		/// <summary>
		/// 所属公司ID
		/// </summary>
		public int? SupplierId
		{
			set{ _supplierid=value;}
			get{return _supplierid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SN
		{
			set{ _sn=value;}
			get{return _sn;}
		}
		/// <summary>
		/// 产品名称
		/// </summary>
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? UsedBeginTime
		{
			set{ _usedbegintime=value;}
			get{return _usedbegintime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? UsedEndTime
		{
			set{ _usedendtime=value;}
			get{return _usedendtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? PayDays
		{
			set{ _paydays=value;}
			get{return _paydays;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ReceiveType
		{
			set{ _receivetype=value;}
			get{return _receivetype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Receiver
		{
			set{ _receiver=value;}
			get{return _receiver;}
		}
		/// <summary>
		/// 产品描述
		/// </summary>
		public string Description
		{
			set{ _description=value;}
			get{return _description;}
		}
		/// <summary>
		/// 产品单位
		/// </summary>
		public string Unit
		{
			set{ _unit=value;}
			get{return _unit;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Class
		{
			set{ _class=value;}
			get{return _class;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? Price
		{
			set{ _price=value;}
			get{return _price;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ProductTypeid
		{
			set{ _producttypeid=value;}
			get{return _producttypeid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ProductTypeName
		{
			set{ _producttypename=value;}
			get{return _producttypename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ProductContentSheet
		{
			set{ _productcontentsheet=value;}
			get{return _productcontentsheet;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CreatedIP
		{
			set{ _createdip=value;}
			get{return _createdip;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreatTime
		{
			set{ _creattime=value;}
			get{return _creattime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string LastModifiedIP
		{
			set{ _lastmodifiedip=value;}
			get{return _lastmodifiedip;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? LastUpdateTime
		{
			set{ _lastupdatetime=value;}
			get{return _lastupdatetime;}
		}
		/// <summary>
		/// 联系人类型
		/// </summary>
		public int? Type
		{
			set{ _type=value;}
			get{return _type;}
		}
		/// <summary>
		/// 联系人状态
		/// </summary>
		public int? Status
		{
			set{ _status=value;}
			get{return _status;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string productPic
		{
			set{ _productpic=value;}
			get{return _productpic;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ProductBatchId
		{
			set{ _productbatchid=value;}
			get{return _productbatchid;}
		}
		#endregion Model

    }
}
