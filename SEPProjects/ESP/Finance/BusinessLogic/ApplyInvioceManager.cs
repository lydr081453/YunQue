using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
namespace ESP.Finance.BusinessLogic
{
    /// <summary>
    /// 业务逻辑类ApplyForInvioceInfo 的摘要说明。
    /// </summary>
     
     
    public static class ApplyForInvioceManager
    {
        //private readonly ESP.Finance.DataAccess.ApplyForInvioceDAL dal = new ESP.Finance.DataAccess.ApplyForInvioceDAL();

        private static ESP.Finance.IDataAccess.IApplyForInvioceDataProvider DataProvider{get{return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.IApplyForInvioceDataProvider>.Instance;}}
        //private const string _dalProviderName = "ApplyForInvioceDALProvider";

        


        #region  成员方法


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(ESP.Finance.Entity.ApplyForInvioceInfo model)
        {
            return DataProvider.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static UpdateResult Update(ESP.Finance.Entity.ApplyForInvioceInfo model)
        {
            int res = 0;
            try
            {
                res = DataProvider.Update(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return UpdateResult.Failed;
            }
            if (res > 0)
            {
                return UpdateResult.Succeed;
            }
            else if (res == 0)
            {
                return UpdateResult.UnExecute;
            }
            return UpdateResult.Failed;
        }

        public static int UpdateStatus(string Ids, ESP.Finance.Utility.ApplyForInvioceStatus.Status status)
        {
            return DataProvider.UpdateStatus(Ids, status);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static DeleteResult Delete(int id)
        {
            int res = 0;
            try
            {
                res = DataProvider.Delete(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return DeleteResult.Failed;
            }
            if (res > 0)
            {
                return DeleteResult.Succeed;
            }
            else if (res == 0)
            {
                return DeleteResult.UnExecute;
            }
            return DeleteResult.Failed;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static ESP.Finance.Entity.ApplyForInvioceInfo GetModel(int id)
        {

            return DataProvider.GetModel(id);
        }

        #region 获得数据列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ApplyForInvioceInfo> GetAllList()
        {
            return GetList(null);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ApplyForInvioceInfo> GetList(string term)
        {
            return GetList(term, null);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ApplyForInvioceInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            return DataProvider.GetList(term, param);
        }
        #endregion 获得数据列表
        #endregion  成员方法


        public static string GetDetailHtml(int id)
        {
            ApplyForInvioceInfo info = ApplyForInvioceManager.GetModel(id);

            //txtInviocePrice.Text = info.InviocePrice.ToString("#,##0.00");
            //txtRemark.Text = info.Remark;
            //ddlFlowTo.Text = ESP.Finance.Utility.Common.ApplyForInvioceFlowTo_Names[(int)info.FlowTo];
            //ddlInvoiceType.Text = ESP.Finance.Utility.Common.InvoiceType_Names[(int)info.InvoiceType];
            //txtInvoiceTitle.Text = info.InvoiceTitle;
            //txtBankName.Text = info.BankName;
            //txtBankNum.Text = info.BankNum;
            //txtTIN.Text = info.TIN;
            //txtAddressPhone.Text = info.AddressPhone;
            //ddlMedia.Text = info.MediaName;

            string html = "<table width=100%>";
            html += "<tr><td width=20%>流向：</td><td>" + ESP.Finance.Utility.Common.ApplyForInvioceFlowTo_Names[(int)info.FlowTo] + "</td></tr>";
            html += "<tr><td>发票类型：</td><td>" + ESP.Finance.Utility.Common.ApplyForInvioceFlowTo_Names[(int)info.FlowTo] + "</td></tr>";
            html += "<tr><td>开票单位名称：</td><td>" + ESP.Finance.Utility.Common.ApplyForInvioceFlowTo_Names[(int)info.FlowTo] + "</td></tr>";
            html += "<tr><td>开户银行：</td><td>" + ESP.Finance.Utility.Common.ApplyForInvioceFlowTo_Names[(int)info.FlowTo] + "</td></tr>";
            html += "<tr><td>账号：</td><td>" + ESP.Finance.Utility.Common.ApplyForInvioceFlowTo_Names[(int)info.FlowTo] + "</td></tr>";
            html += "<tr><td>纳税人识别号：</td><td>" + ESP.Finance.Utility.Common.ApplyForInvioceFlowTo_Names[(int)info.FlowTo] + "</td></tr>";
            html += "<tr><td>开户地址及电话：</td><td>" + ESP.Finance.Utility.Common.ApplyForInvioceFlowTo_Names[(int)info.FlowTo] + "</td></tr>";
            html += "<tr><td>发票金额：</td><td>" + ESP.Finance.Utility.Common.ApplyForInvioceFlowTo_Names[(int)info.FlowTo] + "</td></tr>";
            html += "<tr><td>描述：</td><td>" + ESP.Finance.Utility.Common.ApplyForInvioceFlowTo_Names[(int)info.FlowTo] + "</td></tr>";
            html += "</table>";

            return html;
        }
    }
}

