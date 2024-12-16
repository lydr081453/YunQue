using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.HumanResource.Entity
{
    public class DimissionITDetailsInfo
    {
        public DimissionITDetailsInfo()
        { }
        #region Model
        private int _dimissionitdetailid;
        private int _dimissionid;
        private string _email;
        private bool _emailisdelete;
        private DateTime? _emailsavelastday;
        private bool _accountisdelete;
        private DateTime? _accountsavelastday;
        private string _pccode;
        private string _pcuseddes;
        private string _otherdes;
        private string _ownpccode;
        private int _principalid;
        private string _principalname;
        /// <summary>
        /// 
        /// </summary>
        public int DimissionITDetailId
        {
            set { _dimissionitdetailid = value; }
            get { return _dimissionitdetailid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int DimissionId
        {
            set { _dimissionid = value; }
            get { return _dimissionid; }
        }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email
        {
            set { _email = value; }
            get { return _email; }
        }
        /// <summary>
        /// 邮箱是否删除
        /// </summary>
        public bool EmailIsDelete
        {
            set { _emailisdelete = value; }
            get { return _emailisdelete; }
        }
        /// <summary>
        /// 保留邮箱期限
        /// </summary>
        public DateTime? EmailSaveLastDay
        {
            set { _emailsavelastday = value; }
            get { return _emailsavelastday; }
        }
        /// <summary>
        /// OA系统账号是否删除
        /// </summary>
        public bool AccountIsDelete
        {
            set { _accountisdelete = value; }
            get { return _accountisdelete; }
        }
        /// <summary>
        /// OA系统账号保留期限
        /// </summary>
        public DateTime? AccountSaveLastDay
        {
            set { _accountsavelastday = value; }
            get { return _accountsavelastday; }
        }
        /// <summary>
        /// 公司电脑编号
        /// </summary>
        public string PCCode
        {
            set { _pccode = value; }
            get { return _pccode; }
        }
        /// <summary>
        /// 公司电脑使用情况描述
        /// </summary>
        public string PCUsedDes
        {
            set { _pcuseddes = value; }
            get { return _pcuseddes; }
        }
        /// <summary>
        /// 其他说明
        /// </summary>
        public string OtherDes
        {
            set { _otherdes = value; }
            get { return _otherdes; }
        }
        /// <summary>
        /// OA设备，自购、自带电脑编号
        /// </summary>
        public string OwnPCCode
        {
            set { _ownpccode = value; }
            get { return _ownpccode; }
        }
        /// <summary>
        /// 负责人ID
        /// </summary>
        public int PrincipalID
        {
            set { _principalid = value; }
            get { return _principalid; }
        }
        /// <summary>
        /// 负责人姓名
        /// </summary>
        public string PrincipalName
        {
            set { _principalname = value; }
            get { return _principalname; }
        }
        #endregion Model
        /// <summary>
        /// 格式化数据对象
        /// </summary>
        /// <param name="r"></param>
        public void PopupData(System.Data.IDataReader r)
        {
            if (r["DimissionITDetailId"].ToString() != "")
            {
                _dimissionitdetailid = int.Parse(r["DimissionITDetailId"].ToString());
            }
            if (r["DimissionId"].ToString() != "")
            {
                _dimissionid = int.Parse(r["DimissionId"].ToString());
            }
            _email = r["Email"].ToString();
            if (r["EmailIsDelete"].ToString() != "")
            {
                if ((r["EmailIsDelete"].ToString() == "1") || (r["EmailIsDelete"].ToString().ToLower() == "true"))
                {
                    _emailisdelete = true;
                }
                else
                {
                    _emailisdelete = false;
                }
            }

            if (r["EmailSaveLastDay"].ToString() != "")
            {
                _emailsavelastday = DateTime.Parse(r["EmailSaveLastDay"].ToString());
            }
            if (r["AccountIsDelete"].ToString() != "")
            {
                if ((r["AccountIsDelete"].ToString() == "1") || (r["AccountIsDelete"].ToString().ToLower() == "true"))
                {
                    _accountisdelete = true;
                }
                else
                {
                    _accountisdelete = false;
                }
            }

            if (r["AccountSaveLastDay"].ToString() != "")
            {
                _accountsavelastday = DateTime.Parse(r["AccountSaveLastDay"].ToString());
            }
            _pccode = r["PCCode"].ToString();
            _pcuseddes = r["PCUsedDes"].ToString();
            _otherdes = r["OtherDes"].ToString();
            _ownpccode = r["OwnPCCode"].ToString();
            if (r["PrincipalID"].ToString() != "")
            {
                _principalid = int.Parse(r["PrincipalID"].ToString());
            }
            _principalname = r["PrincipalName"].ToString();
        }

        /// <summary>
        /// 格式化数据对象
        /// </summary>
        /// <param name="r"></param>
        public void PopupData(System.Data.DataRow r)
        {
            if (r["DimissionITDetailId"].ToString() != "")
            {
                _dimissionitdetailid = int.Parse(r["DimissionITDetailId"].ToString());
            }
            if (r["DimissionId"].ToString() != "")
            {
                _dimissionid = int.Parse(r["DimissionId"].ToString());
            }
            _email = r["Email"].ToString();
            if (r["EmailIsDelete"].ToString() != "")
            {
                if ((r["EmailIsDelete"].ToString() == "1") || (r["EmailIsDelete"].ToString().ToLower() == "true"))
                {
                    _emailisdelete = true;
                }
                else
                {
                    _emailisdelete = false;
                }
            }

            if (r["EmailSaveLastDay"].ToString() != "")
            {
                _emailsavelastday = DateTime.Parse(r["EmailSaveLastDay"].ToString());
            }
            if (r["AccountIsDelete"].ToString() != "")
            {
                if ((r["AccountIsDelete"].ToString() == "1") || (r["AccountIsDelete"].ToString().ToLower() == "true"))
                {
                    _accountisdelete = true;
                }
                else
                {
                    _accountisdelete = false;
                }
            }

            if (r["AccountSaveLastDay"].ToString() != "")
            {
                _accountsavelastday = DateTime.Parse(r["AccountSaveLastDay"].ToString());
            }
            _pccode = r["PCCode"].ToString();
            _pcuseddes = r["PCUsedDes"].ToString();
            _otherdes = r["OtherDes"].ToString();
            _ownpccode = r["OwnPCCode"].ToString();
            if (r["PrincipalID"].ToString() != "")
            {
                _principalid = int.Parse(r["PrincipalID"].ToString());
            }
            _principalname = r["PrincipalName"].ToString();
        }
    }
}