using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.Entity
{
    public class CustomerAttachInfo
    {
//        AttachID	int	Unchecked
//CustomerID	int	Checked
//Attachment	nvarchar(200)	Checked
//Description	nvarchar(50)	Checked

        int _attachid;

        public int AttachID
        {
            get { return _attachid; }
            set { _attachid = value; }
        }

        int _customerid;

        public int CustomerID
        {
            get { return _customerid; }
            set { _customerid = value; }
        }

        string _attachment;

        /// <summary>
        /// 附件
        /// </summary>
        public string Attachment
        {
            get { return _attachment; }
            set { _attachment = value; }
        }

        string _description;

        /// <summary>
        /// 描述
        /// </summary>
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        //FrameBeginDate,FrameEndDate,FrameContractTitle,FrameContractCode

        DateTime? _frameBeginDate;
        /// <summary>
        /// 框架开始时间
        /// </summary>
        public DateTime? FrameBeginDate
        {
            get { return _frameBeginDate; }
            set { _frameBeginDate = value; }
        }

        DateTime? _frameEndDate;
        /// <summary>
        /// 框架结束时间
        /// </summary>
        public DateTime? FrameEndDate
        {
            get { return _frameEndDate; }
            set { _frameEndDate = value; }
        }

        string _frameContractTitle;
        /// <summary>
        /// 框架标题
        /// </summary>
        public string FrameContractTitle
        {
            get { return _frameContractTitle; }
            set { _frameContractTitle = value; }
        }

        string _frameContractCode;
        /// <summary>
        /// 框架号
        /// </summary>
        public string FrameContractCode
        {
            get { return _frameContractCode; }
            set { _frameContractCode = value; }
        }

        public ESP.Finance.Utility.Common.CustomerAttachStatus Status { get; set; }

        public int? ProjectId { get; set; }
    }
}
