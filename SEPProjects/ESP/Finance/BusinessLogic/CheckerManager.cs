using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.BusinessLogic
{


    public static class CheckerManager
    {
        #region Project 成员

        public static decimal GetProjectOddAmount(int prjId)
        {
            decimal rate = Convert.ToDecimal(ESP.Finance.Configuration.ConfigurationManager.ProjectAmountOverRate);
            Entity.ProjectInfo prj = ProjectManager.GetModelWithOutDetailList(prjId);
            ESP.Finance.Entity.TaxRateInfo rateModel = null;
            if (prj.ContractTaxID != null && prj.ContractTaxID.Value != 0)
                rateModel = ESP.Finance.BusinessLogic.TaxRateManager.GetModel(prj.ContractTaxID.Value);

            decimal totalamount = prj.TotalAmount.Value * (1 + rate);
            decimal totalContractCost = ContractCostManager.GetTotalAmountByProject(prjId);
            decimal totalSupportCost = SupporterManager.GetTotalAmountByProject(prjId);
            decimal totalPrjExpense = ProjectExpenseManager.GetTotalExpense(prjId);

            decimal oddAmount = totalamount - GetTax(prj, rateModel) - totalContractCost - totalSupportCost - totalPrjExpense;//-TotalAmount*taxRate
            return Convert.ToDecimal(oddAmount);
        }

        public static bool CheckProjectOddAmount(int prjId, decimal amount)
        {
            return (GetProjectOddAmount(prjId) >= amount);
        }


        /// <summary>
        /// 验证项目成本费用
        /// </summary>
        /// <param name="prjId"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static bool CheckProjectCost(int prjId, decimal amount, decimal excludeAmount)
        {
            // decimal rate = Convert.ToDecimal(ESP.Finance.Configuration.ConfigurationManager.ProjectAmountOverRate);
            Entity.ProjectInfo prj = ProjectManager.GetModelWithOutDetailList(prjId);
            ESP.Finance.Entity.TaxRateInfo rateModel = null;
            if (prj.ContractTaxID != null && prj.ContractTaxID != 0)
                rateModel = ESP.Finance.BusinessLogic.TaxRateManager.GetModel(prj.ContractTaxID.Value);

            decimal totalamount = prj.TotalAmount.Value;
            //decimal totalContractCost = ContractCostManager.GetTotalAmountByProject(prjId);
            decimal totalSupportCost = SupporterManager.GetTotalAmountByProject(prjId);
            //decimal totalPrjExpense = ProjectExpenseManager.GetTotalExpense(prjId);
            decimal contractTax = prj.ContractTax == null ? 0 : prj.ContractTax.Value;
            decimal oddAmount = totalamount - GetTaxByVAT(prj, rateModel) - totalSupportCost + excludeAmount * contractTax / 100;//-TotalAmount*taxRate
            return (oddAmount >= amount);
        }

        public static bool CheckSupporterCost(int supId, decimal amount)
        {
            Entity.SupporterInfo sup = SupporterManager.GetModel(supId);
            decimal totalamount = sup.BudgetAllocation.Value;
            return (totalamount >= amount);
        }



        public static bool CheckProjectSubmit(int prjId)
        {
            if (prjId == 0) return false;
            Entity.ProjectInfo model = ProjectManager.GetModelWithOutDetailList(prjId);
            ESP.Finance.Entity.TaxRateInfo rateModel = ESP.Finance.BusinessLogic.TaxRateManager.GetModel(model.ContractTaxID.Value);

            decimal totalcost = ContractManager.GetTotalCostByProject(model.ProjectId);
            decimal totalSupporterAmount = SupporterManager.GetTotalAmountByProject(model.ProjectId);
            decimal taxRate = (model.ContractTax == null ? 0 : Convert.ToDecimal(model.ContractTax) / 100);
            decimal totalAmount = model.TotalAmount == null ? 0 : Convert.ToDecimal(model.TotalAmount);
            decimal totalServices = model.ContractServiceFee == null ? 0 : Convert.ToDecimal(model.ContractServiceFee);
            if (totalAmount - GetTax(model, rateModel) - totalcost - totalSupporterAmount - totalServices > 0)//change totalamount*taxrate
                return true;
            return false;
        }

        #endregion

        #region supporter 成员



        public static decimal GetSupporterOddAmount(int supportId)
        {
            Entity.SupporterInfo supporter = SupporterManager.GetModel(supportId);
            Entity.ProjectInfo projectModel = ProjectManager.GetModelWithOutDetailList(supporter.ProjectID);
            Entity.TaxRateInfo rateModel = null;
            if (projectModel.ContractTaxID != null && projectModel.ContractTaxID.Value != 0)
            { 
            rateModel = TaxRateManager.GetModel(projectModel.ContractTaxID.Value);
            }

            decimal totalcost = SupporterCostManager.GetTotalCostBySupporter(supportId);
            decimal totalexpense = SupporterExpenseManager.GetTotalExpense(supportId);
            decimal retvalue = Convert.ToDecimal(supporter.BudgetAllocation - totalcost - totalexpense);
            
            decimal vatparam1 = 1;
            decimal tax = 0;
            if (rateModel != null)
            {
                vatparam1 = rateModel.VATParam1;
                tax = GetSupporterTaxByVAT(supporter, projectModel, rateModel);
            }
            if (projectModel.IsCalculateByVAT == 1 && rateModel!=null)
            {
                retvalue = (supporter.BudgetAllocation.Value) / vatparam1 - tax - totalcost - totalexpense;
            }
            return Convert.ToDecimal(retvalue.ToString("0.00"));

        }

        public static bool CheckSupporterOddAmount(int supportId, decimal cost)
        {
            return (cost <= GetSupporterOddAmount(supportId));
        }


        /// <summary>
        /// 检查支持方成本
        /// </summary>
        /// <param name="supportId"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static bool 支持方成本是否超总额(int supportId, decimal amount)
        {
            Entity.SupporterInfo supporter = SupporterManager.GetModel(supportId);
            return (supporter.BudgetAllocation >= amount);
        }

        #endregion


        #region service fee & tax 成员


        //public static decimal GetServiceFee(int prjId, int taxrateid)
        //{
        //    Entity.ProjectInfo prj = ProjectManager.GetModelWithOutDetailList(prjId);
        //    if (taxrateid == 0 && prj.ContractTaxID != null)
        //        taxrateid = prj.ContractTaxID.Value;
        //    ESP.Finance.Entity.TaxRateInfo rateModel = null;

        //    if (taxrateid != 0)
        //        rateModel = ESP.Finance.BusinessLogic.TaxRateManager.GetModel(taxrateid);

        //    decimal totalamount = prj.TotalAmount == null ? 0 : prj.TotalAmount.Value;
        //    decimal totalContractCost = ContractCostManager.GetTotalAmountByProject(prjId);
        //    decimal totalSupportCost = SupporterManager.GetTotalAmountByProject(prjId);
        //    decimal totalPrjExpense = ProjectExpenseManager.GetTotalExpense(prjId);
        //    decimal tax = 0;
        //    if (rateModel != null)
        //        tax = GetTax(prj, rateModel);

        //    decimal fee = totalamount - tax - totalContractCost - totalSupportCost - totalPrjExpense;//change totalamount * taxRate
        //    return Convert.ToDecimal(fee.ToString("0.00"));
        //}

        public static decimal GetServiceFee(ESP.Finance.Entity.ProjectInfo projectModel, ESP.Finance.Entity.TaxRateInfo rateModel)
        {
            decimal totalamount = projectModel.TotalAmount == null ? 0 : projectModel.TotalAmount.Value;
            decimal totalContractCost = ContractCostManager.GetTotalAmountByProject(projectModel.ProjectId);
            decimal totalSupportCost = SupporterManager.GetTotalAmountByProject(projectModel.ProjectId);
            decimal totalPrjExpense = ProjectExpenseManager.GetTotalExpense(projectModel.ProjectId);
            decimal totalMediaRebate = RebateRegistrationManager.GetList(" a.projectId =" + projectModel.ProjectId + " and a.status =" + (int)ESP.Finance.Utility.Common.RebateRegistration_Status.Audited).Sum(x => x.RebateAmount);
            decimal refundTotal = RefundManager.GetList(" projectId =" + projectModel.ProjectId+" and status not in(0,-1,1)").Sum(x=>x.Amounts);
            decimal tax = 0;
            if (rateModel == null)
            {
                if (projectModel.ContractTaxID != null && projectModel.ContractTaxID.Value!=0)
                {
                    rateModel = ESP.Finance.BusinessLogic.TaxRateManager.GetModel(projectModel.ContractTaxID.Value);
                }

            }
            
            tax = GetTax(projectModel, rateModel);

            decimal fee = totalamount - tax - totalContractCost - totalSupportCost - totalPrjExpense + refundTotal;//change totalamount * taxRate
            if (projectModel.isRecharge)
            {
                fee = totalamount - totalContractCost - totalSupportCost - totalPrjExpense + refundTotal;
                fee = fee - projectModel.CustomerRebate.Value + projectModel.Recharge.Value - projectModel.MediaCost.Value + totalMediaRebate;
            }
            return Convert.ToDecimal(fee.ToString("0.00"));
        }

        public static decimal GetServiceFeeByVAT(ESP.Finance.Entity.ProjectInfo projectModel, ESP.Finance.Entity.TaxRateInfo rateModel)
        {
            decimal totalamount = projectModel.TotalAmount == null ? 0 : projectModel.TotalAmount.Value;
            decimal totalContractCost = ContractCostManager.GetTotalAmountByProject(projectModel.ProjectId);
            decimal totalSupportCost = SupporterManager.GetTotalAmountByProject(projectModel.ProjectId);
            decimal totalPrjExpense = ProjectExpenseManager.GetTotalExpense(projectModel.ProjectId);
            decimal totalMediaRebate = RebateRegistrationManager.GetList(" a.projectId =" + projectModel.ProjectId+" and a.status ="+ (int)ESP.Finance.Utility.Common.RebateRegistration_Status.Audited).Sum(x=>x.RebateAmount);
            decimal refundTotal = RefundManager.GetList(" projectId =" + projectModel.ProjectId + " and status not in(0,-1,1)").Sum(x => x.Amounts);
            decimal tax = 0;

            if (rateModel == null && projectModel.ContractTaxID != null && projectModel.ContractTaxID.Value!=0)
            {
               rateModel=ESP.Finance.BusinessLogic.TaxRateManager.GetModel(projectModel.ContractTaxID.Value);
            }
            if (rateModel != null)
            {
                tax = GetTaxByVAT(projectModel, rateModel);
            }
            decimal vatparam1 = 1;
            if (rateModel != null)
                vatparam1 = rateModel.VATParam1;
            decimal fee = (totalamount - totalSupportCost) / vatparam1 - tax - totalContractCost - totalPrjExpense + refundTotal;
            if (projectModel.isRecharge)
            {
                fee = (totalamount - totalSupportCost) - totalContractCost - totalPrjExpense + refundTotal;
                //加媒体返点金额
                fee = fee - projectModel.CustomerRebate.Value + projectModel.Recharge.Value - projectModel.MediaCost.Value + totalMediaRebate;
            }
            return Convert.ToDecimal(fee.ToString("0.00"));
        }

        public static decimal GetHisModelFeeByVAT(ESP.Finance.Entity.ProjectInfo projectModel, ESP.Finance.Entity.TaxRateInfo rateModel)
        {
            decimal totalamount = projectModel.TotalAmount == null ? 0 : projectModel.TotalAmount.Value;
            decimal totalContractCost = ContractCostManager.GetTotalAmountByProject(projectModel.ProjectId);
            decimal totalSupportCost = SupporterManager.GetTotalAmountByProject(projectModel.ProjectId);
            decimal totalPrjExpense = ProjectExpenseManager.GetTotalExpense(projectModel.ProjectId);
            decimal tax = 0;

            if (rateModel == null && projectModel.ContractTaxID != null && projectModel.ContractTaxID.Value != 0)
            {
                rateModel = ESP.Finance.BusinessLogic.TaxRateManager.GetModel(projectModel.ContractTaxID.Value);
            }
            if (rateModel != null)
            {
                tax = GetTaxByVAT(projectModel, rateModel);
            }
            decimal vatparam1 = 1;
            if (rateModel != null)
                vatparam1 = rateModel.VATParam1;

            decimal fee = (totalamount - totalSupportCost) / vatparam1 - tax - totalContractCost - totalPrjExpense;
            if (projectModel.isRecharge)
            {
                fee = (totalamount - totalSupportCost) - totalContractCost - totalPrjExpense;
                fee = fee - projectModel.CustomerRebate.Value + projectModel.Recharge.Value - projectModel.MediaCost.Value;
            }
            return Convert.ToDecimal(fee.ToString("0.00"));
        }


        /// <summary>
        /// 判断是否是 不计算税金 的类型
        /// </summary>
        /// <param name="costType"></param>
        /// <returns></returns>
        public static bool isExclude(int costType)
        {
            List<int> list = ESP.Finance.Configuration.ConfigurationManager.ExcludeTaxFeeList;
            foreach (int i in list)
            {
                if (costType == i)
                    return true;
            }
            return false;
        }

        public static decimal GetTax(ESP.Finance.Entity.ProjectInfo prj, ESP.Finance.Entity.TaxRateInfo taxrate)
        {
            //如果是BD项目，无税金
            if (prj.ContractStatusName == ESP.Finance.Utility.ProjectType.BDProject || taxrate == null)
            {
                return 0;
            }
            //如果是无税项目,税金为零
            if (!string.IsNullOrEmpty(prj.ProjectCode) && prj.ProjectCode.ToUpper().EndsWith(Utility.ProjectExtention.C.ToString()))
            {
                return 0;
            }
            //如果客户是自己，无税金
            if (prj.CustomerCode == ESP.Finance.Configuration.ConfigurationManager.ShunYaShortEN)
            { return 0; }

            decimal totalAmount = prj.TotalAmount == null ? 0 : prj.TotalAmount.Value;
            decimal excludeAmount = 0;
            decimal TaxSupporterTotalAmount = 0;//已经支付税金的支持方的总金额
            IList<ESP.Finance.Entity.SupporterInfo> Supporters = ESP.Finance.BusinessLogic.SupporterManager.GetListByProject(prj.ProjectId, "", null);
            if (Supporters != null && Supporters.Count > 0)
            {
                TaxSupporterTotalAmount = Supporters.Where(n => n.TaxType == true).Sum(n => n.BudgetAllocation) ?? 0;
            }

            totalAmount -= TaxSupporterTotalAmount;

            //判断是否广告类发票
            if (prj.ContractTaxID != null && prj.ContractTaxID.Value > 0)
            {
                IList<ESP.Finance.Entity.ContractCostInfo> costdetails = ESP.Finance.BusinessLogic.ContractCostManager.GetListByProject(prj.ProjectId, "", null);
                if (taxrate != null)
                {
                    if (taxrate.BizTypeID == (int)Utility.InvoiceType.ADType)
                    {
                        if (costdetails != null && costdetails.Count > 0)
                        {
                            foreach (Entity.ContractCostInfo cost in costdetails)
                            {
                                if (isExclude(cost.CostTypeID ?? 0))//判断是否需要计算税金,需要计算的才累加
                                {
                                    excludeAmount += cost.Cost ?? 0;
                                }
                            }
                        }
                        return (totalAmount - excludeAmount) * taxrate.TaxRate.Value / 100;
                    }
                }
            }



            return taxrate.TaxRate.Value * totalAmount / 100;
        }

        public static decimal GetTaxByVAT(ESP.Finance.Entity.ProjectInfo prj, ESP.Finance.Entity.TaxRateInfo taxrate)
        {
            //如果是BD项目，无税金
            if (prj.ContractStatusName == ESP.Finance.Utility.ProjectType.BDProject || taxrate == null)
            {
                return 0;
            }
            //如果是无税项目,税金为零
            if (!string.IsNullOrEmpty(prj.ProjectCode) && prj.ProjectCode.ToUpper().EndsWith(Utility.ProjectExtention.C.ToString()))
            {
                return 0;
            }
            //如果客户是自己，无税金
            if (prj.CustomerCode == ESP.Finance.Configuration.ConfigurationManager.ShunYaShortEN)
            { return 0; }

            decimal totalAmount = prj.TotalAmount == null ? 0 : prj.TotalAmount.Value;
            decimal excludeAmount = 0;
            decimal TaxSupporterTotalAmount = 0;//已经支付税金的支持方的总金额
            IList<ESP.Finance.Entity.SupporterInfo> Supporters = ESP.Finance.BusinessLogic.SupporterManager.GetListByProject(prj.ProjectId, "", null);
            if (Supporters != null && Supporters.Count > 0)
            {
                TaxSupporterTotalAmount = Supporters.Sum(n => n.BudgetAllocation) ?? 0;
            }

            //判断是否广告类发票
            //if (prj.ContractTaxID != null && prj.ContractTaxID.Value > 0)

                IList<ESP.Finance.Entity.ContractCostInfo> costdetails = ESP.Finance.BusinessLogic.ContractCostManager.GetListByProject(prj.ProjectId, "", null);

                if (costdetails != null && costdetails.Count > 0)
                {
                    foreach (Entity.ContractCostInfo cost in costdetails)
                    {
                        if (isExclude(cost.CostTypeID ?? 0))//判断是否需要计算税金,需要计算的才累加
                        {
                            excludeAmount += cost.Cost ?? 0;
                        }
                    }

                }
            if(prj.isRecharge)
                excludeAmount += prj.Recharge.Value;

                return (totalAmount - TaxSupporterTotalAmount) / taxrate.VATParam1 * taxrate.VATParam2 * taxrate.VATParam3 + (totalAmount - TaxSupporterTotalAmount) / taxrate.VATParam1 * taxrate.VATParam4 + (totalAmount - TaxSupporterTotalAmount - excludeAmount) * taxrate.VATParam5;
        }

        public static decimal GetTaxWithSupporterByVAT(ESP.Finance.Entity.ProjectInfo prj, ESP.Finance.Entity.TaxRateInfo taxrate)
        {
            //如果是BD项目，无税金
            if (prj.ContractStatusName == ESP.Finance.Utility.ProjectType.BDProject || taxrate == null)
            {
                return 0;
            }
            //如果是无税项目,税金为零
            if (!string.IsNullOrEmpty(prj.ProjectCode) && prj.ProjectCode.ToUpper().EndsWith(Utility.ProjectExtention.C.ToString()))
            {
                return 0;
            }
            //如果客户是自己，无税金
            if (prj.CustomerCode == ESP.Finance.Configuration.ConfigurationManager.ShunYaShortEN)
            { return 0; }

            decimal totalAmount = prj.TotalAmount == null ? 0 : prj.TotalAmount.Value;
            decimal excludeAmount = 0;
           
            IList<ESP.Finance.Entity.ContractCostInfo> costdetails = ESP.Finance.BusinessLogic.ContractCostManager.GetListByProject(prj.ProjectId, "", null);

            if (costdetails != null && costdetails.Count > 0)
            {
                foreach (Entity.ContractCostInfo cost in costdetails)
                {
                    if (isExclude(cost.CostTypeID ?? 0))//判断是否需要计算税金,需要计算的才累加
                    {
                        excludeAmount += cost.Cost ?? 0;
                    }
                }

            }

            return (totalAmount ) / taxrate.VATParam1 * taxrate.VATParam2 * taxrate.VATParam3 + (totalAmount) / taxrate.VATParam1 * taxrate.VATParam4 + (totalAmount - excludeAmount) * taxrate.VATParam5;
        }

        public static decimal GetSupporterTaxByVAT(ESP.Finance.Entity.SupporterInfo supporter,ESP.Finance.Entity.ProjectInfo prj, ESP.Finance.Entity.TaxRateInfo taxrate)
        {
            //如果是BD项目，无税金
            if (prj.ContractStatusName == ESP.Finance.Utility.ProjectType.BDProject || taxrate == null)
            {
                return 0;
            }
            //如果是无税项目,税金为零
            if (!string.IsNullOrEmpty(prj.ProjectCode) && prj.ProjectCode.ToUpper().EndsWith(Utility.ProjectExtention.C.ToString()))
            {
                return 0;
            }
            //如果客户是自己，无税金
            if (prj.CustomerCode == ESP.Finance.Configuration.ConfigurationManager.ShunYaShortEN)
            { return 0; }

            decimal totalAmount = supporter.BudgetAllocation == null ? 0 : supporter.BudgetAllocation.Value;

            decimal adAmount = 0;
            decimal excludeAmount = 0;
             IList<ESP.Finance.Entity.ContractCostInfo> costdetails = ESP.Finance.BusinessLogic.ContractCostManager.GetListByProject(prj.ProjectId, "", null);

             if (costdetails != null && costdetails.Count > 0)
             {
                 foreach (Entity.ContractCostInfo cost in costdetails)
                 {
                     if (isExclude(cost.CostTypeID ?? 0))//判断是否需要计算税金,需要计算的才累加
                     {
                         excludeAmount += cost.Cost ?? 0;
                     }
                 }

             }

             if (excludeAmount > 0)
             {
                 adAmount = totalAmount * taxrate.VATParam5;
             }

            return totalAmount / taxrate.VATParam1 * taxrate.VATParam2 * taxrate.VATParam3 + totalAmount / taxrate.VATParam1 * taxrate.VATParam4 + adAmount;
        }

        #endregion

        #region invoice & payment 成员


        public static decimal GetInvoiceOddAmount(int InvoiceId)
        {
            if (InvoiceId == 0)
                return 0;
            Entity.InvoiceInfo invoice = InvoiceManager.GetModel(InvoiceId);
            decimal totalamount = invoice.InvoiceAmounts == null ? 0 : invoice.InvoiceAmounts.Value;
            decimal totalDetailAmount = InvoiceDetailManager.GetTotalAmountByInvoice(InvoiceId);
            decimal OddAmount = totalamount - totalDetailAmount;
            return OddAmount;
        }

        public static bool CheckInvoiceOddAmount(int InvoiceId, decimal amount)
        {
            if (InvoiceId == 0)
                return true;
            else
                return (GetInvoiceOddAmount(InvoiceId) >= amount);
        }


        public static decimal GetPaymentOddAmount(int PaymentID)
        {
            Entity.PaymentInfo payment = PaymentManager.GetModel(PaymentID);
            decimal totalamount = payment.PaymentBudget == null ? 0 : payment.PaymentBudget.Value;
            decimal totalDetailAmount = InvoiceDetailManager.GetTotalAmountByPayment(PaymentID);
            decimal OddAmount = totalamount - totalDetailAmount;
            return OddAmount;
        }
        public static bool CheckPaymentOddAmount(int PaymentID, decimal amount)
        {
            return (GetPaymentOddAmount(PaymentID) >= amount);
        }

        #endregion

        public static decimal GetRefundTotal(string projectCode, int departmentId)
        {
            return ESP.Finance.BusinessLogic.ReturnManager.GetTotalRefund(projectCode, departmentId);
        }

        /// <summary>
        /// 获得项目内已经使用的成本
        /// </summary>
        /// <param name="projectid"></param>
        /// <returns></returns>
        public static decimal GetOccupyCost(int projectid, int DepartmentID)
        {
            ESP.Finance.Entity.ProjectInfo projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(projectid);
            //已提交的非稿费/对私PR单列表
            List<ESP.Purchase.Entity.GeneralInfo> PRList = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModelList(" (project_code='" + projectModel.ProjectCode + "') and departmentid=" + DepartmentID.ToString() + " and  status not in(-1,0,2,21) and prtype not in(1,6,4,8)", new List<System.Data.SqlClient.SqlParameter>());
            List<ESP.Purchase.Entity.PaymentPeriodInfo> paymentList = null;
            ESP.Finance.Entity.ReturnInfo returnModel = null;
            decimal totalAmount = 0;
            if (PRList != null && PRList.Count > 0)
            {
                foreach (ESP.Purchase.Entity.GeneralInfo g in PRList)
                {
                    paymentList = ESP.Purchase.BusinessLogic.PaymentPeriodManager.GetModelList(" gid=" + g.id.ToString());
                    //因无法按成本项统计PR,只能根据帐期金额来统计PR的金额
                    //如果帐期已经申请PN，则按照PN金额，没有申请PN按帐期的预计金额
                    //如果PN实际付款金额为NULL，按照预计支付金额，否则按实际金额统计
                    if (paymentList != null && paymentList.Count > 0)
                    {
                        foreach (ESP.Purchase.Entity.PaymentPeriodInfo p in paymentList)
                        {
                            if (p != null && p.ReturnId != 0)
                            {
                                returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(p.ReturnId);
                                if (returnModel != null)
                                {
                                    if (returnModel.FactFee != null && returnModel.FactFee.Value != 0)
                                    {
                                        totalAmount += returnModel.FactFee.Value;
                                    }
                                    else
                                    {
                                        totalAmount += returnModel.PreFee.Value;
                                    }
                                }
                            }
                            else
                            {
                                totalAmount += p.expectPaymentPrice;
                            }
                        }
                    }
                }
            }

            //媒体稿费对私单据的成本统计
            PRList = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModelList(" (project_code='" + projectModel.ProjectCode + "') and departmentid= " + DepartmentID.ToString() + " and status not in(-1,0,2,21) and prtype in(1,6)", new List<System.Data.SqlClient.SqlParameter>());
            if (PRList != null && PRList.Count > 0)//获取所有媒体稿费对私单据
            {
                foreach (ESP.Purchase.Entity.GeneralInfo g in PRList)
                {
                    //检查李彦娥是否已经处理完毕
                    ESP.Purchase.Entity.MediaPREditHisInfo relationModel = ESP.Purchase.BusinessLogic.MediaPREditHisManager.GetModelByOldPRID(g.id);
                    if (relationModel == null)//尚未处理
                    {
                        //尚未处理，直接按PR单的帐期的预计金额统计
                        paymentList = ESP.Purchase.BusinessLogic.PaymentPeriodManager.GetModelList(" gid=" + g.id.ToString());
                        foreach (ESP.Purchase.Entity.PaymentPeriodInfo p in paymentList)
                        {
                            totalAmount += p.expectPaymentPrice;
                        }
                    }
                    else//已经处理
                    {
                        //处理后如果产生了新PR单
                        if (relationModel.NewPRId != null && relationModel.NewPRId.Value != 0)
                        {
                            //根据关联表检查处理后产生的新PR单
                            ESP.Purchase.Entity.GeneralInfo newModel = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(relationModel.NewPRId.Value);
                            //按新PR单的帐期统计金额
                            if (newModel != null)
                            {
                                paymentList = ESP.Purchase.BusinessLogic.PaymentPeriodManager.GetModelList(" gid=" + newModel.id.ToString());
                                foreach (ESP.Purchase.Entity.PaymentPeriodInfo p in paymentList)
                                {
                                    totalAmount += p.expectPaymentPrice;
                                }
                            }
                        }
                        //处理后如果产生了新PN单
                        if (relationModel.NewPNId != null && relationModel.NewPNId.Value != 0)
                        {
                            //按照新PN单的实际金额或预计金额统计
                            returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(relationModel.NewPNId.Value);
                            if (returnModel.FactFee != null && returnModel.FactFee.Value != 0)
                            {
                                totalAmount += returnModel.FactFee.Value;
                            }
                            else
                            {
                                totalAmount += returnModel.PreFee.Value;
                            }
                        }
                    }
                }
            }

            //异常关闭的PR单
            PRList = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModelList(" (project_code='" + projectModel.ProjectCode + "') and departmentid= " + DepartmentID.ToString() + " and status=21", new List<System.Data.SqlClient.SqlParameter>());
            if (PRList != null && PRList.Count > 0)
            {
                foreach (ESP.Purchase.Entity.GeneralInfo model in PRList)
                {
                    IList<ESP.Purchase.Entity.OrderInfo> orderList = ESP.Purchase.BusinessLogic.OrderInfoManager.GetListByGeneralId(model.id);
                    foreach (ESP.Purchase.Entity.OrderInfo rModel in orderList)
                    {
                        if (rModel.FactTotal != null && rModel.FactTotal != 0)
                        {
                            totalAmount += rModel.FactTotal;
                        }
                        else
                        {
                            totalAmount += rModel.total;
                        }
                    }
                }
            }
            return totalAmount;
        }
        /// <summary>
        /// 不包含当前提交的PR单
        /// </summary>
        /// <param name="projectid"></param>
        /// <param name="DepartmentID"></param>
        /// <param name="gid"></param>
        /// <returns></returns>
        public static decimal GetOccupyCost(int projectid, int DepartmentID, int gid)
        {
            ESP.Finance.Entity.ProjectInfo projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(projectid);
            //已提交的非稿费/对私PR单列表
            List<ESP.Purchase.Entity.GeneralInfo> PRList = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModelList(" (project_code='" + projectModel.ProjectCode + "') and departmentid=" + DepartmentID.ToString() + " and  status not in(-1,0,2,21) and id!=" + gid.ToString() + " and prtype not in(1,6,4,8)", new List<System.Data.SqlClient.SqlParameter>());
            List<ESP.Purchase.Entity.PaymentPeriodInfo> paymentList = null;
            ESP.Finance.Entity.ReturnInfo returnModel = null;
            List<ESP.Purchase.Entity.OrderInfo> orderlist = null;
            decimal orderFactTotal = 0;
            decimal totalAmount = 0;
            if (PRList != null && PRList.Count > 0)
            {
                foreach (ESP.Purchase.Entity.GeneralInfo g in PRList)
                {
                    orderFactTotal = 0;
                    paymentList = ESP.Purchase.BusinessLogic.PaymentPeriodManager.GetModelList(" gid=" + g.id.ToString());
                    //因无法按成本项统计PR,只能根据帐期金额来统计PR的金额
                    //如果帐期已经申请PN，则按照PN金额，没有申请PN按帐期的预计金额
                    //如果PN实际付款金额为NULL，按照预计支付金额，否则按实际金额统计

                    orderlist = ESP.Purchase.BusinessLogic.OrderInfoManager.GetListByGeneralId(g.id);
                    foreach (ESP.Purchase.Entity.OrderInfo order in orderlist)
                    {
                        orderFactTotal += order.FactTotal;
                    }
                    if (orderFactTotal == 0)
                    {
                        foreach (ESP.Purchase.Entity.PaymentPeriodInfo p in paymentList)
                        {
                            if (p.ReturnId != 0)
                            {
                                returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(p.ReturnId);
                                if (returnModel != null && returnModel.FactFee != null && returnModel.FactFee.Value != 0)
                                {
                                    totalAmount += returnModel.FactFee.Value;
                                }
                                else
                                {
                                    totalAmount += returnModel.PreFee.Value;
                                }
                            }
                            else
                            {
                                totalAmount += p.expectPaymentPrice;
                            }
                        }
                    }
                    else
                    {
                        totalAmount += orderFactTotal;
                    }
                }
            }

            //媒体稿费对私单据的成本统计
            PRList = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModelList(" (project_code='" + projectModel.ProjectCode + "') and status not in(-1,0,2,21) and prtype in(1,6) and departmentid=" + DepartmentID.ToString() + "and id!=" + gid.ToString(), new List<System.Data.SqlClient.SqlParameter>());
            if (PRList != null && PRList.Count > 0)//获取所有媒体稿费对私单据
            {
                foreach (ESP.Purchase.Entity.GeneralInfo g in PRList)
                {
                    //检查李彦娥是否已经处理完毕
                    ESP.Purchase.Entity.MediaPREditHisInfo relationModel = ESP.Purchase.BusinessLogic.MediaPREditHisManager.GetModelByOldPRID(g.id);
                    if (relationModel == null)//尚未处理
                    {
                        //尚未处理，直接按PR单的帐期的预计金额统计
                        paymentList = ESP.Purchase.BusinessLogic.PaymentPeriodManager.GetModelList(" gid=" + g.id.ToString());
                        foreach (ESP.Purchase.Entity.PaymentPeriodInfo p in paymentList)
                        {
                            totalAmount += p.expectPaymentPrice;
                        }
                    }
                    else//已经处理
                    {
                        //处理后如果产生了新PR单
                        if (relationModel.NewPRId != null && relationModel.NewPRId.Value != 0)
                        {
                            //根据关联表检查处理后产生的新PR单
                            ESP.Purchase.Entity.GeneralInfo newModel = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(relationModel.NewPRId.Value);
                            //按新PR单的帐期统计金额
                            if (newModel != null)
                            {
                                paymentList = ESP.Purchase.BusinessLogic.PaymentPeriodManager.GetModelList(" gid=" + newModel.id.ToString());
                                foreach (ESP.Purchase.Entity.PaymentPeriodInfo p in paymentList)
                                {
                                    totalAmount += p.expectPaymentPrice;
                                }
                            }
                        }
                        //处理后如果产生了新PN单
                        if (relationModel.NewPNId != null && relationModel.NewPNId.Value != 0)
                        {
                            //按照新PN单的实际金额或预计金额统计
                            returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(relationModel.NewPNId.Value);
                            if (returnModel.FactFee != null && returnModel.FactFee.Value != 0)
                            {
                                totalAmount += returnModel.FactFee.Value;
                            }
                            else
                            {
                                totalAmount += returnModel.PreFee.Value;
                            }
                        }
                    }
                }
            }

            //异常关闭的PR单
            PRList = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModelList(" (project_code='" + projectModel.ProjectCode + "') and departmentid=" + DepartmentID.ToString() + " and status=21 and id!=" + gid.ToString(), new List<System.Data.SqlClient.SqlParameter>());
            if (PRList != null && PRList.Count > 0)
            {
                foreach (ESP.Purchase.Entity.GeneralInfo model in PRList)
                {
                    IList<ESP.Purchase.Entity.OrderInfo> orderList = ESP.Purchase.BusinessLogic.OrderInfoManager.GetListByGeneralId(model.id);
                    foreach (ESP.Purchase.Entity.OrderInfo rModel in orderList)
                    {
                        if (rModel.FactTotal != null && rModel.FactTotal != 0)
                        {
                            totalAmount += rModel.FactTotal;
                        }
                        else
                        {
                            totalAmount += rModel.total;
                        }
                    }
                }
            }
            return totalAmount;
        }

        public static bool CheckPNOver(ESP.Finance.Entity.ReturnInfo returnModel)
        {
            bool isOver = false;
            decimal TotalCost = 0;
            ESP.Finance.Entity.ProjectInfo projectModel = null;
            projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(returnModel.ProjectID ?? 0);

            if (returnModel.DepartmentID != null && projectModel != null)
            {
                //project
                if (returnModel.DepartmentID.Value == projectModel.GroupID.Value)
                {
                    TotalCost = ESP.Finance.BusinessLogic.ContractCostManager.GetTotalAmountByProject(projectModel.ProjectId);
                }
                else
                {
                    IList<ESP.Finance.Entity.SupporterInfo> supporterList = ESP.Finance.BusinessLogic.SupporterManager.GetList(" projectid=" + projectModel.ProjectId.ToString() + " and groupid=" + returnModel.DepartmentID.Value.ToString());
                    if (supporterList != null && supporterList.Count > 0)
                        TotalCost = ESP.Finance.BusinessLogic.SupporterCostManager.GetTotalCostBySupporter(supporterList[0].SupportID);
                }
            }
            else
            {
                isOver = false;
            }

            decimal TotalPN = ESP.Finance.BusinessLogic.ReturnManager.GetTotalPNFee(returnModel);
            if (returnModel.PreFee > TotalCost - TotalPN)
                isOver = true;
            else
                isOver = false;

            if (returnModel.ReturnType == 11)//押金
            {
                isOver = false;
            }
            return isOver;
        }

        public static List<ESP.Purchase.Entity.GeneralInfo> 得到项目涉及所有PR单(string ProjectCode, int Departmentid)
        {
            if (!string.IsNullOrEmpty(ProjectCode))
            {
                //查询该成本组内非媒体、对私PR单
                string term = " project_code=@projectcode and departmentid=@departmentid and status not in(-1,0,2,21) and prtype not in (4,8)";
                List<System.Data.SqlClient.SqlParameter> paramList = new List<System.Data.SqlClient.SqlParameter>();
                System.Data.SqlClient.SqlParameter param1 = new System.Data.SqlClient.SqlParameter("@projectcode", SqlDbType.NVarChar, 50);
                param1.Value = ProjectCode;
                paramList.Add(param1);
                System.Data.SqlClient.SqlParameter param2 = new System.Data.SqlClient.SqlParameter("@departmentid", SqlDbType.Int, 4);
                param2.Value = Departmentid;
                paramList.Add(param2);
                List<ESP.Purchase.Entity.GeneralInfo> generalList = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModelList(term, paramList);

                //查询该成本组内所有媒体、对私PR单
                //term = " project_code=@projectcode and departmentid=@departmentid and status not in(-1,0,2,21) and prtype in (1,6)";
                //paramList.Clear();
                //param1 = new System.Data.SqlClient.SqlParameter("@projectcode", SqlDbType.NVarChar, 50);
                //param1.Value = ProjectCode;
                //paramList.Add(param1);
                //param2 = new System.Data.SqlClient.SqlParameter("@departmentid", SqlDbType.Int, 4);
                //param2.Value = Departmentid;
                //paramList.Add(param2);
                //List<ESP.Purchase.Entity.GeneralInfo> MediaPRlList = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModelList(term, paramList);
                //foreach (ESP.Purchase.Entity.GeneralInfo general in MediaPRlList)
                //{
                //    generalList.Add(general);
                //}
                //查询该成本组内媒体、对私PR单3000以上
                //term = " project_code=@projectcode and departmentid=@departmentid and status not in(-1,0,2,21) and prtype in (4,8) and id in(select newprid from t_mediapredithis where newprid is not null and newprid<>0)";
                //paramList.Clear();
                //param1 = new System.Data.SqlClient.SqlParameter("@projectcode", SqlDbType.NVarChar, 50);
                //param1.Value = ProjectCode;
                //paramList.Add(param1);
                //param2 = new System.Data.SqlClient.SqlParameter("@departmentid", SqlDbType.Int, 4);
                //param2.Value = Departmentid;
                //paramList.Add(param2);
                //MediaPRlList = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModelList(term, paramList);
                //foreach (ESP.Purchase.Entity.GeneralInfo general in MediaPRlList)
                //{
                //    generalList.Add(general);
                //}
                //查询该成本组内媒体、对私PR单3000以下
                //term = " project_code=@projectcode and departmentid=@departmentid and status not in(-1,0,2,21) and prtype in (1,6) and id in(select oldprid from t_mediapredithis where newpnid is not null and newpnid<>0)";
                //paramList.Clear();
                //param1 = new System.Data.SqlClient.SqlParameter("@projectcode", SqlDbType.NVarChar, 50);
                //param1.Value = ProjectCode;
                //paramList.Add(param1);
                //param2 = new System.Data.SqlClient.SqlParameter("@departmentid", SqlDbType.Int, 4);
                //param2.Value = Departmentid;
                //paramList.Add(param2);
                //MediaPRlList = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModelList(term, paramList);
                //foreach (ESP.Purchase.Entity.GeneralInfo general in MediaPRlList)
                //{
                //    generalList.Add(general);
                //}
                //查询该成本组内异常关闭的
                term = " project_code=@projectcode and departmentid=@departmentid and status in(21) and prtype not in (4,8)";
                paramList.Clear();
                param1 = new System.Data.SqlClient.SqlParameter("@projectcode", SqlDbType.NVarChar, 50);
                param1.Value = ProjectCode;
                paramList.Add(param1);
                param2 = new System.Data.SqlClient.SqlParameter("@departmentid", SqlDbType.Int, 4);
                param2.Value = Departmentid;
                paramList.Add(param2);
                List<ESP.Purchase.Entity.GeneralInfo> MediaPRlList = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModelList(term, paramList);
                foreach (ESP.Purchase.Entity.GeneralInfo general in MediaPRlList)
                {
                    generalList.Add(general);
                }
                return generalList;
            }
            else
                return null;
        }
        /// <summary>
        /// 统计PR单占用成本情况
        /// </summary>
        /// <param name="ProjectCode"></param>
        /// <param name="Departmentid"></param>
        /// <param name="TypeID"></param>
        /// <returns></returns>
        public static decimal 得到某一成本PR使用总额(string ProjectCode, int Departmentid, int TypeID, List<ESP.Purchase.Entity.GeneralInfo> generalList)
        {
            decimal CostTotal = 0;
            ESP.Purchase.Entity.MediaPREditHisInfo relationModel = null;
            decimal PnTotal = 0;
            decimal newPrTotal = 0;
            if (generalList != null)
            {
                foreach (ESP.Purchase.Entity.GeneralInfo general in generalList)
                {
                    List<ESP.Purchase.Entity.OrderInfo> orderList = null;
                    PnTotal = 0;
                    newPrTotal = 0;
                    relationModel = null;
                    string orderterms = " general_id=@generalID";
                    List<System.Data.SqlClient.SqlParameter> OrderParamList = new List<System.Data.SqlClient.SqlParameter>();
                    System.Data.SqlClient.SqlParameter orderparam = new System.Data.SqlClient.SqlParameter("@generalID", SqlDbType.Int, 4);
                    orderparam.Value = general.id;
                    OrderParamList.Add(orderparam);
                    orderList = ESP.Purchase.BusinessLogic.OrderInfoManager.GetModelList(orderterms, OrderParamList);

                    if (general.PRType == (int)ESP.Purchase.Common.PRTYpe.PrivatePR || general.PRType == (int)ESP.Purchase.Common.PRTYpe.MediaPR)
                    {
                        relationModel = ESP.Purchase.BusinessLogic.MediaPREditHisManager.GetModelByOldPRID(general.id);
                        if (relationModel == null)
                        {
                            PnTotal = 0;
                            newPrTotal = 0;
                        }
                        else
                        {
                            if (relationModel.NewPNId != null && relationModel.NewPNId != 0)
                            {
                                ESP.Finance.Entity.ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(relationModel.NewPNId.Value);
                                PnTotal = returnModel.PreFee.Value;
                            }
                            if (relationModel.NewPRId != null && relationModel.NewPRId != 0)
                            {
                                ESP.Purchase.Entity.GeneralInfo newPR = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(relationModel.NewPRId.Value);
                                newPrTotal = newPR == null ? 0 : newPR.totalprice;
                            }
                        }
                        ESP.Purchase.Entity.TypeInfo typeModel = ESP.Purchase.BusinessLogic.TypeManager.GetModel(orderList[0].producttype);
                        if (typeModel != null && typeModel.parentId == TypeID)
                        {
                            CostTotal += newPrTotal;
                            CostTotal += PnTotal;
                        }
                    }

                    //PR单下所有属于该成本项的总额
                    foreach (ESP.Purchase.Entity.OrderInfo orderModel in orderList)
                    {
                        ESP.Purchase.Entity.TypeInfo typeModel = ESP.Purchase.BusinessLogic.TypeManager.GetModel(orderModel.producttype);
                        if (typeModel != null && typeModel.parentId == TypeID)
                        {
                            if (relationModel == null)
                            {
                                if (general.PRType == (int)ESP.Purchase.Common.PRTYpe.PR_PriFA || general.PRType == (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA)
                                {
                                    break;
                                }
                                else
                                {
                                    //异常关闭的PR单物料成本占用计算
                                    if (orderModel.FactTotal != 0)
                                        CostTotal += orderModel.FactTotal;
                                    else
                                        CostTotal += orderModel.total;
                                }
                            }
                        }
                    }
                }
            }
            return CostTotal;
        }

        public static decimal 得到某一成本PR使用总额(string ProjectCode, int Departmentid, int TypeID)
        {
            List<ESP.Purchase.Entity.GeneralInfo> generalList = 得到项目涉及所有PR单(ProjectCode, Departmentid);
            return 得到某一成本PR使用总额(ProjectCode, Departmentid, TypeID, generalList);
        }
        /// <summary>
        /// 得到某一成本PR报销使用总额,如果统计OOP，typeID设为0
        /// </summary>
        /// <param name="ProjectCode"></param>
        /// <param name="Departmentid"></param>
        /// <param name="TypeID"></param>
        /// <returns></returns>
        public static decimal 得到某一成本报销使用总额(string ProjectCode, int Departmentid, int TypeID)
        {
            decimal totalCost = 0;
            string term = " ProjectCode='" + ProjectCode + "' and departmentId=" + Departmentid.ToString();
            term += " and (returntype in(30,31,33,35) and returnstatus not in(-1,0,1)) or(returntype=32 and returnstatus<>140) or (returntype=36 and returnstatus=139)";

            IList<ESP.Finance.Entity.ReturnInfo> returnList = ESP.Finance.BusinessLogic.ReturnManager.GetList(term);
            if (returnList != null && returnList.Count > 0)
            {
                foreach (ESP.Finance.Entity.ReturnInfo model in returnList)
                {
                    if (model == null)
                        continue;
                    IList<ESP.Finance.Entity.ExpenseAccountDetailInfo> detailList = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetList(" and ReturnID=" + model.ReturnID.ToString() + " and CostDetailID=" + TypeID);
                    foreach (ESP.Finance.Entity.ExpenseAccountDetailInfo detail in detailList)
                    {
                        if (detail.Status == 1)
                            totalCost += detail.ExpenseMoney == null ? 0 : detail.ExpenseMoney.Value;
                    }
                }
            }
            return totalCost;
        }

        public static decimal 得到PR成本使用总额(string ProjectCode, int Departmentid)
        {
            ESP.Finance.Entity.ProjectInfo projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelByProjectCode(ProjectCode);
            return GetOccupyCost(projectModel.ProjectId, Departmentid);
        }

        public static decimal 得到OOP总额(string projectcode, int departmentID)
        {
            decimal oop = 0;
            ESP.Finance.Entity.ProjectInfo projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelByProjectCode(projectcode);
            if (projectModel != null)
            {
                if (projectModel.GroupID.Value != departmentID)
                {
                    IList<ESP.Finance.Entity.SupporterInfo> supportList = ESP.Finance.BusinessLogic.SupporterManager.GetList("projectID=" + projectModel.ProjectId.ToString() + " and groupID=" + departmentID.ToString());
                    if (supportList != null && supportList.Count != 0)
                    {
                        IList<ESP.Finance.Entity.SupporterExpenseInfo> expenseList = ESP.Finance.BusinessLogic.SupporterExpenseManager.GetList(" SupporterID=" + supportList[0].SupportID.ToString() + " and description='OOP'");
                        if (expenseList.Count > 0)
                            oop = expenseList[0].Expense == null ? 0 : expenseList[0].Expense.Value;
                        else
                            oop = 0;
                    }
                    else
                        oop = 0;
                }
                else
                {
                    IList<ESP.Finance.Entity.ProjectExpenseInfo> expenseList = ESP.Finance.BusinessLogic.ProjectExpenseManager.GetList(" ProjectID=" + projectModel.ProjectId.ToString() + " and description='OOP'");
                    if (expenseList.Count > 0)
                        oop = expenseList[0].Expense == null ? 0 : expenseList[0].Expense.Value;
                    else
                        oop = 0;
                }
            }
            return oop;
        }

        public static decimal 得到TrafficFee总额(string projectcode, int departmentID)
        {
            decimal fee = 0;
            ESP.Finance.Entity.ProjectInfo projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelByProjectCode(projectcode);
            if (projectModel != null)
            {
                if (projectModel.GroupID.Value != departmentID)
                {
                    IList<ESP.Finance.Entity.SupporterInfo> supportList = ESP.Finance.BusinessLogic.SupporterManager.GetList("projectID=" + projectModel.ProjectId.ToString() + " and groupID=" + departmentID.ToString());
                    IList<ESP.Finance.Entity.SupporterExpenseInfo> expenseList = ESP.Finance.BusinessLogic.SupporterExpenseManager.GetList(" SupporterID=" + supportList[0].SupportID.ToString() + " and description='Media'");
                    if (expenseList.Count > 0)
                        fee = expenseList[0].Expense == null ? 0 : expenseList[0].Expense.Value;
                    else
                        fee = 0;
                }
                else
                {
                    IList<ESP.Finance.Entity.ProjectExpenseInfo> expenseList = ESP.Finance.BusinessLogic.ProjectExpenseManager.GetList(" ProjectID=" + projectModel.ProjectId.ToString() + " and description='Media'");
                    if (expenseList.Count > 0)
                        fee = expenseList[0].Expense == null ? 0 : expenseList[0].Expense.Value;
                    else
                        fee = 0;
                }
            }
            return fee;
        }
        public static decimal 得到TrafficFee总额(ESP.Finance.Entity.ProjectInfo projectModel, int departmentID)
        {
            decimal fee = 0;
            if (projectModel != null)
            {
                if (projectModel.GroupID.Value != departmentID)
                {
                    IList<ESP.Finance.Entity.SupporterInfo> supportList = ESP.Finance.BusinessLogic.SupporterManager.GetList("projectID=" + projectModel.ProjectId.ToString() + " and groupID=" + departmentID.ToString());
                    IList<ESP.Finance.Entity.SupporterExpenseInfo> expenseList = ESP.Finance.BusinessLogic.SupporterExpenseManager.GetList(" SupporterID=" + supportList[0].SupportID.ToString() + " and description='Media'");
                    if (expenseList.Count > 0)
                        fee = expenseList[0].Expense == null ? 0 : expenseList[0].Expense.Value;
                    else
                        fee = 0;
                }
                else
                {
                    IList<ESP.Finance.Entity.ProjectExpenseInfo> expenseList = ESP.Finance.BusinessLogic.ProjectExpenseManager.GetList(" ProjectID=" + projectModel.ProjectId.ToString() + " and description='Media'");
                    if (expenseList.Count > 0)
                        fee = expenseList[0].Expense == null ? 0 : expenseList[0].Expense.Value;
                    else
                        fee = 0;
                }
            }
            return fee;
        }

        public static decimal 得到消耗使用总额(string projectcode, int departmentID)
        {
            decimal total = 0;
            ESP.Finance.Entity.ProjectInfo projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelByProjectCode(projectcode);
            IList<ESP.Finance.Entity.ReturnInfo> returnList = ESP.Finance.BusinessLogic.ReturnManager.GetList(" ProjectID=" + projectModel.ProjectId.ToString() + " and DepartmentID=" + departmentID.ToString() + " and ReturnType=20 and ReturnStatus not in(0,1)");
            foreach (ESP.Finance.Entity.ReturnInfo model in returnList)
            {
                if (model.FactFee == null || model.FactFee.Value == 0)
                {
                    total += model.PreFee.Value;
                }
                else
                    total += model.FactFee.Value;
            }
            return total;
        }

        public static decimal 得到报销费用总额(string projectcode, int departmentID)
        {
            decimal total = 0;
            ESP.Finance.Entity.ProjectInfo projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelByProjectCode(projectcode);
            string term = string.Empty;
            term += " ProjectID=" + projectModel.ProjectId.ToString() + " and DepartmentID=" + departmentID.ToString();
            term += " and (returntype in(30,31,32,33,35) and returnstatus not in(-1,0,1)) ";

            IList<ESP.Finance.Entity.ReturnInfo> returnList = ESP.Finance.BusinessLogic.ReturnManager.GetList(term);

            foreach (ESP.Finance.Entity.ReturnInfo model in returnList)
            {
                if (model.FactFee == null || model.FactFee.Value == 0)
                {
                    total += model.PreFee.Value;
                }
                else
                    total += model.FactFee.Value;
            }
            return total;
        }

        public static decimal 得到第三方总额(string projectCode, int departmentID)
        {
            decimal cost = 0;
            ESP.Finance.Entity.ProjectInfo projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelByProjectCode(projectCode);
            if (projectModel != null)
            {
                if (projectModel.GroupID.Value != departmentID)
                {
                    IList<ESP.Finance.Entity.SupporterInfo> supportList = ESP.Finance.BusinessLogic.SupporterManager.GetList("projectID=" + projectModel.ProjectId.ToString() + " and groupID=" + departmentID.ToString());
                    if (supportList != null && supportList.Count != 0)
                    {
                        cost = ESP.Finance.BusinessLogic.SupporterCostManager.GetTotalCostBySupporter(supportList[0].SupportID);
                    }
                    else
                        cost = 0;
                }
                else
                {
                    cost = ESP.Finance.BusinessLogic.ContractCostManager.GetTotalAmountByProject(projectModel.ProjectId);
                }
            }
            return cost;
        }

        /// <summary>
        /// 根据项目所属组检查
        /// </summary>
        /// <param name="ProjectCode"></param>
        /// <param name="Departmentid"></param>
        /// <param name="TypeID"></param>
        /// <param name="Amounts"></param>
        /// <returns></returns>
        public static bool 检查项目号某一成本项是否超出(string ProjectCode, int Departmentid, int TypeID, decimal Amounts)
        {
            decimal CostTotal = 得到某一成本PR使用总额(ProjectCode, Departmentid, TypeID) +
                                            得到某一成本报销使用总额(ProjectCode, Departmentid, TypeID);
            if (Amounts >= CostTotal)
                return true;
            else
                return false;
        }

        public static bool PR申请是否使用某一成本项(string ProjectCode, int TypeID, int DepartmentID)
        {
            if (!string.IsNullOrEmpty(ProjectCode))
            {
                string term = " project_code=@projectcode and DepartmentID=@DepartmentID and status not in(-1,0,2)";
                List<System.Data.SqlClient.SqlParameter> paramList = new List<System.Data.SqlClient.SqlParameter>();
                System.Data.SqlClient.SqlParameter param1 = new System.Data.SqlClient.SqlParameter("@projectcode", SqlDbType.NVarChar, 50);
                param1.Value = ProjectCode;
                paramList.Add(param1);
                System.Data.SqlClient.SqlParameter param2 = new System.Data.SqlClient.SqlParameter("@DepartmentID", SqlDbType.Int, 4);
                param2.Value = DepartmentID;
                paramList.Add(param2);
                List<ESP.Purchase.Entity.GeneralInfo> generalList = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModelList(term, paramList);
                foreach (ESP.Purchase.Entity.GeneralInfo general in generalList)
                {
                    string orderterms = " general_id=@generalID";
                    List<System.Data.SqlClient.SqlParameter> OrderParamList = new List<System.Data.SqlClient.SqlParameter>();
                    System.Data.SqlClient.SqlParameter orderparam = new System.Data.SqlClient.SqlParameter("@generalID", SqlDbType.Int, 4);
                    orderparam.Value = general.id;
                    OrderParamList.Add(orderparam);

                    List<ESP.Purchase.Entity.OrderInfo> orderList = ESP.Purchase.BusinessLogic.OrderInfoManager.GetModelList(orderterms, OrderParamList);

                    foreach (ESP.Purchase.Entity.OrderInfo orderModel in orderList)
                    {
                        ESP.Purchase.Entity.TypeInfo typeModel = ESP.Purchase.BusinessLogic.TypeManager.GetModel(orderModel.producttype);
                        if (typeModel.parentId == TypeID)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            else
                return false;
        }

        public static string GetBranchNameByDeptID(int groupId)
        {
            return new ESP.Finance.DataAccess.DepartmentViewDataProvider().GetBranchnameByDeptId(groupId);
        }
    }
}
