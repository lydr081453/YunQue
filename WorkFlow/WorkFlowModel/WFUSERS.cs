
using System;
using System.Collections;


namespace WorkFlow.Model
{
    #region WFUSERS


    public class WFUSERS
    {
        #region Member Variables

        protected long _id;
        protected string _uSERNAME;
        protected DateTime _lONGINDATE;
        protected string _pASSWORD;
        protected string _username2;


        #endregion

        #region Constructors

        public WFUSERS() { }

        public WFUSERS(string uSERNAME, DateTime lONGINDATE, string pASSWORD)
        {
            this._uSERNAME = uSERNAME;
            this._lONGINDATE = lONGINDATE;
            this._pASSWORD = pASSWORD;
        }

        #endregion

        #region Public Properties

        public long Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string USERNAME
        {
            get { return _uSERNAME; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for USERNAME", value, value.ToString());
                _uSERNAME = value;
            }
        }

        public string USERNAME2
        {
            get { return _username2; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for USERNAME", value, value.ToString());
                _username2 = value;
            }
        }

        public DateTime LONGINDATE
        {
            get { return _lONGINDATE; }
            set { _lONGINDATE = value; }
        }

        public string PASSWORD
        {
            get { return _pASSWORD; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for PASSWORD", value, value.ToString());
                _pASSWORD = value;
            }
        }

        #endregion

   
    }

    #endregion
}
