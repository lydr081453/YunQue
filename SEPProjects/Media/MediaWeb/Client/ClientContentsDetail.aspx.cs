using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ESP.Media.Entity;
using ESP.Media.BusinessLogic;
public partial class Client_ClientContentsDetail : ESP.Web.UI.PageBase
{
    int id = 0;
    int alertvalue = 0;

    /// <summary>
    /// Raises the <see cref="E:Init"/> event.
    /// </summary>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    override protected void OnInit(EventArgs e)
    {
        //InitDataGridColumn();
        base.OnInit(e);
    }

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        string param = Request.Url.Query;
        if (!string.IsNullOrEmpty(Request["alert"]) && Request["alert"].Length > 0)
        {
            alertvalue = Convert.ToInt32(Request["alert"]);
            param = "?" + ESP.Media.Access.Utilities.Global.AddParam(param, "alert", (alertvalue - 1).ToString());
        }

        this.hidUrl.Value = ESP.Media.Access.Utilities.Global.Url.ClientContentsList + param;
        if (Request["Ccid"] != null)
        {
            if (int.TryParse(Request["Ccid"], out id))
            {
                ClientshistInfo mlClient = ClientsManager.GetHistModel(id);
                if (mlClient != null)
                {
                    if (mlClient.Lastmodifiedbyuserid > 0)
                    {
                        labLastModifyUser.Text = new ESP.Compatible.Employee(mlClient.Lastmodifiedbyuserid).Name;
                    }
                    if (mlClient.Lastmodifieddate != null && mlClient.Lastmodifieddate.Length > 0)
                    {
                        labLastModifyDate.Text = mlClient.Lastmodifieddate;
                    }

                    this.labVersion.Text=mlClient.Version+"";
                    this.labClientName.Text = "公司名称：" + mlClient.Clientcfullname;
                    this.labChFullName.Text = mlClient.Clientcfullname;
                    this.labChShortName.Text = mlClient.Clientcshortname;
                    this.labEnFullName.Text = mlClient.Clientefullname;
                    this.labEnShortName.Text = mlClient.Clienteshortname;
                    this.labBrief.Text = mlClient.Clientdescription;
                    this.imgLogo.Visible = true;
                    if (mlClient.Clientlogo != string.Empty)
                    {
                        this.imgLogo.ImageUrl = mlClient.Clientlogo;
                        imgTitleFull.ImageUrl = mlClient.Clientlogo.Replace(".jpg", "_full.jpg");
                    }
                    else
                    {
                        this.imgLogo.ImageUrl = ESP.Media.Access.Utilities.ConfigManager.DefauleImgPath;
                        this.imgLogo.ImageUrl = ESP.Media.Access.Utilities.ConfigManager.DefauleImgPath.Replace(".jpg", "_full.jpg");
                    }
                    this.imgLogo.Width = 80;
                    this.imgLogo.Height = 80;
                    //ListBind(id);
                }
            }
        }
    }

    //#region 绑定列头
    ///// <summary>
    ///// 初始化表格
    ///// </summary>
    //private void InitDataGridColumn()
    //{
    //    string strColumn = "ProductLineName#ProductLineTitle#ProductLineDescription#ProductLineID#ProductLineID";
    //    string strHeader = "产品线#图像#简介#编辑#删除";
    //    string sort = "ProductLineName####";
    //    MyControls.GridViewOperate.GridViewHelper.DrawGridView(false, strColumn, strHeader,sort, this.dgList);
    //}
    //#endregion

    //#region 添加
    //protected void btnAdd_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect(string.Format("ProductLineAddAndEdit.aspx?Operate=ADD&Cid={0}&CientOperate={1}", id, Request["Operate"]));
    //}
    //#endregion

   

    //#region 绑定列表
    //private void ListBind(int clientID)
    //{
    //    string term = "ClientID = @ClientID";
    //    Hashtable ht = new Hashtable();
    //    ht.Add("@ClientID", clientID);
    //    DataTable dt = ProductlinesManager.GetList(term,ht);
    //    this.dgList.DataSource = dt.DefaultView;

    //    dgList.Columns[0].HeaderStyle.Wrap = false;
    //    dgList.Columns[3].HeaderStyle.Wrap = false;
    //    dgList.Columns[4].HeaderStyle.Wrap = false;
    //    dgList.Columns[1].Visible = false;
    //    dgList.Columns[2].Visible = false;
    //}

    //protected void dgList_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
           
    //        e.Row.Cells[0].Text = (Request["Operate"] == "ProductLineSelect") ? string.Format("<a href='../Project/ProjectAddAndEdit.aspx?Plid={0}&Cid={1}' >{2}</a>", e.Row.Cells[3].Text, Request["Cid"], e.Row.Cells[0].Text) : e.Row.Cells[0].Text;

    //        if (e.Row.Cells[1].Text == "&nbsp;")
    //            e.Row.Cells[1].Text = e.Row.Cells[2].Text;
    //        else
    //            e.Row.Cells[1].Text = string.Format("<table width='100%' style='background-color: transparent'><tr><td><img src='{0}' style='width:80px;height:60px;'></td><td>{1}</td></tr></table>", e.Row.Cells[1].Text, e.Row.Cells[2].Text);

    //        e.Row.Cells[3].Text = string.Format("<a href='ProductLineAddAndEdit.aspx?Operate=EDIT&Plid={0}&Cid={1}&CientOperate={2}' ><img src='{3}' /></a>", e.Row.Cells[3].Text, Request["Cid"], Request["Operate"], ESP.Media.Access.Utilities.ConfigManager.EditIconPath);
    //        e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;

    //        e.Row.Cells[4].Text = string.Format("<a href='ProductLineAddAndEdit.aspx?Operate=DEL&Plid={0}&Cid={1}&CientOperate={2}' onclick= \"return confirm( '真的要删除吗?');\"><img src='{3}' /></a>", e.Row.Cells[4].Text, Request["Cid"], Request["Operate"], ESP.Media.Access.Utilities.ConfigManager.DelIconPath);
    //        e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;

    //    }
    //}
    //#endregion
}
