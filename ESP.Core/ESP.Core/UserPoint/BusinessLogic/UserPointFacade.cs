using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.UserPoint.BusinessLogic
{
    /// <summary>
    ///根据员工的日常操作，获取相应的积分
    /// </summary>
  public static  class UserPointFacade
    {
        /// <summary>
        /// 在供应链系统中添加供应商时获得积分
        /// </summary>
        /// <param name="userid">添加供应商的员工ID</param>
        /// <param name="keycode">t_userpointrule表中对应的规则代码，格式类似Supplier_Add</param>
        /// <returns></returns>
        public static int AddSupplier(int userid,string keycode)
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
        /// 
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="supplierid"></param>
        /// <param name="keycode"></param>
        /// <returns></returns>
        public static int ModifySupplier(int userid,int supplierid,string keycode)
        {
            ESP.UserPoint.Entity.UserPointRuleInfo rule = ESP.UserPoint.BusinessLogic.UserPointRuleManager.GetModelByKey(keycode);
            string currenttime = DateTime.Now.ToString("yyyy-MM-dd");
            IList<ESP.UserPoint.Entity.UserPointRecordInfo> records = ESP.UserPoint.BusinessLogic.UserPointRecordManager.GetList(" and userid=" + userid.ToString() + " and ReferenceID="+supplierid.ToString()+" and ruleid=" + rule.RuleID.ToString() + " and (operationtime between '" + currenttime + " 00:00:00' and '" + currenttime + " 23:59:59')");

            if (records == null || records.Count == 0)
            {
                ESP.UserPoint.Entity.UserPointRecordInfo record = new ESP.UserPoint.Entity.UserPointRecordInfo();
                record.UserID = userid;
                record.RuleID = rule.RuleID;
                record.Points = rule.Points;
                record.Memo = rule.Description;
                record.RefrenceID = supplierid;
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
        public static int SearchSupplier(int userid,string keycode)
        {
            ESP.UserPoint.Entity.UserPointRuleInfo rule = ESP.UserPoint.BusinessLogic.UserPointRuleManager.GetModelByKey(keycode);
            string currenttime = DateTime.Now.ToString("yyyy-MM-dd");
            IList<ESP.UserPoint.Entity.UserPointRecordInfo> records = ESP.UserPoint.BusinessLogic.UserPointRecordManager.GetList(" and userid=" + userid.ToString() + " and ruleid=" + rule.RuleID.ToString() + " and (operationtime between '" + currenttime + " 00:00:00' and '" + currenttime + " 23:59:59')");

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
      /// 
      /// </summary>
      /// <param name="userid"></param>
      /// <param name="supplierId"></param>
      /// <param name="keycode"></param>
      /// <returns></returns>
        public static int ShareSupplier(int userid,int supplierId,string keycode)
        {
            ESP.UserPoint.Entity.UserPointRuleInfo rule = ESP.UserPoint.BusinessLogic.UserPointRuleManager.GetModelByKey(keycode);
            string currenttime = DateTime.Now.ToString("yyyy-MM-dd");
            IList<ESP.UserPoint.Entity.UserPointRecordInfo> records = ESP.UserPoint.BusinessLogic.UserPointRecordManager.GetList(" and userid=" + userid.ToString() + " and ReferenceID=" + supplierId.ToString() + " and ruleid=" + rule.RuleID.ToString() + " and (operationtime between '" + currenttime + " 00:00:00' and '" + currenttime + " 23:59:59')");

            if (records == null || records.Count == 0)
            {
                ESP.UserPoint.Entity.UserPointRecordInfo record = new ESP.UserPoint.Entity.UserPointRecordInfo();
                record.UserID = userid;
                record.RuleID = rule.RuleID;
                record.Points = rule.Points;
                record.Memo = rule.Description;
                record.GiftID = 0;
                record.RefrenceID = supplierId;
                record.OperationTime = DateTime.Now;
                return ESP.UserPoint.BusinessLogic.UserPointRecordManager.Add(record);
            }
            else
                return 0;
        }
    }
}
