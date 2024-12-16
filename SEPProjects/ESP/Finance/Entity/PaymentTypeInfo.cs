using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.Entity
{
    [Serializable]
    public partial class PaymentTypeInfo
    {
        public PaymentTypeInfo()
        { }
        #region Model
        private int _paymenttypeid;
        private string _paymenttypename;
        private string _description;
        private bool _isNeedCode;
        private bool _IsBatch;
        public bool IsBatch
        {
            get { return _IsBatch; }
            set { _IsBatch = value; }
        }
        public Boolean IsNeedCode
        {
            get {return  _isNeedCode; }
            set { _isNeedCode = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int PaymentTypeID
        {
            set { _paymenttypeid = value; }
            get { return _paymenttypeid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PaymentTypeName
        {
            set { _paymenttypename = value; }
            get { return _paymenttypename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }
        private bool _IsNeedBank;
        public bool IsNeedBank
        {
            get { return _IsNeedBank; }
            set { _IsNeedBank = value; }
        }
        private string _tag;
        public string Tag
        {
            get { return _tag; }
            set { _tag = value; }
        }

        #endregion Model

    }
}
