using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.Entity
{
    //  Id, DepId, DirectorId, DirectorName, ManagerId, ManagerName, CEOId, CEOName, FAId, FAName
    [Serializable]
    public class DirectorViewInfo
    {
        int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }


        int _depId;

        public int DepId
        {
            get { return _depId; }
            set { _depId = value; }
        }


        int _directorId;

        public int DirectorId
        {
            get { return _directorId; }
            set { _directorId = value; }
        }


        string _directorName;

        public string DirectorName
        {
            get { return _directorName; }
            set { _directorName = value; }
        }


        int _managerId;

        public int ManagerId
        {
            get { return _managerId; }
            set { _managerId = value; }
        }


        string _managerName;

        public string ManagerName
        {
            get { return _managerName; }
            set { _managerName = value; }
        }


        int _CEOId;

        public int CEOId
        {
            get { return _CEOId; }
            set { _CEOId = value; }
        }


        string _CEOName;

        public string CEOName
        {
            get { return _CEOName; }
            set { _CEOName = value; }
        }

        int _FAId;

        public int FAId
        {
            get { return _FAId; }
            set { _FAId = value; }
        }

        string _FAName;

        public string FAName
        {
            get { return _FAName; }
            set { _FAName = value; }
        }
    }
}
