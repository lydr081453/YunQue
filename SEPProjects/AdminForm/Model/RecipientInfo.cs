using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminForm.Model
{
    public class RecipientInfo
    {
        public RecipientInfo()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        #region Model
        private int _id;
        private int _gid;
        private string _recipientname;
        private DateTime _recipientdate;
        private string _note;
        private decimal _recipientamount;
        private int _status;


        public string FileUrl { get; set; }
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }

        /// <summary>
        /// Gets or sets the gid.
        /// </summary>
        /// <value>The gid.</value>
        public int Gid
        {
            set { _gid = value; }
            get { return _gid; }
        }

        /// <summary>
        /// Gets or sets the name of the recipient.
        /// </summary>
        /// <value>The name of the recipient.</value>
        public string RecipientName
        {
            set { _recipientname = value; }
            get { return _recipientname; }
        }

        /// <summary>
        /// Gets or sets the recipient date.
        /// </summary>
        /// <value>The recipient date.</value>
        public DateTime RecipientDate
        {
            set { _recipientdate = value; }
            get { return _recipientdate; }
        }

        /// <summary>
        /// Gets or sets the note.
        /// </summary>
        /// <value>The note.</value>
        public string Note
        {
            set { _note = value; }
            get { return _note; }
        }

        /// <summary>
        /// Gets or sets the recipient amount.
        /// </summary>
        /// <value>The recipient amount.</value>
        public decimal RecipientAmount
        {
            set { _recipientamount = value; }
            get { return _recipientamount; }
        }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public int Status
        {
            set { _status = value; }
            get { return _status; }
        }

        private int _receivePrice;
        /// <summary>
        /// 是否收到付款
        /// </summary>
        /// <value>The receive price.</value>
        public int receivePrice
        {
            get { return _receivePrice; }
            set { _receivePrice = value; }
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

        private int _IsConfirm;
        /// <summary>
        /// 供应商是否已确认
        /// </summary>
        /// <value>The is confirm.</value>
        public int IsConfirm
        {
            get { return _IsConfirm; }
            set { _IsConfirm = value; }
        }

        private string __recipientNo;
        /// <summary>
        /// 收货单号.
        /// </summary>
        /// <value>The recipient no.</value>
        public string RecipientNo
        {
            get { return __recipientNo; }
            set { __recipientNo = value; }
        }

        private string _AppraiseRemark;
        /// <summary>
        /// 对供应商及本次交易的评价
        /// </summary>
        public string AppraiseRemark
        {
            get { return _AppraiseRemark; }
            set { _AppraiseRemark = value; }
        }

        private string _singleprice;
        private string _num;
        private string _des;

        /// <summary>
        /// 单价
        /// </summary>
        public string SinglePrice
        {
            set { _singleprice = value; }
            get { return _singleprice; }
        }
        /// <summary>
        /// 数量
        /// </summary>
        public string Num
        {
            set { _num = value; }
            get { return _num; }
        }
        /// <summary>
        /// 内容
        /// </summary>
        public string Des
        {
            set { _des = value; }
            get { return _des; }
        }

        #endregion Model
    }

}
