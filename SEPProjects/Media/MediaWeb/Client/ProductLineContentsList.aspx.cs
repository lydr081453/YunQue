using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ESP.Media.BusinessLogic;

public partial class Client_ProductLineContentsList : ESP.Web.UI.PageBase
{
    override protected void OnInit(EventArgs e)
    {
        InitDataGridColumn();
        InitializeComponent();
        base.OnInit(e);
    }

    private void InitializeComponent()
    {

    }

    #region 绑定列头
    /// <summary>
    /// 初始化表格
    /// </summary>
    private void InitDataGridColumn()
    {
        string strColumn = "version#ProductLineTitle#ProductLineName#ProductLineDescription";
        string strHeader = "版本号#产品线图片#产品线名称#描述";
        string sort = "##ProductLineName#ProductLineDescription#";
        string strH = "#center##";
        MyControls.GridViewOperate.GridViewHelper.DrawGridView(false, strColumn, strHeader, sort,strH, this.dgList);
    }
    #endregion

    int alertvalue = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["alert"]) && Request["alert"].Length > 0)
        {
            alertvalue = Convert.ToInt32(Request["alert"]);
        }
        ListBind();
        if (alertvalue==1)
        {
            btnBack.Visible = false;
        }
        else
        {
            btnBack.Visible = true;
        }
    }



    #region 绑定列表
    private void ListBind()
    {
        StringBuilder str = new StringBuilder();
        Hashtable ht = new Hashtable();

        DataTable dt = ProductlinesManager.GetHistListByClientID(Convert.ToInt32( Request["Plid"]));
        this.dgList.DataSource = dt.DefaultView;
    }

    protected void dgList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int id = Convert.ToInt32( dgList.DataKeys[e.Row.RowIndex].Value.ToString());
            string param = Request.Url.Query.Substring(Request.Url.Query.IndexOf('?') + 1);
            param = ESP.Media.Access.Utilities.Global.AddParam(param, "alert", (alertvalue + 1).ToString());
            param = ESP.Media.Access.Utilities.Global.AddParam(param, "Plid", Request["Plid"]);
            param = ESP.Media.Access.Utilities.Global.AddParam(param, "Plcid", id.ToString());
            if (e.Row.Cells[1].Text == "&nbsp;")
            {
                e.Row.Cells[1].Text = string.Format("<img  src='{1}'style='width:80px;height:60px;' runat='server' onclick=\"window.location='/Client/ProductLineContentsDisplay.aspx?{0}';\"/>", param, ESP.Media.Access.Utilities.ConfigManager.DefauleImgPath);
            }
            else
            {
                e.Row.Cells[1].Text = string.Format("<img  src='{1}'style='width:80px;height:60px;'  runat='server' onclick=\"window.location = '/Client/ProductLineContentsDisplay.aspx?{0}';\"/>",param, e.Row.Cells[1].Text.Substring(e.Row.Cells[1].Text.IndexOf("~") + 1));
            }
        
            e.Row.Cells[1].Wrap = false;

            e.Row.Cells[2].Text = string.Format("<a href='#' onclick=\"window.location = '/Client/ProductLineContentsDisplay.aspx?{0}';\" >{1}</a>",param, e.Row.Cells[2].Text);

            
            

        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (Request["Plid"] != null)
        {
            string param = Request.Url.Query;
            param = ESP.Media.Access.Utilities.Global.AddParam(param,"alert", (alertvalue - 1).ToString());
            if (param.Length > 0)
            {
                param = "?" + param;
            }
            Response.Redirect(string.Format("ProductLineDisplay.aspx{0}",param));
        }
    }
}
    #endregion
