using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using Media.Service;
using ESP.Media.BusinessLogic;

using ESP.Media.Entity;
using ESP.Media.Access.Utilities;
using ESP.Media.BusinessLogic;

namespace Media.Service
{

    /// <summary>
    ///PRService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    //若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 
    // [System.Web.Script.Services.ScriptService]
    public class PRWrittingService : BaseService
    {

        public PRWrittingService()
        {

            //如果使用设计的组件，请取消注释以下行 
            //InitializeComponent(); 
        }

        /// <summary>
        /// 获取稿件费用的列表
        /// </summary>
        /// <param name="projectid">项目号</param>
        /// <param name="billid">稿件费用报销单号</param>
        /// <returns>返回稿件费用报销单List对象</returns>
        [WebMethod]
        [SoapHeader("Credentials")]
        public List<WritingfeebillInfo> GetBillList(string projectid, string billid)
        {
            string term = " and projectid=@projectid and Status=@status";
            Hashtable ht = new Hashtable();
            ht.Add("@projectid",projectid);
            ht.Add("@status",Global.BillStatus.Submit);
            return WritingfeebillManager.GetObjectList(term, ht);


        }
        /// <summary>
        /// 获取稿件费用的实例
        /// </summary>
        /// <param name="billid">稿件费用Bill ID</param>
        /// <returns>返回稿件费用实例</returns>
        [WebMethod]
        [SoapHeader("Credentials")]
        public WritingfeebillInfo GetBillModel(int billid)
        {
            return WritingfeebillManager.GetModel(billid);
        }

        /// <summary>
        /// 获取稿件费用明细的实例
        /// </summary>
        /// <param name="itemid">稿件费用明细ID</param>
        /// <returns>返回稿件费用明细实例</returns>
        [WebMethod]
        [SoapHeader("Credentials")]
        public WritingfeeitemInfo GetItemModel(int itemid)
        {
            return WritingfeeitemManager.GetModel(itemid);
        }

        /// <summary>
        /// 获取稿件费用明细列表
        /// </summary>
        /// <param name="billid">稿件费用ID</param>
        /// <returns>返回稿件费用明细列表</returns>
        [WebMethod]
        [SoapHeader("Credentials")]
        public List<WritingfeeitemInfo> GetItemList(int billid)
        {
            return WritingfeeitemManager.GetObjectListByBillID(billid, null, null);
        }

        /// <summary>
        /// 更新稿件费用单据状态（已保存，已提交，已申请）
        /// </summary>
        /// <param name="billid">稿件费用单据ID</param>
        /// <param name="status">状态</param>
        /// <param name="userid">操作人</param>
        /// <returns>返回是否成功</returns>
        [WebMethod]
        [SoapHeader("Credentials")]
        public bool UpdateBillStatus(int billid, Global.BillStatus status, int userid)
        {
            WritingfeebillInfo bill = WritingfeebillManager.GetModel(billid);
            bill.Status = (int)status;
            return WritingfeebillManager.modify(bill, userid);
        }



        /// <summary>
        /// 新增稿件费用明细
        /// </summary>
        /// <param name="item">稿件费用明细实例</param>
        /// <param name="brief">简报实例</param>
        /// <param name="filename">文件路径</param>
        /// <param name="filedata">二进制文件对象</param>
        /// <param name="userid">操作人</param>
        /// <param name="errmsg">返回字符串说明</param>
        /// <returns></returns>
        [WebMethod]
        [SoapHeader("Credentials")]
        public int InsertItem(WritingfeeitemInfo item, ProjectbriefInfo brief, int userid,ref int billid)
        {
            return WritingfeeitemManager.add(item, brief, userid,ref billid);
        }

        /// <summary>
        /// 更新稿件费用明细
        /// </summary>
        /// <param name="item">稿件费用明细实例</param>
        /// <param name="brief">上传简报实例</param>
        /// <param name="userid">操作人</param>
        /// <returns>成功返回True，or False</returns>
        [WebMethod]
        [SoapHeader("Credentials")]
        public bool UpdateItem(WritingfeeitemInfo item, ProjectbriefInfo brief, int userid)
        {
            string errmsg = string.Empty;
            return WritingfeeitemManager.modify(item, brief, userid, out errmsg);
        }

        /// <summary>
        /// 删除稿件费用明细
        /// </summary>
        /// <param name="itemid">稿件费用明细ID</param>
        /// <param name="userid">操作人</param>
        /// <returns>成功返回True，Or False</returns>
        [WebMethod]
        [SoapHeader("Credentials")]
        public bool DeleteItem(int itemid, int userid)
        {
            return WritingfeeitemManager.del(itemid, userid);
        }

        [WebMethod]
        [SoapHeader("Credentials")]
        public string test()
        {
            return "hello world";
        }

    }

}