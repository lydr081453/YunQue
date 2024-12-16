using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.HumanResource.Entity
{
    public class EmployeesInAuxiliariesInfo
    {
        public EmployeesInAuxiliariesInfo()
        { }
        #region Model
        private int _id;
        private int _userid;
        private string _FullNameCN;
        private string _FullNameEN;
        private string _username;
        private string _email;
        private int _auxiliaryid;
        private string _auxiliaryname;
        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int userId
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 用户名
        /// </summary>
        public string username
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 用户中文姓名
        /// </summary>
        public string FullNameCN
        {
            set { _FullNameCN = value; }
            get { return _FullNameCN; }
        }
        /// <summary>
        /// 用户中文姓名
        /// </summary>
        public string FullNameEN
        {
            set { _FullNameEN = value; }
            get { return _FullNameEN; }
        }
        /// <summary>
        /// 用户邮箱
        /// </summary>
        public string email
        {
            set { _email = value; }
            get { return _email; }
        }
        /// <summary>
        /// 辅助工作ID
        /// </summary>
        public int auxiliaryId
        {
            set { _auxiliaryid = value; }
            get { return _auxiliaryid; }
        }
        /// <summary>
        /// 辅助工作名称
        /// </summary>
        public string auxiliaryname
        {
            set { _auxiliaryname = value; }
            get { return _auxiliaryname; }
        }
           
        #endregion Model

    }
}
