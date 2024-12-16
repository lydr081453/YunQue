using System;
using System.Web.UI;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;
using System.Collections;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using WebSupergoo.ABCpdf7;
using System.IO;
using System.Web.Services;

public partial class AddPriceAtt : ESP.Purchase.WebPage.EditPageForPR
{
    string script = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }

    #region 判断返回上级页面的Script
    private string ReturnScript(string atts, string ids)
    {
        //添加页面
        if (Request["page"] != null && Request["page"].ToString() == "add")
        {
            if (Request["type"] != null && Request["type"].ToString() == "f")//非目录物品
            {
                script += "parent.document.getElementById('ctl00_ContentPlaceHolder1_txtIds').value= '" + ids.ToString() + "';";
                script += "parent.document.getElementById('ctl00_ContentPlaceHolder1_txtIds').style.display='none';";
                script += "parent.document.getElementById('ctl00_ContentPlaceHolder1_TabContainer1_Tab2_hidNames1').value= \"" + atts.ToString() + "\";";
                script += "parent.document.getElementById('ctl00_ContentPlaceHolder1_TabContainer1_Tab2_lbPriceAtt1').innerText=\"" + atts.ToString() + "\";";
                script += "parent.document.getElementById('ctl00_ContentPlaceHolder1_TabContainer1_Tab2_lbPriceAtt1').style.display='block';";
            }
            else//目录物品
            {
                script += "parent.document.getElementById('ctl00_ContentPlaceHolder1_txtIds').value= '" + ids.ToString() + "';";
                // script += "parent.document.getElementById('ctl00_ContentPlaceHolder1_txtIds').style.display='none';";
                script += "parent.document.getElementById('ctl00_ContentPlaceHolder1_TabContainer1_Tab2_hidNames').value=\"" + atts.ToString() + "\";";
                script += "parent.document.getElementById('ctl00_ContentPlaceHolder1_TabContainer1_Tab2_lbPriceAtt').innerText= \"" + atts.ToString() + "\";";
                script += "parent.document.getElementById('ctl00_ContentPlaceHolder1_TabContainer1_Tab2_lbPriceAtt').style.display='block';";
            }
        }
        //修改页面
        if (Request["page"] != null && Request["page"].ToString() == "mod")
        {
            script += "parent.document.getElementById('ctl00_ContentPlaceHolder1_hidIds').value= '" + ids.ToString() + "';";
            script += "parent.document.getElementById('ctl00_ContentPlaceHolder1_hidNames').value= \"" + atts.ToString() + "\";";
            script += "parent.document.getElementById('ctl00_ContentPlaceHolder1_lbPriceAtt').innerText= \"" + atts.ToString() + "\";";
            script += "parent.document.getElementById('ctl00_ContentPlaceHolder1_lbPriceAtt').style.display='block';";

        }
        //productInfo控件
        if (Request["page"] == null || Request["page"].ToString() == "pinfo")
        {
            script += "parent.document.getElementById('ctl00_ContentPlaceHolder1_productInfo_hidIds').value= '" + ids.ToString() + "';";
            // script += "parent.document.getElementById('ctl00_ContentPlaceHolder1_txtIds').style.display='none';";
            script += "parent.document.getElementById('ctl00_ContentPlaceHolder1_productInfo_hidNames').value= \"" + atts.ToString() + "\";";
            script += "parent.document.getElementById('ctl00_ContentPlaceHolder1_productInfo_lbPriceAtt').innerText= \"" + atts.ToString() + "\";";
            script += "parent.document.getElementById('ctl00_ContentPlaceHolder1_productInfo_lbPriceAtt').style.display='block';";
        }
        return script;
    }
    #endregion



    #region 添加按钮  本地上传附件页签
    protected void btnAdd1_click(object sender, EventArgs e)
    {
        string atts = "";
        string ids = "";
        int generalid = 0;
        if (!string.IsNullOrEmpty(Request[RequestName.GeneralID]))
        {
            generalid = int.Parse(Request[RequestName.GeneralID]);
        }

        ////判断全部上传文件总大小
        //int allLength = 0;
        //for (int i = 0; i < Request.Files.Count; i++)
        //{
        //    if (Request.Files.Keys[i] == "filBJ2" && Request.Files[i].FileName != "")
        //    {
        //        System.Web.HttpPostedFile postFile = Request.Files[i];
        //        allLength += postFile.ContentLength;
        //    }
        //}
        //if (allLength > 4096000)
        //{
        //    ClientScript.RegisterStartupScript(typeof(string), "", "alert('一次上传的多个文件总大小超过了4M！');", true);
        //    return;
        //}

        for (int i = 0; i < Request.Files.Count; i++)
        {
            if (Request.Files.Keys[i] == "filBJ2" && Request.Files[i].FileName != "")
            {
                //string generalid = "";
                //if (Request["generalid"] != null && Request["generalid"].ToString() == "")
                //{
                //    generalid = Request["generalid"].ToString();
                //}
                //else
                //{
                //    generalid = "988";
                //}

                //上传本地附件到服务器平台附件存放处，调用 upFile1 方法
                System.Web.HttpPostedFile postFile = Request.Files[i];
                //未做上传判断
                //string fileName = "wuliao_" + generalid.ToString() + "_" + DateTime.Now.Ticks.ToString();
                //atts += FileHelper.upFile1(fileName, ESP.Configuration.ConfigurationManager.SafeAppSettings["supplyNewsFilePath"], postFile) + ";";

                //对上传文件做判断
                string filename = SaveAtt(postFile, generalid);
                if (filename == string.Empty)
                {
                    ClientScript.RegisterStartupScript(typeof(string), "", "alert('附件上传失败！');", true);
                    return;
                }
                else
                {
                    atts += filename + ";";
                }
            }
            System.Threading.Thread.Sleep(100);
        }
        if (atts != string.Empty)
            atts = atts.Substring(0, atts.Length - 1);
        script = ReturnScript(atts, ids);
        script += @"parent.hidselect();parent.$('#floatBoxBg').hide();parent.$('#floatBox').hide();";
        ScriptManager.RegisterStartupScript(this, Page.GetType(), new Guid().ToString(), script, true);
    }
    #endregion

    #region 上传本地附件
    private string SaveAtt(System.Web.HttpPostedFile postFile, int generalid)
    {
        if (postFile.ContentLength > 10485760)
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('您上传的文件请不要超过 10M!');", true);
            return string.Empty;
        }
        string exts = ".js .exe .com .dll .bat";
        string extension = System.IO.Path.GetExtension(postFile.FileName).ToLower();
        if (exts.Contains(extension))
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('该文件类型不能上传!');", true);
            return string.Empty;
        }
        try
        {

            //string fileName = LoginVendee.ID.ToString() + LoginVendee.Name + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + "_" + this.fld.FileName;
            //this.fld.PostedFile.SaveAs(Server.MapPath(ConfigurationSettings.AppSettings["SupplierNewsFile"]) + "\\" + fileName);
            //string fileName = "wuliao_" + generalid.ToString() + "_" + DateTime.Now.Ticks.ToString();
            string fileName = "wuliao_" + generalid.ToString() + "_" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second; //+"_" + postFile.FileName;

            return FileHelper.upFile1(fileName, ESP.Configuration.ConfigurationManager.SafeAppSettings["supplyNewsFilePath"], postFile);
        }
        catch
        {
            return string.Empty;
        }
    }
    #endregion

    [WebMethod]

    public static string ValidateFile(string filepath, string filezie)
    {

        var fs = new FileStream(filepath, FileMode.Open, FileAccess.Read);
        return fs.Length.ToString();

        //if (fs.Length > int.Parse(filezie))
        //    return "invalid";
        //return "valid";
    }
}