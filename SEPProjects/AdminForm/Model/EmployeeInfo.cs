using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminForm.Model
{
    public class EmployeeInfo
    {
        public EmployeeInfo()
        { }


        // 摘要:
        //     员工家庭住址
        public string Address { get; set; }
        //
        // 摘要:
        //     地址2
        public string Address2 { get; set; }
        //
        // 摘要:
        //     档案信息是否完整
        public bool ArchiveInfoOK { get; set; }
        //
        // 摘要:
        //     基本信息是否完整
        public bool BaseInfoOK { get; set; }
        //
        // 摘要:
        //     生日
        public DateTime Birthday { get; set; }
        //
        // 摘要:
        //     出生地
        public string BirthPlace { get; set; }
        //
        // 摘要:
        //     员工居住城市
        public string City { get; set; }
        //
        // 摘要:
        //     城市2
        public string City2 { get; set; }
        //
        // 摘要:
        //     员工编号
        public string Code { get; set; }
        //
        // 摘要:
        //     合同是否完整
        public bool ContractInfoOK { get; set; }
        //
        // 摘要:
        //     员工居住国家
        public string Country { get; set; }
        //
        // 摘要:
        //     国家2
        public string Country2 { get; set; }
        //
        // 摘要:
        //     创建时间
        public DateTime CreatedTime { get; set; }
        //
        // 摘要:
        //     创建者
        public int Creator { get; set; }
        //
        // 摘要:
        //     创建人用户名
        public string CreatorName { get; set; }
        //
        // 摘要:
        //     学位
        public string Degree { get; set; }
        //
        // 摘要:
        //     6个月内患过何种疾病
        public string DiseaseInSixMonths { get; set; }
        //
        // 摘要:
        //     6个月内所患疾病信息
        public string DiseaseInSixMonthsInfo { get; set; }
        //
        // 摘要:
        //     现居住地
        public string DomicilePlace { get; set; }
        //
        // 摘要:
        //     学历
        public string Education { get; set; }
        //
        // 摘要:
        //     用户安全Email
        public string Email { get; set; }
        //
        // 摘要:
        //     紧急联络人
        public string EmergencyContact { get; set; }
        //
        // 摘要:
        //     紧急联络电话
        public string EmergencyContactPhone { get; set; }
        //
        // 摘要:
        //     员工传真号码
        public string Fax { get; set; }
        //
        // 摘要:
        //     中文名字
        public string FirstNameCN { get; set; }
        //
        // 摘要:
        //     英文名字
        public string FirstNameEN { get; set; }
        //
        // 摘要:
        //     中文全名
        public string FullNameCN { get; set; }
        //
        // 摘要:
        //     英文命名
        public string FullNameEN { get; set; }

        //
        // 摘要:
        //     毕业时间
        public DateTime GraduatedDate { get; set; }
        //
        // 摘要:
        //     毕业院校
        public string GraduateFrom { get; set; }
        //
        // 摘要:
        //     健康状况
        public string Health { get; set; }
        //
        // 摘要:
        //     家庭电话
        public string HomePhone { get; set; }
        //
        // 摘要:
        //     身份证件号码
        public string IDNumber { get; set; }
        //
        // 摘要:
        //     员工即时通信帐户
        public string IM { get; set; }
        //
        // 摘要:
        //     社会保险信息是否完整
        public bool InsuranceInfoOK { get; set; }
        //
        // 摘要:
        //     员工内部电子邮件地址
        public string InternalEmail { get; set; }
        //
        // 摘要:
        //     IP电话号码
        public string IPCode { get; set; }
        //
        // 摘要:
        //     是否有劳工证
        public bool IsCertification { get; set; }
        //
        // 摘要:
        //     是否外藉员工
        public bool IsForeign { get; set; }
        //
        // 摘要:
        //     最后修改时间
        public DateTime LastModifiedTime { get; set; }
        //
        // 摘要:
        //     最后修改者
        public int LastModifier { get; set; }
        //
        // 摘要:
        //     最后修改人用户名
        public string LastModifierName { get; set; }
        //
        // 摘要:
        //     中文姓氏
        public string LastNameCN { get; set; }
        //
        // 摘要:
        //     英文姓氏
        public string LastNameEN { get; set; }
        //
        // 摘要:
        //     专业/主修课程
        public string Major { get; set; }

        //
        // 摘要:
        //     备注事项
        public string Memo { get; set; }
        //
        // 摘要:
        //     移动电话
        public string MobilePhone { get; set; }
        //
        // 摘要:
        //     电话1
        public string Phone1 { get; set; }
        //
        // 摘要:
        //     电话2
        public string Phone2 { get; set; }
        //
        // 摘要:
        //     照片(图片路径)
        public string Photo { get; set; }
        //
        // 摘要:
        //     邮政编码
        public string PostCode { get; set; }
        //
        // 摘要:
        //     邮政编码2
        public string PostCode2 { get; set; }
        //
        // 摘要:
        //     员工居住省份
        public string Province { get; set; }
        //
        // 摘要:
        //     省份2
        public string Province2 { get; set; }
        //
        // 摘要:
        //     简历
        public string Resume { get; set; }
        //
        // 摘要:
        //     数据记录版本
        public byte[] RowVersion { get; set; }
        //
        // 摘要:
        //     员工状态
        public int Status { get; set; }
        //
        // 摘要:
        //     本年薪资
        public string ThisYearSalary { get; set; }
        //
        // 摘要:
        //     员工类型ID（全职、兼职）
        public int TypeID { get; set; }
        //
        // 摘要:
        //     员工类型名称（全职、兼职）
        public string TypeName { get; set; }
        //
        // 摘要:
        //     用记ID
        public int UserID { get; set; }
        //
        // 摘要:
        //     用户名
        public string Username { get; set; }
        //
        // 摘要:
        //     工资月数
        public int WageMonths { get; set; }
        //
        // 摘要:
        //     员工工作地址
        public string WorkAddress { get; set; }
        //
        // 摘要:
        //     员工工作城市
        public string WorkCity { get; set; }
        //
        // 摘要:
        //     员工工作国家
        public string WorkCountry { get; set; }
        //
        // 摘要:
        //     工作经历
        public string WorkExperience { get; set; }
        //
        // 摘要:
        //     工作地邮政编码
        public string WorkPostCode { get; set; }
        //
        // 摘要:
        //     员工工作省份
        public string WorkProvince { get; set; }
        //
        // 摘要:
        //     工作特长
        public string WorkSpecialty { get; set; }

        #region Model

        private bool _isSendMail = false;
        public bool IsSendMail
        {
            get { return _isSendMail; }
            set { _isSendMail = value; }
        }

        private string _commonName;
        public string CommonName
        {
            get { return _commonName; }
            set { _commonName = value; }
        }

        public int DimissionStatus { get; set; }

        private string _privateemail;
        /// <summary>
        /// 员工个人邮箱。
        /// </summary>
        public string PrivateEmail
        {
            set { _privateemail = value; }
            get { return _privateemail; }
        }

        private bool _ownedpc;
        private int _offerlettertemplate;
        /// <summary>
        /// 自备电脑
        /// </summary>
        public bool OwnedPC
        {
            set { _ownedpc = value; }
            get { return _ownedpc; }
        }
        /// <summary>
        /// 入职通知模板
        /// </summary>
        public int OfferLetterTemplate
        {
            set { _offerlettertemplate = value; }
            get { return _offerlettertemplate; }
        }
        private int _offerLetterSender;
        public int OfferLetterSender
        {
            get { return _offerLetterSender; }
            set { _offerLetterSender = value; }
        }
        /// <summary>
        /// Offer Letter 发送时间。
        /// </summary>
        private DateTime? _offerLetterSendTime;
        public DateTime? OfferLetterSendTime
        {
            get { return _offerLetterSendTime; }
            set { _offerLetterSendTime = value; }
        }
        /// <summary>
        /// 是否是应届毕业生。
        /// </summary>
        private bool _isExamen;
        public bool IsExamen
        {
            get { return _isExamen; }
            set { _isExamen = value; }
        }
        /// <summary>
        /// 员工社会工龄。
        /// </summary>
        private int _seniority;
        public int Seniority
        {
            get { return _seniority; }
            set { _seniority = value; }
        }
        /// <summary>
        /// 工资
        /// </summary>
        private decimal _pay;
        public decimal Pay
        {
            get { return _pay; }
            set { _pay = value; }
        }
        private decimal _performance;
        public decimal Performance
        {
            get { return _performance; }
            set { _performance = value; }
        }

        public string MateName { get; set; }
        public string AdrressNow { get; set; }
        public string PostCodeNow { get; set; }
        public string FamillyDesc { get; set; }

        public string Residence { get; set; }

        public string Political { get; set; }
        public string Nation { get; set; }
        public bool HasChild { get; set; }
        public int EmpProperty { get; set; }

        public string Appearance { get; set; }
        public string Quality { get; set; }
        public string Know { get; set; }
        public string Equal { get; set; }
        public string Motivation { get; set; }
        public string FourD { get; set; }
        public string EQ { get; set; }
        public DateTime? WorkBegin { get; set; }
        #endregion Model
    }
}
