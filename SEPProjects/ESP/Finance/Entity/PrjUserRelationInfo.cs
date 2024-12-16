using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.Entity
{
    public class PrjUserRelationInfo
    {
            public PrjUserRelationInfo()
            { }
            #region Model
            private int _pid;
            private int? _deptid;
            private int? _userid;
            private int? _branchid;
            private string _branchcode;
            private int? _usable;
            private int _gm;
            /// <summary>
            /// 
            /// </summary>
            public int PID
            {
                set { _pid = value; }
                get { return _pid; }
            }
            /// <summary>
            /// 部门ID
            /// </summary>
            public int? DeptID
            {
                set { _deptid = value; }
                get { return _deptid; }
            }
            /// <summary>
            /// User ID
            /// </summary>
            public int? UserID
            {
                set { _userid = value; }
                get { return _userid; }
            }
            /// <summary>
            /// 分公司ID--F_branch
            /// </summary>
            public int? BranchID
            {
                set { _branchid = value; }
                get { return _branchid; }
            }
            /// <summary>
            /// 项目号首位，分公司代码
            /// </summary>
            public string BranchCode
            {
                set { _branchcode = value; }
                get { return _branchcode; }
            }
            /// <summary>
            /// 是否可用Branchcode开头的项目号，1为可用0为不可用
            /// </summary>
            public int? Usable
            {
                set { _usable = value; }
                get { return _usable; }
            }
            /// <summary>
            /// 是否可用GM项目号，1为可用0为不可用
            /// </summary>
            public int GM
            {
                set { _gm = value; }
                get { return _gm; }
            }
            #endregion Model

        }
    }
