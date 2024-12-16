using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using System.IO;
using System.Data.SqlClient;
using ESP.Media.Access;
using ESP.Media.Entity;
namespace ESP.Media.Entity
{
    public class QueryProductLineInfo
    {
        public QueryProductLineInfo(DataRow dr)
        {
            if (dr["productlineid"] != DBNull.Value)
            {
                this.productlineid = Convert.ToInt32(dr["productlineid"]);
            }

            if (dr["lastmodifiedbyuserid"] != DBNull.Value)
            {
                this.lastmodifiedbyuserid = Convert.ToInt32(dr["lastmodifiedbyuserid"]);
            }

            if (dr["clientid"] != DBNull.Value)
            {
                this.clientid = Convert.ToInt32(dr["clientid"]);
            }
            if (dr["currentversion"] != DBNull.Value)
            {
                this.currentversion = Convert.ToInt32(dr["currentversion"]);
            }
            if (dr["status"] != DBNull.Value)
            {
                this.status = Convert.ToInt32(dr["status"]);
            }
            if (dr["createdbyuserid"] != DBNull.Value)
            {
                this.createdbyuserid = Convert.ToInt32(dr["createdbyuserid"]);
            }

            if (dr["productlinename"] != DBNull.Value)
            {
                this.productlinename = dr["productlinename"].ToString();
            }

            if (dr["productlinedescription"] != DBNull.Value)
            {
                this.productlinedescription = dr["productlinedescription"].ToString();

            }

            if (dr["productlinetitle"] != DBNull.Value)
            {
                this.productlinetitle = dr["productlinetitle"].ToString();

            }

            if (dr["createdip"] != DBNull.Value)
            {
                this.createdip = dr["createdip"].ToString();

            }

            if (dr["createddate"] != DBNull.Value)
            {
                this.createddate = dr["createddate"].ToString();

            }

            if (dr["lastmodifiedip"] != DBNull.Value)
            {
                this.lastmodifiedip = dr["lastmodifiedip"].ToString();

            }
            if (dr["lastmodifieddate"] != DBNull.Value)
            {
                this.lastmodifieddate = dr["lastmodifieddate"].ToString();

            }
            if (dr["createddate"] != DBNull.Value)
            {
                this.createddate = dr["createddate"].ToString();

            }

            if (dr["clientcshortname"] != DBNull.Value)
            {
                this.clientcshortname = dr["clientcshortname"].ToString();

            }
            if (dr["clientefullname"] != DBNull.Value)
            {
                this.clientefullname = dr["clientefullname"].ToString();

            }
            if (dr["clienteshortname"] != DBNull.Value)
            {
                this.clienteshortname = dr["clienteshortname"].ToString();

            }


        }
        int productlineid;

        public int Productlineid
        {
            get { return productlineid; }
            set { productlineid = value; }
        }
        int lastmodifiedbyuserid;


        public int Lastmodifiedbyuserid
        {
            get { return lastmodifiedbyuserid; }
            set { lastmodifiedbyuserid = value; }
        }
        int clientid;

        public int Clientid
        {
            get { return clientid; }
            set { clientid = value; }
        }
        int currentversion;

        public int Currentversion
        {
            get { return currentversion; }
            set { currentversion = value; }
        }
        int status;

        public int Status
        {
            get { return status; }
            set { status = value; }
        }
        int createdbyuserid;

        public int Createdbyuserid
        {
            get { return createdbyuserid; }
            set { createdbyuserid = value; }
        }

        string productlinename;

        public string Productlinename
        {
            get { return productlinename; }
            set { productlinename = value; }
        }
        string productlinedescription;

        public string Productlinedescription
        {
            get { return productlinedescription; }
            set { productlinedescription = value; }
        }
        string productlinetitle;

        public string Productlinetitle
        {
            get { return productlinetitle; }
            set { productlinetitle = value; }
        }
        string createdip;

        public string Createdip
        {
            get { return createdip; }
            set { createdip = value; }
        }
        string createddate;

        public string Createddate
        {
            get { return createddate; }
            set { createddate = value; }
        }
        string lastmodifiedip;

        public string Lastmodifiedip
        {
            get { return lastmodifiedip; }
            set { lastmodifiedip = value; }
        }
        string lastmodifieddate;

        public string Lastmodifieddate
        {
            get { return lastmodifieddate; }
            set { lastmodifieddate = value; }
        }
        string clientcfullname;

        public string Clientcfullname
        {
            get { return clientcfullname; }
            set { clientcfullname = value; }
        }
        string clientcshortname;

        public string Clientcshortname
        {
            get { return clientcshortname; }
            set { clientcshortname = value; }
        }
        string clientefullname;

        public string Clientefullname
        {
            get { return clientefullname; }
            set { clientefullname = value; }
        }
        string clienteshortname;

        public string Clienteshortname
        {
            get { return clienteshortname; }
            set { clienteshortname = value; }
        }
    }
}
