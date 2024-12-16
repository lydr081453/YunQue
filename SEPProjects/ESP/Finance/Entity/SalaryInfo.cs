using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.Entity
{
    [Serializable]
    public class SalaryInfo
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserCode { get; set; }
        public string NameCN { get; set; }
        public string NameEN { get; set; }
        public int SalaryYear { get; set; }
        public int SalaryMonth { get; set; }
        public string IDNumber { get; set; }
        public string EmailPassword { get; set; }
        public DateTime EmailSendTime { get; set; }


        /// <summary>
        ///  工资总额
        /// </summary>
        public string SalaryTotal { get; set; }
               /// <summary>
        /// 基本工资
        /// </summary>
        public string SalaryBased { get; set; }
        /// <summary>
        /// 绩效工资
        /// </summary>
        public string SalaryPerformance { get; set; }

        /// <summary>
        /// 迟到扣款
        /// </summary>
        public string LateCut { get; set; }
        /// <summary>
        /// 缺勤扣款早退
        /// </summary>
        public string AbsenceCut { get; set; }

        /// <summary>
        /// 事假扣款
        /// </summary>
        public string AffairCut { get; set; }

        /// <summary>
        /// 病假扣款
        /// </summary>
        public string SickCut { get; set; }
      
        /// <summary>
        /// 忘打卡
        /// </summary>
        public string ClockCut { get; set; }
        /// <summary>
        /// 考勤扣款小计
        /// </summary>
        public string KaoqinTotal { get; set; }
        /// <summary>
        ///  其他减除费用 
        /// </summary>
        public string OtherCut { get; set; }
        /// <summary>
        ///  其他增加费用 
        /// </summary>
        public string OtherIncome { get; set; }
        /// <summary>
        /// 当月收入
        /// </summary>
        public string Income { get; set; }
        /// <summary>
        /// 养老
        /// </summary>
        public string Retirement { get; set; }
        /// <summary>
        /// 医疗
        /// </summary>
        public string Medical { get; set; }
        /// <summary>
        /// 住房
        /// </summary>
        public string Housing { get; set; }
        /// <summary>
        /// 失业
        /// </summary>
        public string UnEmp { get; set; }
        /// <summary>
        /// 社保小计
        /// </summary>
        public string InsuranceTotal { get; set; }
        /// <summary>
        /// 税前工资
        /// </summary>
        public string SalaryPretax { get; set; }
    
        /// <summary>
        ///  本期应预扣预缴税额 
        /// </summary>
        public string Tax3 { get; set; }
      
        /// <summary>
        ///  税后扣款 
        /// </summary>
        public string TaxedCut { get; set; }

        /// <summary>
        /// 实发工资
        /// </summary>
        public string SalaryPaid { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 所属公司代码
        /// </summary>
        public string BranchCode { get; set; }
        /// <summary>
        /// 导入人
        /// </summary>
        public int Importer { get; set; }
        public DateTime ImportTime { get; set; }


        #region"取消字段"
        /// <summary>
        ///  捐款 
        /// </summary>
        public string SalaryDonation { get; set; }
        /// <summary>
        ///  上年度第13月累计 
        /// </summary>
        public string LastYearAdded { get; set; }
        /// <summary>
        /// 上年度奖金
        /// </summary>
        public string LastYearAward { get; set; }
        /// <summary>
        /// 13薪其他
        /// </summary>
        public string LastYearOthers { get; set; }
        /// <summary>
        /// 笔记本补助
        /// </summary>
        public string NoteBook { get; set; }
        /// <summary>
        /// 话费补贴
        /// </summary>
        public string PhoneAllowance { get; set; }
        /// <summary>
        /// 餐补
        /// </summary>
        public string MealAllowance { get; set; }
        /// <summary>
        /// 子女教育
        /// </summary>
        public string ChildEDU { get; set; }
        /// <summary>
        /// 继续教育
        /// </summary>
        public string ContinuingEDU { get; set; }
        /// <summary>
        ///  住房贷款利息 
        /// </summary>
        public string HomeLoan { get; set; }
        /// <summary>
        ///  住房租金 
        /// </summary>
        public string HomeRent { get; set; }
        /// <summary>
        /// 赡养老人
        /// </summary>
        public string OlderSupport { get; set; }
        /// <summary>
        ///  当月专项附加扣除小计
        /// </summary>
        public string SpecialTotal { get; set; }
        /// <summary>
        /// 累计预扣预缴应纳税所得额
        /// </summary>
        public string Tax1 { get; set; }
        /// <summary>
        /// 累计应预扣预缴税额
        /// </summary>
        public string Tax2 { get; set; }
        /// <summary>
        /// 累计已预缴税额
        /// </summary>
        public string Tax4 { get; set; }
        #endregion


    }

}
