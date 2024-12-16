using System;
using System.IO;
using System.Text;

namespace ESP.Supplier.Common
{
    public class Config
    {
        public static readonly long PHOTO_CONTENT_LENGTH = 1024 * 1024;
        public static readonly long USER_ICON_CONTENT_LENGTH = 1024 * 1024;
        public static readonly int USER_ICON_SIZE_SMALL = 48;
        public static readonly int USER_ICON_SIZE = 64;
        public static readonly int USER_ICON_SIZE_LARGE = 200;
        // public static readonly string USER_ICON_DIR
        public static readonly long THUMBNAIL_CONTENT_LENGTH_LIMIT = 0;
        public static readonly string[] PHOTO_CONTENT_TYPES = { "image/bmp", "image/jpeg", "image/pjpeg", "image/gif", "image/x-png", "application/octet-stream", "application/x-shockwave-flash" };
        public static readonly string[] PHOTO_EXTENSIONS = { ".bmp", ".jpg", ".jpg", ".gif", ".png" };
        
        public static readonly string PHOTO_DIR = System.Configuration.ConfigurationManager.AppSettings["PhotoDir"];
        public static readonly string FLASH_DIR = System.Configuration.ConfigurationManager.AppSettings["FlashDir"];
        public static readonly string USER_ICON_DIR = System.Configuration.ConfigurationManager.AppSettings["UserIcon"];
        private static string _wf;

        //过滤敏感词
        public static string WORD_FILTER
        {
            get
            {
                if (_wf == null || _wf.Trim() == "")
                {
                    TextReader tr = System.IO.File.OpenText(ESP.Configuration.ConfigurationManager.SafeAppSettings["WordFilter"]);
                    string str = tr.ReadToEnd();
                    tr.Close();
                    _wf = str.Replace("\r\n", "|");
                }
                return _wf;
            }
            set { _wf = value; }
        }

        //用户头像尺寸
        public struct IconSize
        {
            public const int VERYSMALL = 24;
            public const int SMALL = 48;
            public const int NORMAL = 64;
            public const int LARGE = 200;
        }

        public struct PhotoSizeSettings
        {
            public const int WITHOUTLIMIT = 2048;
            public const int MAXSIZE = 1600;
            public const int LARGESIZE = 1280;
            public const int NORMALSIZE = 1024;
            public const int SMALLSIZE = 800;
            public const int MINSIZE = 640;
        }

        #region Struct
        public struct FlowDisplayType
        {
            public const int LEFT = 0;
            public const int RIGHT = 1;
            public const int CENTER = 2;
        }

        //BlogType
        public struct BlogType
        {
            public const int NORMAL = 0;
            public const int LIVE = 1;
            public const int TOPIC = 2;
        }

        public struct PartnerType
        {
            public const int OWNER = 0;
            public const int FRIEND = 1;
            public const int GROUP = 2;
            public const int BOTH = 4;
            public const int ALL = 8;
            public const int Custom = 16;
        }

        public struct BlogStyle
        {
            public const int LOGLIFE = 0;
            public const int TEXT = 1;
            public const int GALLERY = 2;
        }

        //与某本流水账之间的关系
        public struct BlogRelationShip
        {
            public const int HAVE = 1;
            public const int HAVING = 2;
            public const int TOBE = 3;
        }

        public struct BlogUseful
        {
            public const int USEFUL = 1;
            public const int UNUSEFUL = -1;
        }

        //查询用户时使用
        public struct UserType
        {
            public const int ALL = 0;
            public const int MOBILE = 1;
            public const int ICON = 2;
            public const int WRITEBLOG = 3;
            public const int WRITELIVE = 4;
            public const int WRITETOPIC = 5;
        }

        //用户输入途径
        public struct InputType
        {
            public const int PAGE = 0;
            public const int SMS = 1;
            public const int MMS = 2;
            public const int MSN = 3;
            public const int QQ = 4;
            public const int GTALK = 5;
            public const int SKYPE = 6;
            public const int WAP = 7;
            public const int YAHOO = 8;
        }

        /// <summary>
        /// 获取用户输入途径
        /// </summary>
        /// <param name="itype"></param>
        /// <returns></returns>
        public static string GetInputType(int itype)
        {
            switch (itype)
            {
                case InputType.PAGE:
                    return "通过网页";
                case InputType.SMS:
                    return "通过短信";
                case InputType.MMS:
                    return "通过彩信";
                case InputType.MSN:
                    return "通过MSN";
                case InputType.QQ:
                    return "通过QQ";
                case InputType.GTALK:
                    return "通过Google Talk";
                case InputType.SKYPE:
                    return "通过Skype";
                case InputType.WAP:
                    return "通过WAP";
                case InputType.YAHOO:
                    return "通过雅虎通";
            }
            return "通过未知途径";
        }

        public static string InputTypeName(int itype)
        {
            switch (itype)
            {
                case InputType.PAGE:
                    return "网页";
                case InputType.SMS:
                    return "短信";
                case InputType.MMS:
                    return "彩信";
                case InputType.MSN:
                    return "MSN";
                case InputType.QQ:
                    return "QQ";
                case InputType.GTALK:
                    return "Google Talk";
                case InputType.SKYPE:
                    return "Skype";
                case InputType.WAP:
                    return "WAP";
                case InputType.YAHOO:
                    return "雅虎通";
            }
            return "未知";
        }

        public struct IMBind
        {
            public const int NONE = 0;
            public const int PreBind = 1;
            public const int Binded = 2;
        }

        public struct IMMessageType
        {
            public const int Attention = 0;
            public const int Normal = 1;
            public const int List = 2;
        }

        public struct FriendType
        {
            public const int NA = -1;
            public const int MyFriend = 0;
            public const int IsFriend = 1;
            public const int InviteMe = 2;
        }

        public struct SecurityLevel
        {
            public const int ALL = 0;
            public const int OnlyFriend = 1;
        }

        /// <summary>
        /// 事件类型
        /// </summary>
        public struct EventType
        {
            public const int AddFriend = 0;         //添加好友
            public const int RemoveFriend = 1;      //移除好友
            public const int UpdateSign = 2;        //更新签名
            public const int Subscribe = 3;         //订阅内容
            public const int UpdateReceiver = 4;    //更新接收信息的IM工具
            public const int TurnOffReceiver = 5;   //关闭接收工具
            public const int BindIM = 6;            //绑定IM工具
            public const int UnBindIM = 7;          //绑定IM工具
            public const int BindMobile = 8;        //绑定手机
            public const int UnBindMobile = 9;      //解除绑定手机
            public const int AddAttention = 10;     //关注对方
            public const int RemoveAttention = 11;  //移除关注
            public const int SetSecurtiyLevel = 12; //设置安全性
            public const int UpdateStatus = 13;     //更新状态
            public const int ValidateIM = 14;       //验证IM工具
            public const int ValidateMobile = 15;   //验证手机
            public const int AddComment = 16;       //添加评论
            public const int DelComment = 17;       //删除评论
            public const int AddBlog = 18;       //添加Blog
            public const int UpdateBlog = 19;       //修改Blog
            public const int DelBlog = 20;       //删除Blog
            //发送私信
            //邀请加入组
            //申请加入组
            //审核同意加入组
            //删除组成员
            //邀请加入活动
            //申请加入活动
            //审核同意加入活动
            //删除活动成员
            //添加Shine
            //修改Shine
            //删除Shine
            //续写Shine
            //添加Shine中讨论
            //删除Shine中讨论
            //添加Pink
            //修改Pink
            //删除Pink
            //续写Pink
            //添加Pink中讨论
            //删除Pink中讨论
            //添加拼车
            //修改拼车
            //删除拼车
            //续写拼车
            //添加拼车中讨论
            //删除拼车中讨论
        }

        //首页
        public struct TagType
        {
            public const string BugTag = "[Bug报告版]";
            public const string TestTag = "[测试留言板]";
            public const string HelpTag = "[帮助留言板]";
            public const string ReportTag = "[举报留言板]";
        }

        public static string EventTypeMessage(int type)
        {
            string msg = "";
            switch (type)
            {
                case EventType.AddFriend:
                    msg = "{0}添加{1}为好友";
                    break;
                case EventType.RemoveFriend:
                    msg = "{0}将{1}移出好友列表";
                    break;
                case EventType.UpdateSign:
                    msg = "{0}更新了{1}签名：{2}";
                    break;
                case EventType.Subscribe:
                    msg = "{0}订阅了{1}";
                    break;
                case EventType.UpdateReceiver:
                    msg = "{0}设置信息接收工具为：{1}";
                    break;
                case EventType.TurnOffReceiver:
                    msg = "{0}关闭了{1}的接收功能";
                    break;
                case EventType.BindIM:
                    msg = "{0}绑定了通讯工具{1}";
                    break;
                case EventType.UnBindIM:
                    msg = "{0}取消了{1}的绑定";
                    break;
                case EventType.BindMobile:
                    msg = "{0}绑定了手机";
                    break;
                case EventType.UnBindMobile:
                    msg = "{0}取消了手机的绑定";
                    break;
                case EventType.AddAttention:
                    msg = "{0}开始关注{1}";
                    break;
                case EventType.RemoveAttention:
                    msg = "{0}不再关注{1}";
                    break;
                case EventType.SetSecurtiyLevel:
                    msg = "{0}改变了安全设置：";
                    break;
                case EventType.UpdateStatus:
                    msg = "{0}更新了当前状态：{1}";
                    break;
                case EventType.ValidateIM:
                    msg = "{0}验证了{1}";
                    break;
                case EventType.ValidateMobile:
                    msg = "{0}验证了手机";
                    break;
                case EventType.AddComment:
                    msg = "{0}添加了对{1}的{2}评论：{3}";
                    break;
                case EventType.DelComment:
                    msg = "{0}删除了对{1}的评论";
                    break;
                case EventType.AddBlog:
                    msg = "{0}添加了{1}";
                    break;
                case EventType.UpdateBlog:
                    msg = "{0}修改了{1}";
                    break;
                case EventType.DelBlog:
                    msg = "{0}删除了Blog：{1}";
                    break;
            }
            return msg;
        }
        #endregion

        public const int ItemCountPerPage = 20;
        public const int RecentItemsCount = 400;

        public static string[] PagerTemplate = 
{
    @"<a href='{0}'>第一页</a>&nbsp;<a href='{1}'>上一页</a>&nbsp;{2}<a href='{3}'>下一页</a>&nbsp;<a href='{4}'>最终页</a>"
};
        public const int ASC = 0;//时间顺序
        public const int DESC = 1;//时间逆序

        public enum MESSAGE_TYPE
        {
            Message = 0,
            Event = 1
        }
        public enum FILE_TYPE
        {
            Photo = 0,
            Flash = 1
        }



        public static readonly DateTime NULLTIME = DateTime.Parse("1990-1-1");

        public static string BuildImageLink(int id, string src, string srcSmall, string content, string exif)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<a id='");
            sb.Append(id);
            sb.Append("' href='");
            sb.Append(src);
            sb.Append("' class='highslide' onclick=\"return hs.expand(this, { captionId: 'item");
            sb.Append(id);
            sb.Append("'})\">");
            sb.Append("<img src='");
            sb.Append(srcSmall);
            sb.Append("' title='点击查看'/></a>");
            sb.Append("<div class='highslide-caption' id='item");
            sb.Append(id);
            sb.Append("'>");
            sb.Append("文字内容：");
            sb.Append(content);
            sb.Append("<hr style='width:100%;'/>");
            sb.Append("EXIF信息：<br/>");
            sb.Append(exif.Replace("\r\n", "<br/>"));
            sb.Append("</div>");
            return sb.ToString();
        }

        public static string BuildIconLink(int id, string src, int size)
        {
            if (src != null && src.Trim() != "" && src.IndexOf(USER_ICON_DIR) < 0)
            {
                src = USER_ICON_DIR + src;
            }
            StringBuilder sb = new StringBuilder();
            if (src == null)
            {
                sb.Append(@"<a href='/userhome/default.aspx?u=");
                sb.Append(id);
                sb.Append("'><img src='/usericon/user.jpg' border='0' style='width:");
                sb.Append(size);
                sb.Append("px;height:");
                sb.Append(size);
                sb.Append("px;'></a>");
            }
            else
            {
                string ss = ImageHelper.GetIconVirtualPath(src, size);
                if (ss == src)
                {
                    sb.Append(@"<a href='/userhome/default.aspx?u=");
                    sb.Append(id);
                    sb.Append("'><img src='");
                    sb.Append(src);
                    sb.Append("' title='点击查看' class='highslide'/></a>");
                }
                else
                {
                    sb.Append("<a href='");
                    sb.Append(src);
                    sb.Append("' class='highslide' onclick=\"return hs.expand(this, { captionId: 'item");
                    sb.Append(id);
                    sb.Append("'})\"><img src='");
                    sb.Append(ss);
                    sb.Append("' title='点击查看'/></a>");
                }
            }
            return sb.ToString();
        }

        public static string BuildBlockCoverLink(int id, string src, int size)
        {
            if (src != null && src.Trim() != "" && src.IndexOf(USER_ICON_DIR) < 0)
            {
                src = USER_ICON_DIR + src;
            }
            StringBuilder sb = new StringBuilder();
            if (src == null)
            {
                sb.Append(@"<a href='/userhome/default.aspx?u=");
                sb.Append(id);
                sb.Append("'><img src='/usericon/user.jpg' border='0' style='width:");
                sb.Append(size);
                sb.Append("px;height:");
                sb.Append(size);
                sb.Append("px;'></a>");
            }
            else
            {
                string ss = ImageHelper.GetIconVirtualPath(src, size);
                if (ss == src)
                {
                    sb.Append(@"<a href='/userhome/default.aspx?u=");
                    sb.Append(id);
                    sb.Append("'><img src='");
                    sb.Append(src);
                    sb.Append("' title='点击查看' class='highslide' /></a>");
                }
                else
                {
                    sb.Append("<a href='");
                    sb.Append(src);
                    sb.Append("' class='highslide' onclick=\"return hs.expand(this, { captionId: 'item");
                    sb.Append(id);
                    sb.Append("'})\"><img src='");
                    sb.Append(ss);
                    sb.Append("' title='点击查看'/></a>");
                }
            }
            return sb.ToString();
        }

        public static string BuildSimpleIconLink(int id, string src, int size)
        {
            if (src != null && src.Trim() != "" && src.IndexOf(USER_ICON_DIR) < 0)
            {
                src = USER_ICON_DIR + src;
            }
            StringBuilder sb = new StringBuilder();
            if (src == null || src.Trim() == "")
            {
                sb.Append(@"<a href='/userhome/default.aspx?u=");
                sb.Append(id);
                sb.Append("'><img src='/usericon/user.jpg' border='0' style='width:");
                sb.Append(size);
                sb.Append("px;height:");
                sb.Append(size);
                sb.Append("px;'></a>");
            }
            else
            {
                sb.Append(@"<a href='/userhome/default.aspx?u=");
                sb.Append(id);
                sb.Append("'><img src='" + ImageHelper.GetIconVirtualPath(src, size) + "' border='0'></a>");
            }
            return sb.ToString();
        }

        public static string BuildSimpleIconLinkName(int id, string src, int size, string name)
        {
            if (src != null && src.Trim() != "" && src.IndexOf(USER_ICON_DIR) < 0)
            {
                src = USER_ICON_DIR + src;
            }
            StringBuilder sb = new StringBuilder();
            if (src == null || src.Trim() == "")
            {
                sb.Append(@"<a href='/userhome/default.aspx?u=");
                sb.Append(id);
                sb.Append("'><img src='/usericon/user.jpg' alt='" + name + "' border='0' style='width:");
                sb.Append(size);
                sb.Append("px;height:");
                sb.Append(size);
                sb.Append("px;'></a>");
            }
            else
            {
                sb.Append(@"<a href='/userhome/default.aspx?u=");
                sb.Append(id);
                sb.Append("'><img src='" + ImageHelper.GetIconVirtualPath(src, size) + "' alt='" + name + "' border='0' style='width:");
                sb.Append(size);
                sb.Append("px;height:");
                sb.Append(size);
                sb.Append("px;'></a>");
            }
            return sb.ToString();
        }

        public static string BuildSimpleIconLinkWithNickname(int id, string src, int size, string nickname)
        {
            StringBuilder sb = new StringBuilder();
            string txt = BuildSimpleIconLink(id, src, size);
            sb.Append(txt.Substring(0, txt.Length - 4));
            sb.Append("<br><span>" + nickname + "</span></a>");
            return sb.ToString();
        }

        public static string RenderSpace(string inputtxt)
        {
            string outtxt = inputtxt;
            for (int i = 0; i < 9 - inputtxt.Length; i++)
            {
                outtxt += "　";
            }
            outtxt += "：";
            return outtxt;
        }

        #region 新增的部分
        //groupmember和partner中的验证状态
        public struct ValidateStatus
        {
            public const int JOIN = 1;
            public const int INVITE = 2;
            public const int AGREE = 3;
        }

        //blog的类型
        public struct TypeBlog
        {
            public const int NORMAL = 0;
            public const int GROUPBLOG = 1;
            public const int FREEDOM = 2;
            public const int PINK = 3;
            public const int SHOCK = 4;
        }

        //user的类型
        public struct TypeUser
        {
            public const int NORMAL = 0;
        }

        //group的类型
        public struct GroupType
        {
            public const int NORMAL = 0;
            public const int COMPANY = 1;
        }

        //everyday中的relationtype
        public struct RelationType
        {
            public const int NULL = 0;
            public const int BLOG = 1;
            public const int GROUP = 2;
            public const int USER = 3;
        }

        //T_UserBlock中的type类型
        public struct UserBlockType
        {
            public const int NULL = 0;
            public const int HOME = 1;
            public const int WORK = 2;
            public const int VIEW = 3;
        }
        #endregion

        #region 经纬度
        public struct JWD
        {
            double JD, JF, JM;
            double WD, WF, WM;
            public double Jd, Wd, J, W;
            public const double Ea = 6378137;     //   赤道半径  
            public const double Eb = 6356725;     //   极半径  
            public readonly double Ec;
            public readonly double Ed;

            //   构造函数,   经度:   a   度,   b   分,   c   秒;     纬度:   d   度,   e   分,   f   秒  
            public JWD(double a, double b, double c, double d, double e, double f)
            {
                JD = a; JF = b; JM = c; WD = d; WF = e; WM = f;
                Jd = JD + JF / 60 + JM / 3600;
                Wd = WD + WF / 60 + WM / 3600;
                J = Jd * Math.PI / 180;
                W = Wd * Math.PI / 180;
                Ec = Eb + (Ea - Eb) * (90 - Wd) / 90;
                Ed = Ec * Math.Cos(W);
            }
        }

        public static double Calc(JWD A, JWD B)
        {
            double dx = (B.J - A.J) * A.Ed;
            double dy = (B.W - A.W) * A.Ec;
            double d = Math.Sqrt(dx * dx + dy * dy);
            return d;
        }

        //JWD A0 = new JWD(116, 35, 16.26, 39, 49, 34.16);
        //JWD A1 = new JWD(115, 45, 45.40, 40, 14, 41.73);
        //Calc(A0, A1);
        #endregion
    }
}