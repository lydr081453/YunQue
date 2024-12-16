using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;

namespace ESP.Media.Access.Utilities
{
    public class Global
    {
        public struct ReporterComparePrice
        {
           public double AmountPrice;
           public  double ProjectAvgPrice;
           public  double MediaAvgprice;
        }

        /// <summary>
        /// ʱ���ֵ
        /// </summary>
        public static string DateTimeNullValue = "1900-1-1 0:00:00";

        #region ý������
        /// <summary>
        /// ƽ��ý��
        /// </summary>
        public static int MediaItemType_Plane = 1;
        /// <summary>
        /// ����ý��
        /// </summary>
        public static int MediaItemType_Web = 2;
        /// <summary>
        /// ����ý��
        /// </summary>
        public static int MediaItemType_Tv = 3;
        /// <summary>
        /// �㲥ý��
        /// </summary>
        public static int MediaItemType_Dab = 4;

        /// <summary>
        /// ý����������
        /// </summary>
        public static string[] MediaItemTypeName = { "��", "ƽ��ý��", "����ý��", "����ý��", "�㲥ý��" };
        #endregion

        #region ��������
        /// <summary>
        /// ����
        /// </summary>
        public static int RegionAttribute_Centrality = 1;
        /// <summary>
        /// �ط�
        /// </summary>
        public static int RegionAttribute_District = 2;
        /// <summary>
        /// ����
        /// </summary>
        public static int RegionAttribute_International = 3;
        /// <summary>
        /// ������������
        /// </summary>
        public static string[] RegionAttributeName = {"","����","�ط�","����" };

        /// <summary>
        /// Ĭ��ʡ����������----�й�
        /// </summary>
        public static int DefaultCountry = 2;
        #endregion

        #region ��ť�ı�
        //public static string[] ButtonText = {"���","����" };
        #endregion

        #region ���ʽ
        public static int None = 0;
        /// <summary>
        /// �ֽ�
        /// </summary>
        public static int PaymentMode_Money = 1;
        /// <summary>
        /// ת��
        /// </summary>
        public static int PaymentMode_Virement = 2;
        /// <summary>
        /// ֧Ʊ
        /// </summary>
        public static int PaymentMode_Cheque = 3;
        /// <summary>
        /// ����
        /// </summary>
        public static int PaymentMode_Other = 4;
        /// <summary>
        /// ���ʽ����
        /// </summary>
        public static string[] PaymentModeName = { "","�ֽ�", "ת��", "֧Ʊ", "����" };
        #endregion

        /// <summary>
        /// ý���������� (�Թ�|��˽)
        /// </summary>
        public enum MediaOperateType
        {
            /// <summary>
            /// ��˽ҵ��
            /// </summary>
            MediaPrivate = 0,
            /// <summary>
            /// �Թ�ҵ��
            /// </summary>
            MediaPublic = 1
        }

        public enum ProjectTabs : int 
        { 
            Index = 0,
            MediaAndReporter = 1,
            Daily = 2,
            Event = 3,
            PayMent = 4,
            Members = 5
        }
        public enum PayType:int
        { 
            /// <summary>
            /// ����
            /// </summary>
            Later = 0,
            /// <summary>
            /// ��ǰ
            /// </summary>
            Before = 1
        }

        public static string[] BillStatusString = new string[] { "����","�ύ","PR����","PR�ύ"};
        public enum BillStatus:int
        { 
            /// <summary>
            /// ����
            /// </summary>
            Save = 0,
            /// <summary>
            /// �ύ
            /// </summary>
            Submit = 1,

            PRSave = 2,

            PRSubmit = 3
        }

        public static string[] PayTypeName = { "����֧��", "��ǰ֧��"};
        public enum MediaAuditStatus : int 
        {
            /// <summary>
            /// ����
            /// </summary>
            Save = 0,
            /// <summary>
            /// �ύ
            /// </summary>
            Submit = 1,
            /// <summary>
            /// һ������ͨ��
            /// </summary>
            FirstLevelAudit = 2
        }

        public static string[] PropagatetypeName = { "δ֪", "�ճ�����", "�¼�����" };
        public enum Propagatetype : int
        { 
            /// <summary>
            /// �ճ�
            /// </summary>
            Daily = 1,
            /// <summary>
            /// �¼�
            /// </summary>
            Event = 2
        }
        public enum ReporterPayStatus
        { 
            /// <summary>
            /// δ֧��
            /// </summary>
            NotPay =0,
            /// <summary>
            /// �Ѿ�����˽����Ϣ
            /// </summary>
            SetPrivateMsg = 1,
            /// <summary>
            /// ��֧��
            /// </summary>
            Pay = 2
        }

        public enum Tables : int
        {
            /// <summary>
            /// ý�� 
            /// </summary>
            Media = 1,  
            /// <summary>
            /// ���� 
            /// </summary>                                                                        
            reporters = 2,
            /// <summary>
            /// �ͻ� 
            /// </summary>                                                                                    
            clients = 3,	                                                                                                  
            /// <summary>
            /// ���ű༭ 
            /// </summary> 
            ShortMsg = 4,	
            /// <summary>
            /// ���ŷ��� 
            /// </summary> 
            SendShortMsg = 5,	
            /// <summary>
            /// Email�༭
            /// </summary> 
            EMail = 6,
            /// <summary>
            /// Email����
            /// </summary>
            SendEmail = 7,	
            /// <summary>
            /// ����
            /// </summary> 
            IssueMsg = 8,	
            /// <summary>
            /// �ظ�
            /// </summary> 
            ReplyMsg = 9,
            /// <summary>
            /// ��Ʒ��
            /// </summary>
	        ProductLine = 10
        }

        public enum SysOperateType : int
        { 
            Add = 1,
            Edit = 2,
            Del = 3
        }



        public enum FiledStatus : int
        { 
            /// <summary>
            /// ����
            /// </summary>
            Usable = 0,
            /// <summary>
            /// �Ѿ�ɾ��
            /// </summary>
            Del = 1
        }

        /// <summary>
        /// ��������
        /// </summary>
        public enum PostType
        {
            /// <summary>
            /// ����
            /// </summary>
            Issue = 0,
            /// <summary>
            /// �ظ�
            /// </summary>
            Reply = 1
        }

        public enum IsSystem
        {
            /// <summary>
            /// ����
            /// </summary>
            Blog = 0,
            /// <summary>
            /// ����
            /// </summary>
            SysMsg = 1,
            /// <summary>
            /// ����
            /// </summary>
            Post=2
        }

        public enum UserStatus
        {
            /// <summary>
            /// ������
            /// </summary>
            Overline = 0,
            /// <summary>
            /// ����
            /// </summary>
            Online = 1
        }


        /// <summary>
        /// ������Ϣ��������
        /// </summary>
        public enum OperateType
        {
            /// <summary>
            /// ��ע��
            /// </summary>
            Add = 0,
            /// <summary>
            /// �޸ĸ��˵���
            /// </summary>
            Edit = 1
        }

        public enum Sex
        {
            Men = 1,
            Women = 2
        }

        public enum UserType
        {
            /// <summary>
            /// ��ͨ�û�
            /// </summary>
            Common = 0,
            /// <summary>
            /// ����Ա
            /// </summary>
            Admin = 1,
        }


        public enum BillType:int
        {
            /// <summary>
            /// ǩ����
            /// </summary>
            SignExcel=1,
            /// <summary>
            /// ͨ����
            /// </summary>
            CommunicateExcel=2,
            /// <summary>
            ///�ɹ���ͬǩ���¼
            /// </summary>
            PurchasecontractsignedrecBill = 3,
            /// <summary>
            /// ��ģ��
            /// </summary>
            CashloanBill = 4,
            /// <summary>
            /// ���˷��ñ�����
            /// </summary>
            PersonFeeBill  = 5,
            /// <summary>
            /// �ֽ𸶿����
            /// </summary>
            CashPaymentBill = 6,
            /// <summary>
            /// ֧Ʊ/��㸶�����뵥
            /// </summary>
            CheckPaymentBill = 7,
            /// <summary>
            /// ��ѱ�����ϸ��ģ��
            /// </summary>
            WritingFeeBill = 8
        }


        public enum CanUpload
        {
            CanNot = 0,
            Can = 1
        }
        public enum CanDownload
        {
            CanNot = 0,
            Can = 1
        }
        public static string[] SEX =  { "����", "��", "Ů" };

        public const string pagePostFix = ".aspx";

        public static class DataValueField
        { 
            public static string Country
            {
                get { return "CountryID"; }
            }

            public static string Province
            {
                get { return "Province_ID"; }
            }
            public static string City
            {
                get { return "City_ID"; }
            }

            public static string LanMu
            {
                get { return "typeid"; }
            }

            public static string CoverRage
            {
                get { return "coverid"; }
            }

            public static string Industry
            {
                get { return "IndustryID"; }
            }

            public static string MediaType
            {
                get { return "id"; }
            }
        }

        public static class DataTextField
        {
            public static string Country
            {
                get { return "CountryName"; }
            }

            public static string Province
            {
                get { return "Province_Name"; }
            }
            public static string City
            {
                get { return "City_Name"; }
            }
            public static string LanMu
            {
                get { return "typename"; }
            }

            public static string CoverRage
            {
                get { return "covername"; }
            }

            public static string Industry
            {
                get { return "IndustryName"; }
            }

            public static string MediaType
            {
                get { return "name"; }
            }
        }


        public static class LanMuType
        { 
            public static string[] TypeNames ={"��","��������","��̸�Ի�","Ӱ������","��������","�����Ļ�","ũҵ","��ʳ","����","Ϸ��","����","����","�ƽ�","����","����","����","����","����","����","�ٶ�"};
            public static DataTable GetAllList()
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("typeid", typeof(int));
                dt.Columns.Add("typename", typeof(string));

                for (int i = 0; i < TypeNames.Length; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["typeid"] = i;
                    dr["typename"] = TypeNames[i];
                    dt.Rows.Add(dr);
                }
                return dt;
            }
        }

        public enum CoverRageItem:int
        { 
            NULL = 0,
            COUNTRY = 1,
            PROVINCE = 2,
            CITY = 3
        }

        public static int ChinaID = 1;
        public static class CoverRage
        {
            public static string[] CoverRages ={ "��", "ȫ��", "ʡ", "��" };
            public static DataTable GetAllList()
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("coverid", typeof(int));
                dt.Columns.Add("covername", typeof(string));

                for (int i = 0; i < CoverRages.Length; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["coverid"] = i;
                    dr["covername"] = CoverRages[i];
                    dt.Rows.Add(dr);
                }
                return dt;
            }
        }


        public static class SessionKey
        {
            /// <summary>
            /// ��ǰ�û�
            /// </summary>
            public static string CurrentUser
            {
                get
                {
                    return "CurrentUser";
                }
            }

            /// <summary>
            /// ��ǰҳ��Url
            /// </summary>
            public static string CurrentPage
            {
                get
                {
                    return "CurrentPage";
                }
            }

            /// <summary>
            /// ��ǰҳ��RootUrl
            /// </summary>
            public static string CurrentRootPage
            {
                get
                {
                    return "CurrentRootPage";
                }
            }


            /// <summary>
            /// ��ǰҳ��RootUrl
            /// </summary>
            public static string ProjectRootPage
            {
                get
                {
                    return "ProjectRootPage";
                }
            }
        }


        public static class RequestKey
        {
            /// <summary>
            /// ��ǰҳ��Url
            /// </summary>
            public static string CurrentPage
            {
                get
                {
                    return "CurrentPage";
                }
            }

            /// <summary>
            /// ��ǰҳ��RootUrl
            /// </summary>
            public static string CurrentRootPage
            {
                get
                {
                    return "CurrentRootPage";
                }
            }

            public static string UserMsgType
            {
                get
                {
                    return "UserMsgType";
                }
            }

            /// <summary>
            /// PostID
            /// </summary>
            public static string PostID
            {
                get
                {
                    return "PostID";
                }
            }



            public static string EditPostID
            {
                get { return "EditPostID"; }
            }

            public static string OperateType
            {
                get { return "OperateType"; }
            }

            public static string IsSysMsg
            {
                get
                {
                    return "IsSysMsg";
                }
            }

            public static string MeaiaItemID{
                get {return "Mid";}
            }

        }
        string urlparameters = string.Empty;
        public string Urlparameters
        {
            set { urlparameters = value; }
        }

        public static class Url
        {
            const string default_ = "";//"Message/";
            const string Clientdefault_ = "../Client/";
            const string Mediadefault_ = "../Media/";
            const string Projectdefault_ = "../Project/";

            public static string IndexPage
            {
                get { return "../System/Default.aspx"; }
            }
            public static string PostList
            {
                get { return default_ + "PostList.aspx"; }
            }
            public static string MsgReplyList
            {
                get
                {
                    return default_ + "MsgReplyList.aspx";
                }
            }

            public static string ReleasePosts
            {
                get { return default_ + "ReleasePosts.aspx"; }
                
            }
            public static string MsgList
            {
                get
                {
                    return default_ + "MsgList.aspx";
                }
            }


            public static string Login
            {
                get
                {

                    return default_ + "Login.aspx";
                }
            }

     
            public static string NewPost
            {
                get
                {
                    return default_ + "NewPost.aspx";
                }
            }
            public static string PostDetail
            {
                get
                {
                    return default_ + "PostDetail.aspx";
                }
            }

            public static string ReleaseView
            {
                get
                {
                    return default_ + "ReleaseView.aspx";
                }
            }
            public static string UserRegister
            {
                get
                {
                    return default_ + "UserRegister.aspx";
                }
            }
            public static string ClientList
            {
                get
                {
                    return Clientdefault_ + "ClientList.aspx";
                }
            }
            public static string ClientContentsList
            {
                get
                {
                    return Clientdefault_ + "ClientContentsList.aspx";
                }
            }
            public static string ProductLineList
            {
                get
                {
                    return Clientdefault_ + "ProductLineList.aspx";
                }
            }

            public static string ClientDetail
            {
                get { return Clientdefault_ + "ClientDetail.aspx"; }
            }

            public static string ClientAddAndEdit
            {
                get { return Clientdefault_ + "ClientAddAndEdit.aspx"; }
            }

            public static string ClientLinkProductLineList
            {
                get { return Clientdefault_ + "ClientLinkProductLineList.aspx"; }
            }

            public static string ProductSelectClientList
            {
                get { return Clientdefault_ + "ProductSelectClientList.aspx"; }
            }


            public static string ProductLineContentsList
            {
                get
                {
                    return Clientdefault_ + "ProductLineContentsList.aspx";
                }
            }
            public static string NewMedia
            {
                get
                {
                    return Mediadefault_ + "newMedia.aspx";
                }
            }
            public static string MediaList
            {
                get
                {
                    return Mediadefault_ + "MediaList.aspx";
                }
            }

            public static string AuditedMediaList
            {
                get
                {
                    return Mediadefault_ + "AuditedMediaList.aspx";
                }
            }

            public static string MediaLinkReporterList
            {
                get
                {
                    return Mediadefault_ + "MediaLinkReporterList.aspx";
                }
            }

            public static string DABMediaContentsList
            {
                get
                {
                    return Mediadefault_ + "DABMediaContentsList.aspx";
                }
            }
            public static string PlaneMediaContentsList
            {
                get
                {
                    return Mediadefault_ + "PlaneMediaContentsList.aspx";
                }
            }
            public static string TvMediaContentsList
            {
                get
                {
                    return Mediadefault_ + "TvMediaContentsList.aspx";
                }
            }
            public static string WebMediaContentsList
            {
                get
                {
                    return Mediadefault_ + "WebMediaContentsList.aspx";
                }
            }
            public static string ReporterList
            {
                get
                {
                    return Mediadefault_ + "ReporterList.aspx";
                }
            }
            public static string ReporterContentsList
            {
                get
                {
                    return Mediadefault_ + "ReporterContentsList.aspx";
                }
            }
            public static string ReporterSelectMediaList
            {
                get
                {
                    return Mediadefault_ + "ReporterSelectMediaList.aspx";
                }
            }

            public static string ProjectManageList
            {
                get
                {
                    return Projectdefault_ + "ProjectManageList.aspx";
                }
            }

            public static string ProjectList
            {
                get
                {
                    return Projectdefault_ + "ProjectList.aspx";
                }
            }

            public static string ProjectInfo
            {
                get
                {
                    return Projectdefault_ + "ProjectInfo.aspx";
                }
            }

            public static string ProjectMediaReporter
            {
                get
                {
                    return Projectdefault_ + "ProjectMediaReporter.aspx";
                }
            }

            public static string ProjectDailyEvent
            {
                get
                {
                    return Projectdefault_ + "ProjectDailyEvent.aspx";
                }
            }

            public static string ProjectBill
            {
                get
                {
                    return Projectdefault_ + "ProjectBill.aspx";
                }
            }

            public static string ProjectMembers
            {
                get
                {
                    return Projectdefault_ + "ProjectMembers.aspx";
                }
            }

            public static string UserList
            {
                get
                {
                    return Projectdefault_ + "UserList.aspx";
                }
            }

            public static string ProjectSelectMediaList
            {
                get
                {
                    return Projectdefault_ + "ProjectSelectMediaList.aspx";
                }
            }
            public static string ProjectSelectClientList
            {
                get
                {
                    return Projectdefault_ + "ProjectSelectClientList.aspx";
                }
            }
            public static string ProjectSelectProductLine
            {
                get
                {
                    return Projectdefault_ + "ProjectSelectProductLine.aspx";
                }
            }
            public static string ProjectSelectReporterList
            {
                get
                {
                    return Projectdefault_ + "ProjectSelectReporterList.aspx";
                }
            }
            public static string ProjectReporterSelectMediaList
            {
                get
                {
                    return Projectdefault_ + "ProjectReporterSelectMediaList.aspx";//
                }
            }
            public static string ProjectClientLinkProductLineList
            {
                get
                {
                    return Projectdefault_ + "ProjectClientLinkProductLineList.aspx";
                }
            } 
        }



        public static string AddParam(string querystring, string paramname,object value)
        {
            string q = querystring.Substring(querystring.IndexOf('?') + 1);
            q = RemoveParam(querystring, paramname);
            q += string.Format("&{0}={1}", paramname, value== null?string.Empty:value.ToString());
            return q;
        }


        public static string ModifyParam(string querystring, string paramname, object value)
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
            return newps;
        }

        public static string RemoveParam(string querystring, string paramname)
        {
            string q = querystring.Substring(querystring.IndexOf('?') + 1);
            string[] ps = q.Split('&');
            string newps = string.Empty;
            for (int i = 0; i < ps.Length; i++)
            {
                if (ps[i].Split('=')[0] != paramname)
                {
                    newps += "&"+ps[i];
                }
                
            }
            return newps.TrimStart('&');
        }
        public static class OpenClass
        {
            public static string Common = "height=600, width=1000, top=50, left=50, toolbar=no, menubar=no, scrollbars=false, resizable=no,location=no, status=no";
            //public static string MediaDisplay = "height=600, width=800, top=50, left=50, toolbar=no, menubar=no, scrollbars=yes, resizable=no,location=no, status=no";
        }
    }
}
