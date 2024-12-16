using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ESP.Media.Entity
{
    [Serializable]
    public class QueryMediaItemInfo
    {
        public QueryMediaItemInfo()
        { }
        public QueryMediaItemInfo(DataRow dr)
        {
            if (dr["mediaitemid"] != DBNull.Value)//媒体ID
            {
                this.mediaitemid = Convert.ToInt32(dr["mediaitemid"]);
            }

            if (dr["mediacname"] != DBNull.Value)//媒体中文名称
            {
                this.mediacname = (dr["mediacname"]).ToString();
            }
            else
            {
                this.mediacname = string.Empty;
            }
            if (dr["mediaename"] != DBNull.Value)//媒体英文名称
            {
                this.mediaename = (dr["mediaename"]).ToString();
            }
            else
            {
                this.mediaename = string.Empty;
            }
            if (dr["cshortname"] != DBNull.Value)//媒体中文简称
            {
                this.cshortname = (dr["cshortname"]).ToString();
            }
            else
            {
                this.cshortname = string.Empty;
            }
            if (dr["eshortname"] != DBNull.Value)//媒体英文简称
            {
                this.eshortname = (dr["eshortname"]).ToString();
            }
            else
            {
                this.eshortname = string.Empty;
            }
            if (dr["mediaitemtype"] != DBNull.Value)//媒体类型1平面2网络3电视4广播
            {
                this.mediaitemtype = Convert.ToInt32(dr["mediaitemtype"]);
            }
            if (dr["currentversion"] != DBNull.Value)//媒体当前版本
            {
                this.currentversion = Convert.ToInt32(dr["currentversion"]);
            }
            if (dr["status"] != DBNull.Value)//状态
            {
                this.status = Convert.ToInt32(dr["status"]);
            }
            if (dr["createdbyuserid"] != DBNull.Value)//创建用户id
            {
                this.createdbyuserid = Convert.ToInt32(dr["createdbyuserid"]);
            }

            if (dr["createddate"] != DBNull.Value)//创建日期
            {
                this.createddate = (dr["createddate"]).ToString();
            }
            else
            {
                this.createddate = string.Empty;
            }
            if (dr["lastmodifiedbyuserid"] != DBNull.Value)//修改id
            {
                this.lastmodifiedbyuserid = Convert.ToInt32(dr["lastmodifiedbyuserid"]);
            }
            if (dr["lastmodifieddate"] != DBNull.Value)//修改日期
            {
                this.lastmodifieddate = (dr["lastmodifieddate"]).ToString();
            }
            else
            {
                this.lastmodifieddate = string.Empty;
            }

            if (dr["mediumsort"] != DBNull.Value)//形态属性
            {
                this.mediumsort = (dr["mediumsort"]).ToString();
            }
            else
            {
                this.mediumsort = string.Empty;
            }
            if (dr["countryid"] != DBNull.Value)//国家id
            {
                this.Countryid = Convert.ToInt32(dr["countryid"]);
            }



            if (dr["regionattribute"] != DBNull.Value)//地域属性
            {
                this.regionAttribute = Convert.ToInt32(dr["regionattribute"]);
            }

            if (dr["industryid"] != DBNull.Value)//industryid
            {
                this.industryid = Convert.ToInt32(dr["industryid"]);
            }
            if (dr["issueregion"] != DBNull.Value)//issueregion
            {
                this.issueregion = (dr["issueregion"]).ToString();
            }
            else
            {
                this.issueregion = string.Empty;
            }

            if (dr["Medianame"] != DBNull.Value)
            {
                this.medianame = (dr["Medianame"]).ToString();
            }
            else
            {
                this.medianame = string.Empty;
            }
            if (dr["industryname"] != DBNull.Value)
            {
                this.industryname = (dr["industryname"]).ToString();
            }
            else
            {
                this.industryname = string.Empty;
            }
            if (dr["headquarter"] != DBNull.Value)
            {
                this.headquarter = (dr["headquarter"]).ToString();
            }
            else
            {
                this.headquarter = string.Empty;
            }
            if (dr["mediatypename"] != DBNull.Value)
            {
                this.mediatypename = (dr["mediatypename"]).ToString();
            }
            else
            {
                this.mediatypename = string.Empty;
            }

            if (dr["telephoneExchange"] != DBNull.Value)
            {
                this.telephoneExchange = (dr["telephoneExchange"]).ToString();
            }
            else
            {
                this.telephoneExchange = string.Empty;
            }
            try
            {
                if (dr["publishPeriods"] != DBNull.Value)
                {
                    this.publishPeriods = (dr["publishPeriods"]).ToString();
                }
                else
                {
                    this.publishPeriods = string.Empty;
                }
            }
            catch { this.publishPeriods = string.Empty; };
        }

        int mediaitemid;

        public int Mediaitemid
        {
            get { return mediaitemid; }
            set { mediaitemid = value; }
        }
        string mediacname;

        public string Mediacname
        {
            get { return mediacname; }
            set { mediacname = value; }
        }
        string mediaename;

        public string Mediaename
        {
            get { return mediaename; }
            set { mediaename = value; }
        }
        string cshortname, eshortname;

        public string Eshortname
        {
            get { return eshortname; }
            set { eshortname = value; }
        }

        public string Cshortname
        {
            get { return cshortname; }
            set { cshortname = value; }
        }
        int mediaitemtype, currentversion;

        public int Currentversion
        {
            get { return currentversion; }
            set { currentversion = value; }
        }

        public int Mediaitemtype
        {
            get { return mediaitemtype; }
            set { mediaitemtype = value; }
        }
        int status, createdbyuserid;

        public int Createdbyuserid
        {
            get { return createdbyuserid; }
            set { createdbyuserid = value; }
        }

        public int Status
        {
            get { return status; }
            set { status = value; }
        }
        string createddate, lastmodifieddate;

        public string Lastmodifieddate
        {
            get { return lastmodifieddate; }
            set { lastmodifieddate = value; }
        }

        public string Createddate
        {
            get { return createddate; }
            set { createddate = value; }
        }
        int lastmodifiedbyuserid;

        public int Lastmodifiedbyuserid
        {
            get { return lastmodifiedbyuserid; }
            set { lastmodifiedbyuserid = value; }
        }
        string mediumsort;

        public string Mediumsort
        {
            get { return mediumsort; }
            set { mediumsort = value; }
        }
        int regionAttribute;

        public int RegionAttribute
        {
            get { return regionAttribute; }
            set { regionAttribute = value; }
        }
        int industryid;

        public int Industryid
        {
            get { return industryid; }
            set { industryid = value; }
        }
        int countryid;

        public int Countryid
        {
            get { return countryid; }
            set { countryid = value; }
        }
        string telephoneExchange;

        public string TelephoneExchange
        {
            get { return telephoneExchange; }
            set { telephoneExchange = value; }
        }
        string medianame;

        public string Medianame
        {
            get { return medianame; }
            set { medianame = value; }
        }
        string issueregion, industryname;

        public string Industryname
        {
            get { return industryname; }
            set { industryname = value; }
        }

        public string Issueregion
        {
            get { return issueregion; }
            set { issueregion = value; }
        }
        string headquarter, publishPeriods, mediatypename;

        public string Mediatypename
        {
            get { return mediatypename; }
            set { mediatypename = value; }
        }

        public string PublishPeriods
        {
            get { return publishPeriods; }
            set { publishPeriods = value; }
        }

        public string Headquarter
        {
            get { return headquarter; }
            set { headquarter = value; }
        }
    }
}
