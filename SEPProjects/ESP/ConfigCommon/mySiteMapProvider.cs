using System.Web;

namespace ESP.ConfigCommon
{
    public class mySiteMapProvider : XmlSiteMapProvider
    {

        public mySiteMapProvider() { }

        public override SiteMapNode FindSiteMapNode(string rawUrl)
        {

            SiteMapNode smn = base.FindSiteMapNode(rawUrl); 
            if (smn == null)
            {
                string[] arrUrl = rawUrl.Split('?');
                string sPage = arrUrl[0]; 
                string sQuery = string.Empty;
                if (arrUrl.Length >= 2)
                    sQuery = arrUrl[1]; 

                string[] aQuery = sQuery.Split('&'); 
 
                for (int i = 0; i < aQuery.Length; i++)
                {
                    smn = base.FindSiteMapNode(sPage + "?" + aQuery[i]);
                    if (smn != null)
                    {
                        break;
                    }
                }
                if (smn == null)
                {
                    smn = base.FindSiteMapNode(sPage);
                }
                
                if (smn != null)
                {
                    smn.ReadOnly = false;
                    smn.Url = rawUrl;
                    smn.ReadOnly = true;
                }
            }       
            return smn;
        }

    }
}

