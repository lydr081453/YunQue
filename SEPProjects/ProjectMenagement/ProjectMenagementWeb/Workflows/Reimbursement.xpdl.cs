using ESP.Workflow;
using System;
using System.Collections.Generic;
using ESP.Workflow.DataAccess;

public partial class Reimbursement : WorkflowProcess
{
    /************************************************************************\
     * 审批事件
     *      通过(收货完成)：      Approved
     *      驳回(放弃收货)：      Rejected
     *      发回重审：            Returned
     * 
     * 各步骤名称（URL参数 step=xx）
     *      预审：               pa
     *      项目负责人：         pm
     *      FA：                 fa
     *      总经理：             gm
     *      CEO：                ceo            
     *      总监：               mj
     *      财务第一步：         f1
     *      财务第二步：         f2
     *      财务第三步：         f3
     *      财务总监（Eddy）：   fm
     *      
     * 是否是GM项目费用（URL参数 gm=x）
     *      0： 非GM项目费用
     *      1： GM项目费用
     *      
     * 
     * 
    \************************************************************************/
    protected override void Initialize()
    {
    }

    /// <summary>
    /// 表单数据实体对象 Id （即报销申请单的 Id）
    /// </summary>
    [ESP.Workflow.WorkflowDataFeild]
    private int EntityId { get; set; }


    /// <summary>
    /// 总监 Id (列表)
    /// </summary>
    [ESP.Workflow.WorkflowDataFeild(true)]
    private int[] MajorId { get; set; }

    /// <summary>
    /// 总经理 Id (列表)
    /// </summary>
    [ESP.Workflow.WorkflowDataFeild(true)]
    private int[] GeneralManagerId { get; set; }

    /// <summary>
    /// 项目负责人 Id (列表)
    /// </summary>
    [ESP.Workflow.WorkflowDataFeild(true)]
    private int[] PorjectManagerId { get; set; }

    /// <summary>
    /// 财务助理（FA） Id (列表)
    /// </summary>
    [ESP.Workflow.WorkflowDataFeild(true)]
    private int[] FAId { get; set; }

    /// <summary>
    /// 财务第一级审批人 Id (列表)
    /// </summary>
    [ESP.Workflow.WorkflowDataFeild(true)]
    private int[] Finance1Id { get; set; }

    /// <summary>
    /// 财务第二级审批人 Id (列表)
    /// </summary>
    [ESP.Workflow.WorkflowDataFeild(true)]
    private int[] Finance2Id { get; set; }

    /// <summary>
    /// 财务第三级审批人 Id (列表)
    /// </summary>
    [ESP.Workflow.WorkflowDataFeild(true)]
    private int[] Finance3Id { get; set; }

    [ESP.Workflow.WorkflowDataFeild(true)]
    private int[] Finance4Id { get; set; }

    /// <summary>
    /// CEO Id (列表)
    /// </summary>
    [ESP.Workflow.WorkflowDataFeild(true)]
    private int[] CEOId { get; set; }

    /// <summary>
    /// 财务总监（Eddy） Id (列表)
    /// </summary>
    [ESP.Workflow.WorkflowDataFeild(true)]
    private int[] FinanceMajorId { get; set; }

    /// <summary>
    /// 分公司财务（Rosa） Id (列表)
    /// </summary>
    [ESP.Workflow.WorkflowDataFeild(true)]
    private int[] FilialeFinanceId { get; set; }

    /// <summary>
    /// 预审人 Id (列表)
    /// </summary>
    [ESP.Workflow.WorkflowDataFeild(true)]
    private int[] PreApproverId { get; set; }


    /// <summary>
    /// 是否使用GM项目费用
    /// </summary>
    [ESP.Workflow.WorkflowDataFeild]
    private bool IsGM { get; set; }

    /// <summary>
    /// （非GM费用）提交人是否是项目负责人
    /// </summary>
    [ESP.Workflow.WorkflowDataFeild]
    private bool IsProjectManager { get; set; }

    /// <summary>
    /// （非GM费用）提交人是否是总监
    /// </summary>
    [ESP.Workflow.WorkflowDataFeild]
    private bool IsMajor { get; set; }

    /// <summary>
    /// （非GM费用）总金额是否大于 10000
    /// </summary>
    [ESP.Workflow.WorkflowDataFeild]
    private bool IsGT_10000 { get; set; }

    /// <summary>
    /// （非GM费用）总金额是否大于 50000
    /// </summary>
    [ESP.Workflow.WorkflowDataFeild]
    private bool IsGT_50000 { get; set; }

    /// <summary>
    /// （非GM费用）是否是CEO (Hover) 提交。
    /// </summary>
    [ESP.Workflow.WorkflowDataFeild]
    private bool IsHover { get; set; }

    /// <summary>
    /// （非GM费用）是否是总经理级别提交。
    /// </summary>
    [ESP.Workflow.WorkflowDataFeild]
    private bool IsGeneralManager { get; set; }

    /// <summary>
    /// （非GM费用）单笔餐费否大于 2000
    /// </summary>
    [ESP.Workflow.WorkflowDataFeild]
    private bool IsGT_2000 { get; set; }

    /// <summary>
    /// （GM费用）是否是上每财务人员提交
    /// </summary>
    [ESP.Workflow.WorkflowDataFeild]
    private bool IsFinanceShanghai { get; set; }

    /// <summary>
    /// （GM费用）是否是广州或重庆财务人员提交
    /// </summary>
    [ESP.Workflow.WorkflowDataFeild]
    private bool IsFinanceChongqing { get; set; }

    /// <summary>
    /// （GM费用）是否是北京财务人员提交
    /// </summary>
    [ESP.Workflow.WorkflowDataFeild]
    private bool IsFinanceBeijing { get; set; }

    /// <summary>
    /// （GM费用）是否是财务总监（Eddy）提交
    /// </summary>
    [ESP.Workflow.WorkflowDataFeild]
    private bool IsFinanceMajor { get; set; }

    /// <summary>
    /// （GM费用）是否是行政人员提交
    /// </summary>
    [ESP.Workflow.WorkflowDataFeild]
    private bool IsAdministration { get; set; }

    [ESP.Workflow.WorkflowDataFeild]
    private int ReturnType { get; set; }

    [ESP.Workflow.WorkflowDataFeild]
    private bool IsFinance1W { get; set; }

    [ESP.Workflow.WorkflowDataFeild]
    private bool IsBeijingDept { get; set; }


}



public partial class Loan : WorkflowProcess
{
    /************************************************************************\
     * 审批事件
     *      通过：      Approved
     *      驳回：      Rejected
     *      发回重审：  Returned
     * 
     * 各步骤名称（URL参数 step=xx）
     *      预审：               pa
     *      项目负责人：         pm
     *      FA：                 fa
     *      总经理：             gm
     *      CEO：                ceo            
     *      总监：               mj
     *      财务第一步：         f1
     *      财务第二步：         f2
     *      财务第三步：         f3
     *      财务总监（Eddy）：   fm
     *      附加收货：           ra
     *      收货：               rv
     *      
     * 是否是GM项目费用（URL参数 gm=x）
     *      0： 非GM项目费用
     *      1： GM项目费用
     *      
     * 
     * 
    \************************************************************************/
    protected override void Initialize()
    {
    }

    /// <summary>
    /// 表单数据实体对象 Id （即报销申请单的 Id）
    /// </summary>
    [ESP.Workflow.WorkflowDataFeild]
    private int EntityId { get; set; }


    /// <summary>
    /// 总监 Id (列表)
    /// </summary>
    [ESP.Workflow.WorkflowDataFeild(true)]
    private int[] MajorId { get; set; }

    /// <summary>
    /// 总经理 Id (列表)
    /// </summary>
    [ESP.Workflow.WorkflowDataFeild(true)]
    private int[] GeneralManagerId { get; set; }

    /// <summary>
    /// 项目负责人 Id (列表)
    /// </summary>
    [ESP.Workflow.WorkflowDataFeild(true)]
    private int[] PorjectManagerId { get; set; }

    /// <summary>
    /// 财务助理（FA） Id (列表)
    /// </summary>
    [ESP.Workflow.WorkflowDataFeild(true)]
    private int[] FAId { get; set; }

    /// <summary>
    /// 财务第一级审批人 Id (列表)
    /// </summary>
    [ESP.Workflow.WorkflowDataFeild(true)]
    private int[] Finance1Id { get; set; }

    /// <summary>
    /// 财务第二级审批人 Id (列表)
    /// </summary>
    [ESP.Workflow.WorkflowDataFeild(true)]
    private int[] Finance2Id { get; set; }

    /// <summary>
    /// 财务第三级审批人 Id (列表)
    /// </summary>
    [ESP.Workflow.WorkflowDataFeild(true)]
    private int[] Finance3Id { get; set; }

    [ESP.Workflow.WorkflowDataFeild(true)]
    private int[] Finance4Id { get; set; }

    /// <summary>
    /// CEO Id (列表)
    /// </summary>
    [ESP.Workflow.WorkflowDataFeild(true)]
    private int[] CEOId { get; set; }

    /// <summary>
    /// 财务总监（Eddy） Id (列表)
    /// </summary>
    [ESP.Workflow.WorkflowDataFeild(true)]
    private int[] FinanceMajorId { get; set; }

    /// <summary>
    /// 分公司财务（Rosa） Id (列表)
    /// </summary>
    [ESP.Workflow.WorkflowDataFeild(true)]
    private int[] FilialeFinanceId { get; set; }

    /// <summary>
    /// 预审人 Id (列表)
    /// </summary>
    [ESP.Workflow.WorkflowDataFeild(true)]
    private int[] PreApproverId { get; set; }

    /// <summary>
    /// 附加收货人 Id (列表)
    /// </summary>
    [ESP.Workflow.WorkflowDataFeild(true)]
    private int[] ReceiveAffirmantId { get; set; }


    /// <summary>
    /// 是否使用GM项目费用
    /// </summary>
    [ESP.Workflow.WorkflowDataFeild]
    private bool IsGM { get; set; }

    /// <summary>
    /// （非GM费用）提交人是否是项目负责人
    /// </summary>
    [ESP.Workflow.WorkflowDataFeild]
    private bool IsProjectManager { get; set; }

    /// <summary>
    /// （非GM费用）提交人是否是总监
    /// </summary>
    [ESP.Workflow.WorkflowDataFeild]
    private bool IsMajor { get; set; }

    /// <summary>
    /// （非GM费用）总金额是否大于 10000
    /// </summary>
    [ESP.Workflow.WorkflowDataFeild]
    private bool IsGT_10000 { get; set; }

    /// <summary>
    /// （非GM费用）总金额是否大于 50000
    /// </summary>
    [ESP.Workflow.WorkflowDataFeild]
    private bool IsGT_50000 { get; set; }

    /// <summary>
    /// （非GM费用）是否是CEO (Hover) 提交。
    /// </summary>
    [ESP.Workflow.WorkflowDataFeild]
    private bool IsHover { get; set; }

    /// <summary>
    /// （非GM费用）是否是总经理级别提交。
    /// </summary>
    [ESP.Workflow.WorkflowDataFeild]
    private bool IsGeneralManager { get; set; }

    /// <summary>
    /// （非GM费用）单笔餐费否大于 2000
    /// </summary>
    [ESP.Workflow.WorkflowDataFeild]
    private bool IsGT_2000 { get; set; }

    /// <summary>
    /// （GM费用）是否是上每财务人员提交
    /// </summary>
    [ESP.Workflow.WorkflowDataFeild]
    private bool IsFinanceShanghai { get; set; }

    /// <summary>
    /// （GM费用）是否是重庆财务人员提交
    /// </summary>
    [ESP.Workflow.WorkflowDataFeild]
    private bool IsFinanceChongqing { get; set; }

    /// <summary>
    /// （GM费用）是否是北京财务人员提交
    /// </summary>
    [ESP.Workflow.WorkflowDataFeild]
    private bool IsFinanceBeijing { get; set; }

    /// <summary>
    /// （GM费用）是否是财务总监（Eddy）提交
    /// </summary>
    [ESP.Workflow.WorkflowDataFeild]
    private bool IsFinanceMajor { get; set; }

    ///// <summary>
    ///// 是否是现金借款（否则为 现场采买）
    ///// </summary>
    //[ESP.Workflow.WorkflowDataFeild]
    //private bool IsCashLoan { get; set; }


    /// <summary>
    /// 是否已经收货（工作流内部控制，无需传入该参数）
    /// </summary>
    [ESP.Workflow.WorkflowDataFeild]
    private bool HasReceived { get; set; }

    /// <summary>
    /// （GM费用）是否是行政人员提交
    /// </summary>
    [ESP.Workflow.WorkflowDataFeild]
    private bool IsAdministration { get; set; }

    [ESP.Workflow.WorkflowDataFeild]
    private int ReturnType { get; set; }

    [ESP.Workflow.WorkflowDataFeild]
    private bool IsFinance1W { get; set; }


    [ESP.Workflow.WorkflowDataFeild]
    private bool IsBeijingDept { get; set; }
}



public partial class Charge : WorkflowProcess
{
    /************************************************************************\
     * 审批事件
     *      通过：      Approved
     *      驳回：      Rejected
     *      发回重审：  Returned
     * 
     * 各步骤名称（URL参数 step=xx）
     *      预审：               pa
     *      项目负责人：         pm
     *      FA：                 fa
     *      总经理：             gm
     *      CEO：                ceo            
     *      总监：               mj
     *      财务第一步：         f1
     *      财务第二步：         f2
     *      财务第三步：         f3
     *      财务总监（Eddy）：   fm
     *      附加收货：           ra
     *      收货：               rv
     *      
     * 是否是GM项目费用（URL参数 gm=x）
     *      0： 非GM项目费用
     *      1： GM项目费用
     *      
     * 
     * 
    \************************************************************************/
    protected override void Initialize()
    {
    }

    /// <summary>
    /// 表单数据实体对象 Id （即报销申请单的 Id）
    /// </summary>
    [ESP.Workflow.WorkflowDataFeild]
    private int EntityId { get; set; }


    /// <summary>
    /// 总监 Id (列表)
    /// </summary>
    [ESP.Workflow.WorkflowDataFeild(true)]
    private int[] MajorId { get; set; }

    /// <summary>
    /// 总经理 Id (列表)
    /// </summary>
    [ESP.Workflow.WorkflowDataFeild(true)]
    private int[] GeneralManagerId { get; set; }

    /// <summary>
    /// 项目负责人 Id (列表)
    /// </summary>
    [ESP.Workflow.WorkflowDataFeild(true)]
    private int[] PorjectManagerId { get; set; }

    /// <summary>
    /// 财务助理（FA） Id (列表)
    /// </summary>
    [ESP.Workflow.WorkflowDataFeild(true)]
    private int[] FAId { get; set; }

    /// <summary>
    /// 财务第一级审批人 Id (列表)
    /// </summary>
    [ESP.Workflow.WorkflowDataFeild(true)]
    private int[] Finance1Id { get; set; }

    /// <summary>
    /// 财务第二级审批人 Id (列表)
    /// </summary>
    [ESP.Workflow.WorkflowDataFeild(true)]
    private int[] Finance2Id { get; set; }

    /// <summary>
    /// 财务第三级审批人 Id (列表)
    /// </summary>
    [ESP.Workflow.WorkflowDataFeild(true)]
    private int[] Finance3Id { get; set; }

    [ESP.Workflow.WorkflowDataFeild(true)]
    private int[] Finance4Id { get; set; }

    /// <summary>
    /// CEO Id (列表)
    /// </summary>
    [ESP.Workflow.WorkflowDataFeild(true)]
    private int[] CEOId { get; set; }

    /// <summary>
    /// 财务总监（Eddy） Id (列表)
    /// </summary>
    [ESP.Workflow.WorkflowDataFeild(true)]
    private int[] FinanceMajorId { get; set; }

    /// <summary>
    /// 分公司财务（Rosa） Id (列表)
    /// </summary>
    [ESP.Workflow.WorkflowDataFeild(true)]
    private int[] FilialeFinanceId { get; set; }

    /// <summary>
    /// 预审人 Id (列表)
    /// </summary>
    [ESP.Workflow.WorkflowDataFeild(true)]
    private int[] PreApproverId { get; set; }

    /// <summary>
    /// 附加收货人 Id (列表)
    /// </summary>
    [ESP.Workflow.WorkflowDataFeild(true)]
    private int[] ReceiveAffirmantId { get; set; }


    /// <summary>
    /// 是否使用GM项目费用
    /// </summary>
    [ESP.Workflow.WorkflowDataFeild]
    private bool IsGM { get; set; }

    /// <summary>
    /// （非GM费用）提交人是否是项目负责人
    /// </summary>
    [ESP.Workflow.WorkflowDataFeild]
    private bool IsProjectManager { get; set; }

    /// <summary>
    /// （非GM费用）提交人是否是总监
    /// </summary>
    [ESP.Workflow.WorkflowDataFeild]
    private bool IsMajor { get; set; }

    /// <summary>
    /// （非GM费用）总金额是否大于 10000
    /// </summary>
    [ESP.Workflow.WorkflowDataFeild]
    private bool IsGT_10000 { get; set; }

    /// <summary>
    /// （非GM费用）总金额是否大于 50000
    /// </summary>
    [ESP.Workflow.WorkflowDataFeild]
    private bool IsGT_50000 { get; set; }

    /// <summary>
    /// （非GM费用）是否是CEO (Hover) 提交。
    /// </summary>
    [ESP.Workflow.WorkflowDataFeild]
    private bool IsHover { get; set; }

    /// <summary>
    /// （非GM费用）是否是总经理级别提交。
    /// </summary>
    [ESP.Workflow.WorkflowDataFeild]
    private bool IsGeneralManager { get; set; }

    /// <summary>
    /// （非GM费用）单笔餐费否大于 2000
    /// </summary>
    [ESP.Workflow.WorkflowDataFeild]
    private bool IsGT_2000 { get; set; }

    /// <summary>
    /// （GM费用）是否是上每财务人员提交
    /// </summary>
    [ESP.Workflow.WorkflowDataFeild]
    private bool IsFinanceShanghai { get; set; }

    /// <summary>
    /// （GM费用）是否是重庆财务人员提交
    /// </summary>
    [ESP.Workflow.WorkflowDataFeild]
    private bool IsFinanceChongqing { get; set; }

    /// <summary>
    /// （GM费用）是否是北京财务人员提交
    /// </summary>
    [ESP.Workflow.WorkflowDataFeild]
    private bool IsFinanceBeijing { get; set; }

    /// <summary>
    /// （GM费用）是否是财务总监（Eddy）提交
    /// </summary>
    [ESP.Workflow.WorkflowDataFeild]
    private bool IsFinanceMajor { get; set; }

    ///// <summary>
    ///// 是否是现金借款（否则为 现场采买）
    ///// </summary>
    //[ESP.Workflow.WorkflowDataFeild]
    //private bool IsCashLoan { get; set; }


    /// <summary>
    /// 是否已经收货（工作流内部控制，无需传入该参数）
    /// </summary>
    [ESP.Workflow.WorkflowDataFeild]
    private bool HasReceived { get; set; }

    /// <summary>
    /// （GM费用）是否是行政人员提交
    /// </summary>
    [ESP.Workflow.WorkflowDataFeild]
    private bool IsAdministration { get; set; }

    [ESP.Workflow.WorkflowDataFeild]
    private int ReturnType { get; set; }

    [ESP.Workflow.WorkflowDataFeild]
    private bool IsFinance1W { get; set; }


    [ESP.Workflow.WorkflowDataFeild]
    private bool IsBeijingDept { get; set; }
}