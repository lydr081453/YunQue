using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.ConfigCommon
{
    public interface WorkFlowBase
    {
        bool FormSubmit();
        bool FormAudit();
        bool FormClose();
        //private int _id;
        //private string _workflowname;
        //private string _workflowcode;
        //private DateTime _createduserdate;
        //private int _createduserid;
        //private int _type;

        //public int ID
        //{
        //    get { return _id; }
        //    set { _id = value; }
        //}

        //public string WorkFlowCode
        //{
        //    get { return _workflowcode; }
        //    set { _workflowcode = value; }
        //}

        //public string WorkFlowName
        //{
        //    get { return _workflowname; }
        //    set { _workflowname = value; }
        //}

        //public DateTime CreatedUserDate
        //{
        //    get { return _createduserdate; }
        //    set { _createduserdate = value; }
        //}

        //public int CreatedUserID
        //{
        //    get { return _createduserid; }
        //    set { _createduserid = value; }
        //}

        //public int Type
        //{
        //    get { return _type; }
        //    set { _type = value; }
        //}


    }
}