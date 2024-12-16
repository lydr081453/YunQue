using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.Entity
{
    public class PNBatchRelationInfo
    {
        public PNBatchRelationInfo()
		{}
		#region Model
		private int _batchrelationid;
		private int? _batchid;
		private int? _returnid;
		/// <summary>
		/// 
		/// </summary>
		public int BatchRelationID
		{
			set{ _batchrelationid=value;}
			get{return _batchrelationid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? BatchID
		{
			set{ _batchid=value;}
			get{return _batchid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ReturnID
		{
			set{ _returnid=value;}
			get{return _returnid;}
		}
		#endregion Model

    }
}
