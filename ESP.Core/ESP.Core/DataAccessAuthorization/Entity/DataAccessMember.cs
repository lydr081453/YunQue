using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.DataAccessAuthorization.Entity
{
    /// <summary>
    /// 权限成员描述
    /// </summary>
    public class DataAccessMember
    {
        #region Model
        private int _dataaccessmemberid;
        private string _membername;
        private int _membertype;
        private string _memberdefinition;
        private string _memberservice;
        private DateTime _createtime;
        private int _creator;
        private string _creatorname;
        /// <summary>
        /// 权限成员序号
        /// </summary>
        public int DataAccessMemberID
        {
            set { _dataaccessmemberid = value; }
            get { return _dataaccessmemberid; }
        }
        /// <summary>
        /// 成员名称
        /// </summary>
        public string MemberName
        {
            set { _membername = value; }
            get { return _membername; }
        }
        /// <summary>
        /// 成员类型：（0）特殊个人，无法用逻辑详细描述的群体或角色，根据表单处理事件动态插入；（1）固定角色，关联系统角色表，此角色下的所有人均可进行操作；（2）单一组织，关联到组织机构表，此组织机构下人员均可操作；（3）审批树，此处可以查询WorkFlow表中的相应人员。（4）职位，职位大于某个数值的人员，需要配合人力资源系统进行设置。（5）职位级别，例如大于总监级别，在判定上应该大于总监级别【10】，某些非总监职位的人也拥有10或者10以上的级别
        /// </summary>
        public int MemberType
        {
            set { _membertype = value; }
            get { return _membertype; }
        }
        /// <summary>
        /// 成员标志，由于部门、角色或审批人列表的ID有可能会发生变化，因此直接写定这些ID会造成灵活性不足。我们将来需要实现一种描述方式来根据成员类型确定成员标志。目前我们可以将这种对应关系，写到一个配置文件中，在这里写入配置文件相应的配置节名称即可，根据这个名称去读取配置文件里面的值来确定成员标志
        /// </summary>
        public string MemberDefinition
        {
            set { _memberdefinition = value; }
            get { return _memberdefinition; }
        }
        /// <summary>
        /// 提供Member服务的类，可供反射实例化；需要实现IMemberService接口
        /// </summary>
        public string MemberService
        {
            set { _memberservice = value; }
            get { return _memberservice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Creator
        {
            set { _creator = value; }
            get { return _creator; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CreatorName
        {
            set { _creatorname = value; }
            get { return _creatorname; }
        }
        #endregion Model
    }
}
