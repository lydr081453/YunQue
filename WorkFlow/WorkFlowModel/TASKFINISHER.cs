
using System;
using System.Collections;

namespace WorkFlow.Model
{

    /// <summary>
    /// TASKFINISHER object for NHibernate mapped table 'TASKFINISHER'.
    /// </summary>
    public class TASKFINISHER
    {
        #region Member Variables

        protected long _id;
        protected long _pARTICIPANTID;
        protected long _vIEWTASKTYPE;
        protected string _pARTICIPANTNAME;
        protected int _vIEWTASKNUM;
        protected DateTime _lASTVIEWTIME;
        protected WORKITEMS _wORKITEMS;
       
        #endregion

        #region Constructors

        public TASKFINISHER() { }

        public TASKFINISHER(long pARTICIPANTID, long vIEWTASKTYPE, string pARTICIPANTNAME, int vIEWTASKNUM, DateTime lASTVIEWTIME, WORKITEMS wORKITEMS)
        {
            this._pARTICIPANTID = pARTICIPANTID;
            this._vIEWTASKTYPE = vIEWTASKTYPE;
            this._pARTICIPANTNAME = pARTICIPANTNAME;
            this._vIEWTASKNUM = vIEWTASKNUM;
            this._lASTVIEWTIME = lASTVIEWTIME;
            this._wORKITEMS = wORKITEMS;
        }

        #endregion

      

        public long Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public long PARTICIPANTID
        {
            get { return _pARTICIPANTID; }
            set { _pARTICIPANTID = value; }
        }

        public long VIEWTASKTYPE
        {
            get { return _vIEWTASKTYPE; }
            set { _vIEWTASKTYPE = value; }
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

        public int VIEWTASKNUM
        {
            get { return _vIEWTASKNUM; }
            set { _vIEWTASKNUM = value; }
        }

        public DateTime LASTVIEWTIME
        {
            get { return _lASTVIEWTIME; }
            set { _lASTVIEWTIME = value; }
        }

        public WORKITEMS WORKITEMS
        {
            get { return _wORKITEMS; }
            set { _wORKITEMS = value; }
        }

    
    }

}

