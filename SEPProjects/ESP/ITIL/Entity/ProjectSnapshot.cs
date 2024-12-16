using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.ITIL.Entity
{
    /// <summary>
    /// 项目完整信息快照
    /// </summary>
    [Serializable]
    public class ProjectSnapshot : MarshalByRefObject
    {
        #region Base Info
        /// <summary>
        /// 项目基础信息
        /// </summary>
        public ProjectBaseInfoSnapshot BaseInfo { get; set; }
         /// <summary>
        /// PR单列表
        /// </summary>
        public IList<PrSnapshot> PrList { get; set; }
        /// <summary>
        /// 全部工作项列表
        /// </summary>
        public IList<WorkFlow.Model.WORKITEMS> WorkItemList { get; set; }
        #endregion

        #region 分类列表
        /// <summary>
        /// 对公PR单列表
        /// </summary>
        public IEnumerable<PrSnapshot> GetPublicList
        {
            get
            {
                var list = from pr in PrList where pr.BusinessType == ESP.ITIL.Common.Enum.PrBusinessType.Public orderby pr.BaseInfo.requisition_committime select pr;
                return list;
            }
        }
        /// <summary>
        /// 对私PR单列表
        /// </summary>
        public IEnumerable<PrSnapshot> GetPrivateList
        {
            get
            {
                var list = from pr in PrList where pr.BusinessType == ESP.ITIL.Common.Enum.PrBusinessType.Private orderby pr.BaseInfo.requisition_committime select pr;
                return list;
            }
        }
        /// <summary>
        /// 媒体稿件费用PR单列表
        /// </summary>
        public IEnumerable<PrSnapshot> GetMediaFeeList
        {
            get
            {
                var list = from pr in PrList where pr.BusinessType == ESP.ITIL.Common.Enum.PrBusinessType.MediaFee orderby pr.BaseInfo.requisition_committime select pr;
                return list;
            }
        }
        /// <summary>
        /// 对公有押金PR单列表
        /// </summary>
        public IEnumerable<PrSnapshot> GetPublicWithDepositList
        {
            get
            {
                var list = from pr in PrList where pr.BusinessType == ESP.ITIL.Common.Enum.PrBusinessType.PublicWithDeposit orderby pr.BaseInfo.requisition_committime select pr;
                return list;
            }
        }
        /// <summary>
        /// 对私有借款PR单列表
        /// </summary>
        public IEnumerable<PrSnapshot> GetPrivateWithBorrowingList
        {
            get
            {
                var list = from pr in PrList where pr.BusinessType == ESP.ITIL.Common.Enum.PrBusinessType.PrivateWithBorrowing orderby pr.BaseInfo.requisition_committime select pr;
                return list;
            }
        }
        /// <summary>
        /// 项目审批工作项列表
        /// </summary>
        public IEnumerable<WorkFlow.Model.WORKITEMS> ProjectWorkItemList
        {
            get
            {
                return null;
            }
        }
        /// <summary>
        /// Pr审批工作项列表
        /// </summary>
        public IEnumerable<WorkFlow.Model.WORKITEMS> PrWorkItemList
        {
            get
            {
                return null;
            }
        }
        #endregion
    }
}
