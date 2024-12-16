using System.Configuration;

namespace ESP.Supplier.Common
{
    /// <summary>
    /// State 的摘要说明
    /// </summary>
    public static class State
    {

        #region 供应商状态
        //供应商状态
        public static int SupplierStatus_del = 200;//逻辑删除
        public static int SupplierStatus_save = 0; //供应商注册已保存未提交
        public static int SupplierStatus_commit = 1; //供应商注册已提交
        public static int SupplierStatus_checkregist = 2; //供应商审核通过
        public static int SupplierStatus_questionSubmit = 3;//问卷调查已提交
        public static int SupplierStatus_questionChecked = 4;//问卷调查已批阅
        public static int SupplierStatus_actived = 5;//账户已激活
        public static int SupplierStatus_overrule = 300;//注册驳回

        public static string[] SupplierStatus_show = { "已保存", "已注册", "已审核通过", "问卷调查已提交", "问卷调查已批阅", "账户已激活", "已删除" };
        #endregion
    }

    public enum LogTyp : int
    {
        /// <summary>
        /// 注册审核成功
        /// </summary>
        regOk = 1,
        /// <summary>
        /// 注册审核失败
        /// </summary>
        regFail = 2,
        /// <summary>
        /// 问卷调查审核成功
        /// </summary>
        quesOk = 3,
        /// <summary>
        /// 问卷调查审核失败
        /// </summary>
        quesFail = 4
    }
    public enum FileType : int
    {
        /// <summary>
        /// 公司简介
        /// </summary>
        intro = 0,
        /// <summary>
        /// 产品/服务
        /// </summary>
        product = 1,
        /// <summary>
        /// 产品报价
        /// </summary>
        productPrice = 2,
        /// <summary>
        /// 学历或资质证书
        /// </summary>
        certifications = 3,
        /// <summary>
        /// 成功案例1
        /// </summary>
        case1 = 4,
        /// <summary>
        /// 成功案例2
        /// </summary>
        case2 = 5
    }

    public static class Const
    {
        #region 规模
        #endregion

        #region 行业
        #endregion

        #region 所在地区
        #endregion

        #region 注册资本
        #endregion
    }
}