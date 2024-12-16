using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ESP.HumanResource.BusinessLogic
{
    public class HRAuditLogManager
    {
        private static readonly ESP.HumanResource.DataAccess.HRAuditLogProvider dal = new ESP.HumanResource.DataAccess.HRAuditLogProvider();
        public HRAuditLogManager()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public static bool Exists(int AuditLogId)
		{
			return dal.Exists(AuditLogId);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
        public static int Add(ESP.HumanResource.Entity.HRAuditLogInfo model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
        public static void Update(ESP.HumanResource.Entity.HRAuditLogInfo model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public static void Delete(int AuditLogId)
		{
			dal.Delete(AuditLogId);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        public static ESP.HumanResource.Entity.HRAuditLogInfo GetModel(int AuditLogId)
		{
			return dal.GetModel(AuditLogId);
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public static DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public static DataSet GetAllList()
		{
			return dal.GetList("");
		}

        /// <summary>
        /// 获得审批单据审批记录信息
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="formId">单据编号</param>
        /// <param name="formType">单据类型</param>
        /// <param name="auditStatus">审批状态</param>
        /// <returns></returns>
        public static ESP.HumanResource.Entity.HRAuditLogInfo GetAuditLogInfo(int userId, int formId, int formType, int auditStatus)
        {
            return dal.GetAuditLogInfo(userId, formId, formType, auditStatus);
        }

        /// <summary>
        /// 获得审批单据审批记录信息
        /// </summary>
        /// <param name="formId">单据编号</param>
        /// <param name="formType">单据类型</param>
        /// <param name="auditStatus">审批状态</param>
        /// <returns></returns>
        public static List<ESP.HumanResource.Entity.HRAuditLogInfo> GetAuditLogInfo(int formId, int formType, int auditStatus)
        {
            return dal.GetAuditLogInfo(formId, formType, auditStatus);
        }

        /// <summary>
        /// 获得审批单据审批记录信息
        /// </summary>
        /// <param name="formId">单据编号</param>
        /// <param name="formType">单据类型</param>
        /// <returns></returns>
        public static List<ESP.HumanResource.Entity.HRAuditLogInfo> GetAuditLogInfo(int formId, int formType)
        {
            return dal.GetAuditLogInfo(formId, formType);
        }

        /// <summary>
        /// 获得审批日志信息集合
        /// </summary>
        /// <param name="formId">单据编号</param>
        /// <param name="formType">单据类型</param>
        /// <returns>返回审批日志信息集合</returns>
        public static List<ESP.HumanResource.Entity.HRAuditLogInfo> GetAuditLogInfos(int formId, int formType)
        {
            return dal.GetAuditLogInfos(formId, formType);
        }

        /// <summary>
        /// 获得审批日志信息字符串
        /// </summary>
        /// <param name="formId">单据编号（离职单、入职单等）</param>
        /// <param name="formType">单据类型（离职单、入职单）</param>
        /// <returns>返回审批日志信息集合</returns>
        public static string GetStrAuditLogInfos(int formId, int formType)
        {
            List<ESP.HumanResource.Entity.HRAuditLogInfo> AuditLogList = GetAuditLogInfos(formId, formType);
            
            StringBuilder groupBulider = new StringBuilder();  // 团队审批日志
            StringBuilder hrITBulider = new StringBuilder();  // 集团人力、IT审批日志
            StringBuilder financeBulider = new StringBuilder();  // 财务审批日志
            StringBuilder hrADBulider = new StringBuilder();  // 集团行政、人力审批日志
            StringBuilder tipBulider = new StringBuilder();   //message

            if (AuditLogList != null && AuditLogList.Count > 0)
            {
                foreach (ESP.HumanResource.Entity.HRAuditLogInfo hrAuditLogModel in AuditLogList)
                {
                    StringBuilder strBulider = new StringBuilder();
                    string strStatus = "审批通过";
                    if (hrAuditLogModel.AuditStatus == (int)ESP.HumanResource.Common.AuditStatus.Overrule)
                    {
                        strStatus = "审批驳回";
                    }
                    else if (hrAuditLogModel.AuditStatus == (int)ESP.HumanResource.Common.AuditStatus.Audited)
                    {
                        strStatus = "审批通过";
                    }
                    else
                    {
                        strStatus = "";
                    }
                    strBulider.Append(hrAuditLogModel.AuditDate.Value.ToString("yyyy-MM-dd HH:mm") + hrAuditLogModel.AuditorName + strStatus + "：" + hrAuditLogModel.Requesition + "<br />");

                    switch (hrAuditLogModel.AuditLevel)
                    {
                        case (int)ESP.HumanResource.Common.DimissionFormStatus.WaitPreAuditor:
                            if (string.IsNullOrEmpty(groupBulider.ToString()))
                                groupBulider.Append("团队审批：<br />");
                            groupBulider.Append("&nbsp;&nbsp;&nbsp;&nbsp;" + strBulider.ToString());
                            break;
                        case (int)ESP.HumanResource.Common.DimissionFormStatus.WaitDirector:
                            if (string.IsNullOrEmpty(groupBulider.ToString()))
                                groupBulider.Append("团队审批：<br />");
                            groupBulider.Append("&nbsp;&nbsp;&nbsp;&nbsp;" + strBulider.ToString());
                            break;
                        case (int)ESP.HumanResource.Common.DimissionFormStatus.WaitManager:
                            if (string.IsNullOrEmpty(groupBulider.ToString()))
                                groupBulider.Append("团队审批：<br />");
                            groupBulider.Append("&nbsp;&nbsp;&nbsp;&nbsp;" + strBulider.ToString());
                            break;
                        case (int)ESP.HumanResource.Common.DimissionFormStatus.WaitGroupHR:
                            if (string.IsNullOrEmpty(groupBulider.ToString()))
                                groupBulider.Append("团队审批：<br />");
                            groupBulider.Append("&nbsp;&nbsp;&nbsp;&nbsp;" + strBulider.ToString());
                            break;
                        case (int)ESP.HumanResource.Common.DimissionFormStatus.WaitHRIT:
                            if (string.IsNullOrEmpty(hrITBulider.ToString()))
                                hrITBulider.Append("集团人力、IT审批：<br />");
                            hrITBulider.Append("&nbsp;&nbsp;&nbsp;&nbsp;" + strBulider.ToString());
                            break;
                        case (int)ESP.HumanResource.Common.DimissionFormStatus.WaitFinance:
                            if (string.IsNullOrEmpty(financeBulider.ToString()))
                                financeBulider.Append("财务审批：<br />");
                            financeBulider.Append("&nbsp;&nbsp;&nbsp;&nbsp;" + strBulider.ToString());
                            break;
                        case (int)ESP.HumanResource.Common.DimissionFormStatus.WaitAdministration:
                            if (string.IsNullOrEmpty(hrADBulider.ToString()))
                                hrADBulider.Append("集团行政、人力审批：<br />");
                            hrADBulider.Append("&nbsp;&nbsp;&nbsp;&nbsp;" + strBulider.ToString());
                            break;
                        case (int)ESP.HumanResource.Common.DimissionFormStatus.WaitHR2:
                            if (string.IsNullOrEmpty(hrADBulider.ToString()))
                                hrADBulider.Append("集团行政、人力审批：<br />");
                            hrADBulider.Append("&nbsp;&nbsp;&nbsp;&nbsp;" + strBulider.ToString());
                            break;
                        case (int)ESP.HumanResource.Common.DimissionFormStatus.WaitHRDirector:
                            if (string.IsNullOrEmpty(hrITBulider.ToString()))
                                hrITBulider.Append("集团人力总监审批：<br />");
                            hrITBulider.Append("&nbsp;&nbsp;&nbsp;&nbsp;" + strBulider.ToString());
                            break;
                        case (int)ESP.HumanResource.Common.DimissionFormStatus.DimissionTip:
                            if (string.IsNullOrEmpty(tipBulider.ToString()))
                                tipBulider.Append("留言：<br />");
                            tipBulider.Append("&nbsp;&nbsp;&nbsp;&nbsp;" + strBulider.ToString());
                            break;
                    }
                }
            }
            return groupBulider.ToString() + hrITBulider.ToString() + financeBulider.ToString() + hrADBulider.ToString()+tipBulider;
        }

        public static string GetTransferLogInfos(int formId, int formType)
        {
            List<ESP.HumanResource.Entity.HRAuditLogInfo> AuditLogList = GetAuditLogInfos(formId, formType);

            StringBuilder strBulider = new StringBuilder();

            if (AuditLogList != null && AuditLogList.Count > 0)
            {
                foreach (ESP.HumanResource.Entity.HRAuditLogInfo hrAuditLogModel in AuditLogList)
                {
                    
                    string strStatus = "审批通过";
                    if (hrAuditLogModel.AuditStatus == (int)ESP.HumanResource.Common.AuditStatus.Overrule)
                    {
                        strStatus = "审批驳回";
                    }
                    else if (hrAuditLogModel.AuditStatus == (int)ESP.HumanResource.Common.AuditStatus.Audited)
                    {
                        strStatus = "审批通过";
                    }
                    else
                    {
                        strStatus = "";
                    }
                    strBulider.Append(hrAuditLogModel.AuditDate.Value.ToString("yyyy-MM-dd HH:mm") + hrAuditLogModel.AuditorName + strStatus + "：" + hrAuditLogModel.Requesition + "<br />");
                }
            }
            return strBulider.ToString();
        }

		#endregion  成员方法
    }
}
