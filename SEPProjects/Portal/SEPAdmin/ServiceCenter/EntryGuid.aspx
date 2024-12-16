<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EntryGuid.aspx.cs" Inherits="SEPAdmin.ServiceCenter.EntryGuid" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>在职指引</title>
    <link href="style.css" rel="stylesheet" type="text/css" />
    <%--        <script type="text/javascript" src="js/jquery-1.2.6.js"></script>

    <script type="text/javascript" src="js/jquery.blockUI.js"></script>

    <script type="text/javascript" src="js/dialog.js"></script>--%>

    <script type="text/javascript" src="js/artDialog.js?skin=default"></script>

    <script type="text/javascript" src="js/iframeTools.js"></script>

</head>
<body>

    <script type="text/javascript">
        $(document).ready(function () {
            var Request = new Object();
            Request = GetRequest();
            index = Request["index"];
            if (index == 6) {
                MenuClick(6);
            }
        });

        function GetRequest() {

            var url = location.search;
            var theRequest = new Object();
            if (url.indexOf("?") != -1) {
                var str = url.substr(1);
                strs = str.split("&");
                for (var i = 0; i < strs.length; i++) {
                    theRequest[strs[i].split("=")[0]] = (strs[i].split("=")[1]);
                }
            }
            return theRequest;
        }

        function MenuClick(menuIndex) {
            var td1 = document.getElementById("td1");
            var td2 = document.getElementById("td2")
            var td3 = document.getElementById("td3");
            var td4 = document.getElementById("td4");
            var td5 = document.getElementById("td5");
            var td6 = document.getElementById("td6");
            var td7 = document.getElementById("td7");
            var tdcity = document.getElementById("tdcity");

            var tr1 = document.getElementById("tr1");
            var tr2 = document.getElementById("tr2")
            var tr3 = document.getElementById("tr3");
            var tr4 = document.getElementById("tr4");
            var tr41 = document.getElementById("tr41");
            var tr42 = document.getElementById("tr42");
            var tr5 = document.getElementById("tr5");
            var tr6 = document.getElementById("tr6");
            var tr7 = document.getElementById("tr7");

            var lbldir = document.getElementById("lbldir");

            tdcity.style.display = "none";

            if (menuIndex == 1) {
                td1.className = "menu-orag";
                td2.className = "menu-normal";
                td3.className = "menu-normal menu-line";
                td4.className = "menu-normal menu-line";
                td5.className = "menu-normal menu-line";
                td6.className = "menu-normal menu-line";
                td7.className = "menu-normal menu-line";
                tr1.style.display = "block";
                tr2.style.display = "none";
                tr3.style.display = "none";
                tr4.style.display = "none";
                tr41.style.display = "none";
                tr42.style.display = "none";
                tr5.style.display = "none";
                tr6.style.display = "none";
                tr7.style.display = "none";
                lbldir.innerHTML = "福利政策";
                tdcity.style.display = "block";
            }
            else if (menuIndex == 2) {
                td1.className = "menu-normal";
                td2.className = "menu-orag";
                td3.className = "menu-normal";
                td4.className = "menu-normal menu-line";
                td5.className = "menu-normal menu-line";
                td6.className = "menu-normal menu-line";
                td7.className = "menu-normal menu-line";
                tr1.style.display = "none";
                tr2.style.display = "block";
                tr3.style.display = "none";
                tr4.style.display = "none";
                tr41.style.display = "none";
                tr5.style.display = "none";
                tr6.style.display = "none";
                tr7.style.display = "none";
                tr42.style.display = "none";
                lbldir.innerHTML = "员工手册";
                window.location.href = 'EmpManual.aspx';
            }

            else if (menuIndex == 3) {
                td1.className = "menu-normal menu-line";
                td2.className = "menu-normal  menu-line";
                td3.className = "menu-orag";
                td4.className = "menu-normal";
                td5.className = "menu-normal menu-line";
                td6.className = "menu-normal menu-line";
                td7.className = "menu-normal menu-line";
                tr1.style.display = "none";
                tr2.style.display = "none";
                tr3.style.display = "block";
                tr4.style.display = "none";
                tr41.style.display = "none";
                tr42.style.display = "none";
                tr5.style.display = "none";
                tr6.style.display = "none";
                tr7.style.display = "none";
                lbldir.innerHTML = "人事Q&amp;A";
            }
            else if (menuIndex == 4) {
                td1.className = "menu-normal menu-line";
                td2.className = "menu-normal menu-line";
                td3.className = "menu-normal menu-line";
                td6.className = "menu-normal menu-line";
                td7.className = "menu-normal menu-line";
                td4.className = "menu-orag";
                td5.className = "menu-normal";
                tr1.style.display = "none";
                tr2.style.display = "none";
                tr3.style.display = "none";
                tr4.style.display = "block";
                tr41.style.display = "none";
                tr42.style.display = "none";
                tr5.style.display = "none";
                tr6.style.display = "none";
                tr7.style.display = "none";
                lbldir.innerHTML = "行政Q&amp;A";
            }
            else if (menuIndex == 5) {
                td1.className = "menu-normal menu-line";
                td2.className = "menu-normal menu-line";
                td3.className = "menu-normal menu-line";
                td4.className = "menu-normal menu-line";
                td7.className = "menu-normal menu-line";
                td5.className = "menu-orag";
                td6.className = "menu-normal";
                tr1.style.display = "none";
                tr2.style.display = "none";
                tr3.style.display = "none";
                tr4.style.display = "none";
                tr41.style.display = "none";
                tr42.style.display = "none";
                tr5.style.display = "block";
                tr6.style.display = "none";
                tr7.style.display = "none";
                lbldir.innerHTML = "发票管理条例";
            }
            //else if (menuIndex == 6) {
            //    td1.className = "menu-normal menu-line";
            //    td2.className = "menu-normal menu-line";
            //    td3.className = "menu-normal menu-line";
            //    td4.className = "menu-normal menu-line";
            //    td5.className = "menu-normal menu-line";
            //    td6.className = "menu-orag";
            //    td7.className = "menu-normal";

            //    tr1.style.display = "none";
            //    tr2.style.display = "none";
            //    tr3.style.display = "none";
            //    tr4.style.display = "none";
            //    tr41.style.display = "none";
            //    tr42.style.display = "none";
            //    tr5.style.display = "none";
            //    tr7.style.display = "none";
            //    tr6.style.display = "block";
            //    lbldir.innerHTML = "公司治理制度汇编";
            //}
            //else if (menuIndex == 7) {
            //    td1.className = "menu-normal menu-line";
            //    td2.className = "menu-normal menu-line";
            //    td3.className = "menu-normal menu-line";
            //    td4.className = "menu-normal menu-line";
            //    td5.className = "menu-normal menu-line";
            //    td6.className = "menu-normal menu-line";
            //    td7.className = "menu-orag";
            //    tr1.style.display = "none";
            //    tr2.style.display = "none";
            //    tr3.style.display = "none";
            //    tr4.style.display = "none";
            //    tr41.style.display = "none";
            //    tr42.style.display = "none";
            //    tr5.style.display = "none";
            //    tr6.style.display = "none";
            //    tr7.style.display = "block";
            //    lbldir.innerHTML = "星言云汇印章管理制度";
            //}
        }

        function HRQA(index) {
            var tr1 = document.getElementById("tr1");
            var tr2 = document.getElementById("tr2")
            var tr3 = document.getElementById("tr3");
            var tr4 = document.getElementById("tr4");
            var tr41 = document.getElementById("tr41");
            var tr42 = document.getElementById("tr42");

            tr1.style.display = "none";
            tr2.style.display = "none";
            tr3.style.display = "none";
            tr4.style.display = "none";
            if (index == 1) {
                tr41.style.display = "block";
                tr42.style.display = "none";
            }
            else {
                tr41.style.display = "none";
                tr42.style.display = "block";
            }
        }

        function cityChange(index) {
            var tdbj = document.getElementById("tdbj");
            var tdsh = document.getElementById("tdsh")
            var tdgz = document.getElementById("tdgz");
            var tablebj = document.getElementById("tablebj");
            var tablesh = document.getElementById("tablesh");
            var tablegz = document.getElementById("tablegz");

            if (index == 1) {
                tdbj.style.backgroundImage = "url(images/0110_05.jpg)";
                tdbj.style.color = "#fff";
                tdsh.style.backgroundImage = "url(images/0110_03.jpg)";
                tdsh.style.color = "#4a4a49";
                tdgz.style.backgroundImage = "url(images/0110_03.jpg)";
                tdgz.style.color = "#4a4a49";
                tablebj.style.display = "block";
                tablesh.style.display = "none";
                tablegz.style.display = "none";
            }
            else if (index == 2) {
                tdbj.style.backgroundImage = "url(images/0110_03.jpg)";
                tdbj.style.color = "#4a4a49";
                tdsh.style.backgroundImage = "url(images/0110_05.jpg)";
                tdsh.style.color = "#4a4a49";
                tdgz.style.backgroundImage = "url(images/0110_03.jpg)";
                tdgz.style.color = "#fff";
                tablebj.style.display = "none";
                tablesh.style.display = "block";
                tablegz.style.display = "none";
            }
            else if (index == 3) {
                tdbj.style.backgroundImage = "url(images/0110_03.jpg)";
                tdsh.style.backgroundImage = "url(images/0110_03.jpg)";
                tdgz.style.backgroundImage = "url(images/0110_05.jpg)";
                tdbj.style.color = "#4a4a49";
                tdsh.style.color = "#4a4a49";
                tdgz.style.color = "#fff";
                tablebj.style.display = "none";
                tablesh.style.display = "none";
                tablegz.style.display = "block";
            }

        }
    </script>

    <form id="form1" runat="server">
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td style="padding: 30px 0 25px 40px">
                    <img src="images/staff-center-05.jpg" width="353" height="35" />
                </td>
                <td valign="bottom">
                    <img src="images/home.jpg"></img>
                    <a target='_parent' href='Default.aspx'>首页</a>&nbsp;&nbsp;&nbsp;
                <img src="images/portal.jpg"></img>
                    <a target='_top' href='http://xy.shunyagroup.com'>其他系统</a>
                </td>
            </tr>
        </table>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" background="images/staff-center-03.jpg"
            style="background-repeat: repeat-x; background-position: left top;">
            <tr>
                <td background="images/staff-center-12.jpg" style="background-position: right top; background-repeat: no-repeat; padding: 21px 30px;">
                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td width="220" height="420" valign="top" background="images/staff-center-04.jpg"
                                style="background-repeat: no-repeat; background-position: 5px top;">
                                <table width="210" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="padding: 20px 0 15px 25px;">
                                            <img src="images/staff-center-14.jpg" width="157" height="40" />
                                        </td>
                                    </tr>
                                </table>
                                <table width="210" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td height="280" valign="top">
                                            <table width="210" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td id="td2" class="menu-normal" onclick="MenuClick(2);" style="cursor: pointer;">
                                                        <a href="EmpManual.aspx"><span style="color: #787878;">员工手册</span></a>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td id="td1" class="menu-normal menu-line" onclick="MenuClick(1);">福利政策
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td id="td3" class="menu-normal menu-line" onclick="MenuClick(3);">人事Q&amp;A
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td id="td4" class="menu-normal menu-line" onclick="MenuClick(4);">行政Q&amp;A
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td id="td5" class="menu-normal menu-line" onclick="MenuClick(5);">发票管理条例
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td id="td6" class="menu-normal menu-line" onclick="MenuClick(6);">&nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td id="td7" class="menu-normal menu-line" onclick="MenuClick(7);">&nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <table width="210" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <table width="90%" border="0" align="center" cellpadding="2" cellspacing="0">
                                                <tr>
                                                    <td align="center">
                                                        <a href="Newcomer.aspx">
                                                            <img src="images/staff-center-09.jpg" width="24" height="22" /></a>
                                                    </td>
                                                    <td align="center">
                                                        <a href="EntryGuid.aspx">
                                                            <img src="images/staff-center-10.jpg" width="24" height="22" /></a>
                                                    </td>
                                                    <td align="center">
                                                        <a href="ExitGuid.aspx">
                                                            <img src="images/staff-center-11.jpg" width="32" height="22" /></a>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <a href="Newcomer.aspx" style="color: #b4b4b4;">入职指引</a>
                                                    </td>
                                                    <td align="center">
                                                        <a href="EntryGuid.aspx" style="color: #b4b4b4;">在职指引</a>
                                                    </td>
                                                    <td align="center">
                                                        <a href="ExitGuid.aspx" style="color: #b4b4b4;">离职指引</a>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td valign="top" style="padding: 45px 0 20px 30px;">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td>您目前所在的位置：<span style="color: #fe9000;">员工服务中心 - 在职指引 -
                                            <label id="lbldir">
                                            </label>
                                        </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td id="tdcity" style="display: none;">
                                            <table width="240" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td id="tdbj" onclick="cityChange(1);" style="background-image: url(images/0110_05.jpg); cursor: pointer; background-repeat: repeat-x; width: 80px; height: 28px; text-align: center; color: #fff;">
                                                        <a href="#">北京</a>
                                                    </td>
                                                    <td id="tdsh" onclick="cityChange(2);" style="background-image: url(images/0110_03.jpg); cursor: pointer; width: 80px; height: 28px; text-align: center;">上海
                                                    </td>
                                                    <td id="tdgz" onclick="cityChange(3);" style="background-image: url(images/0110_03.jpg); cursor: pointer; background-repeat: repeat-x; width: 80px; height: 28px; text-align: center;">
                                                        <a href="#">广州</a>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr id="tr1" style="display: none;">
                                        <td style="padding: 25px 0 0 0; line-height: 24px;">
                                            <table id="tablebj" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td style="padding: 5px 0 10px 15px; background-color: #F0F0F0; width: 30%; vertical-align: top;">
                                                        <table>
                                                            <tr align="center">
                                                                <td>
                                                                    <span class="font-color-orag"><strong>(一)社保、公积金类</strong></span>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <img src="images/staff-center-19.jpg" style="width: 12px; height: 14px; vertical-align: middle;" /><a
                                                                        target="_blank" style="cursor: pointer;" onclick="art.dialog.open('Welfare.aspx?QId=1', {title: '福利政策',width:820, height:480,background: '#BFBFBF',opacity: 0.7,lock:true});"><span
                                                                            style="color: #787878;">社保卡使用Q&amp;A</span></a>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="color: #EAEAEA;">
                                                                    <img src="images/staff-center-19.jpg" style="width: 12px; height: 14px; vertical-align: middle;" /><a
                                                                        target="_blank" style="cursor: pointer;" onclick="art.dialog.open('Welfare.aspx?QId=2', {title: '福利政策',width:820, height:480,background: '#BFBFBF',opacity: 0.7,lock:true});"><span
                                                                            style="color: #787878;">社保卡服务网点一览表</span></a>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="color: #EAEAEA;">
                                                                    <img src="images/ico-download.jpg" style="width: 12px; height: 14px; vertical-align: middle;" /><a
                                                                        target="_blank" style="cursor: pointer;" href="shebao.doc"><span style="color: #787878;">社保卡个人二次信息采集表补打申请</span></a>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="color: #EAEAEA;">
                                                                    <img src="images/staff-center-19.jpg" style="width: 12px; height: 14px; vertical-align: middle;" /><a
                                                                        target="_blank" style="cursor: pointer;" onclick="art.dialog.open('Welfare.aspx?QId=3', {title: '福利政策',width:820, height:480,background: '#BFBFBF',opacity: 0.7,lock:true});"><span
                                                                            style="color: #787878;">关于办理北京市社会保险及住房公积金</span></a>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <img src="images/staff-center-19.jpg" style="width: 12px; height: 14px; vertical-align: middle;" /><a
                                                                        target="_blank" style="cursor: pointer;" onclick="art.dialog.open('Welfare.aspx?QId=4', {title: '福利政策',width:820, height:480,background: '#BFBFBF',opacity: 0.7,lock:true});"><span
                                                                            style="color: #787878;">住房公积金支取所需材料</span></a>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <img src="images/staff-center-19.jpg" style="width: 12px; height: 14px; vertical-align: middle;" /><a
                                                                        target="_blank" style="cursor: pointer;" onclick="art.dialog.open('Welfare.aspx?QId=6', {title: '福利政策',width:820, height:480,background: '#BFBFBF',opacity: 0.7,lock:true});"><span
                                                                            style="color: #787878;">住房公积金联名卡办理须知</span></a>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <img src="images/staff-center-19.jpg" style="width: 12px; height: 14px; vertical-align: middle;" /><a
                                                                        target="_blank" style="cursor: pointer;" onclick="window.open('PersonalInsuranceInfo.aspx');"><span
                                                                            style="color: #787878;">五险一金计算器</span></a>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td style="width: 20px">&nbsp;
                                                    </td>
                                                    <td style="padding: 5px 0 10px 15px; background-color: #F0F0F0; width: 30%; vertical-align: top;">
                                                        <table style="vertical-align: top;">
                                                            <tr align="center">
                                                                <td>
                                                                    <span class="font-color-orag"><strong>(二)证件办理类</strong></span>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <img src="images/staff-center-19.jpg" style="width: 12px; height: 14px; vertical-align: middle;" /><a
                                                                        target="_blank" style="cursor: pointer;" onclick="art.dialog.open('Welfare.aspx?QId=7', {title: '福利政策',width:820, height:480,background: '#BFBFBF',opacity: 0.7,lock:true});"><span
                                                                            style="color: #787878;">星言云汇员工申报《北京市工作居住证》的办法</span></a>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <img src="images/staff-center-19.jpg" style="width: 12px; height: 14px; vertical-align: middle;" /><a
                                                                        target="_blank" style="cursor: pointer;" onclick="art.dialog.open('Welfare.aspx?QId=8', {title: '福利政策',width:820, height:480,background: '#BFBFBF',opacity: 0.7,lock:true});"><span
                                                                            style="color: #787878;">外籍人证件办理</span></a>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td style="width: 20px">&nbsp;
                                                    </td>
                                                    <td style="padding: 5px 0 10px 15px; background-color: #F0F0F0; width: 30%; vertical-align: top;">
                                                        <table>
                                                            <tr align="center">
                                                                <td>
                                                                    <span class="font-color-orag"><strong>(三)毕业生接收办理</strong></span>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <img src="images/staff-center-19.jpg" style="width: 12px; height: 14px; vertical-align: middle;" /><a
                                                                        target="_blank" style="cursor: pointer;" onclick="art.dialog.open('Welfare.aspx?QId=9', {title: '福利政策',width:820, height:480,background: '#BFBFBF',opacity: 0.7,lock:true});"><span
                                                                            style="color: #787878;">毕业生接收须知</span></a>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <img src="images/staff-center-19.jpg" style="width: 12px; height: 14px; vertical-align: middle;" /><a
                                                                        target="_blank" style="cursor: pointer;" onclick="art.dialog.open('Welfare.aspx?QId=10', {title: '福利政策',width:820, height:480,background: '#BFBFBF',opacity: 0.7,lock:true});"><span
                                                                            style="color: #787878;">留学归国落户</span></a>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr align="right">
                                                    <td colspan="5">
                                                        <span class="font-color-orag"><strong>*注:&nbsp;&nbsp;<img src="images/staff-center-19.jpg"
                                                            style="width: 12px; height: 14px; vertical-align: middle;" />阅读&nbsp;&nbsp;&nbsp;&nbsp;<img
                                                                src="images/ico-download.jpg" style="width: 12px; height: 14px; vertical-align: middle;" />下载</strong>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table id="tablesh" border="0" style="display: none;" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td style="padding: 5px 0 10px 15px; background-color: #F0F0F0; width: 30%; vertical-align: top;">
                                                        <table>
                                                            <tr align="center">
                                                                <td>
                                                                    <span class="font-color-orag"><strong>(一)社保、公积金类</strong></span>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="color: #EAEAEA;">
                                                                    <img src="images/ico-download.jpg" style="width: 12px; height: 14px; vertical-align: middle;" /><a
                                                                        target="_blank" style="cursor: pointer;" href="shshebao.doc"><span style="color: #787878;">上海员工个人档案、社保、公积金办理</span></a>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="color: #EAEAEA;">
                                                                    <img src="images/ico-download.jpg" style="width: 12px; height: 14px; vertical-align: middle;" /><a
                                                                        target="_blank" style="cursor: pointer;" href="shyiliao.xls"><span style="color: #787878;">补充医疗报销单</span></a>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr align="right">
                                                    <td colspan="5">
                                                        <span class="font-color-orag"><strong>*注:&nbsp;&nbsp;<img src="images/staff-center-19.jpg"
                                                            style="width: 12px; height: 14px; vertical-align: middle;" />阅读&nbsp;&nbsp;&nbsp;&nbsp;<img
                                                                src="images/ico-download.jpg" style="width: 12px; height: 14px; vertical-align: middle;" />下载</strong>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table id="tablegz" style="display: none;" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td style="padding: 5px 0 10px 15px; background-color: #F0F0F0; width: 30%; vertical-align: top;">
                                                        <table>
                                                            <tr align="center">
                                                                <td>
                                                                    <span class="font-color-orag"><strong>(一)社保、公积金类</strong></span>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <img src="images/staff-center-19.jpg" style="width: 12px; height: 14px; vertical-align: middle;" /><a
                                                                        target="_blank" style="cursor: pointer;" onclick="art.dialog.open('Welfare.aspx?QId=11', {title: '福利政策',width:820, height:480,background: '#BFBFBF',opacity: 0.7,lock:true});"><span
                                                                            style="color: #787878;">关于办理广州市社会保险及住房公积金</span></a>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <img src="images/staff-center-19.jpg" style="width: 12px; height: 14px; vertical-align: middle;" /><a
                                                                        target="_blank" style="cursor: pointer;" onclick="art.dialog.open('Welfare.aspx?QId=12', {title: '福利政策',width:820, height:480,background: '#BFBFBF',opacity: 0.7,lock:true});"><span
                                                                            style="color: #787878;">社保卡办理方法</span></a>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <img src="images/staff-center-19.jpg" style="width: 12px; height: 14px; vertical-align: middle;" /><a
                                                                        target="_blank" style="cursor: pointer;" onclick="art.dialog.open('Welfare.aspx?QId=13', {title: '福利政策',width:820, height:480,background: '#BFBFBF',opacity: 0.7,lock:true});"><span
                                                                            style="color: #787878;">社保相关查询网站</span></a>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <img src="images/staff-center-19.jpg" style="width: 12px; height: 14px; vertical-align: middle;" /><a
                                                                        target="_blank" style="cursor: pointer;" onclick="art.dialog.open('Welfare.aspx?QId=14', {title: '福利政策',width:820, height:480,background: '#BFBFBF',opacity: 0.7,lock:true});"><span
                                                                            style="color: #787878;">公积金提取流程和所需资料</span></a>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="color: #EAEAEA;">
                                                                    <img src="images/ico-download.jpg" style="width: 12px; height: 14px; vertical-align: middle;" /><a
                                                                        target="_blank" style="cursor: pointer;" href="gzgongjijin.doc"><span style="color: #787878;">住房公积金提取申请表</span></a>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <img src="images/staff-center-19.jpg" style="width: 12px; height: 14px; vertical-align: middle;" /><a
                                                                        target="_blank" style="cursor: pointer;" onclick="art.dialog.open('Welfare.aspx?QId=15', {title: '福利政策',width:820, height:480,background: '#BFBFBF',opacity: 0.7,lock:true});"><span
                                                                            style="color: #787878;">补充医疗报表申请流程和表格</span></a>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="color: #EAEAEA;">
                                                                    <img src="images/ico-download.jpg" style="width: 12px; height: 14px; vertical-align: middle;" /><a
                                                                        target="_blank" style="cursor: pointer;" href="gzyiliaosuopei.doc"><span style="color: #787878;">医疗索赔申请单</span></a>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="color: #EAEAEA;">
                                                                    <img src="images/ico-download.jpg" style="width: 12px; height: 14px; vertical-align: middle;" /><a
                                                                        target="_blank" style="cursor: pointer;" href="gzmenzhenjiancha.doc"><span style="color: #787878;">门诊检查申请单</span></a>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr align="right">
                                                    <td colspan="5">
                                                        <span class="font-color-orag"><strong>*注:&nbsp;&nbsp;<img src="images/staff-center-19.jpg"
                                                            style="width: 12px; height: 14px; vertical-align: middle;" />阅读&nbsp;&nbsp;&nbsp;&nbsp;<img
                                                                src="images/ico-download.jpg" style="width: 12px; height: 14px; vertical-align: middle;" />下载</strong>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr id="tr2" style="display: none;">
                                        <td style="padding: 25px 0 0 0; line-height: 24px;"></td>
                                    </tr>
                                    <tr id="tr3" style="display: none;">
                                        <td style="padding: 25px 0 0 0; line-height: 24px;">
                                            <table border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td style="padding: 5px 0 10px 15px;">1.
                                                    <label style="cursor: pointer;" onclick="art.dialog.open('HRQA.aspx?QId=31', {title: '人事Q＆A',width:500, height:300,background: '#BFBFBF',opacity: 0.7,lock:true});">
                                                        公司内部是有统一的培训计划吗？</label><br />
                                                        2.
                                                    <label style="cursor: pointer;" onclick="art.dialog.open('HRQA.aspx?QId=32', {title: '人事Q＆A',width:500, height:300,background: '#BFBFBF',opacity: 0.7,lock:true});">
                                                        个人在星言云汇的职业发展怎样规划？公司将提供什么样的帮助？</label><br />
                                                        3.
                                                    <label style="cursor: pointer;" onclick="art.dialog.open('HRQA.aspx?QId=33', {title: '人事Q＆A',width:500, height:300,background: '#BFBFBF',opacity: 0.7,lock:true});">
                                                        我加盟公司后，档案放在哪里？当月是否即可享受社会保险和公积金？</label><br />
                                                        4.
                                                    <label style="cursor: pointer;" onclick="art.dialog.open('HRQA.aspx?QId=34', {title: '人事Q＆A',width:500, height:300,background: '#BFBFBF',opacity: 0.7,lock:true});">
                                                        公司缴纳社保的基数按全额工资还是基本工资？</label><br />
                                                        5.
                                                    <label style="cursor: pointer;" onclick="art.dialog.open('HRQA.aspx?QId=35', {title: '人事Q＆A',width:500, height:300,background: '#BFBFBF',opacity: 0.7,lock:true});">
                                                        请问社保缴纳的基数上限是多少？</label><br />
                                                        6.
                                                    <label style="cursor: pointer;" onclick="art.dialog.open('HRQA.aspx?QId=37', {title: '人事Q＆A',width:500, height:300,background: '#BFBFBF',opacity: 0.7,lock:true});">
                                                        如何查询公积金？</label><br />
                                                        7.
                                                    <label style="cursor: pointer;" onclick="art.dialog.open('HRQA.aspx?QId=38', {title: '人事Q＆A',width:500, height:300,background: '#BFBFBF',opacity: 0.7,lock:true});">
                                                        如何支取公积金？</label><br />
                                                        8.
                                                    <label style="cursor: pointer;" onclick="art.dialog.open('HRQA.aspx?QId=39', {title: '人事Q＆A',width:500, height:300,background: '#BFBFBF',opacity: 0.7,lock:true});">
                                                        如何办理社保卡？何时发放？</label><br />
                                                        9.
                                                    <label style="cursor: pointer;" onclick="art.dialog.open('HRQA.aspx?QId=40', {title: '人事Q＆A',width:500, height:300,background: '#BFBFBF',opacity: 0.7,lock:true});">
                                                        社保卡丢失怎么办？</label><br />
                                                        10.
                                                    <label style="cursor: pointer;" onclick="art.dialog.open('HRQA.aspx?QId=41', {title: '人事Q＆A',width:500, height:300,background: '#BFBFBF',opacity: 0.7,lock:true});">
                                                        本人社保卡未办理成功，需要做二次采集如何办理？</label><br />
                                                        11.
                                                    <label style="cursor: pointer;" onclick="art.dialog.open('HRQA.aspx?QId=42', {title: '人事Q＆A',width:500, height:300,background: '#BFBFBF',opacity: 0.7,lock:true});">
                                                        年假年底没休完怎么办？</label><br />
                                                        12.
                                                    <label style="cursor: pointer;" onclick="art.dialog.open('HRQA.aspx?QId=43', {title: '人事Q＆A',width:500, height:300,background: '#BFBFBF',opacity: 0.7,lock:true});">
                                                        试用期期间，是否可以申请提前转正？</label><br />
                                                        13 .<label style="cursor: pointer;" onclick="art.dialog.open('HRQA.aspx?QId=44', {title: '人事Q＆A',width:500, height:300,background: '#BFBFBF',opacity: 0.7,lock:true});">
                                                            薪资涨幅的比率是多少？根据什么标准来涨幅？</label><br />
                                                        14 .<label style="cursor: pointer;" onclick="">
                                                            女员工如何申领生育津贴？</label><br />
                                                    </td>
                                                </tr>
                                            </table>
                                            <br />
                                            <span>特别提示：有关员工的社保和住房公积金的缴纳，公司严格按照国家相关法规执行。</span><br />
                                            <span class="font-color-orag">若以上问题的回答未能解决您的困惑，或者您还有其他疑问，请写下问题，我们会尽快给您答复。</span>
                                        </td>
                                        <td width="300" valign="bottom" style="padding: 25px 0 0 0; line-height: 24px;">
                                            <table width="300" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td width="1">
                                                        <img src="images/staff-center-16.jpg" width="18" height="48" />
                                                    </td>
                                                    <td background="images/staff-center-17.jpg">
                                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox runat="server" ID="txtTrain"></asp:TextBox>
                                                                </td>
                                                                <td width="30">
                                                                    <asp:Button runat="server" ID="btnTrain" Text="提交" OnClick="btnTrain_OnClick" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td width="1" align="right">
                                                        <img src="images/staff-center-18.jpg" width="32" height="48" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr id="tr4" style="display: none;">
                                        <td style="padding: 25px 0 0 0; line-height: 24px;">
                                            <table border="0" cellpadding="0" cellspacing="0" background="images/sjbg.jpg" style="background-repeat: repeat-x; background-position: left bottom;">
                                                <tr>
                                                    <td valign="bottom">
                                                        <img src="images/sjleft.jpg" width="21" height="17" />
                                                    </td>
                                                    <td>
                                                        <table border="0" cellspacing="0" cellpadding="0" background="images/bookbg.jpg"
                                                            onclick="HRQA(1);" style="background-repeat: no-repeat; cursor: pointer; width: 103px; height: 126px; margin: 0 20px 10px 20px; float: left;">
                                                            <tr>
                                                                <td align="center" class="font-color-orag">（一）<br />
                                                                    内部日常事务
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table border="0" cellspacing="0" cellpadding="0" background="images/bookbg.jpg"
                                                            onclick="HRQA(2);" style="background-repeat: no-repeat; cursor: pointer; width: 103px; height: 126px; margin: 0 20px 10px 20px; float: left;">
                                                            <tr>
                                                                <td align="center" class="font-color-orag">（二）<br />
                                                                    考勤系统的使用
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td valign="bottom">
                                                        <img src="images/sjright.jpg" width="21" height="17" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr id="tr41" style="display: none;">
                                        <td style="padding: 25px 0 0 0; line-height: 24px;">
                                            <strong class=" font-color-orag">一、内部日常事务<br />
                                            </strong>
                                            <table border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td style="padding: 5px 0 10px 15px;">1.
                                                    <label style="cursor: pointer;" onclick="art.dialog.open('HRQA.aspx?QId=1', {title: '内部日常事务',width:820, height:480,background: '#BFBFBF',opacity: 0.7,lock:true});">
                                                        我因公出差需要预定机票、火车票,该如何办理?</label><br />
                                                        2.
                                                    <label style="cursor: pointer;" onclick="art.dialog.open('HRQA.aspx?QId=2', {title: '内部日常事务',width:500, height:300,background: '#BFBFBF',opacity: 0.7,lock:true});">
                                                        我因公出差需要预定酒店,该如何办理?</label><br />
                                                        3.
                                                    <label style="cursor: pointer;" onclick="art.dialog.open('HRQA.aspx?QId=3', {title: '内部日常事务',width:600, height:300,background: '#BFBFBF',opacity: 0.7,lock:true});">
                                                        如何正确填写快递单?</label><br />
                                                        4.
                                                    <label style="cursor: pointer;" onclick="art.dialog.open('HRQA.aspx?QId=4', {title: '内部日常事务',width:600, height:300,background: '#BFBFBF',opacity: 0.7,lock:true});">
                                                        我要印名片,应该向谁申请?</label><br />
                                                        5.
                                                    <label style="cursor: pointer;" onclick="art.dialog.open('HRQA.aspx?QId=5', {title: '内部日常事务',width:600, height:300,background: '#BFBFBF',opacity: 0.7,lock:true});">
                                                        公司的办公用品怎么申请?</label><br />
                                                        6.
                                                    <label style="cursor: pointer;" onclick="art.dialog.open('HRQA.aspx?QId=6', {title: '内部日常事务',width:600, height:300,background: '#BFBFBF',opacity: 0.7,lock:true});">
                                                        我想阅读公司出版的书籍《宣讲》或送给客户,怎么申请?</label><br />
                                                        7.
                                                    <label style="cursor: pointer;" onclick="art.dialog.open('HRQA.aspx?QId=7', {title: '内部日常事务',width:600, height:300,background: '#BFBFBF',opacity: 0.7,lock:true});">
                                                        需要送给客户礼品,怎么申请?</label><br />
                                                        8.
                                                    <label style="cursor: pointer;" onclick="art.dialog.open('HRQA.aspx?QId=8', {title: '内部日常事务',width:500, height:300,background: '#BFBFBF',opacity: 0.7,lock:true});">
                                                        填写各种申请单时,项目号如何使用?</label><br />
                                                        9.
                                                    <label style="cursor: pointer;" onclick="art.dialog.open('HRQA.aspx?QId=9', {title: '内部日常事务',width:500, height:300,background: '#BFBFBF',opacity: 0.7,lock:true});">
                                                        我如果需要用会议室开会,怎么来预订房间?</label><br />
                                                        10.
                                                    <label style="cursor: pointer;" onclick="art.dialog.open('HRQA.aspx?QId=10', {title: '内部日常事务',width:500, height:300,background: '#BFBFBF',opacity: 0.7,lock:true});">
                                                        开会要用投影仪,录音笔,翻页器怎么申请使用?</label><br />
                                                        11.
                                                    <label style="cursor: pointer;" onclick="art.dialog.open('HRQA.aspx?QId=11', {title: '内部日常事务',width:500, height:300,background: '#BFBFBF',opacity: 0.7,lock:true});">
                                                        我要用会议室的多方通话功能,怎么来操作?</label><br />
                                                        12.
                                                    <label style="cursor: pointer;" onclick="art.dialog.open('HRQA.aspx?QId=12', {title: '内部日常事务',width:500, height:300,background: '#BFBFBF',opacity: 0.7,lock:true});">
                                                        我们团队需要一辆公务车去接送客户及看活动场地,如何申请?</label><br />
                                                        13.
                                                    <label style="cursor: pointer;" onclick="art.dialog.open('HRQA.aspx?QId=13', {title: '内部日常事务',width:500, height:300,background: '#BFBFBF',opacity: 0.7,lock:true});">
                                                        公司是否可以借阅书籍?在哪里,如何借阅?</label><br />
                                                        14 .<label style="cursor: pointer;" onclick="art.dialog.open('HRQA.aspx?QId=14', {title: '内部日常事务',width:600, height:300,background: '#BFBFBF',opacity: 0.7,lock:true});">
                                                            集团对于员工的工作服饰是否有规定?</label><br />
                                                        15.
                                                    <label style="cursor: pointer;" onclick="art.dialog.open('HRQA.aspx?QId=15', {title: '内部日常事务',width:500, height:300,background: '#BFBFBF',opacity: 0.7,lock:true});">
                                                        我在哪里可以阅读公司的规章制度,并获取到需要的日常职能工作表格?</label>
                                                    </td>
                                                </tr>
                                            </table>
                                            <span class="font-color-orag">若以上问题的回答未能解决您的困惑，或者您还有其他疑问，请写下问题，我们会尽快给您答复。</span>
                                        </td>
                                        <td width="300" valign="bottom" style="padding: 25px 0 0 0; line-height: 24px;">
                                            <table width="300" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td width="1">
                                                        <img src="images/staff-center-16.jpg" width="18" height="48" />
                                                    </td>
                                                    <td background="images/staff-center-17.jpg">
                                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox runat="server" ID="txtDaily"></asp:TextBox>
                                                                </td>
                                                                <td width="30">
                                                                    <asp:Button runat="server" ID="btnDaily" Text="提交" OnClick="btnDaily_OnClick" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td width="1" align="right">
                                                        <img src="images/staff-center-18.jpg" width="32" height="48" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr id="tr42" style="display: none;">
                                        <td style="padding: 25px 0 0 0; line-height: 24px;">
                                            <strong class=" font-color-orag">一、考勤系统的使用<br />
                                            </strong>
                                            <table border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td style="padding: 5px 0 10px 15px;">1.
                                                    <label style="cursor: pointer;" onclick="art.dialog.open('HRQA.aspx?QId=16', {title: '考勤系统的使用',width:500, height:300,background: '#BFBFBF',opacity: 0.7,lock:true});">
                                                        线上如何鉴定上午和下午的上班时间？</label><br />
                                                        2.
                                                    <label style="cursor: pointer;" onclick="art.dialog.open('HRQA.aspx?QId=17', {title: '考勤系统的使用',width:500, height:300,background: '#BFBFBF',opacity: 0.7,lock:true});">
                                                        平时OT是否需要进行申请？</label><br />
                                                        3.
                                                    <label style="cursor: pointer;" onclick="art.dialog.open('HRQA.aspx?QId=18', {title: '考勤系统的使用',width:500, height:300,background: '#BFBFBF',opacity: 0.7,lock:true});">
                                                        每天的考勤记录时间的截点？</label><br />
                                                        4.
                                                    <label style="cursor: pointer;" onclick="art.dialog.open('HRQA.aspx?QId=19', {title: '考勤系统的使用',width:500, height:300,background: '#BFBFBF',opacity: 0.7,lock:true});">
                                                        考勤管理中的“提交审批”该在什么时候提交？</label><br />
                                                        5.
                                                    <label style="cursor: pointer;" onclick="art.dialog.open('HRQA.aspx?QId=20', {title: '考勤系统的使用',width:500, height:300,background: '#BFBFBF',opacity: 0.7,lock:true});">
                                                        周末OT是否可以调休，调休时间是否可以延长？</label><br />
                                                        6.
                                                    <label style="cursor: pointer;" onclick="art.dialog.open('HRQA.aspx?QId=21', {title: '考勤系统的使用',width:500, height:300,background: '#BFBFBF',opacity: 0.7,lock:true});">
                                                        做了请假申请，为什么还显示旷工或迟到？</label><br />
                                                        7.
                                                    <label style="cursor: pointer;" onclick="art.dialog.open('HRQA.aspx?QId=22', {title: '考勤系统的使用',width:500, height:300,background: '#BFBFBF',opacity: 0.7,lock:true});">
                                                        忘刷卡怎么办？忘带卡怎么办？</label><br />
                                                        8.
                                                    <label style="cursor: pointer;" onclick="art.dialog.open('HRQA.aspx?QId=23', {title: '考勤系统的使用',width:500, height:300,background: '#BFBFBF',opacity: 0.7,lock:true});">
                                                        月底提交考勤需注意些什么？</label><br />
                                                        9.
                                                    <label style="cursor: pointer;" onclick="art.dialog.open('HRQA.aspx?QId=24', {title: '考勤系统的使用',width:500, height:300,background: '#BFBFBF',opacity: 0.7,lock:true});">
                                                        请假申请的线上提交有时间限制吗？</label><br />
                                                        10.
                                                    <label style="cursor: pointer;" onclick="art.dialog.open('HRQA.aspx?QId=25', {title: '考勤系统的使用',width:500, height:300,background: '#BFBFBF',opacity: 0.7,lock:true});">
                                                        我的出差、休假、病假、以及因上一日OT而晚到的考勤说明，需要让谁审批同意？</label><br />
                                                        11.
                                                    <label style="cursor: pointer;" onclick="art.dialog.open('HRQA.aspx?QId=26', {title: '考勤系统的使用',width:500, height:300,background: '#BFBFBF',opacity: 0.7,lock:true});">
                                                        日常考勤和年假等记录在什么地方可以查到？</label><br />
                                                    </td>
                                                </tr>
                                            </table>
                                            <span class="font-color-orag">若以上问题的回答未能解决您的困惑，或者您还有其他疑问，请写下问题，我们会尽快给您答复。</span>
                                        </td>
                                        <td width="300" valign="bottom" style="padding: 25px 0 0 0; line-height: 24px;">
                                            <table width="300" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td width="1">
                                                        <img src="images/staff-center-16.jpg" width="18" height="48" />
                                                    </td>
                                                    <td background="images/staff-center-17.jpg">
                                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox runat="server" ID="txtAdmin"></asp:TextBox>
                                                                </td>
                                                                <td width="30">
                                                                    <asp:Button runat="server" ID="btnAdmin" Text="提交" OnClick="btnAdmin_OnClick" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td width="1" align="right">
                                                        <img src="images/staff-center-18.jpg" width="32" height="48" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr id="tr5" style="display: none;">
                                        <td style="padding: 25px 0 0 0; line-height: 24px;">
                                            <div class="title-orag">
                                                发票管理条例
                                            </div>
                                            <br />
                                            <p>
                                                为确保集团的正常商业运营，降低财务风险，特制定本应收款管理条例。<br />
                                                1、责任人。发票直接责任人负责发票的信息核对、保管、递送和跟踪，确保发票相关事务的顺畅和安全。<br />
                                                发票直接责任人由业务人员担任，根据发票金额的大小，对级别的要求分别对应如下：<br />
                                            </p>
                                            <table border="1" cellpadding="0" cellspacing="0" style="font-weight: bolder;">
                                                <tr>
                                                    <td align="center" style="font-weight: bolder; width: 200px;">金额区间
                                                    </td>
                                                    <td align="center" style="font-weight: bolder; width: 200px;">责任人
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>10(含)万元以下
                                                    </td>
                                                    <td>客户经理(AAM/AM/SAM)或以上
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>10-100(含)万元
                                                    </td>
                                                    <td>客户总监(AAD/AD/GAD)或以上
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>100-500(含)万元
                                                    </td>
                                                    <td>团队副总经理(VP/EVP)或以上
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>500万元以上
                                                    </td>
                                                    <td>团队总经理(MD)或以上
                                                    </td>
                                                </tr>
                                            </table>
                                            <p>
                                                直接责任人的直接汇报上级为二级责任人，承担发票管理的连带责任；<br />
                                                2、离职交接。责任人离职时，发票的记录和管理责任必须交接给同级或更高级别的同事。接手的同事将成为发票新的责任人。交接发票新责任人的确认邮件需转给财务部留底；<br />
                                                3、发票签收：每张开出的发票都只能交给相应的直接责任人，直接责任人随后提供发票签收单。具体操作步骤如下：
                                            <br />
                                                a、项目需要开发票时，项目负责人发邮件给应收财务（若项目负责人级别低于发票直接责任人，邮件需抄送相应直接责任人及责任人的直接汇报上级）。应收财务核实发票登记表信息无误后开出发票，发票直接责任人领取发票时需要在存根联背面签字。同时，应收财务打印发票签收单给直接责任人，并回复邮件告知所开发票相关信息；<br />
                                                b、应收财务督促直接责任人在开出发票1周内提交客户签字的发票签收单（原件、传真件、扫描件均可）并邮件告知相关同事；如因客户的原因不能提供签字签收单的，请提供清晰的证据表明该发票已被客户接收（如照片、录音、邮件等）；<br />
                                                c、应收财务按照公司、客户及项目号的依次顺序留存发票签收单，待收到回款邮件后在此单上做标记，回款半年后可销毁。
                                            <br />
                                                4、除因不可抗拒力外，如因发票信息错误、递送延误、丢失等问题照成公司损失的，发票直接责任人和二级责任人将承担相应的责任，并根据损失的大小受到包括但不限于警告、扣工资、解聘等相应的处罚；<br />
                                                5、特殊情况，团队总经理报请集团总裁审定。<br />
                                            </p>
                                            <br />
                                            <br />
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td align="right" style="padding-right: 50px;">星言云汇管理委员会
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" style="padding-right: 100px;">2013年7月
                                                    </td>
                                                </tr>
                                            </table>
                                            <br />
                                            <br />
                                            <br />
                                            <div class="title-orag">
                                                附件：
                                            </div>
                                            <br />
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td align="center">发票签收单
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <p>
                                                            尊敬的客户：<br />
                                                            为确保您能收到我们提供的发票，并使我们能及时跟踪到发票的到达，烦请收到发票原件后，在本签收单上签名并回传。如对发票有任何疑问，欢迎与我们公司业务人员联系，谢谢！<br />
                                                            <br />
                                                            联系人： 请填写与客户沟通的相应业务人员姓名<br />
                                                            联系电话： 请填写与客户沟通的相应业务人员电话<br />
                                                            传真号码： 请填写与客户沟通的相应业务人员传真<br />
                                                            <br />
                                                            发票明细如下：<br />
                                                            <img src="/images/invoice.jpg" /><br />
                                                            该款项贵公司尚未支付。 烦请尽快办理相关手续，非常感谢！<br />
                                                            我公司账户资料详见付款通知下方。<br />
                                                        </p>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" style="padding-right: 100px;">客户签字（盖章）：           </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" style="padding-right: 130px;">日  期：</td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr id="tr6" style="display: none;">
                                        <td style="padding: 25px 0 0 0; line-height: 24px;">
                                            <div class="title-orag">
                                                公司治理制度汇编
                                            </div>
                                            <br />
                                    </tr>

                                    <tr id="tr7" style="display: none;">
                                        <td style="padding: 25px 0 0 0; line-height: 24px;">
                                            <div class="title-orag">
                                                星言云汇印章管理制度
                                            </div>
                                            <br />
                                            <p>
                                                为了保证公司印章使用的规范性和严肃性，有效地维护公司利益，杜绝违规违法行为的发生，特制定本管理制度。<br />
                                                一．	印章保管<br />
                                                印章必须由专人保管，任何人未经授权均不得擅自使用印章。<br />
                                                1.	印章包含：星言云汇及下属各全资子公司、可控制子公司及分公司的公章、合同章。（注：财务专用章、发票专用章及各公司法人名章，由财务部自行管理。）<br />
                                                2.	专人保管：由专人负责保管，接收时签署书面交接单。<br />
                                                3.	安全保障：上班期间，妥善放置。下班时间及节假日期间，存入保险柜。<br />
                                                4.	印章代管：印章保管人外出、休假或出差时，由集团董事长指定代理保管人，并填写《印章领用归还登记表》。<br />
                                                二．	印章使用<br />
                                                严格执行“负责人制”，即必须有相关负责人签字或批准后才能盖章。<br />
                                                1.	公章：各业务团队的竞标、报价等材料，由团队负责人签字（或邮件审批）后加盖公章；行政、人事、财务等各职能部门的相关材料，如在职证明、收入证明、工商税务等材料，由职能部门负责人签字（或邮件审批）后加盖公章。<br />
                                                2.	合同章：与客户签订的服务合同，经由律师审核，且由各团队负责人签字后加盖合同章；与第三方签订的采购合同，由采购部负责人签字后加盖合同章。盖章前，依公司的相关规定，作必要的核准。
                                                <br />
                                                3.	加盖印章时要压在签字或文字上，不得盖在空白处。空白资料不得加盖公章。营业执照、法人身份证复印件等材料加盖公章前需加盖“资料用途专用章”。<br />
                                                4.	加盖印章时，由申请人填写《公章、合同章使用登记表》。<br />
                                                三．	印章外借<br />
                                                1.	行政、财务等职能部门需要带公章去工商、银行、税务部门办理业务时，由职能部门负责人审批后，可借出公章，当日借当日还，不得隔日还。同时，申请人填写《印章领用归还登记表》。<br />
                                                2.	业务竞标时，团队需要带公章到外地使用，由相关团队负责人以邮件方式向集团董事长申请。审批通过后，由团队负责人亲自领用并填写《印章领用归还登记表》。如遇特殊情况，团队负责人不能亲自领用，则需在邮件中指派专人领用。领用时，公章要密封签字。到达使用地后，由团队负责人亲自启封。使用完毕后，由该团队负责人再行密封签字，并由该专人及时带回。<br />
                                                3.	异地子公司的印章，需要带到外地使用时，由子公司负责人亲自借出，或由子公司负责人邮件审批后，指定专人借出，同时填写《印章领用归还登记表》。<br />
                                                四．	印章作废<br />
                                                1.	因公司更名作废的印章，交由财务部保管。<br />
                                                2.	因公司注销作废的印章，将印章作废处理后交由财务部保管。<br />
                                                五．	责任<br />
                                                1.	印章管理人员必须认真负责，秉公行事。<br />
                                                2.	盖章后出现问题，由签字人或审批人负责；如因印章保管人员违规盖章出现问题，由印章保管人员负责。<br />


                                            </p>
                                            <br />
                                            <br />
                                            <br />
                                            <br />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="0" style="border-top: 1px solid #dededd;">
            <tr>
                <td height="38" align="center" background="images/staff-center-08.jpg" style="border-bottom: 5px solid #eeaf70; color: #717172; font-size: 12px;">Copyright ©1999-2011 Shunya Group. All rights reserved
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
