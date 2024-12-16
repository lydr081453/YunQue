using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.HumanResource.Entity
{
    public class DimissionADDetailsInfo
    {
        public DimissionADDetailsInfo()
        { }
        #region Model
        private int _dimissionaddetailid;
        private int _dimissionid;
        private string _doorcard;
        private string _librarymanage;
        private int _principalid;
        private string _principalname;
        /// <summary>
        /// 编号
        /// </summary>
        public int DimissionADDetailId
        {
            set { _dimissionaddetailid = value; }
            get { return _dimissionaddetailid; }
        }
        /// <summary>
        /// 离职单编号
        /// </summary>
        public int DimissionId
        {
            set { _dimissionid = value; }
            get { return _dimissionid; }
        }
        /// <summary>
        /// 门卡号
        /// </summary>
        public string DoorCard
        {
            set { _doorcard = value; }
            get { return _doorcard; }
        }
        /// <summary>
        /// 图书管理描述信息
        /// </summary>
        public string LibraryManage
        {
            set { _librarymanage = value; }
            get { return _librarymanage; }
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
            if (r["DimissionADDetailId"].ToString() != "")
            {
                _dimissionaddetailid = int.Parse(r["DimissionADDetailId"].ToString());
            }
            if (r["DimissionId"].ToString() != "")
            {
                _dimissionid = int.Parse(r["DimissionId"].ToString());
            }
            _doorcard = r["DoorCard"].ToString();
            _librarymanage = r["LibraryManage"].ToString();
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
            if (r["DimissionADDetailId"].ToString() != "")
            {
                _dimissionaddetailid = int.Parse(r["DimissionADDetailId"].ToString());
            }
            if (r["DimissionId"].ToString() != "")
            {
                _dimissionid = int.Parse(r["DimissionId"].ToString());
            }
            _doorcard = r["DoorCard"].ToString();
            _librarymanage = r["LibraryManage"].ToString();
            if (r["PrincipalID"].ToString() != "")
            {
                _principalid = int.Parse(r["PrincipalID"].ToString());
            }
            _principalname = r["PrincipalName"].ToString();
        }
    }
}
