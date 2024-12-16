using System.Xml.Serialization;

namespace ESP.Supplier.Common
{
    /// <summary>
    /// Common 的摘要说明
    /// </summary>
    public class Common
    {
        /// <summary>
        /// Formats the phone no.
        /// </summary>
        /// <param name="phoneNo">The phone no.</param>
        /// <param name="len">The len.</param>
        /// <returns></returns>
        public static string FormatPhoneNo(string phoneNo,int len)
        {
            string newPhoneNo = "";
            if (!string.IsNullOrEmpty(phoneNo) && phoneNo.Length > 0)
            {
                string[] phones = phoneNo.Split('-');
                if (phones.Length == len)
                {
                    if (!string.IsNullOrEmpty(phones[0]))
                    {
                        newPhoneNo += phones[0];
                    }
                    if (!string.IsNullOrEmpty(phones[1]))
                    {
                        newPhoneNo += "-" + phones[1];
                    }
                    if (!string.IsNullOrEmpty(phones[2]))
                    {
                        newPhoneNo += "-" + phones[2];
                    }
                    if (!string.IsNullOrEmpty(phones[3]))
                    {
                        newPhoneNo += "-" + phones[3];
                    }
                }
            }
            return newPhoneNo;
        }

        /// <summary>
        /// 获得字符串中的数字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetNumbers(string str)
        {
            string returnNumbers = "";
            int value = 0;
            foreach (char c in str)
            {
                if (int.TryParse(c+"",out value))
                    returnNumbers += c;
            }
            return returnNumbers;
        }

        /// <summary>
        /// 活动场地类型
        /// </summary>
        public static readonly string[] FieldTypes = { "商务酒店","会议中心", "餐厅、酒吧", "剧院、演播厅", "路演场地", "试乘试驾", "体育场馆", "艺术园区、公园", "其它" };
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

    /// <summary>
    ///Common 的摘要说明
    /// </summary>
    public class RequestName
    {
        public const string ProjectID = "ProjectID";//项目ID
        public const string ProjectCode = "ProjectCode";//项目Code
        public const string MediaID = "Mid";//媒体ID
        public const string MediaType = "MediaType";//媒体类型
        public const string MediaName = "MediaName";//媒体名称
        public const string ReporterID = "Rid";//记者ID
        public const string ClientID = "ClientID";//客户ID
        public const string ProductLineID = "ProductLineID";//产品线ID
        public const string Alert = "alert";
        public const string ProductType = "ProductType";
        public const string Operate = "Operate";//操作

        public const string BackUrl = "backurl";//返回路径


        //单据
        public const string BillID = "BillID";
        public const string BillType = "BillType";
        public const string PersonFeeBillID = "PersonFeeBillID";//个人费用报销单
        public const string PersonFeeItemID = "PersonFeeItemID";//个人费用报销项

        public const string CashPaymentBillID = "CashPaymentBillID";//现金付款报销单
        public const string CashPaymentItemID = "CashPaymentItemID";//现金付款报销项

        public const string CashloanBillID = "CashloanBillID";//个人费用报销单
        public const string CashloanItemID = "CashloanItemID";//个人费用报销项

        public const string CheckPaymentBillID = "CheckPaymentBillID";//支票/电汇报销单
        public const string CheckPaymentItemID = "CheckPaymentItemID";//支票/电汇报销项

        public const string WritingFeeBillID = "WritingFeeBillID";//稿件费用报销单
        public const string WritingFeeItemID = "WritingFeeItemID";//稿件费用报销项

        public const string meetingID = "meetingid";
        public const string FaceID = "facetofaceid";
        public const string GeneralID = "GeneralID";
        public const string MediaPREntry = "MediaPREntry"; //媒介PR申请入口

        public const string MediaPRID = "MediaPRID";
    }

    public class BillName
    {
        public const string writtingFeeName = "稿件费用报销单";
        public const string personFeeName = "个人报销单";
        public const string paymentFeeName = "现金付款报销单";
        public const string cashLoanName = "借款单";
        public const string channelName = "媒体专栏合作";
    }
    public enum PRTYpe : int
    {
        /// <summary>
        /// 采购系统PR申请
        /// </summary>
        CommonPR = 0,
        /// <summary>
        /// 媒介系统入口PR申请
        /// </summary>
        MediaPR = 1,
        /// <summary>
        /// 专栏合作PR申请
        /// </summary>
        ChanelPR = 2,
        /// <summary>
        /// ADPR申请
        /// </summary>
        ADPR = 3,
        /// <summary>
        /// 媒介稿费报销PR单由FA再次编辑生成大于3000的新PR单
        /// </summary>
        PR_MediaFA = 4,
        /// <summary>
        /// 未使用
        /// </summary>
        OtherPR = 5,
        /// <summary>
        /// 对私PR单
        /// </summary>
        PrivatePR = 6,
        /// <summary>
        /// 媒体合作PR单
        /// </summary>
        MPPR=7,
        /// <summary>
        /// 对私PR单由FA再次编辑生成大于3000的新PR单
        /// </summary>
        PR_PriFA=8,
        /// <summary>
        /// 临时状态,物料审核人李彦娥,无采购总监审批,媒介总监审批,不经过分公司
        /// </summary>
        PR_TMP1=9,
        /// <summary>
        /// 临时状态,物料审核人李盈,有采购总监审批,不经过分公司
        /// </summary>
        PR_TMP2=10,
        /// <summary>
        /// 押金（只应用于PN）
        /// </summary>
        PN_ForeGift=11,
        /// <summary>
        /// 抵消押金（只应用于PN）
        /// </summary>
        PN_KillForeGift=12,
        /// <summary>
        /// 小于10%的现金重新审批的类型
        /// </summary>
        PN_Cash10Down = 13,
        /// <summary>
        /// 抵消现金
        /// </summary>
        PN_KillCash = 14,
        /// <summary>
        /// 财务系统生成的车马费
        /// </summary>
        PROJECT_Media = 20,
        /// <summary>
        /// 报销(只应用于PN)
        /// </summary>
        PN_ExpenseAccount = 30,
        /// <summary>
        /// 现场购买(只应用于PN)
        /// </summary>
        PN_ExpenseAccountBuyLocale = 31,
        /// <summary>
        /// 现金借款(只应用于PN)
        /// </summary>
        PN_ExpenseAccountCashBorrow = 32

    }

    public enum BillType : int
    {
        /// <summary>
        /// 签到表
        /// </summary>
        SignExcel = 1,
        /// <summary>
        /// 通联表
        /// </summary>
        CommunicateExcel = 2,
        /// <summary>
        ///采购合同签署记录
        /// </summary>
        PurchasecontractsignedrecBill = 3,
        /// <summary>
        /// 借款单模版
        /// </summary>
        CashloanBill = 4,
        /// <summary>
        /// 个人费用报销单
        /// </summary>
        PersonFeeBill = 5,
        /// <summary>
        /// 现金付款报销单
        /// </summary>
        CashPaymentBill = 6,
        /// <summary>
        /// 支票/电汇付款申请单
        /// </summary>
        CheckPaymentBill = 7,
        /// <summary>
        /// 稿费报销明细单模板
        /// </summary>
        WritingFeeBill = 8
    }

    /// <summary>
    /// 媒介申请类型 (对公|对私)
    /// </summary>
    public enum MediaOperateType
    {
        /// <summary>
        /// 对私业务
        /// </summary>
        MediaPrivate = 0,
        /// <summary>
        /// 对公业务
        /// </summary>
        MediaPublic = 1,
        /// <summary>
        /// 从媒介过来的新建bill
        /// </summary>
        MediaBill = 2
    }


    public class XmlCommon
    {

        public static byte[] Serialize(System.Type type, object obj)
        {
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                XmlSerializer xs = new XmlSerializer(type);
                xs.Serialize(ms, obj);
                byte[] buf = ms.ToArray();
                ms.Close();
                return buf;
            }
        }


        public static object Deserialize(System.Type type, byte[] data)
        {
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream(data))
            {
                XmlSerializer xs = new XmlSerializer(type);
                object obj = xs.Deserialize(ms);
                ms.Close();
                return obj;
            }
        }
    }

    public class ServiceURL
    {
        private static string prpath = ESP.Configuration.ConfigurationManager.SafeAppSettings["ServicePath"].ToString();
        public static string BillPath = ESP.Configuration.ConfigurationManager.SafeAppSettings["BillPath"].ToString();
        public static string ProjectBriefPath = ESP.Configuration.ConfigurationManager.SafeAppSettings["ProjectBriefPath"].ToString();
        public static string ProjectServicePath = prpath + "ProjectService.asmx";
        public static string DailyServicePath = prpath + "DailyService.asmx";
        public static string EventServicePath = prpath + "EventService.asmx";
        public static string MediaServicePath = prpath + "MediaService.asmx";

        public static string PRCashPaymentServicePath = prpath + "PRCashPaymentService.asmx";
        public static string PRCashLoanServicePath = prpath + "PRCashLoanService.asmx";
        public static string PRPersonFeeServicePath = prpath + "PRPersonFeeService.asmx";
        public static string PRWrittingServicePath = prpath + "PRWrittingService.asmx";
        public static string ReporterServicePath = prpath + "ReporterService.asmx";
    }
}