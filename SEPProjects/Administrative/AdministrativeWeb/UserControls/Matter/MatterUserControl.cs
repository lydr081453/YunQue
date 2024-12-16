using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ESP.Administrative.Entity;
using ESP.Administrative.BusinessLogic;
using System.Web.UI;
using ESP.Administrative.Common;

namespace AdministrativeWeb.UserControls.Matter
{
    public class MatterUserControl : ESP.Web.UI.UserControlBase
    {
        #region 页面属性
        protected static string _selectDateTime;

        /// <summary>
        /// 用户选择的日期
        /// </summary>
        public string SelectDateTime
        {
            get
            {
                return _selectDateTime;
            }
            set
            {
                _selectDateTime = value;
            }
        }

        /// <summary>
        /// 返回URL
        /// </summary>
        public string BackUrl
        {
            get
            {
                return this.ViewState["BackUrl"] as string;
            }
            set
            {
                this.ViewState["BackUrl"] = value;
            }
        }
        #endregion

        /// <summary>
        /// 获得一个审批记录对象
        /// </summary>
        /// <param name="id">审批记录ID</param>
        /// <param name="singelType">单据类型</param>
        /// <returns>审批记录对象</returns>
        protected ApproveLogInfo GetApproveLogModel(string id, int singelType)
        {
            ApproveLogInfo model = new ApproveLogInfo();
            if (!string.IsNullOrEmpty(id))
            {
                model = new ApproveLogManager().GetModel(int.Parse(id));
            }
            else
            {
                ESP.Administrative.BusinessLogic.OperationAuditManageManager operationAuditManager = new ESP.Administrative.BusinessLogic.OperationAuditManageManager();
                ESP.Administrative.Entity.OperationAuditManageInfo opearmodel = operationAuditManager.GetOperationAuditModelByUserID(UserInfo.UserID);
                if (opearmodel != null)
                {
                    if (singelType == (int)Status.MattersSingle.MattersSingle_OffTune || singelType == (int)Status.MattersSingle.MattersSingle_OTLate)
                    {
                        model.ApproveID = opearmodel.HRAdminID;
                        model.ApproveName = opearmodel.HRAdminName;
                    }
                    else
                    {
                        model.ApproveID = opearmodel.TeamLeaderID;
                        model.ApproveName = opearmodel.TeamLeaderName;
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), "alert('没有发现设置的审批人，请与IT部联系');", true);
                    return null;
                }
            }
            model.ApproveState = 0;
            model.ApproveType = singelType;
            model.ApproveUpUserID = 0;
            model.CreateTime = DateTime.Now;
            model.Deleted = false;
            model.IsLastApprove = 0;
            model.OperateorID = UserInfo.UserID;
            model.Sort = 0;
            model.UpdateTime = DateTime.Now;
            return model;
        }

        /// <summary>
        /// 是否可以提交考勤事由，不进行七天提交时间的限制
        /// </summary>
        /// <param name="mattersID">事由ID</param>
        /// <returns>如果允许提交返回true，否则返回false</returns>
        public bool IsCanSubmitMatters(string mattersID)
        {
            bool b = false;
            if (!string.IsNullOrEmpty(mattersID))
            {
                MattersInfo model = new MattersManager().GetModel(int.Parse(mattersID));
                if (model != null && (model.MatterState == Status.MattersState_Overrule || model.MatterState == Status.MattersState_Cancel))
                {
                    b = true;
                }
            }
            return b;
        }

        /// <summary>
        /// 是否可以提交OT单事由，不进行七天提交时间的限制
        /// </summary>
        /// <param name="overtimeID">OT单ID</param>
        /// <returns>如果允许提交返回True，否则返回false</returns>
        public bool IsCanSubmitOvertime(string overtimeID)
        {
            bool b = false;
            if (!string.IsNullOrEmpty(overtimeID))
            {
                SingleOvertimeInfo model = new SingleOvertimeManager().GetModel(int.Parse(overtimeID));
                if (model != null && (model.Approvestate == Status.OverTimeState_Overrule || model.Approvestate == Status.OverTimeState_Cancel))
                {
                    b = true;
                }
            }
            return b;
        }
    }
}
