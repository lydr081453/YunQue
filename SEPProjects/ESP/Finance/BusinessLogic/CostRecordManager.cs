using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Finance.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Globalization;
namespace ESP.Finance.BusinessLogic
{


    public static class CostRecordManager
    {
        private static ESP.Finance.IDataAccess.ICostRecordProvider DataProvider { get { return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.ICostRecordProvider>.Instance; } }
        private const string tablename = "CostRecordInfo";
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public static bool Exists(int RecordID)
        {
            return DataProvider.Exists(RecordID);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(ESP.Finance.Entity.CostRecordInfo model)
        {
            return DataProvider.Add(model);
        }
        public static int Add(ESP.Finance.Entity.CostRecordInfo model, SqlTransaction trans)
        {
            return DataProvider.Add(model, trans);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static void Update(ESP.Finance.Entity.CostRecordInfo model)
        {
            DataProvider.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static void Delete(int RecordID)
        {
            DataProvider.Delete(RecordID);
        }

        public static void DeleteAll(SqlTransaction trans)
        {
            DataProvider.DeleteAll(trans);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static ESP.Finance.Entity.CostRecordInfo GetModel(int RecordID)
        {
            return DataProvider.GetModel(RecordID);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static List<ESP.Finance.Entity.CostRecordInfo> GetList(string strWhere, List<SqlParameter> param)
        {
            return DataProvider.GetList(strWhere, param);
        }

        public static ESP.Finance.Entity.CostRecordInfo ConvertCost(ESP.Finance.Entity.CheckingCost cost)
        {
            ESP.Finance.Entity.CostRecordInfo record = new CostRecordInfo();
            record.PRID = Convert.ToInt32(cost.ID);
            record.PRNO = cost.PRNo;
            record.ProjectCode = cost.ProjectCode;
            record.ProjectID = cost.ProjectID;
            record.PrType = cost.PrType;
            record.Requestor = cost.Requestor;
            record.ReturnType = cost.ReturnType;
            record.SupplierName = cost.SupplierName;
            record.TypeID = cost.TypeID;
            record.TypeName = cost.TypeName;
            record.TypeTotalAmount = Convert.ToDecimal(cost.TypeTotalAmount);
            record.UnPaidAmount = Convert.ToDecimal(cost.UnPaidAmount);
            record.PNTotal = Convert.ToDecimal(cost.PNTotal);
            record.PaidAmount = Convert.ToDecimal(cost.PaidAmount);
            record.GroupName = cost.GroupName;
            record.Description = cost.Description;
            record.DepartmentName = cost.DepartmentName;
            record.DepartmentID = cost.DepartmentID;
            record.CostPreAmount = Convert.ToDecimal(cost.CostPreAmount);
            record.AppDate = cost.AppDate;
            record.AppAmount = Convert.ToDecimal(cost.AppAmount);
            return record;

        }

        public static void WriteLine(string format)
        {
            FileStream fs = new FileStream("f:\\web\\costlog.txt", FileMode.Append);
            StreamWriter streamWriter = new StreamWriter(fs);
            streamWriter.BaseStream.Seek(0, SeekOrigin.End);
            streamWriter.WriteLine(DateTime.Now.ToString(CultureInfo.CurrentCulture) + " " + format);
            streamWriter.Flush();
            streamWriter.Close();
        }


        public static void InsertCost()
        {
            IList<ESP.Finance.Entity.ProjectInfo> projectList = ESP.Finance.BusinessLogic.ProjectManager.GetList(" and (projectCode is not null or projectCode <>'') and status<>34 and enddate>'2010-5-1'");
            List<ESP.Finance.Entity.CostRecordInfo> recordList = new List<CostRecordInfo>();
            foreach (ESP.Finance.Entity.ProjectInfo model in projectList)
            {
                if (model == null)
                    continue;
                try
                {
                    DataProvider.DeleteByProject(model.ProjectId);
                    List<ESP.Finance.Entity.CheckingCost> costList = ESP.Finance.BusinessLogic.CheckingCostManager.CostCollection(model);
                    List<ESP.Finance.Entity.CheckingCost> oopList = ESP.Finance.BusinessLogic.CheckingCostManager.OOPCollection(model, model.GroupID.Value);
                    IList<ESP.Finance.Entity.SupporterInfo> supporterList = ESP.Finance.BusinessLogic.SupporterManager.GetList(" projectid =" + model.ProjectId.ToString());

                    foreach (ESP.Finance.Entity.CheckingCost cost in costList)
                    {
                        if (cost == null)
                            continue;
                        ESP.Finance.Entity.CostRecordInfo record = ESP.Finance.BusinessLogic.CostRecordManager.ConvertCost(cost);
                        record.RecordType = 1;
                        //recordList.Add(record);
                        Add(record);
                    }
                    foreach (ESP.Finance.Entity.CheckingCost cost2 in oopList)
                    {
                        if (cost2 == null)
                            continue;
                        ESP.Finance.Entity.CostRecordInfo record2 = ESP.Finance.BusinessLogic.CostRecordManager.ConvertCost(cost2);
                        record2.RecordType = 2;
                        // recordList.Add(record2);
                        Add(record2);
                    }
                    if (supporterList != null && supporterList.Count != 0)
                    {
                        foreach (ESP.Finance.Entity.SupporterInfo supporter in supporterList)
                        {
                            List<ESP.Finance.Entity.CheckingCost> costsupList = ESP.Finance.BusinessLogic.CheckingCostManager.CostCollectionBySupporter(model, supporter.GroupID.Value);
                            List<ESP.Finance.Entity.CheckingCost> oopsupList = ESP.Finance.BusinessLogic.CheckingCostManager.OOPCollection(model, supporter.GroupID.Value);
                            foreach (ESP.Finance.Entity.CheckingCost cost in costsupList)
                            {
                                if (cost == null)
                                    continue;
                                ESP.Finance.Entity.CostRecordInfo record = ESP.Finance.BusinessLogic.CostRecordManager.ConvertCost(cost);
                                record.RecordType = 1;
                                //recordList.Add(record);
                                Add(record);
                            }
                            foreach (ESP.Finance.Entity.CheckingCost cost2 in oopsupList)
                            {
                                if (cost2 == null)
                                    continue;
                                ESP.Finance.Entity.CostRecordInfo record2 = ESP.Finance.BusinessLogic.CostRecordManager.ConvertCost(cost2);
                                record2.RecordType = 2;
                                //  recordList.Add(record2);
                                Add(record2);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    WriteLine(model.ProjectCode + ex.Message + "#" + ex.StackTrace);
                    continue;
                }
            }

        }

        public static void InsertCost(string projectcode)
        {
            ESP.Finance.Entity.ProjectInfo model = ESP.Finance.BusinessLogic.ProjectManager.GetModelByProjectCode(projectcode);
            List<ESP.Finance.Entity.CostRecordInfo> recordList = new List<CostRecordInfo>();


            DataProvider.DeleteByProject(model.ProjectId);
            List<ESP.Finance.Entity.CheckingCost> costList = ESP.Finance.BusinessLogic.CheckingCostManager.CostCollection(model);
            List<ESP.Finance.Entity.CheckingCost> oopList = ESP.Finance.BusinessLogic.CheckingCostManager.OOPCollection(model, model.GroupID.Value);
            IList<ESP.Finance.Entity.SupporterInfo> supporterList = ESP.Finance.BusinessLogic.SupporterManager.GetList(" projectid =" + model.ProjectId.ToString());

            foreach (ESP.Finance.Entity.CheckingCost cost in costList)
            {
                if (cost == null)
                    continue;
                ESP.Finance.Entity.CostRecordInfo record = ESP.Finance.BusinessLogic.CostRecordManager.ConvertCost(cost);
                record.RecordType = 1;
                //recordList.Add(record);
                Add(record);
            }
            foreach (ESP.Finance.Entity.CheckingCost cost2 in oopList)
            {
                if (cost2 == null)
                    continue;
                ESP.Finance.Entity.CostRecordInfo record2 = ESP.Finance.BusinessLogic.CostRecordManager.ConvertCost(cost2);
                record2.RecordType = 2;
                // recordList.Add(record2);
                Add(record2);
            }
            if (supporterList != null && supporterList.Count != 0)
            {
                foreach (ESP.Finance.Entity.SupporterInfo supporter in supporterList)
                {
                    List<ESP.Finance.Entity.CheckingCost> costsupList = ESP.Finance.BusinessLogic.CheckingCostManager.CostCollectionBySupporter(model, supporter.GroupID.Value);
                    List<ESP.Finance.Entity.CheckingCost> oopsupList = ESP.Finance.BusinessLogic.CheckingCostManager.OOPCollection(model, supporter.GroupID.Value);
                    foreach (ESP.Finance.Entity.CheckingCost cost in costsupList)
                    {
                        if (cost == null)
                            continue;
                        ESP.Finance.Entity.CostRecordInfo record = ESP.Finance.BusinessLogic.CostRecordManager.ConvertCost(cost);
                        record.RecordType = 1;
                        //recordList.Add(record);
                        Add(record);
                    }
                    foreach (ESP.Finance.Entity.CheckingCost cost2 in oopsupList)
                    {
                        if (cost2 == null)
                            continue;
                        ESP.Finance.Entity.CostRecordInfo record2 = ESP.Finance.BusinessLogic.CostRecordManager.ConvertCost(cost2);
                        record2.RecordType = 2;
                        //  recordList.Add(record2);
                        Add(record2);
                    }
                }
            }
        }

    }
}
