using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Purchase.Entity
{
    public class BlackListInfo
    {
            public BlackListInfo()
            { }
            #region Model
            private int _blacklistid;
            private int? _userid;
            private string _employeename;
            private int? _status;
            /// <summary>
            /// 
            /// </summary>
            public int BlackListID
            {
                set { _blacklistid = value; }
                get { return _blacklistid; }
            }
            /// <summary>
            /// 
            /// </summary>
            public int? UserID
            {
                set { _userid = value; }
                get { return _userid; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string EmployeeName
            {
                set { _employeename = value; }
                get { return _employeename; }
            }
            /// <summary>
            /// 
            /// </summary>
            public int? Status
            {
                set { _status = value; }
                get { return _status; }
            }
            #endregion Model

        }
    }
