using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Purchase.BusinessLogic
{
    public static class UserPointManager
    {
        /// <summary>
        /// 在供应链系统中添加供应商时获得积分
        /// </summary>
        /// <param name="userid">添加供应商的员工ID</param>
        /// <param name="keycode">t_userpointrule表中对应的规则代码，格式类似Supplier_Add</param>
        /// <returns></returns>
        public static int AddSupplier(int userid, string keycode)
        {
            ESP.UserPoint.Entity.UserPointRuleInfo rule = ESP.UserPoint.BusinessLogic.UserPointRuleManager.GetModelByKey(keycode);
            ESP.UserPoint.Entity.UserPointRecordInfo record = new ESP.UserPoint.Entity.UserPointRecordInfo();
            record.UserID = userid;
            record.RuleID = rule.RuleID;
            record.Points = rule.Points;
            record.Memo = rule.Description;
            record.GiftID = 0;
            record.OperationTime = DateTime.Now;
            return ESP.UserPoint.BusinessLogic.UserPointRecordManager.Add(record);
        }

        /// <summary>
        /// 在供应链系统中修改供应商时获得积分
        /// </summary>
        /// <param name="userid">修改供应商的员工ID</param>
        /// <param name="keycode">t_userpointrule表中对应的规则代码，格式类似Supplier_Modify</param>
        /// <returns></returns>
        public static int ModifySupplier(int userid, string keycode)
        {
            ESP.UserPoint.Entity.UserPointRuleInfo rule = ESP.UserPoint.BusinessLogic.UserPointRuleManager.GetModelByKey(keycode);
            string currenttime = DateTime.Now.ToString("yyyy-MM-dd");
            IList<ESP.UserPoint.Entity.UserPointRecordInfo> records = ESP.UserPoint.BusinessLogic.UserPointRecordManager.GetList(" and userid=" + userid.ToString() + " and ruleid=" + rule.ToString() + " and (operationtime between '" + currenttime + " 00:00:00' and '" + currenttime + " 23:59:59')");

            if (records == null || records.Count == 0)
            {
                ESP.UserPoint.Entity.UserPointRecordInfo record = new ESP.UserPoint.Entity.UserPointRecordInfo();
                record.UserID = userid;
                record.RuleID = rule.RuleID;
                record.Points = rule.Points;
                record.Memo = rule.Description;
                record.GiftID = 0;
                record.OperationTime = DateTime.Now;
                return ESP.UserPoint.BusinessLogic.UserPointRecordManager.Add(record);
            }
            else
                return 0;
        }

        /// <summary>
        /// 在供应链系统中查询供应商时获得积分
        /// </summary>
        /// <param name="userid">查询供应商的员工ID</param>
        /// <param name="keycode">t_userpointrule表中对应的规则代码，格式类似Supplier_Search</param>
        /// <returns></returns>
        public static int SearchSupplier(int userid, string keycode)
        {
            ESP.UserPoint.Entity.UserPointRuleInfo rule = ESP.UserPoint.BusinessLogic.UserPointRuleManager.GetModelByKey(keycode);
            string currenttime = DateTime.Now.ToString("yyyy-MM-dd");
            IList<ESP.UserPoint.Entity.UserPointRecordInfo> records = ESP.UserPoint.BusinessLogic.UserPointRecordManager.GetList(" and userid=" + userid.ToString() + " and ruleid=" + rule.ToString() + " and (operationtime between '" + currenttime + " 00:00:00' and '" + currenttime + " 23:59:59')");

            if (records == null || records.Count == 0)
            {
                ESP.UserPoint.Entity.UserPointRecordInfo record = new ESP.UserPoint.Entity.UserPointRecordInfo();
                record.UserID = userid;
                record.RuleID = rule.RuleID;
                record.Points = rule.Points;
                record.Memo = rule.Description;
                record.GiftID = 0;
                record.OperationTime = DateTime.Now;
                return ESP.UserPoint.BusinessLogic.UserPointRecordManager.Add(record);
            }
            else
                return 0;
        }

        /// <summary>
        /// 在供应链系统中分享供应商时获得积分
        /// </summary>
        /// <param name="userid">分享供应商的员工ID</param>
        /// <param name="keycode">t_userpointrule表中对应的规则代码，格式类似Supplier_Share</param>
        /// <returns></returns>
        public static int ShareSupplier(int userid, string keycode)
        {
            ESP.UserPoint.Entity.UserPointRuleInfo rule = ESP.UserPoint.BusinessLogic.UserPointRuleManager.GetModelByKey(keycode);
            string currenttime = DateTime.Now.ToString("yyyy-MM-dd");
            IList<ESP.UserPoint.Entity.UserPointRecordInfo> records = ESP.UserPoint.BusinessLogic.UserPointRecordManager.GetList(" and userid=" + userid.ToString() + " and ruleid=" + rule.ToString() + " and (operationtime between '" + currenttime + " 00:00:00' and '" + currenttime + " 23:59:59')");

            if (records == null || records.Count == 0)
            {
                ESP.UserPoint.Entity.UserPointRecordInfo record = new ESP.UserPoint.Entity.UserPointRecordInfo();
                record.UserID = userid;
                record.RuleID = rule.RuleID;
                record.Points = rule.Points;
                record.Memo = rule.Description;
                record.GiftID = 0;
                record.OperationTime = DateTime.Now;
                return ESP.UserPoint.BusinessLogic.UserPointRecordManager.Add(record);
            }
            else
                return 0;
        }
    }
}
