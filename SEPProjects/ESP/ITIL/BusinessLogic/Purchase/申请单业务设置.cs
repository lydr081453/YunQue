using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Purchase.Common;
using ESP.Compatible;
using ESP.Purchase.Entity;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.DataAccess;
using ESP.Finance.Entity;
using System.Data.SqlClient;
using ESP.Finance.BusinessLogic;

namespace ESP.ITIL.BusinessLogic
{

    /// <summary>
    /// 
    /// </summary>
    public class 申请单业务设置
    {
        //  public static int AuditorId2 = int.Parse(ESP.Configuration.ConfigurationManager.SafeAppSettings["AuditorId2"]);
        //public static int AuditorId = int.Parse(ESP.Configuration.ConfigurationManager.SafeAppSettings["AuditorId"]);
        //public static int ADAuditorId = int.Parse(ESP.Configuration.ConfigurationManager.SafeAppSettings["ADAuditorId"]);
        //public static int ADAuditorId2 = int.Parse(ESP.Configuration.ConfigurationManager.SafeAppSettings["ADAuditorId2"]);
        //public static int MediaAuditorId = int.Parse(ESP.Configuration.ConfigurationManager.SafeAppSettings["mediaAuditorId"]);

        /// <summary>
        /// PR单提交（状态变更为“待业务审核”）
        /// </summary>
        /// <param name="CurrentUser">当前登录人</param>
        /// <param name="generalModel">申请单对象</param>
        /// <returns></returns>
        public static string 申请单提交(Employee CurrentUser, ref GeneralInfo generalModel)
        {
            string Msg = "";
            //验证项目金额
            if (!申请单验证规则.PRSubmit(generalModel))
            {
                return "项目成本金额不足，无法提交，请检查！";
            }

            // var operationModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByUserId(generalModel.requestor);
            ESP.Framework.Entity.OperationAuditManageInfo operationModel = null;

            if (generalModel.Project_id != 0)
            {
                operationModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByProjectId(generalModel.Project_id);
            }
            if (operationModel == null)
                operationModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByUserId(generalModel.requestor);


            if (operationModel == null)
                operationModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(generalModel.Departmentid);

            //设置采购部付款申请人
            string nodename = 获得顶级部门(int.Parse(CurrentUser.SysID));

            if (operationModel.PurchaseAuditorId != 0)
            {
                generalModel.PaymentUserID = operationModel.PurchaseAuditorId;
                //generalModel.Filiale_Auditor = operationModel.PurchaseAuditorId;
                //generalModel.Filiale_AuditName = operationModel.PurchaseAuditor;

                generalModel.first_assessor = operationModel.PurchaseAuditorId;
                generalModel.first_assessorname = operationModel.PurchaseAuditor;
            }


            if (operationModel.PurchaseDirectorId != 0)
            {
                generalModel.purchaseAuditor = operationModel.PurchaseDirectorId;
                generalModel.purchaseAuditorName = operationModel.PurchaseDirector;
            }

            if (operationModel.RiskControlAccounter > 0)
            {
                generalModel.RCAuditor = operationModel.RiskControlAccounter;
                generalModel.RCAuditorName = operationModel.RiskControlAccounterName;
            }

            //设置PRType审核人(ref generalModel);
            generalModel.status = State.requisition_operationAduit;//业务审核
            generalModel.PrNo = GeneralInfoManager.createPrNo();//生成申请单号
            generalModel.requisition_committime = DateTime.Now;
            generalModel.fili_overrule = "";
            return Msg;
        }

        /// <summary>
        /// 业务审核通过
        /// 物料流向为“广告媒体采买”，状态改为“待媒介广告采买总监审批”，其他，分公司申请，状态改为“待集团采购审核”，非分公司，状态改为“待分公司采购审核”。
        /// PRType为MediaPR（媒介申请）、PR_MediaFA（媒介大于3000生成）、MPPR（媒体合作PR单）、PR_PriFA（对私大于3000生成），状态改为“待媒介审批”
        /// PRType为PrivatePR（对私PR单），物料流向为“媒体合作”，状态改为“待媒介审批”，物料流向为“广告媒体采买”，状态改为“待媒介广告采买总监审批”
        /// PRType为PR_TMP1、PR_TMP2，状态改为“待集团采购审核”。
        /// </summary>
        /// <param name="CurrentUser">当前登录人</param>
        /// <param name="generalModel">申请单对象</param>
        /// <returns></returns>
        public static string 业务审核通过(Employee CurrentUser, ref GeneralInfo generalModel)
        {
            string Msg = "";
            // var operation = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByUserId(generalModel.requestor);
            ESP.Framework.Entity.OperationAuditManageInfo operation = null;

            if (generalModel.Project_id != 0)
            {
                operation = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByProjectId(generalModel.Project_id);
            }
            if (operation == null)
                operation = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByUserId(generalModel.requestor);

            if (operation == null)
                operation = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(generalModel.Departmentid);

            //验证项目金额
            if (!申请单验证规则.申请单修改金额提交验证(generalModel))
            {
                return "项目成本金额不足，无法审核通过，请检查！";
            }

            string nodename = 获得顶级部门(generalModel.requestor);
            //int typeoperationflow = OrderInfoManager.getTypeOperationFlow(generalModel.id);
            //if (typeoperationflow != State.typeoperationflow_AD && generalModel.PRType != (int)PRTYpe.PR_OtherAdvertisement)
            //{
            //if (nodename != State.filialeName_CQ)//北京总部
            //{
            //低值采购或者采购总监角色为空
            //if (generalModel.ValueLevel == 1 || operation.PurchaseAuditorId == 0)
            if (generalModel.ValueLevel == 1)
            {
                //流向是PO，状态为订单待确认6，写PO单号
                if (generalModel.Requisitionflow == State.requisitionflow_toO)
                {
                    generalModel.status = State.order_sended;
                    generalModel.orderid = generalModel.PrNo.Replace("PR", "PO");
                }
                else//非PO流向，不需要发PO邮件，状态为订单待收货
                    generalModel.status = State.order_confirm;
                generalModel.order_audittime = DateTime.Now;

            }
            else
            {
                if (operation.RiskControlAccounter > 0)
                {
                    generalModel.status = State.requisition_RiskControl;
                }
                else
                {
                    generalModel.status = State.requisition_commit;
                }
            }

            return Msg;
        }

        /// <summary>
        /// 业务审核驳回
        /// </summary>
        /// <param name="CurrentUser">当前登录人</param>
        /// <param name="generalModel">申请单对象</param>
        /// <returns></returns>
        public static string 业务审核驳回(Employee CurrentUser, ref GeneralInfo generalModel)
        {
            string Msg = "";
            //更新状态到申请单驳回
            generalModel.status = State.requisition_return;
            return Msg;
        }

        /// <summary>
        ///分公司审核通过（当不存在下级分公司审核人时，状态变更为“待采购总监审批”）
        /// </summary>
        /// <param name="CurrentUser">当前登录人</param>
        /// <param name="generalModel">申请单对象</param>
        /// <returns></returns>
        public static string 分公司审核通过(Employee CurrentUser, string nextFili, string nextFiliName, ref GeneralInfo generalModel, string majordomoUndoValue, System.Web.UI.Page Page)
        {
            string Msg = "";

            //验证项目金额
            if (!申请单验证规则.申请单修改金额提交验证(generalModel))
            {
                return "项目成本金额不足，无法审核通过，请检查！";
            }

            //设置PRType审核人(ref generalModel);
            if (nextFili == "")
            {
                generalModel.status = State.order_commit;
                generalModel.requisition_overrule = "";
                generalModel.order_overrule = "";
                string[] auditor = OrderInfoManager.getAuditor(generalModel.id);
                if (OrderInfoManager.getTypeIsNeedHQCheck(generalModel.id) > 0)
                {
                    //如果需要集团物料审核的复查，则将初审人设置成为集团物料审核人
                    generalModel.first_assessor = int.Parse(auditor[0]);
                    generalModel.first_assessorname = auditor[1];
                    generalModel.status = State.requisition_commit;
                }
            }
            else
            {
                //如果设置了下级分公司审核人，则更新分公司审核人信息
                generalModel.Filiale_Auditor = int.Parse(nextFili);
                generalModel.Filiale_AuditName = nextFiliName.Trim();
            }
            变更申请单号(ref generalModel, majordomoUndoValue, Page);
            return Msg;
        }

        /// <summary>
        /// 分公司审核驳回(状态变更为“采购审核驳回”)
        /// </summary>
        /// <param name="CurrentUser">当前登录人</param>
        /// <param name="generalModel">申请单对象</param>
        /// <returns></returns>
        public static string 分公司审核驳回(Employee CurrentUser, ref GeneralInfo generalModel)
        {
            string Msg = "";
            generalModel.status = State.requisition_return;
            return Msg;
        }

        /// <summary>
        /// 物料审核驳回（状态变更为“采购审核驳回”）
        /// </summary>
        /// <param name="CurrentUser">当前登录人</param>
        /// <param name="generalModel">申请单对象</param>
        /// <returns></returns>
        public static string 物料审核驳回(Employee CurrentUser, ref GeneralInfo generalModel)
        {
            string Msg = "";
            generalModel.status = State.requisition_return;
            return Msg;
        }

        /// <summary>
        /// 物料审核通过
        /// PRType=PR_TMP1(9)，状态变更为“待媒介审批”。其他PRType，状态变更为“待采购总监审批”
        /// </summary>
        /// <param name="CurrentUser">当前登录人</param>
        /// <param name="generalModel">申请单对象</param>
        /// <returns></returns>
        public static string 物料审核通过(Employee CurrentUser, ref GeneralInfo generalModel, string majordomoUndoValue, System.Web.UI.Page Page)
        {
            string Msg = "";

            //验证项目金额
            if (!申请单验证规则.申请单修改金额提交验证(generalModel))
            {
                return "项目成本金额不足，无法审核通过，请检查！";
            }

            设置PRType(ref generalModel);
            if (generalModel.PRType == (int)PRTYpe.PR_TMP1)
            {
                generalModel.status = State.order_mediaAuditWait;
            }
            else if (generalModel.PRType == (int)PRTYpe.PR_MediaFA || generalModel.PRType == (int)PRTYpe.PR_PriFA)
            {
                if (generalModel.Requisitionflow == State.requisitionflow_toO)
                    generalModel.status = State.order_ok;
                else
                    generalModel.status = State.order_confirm;
            }
            else
            {
                generalModel.status = State.order_commit;
                //设置采购审批人，初审人不为苏艳时军采购总监苏艳军，1w以上的pr单在苏艳军审批通过后才流转至david.duan处，初审人为苏艳军时，审批人为david.duan
                //ESP.Framework.Entity.EmployeeInfo purchaseAuditor = null;
                //if (int.Parse(CurrentUser.SysID) != AuditorId2)
                //    purchaseAuditor =
                //        ESP.Framework.BusinessLogic.EmployeeManager.Get(
                //            AuditorId2);
                //else
                //    purchaseAuditor =
                //        ESP.Framework.BusinessLogic.EmployeeManager.Get(
                //            AuditorId);

                //generalModel.purchaseAuditor = purchaseAuditor.UserID;
                //generalModel.purchaseAuditorName = purchaseAuditor.FullNameCN;
            }
            变更申请单号(ref generalModel, majordomoUndoValue, Page);
            return Msg;
        }


        public static string 风控审核通过(Employee CurrentUser, ref GeneralInfo generalModel, string majordomoUndoValue, System.Web.UI.Page Page)
        {
            string Msg = "";

            generalModel.status = State.requisition_commit;

            return Msg;
        }


        /// <summary>
        /// 采购总监审批通过
        /// 供应商类型为个人，PRType改为PrivatePR（对私PR单）
        /// PRType为MediaPR（媒介申请），PR_MediaFA（媒介大于3000生成），MPPR（媒体合作PR单），状态改为“待媒介审批”。其他，状态改为“审批已完成”，如果申请单流向为“合同”，状态改为“订单待收货”。   
        /// </summary>
        /// <param name="CurrentUser">当前登录人</param>
        /// <param name="generalModel">申请单对象</param>
        /// <returns></returns>
        public static string 采购总监审批通过(Employee CurrentUser, ref GeneralInfo generalModel, string majordomoUndoValue, System.Web.UI.Page Page)
        {
            string DelegateUsers = ",";
            IList<ESP.Framework.Entity.AuditBackUpInfo> delegates = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(Convert.ToInt32(CurrentUser.SysID));
            foreach (ESP.Framework.Entity.AuditBackUpInfo model in delegates)
            {
                DelegateUsers += model.UserID.ToString() + ",";
            }

            string Msg = "";

            //验证项目金额
            //if (!申请单验证规则.申请单修改金额提交验证(generalModel))
            //{
            //    return "项目成本金额不足，无法审批通过，请检查！";
            //}

            设置PRType(ref generalModel);
            //如果是个人的pr单，则将该单转向至李彦娥出
            if (generalModel.OperationType == State.OperationTypePri && generalModel.PRType != (int)PRTYpe.MediaPR && generalModel.PRType != (int)PRTYpe.PR_MediaFA && generalModel.PRType != (int)PRTYpe.PR_PriFA)
            {
                generalModel.PRType = (int)PRTYpe.PrivatePR;
            }
            //如果是DVD，直接改采购总监级/状态
            //if (CurrentUser.SysID == AuditorId.ToString())
            //{
            //    ESP.Framework.Entity.EmployeeInfo purchaseAuditor = null;
            //    purchaseAuditor = ESP.Framework.BusinessLogic.EmployeeManager.Get(AuditorId);
            //    generalModel.purchaseAuditor = purchaseAuditor.UserID;
            //    generalModel.purchaseAuditorName = purchaseAuditor.FullNameCN;
            if (generalModel.PRType == (int)PRTYpe.MediaPR || generalModel.PRType == (int)PRTYpe.PR_MediaFA || generalModel.PRType == (int)PRTYpe.MPPR)//如果pr单由3000以上媒介单生成，应该由媒介总监再次审批
            {
                generalModel.status = State.order_mediaAuditWait;//待媒介审批
            }
            else
            {
                generalModel.status = State.order_ok;
            }

            //如果pr流向为合同 PR， 并且审批后状态为order_ok。则直接设置pr状态为order_confirm,可以直接收货。
            if (generalModel.Requisitionflow == State.requisitionflow_toO)
            {
                generalModel.status = State.order_sended;
            }
            else
            {
                generalModel.status = State.order_confirm;
            }

            // }

            //如果是苏艳军审核大于5w的pr单，则不变pr单状态，审核人设置成david.duan。其他情况按初始设定

            //if (generalModel.totalprice > 100000 && (int.Parse(CurrentUser.SysID) == AuditorId2) && DelegateUsers.IndexOf("," + AuditorId.ToString() + ",") < 0)
            //{
            //    ESP.Framework.Entity.EmployeeInfo purchaseAuditor = null;
            //    purchaseAuditor = ESP.Framework.BusinessLogic.EmployeeManager.Get(AuditorId);
            //    generalModel.purchaseAuditor = purchaseAuditor.UserID;
            //    generalModel.purchaseAuditorName = purchaseAuditor.FullNameCN;
            //}
            //else
            //{
            //    if (generalModel.PRType == (int)PRTYpe.MediaPR || generalModel.PRType == (int)PRTYpe.PR_MediaFA || generalModel.PRType == (int)PRTYpe.MPPR)//如果pr单由3000以上媒介单生成，应该由媒介总监再次审批
            //    {
            //        generalModel.status = State.order_mediaAuditWait;//待媒介审批
            //    }
            //    else
            //    {
            //        generalModel.status = State.order_ok;
            //    }

            //    //如果pr流向为合同 PR， 并且审批后状态为order_ok。则直接设置pr状态为order_confirm,可以直接收货。
            //    if (generalModel.Requisitionflow == State.requisitionflow_toO)
            //    {
            //        generalModel.status = State.order_sended;
            //    }
            //    else
            //    {
            //        generalModel.status = State.order_confirm;
            //    }
            //}
            if (generalModel.Requisitionflow == State.requisitionflow_toO)
            {
                generalModel.order_committime = DateTime.Now;
                generalModel.orderid = generalModel.PrNo.Replace("PR", "PO");
            }
            generalModel.order_audittime = DateTime.Now;
            变更申请单号(ref generalModel, majordomoUndoValue, Page);
            return Msg;
        }

        /// <summary>
        /// 采购总监审批驳回(状态变更为“采购总监审批驳回”）
        /// </summary>
        /// <param name="CurrentUser">当前登录人</param>
        /// <param name="generalModel">申请单对象</param>
        /// <returns></returns>
        public static string 采购总监审批驳回(Employee CurrentUser, ref GeneralInfo generalModel)
        {
            string Msg = "";

            //ESP.Framework.Entity.OperationAuditManageInfo operationModel = null;

            //if (generalModel.Project_id != 0)
            //{
            //    operationModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByProjectId(generalModel.Project_id);
            //}
            //if (operationModel == null)
            //    operationModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByUserId(generalModel.requestor);

            //if (operationModel == null)
            //    operationModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(generalModel.Departmentid);

            //generalModel.purchaseAuditor = operationModel.PurchaseDirectorId;
            //generalModel.purchaseAuditorName = operationModel.PurchaseDirector;

            generalModel.status = State.requisition_return;
            generalModel.orderid = "";
            return Msg;
        }

        /// <summary>
        /// 广告媒体采买审批通过
        /// 二级广告媒体采买审批通过后，状态改为“审批已完成”
        /// </summary>
        /// <param name="CurrentUser">当前登录人</param>
        /// <param name="generalModel">申请单对象</param>
        /// <returns></returns>
        public static string 广告媒体采买审批通过(Employee CurrentUser, ref GeneralInfo generalModel)
        {
            string Msg = "";
            if (ESP.Purchase.BusinessLogic.GeneralInfoManager.IsProjectClosed(generalModel, true))
            {
                return "该项目号已经关闭，无法审核PR单！";
            }
            //验证项目金额
            if (!申请单验证规则.申请单修改金额提交验证(generalModel))
            {
                return "项目成本金额不足，无法审批通过，请检查！";
            }

            //当李雷审批通过后，流转至刘新华处，流新华审批通过后，审批结束
            //ESP.Framework.Entity.EmployeeInfo adAuditor = ESP.Framework.BusinessLogic.EmployeeManager.Get(ADAuditorId);
            //ESP.Framework.Entity.EmployeeInfo adAuditor2 = ESP.Framework.BusinessLogic.EmployeeManager.Get(ADAuditorId2);

            generalModel.status = State.order_ok;

            generalModel.adAuditTime = DateTime.Now;
            //AD审批同采购审批一致, PR-PO类型生成订单号
            if (generalModel.Requisitionflow == State.requisitionflow_toO)
            {
                generalModel.order_committime = DateTime.Now;
                generalModel.orderid = generalModel.PrNo.Replace("PR", "PO");
            }
            generalModel.order_audittime = DateTime.Now;
            return Msg;
        }

        //public static ESP.Framework.Entity.EmployeeInfo getADAuditor(GeneralInfo generalModel, bool setFirstAuditor, Employee CurrentUser)
        //{
        //    int GAuditor1 = int.Parse(ESP.Configuration.ConfigurationManager.SafeAppSettings["GAuditId1"]);
        //    int GAuditor2 = int.Parse(ESP.Configuration.ConfigurationManager.SafeAppSettings["GAuditId2"]);
        //    int GAuditor3 = int.Parse(ESP.Configuration.ConfigurationManager.SafeAppSettings["GAuditId3"]);
        //    int HAuditor1 = int.Parse(ESP.Configuration.ConfigurationManager.SafeAppSettings["HAuditId1"]);
        //    int HAuditor2 = int.Parse(ESP.Configuration.ConfigurationManager.SafeAppSettings["HAuditId2"]);
        //    int HAuditor3 = int.Parse(ESP.Configuration.ConfigurationManager.SafeAppSettings["HAuditId3"]);
        //    int ADAuditorId = int.Parse(ESP.Configuration.ConfigurationManager.SafeAppSettings["ADAuditorId"]);
        //    int ADAuditorId2 = int.Parse(ESP.Configuration.ConfigurationManager.SafeAppSettings["ADAuditorId2"]);
        //    decimal GUpPrice = decimal.Parse(ESP.Configuration.ConfigurationManager.SafeAppSettings["GUpPrice"]);
        //    decimal HUpPrice = decimal.Parse(ESP.Configuration.ConfigurationManager.SafeAppSettings["HUpPrice"]);

        //    int level1TypeID = OrderInfoManager.getTopTypeId(generalModel.id);
        //    string ADTypeID = "," + ESP.Configuration.ConfigurationManager.SafeAppSettings["ADTypeIDs"] + ",";
        //    int typeid = 0;
        //    IList<ESP.Purchase.Entity.OrderInfo> orderList = ESP.Purchase.BusinessLogic.OrderInfoManager.GetListByGeneralId(generalModel.id);
        //    if (orderList != null && orderList.Count > 0)
        //    {
        //        typeid = orderList[0].producttype;
        //    }
        //    if (typeid != 0 && ADTypeID.IndexOf("," + typeid.ToString() + ",") >= 0)
        //    {
        //        //广告服务类
        //        if (setFirstAuditor)
        //        {
        //            return ESP.Framework.BusinessLogic.EmployeeManager.Get(GAuditor1);
        //        }
        //        else
        //        {
        //            if (int.Parse(CurrentUser.SysID) == GAuditor1)
        //                return ESP.Framework.BusinessLogic.EmployeeManager.Get(GAuditor2);
        //            else if (int.Parse(CurrentUser.SysID) == GAuditor2)
        //            {
        //                return null;
        //            }
        //            else
        //                return null;
        //        }
        //    }
        //    else
        //    {
        //        if (level1TypeID == int.Parse(ESP.Configuration.ConfigurationManager.SafeAppSettings["HDYWId"]))
        //        {
        //            //互动业务
        //            if (setFirstAuditor)
        //            {
        //                return ESP.Framework.BusinessLogic.EmployeeManager.Get(HAuditor1);
        //            }
        //            else
        //            {
        //                if (int.Parse(CurrentUser.SysID) == HAuditor1)
        //                    return ESP.Framework.BusinessLogic.EmployeeManager.Get(HAuditor2);
        //                else
        //                    return null;
        //            }
        //        }
        //        else
        //        {
        //            //其他
        //            if (setFirstAuditor)
        //            {
        //                return ESP.Framework.BusinessLogic.EmployeeManager.Get(ADAuditorId);
        //            }
        //            else
        //            {
        //                if (int.Parse(CurrentUser.SysID) == ADAuditorId && generalModel.totalprice >= 100000)
        //                    return ESP.Framework.BusinessLogic.EmployeeManager.Get(ADAuditorId2);
        //                else
        //                    return null;
        //            }
        //        }
        //    }
        //}

        /// <summary>
        /// 广告媒体采买审批驳回(状态变更为“采购审核驳回”)
        /// </summary>
        /// <param name="CurrentUser">当前登录人</param>
        /// <param name="generalModel">申请单对象</param>
        /// <returns></returns>
        public static string 广告媒体采买审批驳回(Employee CurrentUser, ref GeneralInfo generalModel)
        {
            string Msg = "";
            generalModel.adAuditTime = DateTime.Now;
            generalModel.status = State.requisition_return;
            return Msg;
        }

        /// <summary>
        /// 媒介审批通过
        /// PRType为PR_MediaFA(稿件费用大于3000生成)，状态改为“审批已完成”。申请单流向为合同，状态改为“订单待收货”
        /// PRType为MediaPR(媒介申请),状态改为“媒介审批通过”，其他，状态改为“审批已完成”
        /// </summary>
        /// <param name="CurrentUser">当前登录人</param>
        /// <param name="generalModel">申请单对象</param>
        /// <returns></returns>
        public static string 媒介审批通过(Employee CurrentUser, ref GeneralInfo generalModel)
        {
            string Msg = "";
            if (ESP.Purchase.BusinessLogic.GeneralInfoManager.IsProjectClosed(generalModel, true))
            {
                return "该项目号已经关闭，无法审核PR单！";
            }
            //验证项目金额
            if (!申请单验证规则.申请单修改金额提交验证(generalModel))
            {
                return "项目成本金额不足，无法审批通过，请检查！";
            }

            generalModel.status = State.order_confirm;

            generalModel.prMediaAuditTime = DateTime.Now;

            //媒介审批同采购审批一致, PR-PO类型生成订单号
            if (generalModel.Requisitionflow == State.requisitionflow_toO)
            {
                generalModel.order_committime = DateTime.Now;
                generalModel.orderid = generalModel.PrNo.Replace("PR", "PO");
            }
            generalModel.order_audittime = DateTime.Now;
            return Msg;
        }

        /// <summary>
        /// 媒介审批驳回
        /// PRType为PR_TMP1，状态改为“采购总监审批驳回”，其他，状态改为“采购审核驳回”
        /// </summary>
        /// <param name="CurrentUser">当前登录人</param>
        /// <param name="generalModel">申请单对象</param>
        /// <returns></returns> 
        public static string 媒介审批驳回(Employee CurrentUser, ref GeneralInfo generalModel)
        {
            string Msg = "";
            generalModel.prMediaAuditTime = DateTime.Now;
            if (generalModel.PRType != (int)PRTYpe.PR_TMP1)
            {
                generalModel.status = State.requisition_return;
            }
            else
            {
                generalModel.status = State.order_return;
            }
            return Msg;
        }

        /// <summary>
        /// 申请人手动确认申请单，状态变更为“订单待收货”
        /// </summary>
        /// <param name="CurrentUser">当前登录人</param>
        /// <param name="generalModel">申请单对象</param>
        /// <returns></returns>
        public static string 申请人手动确认PR(Employee CurrentUser, ref GeneralInfo generalModel)
        {
            string Msg = "";

            //验证项目金额
            if (!申请单验证规则.申请单修改金额提交验证(generalModel))
            {
                return "项目成本金额不足，无法确认申请单，请检查！";
            }

            generalModel.status = State.order_confirm;
            return Msg;
        }

        /// <summary>
        /// 申请人撤销PR，状态变更为“申请单保存”
        /// </summary>
        /// <param name="CurrentUser">当前登录人</param>
        /// <param name="generalModel">申请单对象</param>
        /// <returns></returns>
        public static string 申请人撤销PR(Employee CurrentUser, ref GeneralInfo generalModel)
        {
            string Msg = "";
            generalModel.status = State.requisition_save;
            return Msg;
        }

        /// <summary>
        /// 物料审核人撤销PR
        /// 如果当前登陆人是申请单的初审人，则撤销至集团物料审核
        /// 如果当前登陆人是申请单的分公司审核人，则撤销至分公司物料审核
        /// </summary>
        /// <param name="CurrentUser">当前登录人</param>
        /// <param name="generalModel">申请单对象</param>
        /// <returns></returns>
        public static string 物料审核人撤销PR(Employee CurrentUser, ref GeneralInfo generalModel)
        {
            string Msg = "";
            if (CurrentUser.SysID == generalModel.first_assessor.ToString()) //如果当前登陆人是申请单的初审人，则撤销至集团物料审核
            {
                generalModel.status = State.requisition_commit;
            }
            if (CurrentUser.SysID == generalModel.Filiale_Auditor.ToString()) //如果当前登陆人是申请单的分公司审核人，则撤销至分公司物料审核
            {
                generalModel.status = State.requisition_temporary_commit;
            }
            return Msg;
        }

        /// <summary>
        /// 给供应商发送申请单确认邮件，状态更改为“订单待确认”
        /// </summary>
        /// <param name="CurrentUser">当前登录人</param>
        /// <param name="generalModel">申请单对象</param>
        /// <returns></returns>
        public static string 供应商邮件发送(Employee CurrentUser, ref GeneralInfo generalModel)
        {
            string Msg = "";

            //验证项目金额
            //if (!申请单验证规则.申请单修改金额提交验证(generalModel))
            //{
            //    return "项目成本金额不足，无法发送邮件，请检查！";
            //}

            generalModel.status = State.order_sended;
            return Msg;
        }

        /// <summary>
        /// 采购确认订单,状态更改为“待收货”
        /// </summary>
        /// <param name="CurrentUser">当前登录人</param>
        /// <param name="generalModel">申请单对象</param>
        /// <returns></returns>
        public static string 采购确认订单(Employee CurrentUser, ref GeneralInfo generalModel)
        {
            string Msg = "";

            //验证项目金额
            if (!申请单验证规则.申请单修改金额提交验证(generalModel))
            {
                return "项目成本金额不足，无法确认订单，请检查！";
            }

            generalModel.status = State.order_confirm;
            return Msg;
        }

        /// <summary>
        /// 采购撤销订单
        /// 申请单类型为PR_TMP1( 临时状态,物料审核人李彦娥,无采购总监审批,媒介总监审批,不经过分公司)，状态变更为“待媒介审批”
        /// 申请单类型为ADPR（ADPR申请），状态变更为“待媒介广告采买总监审批”
        /// 其他类型，状态变更为“待采购总监审批”
        /// </summary>
        /// <param name="CurrentUser">当前登录人</param>
        /// <param name="generalModel">申请单对象</param>
        /// <returns></returns>
        public static string 采购撤销订单(Employee CurrentUser, ref GeneralInfo generalModel)
        {
            string Msg = "";
            //if (int.Parse(CurrentUser.SysID) == AuditorId)
            //    generalModel.isMajordomoUndo = true;
            //int purchaseAuditorId2 = Convert.ToInt32(ESP.Configuration.ConfigurationManager.SafeAppSettings["AuditorId2"].ToString());
            //int purchaseAuditorId = Convert.ToInt32(ESP.Configuration.ConfigurationManager.SafeAppSettings["AuditorId"].ToString());
            //ESP.Compatible.Employee emp = new Employee(purchaseAuditorId2);
            //ESP.Compatible.Employee empD = new Employee(purchaseAuditorId);
            //设置媒介审批人
            // ESP.Framework.Entity.EmployeeInfo mediaAuditor = ESP.Framework.BusinessLogic.EmployeeManager.Get(MediaAuditorId);
            //设置媒体广告采买审批人
            //ESP.Framework.Entity.EmployeeInfo adAuditor = ESP.Framework.BusinessLogic.EmployeeManager.Get(ADAuditorId);
            // ESP.Framework.Entity.EmployeeInfo nextADAuditor = getADAuditor(generalModel, true, CurrentUser);
            switch (generalModel.PRType)
            {
                case (int)PRTYpe.CommonPR:
                case (int)PRTYpe.PrivatePR:
                case (int)PRTYpe.PR_TMP2:
                //generalModel.status = State.order_commit;
                //当前如果是DVD,把采购审批人也改为DVD
                //if (CurrentUser.SysID == purchaseAuditorId.ToString())
                //{
                //    generalModel.purchaseAuditor = purchaseAuditorId2;
                //    generalModel.purchaseAuditorName = emp.Name;
                //}
                // break;
                case (int)PRTYpe.MPPR:
                case (int)PRTYpe.PR_MediaFA:
                case (int)PRTYpe.PR_PriFA:
                case (int)PRTYpe.PR_TMP1:
                case (int)PRTYpe.MediaPR:
                // generalModel.status = State.order_mediaAuditWait;
                //generalModel.mediaAuditor = mediaAuditor.UserID;
                //generalModel.mediaAuditorName = mediaAuditor.Username;
                //break;
                case (int)PRTYpe.ADPR:
                //generalModel.status = State.order_ADAuditWait;
                //generalModel.adAuditor = nextADAuditor.UserID;
                //generalModel.adAuditorName = nextADAuditor.Username;
                //break;
                default:
                    generalModel.status = State.order_commit;
                    break;
            }
            return Msg;
        }

        /// <summary>
        /// 供应商确认订单,状态变更为“订单待收货”
        /// </summary>
        /// <param name="generalModel"></param>
        /// <returns></returns>
        public static void 供应商确认订单(ref GeneralInfo generalModel)
        {
            generalModel.status = State.order_confirm;
        }

        /// <summary>
        /// 申请单收货中，状态更改为“收货中”
        /// </summary>
        /// <param name="CurrentUser">当前登录人</param>
        /// <param name="generalModel">申请单对象</param>
        /// <returns></returns>
        public static string 申请单收货中(Employee CurrentUser, ref GeneralInfo generalModel)
        {
            string Msg = "";

            //验证项目金额
            //if (!申请单验证规则.申请单修改金额提交验证(generalModel))
            //{
            //    return "项目成本金额不足，无法收货，请检查！";
            //}

            //var operationModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByUserId(generalModel.requestor);

            ESP.Framework.Entity.OperationAuditManageInfo operationModel = null;

            if (generalModel.Project_id != 0)
            {
                operationModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByProjectId(generalModel.Project_id);
            }
            if (operationModel == null)
                operationModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByUserId(generalModel.requestor);

            if (operationModel == null)
                operationModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(generalModel.Departmentid);

            if (generalModel.totalprice > 5000 && operationModel.AppendReceiverId != 0)
            {
                generalModel.appendReceiver = operationModel.AppendReceiverId;
                generalModel.appendReceiverName = operationModel.AppendReceiver;
            }


            generalModel.status = (int)ESP.Purchase.Common.State.requisition_recipienting;
            return Msg;
        }

        /// <summary>
        /// 申请单完成收货，状态更改为“已完成收货”
        /// </summary>
        /// <param name="CurrentUser">当前登录人</param>
        /// <param name="generalModel">申请单对象</param>
        /// <returns></returns>
        public static string 申请单完成收货(Employee CurrentUser, ref GeneralInfo generalModel)
        {
            string Msg = "";

            //验证项目金额
            //if (!申请单验证规则.申请单修改金额提交验证(generalModel))
            //{
            //    return "项目成本金额不足，无法收货，请检查！";
            //}
            //var operationModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByUserId(generalModel.requestor);

            ESP.Framework.Entity.OperationAuditManageInfo operationModel = null;

            if (generalModel.Project_id != 0)
            {
                operationModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByProjectId(generalModel.Project_id);
            }
            if (operationModel == null)
                operationModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByUserId(generalModel.requestor);

            if (operationModel == null)
                operationModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(generalModel.Departmentid);

            if (generalModel.totalprice > 5000 && operationModel.AppendReceiverId != 0)
            {
                generalModel.appendReceiver = operationModel.AppendReceiverId;
                generalModel.appendReceiverName = operationModel.AppendReceiver;
            }

            generalModel.status = State.requisition_recipiented;
            return Msg;
        }

        /// <summary>
        /// 申请单撤销收货
        /// 如申请单还存在其他收货，申请单状态变更为“收货中”。不存在，申请单状态变更为“待收货”
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="generalModel"></param>
        public static void 申请单撤销收货(System.Data.SqlClient.SqlTransaction trans, ref GeneralInfo generalModel)
        {
            if (ESP.Purchase.DataAccess.RecipientDataHelper.getRecipientCountByGid(generalModel.id, trans) > 0)
                generalModel.status = ESP.Purchase.Common.State.requisition_recipienting;
            else
                generalModel.status = ESP.Purchase.Common.State.order_confirm;
        }

        /// <summary>
        /// 媒体稿费对私处理，状态变更为“媒体稿费/对私李彦娥处理中”
        /// </summary>
        /// <param name="generalModel"></param>
        public static void 媒体稿费对私处理(ref GeneralInfo generalModel)
        {
            generalModel.status = ESP.Purchase.Common.State.requisition_MediaOperating;//处理中
        }

        /// <summary>
        /// 申请单完全付款
        /// 在[P_FinallyPaymentByFinance]存储过程中实现
        /// </summary>
        public static void 申请单完全付款()
        {
            //在[P_FinallyPaymentByFinance]存储过程中实现
        }

        /// <summary>
        /// 申请单停止，状态改为“申请单停止”
        /// </summary>
        /// <param name="CurrentUser"></param>
        /// <param name="generalModel"></param>
        public static void 申请单停止(Employee CurrentUser, ref GeneralInfo generalModel)
        {
            generalModel.status = State.requisition_Stop;
        }

        /// <summary>
        /// 设置PRType
        /// </summary>
        /// <param name="generalModel">申请单对象</param>
        public static void 设置PRType(ref GeneralInfo generalModel)
        {
            int typeoperationflow = OrderInfoManager.getTypeOperationFlow(generalModel.id);
            if (generalModel.PRType != (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA && generalModel.PRType != (int)ESP.Purchase.Common.PRTYpe.PR_PriFA)
            {
                if (generalModel.OperationType == State.OperationTypePri)
                {
                    if (!generalModel.HaveInvoice)
                    {
                        if (typeoperationflow == State.typeoperationflow_Media)
                            generalModel.PRType = (int)PRTYpe.MediaPR;
                        else
                            generalModel.PRType = (int)PRTYpe.PrivatePR;
                    }
                    else
                    {
                        if (typeoperationflow == State.typeoperationflow_MP)
                            generalModel.PRType = (int)PRTYpe.MPPR;
                        else
                            generalModel.PRType = (int)PRTYpe.CommonPR;
                    }
                }
                else
                {
                    if (generalModel.PRType != (int)PRTYpe.PR_OtherAdvertisement && generalModel.PRType != (int)PRTYpe.PR_OtherMedia)
                    {
                        if (typeoperationflow == State.typeoperationflow_Mormal)
                            generalModel.PRType = (int)PRTYpe.CommonPR;
                        else if (typeoperationflow == State.typeoperationflow_Media)
                            generalModel.PRType = (int)PRTYpe.MediaPR;
                        else if (typeoperationflow == State.typeoperationflow_AD)
                            generalModel.PRType = (int)PRTYpe.ADPR;
                        else if (typeoperationflow == State.typeoperationflow_MFA)
                            generalModel.PRType = (int)PRTYpe.PR_MediaFA;
                        else if (typeoperationflow == State.typeoperationflow_MP)
                            generalModel.PRType = (int)PRTYpe.MPPR;
                        else if (typeoperationflow == State.typeoperationflow_TMP1)
                            generalModel.PRType = (int)PRTYpe.PR_TMP1;
                        else if (typeoperationflow == State.typeoperationflow_TMP2)
                            generalModel.PRType = (int)PRTYpe.PR_TMP2;
                    }
                }
            }
        }

        /// <summary>
        /// 变更申请单号
        /// </summary>
        /// <param name="generalModel"></param>
        /// <param name="majordomoUndoValue"></param>
        /// <param name="Page"></param>
        public static void 变更申请单号(ref GeneralInfo generalModel, string majordomoUndoValue, System.Web.UI.Page Page)
        {
            if (generalModel.isMajordomoUndo && majordomoUndoValue == "yes")
            {
                string orderId = generalModel.PrNo.Replace("PR", "PO");
                //被总监驳回的pr单，如果需要变更pr号，则生成新的pr号
                if (generalModel.PrNo.Contains("-"))
                    generalModel.PrNo = generalModel.PrNo.Split('-')[0] + "-" + (int.Parse(generalModel.PrNo.Split('-')[1]) + 1).ToString();
                else
                    generalModel.PrNo = generalModel.PrNo + "-1";
                generalModel.isMajordomoUndo = false;
                ESP.Purchase.BusinessLogic.SendMailHelper.PrNoChangedMail(generalModel, orderId, Page.Request, Page.Server);
            }
            else
            {
                if (generalModel.isMajordomoUndo)
                {
                    generalModel.isMajordomoUndo = false;
                }
            }
        }

        /// <summary>
        /// 获取当前登录人的顶级部门
        /// </summary>
        /// <param name="CurrentUser">人员ID</param>
        /// <returns></returns>
        public static string 获得顶级部门(int userId)
        {
            ESP.HumanResource.Entity.EmployeesInPositionsInfo positionModel = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetModel(userId);
            string nodename = "总部";
            if (positionModel != null)
            {
                //    string level = dtdep[0].Level.ToString();
                //    if (level == "1")
                //    {
                //        nodename = dtdep[0].NodeName;
                //    }
                //    else if (level == "2")
                //    {
                //        ESP.Compatible.Department dep = ESP.Compatible.DepartmentManager.GetDepartmentByPK(dtdep[0].UniqID);
                //        nodename = dep.Parent.DepartmentName;

                //    }
                //    else if (level == "3")
                //    {
                //        ESP.Compatible.Department dep = ESP.Compatible.DepartmentManager.GetDepartmentByPK(dtdep[0].UniqID);
                //        ESP.Compatible.Department dep2 = ESP.Compatible.DepartmentManager.GetDepartmentByPK(dep.Parent.UniqID);
                //        nodename = dep2.Parent.DepartmentName;
                //    }
                ESP.Framework.Entity.DepartmentInfo deptModel = ESP.Framework.BusinessLogic.DepartmentManager.Get(positionModel.DepartmentID);
                if (!string.IsNullOrEmpty(deptModel.Description))
                {
                    nodename = ESP.Purchase.Common.State.WorkCity[int.Parse(deptModel.Description)];
                }
            }
            return nodename;
        }
    }

    public class 申请单业务检查
    {
        /// <summary>
        /// 申请单提交
        /// （1）是否添加采购物品
        /// （2）项目号是否提交
        /// （3）采购总额是否超过第三方采购成本预算
        /// （4）帐期金额是否等于采购物品总额
        /// </summary>
        /// <param name="generalModel"></param>
        /// <returns></returns>
        public static string 申请单提交(GeneralInfo generalModel)
        {
            if (OrderInfoManager.GetListByGeneralId(generalModel.id).Count < 1)
                return "请添加采购物品！";


            if (generalModel.PRType != (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA && generalModel.PRType != (int)ESP.Purchase.Common.PRTYpe.PR_PriFA)
            {
                if (generalModel.Project_id > 0)
                {
                    ESP.Finance.Entity.ProjectInfo projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(generalModel.Project_id);
                    if (projectModel.Status != (int)ESP.Finance.Utility.Status.FinanceAuditComplete)
                    {
                        if (projectModel.Status == (int)ESP.Finance.Utility.Status.ProjectPreClose)
                        {
                            if (ESP.Purchase.BusinessLogic.GeneralInfoManager.IsProjectClosed(generalModel, false))
                                return "该项目号已经预关闭，请联系项目负责人协调财务核查项目状态，项目打开后才可申请PR单！";
                        }
                        else if (projectModel.Status == (int)ESP.Finance.Utility.Status.ProjectClosed)
                        {
                            return "该项目号已经正式关闭，无法申请PR单！";
                        }

                        return "该项目号变更中，请联系项目负责人跟进项目审批，审批完成后可继续申请PR单！";
                    }

                }


            }
            //if (!ESP.Purchase.BusinessLogic.GeneralInfoManager.contrastPrice(generalModel.id, 0, 0))
            //    return "采购物品总金额已经超过第三方采购成本预算！";
            //if (!申请单验证规则.PRSubmit(generalModel))
            //{
            //    return "项目成本金额不足，无法提交，请检查！";
            //}
            if (PaymentPeriodManager.getPaymentSum(generalModel.id, State.PaymentStatus_save) != OrderInfoDataHelper.GetTotalPrice(generalModel.id))
                return "付款账期总金额必须等于采购物品总金额！";

            if (generalModel.PRType == 1)
            {
                OrderInfo orderModel = OrderInfoManager.GetModelByGeneralID(generalModel.id);
                decimal ordersum = OrderInfoManager.GetListByGeneralId(generalModel.id).Sum(x => x.total);
                decimal mediasum = MediaOrderManager.GetModelList(" orderid =" + orderModel.id).Sum(x => x.TotalAmount ?? 0);
                if (generalModel.totalprice != ordersum || ordersum != mediasum)
                {
                    return "稿费明细与PR单总额不符,请检查填写是否正确！";
                }
            }
            return "";
        }

    }

    public class 申请单验证规则
    {
        public static bool 申请单提交验证(GeneralInfo generalModel)
        {

            return 申请单修改金额提交验证(generalModel);
        }

        private static void AddValue(Dictionary<int, decimal> m, int key, decimal val)
        {
            decimal cv;
            if (m.TryGetValue(key, out cv))
            {
                m[key] = cv + val;
            }
            else
            {
                m.Add(key, val);
            }
        }

        public static bool 申请单修改金额提交验证(GeneralInfo generalModel)
        {
            if (generalModel.Departmentid == 0)
            {
                return true;
            }
            if (generalModel.Project_id == 0 || generalModel.project_code.IndexOf("GM") > 0 || (generalModel.PRType == (int)PRTYpe.PR_PriFA || generalModel.PRType == (int)PRTYpe.PR_MediaFA))
            {
                return true;
            }
            ESP.Finance.Entity.ProjectInfo projectModel;
            IList<ESP.Purchase.Entity.GeneralInfo> PRList;
            IList<ReturnInfo> ReturnList;
            IList<ExpenseAccountDetailInfo> ExpenseDetails;
            Dictionary<int, int> TypeMappings;
            IList<ESP.Purchase.Entity.PaymentPeriodInfo> Periods;
            IList<ESP.Purchase.Entity.MediaPREditHisInfo> MediaPREditHises;
            List<ESP.Purchase.Entity.OrderInfo> Orders;
            List<CostRecordInfo> ExpenseRecords;
            List<CostRecordInfo> PRRecords;

            ESP.Finance.Entity.SupporterInfo supportModel = null;
            IList<ESP.Finance.Entity.SupporterCostInfo> supportCostList = null;
            IList<ESP.Finance.Entity.SupporterExpenseInfo> supportExpenseList = null;

            Dictionary<int, decimal> CostMappings = new Dictionary<int, decimal>();
            Dictionary<int, decimal> ExpenseMappings = new Dictionary<int, decimal>();

            decimal UsedCost;

            projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModel(generalModel.Project_id);
            if (projectModel == null || projectModel.GroupID == null || projectModel.GroupID.Value == 0)
                return true;
            if (projectModel.GroupID != generalModel.Departmentid)
            {
                supportModel = ESP.Finance.BusinessLogic.SupporterManager.GetList(string.Format("ProjectID={0} and GroupID={1}", generalModel.Project_id, generalModel.Departmentid))[0];
                supportCostList = ESP.Finance.BusinessLogic.SupporterCostManager.GetList(supportModel.SupportID, null, null);
                supportExpenseList = ESP.Finance.BusinessLogic.SupporterExpenseManager.GetList(" SupporterID=" + supportModel.SupportID);
            }

            var typelvl2 = ESP.Purchase.BusinessLogic.TypeManager.GetListLvl2();
            typelvl2[0] = "OOP";
            typelvl2[-1] = "[未知]";

            PRList = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModelList(generalModel.Project_id, generalModel.Departmentid);
            ReturnList = ESP.Finance.BusinessLogic.ReturnManager.GetList(generalModel.Project_id, generalModel.Departmentid);
            ExpenseDetails = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetList(ReturnList.Select(x => x.ReturnID).ToArray());
            TypeMappings = ESP.Purchase.BusinessLogic.TypeManager.GetTypeMappings();
            Periods = ESP.Purchase.BusinessLogic.PaymentPeriodManager.GetList(PRList.Select(x => x.id).ToArray());
            MediaPREditHises = ESP.Purchase.BusinessLogic.MediaPREditHisManager.GetModelByOldPRIDs(PRList.Where(x => x.PRType == 1 || x.PRType == 6).Select(x => x.id).ToArray());
            Orders = ESP.Purchase.BusinessLogic.OrderInfoManager.GetListByGeneralIds(PRList.Select(x => x.id).ToArray());

            ExpenseRecords = new List<CostRecordInfo>();
            PRRecords = new List<CostRecordInfo>();
            decimal ProjectSum = 0;

            var currentOrders = Orders.Where(x => x.general_id == generalModel.id);
            foreach (var currentOrder in currentOrders)
            {
                int currentOrderType = currentOrder.producttype;
                TypeMappings.TryGetValue(currentOrderType, out currentOrderType);

                if (supportModel != null)
                    ProjectSum += supportCostList.Where(x => x.CostTypeID == currentOrderType).Sum(x => x.Amounts);
                else
                    ProjectSum += projectModel.CostDetails.Where(x => x.CostTypeID == currentOrderType).Sum(x => x.Cost ?? 0);


                foreach (var pr in PRList)
                {
                    if (pr.PRType == (int)ESP.Purchase.Common.PRTYpe.PR_PriFA || pr.PRType == (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA)
                        continue;

                    decimal paid = 0;
                    var orders = Orders.Where(x => x.general_id == pr.id);

                    paid = ReturnList.Where(x => x.PRID == pr.id && x.ReturnStatus == 140).Sum(x => x.FactFee ?? 0);

                    foreach (var o in orders)
                    {
                        var costTypeId = o.producttype;
                        if (!TypeMappings.TryGetValue(costTypeId, out costTypeId)) costTypeId = 0;
                        if (costTypeId == currentOrderType)
                        {
                            if (o.FactTotal != 0)
                                AddValue(CostMappings, costTypeId, o.FactTotal);
                            else
                                AddValue(CostMappings, costTypeId, o.total);


                            CostRecordInfo detail = new CostRecordInfo()
                            {
                                PRID = pr.id,
                                PRNO = pr.PrNo,
                                SupplierName = pr.supplier_name,
                                Description = pr.project_descripttion,
                                Requestor = pr.requestorname,
                                GroupName = pr.requestor_group,
                                TypeID = o.producttype,
                                TypeName = typelvl2[costTypeId],
                                OrderTotal = o.total,
                                AppAmount = pr.totalprice,
                                PaidAmount = paid,
                                UnPaidAmount = pr.totalprice - paid,
                                CostPreAmount = 0
                            };

                            PRRecords.Add(detail);
                        }
                    }
                }

                foreach (var record in PRRecords)
                {
                    decimal v = 0M;
                    CostMappings.TryGetValue(record.TypeID, out v);
                    record.TypeTotalAmount = v;
                }

                var expenseReturns = ReturnList.Where(x => x.ReturnType == 30
                    || (x.ReturnType == 32 && x.ReturnStatus != 140)
                    || x.ReturnType == 31
                    || x.ReturnType == 37
                    || x.ReturnType == 33
                    || x.ReturnType == 40
                    || (x.ReturnType == 36 && x.ReturnStatus == 139)
                    || x.ReturnType == 35).ToList();
                foreach (var r in expenseReturns)
                {
                    var details = ExpenseDetails.Where(x => x.ReturnID == r.ReturnID && x.Status == 1).ToList();
                    foreach (var d in details)
                    {
                        if (d.CostDetailID == currentOrderType)
                        {
                            if (d.TicketStatus == 1)
                                continue;
                            var e = d.ExpenseMoney ?? 0;
                            if (e != 0)
                                AddValue(ExpenseMappings, d.CostDetailID ?? 0, e);

                            var typeid = d.CostDetailID ?? 0;

                            CostRecordInfo detail = new CostRecordInfo()
                            {
                                ReturnType = r.ReturnType ?? 0,
                                PRNO = r.ReturnCode,
                                Description = d.ExpenseDesc,
                                Requestor = r.RequestEmployeeName,
                                GroupName = r.DepartmentName,
                                TypeID = typeid,
                                TypeName = typelvl2[typeid],
                                AppAmount = e,
                                PaidAmount = (r.ReturnStatus == 140 || r.ReturnStatus == 139) ? e : 0,
                                UnPaidAmount = (r.ReturnStatus != 140 && r.ReturnStatus != 139) ? e : 0,
                                CostPreAmount = 0,
                                PNTotal = r.PreFee ?? 0
                            };
                            ExpenseRecords.Add(detail);
                        }
                    }
                }


                foreach (var record in ExpenseRecords)
                {
                    decimal v = 0M;
                    ExpenseMappings.TryGetValue(record.TypeID, out v);
                    record.TypeTotalAmount = v;
                }

                UsedCost = CostMappings.Sum(x => x.Value) + ExpenseMappings.Where(x => x.Key != 0).Sum(x => x.Value);

                if (ProjectSum < UsedCost)
                    return false;
            }
            return true;
        }

        public static bool PRSubmit(GeneralInfo generalModel)
        {
            if (generalModel.Departmentid == 0)
            {
                return true;
            }
            //if (generalModel.Project_id == 0 || generalModel.project_code.IndexOf("GM*") > 0 || generalModel.project_code.IndexOf("XYYH") > 0)
            if (generalModel.Project_id == 0)
            {
                return true;
            }

                ESP.Finance.Entity.ProjectInfo projectModel;
                IList<ESP.Purchase.Entity.GeneralInfo> PRList;
                IList<ReturnInfo> ReturnList;
                IList<ExpenseAccountDetailInfo> ExpenseDetails;
                Dictionary<int, int> TypeMappings;
                IList<ESP.Purchase.Entity.PaymentPeriodInfo> Periods;
                IList<ESP.Purchase.Entity.MediaPREditHisInfo> MediaPREditHises;
                List<ESP.Purchase.Entity.OrderInfo> Orders;
                List<CostRecordInfo> ExpenseRecords;
                List<CostRecordInfo> PRRecords;

                ESP.Finance.Entity.SupporterInfo supportModel = null;
                IList<ESP.Finance.Entity.SupporterCostInfo> supportCostList = null;
                IList<ESP.Finance.Entity.SupporterExpenseInfo> supportExpenseList = null;

                Dictionary<int, decimal> CostMappings = new Dictionary<int, decimal>();
                Dictionary<int, decimal> ExpenseMappings = new Dictionary<int, decimal>();

                decimal UsedCost;

                //取退款申请释放成本
                decimal refundTotal = 0;
                  
                projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModel(generalModel.Project_id);
                if (projectModel == null || projectModel.GroupID == null || projectModel.GroupID.Value == 0)
                    return true;
                if (projectModel.GroupID != generalModel.Departmentid)
                {
                    supportModel = ESP.Finance.BusinessLogic.SupporterManager.GetList(string.Format("ProjectID={0} and GroupID={1}", generalModel.Project_id, generalModel.Departmentid))[0];
                    supportCostList = ESP.Finance.BusinessLogic.SupporterCostManager.GetList(supportModel.SupportID, null, null);
                    supportExpenseList = ESP.Finance.BusinessLogic.SupporterExpenseManager.GetList(" SupporterID=" + supportModel.SupportID);
                    refundTotal = RefundManager.GetList(" projectId =" + supportModel.ProjectID + " and status not in(0,-1,1) and deptid=" + supportModel.GroupID).Sum(x => x.Amounts);
                }
                else
                {
                    refundTotal = RefundManager.GetList(" projectId =" + projectModel.ProjectId + " and status not in(0,-1,1) and deptid=" + projectModel.GroupID).Sum(x => x.Amounts);
                }

                var typelvl2 = ESP.Purchase.BusinessLogic.TypeManager.GetListLvl2();
                typelvl2[0] = "OOP";
                typelvl2[-1] = "[未知]";

                PRList = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModelList(generalModel.Project_id, generalModel.Departmentid);
                ReturnList = ESP.Finance.BusinessLogic.ReturnManager.GetList(generalModel.Project_id, generalModel.Departmentid);
                ExpenseDetails = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetList(ReturnList.Select(x => x.ReturnID).ToArray());
                TypeMappings = ESP.Purchase.BusinessLogic.TypeManager.GetTypeMappings();
                Periods = ESP.Purchase.BusinessLogic.PaymentPeriodManager.GetList(PRList.Select(x => x.id).ToArray());
                MediaPREditHises = ESP.Purchase.BusinessLogic.MediaPREditHisManager.GetModelByOldPRIDs(PRList.Where(x => x.PRType == 1 || x.PRType == 6).Select(x => x.id).ToArray());
                Orders = ESP.Purchase.BusinessLogic.OrderInfoManager.GetListByGeneralIds(PRList.Select(x => x.id).ToArray());

                ExpenseRecords = new List<CostRecordInfo>();
                PRRecords = new List<CostRecordInfo>();
                decimal ProjectSum = 0;

                var currentOrders = ESP.Purchase.BusinessLogic.OrderInfoManager.GetOrderList(" and general_id=" + generalModel.id);
              foreach (var currentOrder in currentOrders)
              {
                 
                  int currentOrderType = currentOrder.producttype;
                  TypeMappings.TryGetValue(currentOrderType, out currentOrderType);

                  if (supportModel != null)
                      ProjectSum = supportCostList.Where(x => x.CostTypeID == currentOrderType).Sum(x => x.Amounts);
                  else
                      ProjectSum = projectModel.CostDetails.Where(x => x.CostTypeID == currentOrderType).Sum(x => x.Cost ?? 0);


                  foreach (var pr in PRList)
                  {
                      if (pr.PRType == (int)ESP.Purchase.Common.PRTYpe.PR_PriFA || pr.PRType == (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA)
                          continue;

                      decimal paid = 0;
                      var orders = Orders.Where(x => x.general_id == pr.id);

                      paid = ReturnList.Where(x => x.PRID == pr.id && x.ReturnStatus == 140).Sum(x => x.FactFee ?? 0);

                      foreach (var o in orders)
                      {
                          var costTypeId = o.producttype;
                          if (!TypeMappings.TryGetValue(costTypeId, out costTypeId)) costTypeId = 0;
                          if (costTypeId == currentOrderType)
                          {
                              if (o.FactTotal != 0)
                                  AddValue(CostMappings, costTypeId, o.FactTotal);
                              else
                                  AddValue(CostMappings, costTypeId, o.total);


                              CostRecordInfo detail = new CostRecordInfo()
                              {
                                  PRID = pr.id,
                                  PRNO = pr.PrNo,
                                  SupplierName = pr.supplier_name,
                                  Description = pr.project_descripttion,
                                  Requestor = pr.requestorname,
                                  GroupName = pr.requestor_group,
                                  TypeID = o.producttype,
                                  TypeName = typelvl2[costTypeId],
                                  OrderTotal = o.total,
                                  AppAmount = pr.totalprice,
                                  PaidAmount = paid,
                                  UnPaidAmount = pr.totalprice - paid,
                                  CostPreAmount = 0
                              };

                              PRRecords.Add(detail);
                          }
                      }
                  }

                  foreach (var record in PRRecords)
                  {
                      decimal v = 0M;
                      CostMappings.TryGetValue(record.TypeID, out v);
                      record.TypeTotalAmount = v;
                  }

                  var expenseReturns = ReturnList.Where(x => x.ReturnType == 30
                      || (x.ReturnType == 32 && x.ReturnStatus != 140)
                      || x.ReturnType == 31
                      || x.ReturnType == 37
                      || x.ReturnType == 33
                      || x.ReturnType == 40
                      || (x.ReturnType == 36 && x.ReturnStatus == 139)
                      || x.ReturnType == 35).ToList();
                  foreach (var r in expenseReturns)
                  {
                      var details = ExpenseDetails.Where(x => x.ReturnID == r.ReturnID && x.Status == 1).ToList();
                      foreach (var d in details)
                      {
                          if (d.CostDetailID == currentOrderType)
                          {
                              if (d.TicketStatus == 1)
                                  continue;
                              var e = d.ExpenseMoney ?? 0;
                              if (e != 0)
                                  AddValue(ExpenseMappings, d.CostDetailID ?? 0, e);

                              var typeid = d.CostDetailID ?? 0;

                              CostRecordInfo detail = new CostRecordInfo()
                              {
                                  ReturnType = r.ReturnType ?? 0,
                                  PRNO = r.ReturnCode,
                                  Description = d.ExpenseDesc,
                                  Requestor = r.RequestEmployeeName,
                                  GroupName = r.DepartmentName,
                                  TypeID = typeid,
                                  TypeName = typelvl2[typeid],
                                  AppAmount = e,
                                  PaidAmount = (r.ReturnStatus == 140 || r.ReturnStatus == 139) ? e : 0,
                                  UnPaidAmount = (r.ReturnStatus != 140 && r.ReturnStatus != 139) ? e : 0,
                                  CostPreAmount = 0,
                                  PNTotal = r.PreFee ?? 0
                              };
                              ExpenseRecords.Add(detail);
                          }
                      }
                  }

                  foreach (var record in ExpenseRecords)
                  {
                      decimal v = 0M;
                      ExpenseMappings.TryGetValue(record.TypeID, out v);
                      record.TypeTotalAmount = v;
                  }

                  UsedCost = CostMappings.Sum(x => x.Value) + ExpenseMappings.Where(x => x.Key != 0).Sum(x => x.Value)-refundTotal;
                  UsedCost += currentOrder.total; 
                
                  if (ProjectSum < UsedCost)
                      return false;
              }
            return true;
        }
    }


}
