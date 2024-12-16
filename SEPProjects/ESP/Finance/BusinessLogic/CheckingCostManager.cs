using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.BusinessLogic
{
    public static class CheckingCostManager
    {
        public static List<ESP.Finance.Entity.CheckingCost> CostCollection(ESP.Finance.Entity.ProjectInfo projectModel)
        {
            List<ESP.Purchase.Entity.GeneralInfo> generalList = ESP.Finance.BusinessLogic.CheckerManager.得到项目涉及所有PR单(projectModel.ProjectCode, projectModel.GroupID.Value);

            List<ESP.Finance.Entity.CheckingCost> costList1 = GetList(generalList);

            var tmpList = costList1.OrderBy(N => N.TypeID);

            return tmpList.ToList();
        }


        public static List<ESP.Finance.Entity.CheckingCost> CostCollectionBySupporter(ESP.Finance.Entity.ProjectInfo projectModel, int DeparmetId)
        {
            List<ESP.Purchase.Entity.GeneralInfo> generalList = ESP.Finance.BusinessLogic.CheckerManager.得到项目涉及所有PR单(projectModel.ProjectCode, DeparmetId);

            List<ESP.Finance.Entity.CheckingCost> costList1 = GetList(generalList);

            var tmpList = costList1.OrderBy(N => N.TypeID);
            return tmpList.ToList();
        }

        public static List<ESP.Finance.Entity.CheckingCost> OOPCollection(ESP.Finance.Entity.ProjectInfo projectModel, int DeartpmentID)
        {
            //需要冲销的单子，未冲销
            IList<ESP.Finance.Entity.ExpenseAccountDetailInfo> ReturnList = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetList(" and returnID in(select returnid from f_return where projectCode='" + projectModel.ProjectCode + "' and DepartmentID=" + DeartpmentID.ToString() + " and ((returnType=32 and returnStatus>=2)))");
            //需要冲销的单子，已冲销
           // IList<ESP.Finance.Entity.ExpenseAccountDetailInfo> ReturnList2 = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetList(" and returnID in(select returnid from f_return where  projectCode='" + projectModel.ProjectCode + "' and DepartmentID=" + DeartpmentID.ToString() + " and returnType in(31,32,33) and returnStatus =139)");
            //不需要冲销的单子，已提交
            IList<ESP.Finance.Entity.ExpenseAccountDetailInfo> ReturnList3 = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetList(" and returnID in(select returnid from f_return where  projectCode='" + projectModel.ProjectCode + "' and DepartmentID=" + DeartpmentID.ToString() + " and returnType in(30,31,33,35,37,40) and returnStatus>=2)");

            List<ESP.Finance.Entity.CheckingCost> costList1 = GetOOPList(ReturnList);
            //List<ESP.Finance.Entity.CheckingCost> costList2 = GetOOPList(ReturnList2);
            List<ESP.Finance.Entity.CheckingCost> costList3 = GetOOPList(ReturnList3);

            //foreach (ESP.Finance.Entity.CheckingCost cost in costList2)
            //{
            //    costList1.Add(cost);
            //}
            foreach (ESP.Finance.Entity.CheckingCost cost in costList3)
            {
                costList1.Add(cost);
            }
            var tmpList = costList1.OrderBy(N => N.TypeID);
            return tmpList.ToList();
        }

        private static List<ESP.Finance.Entity.CheckingCost> GetOOPList(IList<ESP.Finance.Entity.ExpenseAccountDetailInfo> returnList)
        {
            bool Duplicated = false;
            List<ESP.Finance.Entity.CheckingCost> costList = new List<ESP.Finance.Entity.CheckingCost>();
            foreach (ESP.Finance.Entity.ExpenseAccountDetailInfo model in returnList)
            {
                ESP.Finance.Entity.ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(model.ReturnID.Value);
                Duplicated = false;
                //foreach (ESP.Finance.Entity.CheckingCost c in costList)
                //{
                //    if (c.PRNo.Trim() == returnModel.ReturnCode)
                //    {
                //        Duplicated = true;
                //        break;
                //    }
                //}
                if (!Duplicated)
                {
                    ESP.Purchase.Entity.TypeInfo typeModel = ESP.Purchase.BusinessLogic.TypeManager.GetModel(model.CostDetailID.Value);
                    ESP.Finance.Entity.CheckingCost cost = new ESP.Finance.Entity.CheckingCost();
                    ESP.Finance.Entity.MaterialType materialType = new ESP.Finance.Entity.MaterialType();

                    cost.ID = model.ReturnID.ToString();
                    cost.PRNo = returnModel.ReturnCode;
                    cost.ProjectID = returnModel.ProjectID.Value;
                    cost.ProjectCode = returnModel.ProjectCode;
                    cost.PrType = returnModel.ReturnType.Value;
                    cost.DepartmentID = returnModel.DepartmentID.Value;
                    cost.DepartmentName = returnModel.DepartmentName;
                    cost.PNTotal = returnModel.PreFee.Value.ToString("#,##0.00");
                    materialType.TypeID = model.CostDetailID.Value;
                    cost.Requestor = returnModel.RequestEmployeeName;
                    cost.AppAmount = model.ExpenseMoney.Value.ToString("#,##0.00");
                    cost.AppDate = returnModel.RequestDate.Value;
                    cost.ReturnType = returnModel.ReturnType.Value;
                    cost.SupplierName = "";
                    cost.Description = model.ExpenseDesc;
                    cost.GroupName = returnModel.DepartmentName;
                    if (typeModel != null)
                    {
                        materialType.TypeID = typeModel.typeid;
                        materialType.TypeName = typeModel.typename;
                    }
                    else
                    {
                        materialType.TypeID = 0;
                        materialType.TypeName = "OOP";
                    }

                    if (model.CostDetailID == 0)//oop
                    {
                        materialType.TypeTotalAmount = ESP.Finance.BusinessLogic.CheckerManager.得到某一成本报销使用总额(returnModel.ProjectCode, returnModel.DepartmentID.Value, 0);
                        //冲销
                        if (returnModel.ReturnType.Value == 36)
                        {
                            cost.PaidAmount = model.ExpenseMoney.Value.ToString("#,##0.00");
                            cost.UnPaidAmount = "0.00";
                        }
                        //不需要冲销的报销单和第三方报销
                        else if (returnModel.ReturnType.Value == 30 || returnModel.ReturnType.Value == 32 || returnModel.ReturnType.Value == 31 || returnModel.ReturnType.Value == 33 || returnModel.ReturnType.Value == 35 || returnModel.ReturnType.Value == 40)
                        {
                            if (returnModel.ReturnStatus == 140)//全部审核完成
                            {
                                cost.PaidAmount = returnModel.FactFee == null ? model.ExpenseMoney.Value.ToString("#,##0.00") : model.ExpenseMoney.Value.ToString("#,##0.00");
                                cost.UnPaidAmount = "0.00";
                            }
                            else
                            {
                                cost.UnPaidAmount = returnModel.FactFee == null ? model.ExpenseMoney.Value.ToString("#,##0.00") : model.ExpenseMoney.Value.ToString("#,##0.00");
                                cost.PaidAmount = "0.00";
                            }
                        }
                        //oop总额
                        materialType.CostPreAmount = ESP.Finance.BusinessLogic.CheckerManager.得到OOP总额(returnModel.ProjectCode, returnModel.DepartmentID.Value);
                    }
                    else//第三方成本
                    {
                        materialType.TypeTotalAmount = ESP.Finance.BusinessLogic.CheckerManager.得到某一成本报销使用总额(returnModel.ProjectCode, returnModel.DepartmentID.Value, model.CostDetailID.Value);
                        //已冲销
                        if (returnModel.ReturnType.Value == 36)
                        {
                            cost.PaidAmount = model.ExpenseMoney.Value.ToString("#,##0.00");
                            cost.UnPaidAmount = "0.00";
                        }
                        //不需要冲销的报销单和第三方报销
                        else if (returnModel.ReturnType.Value == 30 || returnModel.ReturnType.Value == 31 || returnModel.ReturnType.Value == 32 || returnModel.ReturnType.Value == 33 || returnModel.ReturnType.Value == 35)
                        {
                            if (returnModel.ReturnStatus == 140)//全部审核完成
                            {
                                cost.PaidAmount = returnModel.FactFee == null ? model.ExpenseMoney.Value.ToString("#,##0.00") : model.ExpenseMoney.Value.ToString("#,##0.00");
                                cost.UnPaidAmount = "0.00";
                            }
                            else
                            {
                                cost.UnPaidAmount = returnModel.FactFee == null ? model.ExpenseMoney.Value.ToString("#,##0.00") : model.ExpenseMoney.Value.ToString("#,##0.00");
                                cost.PaidAmount = "0.00";
                            }
                        }
                        cost.MaterialType = materialType;
                        if (cost.TypeID != 0)
                        {
                            IList<ESP.Finance.Entity.ContractCostInfo> ProjectCostList = ESP.Finance.BusinessLogic.ContractCostManager.GetList(" projectid=" + cost.ProjectID.ToString() + " and costTypeID=" + cost.TypeID.ToString());
                            if (ProjectCostList != null && ProjectCostList.Count > 0)
                            {
                                materialType.CostPreAmount = ProjectCostList[0].Cost.Value;
                            }
                            else
                            {
                                materialType.CostPreAmount = 0;
                            }
                        }
                        else
                        {
                            materialType.CostPreAmount = ESP.Finance.BusinessLogic.CheckerManager.得到OOP总额(cost.ProjectCode, cost.DepartmentID);
                        }

                    }
                    cost.MaterialType = materialType;
                    costList.Add(cost);
                }

            }
            return costList;
        }

        private static List<ESP.Finance.Entity.CheckingCost> GetList(List<ESP.Purchase.Entity.GeneralInfo> generalList)
        {
            bool Duplicated = false;
            Dictionary<int, ESP.Finance.Entity.MaterialType> dicTypes = new Dictionary<int, ESP.Finance.Entity.MaterialType>();

            List<ESP.Finance.Entity.CheckingCost> costList = new List<ESP.Finance.Entity.CheckingCost>();
            if (generalList != null)
            {
                foreach (ESP.Purchase.Entity.GeneralInfo model in generalList)
                {
                    Duplicated = false;
                    //foreach (ESP.Finance.Entity.CheckingCost c in costList)
                    //{
                    //    if (c.PRNo.Trim() == model.PrNo)
                    //    {
                    //        Duplicated = true;
                    //        break;
                    //    }
                    //}
                    if (!Duplicated)
                    {
                        ESP.Finance.Entity.CheckingCost cost = new ESP.Finance.Entity.CheckingCost();
                        cost.ID = model.id.ToString();
                        cost.PRNo = model.PrNo;
                        cost.ProjectID = model.Project_id;
                        cost.ProjectCode = model.project_code;
                        cost.PrType = model.PRType;
                        cost.DepartmentID = model.Departmentid;
                        cost.DepartmentName = model.Department;

                        //获取该PR单的已申请PN总和
                        IList<ESP.Finance.Entity.ReturnInfo> returnList = ESP.Finance.BusinessLogic.ReturnManager.GetList(" prid=" + model.id.ToString());
                        decimal totalPayment = 0;
                        if (returnList != null && returnList.Count > 0)
                        {
                            foreach (ESP.Finance.Entity.ReturnInfo returnModel in returnList)
                            {
                                if (returnModel.FactFee != null && returnModel.FactFee.Value > 0)
                                    totalPayment += returnModel.FactFee.Value;
                                else
                                    totalPayment += returnModel.PreFee.Value;
                            }
                        }
                        List<ESP.Purchase.Entity.OrderInfo> orderList = ESP.Purchase.BusinessLogic.OrderInfoManager.GetListByGeneralId(model.id);
                        if (orderList == null || orderList.Count == 0)
                            continue;
                        ESP.Purchase.Entity.TypeInfo typeModel = ESP.Purchase.BusinessLogic.TypeManager.GetModel(orderList[0].producttype);
                        ESP.Purchase.Entity.TypeInfo type2Model = ESP.Purchase.BusinessLogic.TypeManager.GetModel(typeModel.parentId);
                        cost.PNTotal = totalPayment.ToString("#,##0.00");
                        ESP.Finance.Entity.MaterialType MaterialType;
                        if (!dicTypes.TryGetValue(typeModel.parentId, out MaterialType))
                        {
                            MaterialType = new ESP.Finance.Entity.MaterialType();
                            MaterialType.TypeID = typeModel.parentId;
                            MaterialType.TypeName = type2Model.typename;
                            MaterialType.CostPreAmount = 0;
                            MaterialType.TypeTotalAmount = 0;
                            dicTypes.Add(MaterialType.TypeID, MaterialType);

                            IList<ESP.Finance.Entity.ContractCostInfo> ProjectCostList = ESP.Finance.BusinessLogic.ContractCostManager.GetList(" projectid=" + cost.ProjectID.ToString() + " and costTypeID=" + typeModel.parentId.ToString());
                            if (ProjectCostList != null && ProjectCostList.Count > 0)
                            {
                                MaterialType.CostPreAmount = ProjectCostList[0].Cost.Value;
                            }
                            else
                            {
                                MaterialType.CostPreAmount = 0;
                            }
                        }
                        foreach (ESP.Purchase.Entity.OrderInfo orderModel in orderList)
                        {
                            //异常关闭的PR单物料成本占用计算
                            if (model.status == (int)ESP.Purchase.Common.State.requisition_Stop)
                                MaterialType.TypeTotalAmount += orderModel.FactTotal;
                            else
                                MaterialType.TypeTotalAmount += orderModel.total;
                        }

                        cost.MaterialType = MaterialType;
                        //cost.TypeID = typeModel.parentId;
                        cost.Requestor = model.requestorname;
                        if (model.status == (int)ESP.Purchase.Common.State.requisition_Stop)//如果是异常关闭的PR单，总金额以实际发生为准
                            cost.AppAmount = totalPayment.ToString("#,##0.00");
                        else
                        {
                            totalPayment = 0;
                            List<ESP.Purchase.Entity.PaymentPeriodInfo> periodList = ESP.Purchase.BusinessLogic.PaymentPeriodManager.GetModelList(" gid="+model.id.ToString());
                            if (periodList == null || periodList.Count == 0)
                                continue;
                            foreach (ESP.Purchase.Entity.PaymentPeriodInfo p in periodList)
                            {
                                if (p == null)
                                    continue;
                                if (p.ReturnId != 0)
                                {
                                    ESP.Finance.Entity.ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(p.ReturnId);
                                    if (returnModel == null)
                                        continue;
                                    if (returnModel.FactFee != null && returnModel.FactFee.Value != 0)
                                    {
                                        totalPayment += returnModel.FactFee.Value;
                                    }
                                    else
                                    {
                                        totalPayment += returnModel.PreFee.Value;
                                    }
                                }
                                else
                                {
                                    totalPayment += p.expectPaymentPrice;
                                }
                            }
                        }
                        cost.AppAmount = totalPayment.ToString("#,##0.00");
                        cost.AppDate = model.lasttime;
                        cost.SupplierName = model.supplier_name;
                        cost.Description = model.project_descripttion;
                        cost.GroupName = model.requestor_group;
                        //获取已付PN总和
                        if (model.PRType == (int)ESP.Purchase.Common.PRTYpe.MediaPR || model.PRType == (int)ESP.Purchase.Common.PRTYpe.PrivatePR)
                        {
                            ESP.Purchase.Entity.MediaPREditHisInfo relationModel = ESP.Purchase.BusinessLogic.MediaPREditHisManager.GetModelByOldPRID(model.id);
                            if (relationModel != null)
                            {
                                string str = string.Empty;
                                if (relationModel.NewPRId != null && relationModel.NewPRId.Value != 0)
                                {
                                    str = "(PRID=" + relationModel.NewPRId.ToString();
                                    if (relationModel.NewPNId != null && relationModel.NewPNId.Value != 0)
                                    {
                                        str += "or returnid=" + relationModel.NewPNId + ")";
                                    }
                                    else
                                    {
                                        str += ")";
                                    }
                                }
                                else if (relationModel.NewPNId != null && relationModel.NewPNId.Value != 0)
                                {
                                    str += " returnid=" + relationModel.NewPNId;
                                }


                                returnList = ESP.Finance.BusinessLogic.ReturnManager.GetList(str + " and ReturnStatus=" + ((int)ESP.Finance.Utility.PaymentStatus.FinanceComplete).ToString());
                            }
                        }
                        else
                            returnList = ESP.Finance.BusinessLogic.ReturnManager.GetList(" PRID=" + model.id.ToString() + " and ReturnStatus=" + ((int)ESP.Finance.Utility.PaymentStatus.FinanceComplete).ToString());
                        decimal paidAmount = 0;
                        if (returnList != null && returnList.Count > 0)
                            foreach (ESP.Finance.Entity.ReturnInfo rmodel in returnList)
                            {
                                if (rmodel!=null)
                                paidAmount += rmodel.FactFee == null ? rmodel.PreFee.Value : rmodel.FactFee.Value;
                            }

                        cost.PaidAmount = paidAmount.ToString("#,##0.00");
                        decimal unpaid = (Convert.ToDecimal(cost.AppAmount) - paidAmount);
                        if (unpaid < 0)
                        { cost.UnPaidAmount = "0.00"; }
                        else { cost.UnPaidAmount = unpaid.ToString(); }

                        costList.Add(cost);
                    }
                }
            }
            return costList;
        }
    }
}
