using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Common
{
    public partial class Global
    {
        public enum TwitterMessageType
        {
            Public = 0,
            Private = 1
        }

        public struct TwitterMessageSource
        {
            public const string HTML = "网页";
            public const string MSN = "MSN";
            public const string SMS = "短信";
            public const string BBS = "论坛";
            public const string BBS_REPLY = "论坛回复";
            public const string MSN_DISPLAY_NAME = "MSN签名更新";
            public const string MSN_PERSONAL_INFO = "MSN信息更新";
            public const string CLAW = "挠挠";
            public const string HELP_DEFAULT = "默认帮助信息";
        }

        public enum TwitterDJSendDJMessageType
        {
            Single = 0,
            All = 1,
            List = 2,
            Users = 3
        }

        public enum UserIconSize
        {
            Small = 48,
            Normal = 96,
            Large = 120
        }

        public enum MessengerValidation
        {
            User = 0,
            DJ = 1
        }

        public enum ClawType
        {
            Claw = 0,
            KissTo = 1,
            Hit = 2,
            Kick = 3
        }

        public enum HelpType
        {
            Default = 0,
            Remind = 1
        }
    }
}
