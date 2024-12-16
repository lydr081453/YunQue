
    using System;
    using System.Collections;

    namespace WorkFlow.Model
    {
        #region PREVIOUSWORKITEMS

        /// <summary>
        /// PREVIOUSWORKITEMS object for NHibernate mapped table 'PREVIOUSWORKITEMS'.
        /// </summary>
        public class PREVIOUSWORKITEMS 
        {
            #region Member Variables

            protected long _id;
            protected long _pREVIOUWORKITEMID;
            protected WORKITEMS _wORKITEMS;
            protected long _pARTICIPANTID;
            protected string _pARTICIPANTNAME, _pARTICIPANTNAME2;
            protected string _tASKNAME;
            protected string _instanceid;
            #endregion

            #region Constructors

            public PREVIOUSWORKITEMS() { }

            public PREVIOUSWORKITEMS(long pREVIOUWORKITEMID, WORKITEMS wORKITEMS)
            {
                this._pREVIOUWORKITEMID = pREVIOUWORKITEMID;
                this._wORKITEMS = wORKITEMS;
            }

            #endregion

            #region Public Properties

            public long PREVIOUSWORKITERMID
            {
                get { return _id; }
                set { _id = value; }
            }

            public string INSTANCEID
            {
                get { return _instanceid; }
                set { _instanceid = value; }
            }
            public string PREVIOUSTASKNAME
            {
                get { return _tASKNAME; }
                set
                {
                    if (value != null && value.Length > 255)
                        throw new ArgumentOutOfRangeException("Invalid value for TASKNAME", value, value.ToString());
                    _tASKNAME = value;
                }
            }

            public long PREPARTICIPANTID
            {
                get { return _pARTICIPANTID; }
                set { _pARTICIPANTID = value; }
            }

            public string PREPARTICIPANTNAME
            {
                get { return _pARTICIPANTNAME; }
                set
                {
                    if (value != null && value.Length > 255)
                        throw new ArgumentOutOfRangeException("Invalid value for PARTICIPANTNAME", value, value.ToString());
                    _pARTICIPANTNAME = value;
                }
            }

            public string PREPARTICIPANTNAME2
            {
                get { return _pARTICIPANTNAME2; }
                set
                {
                    if (value != null && value.Length > 255)
                        throw new ArgumentOutOfRangeException("Invalid value for PARTICIPANTNAME", value, value.ToString());
                    _pARTICIPANTNAME2 = value;
                }
            }

            public long WORKITEMID
            {
                get { return _pREVIOUWORKITEMID; }
                set { _pREVIOUWORKITEMID = value; }
            }

            public WORKITEMS WORKITEMS
            {
                get { return _wORKITEMS; }
                set { _wORKITEMS = value; }
            }

          
            #endregion

        }

        #endregion
    }
