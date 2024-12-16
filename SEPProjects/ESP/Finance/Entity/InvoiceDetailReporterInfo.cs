using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.Entity
{
    public class InvoiceDetailReporterInfo : ESP.Finance.Entity.InvoiceDetailInfo
    {
        private string _projectName;
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName
        {
            get { return _projectName; }
            set { _projectName = value; }
        }
        private int _customerID;

        /// <summary>
        /// 客户ID
        /// </summary>
        public int CustomerID
        {
            get { return _customerID; }
            set { _customerID = value; }
        }
        private string _customerName;

        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomerName
        {
            get { return _customerName; }
            set { _customerName = value; }
        }


    }
}
