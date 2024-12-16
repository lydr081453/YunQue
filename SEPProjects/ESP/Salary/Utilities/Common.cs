using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Script.Serialization;
using System.Data;
using System.Linq;
using System.Reflection;
using ESP.Compatible;
using System.IO;
using  System.Globalization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using ESP.Framework.Entity;
using ESP.Framework.BusinessLogic;
using ESP.Salary.SqlDataAccess;
using System.Security.Cryptography;
using System.Linq.Expressions;

namespace ESP.Salary.Utility
{

    public static class PredicateBuilder
    {
        public static Expression<Func<T, bool>> True<T>() { return f => true; }
        public static Expression<Func<T, bool>> False<T>() { return f => false; }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1,
                                                            Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                 (Expression.Or(expr1.Body, invokedExpr), expr1.Parameters);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1,
                                                             Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                 (Expression.And(expr1.Body, invokedExpr), expr1.Parameters);
        }
    }

    public class Common
    {

        public static string EncodePassword(string password, byte[] passwordSaltBytes)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(password);
            byte[] dest = new byte[passwordSaltBytes.Length + bytes.Length];
            Buffer.BlockCopy(passwordSaltBytes, 0, dest, 0, passwordSaltBytes.Length);
            Buffer.BlockCopy(bytes, 0, dest, passwordSaltBytes.Length, bytes.Length);
            HashAlgorithm algorithm = HashAlgorithm.Create("SHA1");
            byte[] inArray = algorithm.ComputeHash(dest);
            return Convert.ToBase64String(inArray);
        }

        public static byte[] GenerateSalt()
        {
            byte[] data = new byte[0x10];
            new RNGCryptoServiceProvider().GetBytes(data);
            return data;
        }

        /// <summary>
        /// 工资的状态
        /// </summary>
        public enum SalaryStatus : int
        {
            /// <summary>
            /// 删除的
            /// </summary>
            Deleted = 0,

            /// <summary>
            /// 正在使用
            /// </summary>
            Normal = 1,

            /// <summary>
            /// 提交
            /// </summary>
            Submit = 2,

            /// <summary>
            /// 总监审核通过
            /// </summary>
            Major_Passed = 3,

            /// <summary>
            /// 总监审核驳回
            /// </summary>
            Major_UnPassed = 4,

            /// <summary>
            /// 总经理审核通过
            /// </summary>
            Manager_Passed = 5,

            /// <summary>
            /// 总经理审核驳回
            /// </summary>
            Manager_UnPassed = 6,

            /// <summary>
            /// 总裁审核通过
            /// </summary>
            Boss_Passed = 7,

            /// <summary>
            /// 总裁审核驳回
            /// </summary>
            Boss_UnPassed = 8,

            /// <summary>
            /// 发放
            /// </summary>
            Provide = 9

        }

        /// <summary>
        /// 奖金的状态
        /// </summary>
        public enum BonusStatus : int
        {
            /// <summary>
            /// 删除的
            /// </summary>
            Deleted = 0,

            /// <summary>
            /// 正在使用
            /// </summary>
            Normal = 1,

            /// <summary>
            /// 提交
            /// </summary>
            Submit = 2,

            /// <summary>
            /// 总监审核通过
            /// </summary>
            Major_Passed = 3,

            /// <summary>
            /// 总监审核驳回
            /// </summary>
            Major_UnPassed = 4,

            /// <summary>
            /// 总经理审核通过
            /// </summary>
            Manager_Passed = 5,

            /// <summary>
            /// 总经理审核驳回
            /// </summary>
            Manager_UnPassed = 6,

            /// <summary>
            /// 总裁审核通过
            /// </summary>
            Boss_Passed = 7,

            /// <summary>
            /// 总裁审核驳回
            /// </summary>
            Boss_UnPassed = 8,

            /// <summary>
            /// 发放
            /// </summary>
            Provide = 9

        }

        /// <summary>
        /// 税率类型
        /// </summary>
        public enum TaxRateType : int
        {
            /// <summary>
            /// 工资
            /// </summary>
            Salary = 1,
            /// <summary>
            /// 税率
            /// </summary>
            Bonus = 2
        }

        /// <summary>
        /// 月工作日
        /// </summary>
        public static decimal WorkDays = Convert.ToDecimal(21.75);

        /// <summary>
        /// 每天工作小时数
        /// </summary>
        public const decimal WorkHoursOfDay = 8;

        /// <summary>
        /// 病事假大于这个数字的时候按照实际工作天数发放餐补和绩效
        /// </summary>
        public const decimal LeaveCountForMealSupplement = 10;

        /// <summary>
        /// 全年累计病假上限(病事假大于这个数字的时候公式会发生改变)
        /// </summary>
        public const decimal LeaveCeiling = 10;

        /// <summary>
        /// 标准餐补
        /// </summary>
        public const decimal MealSupplement = 330;

        /// <summary>
        /// 每日餐补(仅用于新员工)
        /// </summary>
        public const decimal MealSupplementForDay = 15; 

        /// <summary>
        /// 总部ID
        /// </summary>
        public const decimal HeadCompany = 19;

        /// <summary>
        /// 上海ID
        /// </summary>
        public const decimal ShangHaiCompany = 17;

        /// <summary>
        /// 广州ID
        /// </summary>
        public const decimal GuangzhouCompany = 18;

        /// <summary>
        /// 养老保险个人记提比率
        /// </summary>
        public const decimal PensionPersonalRate = 0.08M; 

        /// <summary>
        /// 养老保险公司记提比率
        /// </summary>
        public const decimal PensionPublicRate = 0.2M;

        /// <summary>
        /// 失业保险个人记提比率
        /// </summary>
        public const decimal UnemploymentInsuranceRersonalRate = 0.002M; 

        /// <summary>
        /// 失业保险公司记提比率
        /// </summary>
        public const decimal UnemploymentPublicRersonalRate = 0.01M; 

        /// <summary>
        /// 医疗保险个人记提比率
        /// </summary>
        public const decimal MedicalInsurancePersonalRate = 0.02M;   

        /// <summary>
        /// 医疗保险公司记提比率
        /// </summary>
        public const decimal MedicalInsurancePublicRate = 0.1M;   

        /// <summary>
        /// 医疗保险记提后补冲金额
        /// </summary>
        public const decimal MedicalInsuranceComplement = 3;

        /// <summary>
        /// 住房公积金个人记提比率
        /// </summary>
        public const decimal HousingFundPersonalRate = 0.12M;

        /// <summary>
        /// 住房公积金公司记提比率
        /// </summary>
        public const decimal HousingFundPublicRate = 0.12M;

        /// <summary>
        /// 最低实发额度 
        /// </summary>
        public const decimal MinSalaryReal = 640.00M;

        /// <summary>
        /// 赔偿金计税标尺（社平工资年收入3倍） 
        /// </summary>
        public const decimal CompensationStandard = 134136.00M;

        /// <summary>
        /// 税率对应扣除数
        /// </summary>
        public enum TaxRateMoney : int
        {
            /// <summary>
            /// 百分之5
            /// </summary>
            TaxRate_5Percent = 0,
            /// <summary>
            /// 百分之10
            /// </summary>
            TaxRate_10Percent = 25,
            /// <summary>
            /// 百分之15
            /// </summary>
            TaxRate_15Percent = 125,
            /// <summary>
            /// 百分之20
            /// </summary>
            TaxRate_20Percent = 375,
            /// <summary>
            /// 百分之25
            /// </summary>
            TaxRate_25Percent = 1375,
            /// <summary>
            /// 百分之30
            /// </summary>
            TaxRate_30Percent = 3375,
            /// <summary>
            /// 百分之35
            /// </summary>
            TaxRate_35Percent = 6375,
            /// <summary>
            /// 百分之40
            /// </summary>
            TaxRate_40Percent = 10375,
            /// <summary>
            /// 百分之45
            /// </summary>
            TaxRate_45Percent = 15375
        }

        public enum DeductionAmount : int
        {
            /// <summary>
            /// 普通员工
            /// </summary>
            DefaultDeduction = 2000,

            /// <summary>
            /// 外籍员工并且有工作许可证
            /// </summary>
            ForeignDeduction = 4800
        }

        //默认密钥
        public static string Salary_Keys = "1234abcd1234abcd1234abcd1234abcd";

        //审核类型

        /// <summary>
        /// 总监审核
        /// </summary>
        public static string AuditType_Major = "Major";
        /// <summary>
        /// 总经理审核
        /// </summary>
        public static string AuditType_Manager = "Manager";
        /// <summary>
        /// Boss审核
        /// </summary>
        public static string AuditType_Boss = "Boss";
        /// <summary>
        /// 财务发放
        /// </summary>
        public static string AuditType_Finance = "Finance";


        public static string[] AuditStatusName = { "已删除", "已保存", "已提交", "总监审核通过", "总监审核驳回", "总经理审核通过", "总经理审核驳回", "总裁审核通过", "总裁审核驳回", "已发放" };
        
    }

    public static class QueryString
    {
        public static string AddParam(this string querystring, string paramname, object value)
        {
            string q = querystring.Substring(querystring.IndexOf('?') + 1);
            q = RemoveParam(querystring, paramname);
            q += string.Format("&{0}={1}", paramname, value == null ? string.Empty : value.ToString());
            querystring = q;
            return q;
        }


        public static string ModifyParam(this string querystring, string paramname, object value)
        {
            if (value == null) return querystring;
            string q = querystring.Substring(querystring.IndexOf('?') + 1);
            if (q.IndexOf(paramname) == -1) return q;
            string[] ps = q.Split('&');
            string newps = string.Empty;
            for (int i = 0; i < ps.Length; i++)
            {
                if (ps[i].Split('=')[0] == paramname)
                {
                    ps[i].Split('=')[1] = value.ToString();
                }
                newps += ps[i];
            }
            querystring = newps;
            return newps;
        }

        public static string RemoveParam(this string querystring, string paramname)
        {
            string q = querystring.Substring(querystring.IndexOf('?') + 1);
            string[] ps = q.Split('&');
            string newps = string.Empty;
            for (int i = 0; i < ps.Length; i++)
            {
                if (ps[i].Split('=')[0] != paramname)
                {
                    newps += "&" + ps[i];
                }

            }
            querystring = newps.TrimStart('&');
            return querystring;
        }
    }

}