using System;
using System.Collections.Generic;
using ESP.Framework.DataAccess.Utilities;
using System.Text;

namespace ESP.Framework.Entity
{

    /// <summary>
    /// 员工信息
    /// </summary>
    [Serializable]
    public class EmployeeInfo
    {
        #region Fields
        private int _UserID = NullValues.Int32;
        private String _Code = NullValues.String;
        private String _Username = NullValues.String;
        private String _FirstNameCN = NullValues.String;
        private String _LastNameCN = NullValues.String;
        private String _FirstNameEN = NullValues.String;
        private String _LastNameEN = NullValues.String;

        private Int32 _TypeID = NullValues.Int32;
        private String _TypeName = NullValues.String;

        private String _Phone1 = NullValues.String;
        private String _Phone2 = NullValues.String;
        private String _MobilePhone = NullValues.String;
        private String _HomePhone = NullValues.String;
        private String _Fax = NullValues.String;
        private String _InternalEmail = NullValues.String;
        private String _IM = NullValues.String;
        private String _EmergencyContact = NullValues.String;
        private String _EmergencyContactPhone = NullValues.String;

        private String _Address = NullValues.String;
        private String _City = NullValues.String;
        private String _Province = NullValues.String;
        private String _Country = NullValues.String;
        private String _PostCode = NullValues.String;

        private String _Address2 = NullValues.String;
        private String _City2 = NullValues.String;
        private String _Province2 = NullValues.String;
        private String _Country2 = NullValues.String;
        private String _PostCode2 = NullValues.String;

        private DateTime _Birthday = NullValues.DateTime;
        private String _BirthPlace = NullValues.String;
        private String _DomicilePlace = NullValues.String;
        private String _IDNumber = NullValues.String;
        private String _Photo = NullValues.String;

        private String _Degree = NullValues.String;
        private String _Education = NullValues.String;
        private String _GraduateFrom = NullValues.String;
        private String _Major = NullValues.String;
        private DateTime _GraduatedDate = NullValues.DateTime;

        private String _Health = NullValues.String;
        private String _DiseaseInSixMonths = NullValues.String;
        private String _DiseaseInSixMonthsInfo = NullValues.String;

        private String _WorkExperience = NullValues.String;
        private String _WorkSpecialty = NullValues.String;
        private String _ThisYearSalary = NullValues.String;

        private String _WorkAddress = NullValues.String;
        private String _WorkCity = NullValues.String;
        private String _WorkProvince = NullValues.String;
        private String _WorkCountry = NullValues.String;
        private String _WorkPostCode = NullValues.String;


        private Int32 _Status = NullValues.Int32;
        private Boolean _BaseInfoOK = NullValues.Boolean;
        private Boolean _ContractInfoOK = NullValues.Boolean;
        private Boolean _InsuranceInfoOK = NullValues.Boolean;
        private Boolean _ArchiveInfoOK = NullValues.Boolean;
        private String _Memo = NullValues.String;

        private Int32 _Creator = NullValues.Int32;
        private String _CreatorName = NullValues.String;
        private DateTime _CreatedTime = NullValues.DateTime;
        private Int32 _LastModifier = NullValues.Int32;
        private DateTime _LastModifiedTime = NullValues.DateTime;
        private String _LastModifierName = NullValues.String;

        private Byte[] _RowVersion = NullValues.ByteArray;

        private String _Resume = NullValues.String;
        #endregion

        /// <summary>
        /// 电话1
        /// </summary>
        public String Phone1
        {
            get { return _Phone1; }
            set { _Phone1 = value; }
        }

        /// <summary>
        /// 电话2
        /// </summary>
        public String Phone2
        {
            get { return _Phone2; }
            set { _Phone2 = value; }
        }

        /// <summary>
        /// 移动电话
        /// </summary>
        public String MobilePhone
        {
            get { return _MobilePhone; }
            set { _MobilePhone = value; }
        }

        /// <summary>
        /// 家庭电话
        /// </summary>
        public String HomePhone
        {
            get { return _HomePhone; }
            set { _HomePhone = value; }
        }

        /// <summary>
        /// 紧急联络人
        /// </summary>
        public String EmergencyContact
        {
            get { return _EmergencyContact; }
            set { _EmergencyContact = value; }
        }

        /// <summary>
        /// 紧急联络电话
        /// </summary>
        public String EmergencyContactPhone
        {
            get { return _EmergencyContactPhone; }
            set { _EmergencyContactPhone = value; }
        }

        /// <summary>
        /// 邮政编码
        /// </summary>
        public String PostCode
        {
            get { return _PostCode; }
            set { _PostCode = value; }
        }

        /// <summary>
        /// 地址2
        /// </summary>
        public String Address2
        {
            get { return _Address2; }
            set { _Address2 = value; }
        }

        /// <summary>
        /// 城市2
        /// </summary>
        public String City2
        {
            get { return _City2; }
            set { _City2 = value; }
        }

        /// <summary>
        /// 省份2
        /// </summary>
        public String Province2
        {
            get { return _Province2; }
            set { _Province2 = value; }
        }

        /// <summary>
        /// 国家2
        /// </summary>
        public String Country2
        {
            get { return _Country2; }
            set { _Country2 = value; }
        }

        /// <summary>
        /// 邮政编码2
        /// </summary>
        public String PostCode2
        {
            get { return _PostCode2; }
            set { _PostCode2 = value; }
        }

        /// <summary>
        /// 婚姻状况
        /// </summary>
        public int MaritalStatus { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public int Gender { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public DateTime Birthday
        {
            get { return _Birthday; }
            set { _Birthday = value; }
        }

        /// <summary>
        /// 出生地
        /// </summary>
        public String BirthPlace
        {
            get { return _BirthPlace; }
            set { _BirthPlace = value; }
        }

        /// <summary>
        /// 现居住地
        /// </summary>
        public String DomicilePlace
        {
            get { return _DomicilePlace; }
            set { _DomicilePlace = value; }
        }

        /// <summary>
        /// 身份证件号码
        /// </summary>
        public String IDNumber
        {
            get { return _IDNumber; }
            set { _IDNumber = value; }
        }

        /// <summary>
        /// 照片(图片路径)
        /// </summary>
        public String Photo
        {
            get { return _Photo; }
            set { _Photo = value; }
        }

        /// <summary>
        /// 学位
        /// </summary>
        public String Degree
        {
            get { return _Degree; }
            set { _Degree = value; }
        }

        /// <summary>
        /// 学历
        /// </summary>
        public String Education
        {
            get { return _Education; }
            set { _Education = value; }
        }

        /// <summary>
        /// 毕业院校
        /// </summary>
        public String GraduateFrom
        {
            get { return _GraduateFrom; }
            set { _GraduateFrom = value; }
        }

        /// <summary>
        /// 专业/主修课程
        /// </summary>
        public String Major
        {
            get { return _Major; }
            set { _Major = value; }
        }

        /// <summary>
        /// 毕业时间
        /// </summary>
        public DateTime GraduatedDate
        {
            get { return _GraduatedDate; }
            set { _GraduatedDate = value; }
        }

        /// <summary>
        /// 健康状况
        /// </summary>
        public String Health
        {
            get { return _Health; }
            set { _Health = value; }
        }

        /// <summary>
        /// 6个月内患过何种疾病
        /// </summary>
        public String DiseaseInSixMonths
        {
            get { return _DiseaseInSixMonths; }
            set { _DiseaseInSixMonths = value; }
        }

        /// <summary>
        /// 6个月内所患疾病信息
        /// </summary>
        public String DiseaseInSixMonthsInfo
        {
            get { return _DiseaseInSixMonthsInfo; }
            set { _DiseaseInSixMonthsInfo = value; }
        }

        /// <summary>
        /// 工作经历
        /// </summary>
        public String WorkExperience
        {
            get { return _WorkExperience; }
            set { _WorkExperience = value; }
        }

        /// <summary>
        /// 工作特长
        /// </summary>
        public String WorkSpecialty
        {
            get { return _WorkSpecialty; }
            set { _WorkSpecialty = value; }
        }

        /// <summary>
        /// 本年薪资
        /// </summary>
        public String ThisYearSalary
        {
            get { return _ThisYearSalary; }
            set { _ThisYearSalary = value; }
        }

        /// <summary>
        /// 工作地邮政编码
        /// </summary>
        public String WorkPostCode
        {
            get { return _WorkPostCode; }
            set { _WorkPostCode = value; }
        }

        /// <summary>
        /// 基本信息是否完整
        /// </summary>
        public Boolean BaseInfoOK
        {
            get { return _BaseInfoOK; }
            set { _BaseInfoOK = value; }
        }

        /// <summary>
        /// 合同是否完整
        /// </summary>
        public Boolean ContractInfoOK
        {
            get { return _ContractInfoOK; }
            set { _ContractInfoOK = value; }
        }

        /// <summary>
        /// 社会保险信息是否完整
        /// </summary>
        public Boolean InsuranceInfoOK
        {
            get { return _InsuranceInfoOK; }
            set { _InsuranceInfoOK = value; }
        }

        /// <summary>
        /// 档案信息是否完整
        /// </summary>
        public Boolean ArchiveInfoOK
        {
            get { return _ArchiveInfoOK; }
            set { _ArchiveInfoOK = value; }
        }

        /// <summary>
        /// 备注事项
        /// </summary>
        public String Memo
        {
            get { return _Memo; }
            set { _Memo = value; }
        }


        /// <summary>
        /// 用记ID
        /// </summary>
        public int UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public String Username
        {
            get { return _Username; }
            set { _Username = value; }
        }

        ///<summary>
        ///员工编号
        ///</summary>
        public String Code
        {
            get { return _Code; }
            set { _Code = value; }
        }
        /// <summary>
        /// 中文名字
        /// </summary>
        public String FirstNameCN
        {
            get { return _FirstNameCN; }
            set { _FirstNameCN = value; }
        }
        /// <summary>
        /// 中文姓氏
        /// </summary>
        public String LastNameCN
        {
            get { return _LastNameCN; }
            set { _LastNameCN = value; }
        }
        /// <summary>
        /// 中文全名
        /// </summary>
        public String FullNameCN
        {
            get { return _LastNameCN + _FirstNameCN; }
        }
        /// <summary>
        /// 英文名字
        /// </summary>
        public String FirstNameEN
        {
            get { return _FirstNameEN; }
            set { _FirstNameEN = value; }
        }
        /// <summary>
        /// 英文姓氏
        /// </summary>
        public String LastNameEN
        {
            get { return _LastNameEN; }
            set { _LastNameEN = value; }
        }
        /// <summary>
        /// 英文命名
        /// </summary>
        public String FullNameEN
        {
            get
            {
                if (_FirstNameEN == null || _FirstNameEN.Length == 0)
                    return _LastNameEN;
                if (_LastNameEN == null || _LastNameEN.Length == 0)
                    return _FirstNameEN;
                return _FirstNameEN + " " + _LastNameEN;
            }
        }
        ///<summary>
        ///员工类型ID（全职、兼职）
        ///</summary>
        public Int32 TypeID
        {
            get { return _TypeID; }
            set { _TypeID = value; }
        }
        /// <summary>
        /// 员工类型名称（全职、兼职）
        /// </summary>
        public String TypeName
        {
            get { return _TypeName; }
            set { _TypeName = value; }
        }

        /// <summary>
        /// 员工传真号码
        /// </summary>
        public String Fax
        {
            get { return _Fax; }
            set { _Fax = value; }
        }

        ///<summary>
        ///员工家庭住址
        ///</summary>
        public String Address
        {
            get { return _Address; }
            set { _Address = value; }
        }
        ///<summary>
        ///员工居住城市
        ///</summary>
        public String City
        {
            get { return _City; }
            set { _City = value; }
        }
        ///<summary>
        ///员工居住省份
        ///</summary>
        public String Province
        {
            get { return _Province; }
            set { _Province = value; }
        }
        ///<summary>
        ///员工居住国家
        ///</summary>
        public String Country
        {
            get { return _Country; }
            set { _Country = value; }
        }
        ///<summary>
        ///员工工作地址
        ///</summary>
        public String WorkAddress
        {
            get { return _WorkAddress; }
            set { _WorkAddress = value; }
        }
        ///<summary>
        ///员工工作城市
        ///</summary>
        public String WorkCity
        {
            get { return _WorkCity; }
            set { _WorkCity = value; }
        }
        ///<summary>
        ///员工工作省份
        ///</summary>
        public String WorkProvince
        {
            get { return _WorkProvince; }
            set { _WorkProvince = value; }
        }
        ///<summary>
        ///员工工作国家
        ///</summary>
        public String WorkCountry
        {
            get { return _WorkCountry; }
            set { _WorkCountry = value; }
        }
        ///<summary>
        ///员工内部电子邮件地址
        ///</summary>
        public String InternalEmail
        {
            get { return _InternalEmail; }
            set { _InternalEmail = value; }
        }
        ///<summary>
        ///员工即时通信帐户
        ///</summary>
        public String IM
        {
            get { return _IM; }
            set { _IM = value; }
        }
        ///<summary>
        ///员工状态
        ///</summary>
        public Int32 Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        /// <summary>
        /// 创建者
        /// </summary>
        public int Creator
        {
            get { return _Creator; }
            set { _Creator = value; }
        }

        /// <summary>
        /// 最后修改者
        /// </summary>
        public int LastModifier
        {
            get { return _LastModifier; }
            set { _LastModifier = value; }
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime
        {
            get { return _CreatedTime; }
            set { _CreatedTime = value; }
        }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime LastModifiedTime
        {
            get { return _LastModifiedTime; }
            set { _LastModifiedTime = value; }
        }

        /// <summary>
        /// 创建人用户名
        /// </summary>
        public string CreatorName
        {
            get { return _CreatorName; }
            set { _CreatorName = value; }
        }

        /// <summary>
        /// 最后修改人用户名
        /// </summary>
        public string LastModifierName
        {
            get { return _LastModifierName; }
            set { _LastModifierName = value; }
        }

        /// <summary>
        /// 用户安全Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 数据记录版本
        /// </summary>
        public Byte[] RowVersion
        {
            get { return _RowVersion; }
            set { _RowVersion = value; }
        }

        /// <summary>
        /// 简历
        /// </summary>
        public string Resume
        {
            get { return _Resume; }
            set { _Resume = value; }
        }


        /// <summary>
        /// 是否外藉员工
        /// </summary>
        public bool IsForeign { get; set; }

        /// <summary>
        /// 是否有劳工证
        /// </summary>
        public bool IsCertification { get; set; }

        /// <summary>
        /// 工资月数
        /// </summary>
        public int WageMonths { get; set; }

        /// <summary>
        /// IP电话号码
        /// </summary>
        public string IPCode { get; set; }
    }
}
