using System;
using System.Collections.Generic;

namespace ESP.Finance.Entity
{
    public partial class ExpenseAccountBatchAudit
    {

        ESP.Finance.Entity.ExpenseAccountExtendsInfo model;
        public ESP.Finance.Entity.ExpenseAccountExtendsInfo Model
        {
            get { return model; }
            set { model = value; }
        }


        ESP.Finance.SqlDataAccess.WorkItem workitem;
        public ESP.Finance.SqlDataAccess.WorkItem Workitem
        {
            get { return workitem; }
            set { workitem = value; }
        }


        Dictionary<string, object> prarms;
        public Dictionary<string, object> Prarms
        {
            get { return prarms; }
            set { prarms = value; }
        }


        int currentUserId = 0;
        public int CurrentUserId
        {
            get { return currentUserId; }
            set { currentUserId = value; }
        }


    }
}
