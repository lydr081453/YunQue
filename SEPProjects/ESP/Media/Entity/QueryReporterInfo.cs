using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace ESP.Media.Entity
{
    [Serializable]
    public class QueryReporterInfo
    {
        public QueryReporterInfo() { }
        public QueryReporterInfo(DataRow dr)
        {
            if (dr["reporterid"] != DBNull.Value)
            {
                this.reporterid = Convert.ToInt32(dr["reporterid"]);
            }
            if (dr["mediaitemid"] != DBNull.Value)
            {
                this._mediaid = Convert.ToInt32(dr["mediaitemid"]);
            }

            if (dr["reportername"] != DBNull.Value)
            {
                this.reportername = dr["reportername"].ToString().Trim();
            }
            if (dr["sex"] != DBNull.Value)
            {
                this.sex = dr["sex"].ToString().Trim();
            }
            if (dr["reporterposition"] != DBNull.Value)
            {
                this.reporterposition = dr["reporterposition"].ToString().Trim();
            }
            if (dr["tel"] != DBNull.Value)
            {
                this.tel = dr["tel"].ToString().Trim();
            }
            if (dr["mobile"] != DBNull.Value)
            {
                this.mobile = dr["mobile"].ToString().Trim();
            }
            if (dr["email"] != DBNull.Value)
            {
                this.email = dr["email"].ToString().Trim();
            }
            if (dr["responsibledomain"] != DBNull.Value)
            {
                this.responsibledomain = dr["responsibledomain"].ToString().Trim();
            }
            if (dr["topicproperty"] != DBNull.Value)
            {
                this.topicproperty = dr["topicproperty"].ToString().Trim();
            }
            if (dr["mediatype"] != DBNull.Value && !string.IsNullOrEmpty(dr["mediatype"].ToString()))
            {

                this.mediatype = dr["mediatype"].ToString().Trim();
            }
            if (dr["readersort"] != DBNull.Value)
            {
                this.readersort = dr["readersort"].ToString().Trim();
            }
            if (dr["reporterlevel"] != DBNull.Value && !string.IsNullOrEmpty(dr["reporterlevel"].ToString()))
            {
                this.reporterlevel = Convert.ToInt32(dr["reporterlevel"]);
            }
            if (dr["othermessagesoftware"] != DBNull.Value)
            {
                this.othermessagesoftware = dr["othermessagesoftware"].ToString().Trim();
            }
            if (dr["remark"] != DBNull.Value)
            {
                this.remark = dr["remark"].ToString().Trim();
            }
            if (dr.Table.Columns.Contains("CityName") && dr["CityName"] != DBNull.Value)
            {
                this._cityname = dr["CityName"].ToString().Trim();
            }
            if (dr.Table.Columns.Contains("CardNumber") && dr["CardNumber"] != DBNull.Value)
            {
                this._cardnumber = dr["CardNumber"].ToString().Trim();
            }
            if (dr.Table.Columns.Contains("tel") && dr["tel"] != DBNull.Value)
            {
                this.tel = dr["tel"].ToString().Trim();
            }
            if (dr.Table.Columns.Contains("usualmobile") && dr["usualmobile"] != DBNull.Value)
            {
                this.mobile = dr["usualmobile"].ToString().Trim();
            }
            // bankname,a.PayType,a.bankacountname
            if (dr.Table.Columns.Contains("bankname") && dr["bankname"] != DBNull.Value)
            {
                this._bankname = dr["bankname"].ToString().Trim();
            }
            if (dr.Table.Columns.Contains("PayType") && dr["PayType"] != DBNull.Value)
            {
                this._paytype = dr["PayType"].ToString().Trim();
            }
            if (dr.Table.Columns.Contains("bankacountname") && dr["bankacountname"] != DBNull.Value)
            {
                this._bankaccount = dr["bankacountname"].ToString().Trim();
            }
            if (dr.Table.Columns.Contains("bankcardname") && dr["bankcardname"] != DBNull.Value)
            {
                this._bankcardname = dr["bankcardname"].ToString().Trim();
            }
        }

        int reporterid;

        public int Reporterid
        {
            get { return reporterid; }
            set { reporterid = value; }
        }
        string reportername;
        private int _mediaid;
        public int MediaID
        {
            get { return _mediaid; }
            set { _mediaid = value; }
        }
        public string Reportername
        {
            get { return reportername; }
            set { reportername = value; }
        }
        string sex;

        public string Sex
        {
            get { return sex; }
            set { sex = value; }
        }
        string reporterposition;

        public string Reporterposition
        {
            get { return reporterposition; }
            set { reporterposition = value; }
        }
        string tel;

        public string Tel
        {
            get { return tel; }
            set { tel = value; }
        }
        string mobile;

        public string Mobile
        {
            get { return mobile; }
            set { mobile = value; }
        }
        string email;

        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        string responsibledomain;

        public string Responsibledomain
        {
            get { return responsibledomain; }
            set { responsibledomain = value; }
        }
        string medianame;

        public string Medianame
        {
            get { return medianame; }
            set { medianame = value; }
        }
        string topicproperty;

        public string Topicproperty
        {
            get { return topicproperty; }
            set { topicproperty = value; }
        }
        string mediatype;

        public string Mediatype
        {
            get { return mediatype; }
            set { mediatype = value; }
        }
        string readersort;

        public string Readersort
        {
            get { return readersort; }
            set { readersort = value; }
        }
        int reporterlevel;

        public int Reporterlevel
        {
            get { return reporterlevel; }
            set { reporterlevel = value; }
        }
        string othermessagesoftware;

        public string Othermessagesoftware
        {
            get { return othermessagesoftware; }
            set { othermessagesoftware = value; }
        }
        string remark;

        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }

        private string _cityname;
        public string CityName
        {
            get { return _cityname; }
            set { _cityname = value; }
        }

        public string _cardnumber;
        public string CardNumber
        {
            get { return _cardnumber; }
            set { _cardnumber = value; }
        }
        // bankname,a.PayType,a.bankacountname
        private string _bankname;
        public string BankName
        {
            get { return _bankname; }
            set { _bankname = value; }
        }

        private string _paytype;
        public string PayType
        {
            get { return _paytype; }
            set { _paytype = value; }
        }

        private string _bankaccount;
        public string BankAccountName
        {
            get { return _bankaccount; }
            set { _bankaccount = value; }
        }
        private string _bankcardname;

        public string BankCardName
        {
            get { return _bankcardname; }
            set { _bankcardname = value; }
        }

    }
}
