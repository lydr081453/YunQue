using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.Entity
{
    public class AdminExpenseDetailInfo
    {
        private int _Id;
        private int _AreaId;
        private int _ExpenseTypeId;
        private int _ExpenseTotalCount;
        private int _ExpenseUsedCount;

        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public int AreaId
        {
            get { return _AreaId; }
            set { _AreaId = value; }
        }

        public int ExpenseTypeId
        {
            get { return _ExpenseTypeId; }
            set { _ExpenseTypeId = value; }
        }

        public int ExpenseTotalCount 
        {
            get { return _ExpenseTotalCount; }
            set { _ExpenseTotalCount = value; }
        }

        public int ExpenseUsedCount
        {
            get { return _ExpenseUsedCount; }
            set { _ExpenseUsedCount = value; }
        }
    }
}
