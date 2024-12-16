using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using ESP.Supplier.BusinessLogic;
using ESP.Supplier.Common;
using ESP.Supplier.Entity;

namespace PurchaseWeb.Purchase.Requisition.Print
{
    public partial class SupplierMessageReturn : ESP.Web.UI.PageBase//System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["mrId"] != null)
            {
                int mrId = int.Parse(Request["mrId"]);

                ESP.Supplier.Entity.SC_SupplierMessageReturn mr = ESP.Supplier.BusinessLogic.SC_SupplierMessageReturnManager.GetModel(mrId);
                if (mr != null)
                {
                    this.lbRtitle.Text = mr.Title;
                    //this.lbRname.Text = mr.UserName;

                    //回复人
                    if (mr.UserType == 1)
                    {
                        ESP.Supplier.Entity.SC_Vendee sv = new SC_VendeeManager().GetModel(mr.CreatedUserID);
                        this.lbRname.Text = sv.RealName;
                    }
                    else if (mr.UserType == 0)
                    {
                        ESP.Supplier.Entity.SC_Supplier sp = new ESP.Supplier.BusinessLogic.SC_SupplierManager().GetModel(mr.CreatedUserID);
                        this.lbRname.Text = sp.supplier_name;
                    }

                    //新闻创建人和系统编号
                    SC_SupplierMessages msg = SC_SupplierMessagesManager.GetModel(mr.SuppliserMessageID);
                    if (msg != null)
                    {
                        //this.lblDate.Text = msg.UserName + "   -   " + msg.CreatedDate.ToString();
                        this.lbNo.Text = "#" + (Convert.ToDateTime(msg.CreatedDate).ToString("yyyyMMdd") + msg.ID).ToString();

                        ESP.Supplier.Entity.SC_Supplier supplier = new ESP.Supplier.BusinessLogic.SC_SupplierManager().GetModel(msg.CreatedUserID);
                        if (msg.UserType == 1)
                        {
                            ESP.Supplier.Entity.SC_Vendee sv = new SC_VendeeManager().GetModel(msg.CreatedUserID);
                            this.lbCname.Text = sv.RealName;
                        }
                        else if (msg.UserType == 0)
                        {
                            this.lbCname.Text = supplier.supplier_name;
                        }
                    }

                    this.lbRtime.Text = mr.CreatedDate.ToString();
                    

                    string body = mr.Body;
                    body = TransStringTool(body);
                    body = Ubbcode(body);
                    //body = UbbToHtml(body);
                    //body = UbbToHtmlAgainst(body);
                    this.lbRinfo.Text = body.Replace("&lt;br&nbsp;/&gt;", "<br />");

                }
            }
        }
        public static string UbbToHtml(string str)
        {

            Regex my = new Regex(@"\[media=(mov),*([0-9]*),*([0-9]*),*([0-1]*)\](http://.[^\[]*)\[\/media\]", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<object classid=""clsid:02BF25D5-8C17-4B23-BC80-D3488ABDDC6B"" width=""$2"" height=""$3""><param name=""autostart"" value=""$4"" /><param name=""src"" value=""$5"" /><embed controller=""true"" width=""$2"" height=""$3"" src=""$5"" autostart=""$4""></embed></object>");


            //浮动代码
            my = new Regex(@"\[float=(left|right)\](.[^\[]*)(\[\/float\])", RegexOptions.IgnoreCase);
            //str = my.Replace(str, @"<div style=""float:$1;padding:5px;font-size: 12px; border: solid #CAD9EA; border-width: 1px; background: #FFF ; overflow: hidden; clear:both;"" class=""floatcode"">$2</div>");
            //my = new Regex(@"(\[FLY\])(.[^\[]*)(\[\/FLY\])", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<marquee onmouseover='this.stop();' onmouseout='this.start();'>$2</marquee>");

            //文字字体UBB
            my = new Regex(@"\[font=(.[^\[]*)\](.[^\[]*)(\[\/font\])", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<font face=""$1"">$2</font>");

            //字体倾斜
            my = new Regex(@"\[i\]((.|\n)*?)\[\/i\]", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<i>$1</i>");

            //字体粗体
            my = new Regex(@"\[B\]((.|\n)*?)\[\/B\]", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<b>$1</b>");

            my = new Regex(@"\[U\]((.|\n)*?)\[\/U\]", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<u>$1</u>");



            my = new Regex(@"(\[IMG\])(.[^\[]*)(\[\/IMG\])", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<a href=""$2"" target=_blank><IMG SRC=""$2"" border=0 alt=按此在新窗口浏览图片 onload=""javascript:if(this.width>screen.width-333)this.width=screen.width-333""></a>");

            my = new Regex(@"(\[gif\])(.[^\[]*)(\[\/gif\])", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<a href=""$2"" target=_blank><IMG SRC=""$2"" border=0 alt=按此在新窗口浏览图片 onload=""javascript:if(this.width>screen.width-333)this.width=screen.width-333""></a>");

            my = new Regex(@"(\[jpg\])(.[^\[]*)(\[\/jpg\])", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<a href=""$2"" target=_blank><IMG SRC=""$2"" border=0 alt=按此在新窗口浏览图片 onload=""javascript:if(this.width>screen.width-333)this.width=screen.width-333""></a>");

            my = new Regex(@"\[DIR=*([0-9]*),*([0-9]*)\](.[^\[]*)\[\/DIR]", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<object classid=clsid:166B1BCA-3F9C-11CF-8075-444553540000 codebase=http://download.macromedia.com/pub/shockwave/cabs/director/sw.cab#version=7,0,2,0 width=$1 height=$2><param name=src value=$3><embed src=$3 pluginspage=http://www.macromedia.com/shockwave/download/ width=$1 height=$2></embed></object>");

            my = new Regex(@"\[QT=*([0-9]*),*([0-9]*)\](.[^\[]*)\[\/QT]", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<embed src=$3 width=$1 height=$2 autoplay=true loop=false controller=true playeveryframe=false cache=false scale=TOFIT bgcolor=#000000 kioskmode=false targetcache=false pluginspage=http://www.apple.com/quicktime/>");

            my = new Regex(@"\[MP=*([0-9]*),*([0-9]*)\](.[^\[]*)\[\/MP]", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<object align=middle classid=CLSID:22d6f312-b0f6-11d0-94ab-0080c74c7e95 class=OBJECT id=MediaPlayer width=$1 height=$2 ><param name=ShowStatusBar value=-1><param name=Filename value=$3><embed type=application/x-oleobject codebase=http://activex.microsoft.com/activex/controls/mplayer/en/nsmp2inf.cab#Version=5,1,52,701 flename=mp src=$3  width=$1 height=$2></embed></object>");

            my = new Regex(@"\[RM=*([0-9]*),*([0-9]*)\](.[^\[]*)\[\/RM]", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<OBJECT classid=clsid:CFCDAA03-8BE4-11cf-B84B-0020AFBBCCFA class=OBJECT id=RAOCX width=$1 height=$2><PARAM NAME=SRC VALUE=$3><PARAM NAME=CONSOLE VALUE=Clip1><PARAM NAME=CONTROLS VALUE=imagewindow><PARAM NAME=AUTOSTART VALUE=true></OBJECT><br><OBJECT classid=CLSID:CFCDAA03-8BE4-11CF-B84B-0020AFBBCCFA height=32 id=video2 width=$1><PARAM NAME=SRC VALUE=$3><PARAM NAME=AUTOSTART VALUE=-1><PARAM NAME=CONTROLS VALUE=controlpanel><PARAM NAME=CONSOLE VALUE=Clip1></OBJECT>");

            my = new Regex(@"(\[FLASH\])(.[^\[]*)(\[\/FLASH\])", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<OBJECT codeBase=http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=4,0,2,0 classid=clsid:D27CDB6E-AE6D-11cf-96B8-444553540000 width=500 height=400><PARAM NAME=movie VALUE=""$2""><PARAM NAME=quality VALUE=high><embed src=""$2"" quality=high pluginspage='http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash' type='application/x-shockwave-flash' width=500 height=400>$2</embed></OBJECT>");

            my = new Regex(@"(\[ZIP\])(.[^\[]*)(\[\/ZIP\])", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<br><IMG SRC=pic/zip.gif border=0> <a href=""$2"">点击下载该文件</a>");

            my = new Regex(@"(\[RAR\])(.[^\[]*)(\[\/RAR\])", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<br><IMG SRC=pic/rar.gif border=0> <a href=""$2"">点击下载该文件</a>");

            my = new Regex(@"(\[UPLOAD=(.[^\[]*)\])(.[^\[]*)(\[\/UPLOAD\])", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<br><IMG SRC=""pic/$2.gif"" border=0>此主题相关图片如下：<br><A HREF=""$3"" TARGET=_blank><IMG SRC=""$3"" border=0 alt=按此在新窗口浏览图片 onload=""javascript:if(this.width>screen.width-333)this.width=screen.width-333""></A>");

            my = new Regex(@"(\[URL\])(http:\/\/.[^\[]*)(\[\/URL\])", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<A HREF=""$2"" TARGET=_blank>$2</A>");

            my = new Regex(@"(\[URL\])(.[^\[]*)(\[\/URL\])", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<A HREF=""http://$2"" TARGET=_blank>$2</A>");

            my = new Regex(@"(\[URL=(http:\/\/.[^\[]*)\])(.[^\[]*)(\[\/URL\])", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<A HREF=""$2"" TARGET=_blank>$3</A>");

            //my = new Regex(@"(\[URL=(.[^\[]*)\])(.[^\[]*)(\[\/URL\])", RegexOptions.IgnoreCase);
            //str = my.Replace(str, @"<A HREF=""http://$2"" TARGET=_blank>$3</A>");

            my = new Regex(@"(\[EMAIL\])(\S+\@.[^\[]*)(\[\/EMAIL\])", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<A HREF=""mailto:$2"">$2</A>");

            my = new Regex(@"(\[EMAIL=(\S+\@.[^\[]*)\])(.[^\[]*)(\[\/EMAIL\])", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<A HREF=""mailto:$2"" TARGET=_blank>$3</A>");

            my = new Regex(@"^(HTTP://[A-Za-z0-9\./=\?%\-&_~`@':+!]+)", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<a target=_blank href=$1>$1</a>");

            my = new Regex(@"(HTTP://[A-Za-z0-9\./=\?%\-&_~`@':+!]+)$", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<a target=_blank href=$1>$1</a>");

            my = new Regex(@"[^>=""](HTTP://[A-Za-z0-9\./=\?%\-&_~`@':+!]+)", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<a target=_blank href=$1>$1</a>");

            my = new Regex(@"^(FTP://[A-Za-z0-9\./=\?%\-&_~`@':+!]+)", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<a target=_blank href=$1>$1</a>");

            my = new Regex(@"(FTP://[A-Za-z0-9\./=\?%\-&_~`@':+!]+)$", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<a target=_blank href=$1>$1</a>");

            my = new Regex(@"[^>=""](FTP://[A-Za-z0-9\.\/=\?%\-&_~`@':+!]+)", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<a target=_blank href=$1>$1</a>");

            my = new Regex(@"^(RTSP://[A-Za-z0-9\./=\?%\-&_~`@':+!]+)", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<a target=_blank href=$1>$1</a>");

            my = new Regex(@"(RTSP://[A-Za-z0-9\./=\?%\-&_~`@':+!]+)$", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<a target=_blank href=$1>$1</a>");

            my = new Regex(@"[^>=""](RTSP://[A-Za-z0-9\.\/=\?%\-&_~`@':+!]+)", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<a target=_blank href=$1>$1</a>");

            my = new Regex(@"^(MMS://[A-Za-z0-9\./=\?%\-&_~`@':+!]+)", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<a target=_blank href=$1>$1</a>");
            my = new Regex(@"(MMS://[A-Za-z0-9\./=\?%\-&_~`@':+!]+)$", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<a target=_blank href=$1>$1</a>");

            my = new Regex(@"[^>=""](MMS://[A-Za-z0-9\.\/=\?%\-&_~`@':+!]+)", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<a target=_blank href=$1>$1</a>");

            my = new Regex(@"(\[HTML\])(.[^\[]*)(\[\/HTML\])", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<table width='100%' border='0' cellspacing='0' cellpadding='6' bgcolor=''><td><b>以下内容为程序代码:</b><br>$2</td></table>");

            my = new Regex(@"(\[CODE\])(.[^\[]*)(\[\/CODE\])", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<table width='100%' border='0' cellspacing='0' cellpadding='6' bgcolor=''><td><b>以下内容为程序代码:</b><br>$2</td></table>");

            my = new Regex(@"(\[COLOR=(.[^\[]*)\])(.[^\[]*)(\[\/COLOR\])", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<font COLOR=$2>$3</font>");

            my = new Regex(@"(\[FACE=(.[^\[]*)\])(.[^\[]*)(\[\/FACE\])", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<font FACE=$2>$3</font>");

            my = new Regex(@"(\[ALIGN=(.[^\[]*)\])(.*)(\[\/ALIGN\])", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<div ALIGN=$2>$3</div>");

            my = new Regex(@"(\[QUOTE\])(.*)(\[\/QUOTE\])", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<table cellpadding=0 cellspacing=0 border=0 WIDTH=94% bgcolor=#BDC2C8 align=center><tr><td><table width=100% cellpadding=5 cellspacing=1 border=0><TR><TD>$2</table></table><br>");

            my = new Regex(@"(\[MOVE\])(.*)(\[\/MOVE\])", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<MARQUEE scrollamount=3>$2</marquee>");

            my = new Regex(@"\[GLOW=*([0-9]*),*(#*[a-z0-9]*),*([0-9]*)\](.[^\[]*)\[\/GLOW]", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<table width=$1 style=""filter:glow(color=$2, strength=$3)"">$4</table>");

            my = new Regex(@"\[SHADOW=*([0-9]*),*(#*[a-z0-9]*),*([0-9]*)\](.[^\[]*)\[\/SHADOW]", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<table width=$1 style=""filter:shadow(color=$2, strength=$3)"">$4</table>");

            my = new Regex(@"(\[I\])(.[^\[]*)(\[\/I\])", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<i>$2</i>");

            my = new Regex(@"(\[U\])(.[^\[]*)(\[\/U\])", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<u>$2</u>");

            my = new Regex(@"(\[B\])(.[^\[]*)(\[\/B\])", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<b>$2</b>");

            my = new Regex(@"(\[FLY\])(.[^\[]*)(\[\/FLY\])", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<marquee onmouseover='this.stop();' onmouseout='this.start();'>$2</marquee>");

            my = new Regex(@"(\[SIZE=1\])(.[^\[]*)(\[\/SIZE\])", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<font size=1>$2</font>");

            my = new Regex(@"(\[SIZE=2\])(.[^\[]*)(\[\/SIZE\])", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<font size=2>$2</font>");

            my = new Regex(@"(\[SIZE=3\])(.[^\[]*)(\[\/SIZE\])", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<font size=3>$2</font>");

            my = new Regex(@"(\[SIZE=4\])(.[^\[]*)(\[\/SIZE\])", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<font size=4>$2</font>");


            my = new Regex(@"(\[SIZE=5\])(.[^\[]*)(\[\/SIZE\])", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<font size=5>$2</font>");

            my = new Regex(@"(\[SIZE=6\])(.[^\[]*)(\[\/SIZE\])", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<font size=6>$2</font>");

            my = new Regex(@"(\[SIZE=7\])(.[^\[]*)(\[\/SIZE\])", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<font size=7>$2</font>");

            //my = new Regex(@"(\[SIZE=([0-9]*)\])(.[^\[]*)(\[\/SIZE\])", RegexOptions.IgnoreCase);
            ////my = new Regex(@"\[SIZE=([0-9]*)\]", RegexOptions.IgnoreCase);
            //str = my.Replace(str, @"<font size=$1>$2</font>");


            my = new Regex(@"(\[CENTER\])(.[^\[]*)(\[\/CENTER\])", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<center>$2</center>");



            return str;
        }

        public static string UbbToHtmlAgainst(string str)
        {
            Regex my = new Regex(@"(\[CENTER\])(.[^\[]*)(\[\/CENTER\])", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<center>$2</center>");

            my = new Regex(@"(\[SIZE=7\])(.[^\[]*)(\[\/SIZE\])", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<font size=7>$2</font>");

            my = new Regex(@"(\[SIZE=6\])(.[^\[]*)(\[\/SIZE\])", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<font size=6>$2</font>");

            my = new Regex(@"(\[SIZE=5\])(.[^\[]*)(\[\/SIZE\])", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<font size=5>$2</font>");

            my = new Regex(@"(\[SIZE=4\])(.[^\[]*)(\[\/SIZE\])", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<font size=4>$2</font>");

            my = new Regex(@"(\[SIZE=3\])(.[^\[]*)(\[\/SIZE\])", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<font size=3>$2</font>");

            my = new Regex(@"(\[SIZE=2\])(.[^\[]*)(\[\/SIZE\])", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<font size=2>$2</font>");

            my = new Regex(@"(\[SIZE=1\])(.[^\[]*)(\[\/SIZE\])", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<font size=1>$2</font>");

            my = new Regex(@"(\[FLY\])(.[^\[]*)(\[\/FLY\])", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<marquee onmouseover='this.stop();' onmouseout='this.start();'>$2</marquee>");

            my = new Regex(@"(\[B\])(.[^\[]*)(\[\/B\])", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<b>$2</b>");

            my = new Regex(@"(\[U\])(.[^\[]*)(\[\/U\])", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<u>$2</u>");

            my = new Regex(@"(\[I\])(.[^\[]*)(\[\/I\])", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<i>$2</i>");

            my = new Regex(@"\[SHADOW=*([0-9]*),*(#*[a-z0-9]*),*([0-9]*)\](.[^\[]*)\[\/SHADOW]", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<table width=$1 style=""filter:shadow(color=$2, strength=$3)"">$4</table>");

            my = new Regex(@"\[GLOW=*([0-9]*),*(#*[a-z0-9]*),*([0-9]*)\](.[^\[]*)\[\/GLOW]", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<table width=$1 style=""filter:glow(color=$2, strength=$3)"">$4</table>");

            my = new Regex(@"(\[MOVE\])(.*)(\[\/MOVE\])", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<MARQUEE scrollamount=3>$2</marquee>");

            my = new Regex(@"(\[QUOTE\])(.*)(\[\/QUOTE\])", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<table cellpadding=0 cellspacing=0 border=0 WIDTH=94% bgcolor=#BDC2C8 align=center><tr><td><table width=100% cellpadding=5 cellspacing=1 border=0><TR><TD>$2</table></table><br>");

            my = new Regex(@"(\[ALIGN=(.[^\[]*)\])(.*)(\[\/ALIGN\])", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<div ALIGN=$2>$3</div>");

            my = new Regex(@"(\[FACE=(.[^\[]*)\])(.[^\[]*)(\[\/FACE\])", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<font FACE=$2>$3</font>");

            my = new Regex(@"(\[COLOR=(.[^\[]*)\])(.[^\[]*)(\[\/COLOR\])", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<font COLOR=$2>$3</font>");

            my = new Regex(@"(\[CODE\])(.[^\[]*)(\[\/CODE\])", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<table width='100%' border='0' cellspacing='0' cellpadding='6' bgcolor=''><td><b>以下内容为程序代码:</b><br>$2</td></table>");

            my = new Regex(@"(\[HTML\])(.[^\[]*)(\[\/HTML\])", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<table width='100%' border='0' cellspacing='0' cellpadding='6' bgcolor=''><td><b>以下内容为程序代码:</b><br>$2</td></table>");

            my = new Regex(@"[^>=""](MMS://[A-Za-z0-9\.\/=\?%\-&_~`@':+!]+)", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<a target=_blank href=$1>$1</a>");

            my = new Regex(@"(MMS://[A-Za-z0-9\./=\?%\-&_~`@':+!]+)$", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<a target=_blank href=$1>$1</a>");

            my = new Regex(@"^(MMS://[A-Za-z0-9\./=\?%\-&_~`@':+!]+)", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<a target=_blank href=$1>$1</a>");

            my = new Regex(@"[^>=""](RTSP://[A-Za-z0-9\.\/=\?%\-&_~`@':+!]+)", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<a target=_blank href=$1>$1</a>");

            my = new Regex(@"(RTSP://[A-Za-z0-9\./=\?%\-&_~`@':+!]+)$", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<a target=_blank href=$1>$1</a>");

            my = new Regex(@"^(RTSP://[A-Za-z0-9\./=\?%\-&_~`@':+!]+)", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<a target=_blank href=$1>$1</a>");

            my = new Regex(@"[^>=""](FTP://[A-Za-z0-9\.\/=\?%\-&_~`@':+!]+)", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<a target=_blank href=$1>$1</a>");

            my = new Regex(@"(FTP://[A-Za-z0-9\./=\?%\-&_~`@':+!]+)$", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<a target=_blank href=$1>$1</a>");

            my = new Regex(@"^(FTP://[A-Za-z0-9\./=\?%\-&_~`@':+!]+)", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<a target=_blank href=$1>$1</a>");

            my = new Regex(@"[^>=""](HTTP://[A-Za-z0-9\./=\?%\-&_~`@':+!]+)", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<a target=_blank href=$1>$1</a>");

            my = new Regex(@"(HTTP://[A-Za-z0-9\./=\?%\-&_~`@':+!]+)$", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<a target=_blank href=$1>$1</a>");

            my = new Regex(@"^(HTTP://[A-Za-z0-9\./=\?%\-&_~`@':+!]+)", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<a target=_blank href=$1>$1</a>");

            my = new Regex(@"(\[EMAIL=(\S+\@.[^\[]*)\])(.[^\[]*)(\[\/EMAIL\])", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<A HREF=""mailto:$2"" TARGET=_blank>$3</A>");

            my = new Regex(@"(\[EMAIL\])(\S+\@.[^\[]*)(\[\/EMAIL\])", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<A HREF=""mailto:$2"">$2</A>");

            //my = new Regex(@"(\[URL=(.[^\[]*)\])(.[^\[]*)(\[\/URL\])", RegexOptions.IgnoreCase);
            //str = my.Replace(str, @"<A HREF=""http://$2"" TARGET=_blank>$3</A>");

            my = new Regex(@"(\[URL=(http:\/\/.[^\[]*)\])(.[^\[]*)(\[\/URL\])", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<A HREF=""$2"" TARGET=_blank>$3</A>");

            my = new Regex(@"(\[URL\])(.[^\[]*)(\[\/URL\])", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<A HREF=""http://$2"" TARGET=_blank>$2</A>");

            my = new Regex(@"(\[URL\])(http:\/\/.[^\[]*)(\[\/URL\])", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<A HREF=""$2"" TARGET=_blank>$2</A>");

            my = new Regex(@"(\[UPLOAD=(.[^\[]*)\])(.[^\[]*)(\[\/UPLOAD\])", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<br><IMG SRC=""pic/$2.gif"" border=0>此主题相关图片如下：<br><A HREF=""$3"" TARGET=_blank><IMG SRC=""$3"" border=0 alt=按此在新窗口浏览图片 onload=""javascript:if(this.width>screen.width-333)this.width=screen.width-333""></A>");

            my = new Regex(@"(\[RAR\])(.[^\[]*)(\[\/RAR\])", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<br><IMG SRC=pic/rar.gif border=0> <a href=""$2"">点击下载该文件</a>");

            my = new Regex(@"(\[ZIP\])(.[^\[]*)(\[\/ZIP\])", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<br><IMG SRC=pic/zip.gif border=0> <a href=""$2"">点击下载该文件</a>");

            my = new Regex(@"(\[FLASH\])(.[^\[]*)(\[\/FLASH\])", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<OBJECT codeBase=http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=4,0,2,0 classid=clsid:D27CDB6E-AE6D-11cf-96B8-444553540000 width=500 height=400><PARAM NAME=movie VALUE=""$2""><PARAM NAME=quality VALUE=high><embed src=""$2"" quality=high pluginspage='http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash' type='application/x-shockwave-flash' width=500 height=400>$2</embed></OBJECT>");

            my = new Regex(@"\[RM=*([0-9]*),*([0-9]*)\](.[^\[]*)\[\/RM]", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<OBJECT classid=clsid:CFCDAA03-8BE4-11cf-B84B-0020AFBBCCFA class=OBJECT id=RAOCX width=$1 height=$2><PARAM NAME=SRC VALUE=$3><PARAM NAME=CONSOLE VALUE=Clip1><PARAM NAME=CONTROLS VALUE=imagewindow><PARAM NAME=AUTOSTART VALUE=true></OBJECT><br><OBJECT classid=CLSID:CFCDAA03-8BE4-11CF-B84B-0020AFBBCCFA height=32 id=video2 width=$1><PARAM NAME=SRC VALUE=$3><PARAM NAME=AUTOSTART VALUE=-1><PARAM NAME=CONTROLS VALUE=controlpanel><PARAM NAME=CONSOLE VALUE=Clip1></OBJECT>");

            my = new Regex(@"\[MP=*([0-9]*),*([0-9]*)\](.[^\[]*)\[\/MP]", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<object align=middle classid=CLSID:22d6f312-b0f6-11d0-94ab-0080c74c7e95 class=OBJECT id=MediaPlayer width=$1 height=$2 ><param name=ShowStatusBar value=-1><param name=Filename value=$3><embed type=application/x-oleobject codebase=http://activex.microsoft.com/activex/controls/mplayer/en/nsmp2inf.cab#Version=5,1,52,701 flename=mp src=$3  width=$1 height=$2></embed></object>");

            my = new Regex(@"\[QT=*([0-9]*),*([0-9]*)\](.[^\[]*)\[\/QT]", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<embed src=$3 width=$1 height=$2 autoplay=true loop=false controller=true playeveryframe=false cache=false scale=TOFIT bgcolor=#000000 kioskmode=false targetcache=false pluginspage=http://www.apple.com/quicktime/>");

            my = new Regex(@"\[DIR=*([0-9]*),*([0-9]*)\](.[^\[]*)\[\/DIR]", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<object classid=clsid:166B1BCA-3F9C-11CF-8075-444553540000 codebase=http://download.macromedia.com/pub/shockwave/cabs/director/sw.cab#version=7,0,2,0 width=$1 height=$2><param name=src value=$3><embed src=$3 pluginspage=http://www.macromedia.com/shockwave/download/ width=$1 height=$2></embed></object>");

            my = new Regex(@"(\[jpg\])(.[^\[]*)(\[\/jpg\])", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<a href=""$2"" target=_blank><IMG SRC=""$2"" border=0 alt=按此在新窗口浏览图片 onload=""javascript:if(this.width>screen.width-333)this.width=screen.width-333""></a>");

            my = new Regex(@"(\[gif\])(.[^\[]*)(\[\/gif\])", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<a href=""$2"" target=_blank><IMG SRC=""$2"" border=0 alt=按此在新窗口浏览图片 onload=""javascript:if(this.width>screen.width-333)this.width=screen.width-333""></a>");

            my = new Regex(@"(\[IMG\])(.[^\[]*)(\[\/IMG\])", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<a href=""$2"" target=_blank><IMG SRC=""$2"" border=0 alt=按此在新窗口浏览图片 onload=""javascript:if(this.width>screen.width-333)this.width=screen.width-333""></a>");

            //文字字体UBB
            my = new Regex(@"\[font=(.[^\[]*)\](.[^\[]*)(\[\/font\])", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<font face=""$1"">$2</font>");




            //文字字体UBB
            my = new Regex(@"\[font=(.[^\[]*)\](.[^\[]*)(\[\/font\])", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<font face=""$1"">$2</font>");

            //字体倾斜
            my = new Regex(@"\[i\]((.|\n)*?)\[\/i\]", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<i>$1</i>");

            //字体粗体
            my = new Regex(@"\[B\]((.|\n)*?)\[\/B\]", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<b>$1</b>");

            //字体下划线
            my = new Regex(@"\[U\]((.|\n)*?)\[\/U\]", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<u>$1</u>");


            //浮动代码
            my = new Regex(@"\[float=(left|right)\](.[^\[]*)(\[\/float\])", RegexOptions.IgnoreCase);
            str = my.Replace(str, @"<div style=""float:$1"" class=""floatcode"">$2</div>");

            ////        my = new Regex(@"\[img\](http|https|ftp):\/\/(.[^\[]*)\[\/img\]", RegexOptions.IgnoreCase);
            ////str = my.Replace(str, @"<a onfocus=""this.blur()"" href=""$1://$2"" target=new><img src=""$1://$2"" border=""0"" alt=""按此在新窗口浏览图片"" onload=""javascript:if(this.width>screen.width-333)this.width=screen.width-333""></a>");

            //my = new Regex(@"\[img=*([0-9]*),*([0-9]*)\](http|https|ftp):\/\/(.[^\[]*)\[\/img\]", RegexOptions.IgnoreCase);
            //str = my.Replace(str, @"<a onfocus=""this.blur()"" href=""$3://$4"" target=new><img src=""$3://$4"" border=""0""  width=""$1"" heigh=""$2"" alt=""按此在新窗口浏览图片"" onload=""javascript:if(this.width>screen.width-333)this.width=screen.width-333""></a>");
            //////链接UBB
            ////my = new Regex(@"(\[url\])(.[^\[]*)(\[url\])", RegexOptions.IgnoreCase);
            ////str = my.Replace(str, @"<a href=""$2"" target=""new"">$2</a>");

            ////my = new Regex(@"\[url=(.[^\[]*)\]", RegexOptions.IgnoreCase);
            ////str = my.Replace(str, @"<a href=""$1"" target=""new"">");
            //////邮箱UBB
            ////my = new Regex(@"(\[email\])(.*?)(\[\/email\])", RegexOptions.IgnoreCase);
            ////str = my.Replace(str, @"<img align=""absmiddle"" ""src=images/common/bb_email.gif""><a href=""mailto:$2"">$2</a>");
            ////my = new Regex(@"\[email=(.[^\[]*)\]", RegexOptions.IgnoreCase);
            ////str = my.Replace(str, @"<img align=""absmiddle"" src=""images/common/bb_email.gif""><a href=""mailto:$1"" target=""new"">");
            ////QQ号码UBB
            //my = new Regex(@"\[qq]([0-9]*)\[\/qq\]", RegexOptions.IgnoreCase);
            //str = my.Replace(str, @"<a target=""new"" href=""tencent://message/?uin=$1&Site=http://www.52515.net&Menu=yes""><img border=""0"" src=""http://wpa.qq.com/pa?p=4:$1:4"" alt=""点击这里给我发消息""></a>");
            ////颜色UBB
            //my = new Regex(@"\[color=(.[^\[]*)\]", RegexOptions.IgnoreCase);
            //str = my.Replace(str, @"<font color=""$1"">");
            ////文字大小UBB
            //my = new Regex(@"\[size=([0-9]*)\]", RegexOptions.IgnoreCase);
            //str = my.Replace(str, @"<font size=""$1"">");
            //my = new Regex(@"\[size=([0-9]*)pt\]", RegexOptions.IgnoreCase);
            //str = my.Replace(str, @"<font size=""$1"">");
            //my = new Regex(@"\[size=([0-9]*)px\]", RegexOptions.IgnoreCase);
            //str = my.Replace(str, @"<font size=""$1"">");
            ////文字对齐方式UBB
            //my = new Regex(@"\[align=(center|left|right)\]", RegexOptions.IgnoreCase);
            //str = my.Replace(str, @"<div align=""$1"">");
            ////表格UBB
            //my = new Regex(@"\[table=(.[^\[]*),(.*?)\]", RegexOptions.IgnoreCase);
            //str = my.Replace(str, @"<table width=""$1"" border=""1"" style=""border-collapse:collapse;background:$2"">");


            //my = new Regex(@"\[table=(.[^\[]*)\]", RegexOptions.IgnoreCase);
            //str = my.Replace(str, @"<table width=""$1"" border=""1"" style=""border-collapse:collapse;"">");
            ////表格UBB2
            //my = new Regex(@"\[td=([0-9]*),([0-9]*),(.*?)\]", RegexOptions.IgnoreCase);
            //str = my.Replace(str, @"<td colspan=""$1"" rowspan=""$2"" width=""$3"">");
            //my = new Regex(@"\[td=([0-9]*),([0-9]*)\]", RegexOptions.IgnoreCase);
            //str = my.Replace(str, @"<td colspan=""$1"" rowspan=""$2"">");

            ////字体倾斜
            //my = new Regex(@"\[i\]((.|\n)*?)\[\/i\]", RegexOptions.IgnoreCase);
            //str = my.Replace(str, @"<i>$1</i>");
            ////浮动代码
            //my = new Regex(@"\[float=(left|right)\]", RegexOptions.IgnoreCase);
            //str = my.Replace(str, @"<div style=""float:$1"" class=""floatcode"">");

            ////media
            //my = new Regex(@"\[media=(ra),*([0-9]*),*([0-9]*),*([0-1]*)\](http://.[^\[]*)\[\/media\]", RegexOptions.IgnoreCase);
            //str = my.Replace(str, @"<object classid=""clsid:CFCDAA03-8BE4-11CF-B84B-0020AFBBCCFA"" width=""$2"" height=""32""><param name=""autostart"" value=""$4"" /><param name=""src"" value=""$5"" /><param name=""controls"" value=""controlpanel"" /><param name=""console"" value=""mediaid"" /><embed src=""$5"" type=""audio/x-pn-realaudio-plugin"" width=""$2"" height=""32""></embed></object>");

            //my = new Regex(@"\[media=(rm|rmvb),*([0-9]*),*([0-9]*),*([0-1]*)\](http://.[^\[]*)\[\/media\]", RegexOptions.IgnoreCase);
            //str = my.Replace(str, @"<object classid=""clsid:CFCDAA03-8BE4-11cf-B84B-0020AFBBCCFA"" width=""$2"" height=""$3""><param name=""autostart"" value=""$4""/><param name=""src"" value=""$5""/><param name=""controls"" value=""imagewindow""/><param name=""console"" value=""mediaid""/><embed src=""$5"" type=""audio/x-pn-realaudio-plugin"" controls=""IMAGEWINDOW"" console=""mediaid"" width=""$2"" height=""$3""></embed></object><br/><object classid=""clsid:CFCDAA03-8BE4-11CF-B84B-0020AFBBCCFA"" width=""$2"" height=""32""><param name=""src"" value=""$5"" /><param name=""controls"" value=""controlpanel"" /><param name=""console"" value=""mediaid"" /><embed src=""$5"" type=""audio/x-pn-realaudio-plugin"" controls=""ControlPanel""  console=""mediaid"" width=""$2"" height=""32""></embed></object>");

            //my = new Regex(@"\[media=(wma),*([0-9]*),*([0-9]*),*([0-1]*)\](http://.[^\[]*)\[\/media\]", RegexOptions.IgnoreCase);
            //str = my.Replace(str, @"<object classid=""clsid:6BF52A52-394A-11d3-B153-00C04F79FAA6"" width=""$2"" height=""64""><param name=""autostart"" value=""$4"" /><param name=""url"" value=""$5"" /><embed src=""$5"" autostart=""$4"" type=""audio/x-ms-wma"" width=""$2"" height=""64""></embed></object>");

            //my = new Regex(@"\[media=(mp3),*([0-9]*),*([0-9]*),*([0-1]*)\](http://.[^\[]*)\[\/media\]", RegexOptions.IgnoreCase);
            //str = my.Replace(str, @"<object classid=""clsid:6BF52A52-394A-11d3-B153-00C04F79FAA6"" width=""$2"" height=""64""><param name=""autostart"" value=""$4"" /><param name=""url"" value=""$5"" /><embed src=""$5"" autostart=""$4"" type=""application/x-mplayer2"" width=""$2"" height=""64""></embed></object>");

            //my = new Regex(@"\[media=(wmv),*([0-9]*),*([0-9]*),*([0-1]*)\](http://.[^\[]*)\[\/media\]", RegexOptions.IgnoreCase);
            //str = my.Replace(str, @"<object classid=""clsid:6BF52A52-394A-11d3-B153-00C04F79FAA6"" width=""$2"" height=""$3""><param name=""autostart"" value=""$4"" /><param name=""url"" value=""$5"" /><embed src=""$5"" autostart=""$4"" type=""video/x-ms-wmv"" width=""$2"" height=""$3""></embed></object>");

            //my = new Regex(@"\[media=(mov),*([0-9]*),*([0-9]*),*([0-1]*)\](http://.[^\[]*)\[\/media\]", RegexOptions.IgnoreCase);
            //str = my.Replace(str, @"<object classid=""clsid:02BF25D5-8C17-4B23-BC80-D3488ABDDC6B"" width=""$2"" height=""$3""><param name=""autostart"" value=""$4"" /><param name=""src"" value=""$5"" /><embed controller=""true"" width=""$2"" height=""$3"" src=""$5"" autostart=""$4""></embed></object>");



            return str;
        }


        public static string TransStringTool(string aTransString)
        {
            string returnString = "";
            //aTransString = aTransString.Replace("<BR/>", "\r\n");
            //aTransString = aTransString.Replace("<br/>", "\r\n");
            //aTransString = aTransString.Replace("<br />", "\r\n");
            //aTransString = aTransString.Replace("<", "&lt;");
            //aTransString = aTransString.Replace(">", "&gt;");

            returnString = aTransString;
            return returnString;

        }

        private string Ubbcode(string strcontent)
        {
            RegexOptions ro = RegexOptions.IgnoreCase | RegexOptions.Singleline;
            //dim re
            //Set re=new RegExp
            //re.IgnoreCase =true
            //re.Global=True
            strcontent = @"<style>*{ word-break:break-all;word-wrap:break-word;}.blockcode, .quote { font-size: 12px; margin: 10px 20px; border: solid #CAD9EA; border-width: 4px 1px 1px; background: #FFF url(""images/portalbox_bg.gif""); background-repeat: repeat-x; background-position: 0 0; overflow: hidden; }.blockcode h5, .quote h5 { border: 1px solid; border-color: #FFF #FFF #CAD9EA #FFF; height:20px;line-height: 26px; padding-left: 5px; color: #666; }.blockcode code, .quote blockquote { margin: 1em 1em 1em 3em; line-height: 1.6em; }.blockcode code { font: 14px/1.4em ""Courier New"", Courier, monospace; display: block; padding: 5px; }.blockcode em { float: right; line-height: 1em; padding: 10px 10px 0 0; color: #666; font-size: 12px; cursor: pointer; padding-top: 5px; }.floatcode{padding:5px;font-size: 12px; border: solid #CAD9EA; border-width: 1px; background: #FFF ; overflow: hidden; clear:both;}</style>" + HttpUtility.HtmlEncode(strcontent);
            //'strcontent=Replace(strcontent,"file:","file :")
            //'strcontent=Replace(strcontent,"files:","files :")
            //'strcontent=Replace(strcontent,"script:","script :")
            //'strcontent=Replace(strcontent,"js:","js :")

            //'图片UBB
            strcontent = Regex.Replace(strcontent, @"\[img\](http|https|ftp):\/\/(.[^\[]*)\[\/img\]",
    @"<a onfocus=""this.blur()"" href=""$1://$2"" target=new><img src=""$1://$2"" border=""0"" alt=""按此在新窗口浏览图片"" onload=""javascript:if(this.width>screen.width-333)this.width=screen.width-333""></a>",
    ro);

            strcontent = Regex.Replace(strcontent, @"\[img=*([0-9]*),*([0-9]*)\](http|https|ftp):\/\/(.[^\[]*)\[\/img\]",
    @"<a onfocus=""this.blur()"" href=""$3://$4"" target=new><img src=""$3://$4"" border=""0""  width=""$1"" heigh=""$2"" alt=""按此在新窗口浏览图片"" onload=""javascript:if(this.width>screen.width-333)this.width=screen.width-333""></a>",
         ro);

            //'链接UBB
            strcontent = Regex.Replace(strcontent, @"(\[url\])(.[^\[]*)(\[url\])",
            @"<a href=""$2"" target=""new"">$2</a>", ro);

            strcontent = Regex.Replace(strcontent, @"\[url=(.[^\[]*)\]",
            @"<a href=""$1"" target=""new"">", ro);
            //'邮箱UBB
            strcontent = Regex.Replace(strcontent, @"(\[email\])(.*?)(\[\/email\])",
            @"<img align=""absmiddle"" ""src=/public/js/common/bb_email.gif""><a href=""mailto:$2"">$2</a>", ro);

            strcontent = Regex.Replace(strcontent, @"\[email=(.[^\[]*)\]",
            @"<img align=""absmiddle"" src=""/public/js/common/bb_email.gif""><a href=""mailto:$1"" target=""new"">", ro);
            //'QQ号码UBB
            strcontent = Regex.Replace(strcontent, @"\[qq]([0-9]*)\[\/qq\]",
            @"<a target=""new"" href=""tencent://message/?uin=$1&Site=http://www.52515.net&Menu=yes""><img border=""0"" src=""http://wpa.qq.com/pa?p=4:$1:4"" alt=""点击这里给我发消息""></a>", ro);
            //'颜色UBB
            strcontent = Regex.Replace(strcontent, @"\[color=(.[^\[]*)\]",
            @"<font color=""$1"">", ro);
            //'文字字体UBB
            strcontent = Regex.Replace(strcontent, @"\[font=(.[^\[]*)\]",
            @"<font face=""$1"">", ro);
            //'文字大小UBB
            strcontent = Regex.Replace(strcontent, @"\[size=([0-9]*)\]",
            @"<font style=""font-size:$1"">", ro);
            strcontent = Regex.Replace(strcontent, @"\[size=([0-9]*)pt\]",
            @"<font style=""font-size:$1pt"">", ro);
            strcontent = Regex.Replace(strcontent, @"\[size=([0-9]*)px\]",
            @"<font style=""font-size:$1px"">", ro);
            //'文字对齐方式UBB
            strcontent = Regex.Replace(strcontent, @"\[align=(center|left|right)\]",
            @"<div align=""$1"">", ro);
            //'表格UBB
            strcontent = Regex.Replace(strcontent, @"\[table=(.[^\[]*),(.*?)\]",
            @"<table width=""$1"" border=""1"" style=""border-collapse:collapse;background:$2"">", ro);


            strcontent = Regex.Replace(strcontent, @"\[table=(.[^\[]*)\]",
            @"<table width=""$1"" border=""1"" style=""border-collapse:collapse;"">", ro);
            //'表格UBB2
            strcontent = Regex.Replace(strcontent, @"\[td=([0-9]*),([0-9]*),(.*?)\]",
            @"<td colspan=""$1"" rowspan=""$2"" width=""$3"">", ro);
            strcontent = Regex.Replace(strcontent, @"\[td=([0-9]*),([0-9]*)\]",
            @"<td colspan=""$1"" rowspan=""$2"">", ro);

            //'字体倾斜
            strcontent = Regex.Replace(strcontent, @"\[i\]((.|\n)*?)\[\/i\]",
            @"<i>$1</i>", ro);
            //'浮动代码
            strcontent = Regex.Replace(strcontent, @"\[float=(left|right)\]",
            @"<div style=""float:$1"" class=""floatcode"">", ro);

            //'media
            strcontent = Regex.Replace(strcontent, @"\[media=(ra),*([0-9]*),*([0-9]*),*([0-1]*)\](http://.[^\[]*)\[\/media\]",
            @"<object classid=""clsid:CFCDAA03-8BE4-11CF-B84B-0020AFBBCCFA"" width=""$2"" height=""32""><param name=""autostart"" value=""$4"" /><param name=""src"" value=""$5"" /><param name=""controls"" value=""controlpanel"" /><param name=""console"" value=""mediaid"" /><embed src=""$5"" type=""audio/x-pn-realaudio-plugin"" width=""$2"" height=""32""></embed></object>", ro);

            strcontent = Regex.Replace(strcontent, @"\[media=(rm|rmvb),*([0-9]*),*([0-9]*),*([0-1]*)\](http://.[^\[]*)\[\/media\]",
            @"<object classid=""clsid:CFCDAA03-8BE4-11cf-B84B-0020AFBBCCFA"" width=""$2"" height=""$3""><param name=""autostart"" value=""$4""/><param name=""src"" value=""$5""/><param name=""controls"" value=""imagewindow""/><param name=""console"" value=""mediaid""/><embed src=""$5"" type=""audio/x-pn-realaudio-plugin"" controls=""IMAGEWINDOW"" console=""mediaid"" width=""$2"" height=""$3""></embed></object><br/><object classid=""clsid:CFCDAA03-8BE4-11CF-B84B-0020AFBBCCFA"" width=""$2"" height=""32""><param name=""src"" value=""$5"" /><param name=""controls"" value=""controlpanel"" /><param name=""console"" value=""mediaid"" /><embed src=""$5"" type=""audio/x-pn-realaudio-plugin"" controls=""ControlPanel""  console=""mediaid"" width=""$2"" height=""32""></embed></object>", ro);

            strcontent = Regex.Replace(strcontent, @"\[media=(wma),*([0-9]*),*([0-9]*),*([0-1]*)\](http://.[^\[]*)\[\/media\]",
            @"<object classid=""clsid:6BF52A52-394A-11d3-B153-00C04F79FAA6"" width=""$2"" height=""64""><param name=""autostart"" value=""$4"" /><param name=""url"" value=""$5"" /><embed src=""$5"" autostart=""$4"" type=""audio/x-ms-wma"" width=""$2"" height=""64""></embed></object>", ro);

            strcontent = Regex.Replace(strcontent, @"\[media=(mp3),*([0-9]*),*([0-9]*),*([0-1]*)\](http://.[^\[]*)\[\/media\]",
            @"<object classid=""clsid:6BF52A52-394A-11d3-B153-00C04F79FAA6"" width=""$2"" height=""64""><param name=""autostart"" value=""$4"" /><param name=""url"" value=""$5"" /><embed src=""$5"" autostart=""$4"" type=""application/x-mplayer2"" width=""$2"" height=""64""></embed></object>", ro);

            strcontent = Regex.Replace(strcontent, @"\[media=(wmv),*([0-9]*),*([0-9]*),*([0-1]*)\](http://.[^\[]*)\[\/media\]",
            @"<object classid=""clsid:6BF52A52-394A-11d3-B153-00C04F79FAA6"" width=""$2"" height=""$3""><param name=""autostart"" value=""$4"" /><param name=""url"" value=""$5"" /><embed src=""$5"" autostart=""$4"" type=""video/x-ms-wmv"" width=""$2"" height=""$3""></embed></object>", ro);

            strcontent = Regex.Replace(strcontent, @"\[media=(mov),*([0-9]*),*([0-9]*),*([0-1]*)\](http://.[^\[]*)\[\/media\]",
            @"<object classid=""clsid:02BF25D5-8C17-4B23-BC80-D3488ABDDC6B"" width=""$2"" height=""$3""><param name=""autostart"" value=""$4"" /><param name=""src"" value=""$5"" /><embed controller=""true"" width=""$2"" height=""$3"" src=""$5"" autostart=""autostart""></embed></object>", ro);


            //strcontent = strcontent.Replace("\r\n", "<BR/>");
            //strcontent = strcontent.Replace("\r\n", "<br/>");
            strcontent = strcontent.Replace("\r\n", "<br />");

            //re.pattern="\[code\]((.|\n)*?)\[\/code\]"
            //Set tempcodes=re.Execute(strcontent)
            //For i=0 To tempcodes.count-1
            //  re.pattern="<BR/>"
            //  tempcode=Replace(tempcodes(i),"<BR/>",vbcrlf)
            //  strcontent=replace(strcontent,tempcodes(i),tempcode)
            //next

            var searcharray = new string[] { "[/url]", "[/email]", "[/color]", "[/size]", "[/font]", "[/align]", "[b]", "[/b]", "[u]", "[/u]", "[list]", "[list=1]", "[list=a]", "[list=A]", "[*]", "[/list]", "[indent]", "[/indent]", "[code]", "[/code]", "[quote]", "[/quote]", "[free]", "[/free]", "[hide]", "[/hide]", "[tr]", "[td]", "[/td]", "[/tr]", "[/table]", "[/float]" };
            var replacearray = new string[]{"</a>","</a>","</font>", "</font>", "</font>", "</div>", "<b>", "</b>","<u>", "</u>", "<ul>"
        , "<ol type=1>", "<ol type=a>","<ol type=A>", "<li>", "</ul></ol>", "<blockquote>", "</blockquote>"
        ,@"<div><textarea name=""codes"" id=""codes"" rows=""14"" cols=""60"">"
        ,@"</textarea><br/><input type=""button"" value=""运行代码"" onclick=""RunCode()"" accesskey=""r""> <input type=""button"" value=""复制代码"" onclick=""CopyCode()"" accesskey=""c""><input type=""button"" value=""另存代码"" onclick=""SaveCode()"" accesskey=""s""> <input type=""button"" value=""跳&nbsp;&nbsp;转"" onclick=""Goto(prompt('请输入要跳转到第几行？','1'))""  accesskey=""g""></div>"
        ,@"<div class=""quote""><h5>引用:</h5><blockquote>","</blockquote></div>"
        ,@"<div class=""quote""><h5>免费内容:</h5><blockquote>","</blockquote></div>"
        ,@"<div class=""quote""><h5>隐藏内容:</h5><blockquote>","</blockquote></div>"
        ,"<tr>","<td>&nbsp;","</td>","</tr>","</table>","</div>"};
            for (int i = 0; i < searcharray.Length; i++)
            {
                strcontent = strcontent.Replace(searcharray[i], replacearray[i]);
            }
            //For i=0 To UBound(searcharray)
            //    strcontent=replace(strcontent,searcharray(i),replacearray(i))
            //next
            //set re=Nothing
            return strcontent;
        }
    }

}
