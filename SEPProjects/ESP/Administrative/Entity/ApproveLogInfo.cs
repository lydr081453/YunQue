using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Administrative.Entity
{
    public class ApproveLogInfo : BaseEntityInfo
    {
        public ApproveLogInfo()
        { }
        #region Model
        private int _id;
        private int _approveid;
        private string _approvename;
        private int _approvetype;
        private int _approvedateid;
        private int _approvestate;
        private int _approveupuserid;
        private int _islastapprove;
        private string _approveremark;

        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 审批人ID
        /// </summary>
        public int ApproveID
        {
            set { _approveid = value; }
            get { return _approveid; }
        }
        /// <summary>
        /// 审批人姓名
        /// </summary>
        public string ApproveName
        {
            set { _approvename = value; }
            get { return _approvename; }
        }
        /// <summary>
        /// 审批数据类型  1.考勤记录,2.请假单,3OT单
        /// </summary>
        public int ApproveType
        {
            set { _approvetype = value; }
            get { return _approvetype; }
        }
        /// <summary>
        /// 审批数据ID
        /// </summary>
        public int ApproveDateID
        {
            set { _approvedateid = value; }
            get { return _approvedateid; }
        }
        /// <summary>
        /// 审批状态 1.审批通过，2.审批驳回  数据默认为0
        /// </summary>
        public int ApproveState
        {
            set { _approvestate = value; }
            get { return _approvestate; }
        }
        /// <summary>
        /// 上级审批人ID
        /// </summary>
        public int ApproveUpUserID
        {
            set { _approveupuserid = value; }
            get { return _approveupuserid; }
        }
        /// <summary>
        /// 是否最后一级审批 0,否，1.是
        /// </summary>
        public int IsLastApprove
        {
            set { _islastapprove = value; }
            get { return _islastapprove; }
        }
        /// <summary>
        /// 审批备注
        /// </summary>
        public string Approveremark
        {
            get { return _approveremark; }
            set { _approveremark = value; }
        }
        #endregion Model

        /// <summary>
        /// 格式化数据对象
        /// </summary>
        /// <param name="r"></param>
        public void PopupData(System.Data.IDataReader r)
        {
            if (r["ID"].ToString() != "")
            {
                _id = int.Parse(r["ID"].ToString());
            }
            if (r["ApproveID"].ToString() != "")
            {
                _approveid = int.Parse(r["ApproveID"].ToString());
            }
            _approvename = r["ApproveName"].ToString();
            _approveremark = r["ApproveRemark"].ToString();
            if (r["ApproveType"].ToString() != "")
            {
                _approvetype = int.Parse(r["ApproveType"].ToString());
            }
            if (r["ApproveDateID"].ToString() != "")
            {
                _approvedateid = int.Parse(r["ApproveDateID"].ToString());
            }
            if (r["ApproveState"].ToString() != "")
            {
                _approvestate = int.Parse(r["ApproveState"].ToString());
            }
            if (r["ApproveUpUserID"].ToString() != "")
            {
                _approveupuserid = int.Parse(r["ApproveUpUserID"].ToString());
            }
            if (r["IsLastApprove"].ToString() != "")
            {
                _islastapprove = int.Parse(r["IsLastApprove"].ToString());
            }
            if (r["Deleted"].ToString() != "")
            {
                if ((r["Deleted"].ToString() == "1") || (r["Deleted"].ToString().ToLower() == "true"))
                {
                    Deleted = true;
                }
                else
                {
                    Deleted = false;
                }
            }
            var objCreateTime = r["CreateTime"];
            if (!(objCreateTime is DBNull))
            {
                CreateTime = (DateTime)objCreateTime;
            }
            var objUpdateTime = r["UpdateTime"];
            if (!(objUpdateTime is DBNull))
            {
                UpdateTime = (DateTime)objUpdateTime;
            }
            if (r["OperateorID"].ToString() != "")
            {
                OperateorID = int.Parse(r["OperateorID"].ToString());
            }
        }
        
        /// <summary>
        /// 格式化数据对象
        /// </summary>
        /// <param name="r"></param>
        public void PopupData(System.Data.DataRow r)
        {
            if (r["ID"].ToString() != "")
            {
                _id = int.Parse(r["ID"].ToString());
            }
            if (r["ApproveID"].ToString() != "")
            {
                _approveid = int.Parse(r["ApproveID"].ToString());
            }
            _approvename = r["ApproveName"].ToString();
            _approveremark = r["ApproveRemark"].ToString();
            if (r["ApproveType"].ToString() != "")
            {
                _approvetype = int.Parse(r["ApproveType"].ToString());
            }
            if (r["ApproveDateID"].ToString() != "")
            {
                _approvedateid = int.Parse(r["ApproveDateID"].ToString());
            }
            if (r["ApproveState"].ToString() != "")
            {
                _approvestate = int.Parse(r["ApproveState"].ToString());
            }
            if (r["ApproveUpUserID"].ToString() != "")
            {
                _approveupuserid = int.Parse(r["ApproveUpUserID"].ToString());
            }
            if (r["IsLastApprove"].ToString() != "")
            {
                _islastapprove = int.Parse(r["IsLastApprove"].ToString());
            }
            if (r["Deleted"].ToString() != "")
            {
                if ((r["Deleted"].ToString() == "1") || (r["Deleted"].ToString().ToLower() == "true"))
                {
                    Deleted = true;
                }
                else
                {
                    Deleted = false;
                }
            }
            var objCreateTime = r["CreateTime"];
            if (!(objCreateTime is DBNull))
            {
                CreateTime = (DateTime)objCreateTime;
            }
            var objUpdateTime = r["UpdateTime"];
            if (!(objUpdateTime is DBNull))
            {
                UpdateTime = (DateTime)objUpdateTime;
            }
            if (r["OperateorID"].ToString() != "")
            {
                OperateorID = int.Parse(r["OperateorID"].ToString());
            }
        }
    }
}