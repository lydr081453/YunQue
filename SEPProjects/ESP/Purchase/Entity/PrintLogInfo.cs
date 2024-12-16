using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Purchase.Entity
{
    public class PrintLogInfo
    {
        public PrintLogInfo()
        { }
        #region Model
        private int _printid;
        private int? _formid;
        private int? _formtype;
        private int? _printcount;
        /// <summary>
        /// 
        /// </summary>
        public int PrintID
        {
            set { _printid = value; }
            get { return _printid; }
        }
        /// <summary>
        /// 单据ID
        /// </summary>
        public int? FormID
        {
            set { _formid = value; }
            get { return _formid; }
        }
        /// <summary>
        /// 单据类型
        /// </summary>
        public int? FormType
        {
            set { _formtype = value; }
            get { return _formtype; }
        }
        /// <summary>
        /// 打印次数
        /// </summary>
        public int? PrintCount
        {
            set { _printcount = value; }
            get { return _printcount; }
        }
        #endregion Model

    }
}
