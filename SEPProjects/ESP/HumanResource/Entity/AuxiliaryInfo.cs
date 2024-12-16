using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.HumanResource.Entity
{
    public class AuxiliaryInfo
    {
        public AuxiliaryInfo()
        { }
        #region Model
        private int _id;
        private string _auxiliaryname;
        private string _description;
        private bool _isdisable;
        private int _companyID;
        private string _companyName;
        private int _apply;
        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 辅助工作名
        /// </summary>
        public string auxiliaryName
        {
            set { _auxiliaryname = value; }
            get { return _auxiliaryname; }
        }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }
        /// <summary>
        /// 是否停用
        /// </summary>
        public bool isDisable
        {
            set { _isdisable = value; }
            get { return _isdisable; }
        }

        /// <summary>
        /// 公司ID
        /// </summary>
        public int companyID
        {
            set { _companyID = value; }
            get { return _companyID; }
        }

        /// <summary>
        /// 公司名称
        /// </summary>
        public string companyName
        {
            set { _companyName = value; }
            get { return _companyName; }
        }

        /// <summary>
        /// 使用方（1待入职、2离职）
        /// </summary>
        public int apply
        {
            set { _apply = value; }
            get { return _apply; }
        }
        #endregion Model
    }
}
