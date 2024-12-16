
using System;
using System.Collections;
using System.Collections.Generic;

namespace WorkFlow.Model
{
    #region WORKITEMS

    /// <summary>
    /// WORKITEMS object for NHibernate mapped table 'WORKITEMS'.
    /// </summary>
    public class WORKITEMS 
    {
        #region Member Variables

        protected long _id;
        protected long _pROCESSID;
        protected string _pROCESSNAME;
        protected long _tASKID;
        protected string _tASKDISPLAYNAME;
        protected string _tASKINSTRUCTIONS;
        protected long _pARTICIPANTID;
        protected string _pARTICIPANTNAME, _pARTICIPANTNAME2;
        protected string _tASKNAME;
        protected DateTime _sTARTDATE;
        protected DateTime _eNDDATE;
        protected int _sTATE;
        protected long _cHILDINSTANCEID;
        protected short _pARTICIPANTTYPE;
        protected PROCESSINSTANCES _pROCESSINSTANCES;
        protected IList _wORKITEMEXTENDs;
        protected IList _wORKITEMDATAs;
        protected IList<TASKFINISHER> _tASKFINISHERs;
        protected IList<PREVIOUSWORKITEMS> _pREVIOUSWORKITEMSs;
        protected int _remindercount;
        protected int _serialpointer;
        protected int _userdrivenflag;
        protected int _userdrivenpersonid;
        protected long _instanceid;
        protected string _sytaskname;
        protected string _actiontakenname;
        protected string _submitdisplayname;
        protected int _tasktype;
        private string roleid;

        public string RoleID
        {
            get { return roleid; }
            set { roleid = value; }
        }
        #endregion

        #region Constructors

        public WORKITEMS() { }

        public WORKITEMS(long pROCESSID, string pROCESSNAME, long tASKID, string tASKDISPLAYNAME, string tASKINSTRUCTIONS, long pARTICIPANTID, string pARTICIPANTNAME, string pARTICIPANTNAME2, string tASKNAME, DateTime sTARTDATE, DateTime eNDDATE, int sTATE, long cHILDINSTANCEID, short pARTICIPANTTYPE, PROCESSINSTANCES pROCESSINSTANCES)
        {
            this._pROCESSID = pROCESSID;
            this._pROCESSNAME = pROCESSNAME;
            this._tASKID = tASKID;
            this._tASKDISPLAYNAME = tASKDISPLAYNAME;
            this._tASKINSTRUCTIONS = tASKINSTRUCTIONS;
            this._pARTICIPANTID = pARTICIPANTID;
            this._pARTICIPANTNAME = pARTICIPANTNAME;
            this._pARTICIPANTNAME2 = pARTICIPANTNAME2;
            this._tASKNAME = tASKNAME;
            this._sTARTDATE = sTARTDATE;
            this._eNDDATE = eNDDATE;
            this._sTATE = sTATE;
            this._cHILDINSTANCEID = cHILDINSTANCEID;
            this._pARTICIPANTTYPE = pARTICIPANTTYPE;
            this._pROCESSINSTANCES = pROCESSINSTANCES;
        }

        #endregion

        #region Public Properties

        public int TASKTYPE
        {
            get { return _tasktype; }
            set { _tasktype = value; }
        }
        public long INSTANCEID
        {
            get { return _instanceid; }
            set { _instanceid = value; }
        }

        public string SubmitDisplayName
        {
            get { return _submitdisplayname; }
            set { _submitdisplayname = value; }
        }
        public string SyTaskName
        {
            get { return _sytaskname; }
            set { _sytaskname = value; }
        }

        public string ActionTackenName
        {
            get { return _actiontakenname; }
            set { _actiontakenname = value; }
        }
        public int ReminderCount
        {
            get { return _remindercount; }
            set { _remindercount = value; }
        }

        public int UserDrivenFlag
        {
            get { return _userdrivenflag; }
            set { _userdrivenflag = value; }
        }

        public int UserDrivenPersonID
        {
            get { return _userdrivenpersonid; }
            set { _userdrivenpersonid = value; }
        }

        public long Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public long PROCESSID
        {
            get { return _pROCESSID; }
            set { _pROCESSID = value; }
        }

        public string PROCESSNAME
        {
            get { return _pROCESSNAME; }
            set
            {
                if (value != null && value.Length > 255)
                    throw new ArgumentOutOfRangeException("Invalid value for PROCESSNAME", value, value.ToString());
                _pROCESSNAME = value;
            }
        }

        public long TASKID
        {
            get { return _tASKID; }
            set { _tASKID = value; }
        }

        public string TASKDISPLAYNAME
        {
            get { return _tASKDISPLAYNAME; }
            set
            {
                if (value != null && value.Length > 255)
                    throw new ArgumentOutOfRangeException("Invalid value for TASKDISPLAYNAME", value, value.ToString());
                _tASKDISPLAYNAME = value;
            }
        }

        public string TASKINSTRUCTIONS
        {
            get { return _tASKINSTRUCTIONS; }
            set
            {
                if (value != null && value.Length > 255)
                    throw new ArgumentOutOfRangeException("Invalid value for TASKINSTRUCTIONS", value, value.ToString());
                _tASKINSTRUCTIONS = value;
            }
        }

        public long PARTICIPANTID
        {
            get { return _pARTICIPANTID; }
            set { _pARTICIPANTID = value; }
        }

        public string PARTICIPANTNAME
        {
            get { return _pARTICIPANTNAME; }
            set
            {
                if (value != null && value.Length > 255)
                    throw new ArgumentOutOfRangeException("Invalid value for PARTICIPANTNAME", value, value.ToString());
                _pARTICIPANTNAME = value;
            }
        }

        public int SERIALPOINTER
        {
            get { return _serialpointer; }
            set { _serialpointer = value; }
        }
        public string PARTICIPANTNAME2
        {
            get { return _pARTICIPANTNAME2; }
            set
            {
                if (value != null && value.Length > 255)
                    throw new ArgumentOutOfRangeException("Invalid value for PARTICIPANTNAME", value, value.ToString());
                _pARTICIPANTNAME2 = value;
            }
        }


        public string TASKNAME
        {
            get { return _tASKNAME; }
            set
            {
                if (value != null && value.Length > 255)
                    throw new ArgumentOutOfRangeException("Invalid value for TASKNAME", value, value.ToString());
                _tASKNAME = value;
            }
        }

        public DateTime STARTDATE
        {
            get { return _sTARTDATE; }
            set { _sTARTDATE = value; }
        }

        public DateTime ENDDATE
        {
            get { return _eNDDATE; }
            set { _eNDDATE = value; }
        }

        public int STATE
        {
            get { return _sTATE; }
            set { _sTATE = value; }
        }

        public long CHILDINSTANCEID
        {
            get { return _cHILDINSTANCEID; }
            set { _cHILDINSTANCEID = value; }
        }

        public short PARTICIPANTTYPE
        {
            get { return _pARTICIPANTTYPE; }
            set { _pARTICIPANTTYPE = value; }
        }

        public PROCESSINSTANCES PROCESSINSTANCES
        {
            get { return _pROCESSINSTANCES; }
            set { _pROCESSINSTANCES = value; }
        }

        public IList WORKITEMEXTENDs
        {
            get
            {
                if (_wORKITEMEXTENDs == null)
                {
                    _wORKITEMEXTENDs = new ArrayList();
                }
                return _wORKITEMEXTENDs;
            }
            set { _wORKITEMEXTENDs = value; }
        }

        public IList WORKITEMDATAs
        {
            get
            {
                if (_wORKITEMDATAs == null)
                {
                    _wORKITEMDATAs = new ArrayList();
                }
                return _wORKITEMDATAs;
            }
            set { _wORKITEMDATAs = value; }
        }

        public IList<TASKFINISHER> TASKFINISHERs
        {
            get
            {
                if (_tASKFINISHERs == null)
                {
                    _tASKFINISHERs = new List<TASKFINISHER>();
                }
                return _tASKFINISHERs;
            }
            set { _tASKFINISHERs = value; }
        }

        public IList<PREVIOUSWORKITEMS> PREVIOUSWORKITEMSs
        {
            get
            {
                if (_pREVIOUSWORKITEMSs == null)
                {
                    _pREVIOUSWORKITEMSs = new List<PREVIOUSWORKITEMS>();
                }
                return _pREVIOUSWORKITEMSs;
            }
            set { _pREVIOUSWORKITEMSs = value; }
        }

        #endregion

     
    }

    #endregion
}
