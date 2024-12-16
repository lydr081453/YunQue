using System.Collections.Generic;
using System.Configuration;

namespace ESP.Purchase.Common
{
    /// <summary>
    /// State 的摘要说明
    /// </summary>
    public static class State
    {

        #region 申请单状态
        //订单/申请单状态
        public static int requisition_del = -1;//逻辑删除
        public static int requisition_save = 0; //申请单保存
        public static int requisition_commit = 1; //申请单提交
        public static int requisition_return = 2; //申请单审核驳回
        public static int order_commit = 3; //申请单审核通过
        public static int order_return = 4; //申请单审批驳回
        public static int order_ok = 5; //申请单审批通过
        public static int order_sended = 6; //订单已发送
        public static int order_confirm = 7; //
        public static int requisition_submit = 8;//生成订单处申请单提交(未审批申请单)
        public static int requisition_ok = 9;//申请单审批通过
        public static int requisition_temporary_commit = 10; //分公司申请单提交
        public static int requisition_recipiented = 11;//订单结束
        public static int requisition_operationAduit = 12;//业务审核
        public static int order_mediaAuditWait = 13;//待媒介审批
        public static int order_mediaAuditYes = 14;//媒介审批通过
        public static int order_ADAuditWait = 15;//待媒介广告采买总监审批
        public static int requisition_MediaFAOperated = 20;//3000以上中创PR单创建完成
        public static int requisition_recipienting = 16;//收货中
        public static int requisition_paid= 17;//已完成付款申请
        public static int requisition_MediaOperating = 18;//媒体稿费/对私李彦娥处理中
        public static int requisition_RiskControl = 19;//待风控中心审批
        public static int requisition_Stop = 21;//申请单停止

        public static int CostRecord_FormType_Project = 1;
        public static int CostRecord_FormType_PR = 2;
        public static int CostRecord_FormType_PN = 3;
        public static int CostRecord_FormType_OOP = 4;
        public static int CostRecord_FormType_PaymentPeriod = 5;
        public static int CostRecord_FormType_PRNew = 6;
        public static int CostRecord_FormType_PNNew = 7;

        public static string[] requistionOrorder_state = { "未提交", "待集团采购审核", "采购审核驳回", "待采购总监审批", 
                                                             "采购总监审批驳回", "审批已完成", "订单待确认", "订单待收货",
                                                              "未审批申请单","申请单审批通过","待分公司采购审核","已完成收货","待业务审批","待媒介审批","媒介审批通过","待媒介广告采买总监审批","收货中","已付款","处理中","待风控审批","","申请单停止"};
        public static string[] requistionOrorderValue1_state = {   "待集团采购审核",  "待采购总监审批", 
                                                             "采购总监审批驳回", "审批已完成", "订单待确认", "订单已确认",
                                                              "分公司申请单提交","已完成收货","待业务审批" };
        public static string[] requistionOrorderValue_state = { "1", "3", "4", "5", "6", "7", "10", "11" ,"12"};

        public static string[] requistionOrderValue2_state = { "审批已完成", "订单待确认", "订单已确认", "已完成收货", "已付款" };
        public static string[] requistionOrderValue2_state2 = { "5","6","7","11","17"};
        public static string[] MediaFeeType = { "稿件费用报销单","个人报销单","现金付款报销单","借款单","车马费报销单"};

        //0 新建（待支付） 1 进行中 2 已完成 3 已关闭
       // public static string[] XiaoMiOrderStatus = { "待支付", "进行中", "已完成", "已关闭" };
        public static string[] XiaoMiOrderStatus = { "初始状态", "执行中", "已完成", "已评价", "已关闭","" };

        //代付状态 0-初始状态 1-已支付 2-已拒绝
        public static string[] XiaoMiPayByOtherStatus = { "初始状态", "已支付", "已拒绝" };

        //卖家状态  1 确认完成 2 同意退款 3 已回复 4 已添加链接
        public static string[] XiaoMiSalerStatus = { "", "确认完成", "同意退款", "已回复", "已添加链接" };
        public static string[] XiaoMiSalerStatusV2 = { "初始状态", "已反馈", "确认执行", "已取消"};

        //买家状态  1 已支付 2 申请退款 3 确认完成 4 取消申请退款 5 取消订单 6 已评论
        public static string[] XiaoMiBuyerStatus = { "", "已支付", "申请退款", "确认完成", "取消申请退款", "取消订单", "已评论" };
        public static string[] XiaoMiBuyerStatusV2 = { "初始状态", "已确认待支付预付款", "预付款支付中", "开始执行", "已确认收货待支付尾款", "尾款支付中", "已完成", "已取消" };
        #endregion

        #region 申请单流向
        //申请单流向
        public static int requisitionflow_toO = 0;//审批申请单，生成订单
        public static int requisitionflow_toR = 1;//审批申请单，不生成订单
        public static int requisitionflow_toC = 2;//审批申请单，生成合同
        public static int requisitionflow_toFC = 3;//框架合同。有押金，采购物品为0
        public static string[] requisitionflow_state = { "PO", "PR", "合同" ,"框架合同"};
        #endregion

        #region 申请单类型
        public static int OperationTypePub
        {
            get { return 0; }
        }
        public static int OperationTypePri
        {
            get { return 1; }
        }
        public static string[] OperationTypeShow = { "对公", "个人"};
        #endregion 申请单类型

        #region 物料类别级别
        //物料类别级别
        public static int producttype_l0 = 0;//物料类别0级，根目录
        public static int producttype_l1 = 1;//物料类别1级
        public static int producttype_l2 = 2;//物料类别2级
        public static int producttype_l3 = 3;//物料类别3级
        public static string[] producttype_level = { "根目录", "一级物料", "二级物料", "三级物料" };
        #endregion

        #region 收货确认类型
        public static int recipientstatus_All = 0;//全额收货
        public static int recipientstatus_Part = 1;//部分收货
        public static int recipientstatus_Unsure = 2;//金额不符收货
        public static string[] recipient_state = { "全额收货", "分批收货", "实发金额收货" };
        #endregion

        #region 帐期类型
        public static int PaymentStatus_save = 0;//创建帐期，未和收货关联
        public static int PaymentStatus_wait = 1;//已和收货关联，未付款 保存状态
        public static int PaymentStatus_commit = 2;//已和收货关联，未付款 提交状态
        public static int PaymentStatus_other2 = 3;//待定2
        public static int PaymentStatus_other3 = 4;//待定3
        public static int PaymentStatus_over = 5;//已付款（由财务部门最终支付时确定）
        public static string[] Payment_Status = { "帐期已创建", "待提交", "待业务审核", "待定2", "待定3", "已付款" };
        #endregion 帐期类型

        #region 日志
        public static string log_requisition_commit = "{0}提交申请单 {1}";
        public static string log_requisition_temporary_commit = "{0}分公司审核通过 {1}";
        public static string log_requisition_temporary_return = "{0}分公司审核驳回 {1}";
        public static string log_requisition_ok = "{0}审核通过 {1}";
        public static string log_requisition_return = "{0}审核驳回 {1}";
        public static string log_order_commit = "{0}审批通过 {1}";
        public static string log_prMedia_commit = "{0}媒介审批通过 {1}";
        public static string log_prMedia_return = "{0}媒介审批驳回 {1}";
        public static string log_order_return = "{0}审批驳回 {1}";
        public static string log_order_ok = "{0}审批通过 {1}";
        public static string log_changecheker = "{0}变更初审人为{1} {2}";
        public static string log_confrim = "{0}确认订单 {1}";
        public static string log_operationaudit_yes = "{0}业务审核通过 {1}";
        public static string log_operationaudit_no = "{0}业务审核驳回 {1}";
        public static string log_requisition_cancel = "{0}撤销申请单 {1}";
        public static string log_requisition_cancelJT = "{0}撤销申请单至集团物料审核 {1}";
        public static string log_requisition_cancelFGS = "{0}撤销申请单至分公司物料审核 {1}";
        public static string log_requisition_cancelByauditor = "由{0}撤销至已审核状态 {1}";
        public static string log_recipient = "{0}已收货，收货单号{1} {2}";
        public static string log_recipient_confrim = "{0} 已确认 {1}收货单 {2}";
        public static int log_status_cancel = 0; //销毁的日志
        public static int log_status_normal = 1; //正常的日志
        public static string log_disabled = "{0}将{1}申请单暂停";
        public static string log_used = "{0}将{1}申请单启用";
        public static string log_prStop = "{0}将{1}申请单停止";
        public static string log_disabledProject = "{0}将{1}申请单所属项目暂停";
        public static string log_usedProject = "{0}将{1}申请单所属项目启用";
        #endregion

        #region 申请单添加步骤
        public static string addstatus_1 = "AddRequisitionStep2.aspx?" + RequestName.GeneralID + "={0}";
        public static string addstatus_2 = "AddRequisitionStep3.aspx?" + RequestName.GeneralID + "={0}";
        public static string addstatus_3 = "AddRequisitionStep6.aspx?" + RequestName.GeneralID + "={0}";
        public static string addstatus_4 = "AddRequisitionStep4.aspx?" + RequestName.GeneralID + "={0}";
        public static string addstatus_5 = "AddRequisitionStep7.aspx?" + RequestName.GeneralID + "={0}";
        public static string addstatus_6 = "AddRequisitionStep5.aspx?" + RequestName.GeneralID + "={0}";
        public static string addstatus_7 = "EditRequisition.aspx?" + RequestName.GeneralID + "={0}";
        public static string addstatus_Back = "EditRequisition.aspx?" + RequestName.GeneralID + "={0}&"+RequestName.BackUrl+"={1}";
        #endregion

        #region 其他信息
        public static string[] materiel_type = { 
            "市内、全国快递",
            "货运",
            "兼职",
            "EMS",
            "日常机票",
            "洗像",
            "复印、装订公司",
            "摄像公司",
            "摄像及视频制作公司",
            "客运租车",
            "剪报公司",
            "北京翻译",
            "翻译个人",
            "速记",
            "租图公司",
            "AV租赁",
            "木工",
            "美工、租赁",
            "旅游公司",
            "演艺、礼仪、主持人"
        };

        //人民币类型
        public static string[] money_type = {
            "人民币","美元","日元","欧元","英镑","加元","澳元"
        };

        /// <summary>
        /// 最小日期
        /// </summary>
        public static string datetime_minvalue = "1900-1-1 0:00:00";

        /// <summary>
        /// 获得用户邮箱
        /// </summary>
        /// <param name="SysUserId"></param>
        /// <returns></returns>
        public static string getEmployeeEmailBySysUserId(int SysUserId)
        {
            return new ESP.Compatible.Employee(SysUserId).EMail;
        }

        /// <summary>
        /// 收货默认地址
        /// </summary>
        public static string ShunYa_Default_Address = "北京市朝阳区双桥路12号 电子城数字新媒体创新产业园D1楼";
        public static string ShunYa_CQ_Address = "重庆市渝北区大竹林街道杨柳路6号三狼公园6号D4-102";
        public static string ShunYa_SH_Address = "上海市普陀区南郑路355弄9号鸿企中心22层2201室";

        /// <summary>
        /// 采购方名称
        /// </summary>
        public static System.Collections.Hashtable NameHT = InitNameHashtable();
        public static System.Collections.Hashtable InitNameHashtable()
        {
            System.Collections.Hashtable ht = new System.Collections.Hashtable();
            ht.Add("X", "天津星言云汇网络科技有限公司"); //
            ht.Add("S", "北京星声场网络科技有限公司"); //
            ht.Add("K", "北京云柯网络科技有限公司"); //
            ht.Add("D", "北京星美达网络科技有限公司"); //
            ht.Add("C", "北京星畅网络科技有限公司"); //
            ht.Add("Z", "北京星畅网络科技有限公司重庆分公司"); //
            ht.Add("Q", "宣亚国际营销科技（北京）股份有限公司上海分公司"); //
            return ht;
        }

        /// <summary>
        /// 采购方地址
        /// </summary>
        public static System.Collections.Hashtable AddressHT = InitAddressHashtable();
        public static System.Collections.Hashtable InitAddressHashtable()
        {
            System.Collections.Hashtable ht = new System.Collections.Hashtable();
            ht.Add("X", ShunYa_Default_Address); 
            ht.Add("S", ShunYa_Default_Address); 
            ht.Add("K", ShunYa_Default_Address); 
            ht.Add("D", ShunYa_Default_Address); 
            ht.Add("C", ShunYa_Default_Address); 
            ht.Add("Z", ShunYa_CQ_Address);
            ht.Add("Q", ShunYa_SH_Address); 
            return ht;
        }

        public static int productAttribute_ml = 1;
        public static int productAttribute_fml = 2;

        /// <summary>
        /// 物料属性
        /// </summary>
        public enum productAttribute : int
        {
            /// <summary>
            /// 目录物品
            /// </summary>
            ml = 1,
            /// <summary>
            /// 非目录物品
            /// </summary>
            fml = 2
        }

        //物料类别根目录
        public static int RootType = 0;

        //业务审核名称
        /// <summary>
        /// 项目负责人
        /// </summary>
        public static string operationAudit_principal = "项目负责人";
        /// <summary>
        /// 媒介总监
        /// </summary>
        public static string operationAudit_MediaMajor = "媒介总监";
        /// <summary>
        /// 总监
        /// </summary>
        public static string operationAudit_majordomo = "总监";
        /// <summary>
        /// 总经理
        /// </summary>
        public static string operationAudit_generalmanager = "总经理";
        /// <summary>
        /// CEO
        /// </summary>
        public static string operationAudit_CEO = "CEO";

        /// <summary>
        /// 业务审核状态
        /// </summary>
        public enum operationAudit_status : int
        {
            /// <summary>
            /// 审核驳回
            /// </summary>
            No = 0,
            /// <summary>
            /// 审核通过
            /// </summary>
            Yes = 1,
            /// <summary>
            /// 留言
            /// </summary>
            Message=2
        }
        public static string[] operationAudit_statusName = { "审核驳回","审核通过","留言"};

        /// <summary>
        /// 是否收到预付款
        /// </summary>
        public enum receivePrepay : int
        {
            /// <summary>
            /// 未收
            /// </summary>
            No = 0,
            /// <summary>
            /// 已收
            /// </summary>
            Yes = 1
        }

        /// <summary>
        /// 是否收到付款
        /// </summary>
        public enum receivePrice : int
        {
            /// <summary>
            /// 未收
            /// </summary>
            No = 0,
            /// <summary>
            /// 已收
            /// </summary>
            Yes = 1
        }

        #endregion 其他信息

        #region 供应商信息

        #region 供应商来源
        /// <summary>
        /// 业务推荐
        /// </summary>
        public static string suppliersource_yw = "业务推荐";
        /// <summary>
        /// 客户指定
        /// </summary>
        public static string suppliersource_kh = "客户指定";
        /// <summary>
        /// 采购部推荐
        /// </summary>
        public static string suppliersource_cg = "采购部推荐";
        /// <summary>
        /// 临时供应商
        /// </summary>
        public static string suppliersource_ls = "临时供应商";
        /// <summary>
        /// 协议供应商
        /// </summary>
        public static string suppliersource_xy = "协议供应商";
        #endregion

        #region 供应商类型
        /// <summary>
        /// 供应商类型
        /// </summary>
        public enum supplier_type : int
        {
            /// <summary>
            /// 协议供应商
            /// </summary>
            agreement = 1,
            /// <summary>
            /// 推荐供应商
            /// </summary>
            recommend = 2,
            /// <summary>
            ///  非协议供应商
            /// </summary>
            noAgreement = 3
        }
        public static string[] supplierType_Names = {"","协议供应商","推荐供应商","非协议供应商" };
        #endregion 供应商类型

        #region 供应商状态
        /// <summary>
        /// 停用
        /// </summary>
        public static int supplierstatus_block = 0;

        /// <summary>
        /// 可用
        /// </summary>
        public static int supplierstatus_used = 1;

        /// <summary>
        /// 供应商状态显示信息
        /// </summary>
        public static string[] supplierstatus = { "停用", "使用" };
        #endregion

        #region 协议供应商账期
        /// <summary>
        /// 60天账期
        /// </summary>
        public static int supplierpayment_60d = 0;

        /// <summary>
        /// 90天账期
        /// </summary>
        public static int supplierpayment_90d = 1;

        /// <summary>
        /// 120天账期
        /// </summary>
        public static int supplierpayment_120d = 2;

        /// <summary>
        /// 供应商状态显示信息
        /// </summary>
        public static string[] supplierpayment = { "45-60", "75-90","105-120" };
        #endregion 协议供应商账期

        #endregion 供应商信息


        #region 物料类别信息

        #region 物料类别状态
        /// <summary>
        /// 停用
        /// </summary>
        public static int typestatus_block = 0;

        /// <summary>
        /// 可用
        /// </summary>
        public static int typestatus_used = 1;

        /// <summary>
        /// 媒介专用，采购暂时用不到
        /// </summary>
        public static int typestatus_media = 2;

        /// <summary>
        /// 3000以上生成的单子，才可以使用为这个状态的物料类别。
        /// </summary>
        public static int typestatus_mediaUp3000 = 3;

        /// <summary>
        /// 物料类别状态显示信息
        /// </summary>
        public static string[] typestatus = { "停用", "使用" };
        #endregion

        #region 物料类别是否需要集团物料审核人复查
        /// <summary>
        /// 分公司提交后，直接跳转至总监级审批，不需要总部物料审核人复查
        /// </summary>
        public static int typeHQCheck_No = 0;

        /// <summary>
        /// 分公司提交后，跳转至集团物料审核人处，需要总部物料审核人复查
        /// </summary>
        public static int typeHQCheck_Yes = 1;
        #endregion

        #region 物料类别业务流向
        //由物料类别的不同导致不同的pr单的流转，主要体现在不同的采购审核权限

        /// <summary>
        /// 第三方流转（普通采购流程）
        /// </summary>
        public static int typeoperationflow_Mormal = 0;

        /// <summary>
        /// 媒体稿费报销（由媒介部负责）
        /// </summary>
        public static int typeoperationflow_Media = 1;

        /// <summary>
        /// 第三方媒体承包公司（由媒介部负责）
        /// </summary>
        public static int typeoperationflow_OtheMedia = 8;
        /// <summary>
        /// 线上合作（王桢）
        /// </summary>
        public static int typeoperationflow_Advertisement = 9;
        /// <summary>
        /// 广告媒体采买（由李雷负责）
        /// </summary>
        public static int typeoperationflow_AD = 2;

        //需要媒介总监审批的()
        public static int typeoperationflow_MFA = 4;

        //媒体合作
        public static int typeoperationflow_MP = 5;

        //临时状态,物料审核人李彦娥,无采购总监审批,媒介总监审批,不经过分公司
        public static int typeoperationflow_TMP1 = 6;

        //临时状态,物料审核人李盈,有采购总监审批,不经过分公司
        public static int typeoperationflow_TMP2 = 7;
        //业务流向对应的审核人
        public static string[] typeoperationflowAuditNames = { "", "吴卫华", "李雷，刘新华", "", "吴卫华", "吴卫华", "", "","","" };
        #endregion 物料类别业务流向

        #endregion 物料类别信息

        #region 收货状态
        /// <summary>
        /// 未收货
        /// </summary>
        public static string recipient_WS = "未收货";
        /// <summary>
        /// 收货中
        /// </summary>
        public static string recipient_SHZ = "收货中";
        /// <summary>
        /// 收货完成
        /// </summary>
        public static string recipient_YSW = "收货完成";

        #endregion

        #region 分公司名称
        public static string filialeName_CQ = "重庆分公司";

        public static string filialeName_CQID = "230";

        #endregion

        #region 传真
        public static string fax_CQ = "";
        public static string fax_ZB = "010-85095795";
            #endregion

        #region 收货确认
        /// <summary>
        /// 未确认
        /// </summary>
        public static int recipentConfirm_No = 0;
        /// <summary>
        /// 供应商确认
        /// </summary>
        public static int recipentConfirm_Supplier = 1;
        /// <summary>
        /// 员工确认
        /// </summary>
        public static int recipentConfirm_Emp1 = 5;//收货人确认
        public static int recipentConfirm_Emp2 = 2;//附加收货人确认
        /// <summary>
        /// 已关联付款账期
        /// </summary>
        //public static int recipentConfirm_Payment = 3;

        /// <summary>
        /// 已关联付款账期 并提交
        /// </summary>
        public static int recipentConfirm_PaymentCommit = 4;
        public static string[] recipientConfirm_Names = {"未确认","供应商已确认","待供应商确认","","","待附加收货人确认" };
        #endregion

        #region 付款
        /// <summary>
        /// 付款账期类型
        /// </summary>
        public enum PeriodType : int
        {
            /// <summary>
            /// 付款账期
            /// </summary>
            period = 0,
            /// <summary>
            /// 预付款
            /// </summary>
            prepay = 1
        }
        //public static string[] paymentTypeName = {"付款账期","预付款" };
        #endregion 付款

        #region 媒介订单MediaOrder
        public static int MediaOrder_no = 0;
        public static int MediaOrder_used = 1;
        public static string[] MediaOrder_Status = { "未绑定", "已绑定" };
        #endregion

        #region 提示信息
        /// <summary>
        /// 付款信息
        /// </summary>
        public static string alertMsg_checkPayPrice_F = "预付金额与付款账期金额之和应等于采购物品的总价，请修改后再保存！";
        public static string alertMsg_checkPayPrice_X = "预付金额不能大于采购物品的总价，请修改后再保存！";

        public static string[] Period_PeriodType = { "标准条款","预付条款"  };
        public static string[] Period_PeriodDatumPoint = { "收货或活动日期", "合同/订单日期" };
        public static string[] Period_DateType = { "工作日","自然日" };

        #endregion

        #region 项目相关信息
        public static int projectstatus_ok = 32; //已审批的项目号状态
        #endregion 项目相关信息

        public static int Message_Area_BJ = 0;
        public static int Message_Area_CQ = 1;

        public static string[] Message_Area = { "北京", "重庆","上海" };
        public static Dictionary<int, string> WorkCity = new Dictionary<int, string> { { 19, "北京" }, { 230, "重庆" }, { 247, "上海" } };
        
        #region 业务审批类型
        /// <summary>
        /// 预审
        /// </summary>
        public static int operationAudit_Type_YS = 1;
        /// <summary>
        /// 总监审批
        /// </summary>
        public static int operationAudit_Type_ZJSP = 2;
        /// <summary>
        /// 总监知会
        /// </summary>
        public static int operationAudit_Type_ZJZH = 3;
        /// <summary>
        /// 总监知会
        /// </summary>
        public static int operationAudit_Type_ZJFJ = 4;
        /// <summary>
        /// 总经理审批
        /// </summary>
        public static int operationAudit_Type_ZJLSP = 5;
        /// <summary>
        /// 总经理知会
        /// </summary>
        public static int operationAudit_Type_ZJLZH = 6;
        /// <summary>
        /// 总经理附加
        /// </summary>
        public static int operationAudit_Type_ZJLFJ = 7;
        /// <summary>
        /// CEO审批
        /// </summary>
        public static int operationAudit_Type_CEO = 8;

        public static int operationAudit_Type_CG = 9;
        public static int operationAudit_Type_CGZJ = 10;

        public static string[] operationAudit_Type_Names = {"", "预审", "总监审批", "总监知会", "总监附加", "总经理审批", "总经理知会", "总经理附加", "CEO审批" ,"物料审核","采购总监审批"};
        #endregion

        public static string[] oldFlagNames = { "(线上)","(线下)" };

        /// <summary>
        /// 数据类型
        /// </summary>
        public enum DataType : int
        {
            /// <summary>
            /// 申请单
            /// </summary>
            PR = 0,
            /// <summary>
            ///收货单
            /// </summary>
            GR =1,
            /// <summary>
            /// 项目号申请单
            /// </summary>
            Project=2,
            /// <summary>
            /// 支持方
            /// </summary>
            Supporter=3,
            /// <summary>
            /// 付款通知
            /// </summary>
            Payment=4,
            /// <summary>
            /// 付款申请
            /// </summary>
            Return=5,

        }

        /// <summary>
        /// 申请单使用状态
        /// </summary>
        public enum PRInUse : int
        {
            /// <summary>
            /// 申请单停用
            /// </summary>
            Disable = 0,
            /// <summary>
            /// 项目停用
            /// </summary>
            DisableProject = 1,
            /// <summary>
            /// 正常使用
            /// </summary>
            Use = 2
        }
        public static string[] PRInUse_Names = { "申请单停用", "项目停用", "正常使用" };

        /// <summary>
        /// 付款状态
        /// </summary>
        public enum PaymentStatus : int
        {
            /// <summary>
            /// 未付款
            /// </summary>
            Not = 0,
            /// <summary>
            /// 创建付款
            /// </summary>
            Create = 1,
            /// <summary>
            /// 财务已付款
            /// </summary>
            Complete
        }
        /// <summary>
        /// 付款状态描述
        /// </summary>
        public static string[] PaymentStatus_Names = { "未付款", "创建付款", "财务已付款" };

        public static string DisabledMessageForPRView = "该申请单已被暂停。";
        public static string DisabledMessageForProjectView = "该申请单所属项目已被暂停。";
        public static string DisabledMessageForPR = DisabledMessageForPRView + "您不能进行任何操作！";
        public static string DisabledMessageForProject = DisabledMessageForProjectView + "您不能进行任何操作！";
        public static string StopMessageForPRView = "该申请单已被停止。";
        public static string StopMessageForPR = StopMessageForPRView + "您不能进行任何操作！";

        #region "黑名单主表状态"
        public enum BlackListStatus : int
        {
            /// <summary>
            /// 已发送邮件，等待业务反馈
            /// </summary>
            MailSended = 0,
            /// <summary>
            /// 反馈后列入黑名单
            /// </summary>
            Responsed = 1,
            /// <summary>
            /// 未反馈列入黑名单
            /// </summary>
            UnResponsed=2
        }
        #endregion

        #region "固定式/开放式账期"
        public enum PeriodType4CreatePN : int
        { 
            /// <summary>
            /// 固定式账期
            /// </summary>
              Standard=0,
            /// <summary>
            /// 开放式账期
            /// </summary>
            Openning=1
        }
        public static string[] PeriodType4CreatePNDesc = { "固定式账期", "开放式账期" };

        /// <summary>
        /// 邮件提醒数据类型
        /// </summary>
        public enum RemindOrderType : int
        {
            /// <summary>
            /// 付款申请
            /// </summary>
            Payment=0,
            /// <summary>
            /// 收货
            /// </summary>
            Recipient=1
        }
        #endregion

        /// <summary>
        /// 特殊物料使用权限状态
        /// </summary>
        public enum PRAuthorizationStatus : int
        {
            /// <summary>
            /// 关闭
            /// </summary>
            PRAuthorizationStatus_Close = 0,
            /// <summary>
            /// 使用
            /// </summary>
            PRAuthorizationStatus_Use = 1
        }
        public static string[] PRAuthorizationStatus_Names = {"已关闭","使用中"};

        /// <summary>
        /// PR成本记录类型
        /// </summary>
        public enum PR_CostRecordsType
        {
            PR单提交 = 0,
            PR单撤销 = 1,
            PR单业务审核通过 = 2,
            PR单业务审核驳回 = 3,
            PR单分公司审核通过 = 4,
            PR单分公司审核驳回 = 5,
            PR单采购初审通过 = 6,
            PR单采购初审驳回 = 7,
            PR单采购总监审批通过 = 8,
            PR单采购总监审批驳回 = 9,
            PR单广告购买审批通过 = 10,
            PR单广告购买审批驳回 = 11,
            PR单媒介审批通过 = 12,
            PR单媒介审批驳回 = 13,
            PR单个人3000以下PN申请 = 14,
            PR单个人3000以上PR申请 = 15,
            PR单媒介3000以下PN申请 = 16,
            PR单媒介3000以上PR申请 = 17,
            PR单媒介需要税单PN申请 = 18
        }
    }

    public static class FieldTypeList
    {
        public static readonly string[] FieldTypes = { "商务酒店", "会议中心", "餐厅、酒吧", "剧院、演播厅", "路演场地", "试乘试驾", "体育场馆", "艺术园区、公园", "其它" }; 
    }

}