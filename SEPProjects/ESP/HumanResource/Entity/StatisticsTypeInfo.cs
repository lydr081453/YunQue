using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.HumanResource.Entity
{
    public class StatisticsTypeInfo
    {
        public StatisticsTypeInfo()
		{}
		#region Model
		private int _id;
		private string _statisticstypename;
		private string _typeid;
		private bool _isdeleted;
        private string _statisticstypevalue;
		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 导出条件名字
		/// </summary>
		public string StatisticsTypeName
		{
			set{ _statisticstypename=value;}
			get{return _statisticstypename;}
		}
		/// <summary>
		/// 导出条件类型（暂无用）
		/// </summary>
		public string TypeID
		{
			set{ _typeid=value;}
			get{return _typeid;}
		}
		/// <summary>
		/// 是否删除
		/// </summary>
		public bool IsDeleted
		{
			set{ _isdeleted=value;}
			get{return _isdeleted;}
		}
        /// <summary>
        /// 导出条件的字段
        /// </summary>
        public string StatisticsTypeValue
        {
            set { _statisticstypevalue = value; }
            get { return _statisticstypevalue; }
        }
		#endregion Model

    }
}
