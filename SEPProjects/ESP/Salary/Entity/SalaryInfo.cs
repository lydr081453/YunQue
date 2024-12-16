using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Salary.Entity
{
    public partial class SalaryInfo
    {
        private int _userID;
        public int UserID
        {
            get { return _userID; }
            set { _userID = value; }
        }

        private string _reissueBonuses;
        public string ReissueBonuses
        {
            get { return _reissueBonuses; }
            set { _reissueBonuses = value; }
        }

        private string _bonusesAverageMonth13th;
        public string BonusesAverageMonth13th
        {
            get { return _bonusesAverageMonth13th; }
            set { _bonusesAverageMonth13th = value; }
        }

    }
}
