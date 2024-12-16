using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.HumanResource.Entity
{
    public class DimissionIndemnityInfo
    {
        public DimissionIndemnityInfo()
        { }

        #region Model
        private int _dimissionindemnityid;
        private int _dimissionid;
        private string _indemnityitem;
        private decimal _indemnityamount;
        private string _indemnitydesc;
        private DateTime? _createtime;
        private DateTime? _updatetime;
        private int? _createuserid;
        /// <summary>
        /// 编号
        /// </summary>
        public int DimissionIndemnityId
        {
            set { _dimissionindemnityid = value; }
            get { return _dimissionindemnityid; }
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
        /// 赔款事项
        /// </summary>
        public string IndemnityItem
        {
            set { _indemnityitem = value; }
            get { return _indemnityitem; }
        }
        /// <summary>
        /// 赔款金额
        /// </summary>
        public decimal IndemnityAmount
        {
            set { _indemnityamount = value; }
            get { return _indemnityamount; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string IndemnityDesc
        {
            set { _indemnitydesc = value; }
            get { return _indemnitydesc; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? UpdateTime
        {
            set { _updatetime = value; }
            get { return _updatetime; }
        }
        /// <summary>
        /// 创建人
        /// </summary>
        public int? CreateUserid
        {
            set { _createuserid = value; }
            get { return _createuserid; }
        }
        #endregion Model


        /// <summary>
        /// 格式化数据对象
        /// </summary>
        /// <param name="r"></param>
        public void PopupData(System.Data.IDataReader r)
        {
            if (r["DimissionIndemnityId"].ToString() != "")
            {
                _dimissionindemnityid = int.Parse(r["DimissionIndemnityId"].ToString());
            }
            if (r["DimissionId"].ToString() != "")
            {
                _dimissionid = int.Parse(r["DimissionId"].ToString());
            }
            _indemnityitem = r["IndemnityItem"].ToString();
            if (r["IndemnityAmount"].ToString() != "")
            {
                _indemnityamount = decimal.Parse(r["IndemnityAmount"].ToString());
            }
            _indemnitydesc = r["IndemnityDesc"].ToString();
            if (r["CreateTime"].ToString() != "")
            {
                _createtime = DateTime.Parse(r["CreateTime"].ToString());
            }
            if (r["UpdateTime"].ToString() != "")
            {
                _updatetime = DateTime.Parse(r["UpdateTime"].ToString());
            }
            if (r["CreateUserid"].ToString() != "")
            {
                _createuserid = int.Parse(r["CreateUserid"].ToString());
            }
        }

        /// <summary>
        /// 格式化数据对象
        /// </summary>
        /// <param name="r"></param>
        public void PopupData(System.Data.DataRow r)
        {
            if (r["DimissionIndemnityId"].ToString() != "")
            {
                _dimissionindemnityid = int.Parse(r["DimissionIndemnityId"].ToString());
            }
            if (r["DimissionId"].ToString() != "")
            {
                _dimissionid = int.Parse(r["DimissionId"].ToString());
            }
            _indemnityitem = r["IndemnityItem"].ToString();
            if (r["IndemnityAmount"].ToString() != "")
            {
                _indemnityamount = decimal.Parse(r["IndemnityAmount"].ToString());
            }
            _indemnitydesc = r["IndemnityDesc"].ToString();
            if (r["CreateTime"].ToString() != "")
            {
                _createtime = DateTime.Parse(r["CreateTime"].ToString());
            }
            if (r["UpdateTime"].ToString() != "")
            {
                _updatetime = DateTime.Parse(r["UpdateTime"].ToString());
            }
            if (r["CreateUserid"].ToString() != "")
            {
                _createuserid = int.Parse(r["CreateUserid"].ToString());
            }
        }
    }
}
