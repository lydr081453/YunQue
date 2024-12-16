using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ESP.ITIL.BusinessLogic
{
    /// <summary>
    /// 项目运维业务逻辑
    /// </summary>
    public partial class Project
    {
    }

    public class 项目费用统计
    {
        //这个类中的方法都是在项目信息中设置的费用统计，无采购、财务系统的花费统计

        public static decimal 项目_主申请方第三方费用(int pid)
        {
            string terms = string.Empty;
            decimal sum = 0;

            if (pid > 0)
            {
                terms += string.Format(" and projectid={0} ", pid.ToString());
            }

            DataSet ds = ESP.ITIL.DataAccess.Project.Project.主申请方费用明细(terms);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (null != ds.Tables[0].Rows[i]["Cost"] && ds.Tables[0].Rows[i]["Cost"].ToString() != "" && decimal.Parse(ds.Tables[0].Rows[i]["Cost"].ToString()) > 0)
                    {
                        sum += decimal.Parse(ds.Tables[0].Rows[i]["Cost"].ToString());
                    }
                }
            }

            return sum;
        }

        public static decimal 项目_主申请方OOP费用(int pid)
        {
            string terms = string.Empty;
            decimal sum = 0;

            if (pid > 0)
            {
                terms += string.Format(" and projectid={0} and description='OOP' ", pid.ToString());
            }

            DataSet ds = ESP.ITIL.DataAccess.Project.Project.项目_主申请方其他费用(terms);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (null != ds.Tables[0].Rows[i]["Expense"] && ds.Tables[0].Rows[i]["Expense"].ToString() != "" && decimal.Parse(ds.Tables[0].Rows[i]["Expense"].ToString()) > 0)
                    {
                        sum += decimal.Parse(ds.Tables[0].Rows[i]["Expense"].ToString());
                    }
                }
            }

            return sum;
        }

        public static decimal 项目_主申请方媒体车马费用(int pid)
        {
            string terms = string.Empty;
            decimal sum = 0;

            if (pid > 0)
            {
                terms += string.Format(" and projectid={0} and description='Media' ", pid.ToString());
            }

            DataSet ds = ESP.ITIL.DataAccess.Project.Project.项目_主申请方其他费用(terms);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (null != ds.Tables[0].Rows[i]["Expense"] && ds.Tables[0].Rows[i]["Expense"].ToString() != "" && decimal.Parse(ds.Tables[0].Rows[i]["Expense"].ToString()) > 0)
                    {
                        sum += decimal.Parse(ds.Tables[0].Rows[i]["Expense"].ToString());
                    }
                }
            }

            return sum;
        }

        public static decimal 项目_所有支持方第三方费用(int pid)
        {
            //得出的结果包含一个项目中所有的支持方的金额
            return 项目_某一支持方成本明细费用(pid, 0);
        }

        public static decimal 项目_某一支持方成本明细费用(int pid, int groupid)
        {
            //得出的结果包含一个项目中的某一支持方的金额，groupid为这个支持方的部门ID
            string terms = string.Empty;
            decimal sum = 0;

            if (pid > 0)
            {
                terms += string.Format(" and b.ProjectID={0} ", pid.ToString());
            }

            if (groupid > 0)
            {
                terms += string.Format(" and GroupID={0} ", groupid.ToString());
            }

            DataSet ds = ESP.ITIL.DataAccess.Project.Project.支持方费用明细(terms);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (null != ds.Tables[0].Rows[i]["Amounts"] && ds.Tables[0].Rows[i]["Amounts"].ToString() != "" && decimal.Parse(ds.Tables[0].Rows[i]["Amounts"].ToString()) > 0)
                    {
                        sum += decimal.Parse(ds.Tables[0].Rows[i]["Amounts"].ToString());
                    }
                }
            }

            return sum;
        }

        public static decimal 项目_所有支持方OOP费用(int pid)
        {
            return 项目_支持方OOP费用(pid,0);
        }

        public static decimal 项目_所有支持方媒体车马费用(int pid)
        {
            return 项目_支持方媒体车马费用(pid, 0);
        }

        public static decimal 项目_支持方OOP费用(int pid,int groupid)
        {
            //得出的结果包含一个项目中的某一支持方的金额，groupid为这个支持方的部门ID
            string terms = string.Empty;
            decimal sum = 0;
            terms += " and description='OOP' ";
            if (pid > 0)
            {
                terms += string.Format(" and b.ProjectID={0} ", pid.ToString());
            }

            if (groupid > 0)
            {
                terms += string.Format(" and GroupID={0} ", groupid.ToString());
            }

            DataSet ds = ESP.ITIL.DataAccess.Project.Project.项目_支持方其他费用(terms);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (null != ds.Tables[0].Rows[i]["Expense"] && ds.Tables[0].Rows[i]["Expense"].ToString() != "" && decimal.Parse(ds.Tables[0].Rows[i]["Expense"].ToString()) > 0)
                    {
                        sum += decimal.Parse(ds.Tables[0].Rows[i]["Expense"].ToString());
                    }
                }
            }

            return sum;
        }

        public static decimal 项目_支持方媒体车马费用(int pid,int groupid)
        {

            //得出的结果包含一个项目中的某一支持方的金额，groupid为这个支持方的部门ID
            string terms = string.Empty;
            decimal sum = 0;
            terms += " and description='Media' ";

            if (pid > 0)
            {
                terms += string.Format(" and b.ProjectID={0} ", pid.ToString());
            }

            if (groupid > 0)
            {
                terms += string.Format(" and GroupID={0} ", groupid.ToString());
            }

            DataSet ds = ESP.ITIL.DataAccess.Project.Project.项目_支持方其他费用(terms);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (null != ds.Tables[0].Rows[i]["Expense"] && ds.Tables[0].Rows[i]["Expense"].ToString() != "" && decimal.Parse(ds.Tables[0].Rows[i]["Expense"].ToString()) > 0)
                    {
                        sum += decimal.Parse(ds.Tables[0].Rows[i]["Expense"].ToString());
                    }
                }
            }

            return sum;
        }

        public static decimal 项目_第三方费用(int pid)
        {
            return 项目_主申请方第三方费用(pid) + 项目_所有支持方第三方费用(pid);
        }

        public static decimal 项目_项目OOP费用(int pid)
        {
            return 项目_主申请方OOP费用(pid) + 项目_所有支持方OOP费用(pid);
        }

        public static decimal 项目_项目媒体车马费用(int pid)
        {
            return 项目_主申请方媒体车马费用(pid) + 项目_所有支持方媒体车马费用(pid);
        }

        public static decimal 项目费用总和(int pid)
        {
            return 项目_第三方费用(pid) + 项目_项目OOP费用(pid) + 项目_项目媒体车马费用(pid);
        }

        #region 根据projectid和groupid确定项目费用
        public static decimal 项目_第三方费用(int pid, int groupid)
        {
            string terms = string.Empty;
            decimal sum = 0;

            if (pid > 0)
            {
                terms += string.Format(" and ProjectId={0} ", pid.ToString());
            }

            if (groupid > 0)
            {
                terms += string.Format(" and GroupID={0} ", groupid.ToString());
            }

            DataSet ds = ESP.ITIL.DataAccess.Project.Project.项目_项目信息(terms);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                sum = 项目_主申请方第三方费用(pid);
            }
            else
            {
                sum = 项目_某一支持方成本明细费用(pid, groupid);
            }
            return sum;

        }
        public static decimal 项目_OOP费用(int pid, int groupid)
        {
            string terms = string.Empty;
            decimal sum = 0;

            if (pid > 0)
            {
                terms += string.Format(" and ProjectId={0} ", pid.ToString());
            }

            if (groupid > 0)
            {
                terms += string.Format(" and GroupID={0} ", groupid.ToString());
            }

            DataSet ds = ESP.ITIL.DataAccess.Project.Project.项目_项目信息(terms);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                sum = 项目_主申请方OOP费用(pid);
            }
            else
            {
                sum = 项目_支持方OOP费用(pid, groupid);
            }
            return sum;
        }
        public static decimal 项目_媒体车马费用(int pid, int groupid)
        {
            string terms = string.Empty;
            decimal sum = 0;

            if (pid > 0)
            {
                terms += string.Format(" and ProjectId={0} ", pid.ToString());
            }

            if (groupid > 0)
            {
                terms += string.Format(" and GroupID={0} ", groupid.ToString());
            }

            DataSet ds = ESP.ITIL.DataAccess.Project.Project.项目_项目信息(terms);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                sum = 项目_主申请方媒体车马费用(pid);
            }
            else
            {
                sum = 项目_支持方媒体车马费用(pid, groupid);
            }
            return sum;
        }
        #endregion
    }

    public class 已提交项目费用成本明细统计
    {
        //某一项目下某成本明细已花费用
        public static decimal 已花费第三方费用(int pid, int typeid, int did)
        {
            //项目信息中修改成本明细时用这个类来做校验，因为无法使用帐期来判断，只能用t_orderinfo来判断

            //在项目修改成本明细费用时，需要先按照这个函数统计已提交的PR单中已占用的费用，并且由于无法准确确认真实已花费的金额，只按照orderinfo中的金额计算
            string terms = string.Empty;
            decimal sum = 0;

            if (pid > 0)
            {
                terms += string.Format(" and project_id={0} ", pid.ToString());
            }

            //当typeid<>0时，取得特殊类别的采购物品，当typeid==0,取得所有物品
            if (typeid > 0)
            {
                terms += string.Format(" and producttype={0} ", typeid.ToString());
            }

            //当did<>0时，相应部门的物品，当did==0,取得所有物品
            if (did > 0)
            {
                terms += string.Format(" and a.departmentid={0} ", did.ToString());
            }
            DataSet ds = ESP.ITIL.DataAccess.Project.Project.采购物品明细(terms);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (null != ds.Tables[0].Rows[i]["total"] && ds.Tables[0].Rows[i]["total"].ToString() != "" && decimal.Parse(ds.Tables[0].Rows[i]["total"].ToString()) > 0)
                    {
                        sum += decimal.Parse(ds.Tables[0].Rows[i]["total"].ToString());
                    }
                }
            }

            return sum;
        }
    }

    public class 第三方费用统计
    {
        //已完成付款的pr单和已停止的PR单
        public static decimal 实际花费_排除本身_已付款和已停止的PR单统计总和(int pid, int groupid, int prid)
        {
            //对于已完成付款和已停止的pr单，只计算完成付款的return纪录
            string terms = string.Empty;
            decimal sum = 0;
            terms += " and a.status in({0},{1}) and returnstatus={2} and b.Status={3} and a.prtype not in(1,6)";
            terms = string.Format(terms, ESP.Purchase.Common.State.requisition_paid, ESP.Purchase.Common.State.requisition_Stop, (int)ESP.Finance.Utility.PaymentStatus.FinanceComplete,
                ESP.Purchase.Common.State.PaymentStatus_over);
            if (pid > 0)
            {
                terms += string.Format(" and project_id={0} ", pid.ToString());
            }

            if (groupid > 0)
            {
                terms += string.Format(" and a.DepartmentId={0} ", groupid.ToString());
            }

            if (prid > 0)
            {
                terms += string.Format(" and a.id<>{0} ", prid.ToString());
            }
            DataSet ds = ESP.ITIL.DataAccess.Project.Project.实际花费_采购系统产生的费用(terms);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (null != ds.Tables[0].Rows[i]["factfee"] && ds.Tables[0].Rows[i]["factfee"].ToString() != "" && decimal.Parse(ds.Tables[0].Rows[i]["factfee"].ToString()) > 0)
                    {
                        sum += decimal.Parse(ds.Tables[0].Rows[i]["factfee"].ToString());
                    }
                }
            }

            return sum;
        }

        #region 对私和稿费3000元以下,直接生成的PN单
        #region 统计全部对私和稿费3000元以下,直接生成的PN单
        public static decimal 实际花费_单项目中所有已提交未付款3000以下PN单总和(int pid)
        {
            return 实际花费_单项目中所有已提交未付款3000以下PN单按部门统计总和(pid, 0);
        }

        public static decimal 实际花费_单项目中所有已付款3000以下PN单总和(int pid)
        {
            return 实际花费_单项目中所有已付款3000以下PN单按部门统计总和(pid, 0);
        }

        public static decimal 实际花费_单项目中所有已提交未付款3000以下PN单按部门统计总和(int pid, int groupid)
        {
            return 实际花费_排除本身_单项目中所有已提交未付款3000以下PN单按部门统计总和(pid, groupid, 0);
        }

        public static decimal 实际花费_单项目中所有已付款3000以下PN单按部门统计总和(int pid, int groupid)
        {
            return 实际花费_排除本身_单项目中所有已付款3000以下PN单按部门统计总和(pid, groupid, 0);
        }

        public static decimal 实际花费_单项目中所有3000以下PN单总和(int pid)
        {
            return 实际花费_单项目中所有已提交未付款3000以下PN单总和(pid) + 实际花费_单项目中所有已付款3000以下PN单总和(pid);
        }

        public static decimal 实际花费_单项目中所有3000以下PN单总和(int pid, int groupid)
        {
            return 实际花费_单项目中所有已提交未付款3000以下PN单按部门统计总和(pid, groupid) + 实际花费_单项目中所有已付款3000以下PN单按部门统计总和(pid, groupid);
        }
        #endregion

        #region 排除自己后统计全部对私和稿费3000元以下,直接生成的PN单
        public static decimal 实际花费_排除本身_单项目中所有已提交未付款3000以下PN单总和(int pid, int returnid)
        {
            return 实际花费_排除本身_单项目中所有已提交未付款3000以下PN单按部门统计总和(pid, 0, returnid);
        }

        public static decimal 实际花费_排除本身_单项目中所有已付款3000以下PN单总和(int pid, int returnid)
        {
            return 实际花费_排除本身_单项目中所有已付款3000以下PN单按部门统计总和(pid, 0, returnid);
        }

        public static decimal 实际花费_排除本身_单项目中所有已提交未付款3000以下PN单按部门统计总和(int pid, int groupid, int returnid)
        {
            string terms = string.Empty;
            decimal sum = 0;
            terms += " and returntype in({0},{1}) and returnstatus<>{2} and returnstatus>{3}";
            terms = string.Format(terms, (int)ESP.Purchase.Common.PRTYpe.MediaPR, (int)ESP.Purchase.Common.PRTYpe.PrivatePR, (int)ESP.Finance.Utility.PaymentStatus.FinanceComplete, (int)ESP.Finance.Utility.PaymentStatus.Save);
            if (pid > 0)
            {
                terms += string.Format(" and projectid={0} ", pid.ToString());
            }

            if (groupid > 0)
            {
                terms += string.Format(" and DepartmentId={0} ", groupid.ToString());
            }

            if (returnid > 0)
            {
                terms += string.Format(" and ReturnID<>{0} ", returnid.ToString());
            }
            DataSet ds = ESP.ITIL.DataAccess.Project.Project.实际花费_其他费用(terms);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (null != ds.Tables[0].Rows[i]["PreFee"] && ds.Tables[0].Rows[i]["PreFee"].ToString() != "" && decimal.Parse(ds.Tables[0].Rows[i]["PreFee"].ToString()) > 0)
                    {
                        sum += decimal.Parse(ds.Tables[0].Rows[i]["PreFee"].ToString());
                    }
                }
            }

            return sum;
        }

        public static decimal 实际花费_排除本身_单项目中所有已付款3000以下PN单按部门统计总和(int pid, int groupid, int returnid)
        {

            string terms = string.Empty;
            decimal sum = 0;
            terms += " and returntype in({0},{1}) and returnstatus={2}";
            terms = string.Format(terms, (int)ESP.Purchase.Common.PRTYpe.MediaPR, (int)ESP.Purchase.Common.PRTYpe.PrivatePR, (int)ESP.Finance.Utility.PaymentStatus.FinanceComplete);
            if (pid > 0)
            {
                terms += string.Format(" and projectid={0} ", pid.ToString());
            }

            if (groupid > 0)
            {
                terms += string.Format(" and DepartmentId={0} ", groupid.ToString());
            }

            if (returnid > 0)
            {
                terms += string.Format(" and ReturnID<>{0} ", returnid.ToString());
            }
            DataSet ds = ESP.ITIL.DataAccess.Project.Project.实际花费_其他费用(terms);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (null != ds.Tables[0].Rows[i]["FactFee"] && ds.Tables[0].Rows[i]["FactFee"].ToString() != "" && decimal.Parse(ds.Tables[0].Rows[i]["FactFee"].ToString()) > 0)
                    {
                        sum += decimal.Parse(ds.Tables[0].Rows[i]["FactFee"].ToString());
                    }
                }
            }

            return sum;
        }

        public static decimal 实际花费_排除本身_单项目中所有3000以下PN单总和(int pid, int returnid)
        {
            return 实际花费_排除本身_单项目中所有已提交未付款3000以下PN单总和(pid, returnid) + 实际花费_排除本身_单项目中所有已付款3000以下PN单总和(pid, returnid);
        }

        public static decimal 实际花费_排除本身_单项目中所有3000以下PN单总和(int pid, int groupid, int returnid)
        {
            return 实际花费_排除本身_单项目中所有已提交未付款3000以下PN单按部门统计总和(pid, groupid, returnid) + 实际花费_排除本身_单项目中所有已付款3000以下PN单按部门统计总和(pid, groupid, returnid);
        }
        #endregion
        #endregion

        #region 对私和稿费3000元以上,生成的PR单的费用
        public static decimal 实际花费_已提交未创建付款新PR单费用(int pid, int groupid, int prid)
        {
            string terms = string.Empty;
            decimal sum = 0;
            terms += " and b.Status={0} and prtype in({1},{2}) and a.status not in({3},{4},{5}) ";
            terms = string.Format(terms, ESP.Purchase.Common.State.PaymentStatus_save, (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA, (int)ESP.Purchase.Common.PRTYpe.PR_PriFA, ESP.Purchase.Common.State.requisition_paid, ESP.Purchase.Common.State.requisition_MediaOperating, ESP.Purchase.Common.State.requisition_Stop);
            if (pid > 0)
            {
                terms += string.Format(" and project_id={0} ", pid.ToString());
            }

            if (groupid > 0)
            {
                terms += string.Format(" and a.DepartmentId={0} ", groupid.ToString());
            }

            if (prid > 0)
            {
                terms += string.Format(" and a.id<>{0} ", prid.ToString());
            }
            DataSet ds = ESP.ITIL.DataAccess.Project.Project.实际花费_采购系统产生的费用(terms);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (null != ds.Tables[0].Rows[i]["expectpaymentprice"] && ds.Tables[0].Rows[i]["expectpaymentprice"].ToString() != "" && decimal.Parse(ds.Tables[0].Rows[i]["expectpaymentprice"].ToString()) > 0)
                    {
                        sum += decimal.Parse(ds.Tables[0].Rows[i]["expectpaymentprice"].ToString());
                    }
                }
            }

            return sum;
        }

        public static decimal 实际花费_已提交未实际付款新PR单费用(int pid, int groupid, int prid)
        {
            string terms = string.Empty;
            decimal sum = 0;
            terms += " and b.Status not in({0},{1}) and prtype in({2},{3}) and a.status not in({4},{5},{6}) ";
            terms = string.Format(terms, ESP.Purchase.Common.State.PaymentStatus_save, ESP.Purchase.Common.State.PaymentStatus_over, (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA, (int)ESP.Purchase.Common.PRTYpe.PR_PriFA, ESP.Purchase.Common.State.requisition_paid, ESP.Purchase.Common.State.requisition_MediaOperating, ESP.Purchase.Common.State.requisition_Stop);
            if (pid > 0)
            {
                terms += string.Format(" and project_id={0} ", pid.ToString());
            }

            if (groupid > 0)
            {
                terms += string.Format(" and a.DepartmentId={0} ", groupid.ToString());
            }

            if (prid > 0)
            {
                terms += string.Format(" and a.id<>{0} ", prid.ToString());
            }
            DataSet ds = ESP.ITIL.DataAccess.Project.Project.实际花费_采购系统产生的费用(terms);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (null != ds.Tables[0].Rows[i]["PreFee"] && ds.Tables[0].Rows[i]["PreFee"].ToString() != "" && decimal.Parse(ds.Tables[0].Rows[i]["PreFee"].ToString()) > 0)
                    {
                        sum += decimal.Parse(ds.Tables[0].Rows[i]["PreFee"].ToString());
                    }
                }
            }

            return sum;
        }

        public static decimal 实际花费_已实际付款新PR单费用(int pid, int groupid, int prid)
        {
            string terms = string.Empty;
            decimal sum = 0;
            terms += " and b.Status ={0} and returnstatus={1} and prtype in({2},{3}) and a.status not in({4},{5},{6}) ";
            terms = string.Format(terms, ESP.Purchase.Common.State.PaymentStatus_over,(int)ESP.Finance.Utility.PaymentStatus.FinanceComplete, (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA, (int)ESP.Purchase.Common.PRTYpe.PR_PriFA, ESP.Purchase.Common.State.requisition_paid, ESP.Purchase.Common.State.requisition_MediaOperating, ESP.Purchase.Common.State.requisition_Stop);
            if (pid > 0)
            {
                terms += string.Format(" and project_id={0} ", pid.ToString());
            }

            if (groupid > 0)
            {
                terms += string.Format(" and a.DepartmentId={0} ", groupid.ToString());
            }

            if (prid > 0)
            {
                terms += string.Format(" and a.id<>{0} ", prid.ToString());
            }
            DataSet ds = ESP.ITIL.DataAccess.Project.Project.实际花费_采购系统产生的费用(terms);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (null != ds.Tables[0].Rows[i]["factfee"] && ds.Tables[0].Rows[i]["factfee"].ToString() != "" && decimal.Parse(ds.Tables[0].Rows[i]["factfee"].ToString()) > 0)
                    {
                        sum += decimal.Parse(ds.Tables[0].Rows[i]["factfee"].ToString());
                    }
                }
            }

            return sum;
        }

        public static decimal 实际花费_对私稿费3000以上所有费用(int pid, int groupid, int prid)
        {
            return 实际花费_已提交未创建付款新PR单费用(pid, groupid, prid) + 实际花费_已提交未实际付款新PR单费用(pid, groupid, prid) +
                   实际花费_已实际付款新PR单费用(pid, groupid, prid);
        }
        #endregion

        #region 对私和稿费未再处理前的费用
        public static decimal 实际花费_对私和稿费未再处理前的费用(int pid, int groupid, int prid)
        {
            string terms = string.Empty;
            decimal sum = 0;
            terms += " and b.Status={0} and prtype in({1},{2}) and a.status not in({3},{4},{5})  ";
            terms = string.Format(terms, ESP.Purchase.Common.State.PaymentStatus_save, (int)ESP.Purchase.Common.PRTYpe.MediaPR, (int)ESP.Purchase.Common.PRTYpe.PrivatePR, ESP.Purchase.Common.State.requisition_paid, ESP.Purchase.Common.State.requisition_MediaOperating, ESP.Purchase.Common.State.requisition_Stop);
            if (pid > 0)
            {
                terms += string.Format(" and project_id={0} ", pid.ToString());
            }

            if (groupid > 0)
            {
                terms += string.Format(" and a.DepartmentId={0} ", groupid.ToString());
            }

            if (prid > 0)
            {
                terms += string.Format(" and a.id<>{0} ", prid.ToString());
            }
            DataSet ds = ESP.ITIL.DataAccess.Project.Project.实际花费_采购系统产生的费用(terms);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (null != ds.Tables[0].Rows[i]["expectpaymentprice"] && ds.Tables[0].Rows[i]["expectpaymentprice"].ToString() != "" && decimal.Parse(ds.Tables[0].Rows[i]["expectpaymentprice"].ToString()) > 0)
                    {
                        sum += decimal.Parse(ds.Tables[0].Rows[i]["expectpaymentprice"].ToString());
                    }
                }
            }

            return sum;
        }
        #endregion

        #region 普通PR单中的费用
        public static decimal 实际花费_已提交未创建付款普通PR单费用(int pid, int groupid, int prid)
        {
            string terms = string.Empty;
            decimal sum = 0;
            terms += " and b.Status={0} and prtype not in({1},{2},{3},{4}) and a.status not in({5},{6}) ";
            terms = string.Format(terms, ESP.Purchase.Common.State.PaymentStatus_save, (int)ESP.Purchase.Common.PRTYpe.MediaPR, (int)ESP.Purchase.Common.PRTYpe.PrivatePR, (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA, (int)ESP.Purchase.Common.PRTYpe.PR_PriFA, ESP.Purchase.Common.State.requisition_paid, ESP.Purchase.Common.State.requisition_Stop);
            if (pid > 0)
            {
                terms += string.Format(" and project_id={0} ", pid.ToString());
            }

            if (groupid > 0)
            {
                terms += string.Format(" and a.DepartmentId={0} ", groupid.ToString());
            }

            if (prid > 0)
            {
                terms += string.Format(" and a.id<>{0} ", prid.ToString());
            }
            DataSet ds = ESP.ITIL.DataAccess.Project.Project.实际花费_采购系统产生的费用(terms);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (null != ds.Tables[0].Rows[i]["expectpaymentprice"] && ds.Tables[0].Rows[i]["expectpaymentprice"].ToString() != "" && decimal.Parse(ds.Tables[0].Rows[i]["expectpaymentprice"].ToString()) > 0)
                    {
                        sum += decimal.Parse(ds.Tables[0].Rows[i]["expectpaymentprice"].ToString());
                    }
                }
            }

            return sum;
        }

        public static decimal 实际花费_已提交未实际付款普通PR单费用(int pid, int groupid, int prid)
        {
            string terms = string.Empty;
            decimal sum = 0;
            terms += " and b.Status not in({0},{1}) and prtype not in({2},{3},{4},{5})  and a.status not in({6},{7})  ";
            terms = string.Format(terms, ESP.Purchase.Common.State.PaymentStatus_save, ESP.Purchase.Common.State.PaymentStatus_over, (int)ESP.Purchase.Common.PRTYpe.MediaPR, (int)ESP.Purchase.Common.PRTYpe.PrivatePR, (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA, (int)ESP.Purchase.Common.PRTYpe.PR_PriFA, ESP.Purchase.Common.State.requisition_paid, ESP.Purchase.Common.State.requisition_Stop);
            if (pid > 0)
            {
                terms += string.Format(" and project_id={0} ", pid.ToString());
            }

            if (groupid > 0)
            {
                terms += string.Format(" and a.DepartmentId={0} ", groupid.ToString());
            }

            if (prid > 0)
            {
                terms += string.Format(" and a.id<>{0} ", prid.ToString());
            }
            DataSet ds = ESP.ITIL.DataAccess.Project.Project.实际花费_采购系统产生的费用(terms);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (null != ds.Tables[0].Rows[i]["PreFee"] && ds.Tables[0].Rows[i]["PreFee"].ToString() != "" && decimal.Parse(ds.Tables[0].Rows[i]["PreFee"].ToString()) > 0)
                    {
                        sum += decimal.Parse(ds.Tables[0].Rows[i]["PreFee"].ToString());
                    }
                }
            }

            return sum;
        }

        public static decimal 实际花费_已实际付款普通PR单费用(int pid, int groupid, int prid)
        {
            string terms = string.Empty;
            decimal sum = 0;
            terms += " and b.Status ={0} and returnstatus={1} and prtype not in({2},{3},{4},{5})  and a.status not in({6},{7})  ";
            terms = string.Format(terms, ESP.Purchase.Common.State.PaymentStatus_over,(int)ESP.Finance.Utility.PaymentStatus.FinanceComplete, (int)ESP.Purchase.Common.PRTYpe.MediaPR, (int)ESP.Purchase.Common.PRTYpe.PrivatePR, (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA, (int)ESP.Purchase.Common.PRTYpe.PR_PriFA, ESP.Purchase.Common.State.requisition_paid, ESP.Purchase.Common.State.requisition_Stop);
            if (pid > 0)
            {
                terms += string.Format(" and project_id={0} ", pid.ToString());
            }

            if (groupid > 0)
            {
                terms += string.Format(" and a.DepartmentId={0} ", groupid.ToString());
            }

            if (prid > 0)
            {
                terms += string.Format(" and a.id<>{0} ", prid.ToString());
            }
            DataSet ds = ESP.ITIL.DataAccess.Project.Project.实际花费_采购系统产生的费用(terms);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (null != ds.Tables[0].Rows[i]["factfee"] && ds.Tables[0].Rows[i]["factfee"].ToString() != "" && decimal.Parse(ds.Tables[0].Rows[i]["factfee"].ToString()) > 0)
                    {
                        sum += decimal.Parse(ds.Tables[0].Rows[i]["factfee"].ToString());
                    }
                }
            }

            return sum;
        }


        public static decimal 实际花费_普通PR单所有费用(int pid, int groupid, int prid)
        {
            return 实际花费_已提交未创建付款普通PR单费用(pid, groupid, prid) + 实际花费_已提交未实际付款普通PR单费用(pid, groupid, prid) +
                   实际花费_已实际付款普通PR单费用(pid, groupid, prid);
        }
        #endregion


        #region 报销中除oop外其他的费用的统计
        public static decimal 实际花费_单项目中所有已提交未付款除OOP外其他的费用按部门统计总和(int pid, int groupid, int returnid)
        {
            string terms1 = string.Empty;
            string terms2 = string.Empty;
            decimal sum = 0;
            terms1=" and CostDetailID>0";

            terms2 += " and returntype in ({0}) and returnstatus<>{1} and returnstatus>{2} ";
            terms2 = string.Format(terms2, (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccount + "," + (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountCashBorrow + "," + (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountBusinessCard + "," + (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountCheckAndTelegraphic + ","  + (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountThirdParty , (int)ESP.Finance.Utility.PaymentStatus.FinanceComplete, (int)ESP.Finance.Utility.PaymentStatus.Save);
               //30,32,33,31,35
            if (pid > 0)
            {
                terms2 += string.Format(" and projectid={0} ", pid.ToString());
            }

            if (groupid > 0)
            {
                terms2 += string.Format(" and DepartmentId={0} ", groupid.ToString());
            }

            if (returnid > 0)
            {
                terms2 += string.Format(" and ReturnID<>{0} ", returnid.ToString());
            }
            DataSet ds = ESP.ITIL.DataAccess.Project.Project.实际花费_报销费用(terms1,terms2);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (null != ds.Tables[0].Rows[i]["totalcost"] && ds.Tables[0].Rows[i]["totalcost"].ToString() != "" && decimal.Parse(ds.Tables[0].Rows[i]["totalcost"].ToString()) > 0)
                    {
                        sum += decimal.Parse(ds.Tables[0].Rows[i]["totalcost"].ToString());
                    }
                }
            }

            return sum;
        }

        public static decimal 实际花费_单项目中所有已付款除OOP外其他的费用按部门统计总和(int pid, int groupid, int returnid)
        {

            string terms1 = string.Empty;
            string terms2 = string.Empty;
            decimal sum = 0;
            terms1 = " and CostDetailID>0";
            terms2 += " and returntype in ({0}) and returnstatus={1}  ";
            terms2 = string.Format(terms2, (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccount + "," + (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountCashBorrow + "," + (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountBusinessCard + "," + (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountCheckAndTelegraphic + ","  + (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountThirdParty , (int)ESP.Finance.Utility.PaymentStatus.FinanceComplete);
            if (pid > 0)
            {
                terms2 += string.Format(" and projectid={0} ", pid.ToString());
            }

            if (groupid > 0)
            {
                terms2 += string.Format(" and DepartmentId={0} ", groupid.ToString());
            }

            if (returnid > 0)
            {
                terms2 += string.Format(" and ReturnID<>{0} ", returnid.ToString());
            }
            DataSet ds = ESP.ITIL.DataAccess.Project.Project.实际花费_报销费用(terms1, terms2);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (null != ds.Tables[0].Rows[i]["totalcost"] && ds.Tables[0].Rows[i]["totalcost"].ToString() != "" && decimal.Parse(ds.Tables[0].Rows[i]["totalcost"].ToString()) > 0)
                    {
                        sum += decimal.Parse(ds.Tables[0].Rows[i]["totalcost"].ToString());
                    }
                }
            }

            return sum;
        }

        public static decimal 实际花费_除OOP外其他的费用(int pid, int groupid, int returnid)
        {
            return 实际花费_单项目中所有已提交未付款除OOP外其他的费用按部门统计总和(pid, groupid, returnid) +
                   实际花费_单项目中所有已付款除OOP外其他的费用按部门统计总和(pid, groupid, returnid);
        }
        #endregion 报销中除oop外其他的费用的统计

        //所有已花费的第三方费用
        public static decimal 所有已花费的第三方费用(int pid,int groupid)
        {
            //return 实际花费_排除本身_已付款和已停止的PR单统计总和(pid, groupid, 0) + 实际花费_单项目中所有3000以下PN单总和(pid, groupid) +
            //       实际花费_对私稿费3000以上所有费用(pid, groupid, 0) + 实际花费_对私和稿费未再处理前的费用(pid, groupid, 0) +
            //       实际花费_普通PR单所有费用(pid, groupid, 0) + 实际花费_除OOP外其他的费用(pid, groupid, 0);
            return ESP.Finance.BusinessLogic.CheckerManager.GetOccupyCost(pid, groupid) + 实际花费_除OOP外其他的费用(pid, groupid, 0);
        }

        public static decimal 所有已花费的第三方费用(int pid, int groupid, int prid)
        {
            //return 实际花费_排除本身_已付款和已停止的PR单统计总和(pid, groupid, prid) + 实际花费_单项目中所有3000以下PN单总和(pid, groupid) +
            //       实际花费_对私稿费3000以上所有费用(pid, groupid, prid) + 实际花费_对私和稿费未再处理前的费用(pid, groupid, prid) +
            //       实际花费_普通PR单所有费用(pid, groupid, prid) + 实际花费_除OOP外其他的费用(pid, groupid, 0);
            return ESP.Finance.BusinessLogic.CheckerManager.GetOccupyCost(pid,groupid,prid)+ 实际花费_除OOP外其他的费用(pid, groupid, 0);
        }
    }
    
    public class OOP费用统计
    {
        #region 统计全部
        public static decimal 实际花费_单项目中所有已提交未付款OOP费总和(int pid)
        {
            return 实际花费_单项目中所有已提交未付款OOP费按部门统计总和(pid, 0);
        }

        public static decimal 实际花费_单项目中所有已付款OOP费总和(int pid)
        {
            return 实际花费_单项目中所有已付款OOP费按部门统计总和(pid, 0);
        }

        public static decimal 实际花费_单项目中所有已提交未付款OOP费按部门统计总和(int pid, int groupid)
        {
            return 实际花费_排除本身_单项目中所有已提交未付款OOP费按部门统计总和(pid, groupid, 0);
        }

        public static decimal 实际花费_单项目中所有已付款OOP费按部门统计总和(int pid, int groupid)
        {
            return 实际花费_排除本身_单项目中所有已付款OOP费按部门统计总和(pid, groupid, 0);
        }

        public static decimal 实际花费_单项目中所有OOP费总和(int pid)
        {
            return 实际花费_单项目中所有已提交未付款OOP费总和(pid) + 实际花费_单项目中所有已付款OOP费总和(pid);
        }

        public static decimal 实际花费_单项目中所有OOP费总和(int pid,int groupid)
        {
            return 实际花费_单项目中所有已提交未付款OOP费按部门统计总和(pid, groupid) + 实际花费_单项目中所有已付款OOP费按部门统计总和(pid, groupid);
        }
        #endregion

        #region 排除自己后统计全部
        public static decimal 实际花费_排除本身_单项目中所有已提交未付款OOP费总和(int pid, int returnid)
        {
            return 实际花费_排除本身_单项目中所有已提交未付款OOP费按部门统计总和(pid, 0, returnid);
        }

        public static decimal 实际花费_排除本身_单项目中所有已付款OOP费总和(int pid, int returnid)
        {
            return 实际花费_排除本身_单项目中所有已付款OOP费按部门统计总和(pid, 0, returnid);
        }

        public static decimal 实际花费_排除本身_单项目中所有已提交未付款OOP费按部门统计总和(int pid, int groupid, int returnid)
        {

            string terms1 = string.Empty;
            string terms2 = string.Empty;
            decimal sum = 0;
            terms1 = " and CostDetailID=0";
            terms2 += " and returntype in ({0}) and returnstatus<>{1} and returnstatus>{2} ";
            terms2 = string.Format(terms2, (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccount + "," + (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountCashBorrow + "," + (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountBusinessCard + "," + (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountThirdParty + "," + (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountCheckAndTelegraphic, (int)ESP.Finance.Utility.PaymentStatus.FinanceComplete, (int)ESP.Finance.Utility.PaymentStatus.Save);
            if (pid > 0)
            {
                terms2 += string.Format(" and projectid={0} ", pid.ToString());
            }

            if (groupid > 0)
            {
                terms2 += string.Format(" and DepartmentId={0} ", groupid.ToString());
            }

            if (returnid > 0)
            {
                terms2 += string.Format(" and ReturnID<>{0} ", returnid.ToString());
            }
            DataSet ds = ESP.ITIL.DataAccess.Project.Project.实际花费_报销费用(terms1, terms2);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (null != ds.Tables[0].Rows[i]["totalcost"] && ds.Tables[0].Rows[i]["totalcost"].ToString() != "" && decimal.Parse(ds.Tables[0].Rows[i]["totalcost"].ToString()) > 0)
                    {
                        sum += decimal.Parse(ds.Tables[0].Rows[i]["totalcost"].ToString());
                    }
                }
            }

            return sum;
        }

        public static decimal 实际花费_排除本身_单项目中所有已付款OOP费按部门统计总和(int pid, int groupid, int returnid)
        {
            string terms1 = string.Empty;
            string terms2 = string.Empty;
            decimal sum = 0;
            terms1 = " and CostDetailID=0";
            terms2 += " and returntype in ({0}) and returnstatus={1} ";
            terms2 = string.Format(terms2, (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccount + "," + (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountCashBorrow + "," + (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountCheckAndTelegraphic + "," + (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountBusinessCard + "," + (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountThirdParty, (int)ESP.Finance.Utility.PaymentStatus.FinanceComplete);
            if (pid > 0)
            {
                terms2 += string.Format(" and projectid={0} ", pid.ToString());
            }

            if (groupid > 0)
            {
                terms2 += string.Format(" and DepartmentId={0} ", groupid.ToString());
            }

            if (returnid > 0)
            {
                terms2 += string.Format(" and ReturnID<>{0} ", returnid.ToString());
            }
            DataSet ds = ESP.ITIL.DataAccess.Project.Project.实际花费_报销费用(terms1, terms2);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (null != ds.Tables[0].Rows[i]["totalcost"] && ds.Tables[0].Rows[i]["totalcost"].ToString() != "" && decimal.Parse(ds.Tables[0].Rows[i]["totalcost"].ToString()) > 0)
                    {
                        sum += decimal.Parse(ds.Tables[0].Rows[i]["totalcost"].ToString());
                    }
                }
            }

            return sum;
        }

        public static decimal 实际花费_排除本身_单项目中所有OOP费总和(int pid, int returnid)
        {
            return 实际花费_排除本身_单项目中所有已提交未付款OOP费总和(pid, returnid) + 实际花费_排除本身_单项目中所有已付款OOP费总和(pid, returnid);
        }
        #endregion
    }
}
