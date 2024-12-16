using ESP.Finance.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Purchase.Entity;
using ESP.Finance.BusinessLogic;
using ESP.Purchase.BusinessLogic;

namespace ESP.Administrative.Entity
{
    public partial class RequestForSealInfo
    {
        public int Id { get; set; }

        public int BranchId { get; set; }

        /// <summary>
        /// 相关数据单号
        /// </summary>
        public string DataNum { get; set; }

        public int RequestorId {get;set;}

        public string RequestorName { get; set; }

        public int DepartmentId { get; set; }

        /// <summary>
        /// 用印时间
        /// </summary>
        public DateTime RequestDate { get; set; }

        /// <summary>
        /// 印章类型
        /// </summary>
        public string SealType { get; set; }

        /// <summary>
        /// 文件类型
        /// </summary>
        public string FileType {get;set;}

        /// <summary>
        /// 文件数量
        /// </summary>
        public int FileQuantity { get; set; }

        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 上传文件，多文件#号分隔
        /// </summary>
        public string Files { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public ESP.Administrative.Common.Status.RequestForSealStatus Status { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// 待审批人
        /// </summary>
        public int AuditorId { get; set; }

        public string AuditorName { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public string SANo { get; set; }
    }

    public partial class RequestForSealInfo
    {
        public string BranchName { get; set; }

        public int DeptId1 { get; set; }

        public string DeptName1 { get; set; }

        public int DeptId2 { get; set; }

        public string DeptName2 { get; set; }

        public int DeptId3 { get; set; }

        public string DeptName3 { get; set; }

        public ProjectInfo Project
        {
            get
            {
                if (!string.IsNullOrEmpty(DataNum))
                {
                    return ProjectManager.GetModelByProjectCode(DataNum);
                }
                return null;
            }
        }

        public GeneralInfo PR
        {
            get
            {
                if (!string.IsNullOrEmpty(DataNum))
                {
                    var gList = GeneralInfoManager.GetModelList(" prNo=" + DataNum, null);
                    if (gList != null && gList.Count > 0)
                        return gList[0];
                }
                return null;
            }
        }

        public int AuditStatus { get; set; }
    }
}
