using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Purchase.Entity
{
    public class SupplierSendInfo
    {
        private int _Id;
        private int _DataId;
        private int _DataType;
        private string _Email;

        /// <summary>
        /// 自增ID
        /// </summary>
        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        /// <summary>
        /// 数据ID
        /// </summary>
        public int DataId
        {
            get { return _DataId; }
            set { _DataId = value; }
        }

        /// <summary>
        /// 数据类型
        /// </summary>
        public int DataType
        {
            get { return _DataType; }
            set { _DataType = value; }
        }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }
    }
}
