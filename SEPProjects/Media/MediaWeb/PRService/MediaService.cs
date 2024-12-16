using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Data;
using System.Data.SqlClient;
using ESP.Media.Entity;
namespace Media.Service
{
    /// <summary>
    ///MediaService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    //若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 
    [System.Web.Script.Services.ScriptService]
    public class MediaService : BaseService
    {

        public MediaService()
        {

            //如果使用设计的组件，请取消注释以下行 
            //InitializeComponent(); 
        }

        [WebMethod]
        [System.Web.Script.Services.ScriptMethod(UseHttpGet = false)]
        [SoapHeader("Credentials")]
        public List<PostsInfo> getMediaPostList()
        {
            List<PostsInfo> posts = new List<PostsInfo>();
            DataTable dt = ESP.Media.BusinessLogic.PostsManager.GetPostMsgTop10(10);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PostsInfo post = new PostsInfo();
                post.Id = Convert.ToInt32(dt.Rows[i].ItemArray[0].ToString());
                post.Parentid = Convert.ToInt32(dt.Rows[i].ItemArray[1].ToString());
                post.No = Convert.ToInt32(dt.Rows[i].ItemArray[2].ToString());
                post.Type = Convert.ToInt32(dt.Rows[i].ItemArray[3].ToString());
                post.Issysmsg = Convert.ToInt32(dt.Rows[i].ItemArray[4].ToString());
                post.Issuedate = dt.Rows[i].ItemArray[5].ToString();
                post.Userid = Convert.ToInt32(dt.Rows[i].ItemArray[6].ToString());
                post.Subject = dt.Rows[i].ItemArray[7].ToString();
                post.Body = dt.Rows[i].ItemArray[8].ToString();
                post.Lastreplyuserid = Convert.ToInt32(dt.Rows[i].ItemArray[9].ToString());
                post.Lastreplydate = dt.Rows[i].ItemArray[10].ToString();
                post.Del = Convert.ToInt32(dt.Rows[i].ItemArray[11].ToString());

                posts.Add(post);


            }

            return posts;
        }


        [WebMethod]
        [SoapHeader("Credentials")]
        public MediaitemsInfo GetModel(int mediaitemid)
        {
            return ESP.Media.BusinessLogic.MediaitemsManager.GetModel(mediaitemid);
        }

        [WebMethod]
        [SoapHeader("Credentials")]
        public List<QueryMediaItemInfo> GetList(string term, List<SqlParameter> param)
        {
            return ESP.Media.BusinessLogic.MediaitemsManager.GetAllObjectList(term, param);
        }

    }

}