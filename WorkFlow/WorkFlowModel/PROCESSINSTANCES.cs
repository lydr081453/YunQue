
using System;
using System.Collections;


namespace WorkFlow.Model
{
    #region PROCESSINSTANCES

    /// <summary>
    /// PROCESSINSTANCES object for NHibernate mapped table 'PROCESSINSTANCES'.
    /// </summary>
    public class PROCESSINSTANCES 
    {
        #region Member Variables

        protected long _id;
        protected long _pROCESSID;
        protected string _pROCESSNAME;
        protected long _iNITIATORID;
        protected string _iNITIATORNAME;
        protected DateTime _sTARTDATE;
        protected DateTime _eNDDATE;
        protected long _pARENTPROCESSINSTANCEID;
        protected long _aCTIVEWOEKITEMID;
        protected long _nOTIFYPARENTPROCESS;
        protected string _pARENTADDRESS;
        protected long _pROCESSINSTANCESTATE;
        protected long _aCTIVEPERSONID;
        protected IList _pROCESSEXTENDs;
        protected IList _wORKITEMSs;
        protected string _username2;


        #endregion

        #region Constructors

        public PROCESSINSTANCES() { }

        public PROCESSINSTANCES(long pROCESSID, string pROCESSNAME, long iNITIATORID, string iNITIATORNAME, DateTime sTARTDATE, DateTime eNDDATE, long pARENTPROCESSINSTANCEID, long aCTIVEWOEKITEMID, long nOTIFYPARENTPROCESS, string pARENTADDRESS, long pROCESSINSTANCESTATE, long aCTIVEPERSONID)
        {
            this._pROCESSID = pROCESSID;
            this._pROCESSNAME = pROCESSNAME;
            this._iNITIATORID = iNITIATORID;
            this._iNITIATORNAME = iNITIATORNAME;
            this._sTARTDATE = sTARTDATE;
            this._eNDDATE = eNDDATE;
            this._pARENTPROCESSINSTANCEID = pARENTPROCESSINSTANCEID;
            this._aCTIVEWOEKITEMID = aCTIVEWOEKITEMID;
            this._nOTIFYPARENTPROCESS = nOTIFYPARENTPROCESS;
            this._pARENTADDRESS = pARENTADDRESS;
            this._pROCESSINSTANCESTATE = pROCESSINSTANCESTATE;
            this._aCTIVEPERSONID = aCTIVEPERSONID;
        }

        #endregion

        #region Public Properties

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

        public long INITIATORID
        {
            get { return _iNITIATORID; }
            set { _iNITIATORID = value; }
        }

        public string INITIATORNAME
        {
            get { return _iNITIATORNAME; }
            set
            {
                if (value != null && value.Length > 255)
                    throw new ArgumentOutOfRangeException("Invalid value for INITIATORNAME", value, value.ToString());
                _iNITIATORNAME = value;
            }
        }

        public string INITIATORNAME2
        {
            get { return _username2; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for INITIATORNAME", value, value.ToString());
                _username2 = value;
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

        public long PARENTPROCESSINSTANCEID
        {
            get { return _pARENTPROCESSINSTANCEID; }
            set { _pARENTPROCESSINSTANCEID = value; }
        }

        public long ACTIVEWOEKITEMID
        {
            get { return _aCTIVEWOEKITEMID; }
            set { _aCTIVEWOEKITEMID = value; }
        }

        public long NOTIFYPARENTPROCESS
        {
            get { return _nOTIFYPARENTPROCESS; }
            set { _nOTIFYPARENTPROCESS = value; }
        }

        public string PARENTADDRESS
        {
            get { return _pARENTADDRESS; }
            set
            {
                if (value != null && value.Length > 255)
                    throw new ArgumentOutOfRangeException("Invalid value for PARENTADDRESS", value, value.ToString());
                _pARENTADDRESS = value;
            }
        }

        public long PROCESSINSTANCESTATE
        {
            get { return _pROCESSINSTANCESTATE; }
            set { _pROCESSINSTANCESTATE = value; }
        }

        public long ACTIVEPERSONID
        {
            get { return _aCTIVEPERSONID; }
            set { _aCTIVEPERSONID = value; }
        }

        public IList PROCESSEXTENDs
        {
            get
            {
                if (_pROCESSEXTENDs == null)
                {
                    _pROCESSEXTENDs = new ArrayList();
                }
                return _pROCESSEXTENDs;
            }
            set { _pROCESSEXTENDs = value; }
        }

        public IList WORKITEMSs
        {
            get
            {
                if (_wORKITEMSs == null)
                {
                    _wORKITEMSs = new ArrayList();
                }
                return _wORKITEMSs;
            }
            set { _wORKITEMSs = value; }
        }

        #endregion

   
    }

    #endregion
}
