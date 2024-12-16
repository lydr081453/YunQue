using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.Entity
{
    public class ReturnGeneralInfoListViewInfo : ReturnInfo
    {
        string _department;

        public string Department
        {
            get { return _department; }
            set { _department = value; }
        }
        string _orderid;

        public string Orderid
        {
            get { return _orderid; }
            set { _orderid = value; }
        }
        string account_name;

        public string Account_name
        {
            get { return account_name; }
            set { account_name = value; }
        }
        string account_bank;

        public string Account_bank
        {
            get { return account_bank; }
            set { account_bank = value; }
        }
        string account_number;

        public string Account_number
        {
            get { return account_number; }
            set { account_number = value; }
        }
    }
}
