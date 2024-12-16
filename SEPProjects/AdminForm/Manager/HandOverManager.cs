using System;
using System.Data;
using System.Collections.Generic;
using System.Linq.Expressions;
using AdminForm.Model;
using AdminForm.Data;
using System.Data.SqlClient;
using System.Linq;

namespace AdminForm.Manager
{
    public static class HandOverManager
    {

        public static int FirstFinanceHandOver(UserInfo DimissionUser,UserInfo ReceiverUser,int branchId)
        {
            int ret = 0;
            var branchModel = BranchManager.Get(branchId);
            var branchDeptList = BranchDeptManager.GetList(x=>x.FianceFirstAuditorID==DimissionUser.UserID);

            string connstring =System.Configuration.ConfigurationManager.ConnectionStrings["ESP"].ToString();

            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                        //更新分公司出纳
                    branchModel.FirstFinanceID = ReceiverUser.UserID;
                    branchModel.DimissionAuditor = ReceiverUser.UserID;
                    branchModel.OtherFinancialUsers = branchModel.OtherFinancialUsers + ReceiverUser.UserID + ",";
                    BranchManager.Update(branchModel);

                    string[] returntypes={"30","31","32","33","34","35","36","37","40"};

                    var returnList = ReturnManager.GetList(x => x.BranchCode == branchModel.BranchCode && returntypes.Contains(x.ReturnType.ToString()));
                    
                   
                    foreach (var model in returnList)
                    {
                        var workItemList = WorkItemManager.GetList(x =>x.EntityId==model.ReturnID);
                        var instanceModel = WorkflowInstancesManager.Get(workItemList[0].WorkflowInstanceId);

                        //更新工作流主表
                        instanceModel.Users = instanceModel.Users.Replace(DimissionUser.UserID.ToString(), ReceiverUser.UserID.ToString());
                        WorkflowInstancesManager.Update(instanceModel);
                        //更新工作流节点审批人
                        WorkItemAssigneesManager.UpdateFirstFianceHandOver(workItemList, DimissionUser.UserID, ReceiverUser.UserID);
                        //更新PN单审批人记录表
                        ExpenseAuditerListManager.UpdateFirstFinanceHandOver(model, DimissionUser, ReceiverUser);

                    }

                   

                    trans.Commit();

                }
                catch
                {
                    trans.Rollback();
                    return 0;
                }
            }

            return ret;
        }
    }
}
