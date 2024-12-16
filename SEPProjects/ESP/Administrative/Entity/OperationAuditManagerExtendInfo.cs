using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Administrative.Entity
{
    public class OperationAuditManagerExtendInfo :OperationAuditManageInfo
    {
        private string _username;
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }

        /// <summary>
        /// 格式化数据对象
        /// </summary>
        /// <param name="r"></param>
        public new void PopupData(System.Data.IDataReader r)
        {
            if (r["ID"].ToString() != "")
            {
                ID = int.Parse(r["ID"].ToString());
            }
            if (r["UserID"].ToString() != "")
            {
                UserID = int.Parse(r["UserID"].ToString());
            }
            UserName = r["UserName"].ToString();
           
            if (r["TeamLeaderID"].ToString() != "")
            {
                TeamLeaderID = int.Parse(r["TeamLeaderID"].ToString());
            }
            TeamLeaderName = r["TeamLeaderName"].ToString();            
            if (r["Deleted"].ToString() != "")
            {
                if ((r["Deleted"].ToString() == "1") || (r["Deleted"].ToString().ToLower() == "true"))
                {
                    Deleted = true;
                }
                else
                {
                    Deleted = false;
                }
            }
           
        }

        /// <summary>
        /// 格式化数据对象
        /// </summary>
        /// <param name="r"></param>
        public new void PopupData(System.Data.DataRow r)
        {
            if (r["ID"].ToString() != "")
            {
                ID = int.Parse(r["ID"].ToString());
            }
            if (r["UserID"].ToString() != "")
            {
                UserID = int.Parse(r["UserID"].ToString());
            }
            UserName = r["UserName"].ToString();

            if (r["TeamLeaderID"].ToString() != "")
            {
                TeamLeaderID = int.Parse(r["TeamLeaderID"].ToString());
            }
            TeamLeaderName = r["TeamLeaderName"].ToString();
            if (r["Deleted"].ToString() != "")
            {
                if ((r["Deleted"].ToString() == "1") || (r["Deleted"].ToString().ToLower() == "true"))
                {
                    Deleted = true;
                }
                else
                {
                    Deleted = false;
                }
            }
        }
    }
}
