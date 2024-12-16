using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SEPAdmin.ServiceCenter
{
    public partial class FileDownload : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string index = Request["index"];
                switch (index)
                {
                    case "1":
                        fileDownload("doc/股东大会议事规则.pdf");
                        break;
                    case "2":
                        fileDownload("doc/董事会审计委员会工作制度.pdf");
                        break;
                    case "3":
                        fileDownload("doc/董事会提名委员会工作制度.pdf");
                        break;
                    case "4":
                        fileDownload("doc/董事会议事规则.pdf");
                        break;
                    case "5":
                        fileDownload("doc/董事会战略委员会工作制度.pdf");
                        break;
                    case "6":
                        fileDownload("doc/董事会薪酬与考核委员会工作制度.pdf");
                        break;
                    case "7":
                        fileDownload("doc/董事监事高级管理人持有和买卖公司股票管理制度.pdf");
                        break;
                    case "8":
                        fileDownload("doc/董事会秘书工作细则.pdf");
                        break;
                    case "9":
                        fileDownload("doc/独立董事工作细则.pdf");
                        break;
                    case "10":
                        fileDownload("doc/对外担保管理制度.pdf");
                        break;
                    case "11":
                        fileDownload("doc/对外投资管理制度.pdf");
                        break;
                    case "12":
                        fileDownload("doc/关联交易管理制度.pdf");
                        break;
                    case "13":
                        fileDownload("doc/规范与关联方资金往来的管理制度.pdf");
                        break;
                    case "14":
                        fileDownload("doc/监事会议事规则.pdf");
                        break;
                    case "15":
                        fileDownload("doc/募集资金管理制度.pdf");
                        break;
                    case "16":
                        fileDownload("doc/内幕信息知情人登记制度.pdf");
                        break;
                    case "17":
                        fileDownload("doc/年报信息披露重大差错责任追究制度.pdf");
                        break;
                    case "18":
                        fileDownload("doc/投资者关系管理制度.pdf");
                        break;
                    case "19":
                        fileDownload("doc/重大信息内部报告制度.pdf");
                        break;
                    case "20":
                        fileDownload("doc/总经理工作细则.pdf");
                        break;
                    case "21":
                        fileDownload("doc/信息披露事务管理制度.pdf");
                        break;
                    case "22":
                        fileDownload("doc/公司章程.pdf");
                        break;
                    case "23":
                        fileDownload("doc/星言云汇.pdf");
                        break;
                }
            }
        }

        private void fileDownload(string fileName)
        {
            Response.ClearContent();//清空缓冲区内容
            Response.ClearHeaders();//清空缓冲区HTTP头

            Response.ContentType = "application/PDF";//设置HTTP头为PDF文档其它类似

            Response.WriteFile(fileName);//写入客户端
            Response.Flush();//客户更新
            //Response.Close();//写入关闭
            Response.End();
        }
    }
}