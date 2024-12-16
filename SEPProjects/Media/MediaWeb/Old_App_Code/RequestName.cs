using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///Common 的摘要说明
/// </summary>
public class RequestName
{
    public const string ProjectID = "ProjectID";//项目ID
    public const string ProjectCode = "ProjectCode";//项目号
    public const string MediaID = "Mid";//媒体ID
    public const string ReporterID = "Rid";//记者ID
    public const string ClientID = "ClientID";//客户ID
    public const string ProductLineID = "ProductLineID";//产品线ID
    public const string Alert = "alert";

    public const string Operate = "Operate";//操作

    public const string BackUrl = "backurl";//返回路径


    //单据
    public const string BillID = "BillID";
    public const string BillType = "BillType";
    public const string PersonFeeBillID = "PersonFeeBillID";//个人费用报销单
    public const string PersonFeeItemID = "PersonFeeItemID";//个人费用报销项

    public const string CashPaymentBillID = "CashPaymentBillID";//现金付款报销单
    public const string CashPaymentItemID = "CashPaymentItemID";//现金付款报销项

    public const string CashloanBillID = "CashloanBillID";//个人费用报销单
    public const string CashloanItemID = "CashloanItemID";//个人费用报销项

    public const string CheckPaymentBillID = "CheckPaymentBillID";//支票/电汇付款单
    public const string CheckPaymentItemID = "CheckPaymentItemID";//支票/电汇报销项

    public const string WritingFeeBillID = "WritingFeeBillID";//稿件费用报销单
    public const string WritingFeeItemID = "WritingFeeItemID";//稿件费用报销项

    public const string meetingID = "meetingid";
    public const string FaceID = "facetofaceid";
    public const string GeneralID = "GeneralID";
    public const string MediaPREntry = "MediaPREntry"; //媒介PR申请入口
}
/// <summary>
/// 媒介申请类型 (对公|对私)
/// </summary>
public enum MediaOperateType
{
    /// <summary>
    /// 对私业务
    /// </summary>
    MediaPrivate = 0,
    /// <summary>
    /// 对公业务
    /// </summary>
    MediaPublic = 1,
    /// <summary>
    /// 从媒介过来的新建bill
    /// </summary>
    MediaBill = 2
}