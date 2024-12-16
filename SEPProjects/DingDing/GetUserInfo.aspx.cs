using ESP.Ding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DingDing
{
    public partial class getUserInfo : System.Web.UI.Page
    {
        protected string code;

        private DingService dingdingService;

        public getUserInfo()
        {
            dingdingService = new DingService();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            code = Request.QueryString["code"];
            string uid = dingdingService.GetUserInfo(code).userid;
            ResultInfo<object> result = new ResultInfo<object>();
            Dingding_User user = dingdingService.GetUser(uid);
            result.Code = ResultCode.Success;
            result.Message = "免登录，获取个人信息成功！";
            Response.Write(JsonHelper.GetJson(result));

        }
    }
}