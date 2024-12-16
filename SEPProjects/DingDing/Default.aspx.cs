using ESP.Ding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dingding
{
    public partial class _default : System.Web.UI.Page
    {
        protected Dictionary<string, string> dic = new Dictionary<string, string>();
        private DingService dingdingService;
        public _default()
        {
            dingdingService = new DingService();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //DingdingService.GetUser("5");
            var data = dingdingService.GetDingdingConfig(RequstHelper.GetUrl());
            if (data.Code == ResultCode.Success)
            {
                dic = (Dictionary<String, String>)data.Data;
            }
        }
    }
}