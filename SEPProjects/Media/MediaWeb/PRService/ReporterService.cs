using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using System.IO;
using System.Web.Services.Protocols;
using ESP.Media.Entity;
namespace Media.Service
{

    /// <summary>
    ///ReporterService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    //若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 
    // [System.Web.Script.Services.ScriptService]
    public class ReporterService : BaseService
    {

        public ReporterService()
        {

            //如果使用设计的组件，请取消注释以下行 
            //InitializeComponent(); 
        }

        [WebMethod]
        [System.Web.Script.Services.ScriptMethod(UseHttpGet = false)]
        [SoapHeader("Credentials")]
        public ReportersInfo GetModel(int reporterid)
        {
            return ESP.Media.BusinessLogic.ReportersManager.GetModel(reporterid);
        }

        [WebMethod]
        public List<ESP.Media.Entity.QueryReporterInfo> GetList(string term, List<SqlParameter> param)
        {
            return ESP.Media.BusinessLogic.ReportersManager.GetAllObjectList(term, param);
        }

        [WebMethod]
        [System.Web.Script.Services.ScriptMethod(UseHttpGet = false)]
        [SoapHeader("Credentials")]
        public int ReporterUpdate(ESP.Media.Entity.QueryReporterInfo reporter,int currentuserid)
        {
            string err;
            ReportersInfo obj=null;
            if (reporter.Reporterid == 0)
            {
                obj = new ReportersInfo();
                obj.Reportername = reporter.Reportername;
                obj.Media_id = reporter.MediaID;
                obj.CityName = reporter.CityName;
                obj.Cardnumber = reporter.CardNumber;
                obj.Bankname = reporter.BankName;
                obj.Bankacountname = reporter.BankAccountName;
                obj.Bankcardname = reporter.BankCardName;
                obj.Paytype = Convert.ToInt32(reporter.PayType);
                obj.Usualmobile = reporter.Mobile;
                obj.Tel_o = reporter.Tel;
                obj.Createddate = DateTime.Now.ToString();
                return ESP.Media.BusinessLogic.ReportersManager.Add(obj, null, currentuserid, out err);
            }
            else
            {
                obj = ESP.Media.BusinessLogic.ReportersManager.GetModel(reporter.Reporterid);
                obj.Reportername = reporter.Reportername;
                obj.Media_id = reporter.MediaID;
                obj.CityName = reporter.CityName;
                obj.Cardnumber = reporter.CardNumber;
                obj.Bankname = reporter.BankName;
                obj.Bankacountname = reporter.BankAccountName;
                obj.Bankcardname = reporter.BankCardName;
                obj.Paytype = Convert.ToInt32(reporter.PayType);
                obj.Usualmobile = reporter.Mobile;
                obj.Tel_o = reporter.Tel;
                return ESP.Media.BusinessLogic.ReportersManager.Update(obj, null, currentuserid, out err);
            }
           
            
        }

        [WebMethod]
        [System.Web.Script.Services.ScriptMethod(UseHttpGet = false)]
        [SoapHeader("Credentials")]
        public string SaveFile(string path, string name, byte[] data, bool checkdic)
        {
            string fname = string.Empty;
            fname = name.Substring(name.LastIndexOf('\\') + 1);
            string serverpath = ESP.Configuration.ConfigurationManager.SafeAppSettings["ProjectBriefPath"];
            serverpath = Server.MapPath(serverpath);
            if (checkdic)
            {
                if (!Directory.Exists(serverpath))
                    Directory.CreateDirectory(serverpath);
            }
            if (File.Exists(serverpath+fname))
            {
                fname = Guid.NewGuid().ToString() + fname;
            }
            FileStream fs = new FileStream(serverpath + "\\" + fname, FileMode.CreateNew);
            fs.Write(data, 0, data.Length);
            fs.Close();

            return ESP.Configuration.ConfigurationManager.SafeAppSettings["ProjectBriefPath"] + fname;
        }

    }

}