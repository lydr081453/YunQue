using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.Entity
{
   public  class ProjectHistLogInfo
    {
       public ProjectHistLogInfo()
		{}
		#region Model
		private int _logid;
		private int? _projectid;
		private string _remark;
		private DateTime? _logdate;
		private int? _updateuserid;
		private string _updateuserempname;
		/// <summary>
		/// 
		/// </summary>
		public int LogID
		{
			set{ _logid=value;}
			get{return _logid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ProjectID
		{
			set{ _projectid=value;}
			get{return _projectid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? LogDate
		{
			set{ _logdate=value;}
			get{return _logdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? UpdateUserID
		{
			set{ _updateuserid=value;}
			get{return _updateuserid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UpdateUserEmpName
		{
			set{ _updateuserempname=value;}
			get{return _updateuserempname;}
		}
		#endregion Model
    }
}
