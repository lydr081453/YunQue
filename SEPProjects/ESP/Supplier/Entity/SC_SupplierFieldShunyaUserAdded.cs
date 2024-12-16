using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Supplier.Entity
{
    public class SC_SupplierFieldShunyaUserAdded
    {
        private int _Id ;
        private int _SupplierId ;
        private string _CityName ;
        private string _FieldName ;
        private string _FieldType ;
        private string _FieldLevel;
        private string _LinkerName;
        private string _LinkerTel ;
        private int _RoomCount ;
        private string _FieldAddress;
        private string _EditUserType;
        private int _EditUserId ;
        private DateTime _AddTime ;
        private string _AddIp ;
        private DateTime _EditTime ;
        private string _EditIp ;

        private decimal _Length ;
        private decimal _Width;
        private decimal _Height;
        private decimal _Area;
        private int _TheaterNum;
        private int _ClassroomNum;
        private int _BanquetNum;
        private int _CocktailNum;
        private int _BoardRoomNum; 
        private int _UShapedNum;
        private string _BallRoomDesc ;

        private string _FieldStyle ;
        private string _CaseDesc ;
        private string _ApplyType ;
        private string _Price ;
        private string _SupplierName ;
        private string _Email ;
        private string _Floor;












        public int Id 
        { 
            set { _Id = value;} 
            get { return _Id;} 
        }
        public int SupplierId
        { 
            set { _SupplierId = value;} 
            get { return _SupplierId;} 
        }
        public string CityName
        { 
            set { _CityName = value;} 
            get { return _CityName;} 
        }
        public string FieldName
        { 
            set { _FieldName = value;} 
            get { return _FieldName;} 
        }
        public string FieldType
        { 
            set { _FieldType = value;} 
            get { return _FieldType;} 
        }
        public string FieldLevel
        { 
            set { _FieldLevel = value;} 
            get { return _FieldLevel;} 
        }
        public string LinkerName
        { 
            set { _LinkerName = value;} 
            get { return _LinkerName;} 
        }
        public string LinkerTel 
        { 
            set { _LinkerTel = value;} 
            get { return _LinkerTel;} 
        }
        public int RoomCount 
        { 
            set { _RoomCount = value;} 
            get { return _RoomCount;} 
        }
        public string FieldAddress 
        { 
            set { _FieldAddress = value;} 
            get { return _FieldAddress;} 
        }
        public string EditUserType 
        { 
            set { _EditUserType = value;} 
            get { return _EditUserType;} 
        }
        public int EditUserId 
        { 
            set { _EditUserId = value;} 
            get { return _EditUserId;} 
        }
        public DateTime AddTime 
        { 
            set { _AddTime = value;} 
            get { return _AddTime;} 
        }
        public string AddIp 
        { 
            set { _AddIp = value;} 
            get { return _AddIp;} 
        }
        public DateTime EditTime 
        { 
            set { _EditTime = value;} 
            get { return _EditTime;} 
        }
        public string EditIp 
        { 
            set { _EditIp = value;} 
            get { return _EditIp;} 
        }

        public decimal Length 
        { 
            set { _Length = value;} 
            get { return _Length;} 
        }
        public decimal Width 
        { 
            set { _Width = value;} 
            get { return _Width;} 
        }
        public decimal Height 
        { 
            set { _Height = value;} 
            get { return _Height;} 
        }
        public decimal Area 
        { 
            set { _Area = value;} 
            get { return _Area;} 
        }
        public int TheaterNum 
        { 
            set { _TheaterNum = value;} 
            get { return _TheaterNum;} 
        }
        public int ClassroomNum 
        { 
            set { _ClassroomNum = value;} 
            get { return _ClassroomNum;} 
        }
        public int BanquetNum 
        { 
            set { _BanquetNum = value;} 
            get { return _BanquetNum;} 
        }
        public int CocktailNum 
        { 
            set { _CocktailNum = value;} 
            get { return _CocktailNum;} 
        }
        public int BoardRoomNum 
        { 
            set { _BoardRoomNum = value;} 
            get { return _BoardRoomNum;} 
        }
        public int UShapedNum 
        { 
            set { _UShapedNum = value;} 
            get { return _UShapedNum;} 
        }
        public string BallRoomDesc 
        { 
            set { _BallRoomDesc = value;} 
            get { return _BallRoomDesc;} 
        }

        public string FieldStyle 
        { 
            set { _FieldStyle = value;} 
            get { return _FieldStyle;} 
        }
        public string CaseDesc 
        { 
            set { _CaseDesc = value;} 
            get { return _CaseDesc;} 
        }
        public string ApplyType 
        { 
            set { _ApplyType = value;} 
            get { return _ApplyType;} 
        }
        public string Price 
        { 
            set { _Price = value;} 
            get { return _Price;} 
        }
        public string SupplierName 
        { 
            set { _SupplierName = value;} 
            get { return _SupplierName;} 
        }
        public string Email 
        { 
            set { _Email = value;} 
            get { return _Email;} 
        }
        public string Floor 
        { 
            set { _Floor = value;} 
            get { return _Floor;} 
        }



        private string _XPoint;
        private string _YPoint;
        private string _MapLink;
        private string _MapKey;
        private bool _IsActive;
        private bool _IsApproved;
        private int _ShunYaUserID;
        private int _ApprovedShunYaUserID;
        private int _OldFiledID;
        private string _ShunYaUserName;
        private bool _IsDel;

        
        public string XPoint 
        { 
            set { _XPoint = value;} 
            get { return _XPoint;} 
        }
        public string YPoint 
        { 
            set { _YPoint = value;} 
            get { return _YPoint;} 
        }
        public string MapLink 
        { 
            set { _MapLink = value;} 
            get { return _MapLink;} 
        }
        public string MapKey 
        { 
            set { _MapKey = value;} 
            get { return _MapKey;} 
        }
        public bool IsActive 
        { 
            set { _IsActive = value;} 
            get { return _IsActive;} 
        }
        public bool IsApproved 
        { 
            set { _IsApproved = value;} 
            get { return _IsApproved;} 
        }
        public int ShunYaUserID 
        { 
            set { _ShunYaUserID = value;} 
            get { return _ShunYaUserID;} 
        }
        public int ApprovedShunYaUserID 
        { 
            set { _ApprovedShunYaUserID = value;} 
            get { return _ApprovedShunYaUserID;} 
        }
        public int OldFiledID 
        { 
            set { _OldFiledID = value;} 
            get { return _OldFiledID;} 
        }
        public string ShunYaUserName 
        { 
            set { _ShunYaUserName = value;} 
            get { return _ShunYaUserName;} 
        }
        public bool IsDel 
        { 
            set { _IsDel = value;} 
            get { return _IsDel;} 
        }
    }
}
