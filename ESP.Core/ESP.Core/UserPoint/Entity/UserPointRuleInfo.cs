using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.UserPoint.Entity
{
    /// <summary>
    /// 
    /// </summary>
    public class UserPointRuleInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public UserPointRuleInfo()
		{}
		#region Model
		private int _ruleid;
		private string _rulename;
		private string _rulekey;
		private string _description;
		private int _points;
		/// <summary>
		/// 
		/// </summary>
		public int RuleID
		{
		set{ _ruleid=value;}
		get{return _ruleid;}
		}
		/// <summary>
		/// 积分规则的名称
		/// </summary>
		public string RuleName
		{
		set{ _rulename=value;}
		get{return _rulename;}
		}
		/// <summary>
		/// 积分规则的Key，用来在程序中起到标志作用（例如在XML中可以使用一个英文标识来说明这个积分设定，这样可以避免使用ID的数字和Name的中文来当作字段或属性的名称）
		/// </summary>
		public string RuleKey
		{
		set{ _rulekey=value;}
		get{return _rulekey;}
		}
		/// <summary>
		/// 积分规则的解释说明
		/// </summary>
		public string Description
		{
		set{ _description=value;}
		get{return _description;}
		}
		/// <summary>
		/// 分数设置
		/// </summary>
		public int Points
		{
		set{ _points=value;}
		get{return _points;}
		}
		#endregion Model
    }
}
