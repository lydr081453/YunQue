using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace ESP.HumanResource.Entity
{
    public class SalaryDetailInfo
    {
        public SalaryDetailInfo()
        { }
        #region Model
        private int _id;
        private int _creater;
        private string _creatername;
        private DateTime _createdate = DateTime.Parse("1900-1-1 0:00:00");
        private int _sysid;
        private string _sysusername;
        private decimal _nowbasepay;
        private decimal _nowmeritpay;
        private decimal _newbasepay;
        private decimal _newmeritpay;
        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 创建人
        /// </summary>
        public int creater
        {
            set { _creater = value; }
            get { return _creater; }
        }
        /// <summary>
        /// 创建人姓名
        /// </summary>
        public string createrName
        {
            set { _creatername = value; }
            get { return _creatername; }
        }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime createDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 员工编号
        /// </summary>
        public int sysid
        {
            set { _sysid = value; }
            get { return _sysid; }
        }
        /// <summary>
        /// 员工姓名
        /// </summary>
        public string sysUserName
        {
            set { _sysusername = value; }
            get { return _sysusername; }
        }
        /// <summary>
        /// 原月薪 (总)
        /// </summary>
        public decimal nowBasePay
        {
            set { _nowbasepay = value; }
            get { return _nowbasepay; }
        }
        /// <summary>
        /// 原绩效工资
        /// </summary>
        public decimal nowMeritPay
        {
            set { _nowmeritpay = value; }
            get { return _nowmeritpay; }
        }
        /// <summary>
        /// 调整月薪(总)
        /// </summary>
        public decimal newBasePay
        {
            set { _newbasepay = value; }
            get { return _newbasepay; }
        }
        /// <summary>
        /// 新绩效工资
        /// </summary>
        public decimal newMeritPay
        {
            set { _newmeritpay = value; }
            get { return _newmeritpay; }
        }

        /// <summary>
        /// 状态
        /// </summary>
        /// <param name="r"></param>
        private int _status;
        public int status
        {
            set { _status = value; }
            get { return _status; }
        }
        #endregion Model

        public void PopupData(IDataReader r)
        {
            if (r["id"].ToString() != "")
            {
                _id = int.Parse(r["id"].ToString());
            }
            if (r["creater"].ToString() != "")
            {
                _creater = int.Parse(r["creater"].ToString());
            }
            _creatername = r["createrName"].ToString();
            if (r["createDate"].ToString() != "")
            {
                _createdate = DateTime.Parse(r["createDate"].ToString());
            }
            if (r["sysid"].ToString() != "")
            {
                _sysid = int.Parse(r["sysid"].ToString());
            }
            _sysusername = r["sysUserName"].ToString();
            if (r["nowBasePay"].ToString() != "")
            {
                _nowbasepay = decimal.Parse(r["nowBasePay"].ToString());
            }
            if (r["nowMeritPay"].ToString() != "")
            {
                _nowmeritpay = decimal.Parse(r["nowMeritPay"].ToString());
            }
            if (r["newBasePay"].ToString() != "")
            {
                _newbasepay = decimal.Parse(r["newBasePay"].ToString());
            }
            if (r["newMeritPay"].ToString() != "")
            {
                _newmeritpay = decimal.Parse(r["newMeritPay"].ToString());
            }
            if (r["status"].ToString() != "")
            {
                _status = int.Parse(r["status"].ToString());
            }
        }
    }
}
