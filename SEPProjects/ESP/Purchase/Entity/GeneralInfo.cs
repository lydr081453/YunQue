using System;
using System.Data;
using System.Collections.Generic;

namespace ESP.Purchase.Entity
{
    /// <summary>
    /// 实体类T_GeneralInfo 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class GeneralInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GeneralInfo"/> class.
        /// </summary>
        public GeneralInfo()
        { }

        #region Model
        private int _id;
        private string _prNo;
        private int _requestor;
        private string _requestorname;
        private DateTime _app_date;
        private string _requestor_info;
        private string _requestor_group;
        private int _enduser;
        private string _endusername;
        private string _enduser_info;
        private string _enduser_group;
        private int _goods_receiver;
        private string _receivername;
        private string _receiver_info;
        private string _ship_address;
        private string _project_code;
        private int _project_id;
        private string _project_descripttion;
        private decimal _buggeted;
        private string _moneytype;
        private string _supplier_name;
        private string _supplier_address;
        private string _supplier_linkman;
        private string _supplier_cellphone;
        private string _supplier_phone;
        private string _supplier_fax;
        private string _supplier_email;
        private string _source;
        private string _fa_no;
        private string _sow;
        private string _sow2;
        private string _sow3;
        private string _sow4;
        private string _payment_terms;
        private string _orderid;
        private string _type;
        private string _contrast;
        private string _consult;
        private int _first_assessor;
        private string _first_assessorname;
        private string _afterwardsname;
        private int _status;
        private string _others;
        private int _prtype;
        private string _emBuy;
        private string _cusAsk;
        private string _cusName;
        private string _contractNo;
        private string _receivergroup;
        private int workitemid;
        private int _operationType;
        private decimal _foregift;
        private int _appendReceiver;
        private string _appendReceiverName;
        private string _appendReceiverInfo;
        private string _appendReceiverGroup;
        private string _NewMediaOrderIDs;
        private int _ValueLevel = 0;

        /// <summary>
        /// 发票类型
        /// </summary>
        public string InvoiceType { get; set; }

        /// <summary>
        /// 税率
        /// </summary>
        public int? TaxRate { get; set; }

        public int? RCAuditor { get; set; }
        public string RCAuditorName { get; set; }

        public int? PRAuthorizationId { get; set; }
        /// <summary>
        /// 1为低估值PR单5000以下，2为高估值PR单5000以上
        /// </summary>
        public int ValueLevel
        {
            get { return _ValueLevel; }
            set { _ValueLevel = value; }
        }

        private bool _HaveInvoice;
        /// <summary>
        /// 是否有发票
        /// </summary>
        public bool HaveInvoice
        {
            get { return _HaveInvoice; }
            set { _HaveInvoice = value; }
        }

        public string NewMediaOrderIDs
        {
            get { return _NewMediaOrderIDs; }
            set { _NewMediaOrderIDs = value;  }
        }
        private decimal _MediaOldAmount;
        public decimal MediaOldAmount
        {
            get { return _MediaOldAmount; }
            set { _MediaOldAmount = value; }
        }

        /// <summary>
        /// 协议供应商PR单采购部付款提交人
        /// </summary>
        public int PaymentUserID { get; set; }
        /// <summary>
        /// 固定式账期为0，开放式账期为1
        /// </summary>
        public int PeriodType { get; set; }
        /// <summary>
        /// 附加收货人USERID
        /// </summary>
        public int appendReceiver
        {
            get { return _appendReceiver; }
            set { _appendReceiver = value; }
        }

        /// <summary>
        /// 附加收货人
        /// </summary>
        public string appendReceiverName
        {
            get { return _appendReceiverName; }
            set { _appendReceiverName = value; }
        }
        
        /// <summary>
        /// 附加收货人联系方式
        /// </summary>
        public string appendReceiverInfo
        {
            get { return _appendReceiverInfo; }
            set { _appendReceiverInfo = value; }
        }

        /// <summary>
        /// 附加收货人业务组
        /// </summary>
        public string appendReceiverGroup
        {
            get { return _appendReceiverGroup; }
            set { _appendReceiverGroup = value; }
        }

        /// <summary>
        /// 押金
        /// </summary>
        public decimal Foregift
        {
            get { return _foregift; }
            set { _foregift = value; }
        }

        private bool _isMajordomoUndo = false;
        /// <summary>
        /// 是否为总监撤销
        /// </summary>
        public bool isMajordomoUndo
        {
            get { return _isMajordomoUndo; }
            set { _isMajordomoUndo = value; }
        }

        private decimal _ototalprice;
        /// <summary>
        /// Gets or sets the total price.
        /// </summary>
        /// <value>The O total price.</value>
        public decimal OTotalPrice
        {
            get { return _ototalprice; }
            set { _ototalprice = value; }
        }

        private int prjid;
        /// <summary>
        /// Gets or sets the project ID.
        /// </summary>
        /// <value>The project ID.</value>
        public int ProjectID
        {
            get { return prjid; }
            set { prjid = value; }
        }

        /// <summary>
        /// 用于对应工作流中的任务项ID
        /// </summary>
        /// <value>The workitem ID.</value>
        public int WorkitemID
        {
            get { return workitemid; }
            set { workitemid = value; }
        }

        private int instanceid;
        /// <summary>
        /// 用于对应工作流实例ID
        /// </summary>
        /// <value>The instance ID.</value>
        public int InstanceID
        {
            get { return instanceid; }
            set { instanceid = value; }
        }

        /// <summary>
        /// Gets or sets the type of the PR.
        /// </summary>
        /// <value>The type of the PR.</value>
        public int PRType
        {
            get { return _prtype; }
            set { _prtype = value; }
        }

        private string workitemname;
        /// <summary>
        /// 用于对应工作流中的任务项名称
        /// </summary>
        /// <value>The name of the work item.</value>
        public string WorkItemName
        {
            get { return workitemname; }
            set { workitemname = value; }
        }

        private int processid;
        /// <summary>
        /// 获取工作流模板
        /// </summary>
        /// <value>The process ID.</value>
        public int ProcessID
        {
            get { return processid; }
            set { processid = value; }
        }

        private string _glideno;
        /// <summary>
        /// Gets the glideno.
        /// </summary>
        /// <value>The glideno.</value>
        public string glideno
        {
            get
            {
                _glideno = "";
                for (int i = 0; i < (7 - this.id.ToString().Length); i++)
                {
                    _glideno += "0";
                }
                return _glideno + this.id;
            }
        }

        /// <summary>
        /// 其他
        /// </summary>
        /// <value>The others.</value>
        public string others
        {
            get { return _others; }
            set { _others = value; }
        }

        /// <summary>
        /// 流水号自增ID
        /// </summary>
        /// <value>The id.</value>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }

        /// <summary>
        /// 申请单号
        /// </summary>
        /// <value>The pr no.</value>
        public string PrNo
        {
            get { return _prNo; }
            set { _prNo = value; }
        }

        /// <summary>
        /// 申请人ID
        /// </summary>
        /// <value>The requestor.</value>
        public int requestor
        {
            set { _requestor = value; }
            get { return _requestor; }
        }

        /// <summary>
        /// 申请人Name
        /// </summary>
        /// <value>The requestorname.</value>
        public string requestorname
        {
            set { _requestorname = value; }
            get { return _requestorname; }
        }

        /// <summary>
        /// 申请日期
        /// </summary>
        /// <value>The app_date.</value>
        public DateTime app_date
        {
            set { _app_date = value; }
            get { return _app_date; }
        }

        /// <summary>
        /// 申请人联络方式
        /// </summary>
        /// <value>The requestor_info.</value>
        public string requestor_info
        {
            set { _requestor_info = value; }
            get { return _requestor_info; }
        }

        /// <summary>
        /// 业务组
        /// </summary>
        /// <value>The requestor_group.</value>
        public string requestor_group
        {
            set { _requestor_group = value; }
            get { return _requestor_group; }
        }

        /// <summary>
        /// 使用人ID
        /// </summary>
        /// <value>The enduser.</value>
        public int enduser
        {
            set { _enduser = value; }
            get { return _enduser; }
        }

        /// <summary>
        /// 使用人Name
        /// </summary>
        /// <value>The endusername.</value>
        public string endusername
        {
            set { _endusername = value; }
            get { return _endusername; }
        }

        /// <summary>
        /// 使用人联络方式
        /// </summary>
        /// <value>The enduser_info.</value>
        public string enduser_info
        {
            set { _enduser_info = value; }
            get { return _enduser_info; }
        }

        /// <summary>
        /// 使用人业务组
        /// </summary>
        /// <value>The enduser_group.</value>
        public string enduser_group
        {
            set { _enduser_group = value; }
            get { return _enduser_group; }
        }

        /// <summary>
        /// 收货人ID
        /// </summary>
        /// <value>The goods_receiver.</value>
        public int goods_receiver
        {
            set { _goods_receiver = value; }
            get { return _goods_receiver; }
        }

        /// <summary>
        /// 收货人Name
        /// </summary>
        /// <value>The receivername.</value>
        public string receivername
        {
            set { _receivername = value; }
            get { return _receivername; }
        }

        /// <summary>
        /// 收货人联络方式
        /// </summary>
        /// <value>The receiver_info.</value>
        public string receiver_info
        {
            set { _receiver_info = value; }
            get { return _receiver_info; }
        }

        /// <summary>
        /// 收货地址
        /// </summary>
        /// <value>The ship_address.</value>
        public string ship_address
        {
            set { _ship_address = value; }
            get { return _ship_address; }
        }

        /// <summary>
        /// 项目号
        /// </summary>
        /// <value>The project_code.</value>
        public string project_code
        {
            set { _project_code = value; }
            get { return _project_code; }
        }

        /// <summary>
        /// 项目号id.
        /// </summary>
        /// <value>The project_id.</value>
        public int Project_id
        {
            set { _project_id = value; }
            get { return _project_id; }
        }

        /// <summary>
        /// 项目号内容描述
        /// </summary>
        /// <value>The project_descripttion.</value>
        public string project_descripttion
        {
            set { _project_descripttion = value; }
            get { return _project_descripttion; }
        }

        /// <summary>
        /// 第三方采购成本预算
        /// </summary>
        /// <value>The buggeted.</value>
        public decimal buggeted
        {
            set { _buggeted = value; }
            get { return _buggeted; }
        }

        /// <summary>
        /// 货币类型
        /// </summary>
        /// <value>The moneytype.</value>
        public string moneytype
        {
            get { return _moneytype; }
            set { _moneytype = value; }
        }

        /// <summary>
        /// 供应商名称
        /// </summary>
        /// <value>The supplier_name.</value>
        public string supplier_name
        {
            set { _supplier_name = value; }
            get { return _supplier_name; }
        }

        /// <summary>
        /// 供应商地址
        /// </summary>
        /// <value>The supplier_address.</value>
        public string supplier_address
        {
            set { _supplier_address = value; }
            get { return _supplier_address; }
        }

        /// <summary>
        /// 供应商联系人
        /// </summary>
        /// <value>The supplier_linkman.</value>
        public string supplier_linkman
        {
            set { _supplier_linkman = value; }
            get { return _supplier_linkman; }
        }

        /// <summary>
        /// 供应商手机
        /// </summary>
        /// <value>The supplier_cellphone.</value>
        public string Supplier_cellphone
        {
            get { return _supplier_cellphone; }
            set { _supplier_cellphone = value; }
        }

        /// <summary>
        /// 供应商联系电话
        /// </summary>
        /// <value>The supplier_phone.</value>
        public string supplier_phone
        {
            set { _supplier_phone = value; }
            get { return _supplier_phone; }
        }

        /// <summary>
        /// 供应商传真
        /// </summary>
        /// <value>The supplier_fax.</value>
        public string supplier_fax
        {
            set { _supplier_fax = value; }
            get { return _supplier_fax; }
        }

        /// <summary>
        /// 供应商邮件
        /// </summary>
        /// <value>The supplier_email.</value>
        public string supplier_email
        {
            set { _supplier_email = value; }
            get { return _supplier_email; }
        }

        /// <summary>
        /// 供应商来源
        /// </summary>
        /// <value>The source.</value>
        public string source
        {
            set { _source = value; }
            get { return _source; }
        }

        /// <summary>
        /// 框架协议号码
        /// </summary>
        /// <value>The fa_no.</value>
        public string fa_no
        {
            set { _fa_no = value; }
            get { return _fa_no; }
        }

        /// <summary>
        /// 工作需求描述
        /// </summary>
        /// <value>The sow.</value>
        public string sow
        {
            set { _sow = value; }
            get { return _sow; }
        }

        /// <summary>
        /// 工作描述附件
        /// </summary>
        /// <value>The sow2.</value>
        public string sow2
        {
            set { _sow2 = value; }
            get { return _sow2; }
        }

        /// <summary>
        /// 采购物品备注
        /// </summary>
        /// <value>The sow3.</value>
        public string sow3
        {
            set { _sow3 = value; }
            get { return _sow3; }
        }

        /// <summary>
        /// 预付金额
        /// </summary>
        /// <value>The sow4.</value>
        public string sow4
        {
            set { _sow4 = value; }
            get { return _sow4; }
        }

        /// <summary>
        /// 付款账期
        /// </summary>
        /// <value>The payment_terms.</value>
        public string payment_terms
        {
            set { _payment_terms = value; }
            get { return _payment_terms; }
        }

        /// <summary>
        /// 订单编号
        /// </summary>
        /// <value>The orderid.</value>
        public string orderid
        {
            set { _orderid = value; }
            get { return _orderid; }
        }

        /// <summary>
        /// 谈判类型
        /// </summary>
        /// <value>The type.</value>
        public string type
        {
            set { _type = value; }
            get { return _type; }
        }

        /// <summary>
        /// 比价节约
        /// </summary>
        /// <value>The contrast.</value>
        public string contrast
        {
            set { _contrast = value; }
            get { return _contrast; }
        }

        /// <summary>
        /// 议价节约
        /// </summary>
        /// <value>The consult.</value>
        public string consult
        {
            set { _consult = value; }
            get { return _consult; }
        }

        /// <summary>
        /// 初审人ID
        /// </summary>
        /// <value>The first_assessor.</value>
        public int first_assessor
        {
            set { _first_assessor = value; }
            get { return _first_assessor; }
        }

        /// <summary>
        /// 初审人Name
        /// </summary>
        /// <value>The first_assessorname.</value>
        public string first_assessorname
        {
            set { _first_assessorname = value; }
            get { return _first_assessorname; }
        }

        /// <summary>
        /// 暂不使用
        /// </summary>
        /// <value>The afterwardsname.</value>
        public string afterwardsname
        {
            set { _afterwardsname = value; }
            get { return _afterwardsname; }
        }

        /// <summary>
        /// 采购单状态 0保存，1提交，2通过
        /// </summary>
        public int status
        {
            set { _status = value; }
            get { return _status; }
        }

        private string _itemno;
        /// <summary>
        /// 采购物品名称
        /// </summary>
        /// <value>The itemno.</value>
        public string itemno
        {
            get { return _itemno; }
            set { _itemno = value; }
        }

        /// <summary>
        /// 采购物料
        /// </summary>
        /// <value>The itemno.</value>
        public string ProductType { get; set; }

        private decimal _totalprice;
        /// <summary>
        /// 采购总金额
        /// </summary>
        /// <value>The totalprice.</value>
        public decimal totalprice
        {
            get { return _totalprice; }
            set { _totalprice = value; }
        }

        private string _thirdParty_materielDesc;
        /// <summary>
        /// 第三方采购物料描述
        /// </summary>
        /// <value>The third party_materiel desc.</value>
        public string thirdParty_materielDesc
        {
            get { return _thirdParty_materielDesc; }
            set { _thirdParty_materielDesc = value; }
        }

        private string _thirdParty_materielID;
        /// <summary>
        /// 第三方采购物料id
        /// </summary>
        /// <value>The third party_materiel desc.</value>
        public string thirdParty_materielID
        {
            get { return _thirdParty_materielID; }
            set { _thirdParty_materielID = value; }
        }

        private string _requisition_overrule;
        /// <summary>
        /// 申请单驳回信息
        /// </summary>
        /// <value>The requisition_overrule.</value>
        public string requisition_overrule
        {
            get { return _requisition_overrule; }
            set { _requisition_overrule = value; }
        }

        private string _order_overrule;
        /// <summary>
        /// 订单驳回信息
        /// </summary>
        /// <value>The order_overrule.</value>
        public string order_overrule
        {
            get { return _order_overrule; }
            set { _order_overrule = value; }
        }

        private DateTime _lasttime;
        /// <summary>
        /// 最后修改时间
        /// </summary>
        /// <value>The lasttime.</value>
        public DateTime lasttime
        {
            get { return _lasttime; }
            set { _lasttime = value; }
        }

        private DateTime _requisition_committime;
        /// <summary>
        /// 申请单提交时间
        /// </summary>
        /// <value>The requisition_committime.</value>
        public DateTime requisition_committime
        {
            get { return _requisition_committime; }
            set { _requisition_committime = value; }
        }

        private DateTime _order_committime;
        /// <summary>
        /// 订单生成时间
        /// </summary>
        /// <value>The order_committime.</value>
        public DateTime order_committime
        {
            get { return _order_committime; }
            set { _order_committime = value; }
        }

        private DateTime _order_audittime;
        /// <summary>
        /// 订单审核时间
        /// </summary>
        /// <value>The order_audittime.</value>
        public DateTime order_audittime
        {
            get { return _order_audittime; }
            set { _order_audittime = value; }
        }

        /// <summary>
        /// 紧急采购
        /// </summary>
        /// <value>The em buy.</value>
        public string EmBuy
        {
            get { return _emBuy; }
            set { _emBuy = value; }
        }

        /// <summary>
        /// 客户指定
        /// </summary>
        /// <value>The cus ask.</value>
        public string CusAsk
        {
            get { return _cusAsk; }
            set { _cusAsk = value; }
        }

        /// <summary>
        /// 客户名称
        /// </summary>
        /// <value>The name of the cus.</value>
        public string CusName
        {
            get { return _cusName; }
            set { _cusName = value; }
        }

        /// <summary>
        /// 合同号码
        /// </summary>
        /// <value>The contract no.</value>
        public string ContractNo
        {
            get { return _contractNo; }
            set { _contractNo = value; }
        }

        /// <summary>
        /// 收货人业务组
        /// </summary>
        /// <value>The receiver group.</value>
        public string ReceiverGroup
        {
            get { return _receivergroup; }
            set { _receivergroup = value; }
        }

        private int _Filiale_Auditor;
        /// <summary>
        /// 分公司审核人
        /// </summary>
        /// <value>The filiale_ auditor.</value>
        public int Filiale_Auditor
        {
            get { return _Filiale_Auditor; }
            set { _Filiale_Auditor = value; }
        }

        private string _Department;
        /// <summary>
        /// 成本所属组
        /// </summary>
        /// <value>The department.</value>
        public string Department
        {
            get { return _Department; }
            set { _Department = value; }
        }

        private int _departmentid;
        /// <summary>
        /// 成本所属组id
        /// </summary>
        /// <value>The departmentid.</value>
        public int Departmentid
        {
            get { return _departmentid; }
            set { _departmentid = value; }
        }

        private int _requisitionflow;
        /// <summary>
        /// 申请单流向
        /// </summary>
        /// <value>The requisitionflow.</value>
        public int Requisitionflow
        {
            get { return _requisitionflow; }
            set { _requisitionflow = value; }
        }

        private int _addstatus;
        /// <summary>
        /// 申请单流向
        /// </summary>
        /// <value>The addstatus.</value>
        public int Addstatus
        {
            get { return _addstatus; }
            set { _addstatus = value; }
        }

        private string _afterwardsReason;
        /// <summary>
        /// 事后申请原因
        /// </summary>
        /// <value>The afterwards reason.</value>
        public string afterwardsReason
        {
            get { return _afterwardsReason; }
            set { _afterwardsReason = value; }
        }

        private string _EmBuyReason;
        /// <summary>
        /// 紧急采购原因
        /// </summary>
        /// <value>The em buy reason.</value>
        public string EmBuyReason
        {
            get { return _EmBuyReason; }
            set { _EmBuyReason = value; }
        }

        private string _CusAskYesReason;
        /// <summary>
        /// 客户指定原因
        /// </summary>
        /// <value>The cus ask yes reason.</value>
        public string CusAskYesReason
        {
            get { return _CusAskYesReason; }
            set { _CusAskYesReason = value; }
        }

        private decimal _totalprices;
        /// <summary>
        /// 总价格数
        /// </summary>
        /// <value>The totalprices.</value>
        public decimal totalprices
        {
            get { return _totalprices; }
            set { _totalprices = value; }
        }

        private string _contrastFile;
        /// <summary>
        /// 比价节约附件
        /// </summary>
        /// <value>The contrast file.</value>
        public string contrastFile
        {
            get { return _contrastFile; }
            set { _contrastFile = value; }
        }

        private string _consultFile;
        /// <summary>
        /// 议价节约附件
        /// </summary>
        /// <value>The consult file.</value>
        public string consultFile
        {
            get { return _consultFile; }
            set { _consultFile = value; }
        }

        private string _receiver_Otherinfo;
        /// <summary>
        /// 收货人其他联络方式
        /// </summary>
        /// <value>The receiver_ otherinfo.</value>
        public string receiver_Otherinfo
        {
            get { return _receiver_Otherinfo; }
            set { _receiver_Otherinfo = value; }
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

        private string _fili_overrule;
        /// <summary>
        /// 分公司审核驳回信息
        /// </summary>
        /// <value>The fili_overrule.</value>
        public string fili_overrule
        {
            get { return _fili_overrule; }
            set { _fili_overrule = value; }
        }

        private int _receivePrepay;
        /// <summary>
        /// 是否收到预付款
        /// 0:未收 1:已收
        /// </summary>
        /// <value>The receive prepay.</value>
        public int receivePrepay
        {
            get { return _receivePrepay; }
            set { _receivePrepay = value; }
        }

        private string _Filiale_AuditName;
        /// <summary>
        /// 分公司审核人名称
        /// </summary>
        /// <value>The name of the filiale_ audit.</value>
        public string Filiale_AuditName
        {
            get { return _Filiale_AuditName; }
            set { _Filiale_AuditName = value; }
        }

        private string _CusAskEmailFile;
        /// <summary>
        /// 客户指定邮件附件
        /// </summary>
        /// <value>The cus ask email file.</value>
        public string CusAskEmailFile
        {
            get { return _CusAskEmailFile; }
            set { _CusAskEmailFile = value; }
        }

        private string _account_name;
        /// <summary>
        /// 开户公司名称
        /// </summary>
        /// <value>The account_name.</value>
        public string account_name
        {
            get { return _account_name; }
            set { _account_name = value; }
        }

        private string _account_bank;
        /// <summary>
        /// 开户银行
        /// </summary>
        /// <value>The account_bank.</value>
        public string account_bank
        {
            get { return _account_bank; }
            set { _account_bank = value; }
        }

        private string _account_number;
        /// <summary>
        /// 账号
        /// </summary>
        /// <value>The account_number.</value>
        public string account_number
        {
            get { return _account_number; }
            set { _account_number = value; }
        }

        private string _contrastRemark;
        /// <summary>
        /// 比价信息备注
        /// </summary>
        /// <value>The contrast remark.</value>
        public string contrastRemark
        {
            get { return _contrastRemark; }
            set { _contrastRemark = value; }
        }

        private string _contrastUpFiles;
        /// <summary>
        /// 比价信息附件，以#分隔
        /// </summary>
        /// <value>The contrast up files.</value>
        public string contrastUpFiles
        {
            get { return _contrastUpFiles; }
            set { _contrastUpFiles = value; }
        }

        private DateTime _prepayBegindate;
        /// <summary>
        /// 预付款起始时间
        /// </summary>
        public DateTime prepayBegindate
        {
            get { return _prepayBegindate; }
            set { _prepayBegindate = value; }
        }

        private DateTime _prepayEnddate;
        /// <summary>
        /// 预付款结束时间
        /// </summary>
        public DateTime prepayEnddate
        {
            get { return _prepayEnddate; }
            set { _prepayEnddate = value; }
        }

        private string _prMediaRemark;
        /// <summary>
        /// 媒介审批备注
        /// </summary>
        public string prMediaRemark
        {
            get { return _prMediaRemark; }
            set { _prMediaRemark = value; }
        }

        private DateTime _prMediaAuditTime = DateTime.Parse("1900-1-1 0:00:00");
        /// <summary>
        /// 媒介审批时间
        /// </summary>
        public DateTime prMediaAuditTime
        {
            get { return _prMediaAuditTime; }
            set { _prMediaAuditTime = value; }
        }

        /// <summary>
        /// 申请单业务类型，对公对私等
        /// </summary>
        /// <value>The type of the operation.</value>
        public int OperationType
        {
            get { return _operationType; }
            set { _operationType = value; }
        }

        private int _purchaseAuditor;
        /// <summary>
        /// 采购总监审批人ID
        /// </summary>
        public int purchaseAuditor
        {
            get { return _purchaseAuditor; }
            set { _purchaseAuditor = value; }
        }

        private string _purchaseAuditorName;
        /// <summary>
        /// 采购总监审批人
        /// </summary>
        public string purchaseAuditorName
        {
            get { return _purchaseAuditorName; }
            set { _purchaseAuditorName = value; }
        }

        private int _mediaAuditor;
        /// <summary>
        /// 媒介审批人ID
        /// </summary>
        public int mediaAuditor
        {
            get { return _mediaAuditor; }
            set { _mediaAuditor = value; }
        }

        private string _mediaAuditorName;
        /// <summary>
        /// 媒介审批人
        /// </summary>
        public string mediaAuditorName
        {
            get { return _mediaAuditorName; }
            set { _mediaAuditorName = value; }
        }

        private int _adAuditor;
        /// <summary>
        /// AD审批人ID
        /// </summary>
        public int adAuditor
        {
            get { return _adAuditor; }
            set { _adAuditor = value; }
        }

        private string _adAuditorName;
        /// <summary>
        /// AD审批人
        /// </summary>
        public string adAuditorName
        {
            get { return _adAuditorName; }
            set { _adAuditorName = value; }
        }

        private string _adRemark;
        /// <summary>
        /// AD审批备注
        /// </summary>
        public string adRemark
        {
            get { return _adRemark; }
            set { _adRemark = value; }
        }

        private DateTime? _adAuditTime;
        /// <summary>
        /// AD审批时间
        /// </summary>
        public DateTime? adAuditTime
        {
            get { return _adAuditTime; }
            set { _adAuditTime = value; }
        }

        private bool _oldflag;
        /// <summary>
        /// 线上/线下标识 1:线下 0:线上
        /// </summary>
        public bool oldFlag
        {
            get { return _oldflag; }
            set { _oldflag = value; }
        }

        private int _isCast;
        /// <summary>
        /// 是否计算 0：计算 1：不计算
        /// </summary>
        public int isCast
        {
            get { return _isCast; }
            set { _isCast = value; }
        }

        private int _inUse;
        /// <summary>
        /// 是否在使用中 0：停用 1：项目号停用 2：使用
        /// </summary>
        public int InUse
        {
            get { return _inUse; }
            set { _inUse = value; }
        }

        public int IsMediaOrder { get; set; }

        public int IsFactoring { get; set; }
        public DateTime FactoringDate { get; set; }

        /// <summary>
        /// 关联的框架协议PrId集合 （前后，号分隔）
        /// </summary>
        public string FCPrIds { get; set; }

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
                id = int.Parse(r["id"].ToString());
            }
            PrNo = r["prNo"].ToString();
            if (null != r["requestor"] && r["requestor"].ToString() != "")
            {
                requestor = int.Parse(r["requestor"].ToString());
            }
            requestorname = r["requestorname"].ToString();
            if (null != r["app_date"] && r["app_date"].ToString() != "")
            {
                app_date = DateTime.Parse(r["app_date"].ToString());
            }
            requestor_info = r["requestor_info"].ToString();
            requestor_group = r["requestor_group"].ToString();
            if (null != r["enduser"] && r["enduser"].ToString() != "")
            {
                enduser = int.Parse(r["enduser"].ToString());
            }
            endusername = r["endusername"].ToString();
            enduser_info = r["enduser_info"].ToString();
            enduser_group = r["enduser_group"].ToString();
            if (null != r["goods_receiver"] && r["goods_receiver"].ToString() != "")
            {
                goods_receiver = int.Parse(r["goods_receiver"].ToString());
            }
            receivername = r["receivername"].ToString();
            receiver_info = r["receiver_info"].ToString();
            ship_address = r["ship_address"].ToString();
            project_code = r["project_code"].ToString();
            if (null != r["project_id"] && r["project_id"].ToString() != "")
            {
                Project_id = int.Parse(r["project_id"].ToString());
            }
            project_descripttion = r["project_descripttion"].ToString();
            if (null != r["buggeted"] && r["buggeted"].ToString() != "")
            {
                buggeted = decimal.Parse(r["buggeted"].ToString());
            }
            supplier_name = r["supplier_name"].ToString();
            supplier_address = r["supplier_address"].ToString();
            supplier_linkman = r["supplier_linkman"].ToString();
            Supplier_cellphone = r["supplier_cellphone"].ToString();
            supplier_phone = r["supplier_phone"].ToString();
            supplier_fax = r["supplier_fax"].ToString();
            supplier_email = r["supplier_email"].ToString();
            source = r["source"].ToString();
            fa_no = r["fa_no"].ToString();
            sow = r["sow"].ToString();
            sow2 = r["sow2"].ToString();
            sow3 = r["sow3"].ToString();
            sow4 = r["sow4"].ToString();
            payment_terms = r["payment_terms"].ToString();
            orderid = r["orderid"].ToString();
            type = r["type"].ToString();
            contrast = r["contrast"].ToString();
            consult = r["consult"].ToString();
            if (null != r["first_assessor"] && r["first_assessor"].ToString() != "")
            {
                first_assessor = int.Parse(r["first_assessor"].ToString());
            }
            first_assessorname = r["first_assessorname"].ToString();
            afterwardsname = r["afterwardsname"].ToString();
            if (null != r["status"] && r["status"].ToString() != "")
            {
                status = int.Parse(r["status"].ToString());
            }
            others = r["others"].ToString();
            try
            {
                _ototalprice = r["ototalprice"] == DBNull.Value ? 0 : decimal.Parse(r["ototalprice"].ToString());
                itemno = (r["item_no"] == null || r["item_no"] == DBNull.Value) ? "" : r["item_no"].ToString();
                ProductType = (r["ProductType"] == null || r["ProductType"] == DBNull.Value) ? "" : r["ProductType"].ToString();
                producttypename = (r["producttypename"] == null || r["producttypename"] == DBNull.Value) ? "" : r["producttypename"].ToString();
            }
            catch (IndexOutOfRangeException ex) { }
            totalprice = r["totalprice"] == DBNull.Value ? 0 : decimal.Parse(r["totalprice"].ToString());
            _thirdParty_materielDesc = r["thirdParty_materielDesc"].ToString();
            _thirdParty_materielID = r["thirdParty_materielID"].ToString();
            _requisition_overrule = r["requisition_overrule"].ToString();
            _order_overrule = r["order_overrule"].ToString();
            _lasttime = r["lasttime"] == DBNull.Value ? DateTime.MinValue : DateTime.Parse(r["lasttime"].ToString());
            _requisition_committime = r["requisition_committime"] == DBNull.Value ? DateTime.MinValue : DateTime.Parse(r["requisition_committime"].ToString());
            _order_committime = r["order_committime"] == DBNull.Value ? DateTime.MinValue : DateTime.Parse(r["order_committime"].ToString());
            _order_audittime = r["order_audittime"] == DBNull.Value ? DateTime.MinValue : DateTime.Parse(r["order_audittime"].ToString());

            _emBuy = r["EmBuy"].ToString();
            _cusAsk = r["CusAsk"].ToString();
            _cusName = r["CusName"].ToString();
            _contractNo = r["ContractNo"].ToString();
            _receivergroup = r["receivergroup"].ToString();

            _Filiale_Auditor = r["Filiale_Auditor"] == DBNull.Value ? 0 : int.Parse(r["Filiale_Auditor"].ToString());
            _Department = r["Department"] == DBNull.Value ? "" : r["Department"].ToString();
            if (null != r["DepartmentId"] && r["DepartmentId"].ToString() != "")
            {
                Departmentid = int.Parse(r["DepartmentId"].ToString());
            }
            if (null != r["requisitionflow"] && r["requisitionflow"].ToString() != "")
            {
                _requisitionflow = int.Parse(r["requisitionflow"].ToString());
            }
            if (null != r["addstatus"] && r["addstatus"].ToString() != "")
            {
                _addstatus = int.Parse(r["addstatus"].ToString());
            }
            _afterwardsReason = r["afterwardsReason"].ToString();
            _EmBuyReason = r["EmBuyReason"].ToString();
            _CusAskYesReason = r["CusAskYesReason"].ToString();
            moneytype = r["moneyType"].ToString();
            _consultFile = r["consultFile"].ToString();
            _contrastFile = r["consultFile"].ToString();
            _receiver_Otherinfo = r["receiver_Otherinfo"].ToString();
            _fili_overrule = r["fili_overrule"].ToString();
            _receivePrepay = int.Parse(r["receivePrepay"] == DBNull.Value ? "0" : r["receivePrepay"].ToString());
            prjid = int.Parse(r["projectid"] == DBNull.Value ? "0" : r["projectid"].ToString());
            _prtype = int.Parse(r["prtype"] == DBNull.Value ? "0" : r["prtype"].ToString());
            _Filiale_AuditName = r["Filiale_AuditName"].ToString();
            _CusAskEmailFile = r["CusAskEmailFile"].ToString();
            _account_name = r["account_name"].ToString();
            _account_bank = r["account_bank"].ToString();
            _account_number = r["account_number"].ToString();
            _contrastRemark = r["contrastRemark"].ToString();
            _contrastUpFiles = r["contrastUpFiles"].ToString();
            _prepayBegindate = r["prepayBegindate"] == DBNull.Value ? DateTime.Parse(Common.State.datetime_minvalue) : DateTime.Parse(r["prepayBegindate"].ToString());
            _prepayEnddate = r["prepayEnddate"] == DBNull.Value ? DateTime.Parse(Common.State.datetime_minvalue) : DateTime.Parse(r["prepayEnddate"].ToString());
            instanceid = r["instanceId"] == DBNull.Value ? 0 : int.Parse(r["instanceId"].ToString());
            processid = r["processId"] == DBNull.Value ? 0 : int.Parse(r["processId"].ToString());
            _prMediaRemark = r["prMediaRemark"].ToString();
            _prMediaAuditTime = r["prMediaAuditTime"] == DBNull.Value ? DateTime.Parse(Common.State.datetime_minvalue) : DateTime.Parse(r["prMediaAuditTime"].ToString());
            _isMajordomoUndo = r["isMajordomoUndo"] == DBNull.Value ? false : bool.Parse(r["isMajordomoUndo"].ToString());
            _operationType = int.Parse(r["OperationType"] == DBNull.Value ? "0" : r["OperationType"].ToString());
            _purchaseAuditor = r["purchaseAuditor"] == DBNull.Value ? 0 : int.Parse(r["purchaseAuditor"].ToString());
            _purchaseAuditorName = r["purchaseAuditorName"].ToString();
            _mediaAuditor = r["mediaAuditor"] == DBNull.Value ? 0 : int.Parse(r["mediaAuditor"].ToString());
            _mediaAuditorName = r["mediaAuditorName"].ToString();
            _adAuditor = r["adAuditor"] == DBNull.Value ? 0 : int.Parse(r["adAuditor"].ToString());
            _adAuditorName = r["adAuditorName"].ToString();
            _adRemark = r["adRemark"].ToString();
            _adAuditTime = r["adAuditTime"] == DBNull.Value ? DateTime.Parse(Common.State.datetime_minvalue) : DateTime.Parse(r["adAuditTime"].ToString());
            _oldflag = r["oldflag"] == DBNull.Value ? false : bool.Parse(r["oldflag"].ToString());
            _isCast = r["isCast"] == DBNull.Value ? 1 : int.Parse(r["isCast"].ToString());
            _inUse = r["inuse"] == DBNull.Value ? ((int)Common.State.PRInUse.Use) : int.Parse(r["inuse"].ToString());
            _foregift = r["foregift"] == DBNull.Value ? 0 : decimal.Parse(r["foregift"].ToString());
            _appendReceiver = r["appendReceiver"] == DBNull.Value ? 0 : int.Parse(r["appendReceiver"].ToString());
            _appendReceiverName = r["appendReceiverName"] == DBNull.Value ? "" : r["appendReceiverName"].ToString();
            _appendReceiverInfo = r["appendReceiverInfo"] == DBNull.Value ? "" : r["appendReceiverInfo"].ToString();
            _appendReceiverGroup = r["appendReceiverGroup"] == DBNull.Value ? "" : r["appendReceiverGroup"].ToString();
            PaymentUserID = r["PaymentUserID"] == DBNull.Value ? 0 : int.Parse(r["PaymentUserID"].ToString());
            PeriodType = r["PeriodType"] == DBNull.Value ? 0 : int.Parse(r["PeriodType"].ToString());
            _MediaOldAmount = r["MediaOldAmount"] == DBNull.Value ? 0 : decimal.Parse(r["MediaOldAmount"].ToString());
            _NewMediaOrderIDs = r["NewMediaOrderIDs"].ToString();
            _HaveInvoice = r["HaveInvoice"] == DBNull.Value ? false : bool.Parse(r["HaveInvoice"].ToString());
            _ValueLevel = r["ValueLevel"] == DBNull.Value ? 0 : int.Parse(r["ValueLevel"].ToString());
            if( r["PRAuthorizationId"] != DBNull.Value)
                PRAuthorizationId = int.Parse(r["PRAuthorizationId"].ToString());

            if (null != r["IsMediaOrder"] && r["IsMediaOrder"].ToString() != "")
            {
                IsMediaOrder = int.Parse(r["IsMediaOrder"].ToString());
            }
            if (null != r["IsFactoring"] && r["IsFactoring"].ToString() != "")
            {
                IsFactoring = int.Parse(r["IsFactoring"].ToString());
            }
            FactoringDate = r["FactoringDate"] == DBNull.Value ? DateTime.MinValue : DateTime.Parse(r["FactoringDate"].ToString());
            if (null != r["RCAuditor"] && r["RCAuditor"].ToString() != "")
            {
                RCAuditor = int.Parse(r["RCAuditor"].ToString());
            }
            RCAuditorName = r["RCAuditorName"].ToString();
            InvoiceType = r["InvoiceType"].ToString();
            if (r["TaxRate"] != DBNull.Value)
                TaxRate = int.Parse(r["TaxRate"].ToString());
            FCPrIds = r["FCPrIds"] == DBNull.Value ? "" : r["FCPrIds"].ToString();
        }
        #endregion

        [Serializable]
        public partial class GeneralPageModel
        {
            public List<GeneralInfo> modelList = new List<GeneralInfo>();

            public int PageSize { get; set; }

            public int RecordCount { get; set; }

            public int PageIndex { get; set; }

            public int PageCount { get; set; }

            public string Key { get; set; }

            public string Status { get; set; }
        }
    }
}