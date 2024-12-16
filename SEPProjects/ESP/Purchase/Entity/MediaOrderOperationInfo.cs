using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Purchase.Entity
{
    public class MediaOrderOperationInfo
    {
        string _mediaOrderIDS;

        public string MediaOrderIDS
        {
            get { return _mediaOrderIDS; }
            set { _mediaOrderIDS = value; }
        }
        decimal _totalPrice;

        public decimal TotalPrice
        {
            get { return _totalPrice; }
            set { _totalPrice = value; }
        }
        string _currentUserID;

        public string CurrentUserID
        {
            get { return _currentUserID; }
            set { _currentUserID = value; }
        }
        string _currentUserCode;
        public string CurrentUserCode
        {
            get { return _currentUserCode; }
            set { _currentUserCode = value; }
        }

        string _currentUserEmpName;

        public string CurrentUserEmpName
        {
            get { return _currentUserEmpName; }
            set { _currentUserEmpName = value; }
        }

        string _currentUserName;

        public string CurrentUserName
        {
            get { return _currentUserName; }
            set { _currentUserName = value; }
        }
        string _fileName;

        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }
        int _generalID;
        public int GeneralID
        {
            get { return _generalID; }
            set { _generalID = value; }
        }
        string _PRNO;
        public string PRNO
        {
            get { return _PRNO; }
            set { _PRNO = value; }
        }

        int _flag;
        public int Flag
        {
            get { return _flag; }
            set { _flag = value; }
        }
        string _bankname;
        public string BankName
        {
            get { return _bankname; }
            set { _bankname = value; }
        }
        string _BankAccountName;
        public string BankAccountName
        {
            get { return _BankAccountName; }
            set { _BankAccountName = value; }
        }
        string _BankAccount;
        public string BankAccount
        {
            get { return _BankAccount; }
            set { _BankAccount = value; }
        }
    }
}
