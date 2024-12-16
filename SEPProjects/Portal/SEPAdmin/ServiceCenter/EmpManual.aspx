<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmpManual.aspx.cs" Inherits="SEPAdmin.ServiceCenter.EmpManual" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>员工手册</title>
    <link href="style.css" rel="stylesheet" type="text/css" />
</head>
<body>

    <script type="text/javascript">
        function MenuClick(menuIndex) {
            var tdyinyan = document.getElementById("tdyinyan");

            var td1 = document.getElementById("td1");
            var td2 = document.getElementById("td2");
            var td5 = document.getElementById("td5");
            var td6 = document.getElementById("td6")
            var td7 = document.getElementById("td7");
            var td8 = document.getElementById("td8");
            var td9 = document.getElementById("td9");
            var td10 = document.getElementById("td10");

            var td11 = document.getElementById("td11");
            var td12 = document.getElementById("td12");

            var td21 = document.getElementById("td21");
            var td22 = document.getElementById("td22");

            var td24 = document.getElementById("td24");
            var td25 = document.getElementById("td25");
            var td26 = document.getElementById("td26");

            var td27 = document.getElementById("td27");
            var td28 = document.getElementById("td28");
            var td29 = document.getElementById("td29");


            var td51 = document.getElementById("td51");
            var td52 = document.getElementById("td52");
            var td53 = document.getElementById("td53");
            var td54 = document.getElementById("td54");
            var td55 = document.getElementById("td55");

            var td61 = document.getElementById("td61");
            var td62 = document.getElementById("td62");
            var td63 = document.getElementById("td63");
            var td64 = document.getElementById("td64");

            var td71 = document.getElementById("td71");
            var td81 = document.getElementById("td81");
            var td91 = document.getElementById("td91");
            var td101 = document.getElementById("td101");
            var td102 = document.getElementById("td102");

            var td111 = document.getElementById("td111");
            var td121 = document.getElementById("td121");

            var td211 = document.getElementById("td211");
            var td221 = document.getElementById("td221");

            var td241 = document.getElementById("td241");
            var td251 = document.getElementById("td251");
            var td261 = document.getElementById("td261");

            var td271 = document.getElementById("td271");
            var td281 = document.getElementById("td281");
            var td291 = document.getElementById("td291");

            var td511 = document.getElementById("td511");
            var td521 = document.getElementById("td521");
            var td531 = document.getElementById("td531");
            var td541 = document.getElementById("td541");
            var td551 = document.getElementById("td551");

            var td611 = document.getElementById("td611");
            var td621 = document.getElementById("td621");
            var td631 = document.getElementById("td631");
            var td641 = document.getElementById("td641");


            var td711 = document.getElementById("td711");
            var td811 = document.getElementById("td811");
            var td911 = document.getElementById("td911");
            var td1011 = document.getElementById("td1011");
            var td1021 = document.getElementById("td1021");

            td11.style.display = "none";
            td12.style.display = "none";

            td21.style.display = "none";
            td22.style.display = "none";

            td24.style.display = "none";
            td25.style.display = "none";
            td26.style.display = "none";

            td27.style.display = "none";
            td28.style.display = "none";
            td29.style.display = "none";


            td51.style.display = "none";
            td52.style.display = "none";
            td53.style.display = "none";
            td54.style.display = "none";
            td55.style.display = "none";

            td61.style.display = "none";
            td62.style.display = "none";
            td63.style.display = "none";
            td64.style.display = "none";

            td71.style.display = "none";
            td81.style.display = "none";
            td91.style.display = "none";
            td101.style.display = "none";
            td102.style.display = "none";

            td11.className = "leftmenu-greytip";
            td12.className = "leftmenu-greytip";

            td21.className = "leftmenu-greytip";
            td22.className = "leftmenu-greytip";

            td24.className = "leftmenu-greytip";
            td25.className = "leftmenu-greytip";
            td26.className = "leftmenu-greytip";

            td27.className = "leftmenu-greytip";
            td28.className = "leftmenu-greytip";
            td29.className = "leftmenu-greytip";



            td51.className = "leftmenu-greytip";
            td52.className = "leftmenu-greytip";
            td53.className = "leftmenu-greytip";
            td54.className = "leftmenu-greytip";
            td55.className = "leftmenu-greytip";

            td61.className = "leftmenu-greytip";
            td62.className = "leftmenu-greytip";
            td63.className = "leftmenu-greytip";
            td64.className = "leftmenu-greytip";

            td71.className = "leftmenu-greytip";
            td81.className = "leftmenu-greytip";
            td91.className = "leftmenu-greytip";
            td101.className = "leftmenu-greytip";
            td102.className = "leftmenu-greytip";

            td111.style.display = "none";
            td121.style.display = "none";

            td211.style.display = "none";
            td221.style.display = "none";

            td241.style.display = "none";
            td251.style.display = "none";
            td261.style.display = "none";

            td271.style.display = "none";
            td281.style.display = "none";
            td291.style.display = "none";

            td511.style.display = "none";
            td521.style.display = "none";
            td531.style.display = "none";
            td541.style.display = "none";
            td551.style.display = "none";

            td611.style.display = "none";
            td621.style.display = "none";
            td631.style.display = "none";
            td641.style.display = "none";
            td711.style.display = "none";
            td811.style.display = "none";
            td911.style.display = "none";
            td1011.style.display = "none";
            td1021.style.display = "none";

            tdyinyan.style.display = "none";
            if (menuIndex == 1) {
                td11.style.display = "block";
                td12.style.display = "block";
                td11.className = "leftmenu-yellowtip";
                td111.style.display = "block";
            }
            else if (menuIndex == 2) {
                td21.style.display = "block";
                td22.style.display = "block";

                td24.style.display = "block";
                td25.style.display = "block";
                td26.style.display = "block";

                td27.style.display = "block";
                td28.style.display = "block";
                td29.style.display = "block";

                td21.className = "leftmenu-yellowtip";
                td211.style.display = "block";

            }
            else if (menuIndex == 5) {
                td51.style.display = "block";
                td52.style.display = "block";
                td53.style.display = "block";
                td54.style.display = "block";
                td55.style.display = "block";
                td51.className = "leftmenu-yellowtip";
                td511.style.display = "block";
            }
            else if (menuIndex == 6) {
                td61.style.display = "block";
                td62.style.display = "block";
                td63.style.display = "block";
                td64.style.display = "block";
                td61.className = "leftmenu-yellowtip";
                td611.style.display = "block";
            }
            else if (menuIndex == 7) {
                td71.style.display = "block";
                td71.className = "leftmenu-yellowtip";
                td711.style.display = "block";
            }
            else if (menuIndex == 8) {
                td81.style.display = "block";
                td81.className = "leftmenu-yellowtip";
                td811.style.display = "block";
            }
            else if (menuIndex == 9) {
                td91.style.display = "block";
                td91.className = "leftmenu-yellowtip";
                td911.style.display = "block";
            }
            else if (menuIndex == 10) {
                td101.style.display = "block";
                td101.className = "leftmenu-yellowtip";
                td102.style.display = "block";
                td1011.style.display = "block";
            }
            else if (menuIndex == 11) {
                td11.style.display = "block";
                td12.style.display = "block";
                td11.className = "leftmenu-yellowtip";
                td111.style.display = "block";
            }
            else if (menuIndex == 12) {
                td11.style.display = "block";
                td12.style.display = "block";
                td12.className = "leftmenu-yellowtip";
                td121.style.display = "block";
            }
            else if (menuIndex == 21) {
                td21.style.display = "block";
                td22.style.display = "block";

                td24.style.display = "block";
                td25.style.display = "block";
                td26.style.display = "block";

                td27.style.display = "block";
                td28.style.display = "block";
                td29.style.display = "block";
                td21.className = "leftmenu-yellowtip";
                td211.style.display = "block";
            }
            else if (menuIndex == 22) {
                td21.style.display = "block";
                td22.style.display = "block";

                td24.style.display = "block";
                td25.style.display = "block";
                td26.style.display = "block";

                td27.style.display = "block";
                td28.style.display = "block";
                td29.style.display = "block";
                td22.className = "leftmenu-yellowtip";
                td221.style.display = "block";
            }
            else if (menuIndex == 23) {
                td21.style.display = "block";
                td22.style.display = "block";

                td24.style.display = "block";
                td25.style.display = "block";
                td26.style.display = "block";

                td27.style.display = "block";
                td28.style.display = "block";
                td29.style.display = "block";

            }
            else if (menuIndex == 24) {
                td21.style.display = "block";
                td22.style.display = "block";

                td24.style.display = "block";
                td25.style.display = "block";
                td26.style.display = "block";

                td27.style.display = "block";
                td28.style.display = "block";
                td29.style.display = "block";

                td24.className = "leftmenu-yellowtip";
                td241.style.display = "block";
            }
            else if (menuIndex == 25) {
                td21.style.display = "block";
                td22.style.display = "block";

                td24.style.display = "block";
                td25.style.display = "block";
                td26.style.display = "block";

                td27.style.display = "block";
                td28.style.display = "block";
                td29.style.display = "block";

                td25.className = "leftmenu-yellowtip";
                td251.style.display = "block";
            }
            else if (menuIndex == 26) {
                td21.style.display = "block";
                td22.style.display = "block";

                td24.style.display = "block";
                td25.style.display = "block";
                td26.style.display = "block";

                td27.style.display = "block";
                td28.style.display = "block";
                td29.style.display = "block";

                td26.className = "leftmenu-yellowtip";
                td261.style.display = "block";
            }
            else if (menuIndex == 27) {
                td21.style.display = "block";
                td22.style.display = "block";

                td24.style.display = "block";
                td25.style.display = "block";
                td26.style.display = "block";

                td27.style.display = "block";
                td28.style.display = "block";
                td29.style.display = "block";

                td27.className = "leftmenu-yellowtip";
                td271.style.display = "block";
            }
            else if (menuIndex == 28) {
                td21.style.display = "block";
                td22.style.display = "block";

                td24.style.display = "block";
                td25.style.display = "block";
                td26.style.display = "block";

                td27.style.display = "block";
                td28.style.display = "block";
                td29.style.display = "block";

                td28.className = "leftmenu-yellowtip";
                td281.style.display = "block";
            }
            else if (menuIndex == 29) {
                td21.style.display = "block";
                td22.style.display = "block";

                td24.style.display = "block";
                td25.style.display = "block";
                td26.style.display = "block";

                td27.style.display = "block";
                td28.style.display = "block";
                td29.style.display = "block";

                td29.className = "leftmenu-yellowtip";
                td291.style.display = "block";
            }
            else if (menuIndex == 51) {
                td51.style.display = "block";
                td52.style.display = "block";
                td53.style.display = "block";
                td54.style.display = "block";
                td55.style.display = "block";
                td51.className = "leftmenu-yellowtip";
                td511.style.display = "block";
            }
            else if (menuIndex == 52) {
                td51.style.display = "block";
                td52.style.display = "block";
                td53.style.display = "block";
                td54.style.display = "block";
                td55.style.display = "block";
                td52.className = "leftmenu-yellowtip";
                td521.style.display = "block";
            }
            else if (menuIndex == 53) {
                td51.style.display = "block";
                td52.style.display = "block";
                td53.style.display = "block";
                td54.style.display = "block";
                td55.style.display = "block";
                td53.className = "leftmenu-yellowtip";
                td531.style.display = "block";
            }
            else if (menuIndex == 54) {
                td51.style.display = "block";
                td52.style.display = "block";
                td53.style.display = "block";
                td54.style.display = "block";
                td55.style.display = "block";
                td54.className = "leftmenu-yellowtip";
                td541.style.display = "block";
            }
            else if (menuIndex == 55) {
                td51.style.display = "block";
                td52.style.display = "block";
                td53.style.display = "block";
                td54.style.display = "block";
                td55.style.display = "block";
                td55.className = "leftmenu-yellowtip";
                td551.style.display = "block";
            }
            else if (menuIndex == 61) {
                td61.style.display = "block";
                td62.style.display = "block";
                td63.style.display = "block";
                td64.style.display = "block";
                td61.className = "leftmenu-yellowtip";
                td611.style.display = "block";
            }
            else if (menuIndex == 62) {
                td61.style.display = "block";
                td62.style.display = "block";
                td63.style.display = "block";
                td64.style.display = "block";
                td62.className = "leftmenu-yellowtip";
                td621.style.display = "block";
            }
            else if (menuIndex == 63) {
                td61.style.display = "block";
                td62.style.display = "block";
                td63.style.display = "block";
                td64.style.display = "block";
                td63.className = "leftmenu-yellowtip";
                td631.style.display = "block";
            }
            else if (menuIndex == 64) {
                td61.style.display = "block";
                td62.style.display = "block";
                td63.style.display = "block";
                td64.style.display = "block";
                td64.className = "leftmenu-yellowtip";
                td641.style.display = "block";
            }
            else if (menuIndex == 71) {
                td71.style.display = "block";
                td71.className = "leftmenu-yellowtip";
                td711.style.display = "block";
            }
            else if (menuIndex == 81) {
                td81.style.display = "block";
                td81.className = "leftmenu-yellowtip";
                td811.style.display = "block";
            }
            else if (menuIndex == 91) {
                td91.style.display = "block";
                td91.className = "leftmenu-yellowtip";
                td911.style.display = "block";
            }
            else if (menuIndex == 101) {
                td101.style.display = "block";
                td102.style.display = "block";
                td101.className = "leftmenu-yellowtip";
                td1011.style.display = "block";
            }
            else if (menuIndex == 102) {
                td101.style.display = "block";
                td102.style.display = "block";
                td102.className = "leftmenu-yellowtip";
                td1021.style.display = "block";
            }
        }

        function scrollbar() {
            document.documentElement.scrollTop = 0;
        }
    </script>

    <form id="form1" runat="server">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" style="background-image: url(images/staff-center-32.jpg); background-repeat: repeat-x; background-position: left top;">
            <tr>
                <td style="padding: 26px 0 10px 43px;">
                    <img src="images/staff-center-33.jpg" width="356" height="37" />
                </td>
            </tr>
            <tr>
                <td>
                    <table width="96%" border="0" align="center" cellpadding="0" cellspacing="0">
                        <tr>
                            <td height="70" valign="top" background="images/staff-center-34.jpg" style="background-repeat: no-repeat; background-position: 177px bottom;">
                                <table border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td style="padding: 10px 0 0 400px; color: #cd6700;" align="right">
                                            <span style="font-size: 20px; font-family: 微软雅黑;">员工服务中心 - 员工手册</span>（v1.5）2015年&nbsp;&nbsp;&nbsp;
                                        <img src="images/home.jpg"></img>
                                            <a target='_parent' href='Default.aspx'>首页</a>&nbsp;&nbsp;&nbsp;
                                        <img src="images/portal.jpg"></img>
                                            <a target='_top' href='http://xy.shunyagroup.com'>其他系统</a>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td background="images/staff-center-35.jpg" style="background-repeat: repeat-y; background-position: 177px top;">
                                <table width="955" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td width="201" valign="top">
                                            <table width="201" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td id="td1" class="leftmenu-and-line" onclick="MenuClick(1);" align="center">
                                                        <a style="cursor: pointer;">
                                                            <img src="images/staff-center-22.jpg" width="19" height="20" />企业文化</a>
                                                    </td>
                                                </tr>
                                                <tr align="center">
                                                    <td id="td11" class="leftmenu-greytip" onclick="MenuClick(11);" style="display: none;"
                                                        align="center">
                                                        <a style="cursor: pointer;">
                                                            <br />
                                                            简介</a>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td id="td12" class="leftmenu-greytip" onclick="MenuClick(12);" style="display: none;"
                                                        align="center">
                                                        <a style="cursor: pointer;">
                                                            <br />
                                                            经营理念、<br />
                                                            价值观和道德标准</a>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td id="td2" class="leftmenu-and-line" onclick="MenuClick(2);" align="center">
                                                        <a style="cursor: pointer;">
                                                            <img src="images/staff-center-22.jpg" width="19" height="20" />人事制度</a>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td id="td21" class="leftmenu-greytip" onclick="MenuClick(21);" style="display: none;"
                                                        align="center">
                                                        <a style="cursor: pointer;">
                                                            <br />
                                                            合同签订与终止</a>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td id="td22" class="leftmenu-greytip" onclick="MenuClick(22);" style="display: none;"
                                                        align="center">
                                                        <a style="cursor: pointer;">
                                                            <br />
                                                            薪酬与福利</a>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td id="td24" class="leftmenu-greytip" onclick="MenuClick(24);" style="display: none;"
                                                        align="center">
                                                        <a style="cursor: pointer;">
                                                            <br />
                                                            绩效与激励</a>
                                                    </td>
                                                </tr>
                                    </tr>
                                    <tr>
                                        <td id="td25" class="leftmenu-greytip" onclick="MenuClick(25);" style="display: none;"
                                            align="center">
                                            <a style="cursor: pointer;">
                                                <br />
                                                培训制度</a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td id="td26" class="leftmenu-greytip" onclick="MenuClick(26);" style="display: none;"
                                            align="center">
                                            <a style="cursor: pointer;">
                                                <br />
                                                晋升制度</a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td id="td27" class="leftmenu-greytip" onclick="MenuClick(27);" style="display: none;"
                                            align="center">
                                            <a style="cursor: pointer;">
                                                <br />
                                                工作调动</a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td id="td28" class="leftmenu-greytip" onclick="MenuClick(28);" style="display: none;"
                                            align="center">
                                            <a style="cursor: pointer;">
                                                <br />
                                                奖惩条例</a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td id="td29" class="leftmenu-greytip" onclick="MenuClick(29);" style="display: none;"
                                            align="center">
                                            <a style="cursor: pointer;">
                                                <br />
                                                申诉渠道和程序</a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td id="td5" class="leftmenu-and-line" onclick="MenuClick(5);" align="center">
                                            <a style="cursor: pointer;">
                                                <img src="images/staff-center-26.jpg" width="19" height="20" />行政制度</a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td id="td51" class="leftmenu-greytip" onclick="MenuClick(51);" style="display: none;"
                                            align="center">
                                            <a style="cursor: pointer;">
                                                <br />
                                                考勤管理制度</a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td id="td52" class="leftmenu-greytip" onclick="MenuClick(52);" style="display: none;"
                                            align="center">
                                            <a style="cursor: pointer;">
                                                <br />
                                                行为规范</a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td id="td53" class="leftmenu-greytip" onclick="MenuClick(53);" style="display: none;"
                                            align="center">
                                            <a style="cursor: pointer;">
                                                <br />
                                                绿色办公</a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td id="td54" class="leftmenu-greytip" onclick="MenuClick(54);" style="display: none;"
                                            align="center">
                                            <a style="cursor: pointer;">
                                                <br />
                                                安全守则</a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td id="td55" class="leftmenu-greytip" onclick="MenuClick(55);" style="display: none;"
                                            align="center">
                                            <a style="cursor: pointer;">
                                                <br />
                                                图书管理</a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td id="td6" class="leftmenu-and-line" onclick="MenuClick(6);" align="center">
                                            <a style="cursor: pointer;">
                                                <img src="images/staff-center-27.jpg" width="19" height="20" />财务制度</a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td id="td61" class="leftmenu-greytip" onclick="MenuClick(61);" style="display: none;"
                                            align="center">
                                            <a style="cursor: pointer;">
                                                <br />
                                                费用申请、报销与借款管理条例</a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td id="td62" class="leftmenu-greytip" onclick="MenuClick(62);" style="display: none;"
                                            align="center">
                                            <a style="cursor: pointer;">
                                                <br />
                                                商务信用卡管理条例</a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td id="td63" class="leftmenu-greytip" onclick="MenuClick(63);" style="display: none;"
                                            align="center">
                                            <a style="cursor: pointer;">
                                                <br />
                                                应收款项管理条例</a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td id="td64" class="leftmenu-greytip" onclick="MenuClick(64);" style="display: none;"
                                            align="center">
                                            <a style="cursor: pointer;">
                                                <br />
                                                发票管理条例</a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td id="td7" class="leftmenu-and-line" onclick="MenuClick(7);" align="center">
                                            <a style="cursor: pointer;">
                                                <img src="images/staff-center-28.jpg" width="19" height="20" />采购制度</a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td id="td71" class="leftmenu-greytip" onclick="MenuClick(71);" style="display: none;"
                                            align="center">
                                            <a style="cursor: pointer;">
                                                <br />
                                                采购管理办法</a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td id="td8" class="leftmenu-and-line" onclick="MenuClick(8);" align="center">
                                            <a style="cursor: pointer;">
                                                <img src="images/staff-center-28.jpg" width="19" height="20" />网络制度</a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td id="td81" class="leftmenu-greytip" onclick="MenuClick(81);" style="display: none;"
                                            align="center">
                                            <a style="cursor: pointer;">
                                                <br />
                                                网络及设备安全管理办法</a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td id="td9" class="leftmenu-and-line" onclick="MenuClick(9);" align="center">
                                            <a style="cursor: pointer;">
                                                <img src="images/staff-center-28.jpg" width="19" height="20" />内审制度</a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td id="td91" class="leftmenu-greytip" onclick="MenuClick(91);" style="display: none;"
                                            align="center">
                                            <a style="cursor: pointer;">
                                                <br />
                                                内审条例</a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td id="td10" class="leftmenu-and-line" onclick="MenuClick(10);" align="center">
                                            <a style="cursor: pointer;">
                                                <img src="images/staff-center-28.jpg" width="19" height="20" />休假政策</a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td id="td101" class="leftmenu-greytip" onclick="MenuClick(101);" style="display: none;"
                                            align="center">
                                            <a style="cursor: pointer;">
                                                <br />
                                                休假的管理原则</a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td id="td102" class="leftmenu-greytip" onclick="MenuClick(102);" style="display: none;"
                                            align="center">
                                            <a style="cursor: pointer;">
                                                <br />
                                                特别声明</a>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <table width="90%" border="0" align="center" cellpadding="2" cellspacing="0">
                                    <tr>
                                        <td align="center">
                                            <a href="Newcomer.aspx">
                                                <img src="images/newer01.jpg" width="24" height="22" /></a>
                                        </td>
                                        <td align="center">
                                            <a href="EntryGuid.aspx">
                                                <img src="images/entry01.jpg" width="24" height="22" /></a>
                                        </td>
                                        <td align="center">
                                            <a href="ExitGuid.aspx">
                                                <img src="images/exit01.jpg" width="32" height="22" /></a>
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
                            <td id="tdyinyan" valign="top" style="padding: 10px 30px; line-height: 20px;">
                                <div class="title-orag">
                                    引 言
                                </div>
                                <br />
                                <p>
                                    <strong>欢迎你，亲爱的新同事！<br />
                                        <br />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;欢迎你加入星言云汇，开始你个人事业发展的新历程。我们相信你能很快融入星言云汇朝气蓬勃的员工队伍，感悟到星言云汇开放进取的公司文化特色，延承星言云汇踏实严谨的工作作风。<br />
                                        <br />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;员工是公司发展之本，是公司最宝贵的财富。积极发挥员工的潜能，切实关心员工的利益，是公司走向成功的基础。因此，星言云汇愿意并努力为你提供一切可能的个人发展机会与空间，使你尽情施展才华，实现你职业理想的同时也成就我们共同的目标。<br />
                                        <br />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;本手册将引导你全面了解公司对员工的责任以及员工须遵守的工作守则和纪律，协助你明确公司的发展目标与规章制度，请务必详细阅读，并参照执行。如有任何询问或建议，请随时向你的上级主管或人力资源与行政部门提出。<br />
                                        <br />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;本员工手册一切有关条文于适当的时间可能有所更改，以符合新的法律条款或公司需求。公司将保留必要时做修改的权利。
                                    <br />
                                        <br />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;本员工手册解释权属于星言云汇人力资源与行政部。<br />
                                        <br />
                                        <br />
                                    </strong><span style="font-style: italic;">（本手册属于公司财产，仅限公司在职人员持有，员工不得将此手册对外流传，违者必究。）</span><br />
                                </p>
                            </td>
                            <td id="td111" valign="top" style="padding: 10px 30px; line-height: 20px; display: none;">
                                <div class="title-orag">
                                    （星言云汇）
                                </div>
                                <br />
                                <br />
                                <p>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
                                </p>
                            </td>
                            <td id="td121" valign="top" style="padding: 10px 30px; line-height: 20px; display: none;">
                                <div class="title-orag">
                                    经营理念、价值观和道德标准
                                </div>
                                <br />
                                <p>
                                    <strong>1. 我们的价值观： 专业、尊重、诚信、责任、开放</strong>
                                    <br />
                                    <br />
                                    <strong>2.我们的使命：创造传播奇迹，体验生命之美</strong>
                                    <br />
                                    “传播”的真正有意义，是我们以自己的方式将其演化成一门迷人的生活艺术，并以它的名义创造出真正有益于人类和社会的价值。
                                <br />
                                    在创造价值的同时，传播之道也赋予我们一种新的生活方式和新的生活态度：
                                <br />
                                    我们向往美好的未来。我们的团队勇敢而智慧。<br />
                                    <br />
                                    我们以出色的学习力和无穷的创造力在我们自己选择的土地上耕耘着，以前所未有的速度和机敏引领市场发展的潮流。我们遵守现有的游戏规则，同时致力于建立更成熟、更科学、更公平的新的游戏规则。<br />
                                    不求大，只求精；
                                <br />
                                    不求量，只求质；
                                <br />
                                    不求小利，只求大丰。<br />
                                    我们要以创新、专业、敬业之风，成就中国传播业的永久辉煌与荣光。
                                <br />
                                    <br />
                                    <strong>3.我们的经营理念：合适就是竞争力</strong>
                                    <br />
                                    <br />
                                    <strong>4．我们的道德标准（共10条）：</strong>
                                    <br />
                                    1) 我们鼓励创新。因为只有创新，生命才变的富有价值；只有创新，生活才富有意义；只有创新，企业才富有生机。
                                <br />
                                    2) 我们欣赏自信的专业人士，钦佩以高超技艺尽职尽责工作的传播业专家。因为内心强大，会更充分的尊重同事、尊重客户。
                                <br />
                                    3) 我们欣赏富有敬业精神的人。因为不拖延、主动承担的责任感是执行力的保证。
                                <br />
                                    4) 我们喜欢富有工作激情的人，因为激情可以让事业得以善始，执行得以专注，自己的选择得以快乐。
                                <br />
                                    5) 我们喜欢穿着得体、平易近人、举止优雅的人，因为只有这样才是我们的同类，使心灵更容易相通，彼此配合更易默契。
                                <br />
                                    6) 我们讨厌搬弄是非的人。因为做人重于做事，人品是最被看重的要素。
                                <br />
                                    7) 我们最看不起的就是那些技术卓越的马屁专家。因为真实、诚信是我们信奉的原则。
                                <br />
                                    8) 我们敬佩那些诚心诚意培养自己接班人的主管，我们认为那些心甘情愿让位于更有才华的人的公司领导或部门主管是商业社会中最可爱的人。
                                <br />
                                    9) 我们认为赢得客户的赞誉是我们最大的荣耀，不断为客户创造价值是我们永无止境的追求。
                                <br />
                                    10)我们从不害怕失败，也从不隐瞒错误，我们始终坚持不断的进行自检和自省，从而保持不骄不燥的企业作风；这正是我们赖以前进的护卫艇，她伴随我们渡过险滩、急流勇进，始终傲立潮头！
                                <br />
                                </p>
                            </td>
                            <td id="td211" valign="top" style="padding: 10px 30px; line-height: 20px; display: none;">
                                <div class="title-orag">
                                    聘用政策
                                </div>
                                <br />
                                <p>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;根据业务发展需要，通过审慎的筛选面试，星言云汇挑选聘用适合的员工。一经聘用，星言云汇期望员工与公司共同发展，并愿意为员工提供尽可能的个人发展空间与成长机会。<br />
                                    <br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;星言云汇努力保持员工队伍的稳定性，以利于公司业务和客户关系的维护及发展。公司全力建设一支高素质、专业性强、敬业负责、团结互助、朝气蓬勃、积极向上的团队。
                                </p>
                                <p>
                                    <strong>1.聘用</strong><br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;星言云汇组织严格的笔试及面试程序对候选人进行全面考量，之后向合适的应聘者发送公司的聘用信（Offer Letter）；并依照相关国家规定，与每位正式员工签订劳动合同。<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;新员工报到时应向公司人力行政发展中心交验、备份相关证件，具体请参见Offer Letter。<br />
                                    <br />
                                    <strong>2.下列情形之一者不予聘用：</strong><br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;1) 有刑事犯罪行为者<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2) 身体患有重大疾病或传染疾病者<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3) 年龄不满16周岁者<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4) 曾被本公司辞退或未经批准擅自离职者<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;5) 与原单位劳动合同未解除者<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;6) 未婚先孕及违反国家生育政策的女性<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;7) 或有其他不适合情况者（如提供不实履历等不诚信行为者）<br />
                                    <br />
                                    <strong>3．试用期</strong><br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;被录用的员工将在到职时与公司签署《劳动合同》，并按照合同期限规定一定的试用期（试用期时间详见《劳动合同》）。试用期结束前，公司将对员工的工作能力和表现进行全面的评估考核，员工的直接上级根据其试用期表现填写《试用期评估表》报管理层审批，如员工的工作表现达到公司规定的标准，则成为公司正式员工，享受正式员工的相关待遇；反之，若未能达到规定的标准，公司将在试用期内书面通知解除《劳动合同》。<br />
                                    <br />
                                    <strong>4．员工档案</strong><br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;凡本公司正式聘用的合同制员工必须与原单位终止或解除劳动关系；公司为员工建立内部个人档案，记录员工各种人事和培训等纪录，员工须提供正确的个人资料，如有任何个人信息变更（包括住址、电话、婚姻、子女等），应及时呈报人力资源与行政部备案，否则由此产生的一切后果由员工承担。此外，员工应提供其他相关个人背景资质资料，如通过各类考试、技能职称和学历变化等，公司在安排员工培训、组织晋升评估、充实空缺职位时将受到优先考虑。<br />
                                    <br />
                                    <strong>5． 辞职及终止聘用</strong><br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;员工和公司均有权解除劳动合同，员工提出解除劳动合同的，需提前三十（30）天以书面形式通知公司人力资源与行政部。员工在试用期内不受此限，按本手册离职手续规定的有关内容执行。正式员工合同到期视双方意向决定是否续签或自动终止。<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;正式员工在工作中严重违反国家法律或公司劳动纪律及规章制度，或有任何形式的不诚信行为（如营私舞弊、收取回扣、盗用公司时间资源、欺上瞒下、提供任何虚假信息等）、重大过失、严重失职、给公司造成损失者，公司有权即时解除劳动合同，并无须提前警示与支付补偿金。被公司免职者，日后一律不再聘用。<br />
                                </p>
                                <div class="title-orag">
                                    合同管理
                                </div>
                                <br />
                                <p>
                                    <strong>1. 劳动关系成立前提</strong><br />
                                    员工与星言云汇确立劳动关系的前提条件是：员工本人确保在入职之前已经与原单位终止或解除劳动关系的事实。
                                <br />
                                    <br />
                                    <strong>2. 劳动合同的签订</strong><br />
                                    公司有权决定与员工的劳动合同签订或续订的期限，并在《劳动合同法》规定的范围内约定新入职员工的试用期限。
                                <br />
                                    <br />
                                    <strong>3. 劳动合同的解除、终止与续订</strong><br />
                                    <br />
                                    <strong>3.1 公司提前通知的合同解除情形</strong>
                                    <br />
                                    <br />
                                    有下列情形之一，公司可以解除劳动合同，但应提前30日通知员工或支付相当薪金作为代通知金，员工应及时办理离职手续，如果由于员工未按规定办理交接手续，所带来的一切影响及损失由员工承担。<br />
                                    1）由于公司营业亏损或生产经营发生困难，缩减或终止某个业务<br />
                                    2）公司经营方针发生变更或调整<br />
                                    3）员工无法胜任工作，经过指导和帮助，仍不能胜任工作的。例如，工作品质低劣，传播负能量信息、工作态度消极、经常无法如期完成工作，或个人技能差而影响团队工作进度，重复判断或执行错误，长期缺乏专业技能，以及被客户/供应商投诉达3次以上。<br />
                                    <br />
                                    <strong>3.2 公司无提前通知的合同解除情形</strong>
                                    <br />
                                    <br />
                                    有下列情形之一的，公司有权即时解除劳动关系，并无需提前通知，且无须支付任何经济补偿：（同时参考<span style="font-style: italic;">《星言云汇奖惩条例》</span>
                                    ）<br />
                                    1)  在试用期内，公司认为不符合录用条件或员工违反治安管理、计划生育等国家政策的<br />
                                    2)  员工在签聘用信或办理入职手续、面试时提供不实信息的<br />
                                    3)  员工对公司或其他员工施以暴行或有不道德行为<br />
                                    4)  员工工作态度恶劣，不诚实，效率低下，屡教不改<br />
                                    5)  员工严重违反国家法律、行业准则<br />
                                    6)  员工毁坏公司的财物或故意过度滥用公司设备<br />
                                    7)	员工泄露属于公司商业信息和机密<br />
                                    8)	员工入职一个月内未能将社保关系转入公司<br />
                                    9)	严重违反公司各项规定、公司制度、工作守则和纪律、人力资源政策、《员工手册》、《奖惩条例》等，按照公司相应规定可以解除劳动合同的<br />
                                    <br />
                                    <strong>3.3 员工解除劳动合同的情形 </strong>
                                    <br />
                                    <br />
                                    1)	员工解除劳动合同应当提前30日以书面形式通知公司。<br />
                                    2)	员工解除劳动合同未提前30日以书面形式通知公司，或通知公司后未交接工作完毕，未经公司批准就擅自离职，由此给公司造成的经济损失，公司有权要求员工进行经济补偿及赔偿，并可在员工工资中扣除及以其他方式追偿。
                                <br />
                                    <br />
                                    <strong>3.4 劳动合同续订<br />
                                        <br />
                                    </strong>
                                    劳动合同期限届满，劳动合同即终止，合同双方经协商同意可以续订劳动合同。<br />
                                </p>
                                <div class="title-orag">
                                    员工离职手续规定
                                </div>
                                <br />
                                <p>
                                    <strong>1、辞职</strong><br />
                                    1）正式员工<br />
                                    辞职至少须提前30日告知业务主管和人力资源与行政部（同时，亦可在OA系统提交离职申请），以便公司进行必要安排。辞职生效日期应以各团队领导批复为准。<br />
                                    <br />
                                    2）试用期员工<br />
                                    试用期内员工辞职需提前三天以告知业务主管和人力资源与行政部（同时亦可在OA系统中提交离职申请），但应有足够时间安排好工作交接。<br />
                                    <br />
                                    <strong>2.离职程序和手续</strong><br />
                                    1)  薪资结算程序<br />
                                    正常状况下，公司将在发薪日给付上月考勤对应的当月薪资，次月发薪日发放结余薪资，其中包括：减除各项应扣除金额后所余薪资等。<br />
                                    <br />
                                    2)  员工任职的最后一个工作日前，须归还所有公司物品和个人借款，办理好所有离职手续（工作日以实际工作为界限，办理手续日不计），应严格遵守保密协议，不得私人携带任何公司资料离开公司。<br />
                                    <br />
                                    3)	未获公司批准而擅自离职者，将视为旷工并按照旷工处罚条例处理。<br />
                                    <br />
                                    4)	离职员工需在离职日期后将个人档案和社保等同步从星言云汇转出，公司将不再负责相关的文件证明或其他协助的办理，并不承担任何费用及责任。<br />
                                    <br />
                                </p>
                            </td>
                            <td id="td221" valign="top" style="padding: 10px 30px; line-height: 20px; display: none;">
                                <div class="title-orag">
                                    薪酬与福利
                                </div>
                                <br />
                                <p>
                                    <strong>1.薪资政策</strong><br />
                                    1)	保持在同行业中有竞争力的薪资待遇。<br />
                                    2)	星言云汇薪资是绝对保密的，任何人不得打探或议论，违者公司有权重罚（扣除当月绩效）或即时解除劳动合同且不支付任何经济补偿。<br />
                                    3)	员工薪资是由岗位价值、本人工作能力和个人业绩成果以及公司的经营状况等要素来确定的，并可根据此相关要素适时调整。<br />
                                    4)	各职位间的薪资结构做到公平合理地回报员工对公司的贡献。<br />
                                    5)	员工每次薪资调整或升职加薪间隔不得少于半年。<br />
                                    6)	职位相同的员工，按照其个人综合技能以及对公司贡献大小分为不同级别，享受不同待遇和奖励。<br />
                                    7)	员工（含试用期和正式）无法胜任职责内工作，或其他表现欠佳，公司可随时通过严肃的工作评估，对其采取降职、降薪等措施。<br />
                                    8)	公司有权根据员工的表现、擅长和短板、以结合公司业务发展的需要，来调整员工的职位以及对应的薪资。<br />
                                    9)	公司严格按照国家相关规定执行，所有报酬均须缴纳个人所得税。<br />
                                    10)	劳动关系存续期间，员工绩效工资与公司及员工所在业务团队的经营状况密切相关。同时，公司及员工所在业务团队可以根据员工工作表现、业绩情况、业务水平、员工岗位职务调整变化情况对员工的绩效工资等工资水平进行确定或调整，但确定或调整后的工资不低于本地最低工资。<br />
                                    <br />
                                    <strong>2. 薪资结构和发放</strong><br />
                                    1)	根据星言云汇的薪资政策，员工薪酬包括：<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;a) 月基本工资+月绩效工资<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;b) 年度双薪即第13个月工资（本年度12月31日在职员工享有）<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;c) 奖金：视公司效益、团队业绩和个人工作表现而定<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;d) 补贴：每个工作日15元的餐补；
                                <br />
                                    2)	公司实行月薪制，每月发薪日为次月5日前。<br />
                                    3)	新员工第一个月工资以及离职员工的最后一个月工资以其实际工作天数计算。<br />
                                    4)	员工工资调整，如转正、升职等，从正式生效之日开始计算。<br />
                                    5)	各类特殊、非常规病事年假等工资结算原则请参见《星言云汇休假政策》<br />
                                    <br />
                                    <strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;为员工提供一套完整、完善的员工福利制度是星言云汇的既定政策。除法定福利外，所有全职员工还可享有额外的商业意外保险、商业补充医疗险、年度健康体检、疫苗注射等福利。</strong><br />
                                    <br />
                                    <strong>1、社会福利</strong><br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;目前公司员工均享有按照国家法律规定的、企业必须提供的社会保险和公积金等社会福利，通常包括以下六方面。<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;养老保险<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;失业保险<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;医疗保险<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;工伤保险<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;生育保险<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;住房公积金<br />
                                    <br />
                                    <strong>2、补充福利</strong><br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;除根据当地法规提供社会福利，作为整体薪酬的一部分，员工还享有以下补充福利：<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;1) 公司每年组织一次体检。<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2) 节日礼物<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3) 意外伤残险<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4) 意外医疗险<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;5) 乘坐交通工具意外险。<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;6) 全职正式员工享有商业补充医疗险。<br />
                                    <br />
                                    <strong>有关福利相关内容，具体请参见：星言云汇内网—福利中心<a href="http://xy.shunyagroup.com" target="_blank"
                                        style="text-decoration: underline;">http://xy.shunyagroup.com/</a></strong>
                                </p>
                            </td>
                            <td id="td241" valign="top" style="padding: 10px 30px; line-height: 20px; display: none;">
                                <div class="title-orag">
                                    绩效与激励
                                </div>
                                <br />
                                <strong>人才推荐</strong><br />
                                <p>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;星言云汇不仅将人才作为核心竞争力，更视为星言云汇最可宝贵的资本和资源。自星言云汇建立以来，就极度重视人才的甄选发掘，培养提拔；对品行优良，业绩出色的成员，必授以实权，委以重任。人才的进步和提升与星言云汇的发展和壮大，具有相互依存，密不可分的关联性。<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;为了更有效地发掘人才，星言云汇长期推行“内部人才推荐奖励计划”，简称“伯乐计划”。星言云汇鼓励每位同事：不仅要关注自身的成长与成就，也需重视各类人才的发现和推荐；要举贤荐能，成为发现人才的“伯乐”。<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;为了更好地推进伯乐计划，星言云汇特制定奖励细则，其中具体的伯乐计划定义如下：员工内部推荐是指公司在职员工根据公司发布的空缺岗位的任职资格要求，向公司推荐同学、朋友、亲属等非公司内部员工的招聘方式，即使被推荐人的信息已在公司内部备案，但被推荐人的简历在近期招募中未曾被HR或团队联系过的即被认定为内部推荐。 
                                <br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;1.	推荐人推荐的职位不受限制，成功推荐人才（指所推荐的人已经顺利转为正式员工）的在职推荐人将获得相应的奖励金。职位的确认以入职的职位为准。<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2.	星言云汇推荐人不受职位级别限制。除公司及各BU人力资源部员工及各BU总监以上人员外，其他正式员工如成功推荐人才均可享受推荐奖励金。<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3.	被推荐员工一旦获得转正，即可确认在职推荐人将享受推荐奖金。在推荐人及被推荐人均未提出离职情况下，推荐奖金将于被推荐人转正后次月发放，推荐人承担个人所得税。<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4.	所有员工推荐候选人，都需填写《星言云汇人才推荐表》。表中列明各项推荐信息及推荐理由，并直接发至HR，对推荐信息进行登记和确认。如被推荐人入职前，推荐人未将《推荐表》在人力资源部备案，则该推荐无效，推荐人也不能享受推荐奖励。推荐信息由公司人力资源部负责人汇总核准，各分/子公司HR需要定期向公司总部上报推荐信息，以统一执行兑现政策。<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;5.	如果几位员工推荐同一候选人，以推荐日期最早在HR处备案的为准。<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;6.	公司的招聘职位发布在公司外部网页，或可直接向HR查询。推荐奖励的具体政策及奖励金标准请见公司当年度发布的《内部人才推荐－“伯乐”计划细则》。<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;7.	本“内部人才推荐奖励计划”最终解释和说明权由公司人力行政发展中心负责。<br />
                                    <br />
                                </p>
                            </td>
                            <td id="td251" valign="top" style="padding: 10px 30px; line-height: 20px; display: none;">
                                <div class="title-orag">
                                    培训制度
                                </div>
                                <br />
                                <p>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;公司致力于向员工提供公司内部和外部的学习与发展机会。员工个人必须拿出动力与动机，承担自己职业发展的第一责任。同时，公司为员工提供适当的学习和发展资源，以确保员工掌握工作中必要的知识、技能和观念。<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;个人学习的方法是多样化的，即70％是“工作中培训”，20％是“课堂培训计划及研讨会”，以及10％的自学。
                                </p>
                                <br />
                                <p>
                                    <strong>1.培训目标</strong>
                                    <br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;全面提高员工的综合业务素质，加强客户服务及项目管理等方面的专业知识和工作技能，确保星言云汇始终拥有一支高服务水准和强敬业精神的精英团队。<br />
                                    <br />
                                    <strong>2.培训计划的制订</strong>
                                    <br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;星言云汇人力行政发展中心组织制定执行和管理员工培训发展计划。<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;员工根据职位的要求、绩效目标和个人职业发展目标制订“个人在星言云汇的发展计划”，并与直线主管达成一致。<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;公司根据企业发展目标，业务发展状况以及员工需求制订公司培训计划。<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;各业务团队和部门根据公司业务发展状况以及员工需求制订本部门培训计划。<br />
                                    <br />
                                    <strong>3.培训课程安排</strong>
                                    <br />
                                    各业务团队和职能部门均可根据业务需要提出培训计划并安排培训；覆盖星言云汇全体员工的培训，统一由公司人力及行政发展中心整体安排协调。<br />
                                    <br />
                                    <strong>4.培训讲师和教材</strong>
                                    <br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;1)	公司的所有总监、客户经理及高级客户主管，均应作为相关知识领域的讲师，并成为其岗位职责的一部分。<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2)	公司不定期邀请外部行业专家到公司组织专题培训和分享。<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3)	员工参加公司资助的外训课程后，有义务将其所学知识分享公司其他同事。 
                                    <br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4)	员工可随时在公司内部服务器中学习公司历次培训的内容。<br />
                                    <br />
                                    <strong>5.培训时间</strong>
                                    <br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;1)	公司内部业务技能培训，主要安排在正常工作时间或利用节假日（周末）举行。公司要求参加的员工必须参加，公司不提供任何形式的补偿。<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2)	经公司特别批准，参加外部培训课程，视需要可利用工作时间，但必须做好工作安排并在HR处报备。<br />
                                    <br />
                                    <strong>6.培训的申请和记录</strong>
                                    <br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;1)	参加内部培训，无须填写申请表，遵从公司安排即可。<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2)	如果员工对公司外部专项培训有兴趣，须经部门领导批准。如若需公司资助费用，则需经公司总裁核准，并与公司签订培训协议。<br />
                                    <br />
                                    <strong>7.员工的培训义务</strong>
                                    <br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;1)	按照公司的培训要求，相关员工应准时参加课程，不得迟到、早退。<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2)	员工不得拒绝公司培训讲师所要求的提问与测验考核，HR应记录成绩并保存。<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3)	员工应在其直属领导监督指导下，在工作中积极实践和应用培训中所掌握的理论、知识和技能，并和其他同事分享成功经验。员工直属领导有权考察其培训效果，对于培训无效的员工，如属个人原因，将视实际情况作为年终考核分数的参考。<br />
                                    <br />
                                </p>
                            </td>
                            <td id="td261" valign="top" style="padding: 10px 30px; line-height: 20px; display: none;">
                                <div class="title-orag">
                                    晋升制度
                                </div>
                                <br />
                                1. 目的<br />
                                为了进一步发展与吸引星言云汇优秀人才，激发星言云汇员工与企业共同进取的上进心，提高全体员工的工作能力和职业素质。<br />
                                <br />
                                2. 晋升申请必备基本条件<br />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;1)	较高职位的全面工作技能或潜能<br />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2)	相关的工作经验和资历<br />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3)	本职工作表现优秀<br />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4)	较好的团队适应力和个人发展潜力<br />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;5)	认同星言云汇核心价值观<br />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;6)	距上一次获准晋升或申请评估（包括转正）至少间隔6个月<br />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;7)	当出现职位空缺时，应首先考虑现有在职员工。如确无合适的备选对象，再考虑对外招聘等引进外部人才的有效措施。<br />
                                <br />
                                3. 员工晋升的方式<br />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;1)	定期评估<br />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;每年度由星言云汇人力行政发展中心根据公司整体业务的运作状况与公司本年度业绩考核安排，协调各业务团队对全体员工进行业绩考核评估，择优晋升，并报星言云汇管理委员会核准；<br />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2)	不定期<br />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;在年度工作中，对做出重大贡献或表现突出的员工，随时晋升；<br />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3)	内部竞聘<br />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;在内部职位（SAE以上）出现空缺时，由人力行政发展中心发布信息并组织竞聘考核工作。<br />
                                <br />
                                <strong>4.	员工晋升的审核权限</strong><br />
                                <table border="1" cellpadding="0" cellspacing="0">
                                    <tr style="border-width: thin; border: 0 1 0 0; background-color: Olive;">
                                        <td style="width: 200px;" align="center;">
                                            <span style="font-weight: bolder; font-style: italic;">职位</span>
                                        </td>
                                        <td style="width: 300px;" align="center">
                                            <span style="font-weight: bolder; font-style: italic;">提名人</span>
                                        </td>
                                        <td style="width: 200px;" align="center">
                                            <span style="font-weight: bolder; font-style: italic;">审核与核定</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 200px;">AM（含AAM）及以上职位
                                        </td>
                                        <td style="width: 300px;">团队总经理（含）以上
                                        </td>
                                        <td style="width: 200px;">星言云汇总裁核定
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 200px;">SAE（含SAE）以下职位
                                        </td>
                                        <td style="width: 300px;" rowspan="2">业务部总监提出名单及资格说明
                                        </td>
                                        <td style="width: 200px;" rowspan="2">团队总经理（含）以上核定，<br />
                                            人力行政发展中心复核<br />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td id="td271" valign="top" style="padding: 10px 30px; line-height: 20px; display: none;">
                                <div class="title-orag">
                                    工作调配
                                </div>
                                <br />
                                <p>
                                    <strong>1. 目的</strong><br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;1)	促进组织内的合理人才流动，进而减少人才的外流。<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2)	通过人才合理流动，从而促进人才与职位的适配率，进而提高工作效能。<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3)	尊重人才的兴趣与特长，提供相应的职位机会，从而帮助人才的快速成长，帮助调整倦怠期。<br />
                                    <strong>2. 前提与基础</strong><br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;尊重、开放、全局观<br />
                                    <strong>3. 人员调配细则</strong><br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;1)	调配概念：是指因公司业务原因或个人合理原因而在星言云汇内部产生的人员流动。分为公司主动型及员工主动型。<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2)	调配资格（员工主动型）：<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;☆ 调配前连续3个月内或实习期内，绩效表现为3.5分以上时，方可考虑调配。<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;☆ 员工入职满1年。<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3)	公司主动型：<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;☆ 当出现“组织架构调整”、“客户转换”、“岗位空缺的需求”、“应届生实习后的定岗”等原因时，则原Team需要与HR沟通，提前表达意向。<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;☆ 当员工提出辞职时，或主管发现员不适岗或倦怠时，其主管需与员工进行深入沟通，除帮助员工指引方向及答疑解惑外，也可以建议员工调配到其他组或服务其他的客户。员工接受此建议后，主管需与HR沟通，以询问其他团队的职位需求，尽量将欲辞职员工留在星言云汇，以减少人才的流失。<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4)	员工主动型：<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;☆  调配前连续3个月内或实习期内，绩效表现为3.5分以上时，方可考虑调配。<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;☆员工入职2年后，如因对某种客户类型产生倦怠或因自我发展所提出的提高能力和丰富职业经验等需求而提出的调配，经过HR协调后，在不影响业务的情况下，公司及主管会最大程度的给予支持，星言云汇愿意提供广阔的平台使员工发挥所长、得到所需。<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;☆员工入职2年以内，因其个人兴趣、特长等原因自认为与原工作不完全匹配时而提出的调配，先经过HR的协调，原团队主管会根据实际情况综合分析考虑来建议是否调配。<br />
                                    <strong>4.  操作程序及重点</strong><br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;1)	如果某业务主管希望调用其他团队的某员工，可先与HR商谈，了解相关情况，并且与该员工的团队领导讨论可行性。若决定可以调职，应先与该员工交换意见，取得共识，确定日期及工作交接，由HR协调，处理好有关事宜。<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2)	如果某业务主管希望对外推荐员工，可先与HR商谈，了解其他团队的需求，并且与需求的团队领导讨论可行性。若决定可以调职，应先与该员工交换意见，取得共识，确定日期及工作交接，由HR协调，处理好有关事宜。<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3)	如果员工本人有调职的想法，需先与HR讨论，HR了解其他团队需求情况后，明确员工基本符合需求团队要求时，再与其直属主管商讨，并由团队负责人与HR及希望调往的部门领导讨论协调。有此意向的员工必须先向HR口头申请及沟通，由HR协调，禁止员工先行自行沟通。调动未确定之前，不可单方面对外宣扬。<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4)	原主管、新主管与人力资源与行政部需要一起讨论所涉及员工的正式调职日期及其他需要进行调整的事项，如职称、薪资等，并达成共识，完成调岗手续。<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;5)	HR需统筹各团队之间的人员调配需求，并积极组织实施。<br />
                                    <br />
                                </p>
                            </td>
                            <td id="td281" valign="top" style="padding: 10px 30px; line-height: 20px; display: none;">
                                <div class="title-orag">
                                    奖惩条例
                                </div>
                                <br />
                                <p>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;为增强公司的经营活力，鼓励员工积极向上，严肃纪律，维护公司正常的工作秩序，保持高度工作效率和优良服务水准，特此发布实施本奖惩条例。<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;公司实施奖惩条例时，在奖励上，始终将精神与物质相结合；在惩罚上，坚持惩前毖后、治病救人的原则。<br />
                                </p>
                                <p>
                                    <strong>1. 审批权限：</strong>
                                    <br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;由部门主管提出，各公司的业务总监、总经理（含）以上审批，必要时交由星言云汇管理委员会复审确认后，由星言云汇人力行政发展中心执行并备案。<br />
                                    <br />
                                    <strong>2.奖励种类与形式：</strong><br />
                                    1)	公开表彰（在公司内网及大会公开表扬，优先提供职业培训与发展机会）<br />
                                    2)	通令嘉奖（同上，并授予奖牌或奖状，加发半个月工资）<br />
                                    3)	总裁特别奖（授予奖牌或奖状，加发一个月工资）<br />
                                    4)	授予星言云汇最高荣誉称号（载入公司杰出员工纪念册、授予奖杯、加发一个月工资）<br />
                                    5)	授予绩效表现嘉奖（根据当月绩效突出程度给予一定的绩效奖金）<br />
                                    <br />
                                    <strong>3.奖励条件：</strong><br />
                                    <strong>1) 员工有下列情形之一者，给予公开表彰：</strong><br />
                                    ☆ 工作效率出众，工作质量出色，成为团队楷模与骨干<br />
                                    ☆ 提出工作流程上合理化建议并取得明显成绩和成果<br />
                                    ☆ 维护团体荣誉、热心公司事务、有具体表现者<br />
                                    ☆ 一贯忠于职守，真诚、积极负责，富有工作激情并表现突出者<br />
                                    ☆ 遇有困难或危险，勇于负责，能处理得当者<br />
                                    ☆ 拾金不昧者<br />
                                    ☆ 其他应当给予奖励者<br />
                                    <br />
                                    <strong>2) 员工有下列情形之一者，给予通令嘉奖：</strong><br />
                                    ☆ 在完成工作、执行任务、提高服务质量、节约公司资源等方面，成绩显著者<br />
                                    ☆ 管理有方、专业技能超群、使所负责业务进展有相当具体的成效<br />
                                    ☆ 为维护公司利益做出突出贡献，或在社会活动中为公司争得荣誉者<br />
                                    ☆ 甘心为公司利益或团队进步做出无私奉献并有具体事迹者<br />
                                    ☆ 遇有重大困难或灾难，勇于负责，并处理得当因而减少损失者<br />
                                    ☆ 勇于揭发、检举损害公司利益和声誉的各种违纪违法行为者<br />
                                    ☆ 其他与上述所列事实类似者<br />
                                    <br />
                                    <strong>3) 员工有下列情形之一者，授予总裁特别奖：</strong><br />
                                    ☆ 在公司业务开拓发展以及经营管理上，有重大贡献，成效卓越<br />
                                    ☆ 对重大危害本公司权益事情或重大舞弊事件，能事先检举、防止，使公司避免重大损失<br />
                                    ☆ 遇非常事故临机应变，措施得当或不顾自身安危，勇敢救护而保全人命及公物<br />
                                    ☆ 其他与上述所列事实类似者<br />
                                    <br />
                                    4)	员工符合有下列条件者，授予星言云汇最高荣誉：<br />
                                    ☆ 认同星言云汇企业文化和价值观，热爱公司并乐意与公司一起成长<br />
                                    ☆ 品德正直，言行得体，符合星言云汇之道德标准规范<br />
                                    ☆ 勇于承担责任和压力，效率出众，质量出色，能成为团队楷模<br />
                                    ☆ 愿意为公司利益或团队进步做出无私奉献<br />
                                    ☆ 为公司业务创新、服务完善、团队发展、文化延承持续努力<br />
                                    5)	员工符合有下列条件者，绩效表现嘉奖：<br />
                                    ☆ 积极努力地履行职责，且能够突破资源的限制完成不易完成的工作内容。<br />
                                    ☆ 能够主动承担，且超额完成工作任务。<br />
                                    <br />
                                    <strong>4.纪律处分类别及审批权限：</strong><br />
                                    纪律处分分以下三个类别，审批权限见下表。<br />
                                </p>
                                <table border="1" cellpadding="0" cellspacing="0">
                                    <tr style="border-width: thin; border: 0 1 0 0;" align="center">
                                        <td style="width: 30%;">纪律处分类别
                                        </td>
                                        <td style="width: 70%;">审批权限
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 30%;">1.口头警告<br />
                                            （记录在个人档案）
                                        </td>
                                        <td style="width: 70%;">部门主管/业务负责人提出，各业务团队总监/总经理审核执行并交人力资源与行政部备案。<br />
                                            三个月内表现良好，可由各业务团队向公司提出撤消。
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 30%;">2.书面警告<br />
                                            （记录在个人档案）
                                        </td>
                                        <td style="width: 70%;">部门主管/业务负责人提出，各业务团队总监/总经理审核，如有必要由星言云汇管理委员会复审确认后，由公司星言云汇人力资源与行政部执行备案<br />
                                            公司可以根据违纪情节的严重程度，适当变更当事员工的工作岗位或降职、降薪。
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 30%;">3.辞退（解除劳动合同）
                                        </td>
                                        <td style="width: 70%;">部门主管/业务负责人提出，各业务团队总监/总经理审核，如有必要由星言云汇管理委员会复审确认后，由公司星言云汇人力资源与行政部执行备案。
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <p>
                                    <br />
                                    <strong>5.纪律处分适用情况：</strong><br />
                                    违反公司各项规定和纪律，视情节轻重，公司有权给予口头警告、书面警告，直至即时解除劳动合同且不支付任何经济补偿。<br />
                                    <br />
                                    <strong>1)	员工有下列情形之一者，公司有权给予口头警告：</strong><br />
                                    ☆ 违反公司规定在办公区域吸烟喝酒并劝告无效<br />
                                    ☆ 下班后无故在公司逗留从事与工作无关的行为（如玩游戏等）并劝告无效<br />
                                    ☆ 上班时间睡觉或情绪倦怠及精神不佳者<br />
                                    ☆ 因粗心大意工作发生过失而影响工作质量<br />
                                    ☆ 着装不符合公司要求，并已三次以上给予提醒和解释<br />
                                    ☆ 一个月内无故迟到/早退3次以上者<br />
                                    ☆ 其他与上述情形类似、违反公司制度条例之具体事实<br />
                                    <br />
                                    <strong>2)	员工有下列情形之一者，认知态度欠佳，公司有权给予书面警告或降薪降职：</strong><br />
                                    ☆ 一个月内无故迟到早退5次以上者<br />
                                    ☆ 上班时间擅离岗位，或消极怠工<br />
                                    ☆ 不服从主管人员合理指导或工作安排
                                    <br />
                                    ☆ 工作时间内做与工作无关的事（如上网因私聊天、购物、游戏）者
                                    <br />
                                    ☆ 无故不按期完成上级指令，并影响公司利益<br />
                                    ☆ 对同事恶意攻击伤害、制造事端，情节轻微者<br />
                                    ☆ 工作中赌博、酗酒等，影响自己或他人工作<br />
                                    ☆ 其他与上述情形类似、违反公司制度条例之具体事实<br />
                                    ☆ 连续超过3次不按照公司规定按时提交考勤者<br />
                                    <br />
                                    <strong>3)	员工有下列情形者，视为严重违反本公司规章制度，公司有权即时解除劳动合同且不支付任何经济补偿，并视情节给予通告：</strong><br />
                                    ☆ 在书面警告后仍无改善现象，或三个月内再次违反公司规定<br />
                                    ☆ 存在以下任何一条情形者：<br />
                                    √ 连续2个月成为团队迟到早退最多员工者<br />
                                    √ 累计一个月内无故迟到早退6次以上者<br />
                                    √ 委托或伪造出勤记录者，托人或代人打卡者<br />
                                    √ 连续旷工2天以上（含2天），或一年内累计旷工2天以上（含2天）者<br />
                                    √ 受到口头警告3次以上（含3次）者<br />
                                    √ 受到书面警告2次以上（含2次）者<br />
                                    √ 发现危害公司安全，不迅速报告而任其发生者<br />
                                    √ 因疏忽或监督不周导致发生重大事故者<br />
                                    √ 在公司大吵大闹、打架、损坏公司物品或设备等，搅乱正常工作秩序或影响工作开展者<br />
                                    √ 对客户缺乏礼貌、有损公司形象或由于渎职或者疏忽大意导致客户投诉<br />
                                    √ 泄露公司商业秘密<br />
                                    √ 不服从主管合理工作指导或安排，屡劝不听者<br />
                                    √ 拒不服从公司人事调动、岗位调整和工作安排者<br />
                                    √ 传播负能量、造谣惑众，破坏团结，煽动他人闹事、怠工者<br />
                                    √ 对公司及同事造谣中伤、诽谤、诋毁、离间者<br />
                                    √ 弄虚作假和欺诈，包括但不限于伪造单据、伪造履历、隐瞒病史、未按财务规定进行报销、未按采购流程及相应管理规定进行采购申请与审批、提供虚假证明文件及相关信息<br />
                                    √ 违反诚信原则，造成恶劣影响者<br />
                                    √ 事先未经本公司许可，与其他单位建立劳动或劳务关系者<br />
                                    √ 公开诋毁公司的名誉和信用，使公司受到伤害<br />
                                    √ 贪污挪用公款、挥霍公司资源、盗窃公司或他人物品、对他人使用诬蔑、恐吓、胁迫、暴力等不法行为、对他人名誉或人身权利造成损害、收受贿赂回扣等<br />
                                    √ 泄露自己或探听他人薪资或奖金福利者<br />
                                    √ 自行将公司资料以各种形式带出公司他用或私用，或自行在公司电脑上安装软件、变更系统设定、利用公司邮箱等对公司造成严重后果者<br />
                                    √ 串通客户或供应商欺骗公司、构成欺诈行为者<br />
                                    √ 被行政或司法机关依法处以行政拘留等行政处罚者<br />
                                    √ 未婚先孕等违反计划生育政策者<br />
                                    √ 工作失职或营私舞弊，给公司名誉造成影响或给公司造成经济损失者<br />
                                    √ 自行或介绍、鼓动公司员工“干私活”者<br />
                                    √ 经星言云汇内审部内审，在星言云汇内审部出具的《内审报告》中被认定为严重违反公司纪律者<br />
                                    √ 其他与上述违纪行为类似者，以及违反公司其他制度等。<br />
                                    <br />
                                    以上为部分违纪行为举例，但不包括全部行为，对于其他未经举例的违纪行为，公司亦有权进行相应处理，直至即使解除劳动合同。<br />

                                    <strong>6.奖惩申诉：</strong><br />
                                    1)	员工认为奖惩不当，可以直接向直属部门主管、公司星言云汇人力资源与行政部申诉；<br />
                                    2)	书面方式申诉，须注明姓名、业务公司、业务部门。<br />
                                </p>
                            </td>
                            <td id="td291" valign="top" style="padding: 10px 30px; line-height: 20px; display: none;">
                                <div class="title-orag">
                                    申诉渠道和程序
                                </div>
                                <br />
                                <br />
                                <p>
                                    <strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;公司高度重视并鼓励和员工充分表达自己意见和建议，并积极与相关领导沟通；同时公司要求中高层管理人员及时回应员工任何意见与建议，或组织召开各类沟通会议以广泛征询员工意见。员工可使用下列四种制度化的沟通方式，随时表达任何意见和建议。</strong>
                                </p>
                                <br />
                                <p>
                                    <strong>1. 员工意见调查</strong><br />
                                    公司不定期或针对具体事项进行员工意见调查。员工可以对公司管理层、福利待遇、工资等方面表达意见，以协助公司营造一个更好的工作环境。<br />
                                    <br />
                                    <strong>2．电话/邮件沟通</strong><br />
                                    员工可以直接通过电话、电子邮件或公司意见箱，将意见送达公司相关领导乃至星言云汇董事长，并得到及时的答复，不必担心畅所欲言的任何风险。<br />
                                    <br />
                                    <strong>3．面谈</strong><br />
                                    员工可以要求与任何上级领导进行对话，反映个人对公司或团队的意见、建议和自己认为需要公司了解的情况。这种面谈可以是保密的。员工所反映的情况，公司将分类集中处理。<br />
                                    <br />
                                    <strong>4．申诉</strong><br />
                                    员工如对团队或个人工作安排或公司制度方面有疑问，应首先与自己的直线领导交流沟通，这是解决问题的捷径；如遇困难，或员工认为不便和直线领导对话，则可以向公司业务总监、总经理、人事负责人、总裁申诉；此申诉会得到及时关注、调查和处理。<br />
                                </p>
                            </td>
                            <td id="td511" valign="top" style="padding: 10px 30px; line-height: 20px; display: none;">
                                <div class="title-orag">
                                    考勤管理制度
                                </div>
                                <br />
                                <p>
                                    <strong>1.管理原则</strong><br />
                                    公司对员工日常出勤要求及工作时间如有调整，由公司统一公告实施。<br />
                                    1)	严格考勤制度是企业管理的组成部分，它建立在员工自觉自律和良好的职业道德的基础之上。<br />
                                    2)	员工不得委托或伪造出勤记录，违规者依据本手册有关规定处理。<br />

                                    <br />
                                    <strong>2. 工作时间：</strong><br />
                                    星言云汇统一使用考勤管理系统用于日常考勤管理工具，员工需要每月按时（每个月5号之前）在系统上填报并提交考勤。如延迟填写和提交，公司有权对当月工资暂缓发放。 
                                    <br />
                                    1)	公司实行每天8小时工作制。具体工作时间为每周一至周五（法定节假日除外）上午 09:30—12:30，下午13:30—18:30。
                                    <br />
                                    2)	每天上下班时需在正门刷卡记录上下班时间；任何情况、任何人均不得代他人刷卡；代他人打卡者将按照本手册中《奖惩条例》有关规定执行。<br />
                                    3） 由于漏忘打卡、门禁卡故障或暂无门禁卡而无考勤记录者，须及时在人力资源与行政部填写“<span style="font-style: italic;">考勤情况说明表</span>”追认其当天的考勤记录，说明原因，如有必要需部门总监审批签字确认。
                                    <br />

                                    <br />
                                    <strong>3. 迟到早退：</strong><br />
                                    1)	员工不得无故迟到早退。<br />
                                    2)	9:30之后打卡视为迟到，18:30前打卡视为早退。<br />
                                    3)	根据每月出勤情况，当月迟到及早退者，公司有权选择对其进行责罚。<br />

                                    <br />
                                    <strong>4. 旷工</strong><br />
                                    1)	公司对旷工的定义是：事先未请假，请假未批准且未出勤以及擅自离开工作地点、岗位的行为；工作日正常上班迟到1小时以上或早退1小时以上，或未按公司要求时间到达指定工作岗位的行为；<br />
                                    2）旷工处理：<br />
                                    ☆ 旷工半天：缺勤超过1小时，扣除当月绩效工资的50%；<br />
                                    ☆ 旷工一天：缺勤超过3小时，扣除当月绩效工资的100%；<br />
                                    ☆ 旷工二天，属严重违反公司规章制度，公司将按照《员工手册》中的“奖惩条例”有关规定处理。<br />
                                    <br />
                                    <strong>5. 出差</strong><br />
                                    1)	如员工因公出差，需提前在OA系统上填写申请，报部门总监进行审批<br />
                                    2)	若遇特殊情况无法及时在OA系统上提交申请者，需电话、短信、微信或以电子邮件的形式通知本部门直属领导，正常上班后及时补登“OA系统考勤记录。<br />
                                    <br />
                                    <strong>6. 外出</strong><br />
                                    上班期间如因公外出办事，需在返回公司后第一时间在OA系统中补登考勤说明并经总监审批。<br />
                                    <br />
                                    <strong>7. 请假</strong><br />
                                    1)	休假、调休必须提前申请。<br />
                                    2)	需提前以电话、信息或电子邮件方式通知直接主管，获得批准后方可休假，且及时根据实际情况详细准确地在公司系统填写线上申请，报部门总监审批，否则视为旷工处理。<br />
                                    3)	年度休假、调休可以半日为计算单位。<br />

                                    <br />
                                    <strong>详细信息请参见《星言云汇休假政策》</strong><br />
                                    <br />
                                    <strong>8. 其他情况：</strong><br />
                                    遇其他情况，无法正常做考勤记录者，请尽量配合行政部做好考勤核对工作。<br />
                                    如：进驻客户公司无法正常刷卡做考勤记录者，仍须依照本细则规定做好考勤记录，人力资源与行政部可通过电话或电子邮件形式抽查日常考勤工作，其他与考勤相关的申请凭证也需及时发回公司，以免耽误正常考勤记录。
                                    <br />
                                    <br />
                                </p>
                            </td>
                            <td id="td521" valign="top" style="padding: 10px 30px; line-height: 20px; display: none;">
                                <div class="title-orag">
                                    行为规范
                                </div>
                                <br />
                                <p>
                                    <strong>1.上班着装</strong><br />
                                    1)	要求员工具备非常职业的品位着装，应最大程度重视工作场所的着装。<br />
                                    2)	员工上班时间着装应保持整洁、端庄、得体，工作区内不得穿背心、短裤、球鞋、拖鞋（包括夏季），或其他紧、透、露服（时）装。<br />
                                    3)	女职员宜化工作淡妆，不得佩带过于夸张的饰品。<br />
                                    4)	具体要求请参阅：星言云汇内网—员工服务中心—在职指引—行政Q&A。<br />
                                    <br />
                                    <strong>2. 接待客户</strong><br />
                                    1)	接待客户与其他来访人员，态度应亲切诚恳，面带微笑，不得怠慢、欺侮来访者。<br />
                                    2)	客户进门，应主动招呼，多用“请”、“谢谢”等礼貌用语，送客时则应用“再见”等用语。<br />
                                    3)	对客户应一视同仁，不论衣着面貌，亦不论其合作关系如何，均应以诚相待。<br />
                                    4)	切勿在客户面前交头接耳，窃窃私语，以免令人生疑，产生误会。<br />
                                    5)	对客户的建议，应表示谢意，并及时记录，及时反映。<br />
                                    <br />
                                    <strong>3.工作准则</strong><br />
                                    1)	员工在公司期间，不应使用公司设备或公司电子邮箱从事与工作无关的活动。<br />
                                    2)	员工须遵守公司出勤制度，具体规定请认真阅读 “星言云汇员工出勤管理制度”。<br />
                                    3)	员工在工作中应勇于负责，但遇到自己不了解、不熟悉的情况，须向同事请教或请示上级主管，不可擅自行事。<br />
                                    4)	公司鼓励员工开发新业务，与新客户建立联系。为了保证工作的顺利与实效，未经授权，总监级以下的员工不得单独代表公司与新客户进行实质性接触。<br />
                                    5)	员工须遵守职业道德，绝对保守本公司及其客户的商业机密，不得泄露或利用这些机密从事个人和与公司业务无关的活动。无论是否所服务客户领域范围，均不得泄露，离职后此条款要求同样适用。<br />
                                    6)	在工作中，我们应在星言云汇内部共享资源信息，相互提供客户在公关、会展、广告、市场营销、创意、设计等方面的业务需求信息，相互介绍客户关系，以利于星言云汇的整体发展。<br />
                                    7)	我们为客户提供专业服务、最大程度满足客户需求，但我们与客户之间是企业间平等合作的关系。对于客户的合理要求，我们理应满足；对于客户不合理、不适当、不正确的要求，我们应尽力说服，提出我们的积极建议，体现我们作为专业公司的素质水平，切不可一味无原则地盲从，以至最终既损害客户根本利益，也损害星言云汇的专业形象。<br />
                                    8)	公司使用星言云汇统一签约的图文复印、快递、摄影摄像、音像制作等第三方供应商，并按月凭作业单底联与供应商结算。<br />
                                    <br />
                                    <strong>4.工作纪律</strong><br />
                                    1)	员工对工作应始终踏实严谨，不得有欺瞒、虚假、谋私利等行为。<br />
                                    2)	员工每人获发公司门卡（包括门卡链）。如有遗失，应及时向行政部门报失、进行补办并缴纳补办费用200元。各类办公家具钥匙丢失或损坏需缴纳补办费用50元，以上物品离职时需完好交还公司行政部门。<br />
                                    3)	员工上班时间不得从事与工作无关的活动，包括闲聊、阅读浏览无关书刊网站、玩游戏、逛商场等；不得接待与公司业务无关人员，并将其带入办公区。<br />
                                    4)	不得在工位区域就餐。<br />
                                    5)	来访客人需在前台进行登记并由公司员工陪同，方可进入办公区。有重要客人来访，须事先通知前台及安排会议室，作礼宾接待。<br />
                                    6)	不得在工作场所吵架斗殴，影响公司正常工作秩序。<br />
                                    7)	员工有责任创造保持一个良好、清洁、安静的办公环境，随时清理个人办公桌及其周围的物品，不得乱堆乱放杂物。不得在办公区吸烟及加热自带食品。<br />
                                    8)	员工不得议论自己或他人的工资、奖金和其他福利情况。违者将受到警告处分和经济处罚或予以即时除名并不提供任何经济补偿。<br />
                                    9)	公司提倡员工关注并了解公司发布的各类讯息。除非获得公司总裁授权，任何员工在任何情况下不应擅自向全公司所有员工发送任何邮件或其他讯息。<br />
                                    10)	未经公司批准，员工不得对外就公司政策及业务等事项发表口头或书面的公开评论与公开声明。<br />
                                    11)	员工有义务对其在职期间获得的或为了完成工作而被告知的任何或全部信息与数据进行保密，包括个人电脑内存放的公司文件。同时，公司有权对以下网络行为进行管理：包括业务往来邮件、业务及时通信息（微信,QQ等）、浏览网站及网页信息、业务下载信息、端口流量监控等。若公司在对前述渠道或载体进行管理的过程中发现员工未适当履行维护公司信息安全义务的，公司对责任人有权按照本手册有关规定处理。如情节严重，公司有权依法追究其法律责任。<br />
                                    12)	员工有责任维护公司各类办公设备及设施的正确使用和保管。<br />
                                    13)	员工每天下班需关闭自己电脑及有关设备，整理和保管好自己的文件。<br />
                                    14)	员工出入公司大门时，必须保证随手关门。未关门者，直接扣罚500元，如若造成财物损失的，则需按等价赔偿。<br />
                                    15)	最后一名员工离开办公室时有责任关闭所有照明灯，锁定办公室大门。如遇意外（无钥匙、门锁故障等），可求助于当地大厦物业部或公司行政部。<br />
                                    16)	最后一名离开办公室的员工在任何情况下均不可不关灯、不锁大门离开。违者将视具体情节严重程度，受到通报批评、警告处分和经济处罚或即时除名并不提供任何经济补偿。<br />
                                    17)	员工个人手机号码列入公司通讯录(公司视具体需要为员工报销部分通讯费用)。员工应保证其手机或其他联络方式的畅通，尤其在客户服务项目进行阶段或节假日，不可因手机未充电、关机、丢失等理由导致不能通话而影响工作。<br />
                                    18)	按公司规定领取或借用星言云汇的笔记本电脑、移动硬盘、移动上网网卡、激光笔等设备的同事，应严格遵照相关规定做妥善保管。因个人任何原因造成的损坏或遗失，当事人需负责一切后果，并承担设备的修理、重置等相关赔偿费用。<br />
                                    19)	员工不得利用公司办公设备从事个人或与公司业务无关的活动。员工应本着爱惜节俭的原则使用办公设备。新员工到岗后，其上级主管应负责向其介绍所有办公设备的正确使用和有关规定。<br />
                                    20)	公司办公文具采取统一采购、分级管理方式。员工领取个人办公、业务活动所需文具，需事先填写“办公用品领取申请单”，经公司领导签字后在行政部门登记领取。<br />
                                    21)	公司行政部门负责统一印制各部门、分公司员工名片。新员工印制名片时须填写申请单，经公司、部门领导签字后交行政部门印制。员工加印名片需提供底样；如底样职位变更，需所属公司、部门领导签字确认。<br />
                                    22)	员工必须保守公司经营管理各方面的机密。<br />
                                    23)	员工必须妥善保管公司机密文件及内部资料。机密文件和资料不得擅自复印，未经特许，不得带出公司。<br />
                                    24)	机密文件和资料无需保留时, 必须用碎纸机粉碎销毁。<br />
                                    25)	员工未经公司授权或批准，不准对外提供标有密级的公司文件以及如下信息：价格体系、渠道销售政策、财务状况、技术情况、人力管理（含通讯录）、法律事务、领导决策文件。<br />
                                    26)	严禁任何人以任何理由带领公司外人员进入公司财务室等地。<br />
                                    27)	其他保密信息请遵照《保密协议》执行。<br />
                                    <br />
                                </p>
                            </td>
                            <td id="td531" valign="top" style="padding: 10px 30px; line-height: 20px; display: none;">
                                <div class="title-orag">
                                    绿色办公
                                </div>
                                <br />
                                <p>
                                    <strong>一、节约用纸</strong><br />
                                    1)	打印文件应注意节约纸张，除打印提交给客户的正本文件之外，打印其余文件（草稿、自用、内部传阅等）一律使用作废文件用纸的背面。员工应妥善保管好领取的耐用文具，并在离职时交回，非耗用品的遗失和损坏须予以赔偿。<br />
                                    2)	打印区域和办公区域均设立了“废纸”、“二次用纸”的回收箱，员工需要将作废的纸张分别放入相应的回收箱中。<br />
                                    <br />
                                    <br />
                                    <strong>二、节约用水</strong><br />
                                    随手关上水龙头，遇到没关紧的水龙头马上关紧。<br />
                                    <br />
                                    <strong>三、电池回收</strong><br />
                                    请将废旧不用的电池放入专用的“电池回收箱”内。<br />
                                    <br />
                                    <strong>四、电灯与电器设备</strong><br />
                                    1)	在午休、加班时，关闭部分电灯，不用的用电设备也要及时关闭，或设置成节电状态；下班后，关闭办公室所有的灯、空调等电器设备。<br />
                                    2)	会议室使用完毕后，请及时关闭会议室的电灯。<br />
                                    3)	天气晴朗的时候，使用自然采光。<br />
                                    <br />
                                    <strong>五、通风设备及空调</strong><br />
                                    1)	会议室使用完毕后，请及时关闭会议室的空调。<br />
                                    2)	控制室内空调温度，适宜的室温一般是26℃，调整空调温度的设定应定于26℃。<br />
                                    3)	在天气好的时候尽量不开空调，引入自然风。<br />
                                    4)	使用空调时，随时关闭门窗，窗帘，可以有效降低夏季室内日照的强度。<br />
                                    <br />
                                    <strong>六、有效减少废弃物</strong><br />
                                    1)	提倡使用电子邮件传发信息，或者以传阅文件的形式，减少复印用纸张。<br />
                                    2)	提倡减少一次性用品的使用，如杯子、碗碟等，多使用瓷器或塑料用品。<br />
                                    3)	提倡减少纸巾的使用量，多用抹布、毛巾。<br />
                                    4)	鼓励使用自备的餐具，减少丢弃一次性餐具。<br />
                                    5)	鼓励在办公室内使用电器时，尽量使用交流电，减少废电池量。<br />
                                    <br />
                                </p>
                            </td>
                            <td id="td541" valign="top" style="padding: 10px 30px; line-height: 20px; display: none;">
                                <div class="title-orag">
                                    安全守则
                                </div>
                                <br />
                                <p>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;为确保公司人员、财产的安全，严格贯彻、执行“预防为主，消防结合”的消防安全管理方针，实行“谁主管，谁负责”的工作原则，依据《中华人民共和国治安管理处罚法》、《中华人民共和国消防法》、《北京市防火安全责任制的规定》、《北京市消防条例》等相关的法律、法规内容，将消防安全工作纳入公司的经营管理之中，实行同布置、同落实、同检查、同总结、同评比，使安全管理工作经常化、正常化、制度化。<br />
                                    <br />
                                    1)	公司全体员工应严格执行、遵守国家现行的相关法律、法规。<br />
                                    2)	公司严禁存放易燃、易爆等化学物品及使用大功率电器设备。<br />
                                    3)	公司内严禁吸烟。<br />
                                    4)	员工每日下班后，应检查、确认电器设备、设施已切断电源，关好门窗，方可离开。<br />
                                    5)	公司内严禁存放个人贵重物品，如发现遗失，个人承担全部责任。<br />
                                    6)	公司员工应掌握消防器材扑救一般性火灾的使用方法、会疏散逃生。<br />
                                    7)	发现火情，要及时报警并通知公司行政部，并积极参加扑救工作，保护好火灾现场，配合相关部门进行调查工作。<br />
                                    8)	公司员工有义务及责任及时发现、消除各类消防安全隐患。<br />
                                    9)	公司员工如因玩忽职守、违反国家法律、法规造成公司财产损失的，须承担相应的法律责任。<br />
                                    10)	为防止外来人员随意出入公司，员工不能使用自己的门禁卡为外来陌生人员随意开门，应指引其从正门前台进入公司<br />
                                    11)	办公区属于公共区域，请大家保管好自己的物品，不要将贵重物品随意放置在工位上，包括饰品、钱包、手机、数码设备等。<br />
                                    12) 安保规范详见《办公室安保管理》<br />
                                </p>
                            </td>
                            <td id="td551" valign="top" style="padding: 10px 30px; line-height: 20px; display: none;">
                                <div class="title-orag">
                                    图书管理条例
                                </div>
                                <br />
                                <p>
                                    1)	图书存放地点：四层图书区<br />
                                    2)	图书借阅地点：一层前台<br />
                                    3)	如若需要将图书带出办公区，读者须在规定时间借阅图书资料：<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp; 1、图书借阅时间：每周五  10：00-18：00
                                    <br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;2、图书归还时间：每周三  10：00-18：00<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;3、午休时间可在四层图书区阅读，不用办理借阅手续。<br />

                                    4)	图书阅览后须放回原处，未经正规的借阅手续不得自行带走图书。<br />

                                    5)	读者对所借图书要及时检查，如发现污损、撕开、严重破损等情况要主动说明，以明责任，否则由本人负责。<br />

                                    6)	读者可根据需要到管理员处查询图书的借阅情况，再按照规定在图书管理员处办理借阅手续。<br />

                                    7)	请大家在规定借阅、归还时间办理相关手续，在非借阅、归还时间图书馆不办理上述手续。
                                    <br />

                                    8)	每人每次限借2册，借期最长20天，续借期5天。
                                    <br />

                                    9)	借阅人需按期归还图书，如未办理续借手续又未按时归还的，管理员有权对其进行处罚，罚款为：超期1天罚款1元。<br />

                                    10)	图书馆设施、图书均是公司财产，请大家爱护图书、保护公司公物，书刊不得卷折、撕页、涂抹、污损，图书馆内家具不得刻意损坏否则一经核实，将视其情节轻重予以罚款赔偿或行政处分。<br />

                                    11)	遗失图书、污损图书均应按原价赔偿图书，如遗失、损毁工具书、珍贵文献按书价5倍赔偿。多卷书刊损坏、遗失其中一册的以成套价格赔偿。对有意破坏书刊、盗窃书刊或谎报遗失者，除照章赔偿外，应视其情节轻重，上报有关部门给予适当处分或罚款。造成污损或遗失书刊的，在未赔偿前停止借阅。<br />

                                    12)	 本办法未尽事宜将根据需要协商解决。<br />
                                </p>
                            </td>
                            <td id="td911" valign="top" style="padding: 10px 30px; line-height: 20px; display: none;">
                                <div class="title-orag">
                                    内审条例
                                </div>
                                <br />
                                <p>
                                    <strong>第一．总则</strong><br />
                                    星言云汇内审条例（本条例）是为保障集团各类规章制度的有效落实，维护集团下各公司的健康运行，杜绝违法、违纪行为的发生所制定。<br />
                                    本条例与集团各个职能部门的管理政策互为补充，协同管理，为企业实现有效的内部控制和运营管理提供保障基础。<br />
                                    本条例适用于星言云汇总部、各分公司和子公司，由集团管理层负责统一制定、管理、颁布与实施。为了更好适应集团的发展需要，本条例将不定期审订。
                                    <br />
                                    <br />
                                    <strong>第二．审计范围</strong><br />
                                    集团下各所属业务团队、分公司、子公司，以及行政、人事、采购、财务等运营部门，都属于审计对象。<br />
                                    <br />
                                    <strong>第三．审计依据</strong><br />
                                    内审部进行审计工作时的依据包括：<br />
                                    1.	国家颁布的法律、法规和政策； 
                                    <br />
                                    2.	公司颁布的各项规章制度，如《员工手册》中的〈人事制度〉、〈行政制度〉、〈财务制度〉、〈采购制度〉和〈网络制度〉等；<br />
                                    3.	业务作业单、预算及合同等；<br />
                                    4.	团队计划及业务指标等。<br />

                                    <br />
                                    <strong>第四．审计内容</strong><br />
                                    1、行政工作：<br />
                                    （1）	是否保持舒适、美观的办公环境；<br />
                                    （2）	是否认真执行《考勤管理制度》；<br />
                                    （3）	是否合理支配行政费用等。<br />

                                    2、采购工作<br />
                                    （1）	是否认真执行《采购管理办法》；<br />
                                    （2）	协议供应商的甄选程序是否规范；<br />
                                    （3）	采购过程中是否存在行贿、受贿行为等；<br />
                                    <br />
                                    3、财务工作<br />
                                    （1）	是否认真遵守《企业会计制度》；<br />
                                    （2）	是否认真执行《费用申请、报销与借款管理条例》；<br />
                                    （3）	是否认真执行《应收款管理条例》等。<br />
                                    <br />
                                    4、业务活动<br />
                                    （1）	是否认真执行集团有关现金流管理的相关政策，比如，与客户方合同中的付款方式、广告投放项目中对垫付费用的规定等；<br />
                                    （2）	是否认真执行集团有关成本控制管理的相关政策，比如，对超预算的、项目号已关闭的费用的报销规定等；<br />
                                    （3）	是否认真遵守集团及各部门制订的其它相关管理规定。<br />
                                    <br />
                                    <strong>第五．审计方式</strong><br />
                                    1.	内审部按计划，对各团队、各部门进行不定期的审计；<br />
                                    2.	按照集团的要求，对某一团队、某一部门进行针对性的审计；<br />
                                    3.	经某一团队、某一部门领导申请，报请集团批准后，对该团队或该部门进行审计；<br />
                                    4.	内审部特别申请，并报请集团批准后，对某一团队、某一部门或某一个人进行审计<br />
                                    <br />
                                    <strong>第六．处罚规定</strong><br />
                                    1.	违纪金额在一万元以上的经济问题，且构成违犯国家法律、法规的，移交国家司法机关依法处理；<br />
                                    2.	一般违纪行为，依《员工手册》中〈奖惩条例〉的具体规定执行；<br />
                                    3.	审计报告将提交集团，作为员工评估的参考。<br />

                                    <br />
                                    <strong>第七．特别说明</strong><br />
                                    1.	内审部欢迎大家对违纪行为进行举报，举报电话：010-8509 5766。内审部对举报人予以保密，并对检举重大违纪行为的有功人员给予奖励。<br />
                                    2.	内审部在开始审计前，将提前通知被审计的团队、部门或个人。但在特殊情况下的审计，不作提前通知。<br />

                                </p>
                            </td>
                            <td id="td611" valign="top" style="padding: 10px 30px; line-height: 20px; display: none;">
                                <div class="title-orag">
                                    费用申请、报销与借款管理条例
                                </div>
                                <br />
                                <p>
                                    为了规范星言云汇及各所属专业团队员工的与经营活动相关的费用报销及借款审批流程，规范星言云汇的财务管理，特制定本条例。<br />
                                </p>
                                <p>
                                    <strong>一．原则</strong><br />
                                    财务报销、借款和审批应遵循以下原则：<br />
                                    1)	所有费用必须与公司的管理或业务相关；<br />
                                    2)	各项费用做到合理、透明、准确，并符合国家的各项相关规定；<br />
                                    3)	费用的报销应在该费用发生后1个月内完成申请审批；<br />
                                    4)	2万（含）以下现金借款申请须在领取现金前48小时（工作时间）递交到财务部，2万-4万现金借款申请须在领取现金前72小时（工作时间）递交到财务部，以此类推；<br />
                                    5)	费用未得到批准前不能进行和采购相关的活动；<br />
                                    6)	费用申请、报销和借款申请均须按《审批授权规定》审批，各级审批人对申请内容的真实性、准确性和有效性负责。<br />
                                    <br />
                                    <strong>二．费用报销管理</strong><br />
                                    <strong>1、报销日期</strong><br />
                                    报销申请随时都可以提交，每月1日及14号为报销提交截止日，具体时间以系统首页提示为准。财务部仅对当次审批通过的报销单进行处理。截止日后审批通过的报销单，系统将自动归为下次报销。<br />
                                    <strong>2、报销标准</strong><br />
                                    1)	业务招待费（包括团队活动）：<br />
                                    星言云汇不再只按统一标准做硬性规定，由各位自主按需把控，MD全权审核即可。<br />
                                    2)	因业务原因产生的出租车费、员工私车的停车费、汽油费可以报销：<br />
                                    停车费报销时需注明停车场所及活动内容，出租车费及汽油费报销时需写明起止地点及工作内容，汽油费报销还需写明里程数，报销标准为1.50元/公里。<br />
                                    在公司办公楼地下车库的租金不予报销。
                                    <br />
                                    3)	差旅费<br />
                                    住宿：<br />
                                    员工应尽量入住星言云汇指定的协议酒店；如出差地无协议酒店，须按如下房间标准入住酒店，并在报销时必须提供酒店水单。<br />
                                </p>
                                <table border="1" cellpadding="0" cellspacing="0">
                                    <tr style="border-width: thin; border: 0 1 0 0;" align="center">
                                        <td style="width: 30%;">住宿标准
                                        </td>
                                        <td style="width: 30%;">一类城市<br />
                                            （北京/上海/广州/深圳)
                                        </td>
                                        <td style="width: 30%;">其他城市
                                        </td>
                                    </tr>
                                    <tr style="border-width: thin; border: 0 1 0 0;" align="center">
                                        <td style="width: 30%;">全体员工
                                        </td>
                                        <td style="width: 30%;">标准间≤600元
                                        </td>
                                        <td style="width: 30%;">标准间≤400元
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <p>
                                    餐费（须员工自行报销，不得代报销）：<br />
                                    按出差天数每日餐费实报实销，标准为：一类城市上限为100元，其他城市上限为80元。<br />
                                    出发及回程可报销餐费，抵达或离开的时间、标准如下：<br />
                                    <br />
                                    - 早餐：上午10:00前抵达或离开<br />
                                    - 午餐：下午3:00前抵达或离开<br />
                                    - 晚餐：下午6:00前抵达或离开<br />
                                    <br />
                                </p>
                                <table border="1" cellpadding="0" cellspacing="0">
                                    <tr style="border-width: thin; border: 0 1 0 0;" align="center">
                                        <td style="width: 30%;">一类城市<br />
                                            （北京/上海/广州/深圳)
                                        </td>
                                        <td style="width: 30%;">其他城市
                                        </td>
                                    </tr>
                                    <tr style="border-width: thin; border: 0 1 0 0;" align="center">
                                        <td style="width: 30%;">早餐≤20元<br />
                                            中、晚餐≤40元<br />
                                            一日三餐合计≤100元
                                        </td>
                                        <td style="width: 30%;">早餐≤10元<br />
                                            中、晚餐≤35元<br />
                                            一日三餐合计≤80元
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <p>
                                    洗衣费：<br />
                                    连续出差时间≥3天，可凭相关发票报销；一般情况下，不能超过入住酒店费用的5%。<br />
                                    <br />
                                    机票：<br />
                                    去程只能通过星言云汇指定的票务代理商由公司统一购票；回程机票、机票改签费用不在此列。<br />
                                    <br />
                                    火车票：<br />
                                    凡出差目的地高铁车程在10小时以内，员工需乘坐火车（高铁二等座）出行。如遇特殊情况需选择其他出行方式，须经团队总经理审批。<br />
                                    <br />
                                    航空保险：<br />
                                    因所有员工均享有星言云汇提供的意外保险，航空保险不能再作报销。兼职、实习生出差不在此列。<br />
                                    <br />
                                    <strong>3. 报销流程</strong><br />
                                    1)	报销审批办法请参照《审批授权规定》；报销提交后，系统会按《审批授权规定》自动显示审批人，若团队有特别要求，需由申请人自行添加预审人等附加审核人；<br />
                                    2)	支持部门员工涉及项目的报销，在支持部门本部门审批外，还需经过相关项目负责人、项目总监认定；<br />
                                    3)	各级审批人须对各项支出进行认真核查。如有费用不明或填写金额与发票金额不符等情况，须与申请人进行核实与更正。<br />
                                    4)	打印审批完成后的报销单，按填写的顺序，将发票整理好，平贴在报销单后面，注意粘贴时露出发票金额。申请人将完成审批的报销单据放入各楼层报销单收取处，由公司财务统一收取；<br />
                                    5)	各公司出纳根据员工提交的发票，审查支出是否符合本条例规定，发票是否规范，金额是否准确。并将报销中的问题发邮件与申请人，同时限定期限解决，逾期未能解决的当月不予报销，发票退回申请人。<br />
                                    6)	费用如有变化，需由报销申请人重新开始审批流程；<br />
                                    7)	财务审核完成后，财务人员将报销款存入报销申请人的银行卡内，申请人可在系统中查询报销单状态。报销申请人须仔细核实报销款项与数额，任何问题与疑问须在通知发出后3日内到财务部核对；<br />
                                    8)	报销完成后，星言云汇审计部将对报销内容进行审计；核查出现问题的，将对报销申请人和审批负责人按本条例中第五条“特别规定”中的规定处理；<br />
                                    9)	填写报销表的注意事项：<br />
                                    费用所属组别指费用发生项目号所属的组别，并非报销人所属的组别。<br />
                                    费用描述一栏要求明细填写。涉及到类别和数量时，一定要注明。<br />
                                    如： 车费：出发地—目的地，事由;<br />
                                    餐费：午（晚）餐，客户，参与人姓名等;<br />
                                    按单笔费用单行填写。如，几张车票不能放在一起，须一张车票填一行，日期是该笔费用发生的日期，而非报销日期。<br />
                                    报销表所填费用均为有合格发票的费用，无发票的费用不予报销；<br />
                                    星言云汇协议供应商业务人员负责提交，再由星言云汇采购部负责汇总费用申请，按月整理、审核，当月申请上月发生的费用，每月15日提交上月发生的费用报销，下月中旬由财务部安排付款；<br />
                                    非星言云汇固定供应商的付款申请由业务人员负责提交；<br />
                                    进入一个项目的费用需要有独立的发票，不能将一张发票的费用分别填到几个项目中，如手机费用等；<br />
                                    <br />
                                    <strong>三．借款申请管理（包括现金和支票／电汇）</strong><br />
                                    <br />
                                    <strong>1．现金借款管理</strong><br />
                                    1)	借款申请的流程同报销审批流程。申请人须为已转正正式员工，试用期员工不得申请现金借款；<br />
                                    2)	借款需在系统中提交“现金借款单”；需详细填写现金借款单上每一项内容，包括申请日期、领用日期、组别、项目号，需要明细并实际填写费用项目和金额，不能随意加大借款金额。<br />
                                    所有借款只能用于借款人在借款单上描述的用途，不允许款项在不同项目间借用；<br />
                                    3)	2万（含）以下现金借款申请须在领取现金前48小时（工作时间）递交到财务部，2万-4万现金借款申请须在领取现金前72小时（工作时间）递交到财务部，以此类推，以备财务部准备现金，未按时提交的借款申请由申请人先行垫付；<br />
                                    4)	借款单上的借款人将对其所借金额的使用担负完全责任，不得拆解给其他人。如借款人不能亲自领取借款，需在借款单备注中写明领取人，此笔借款仍由借款人负责归还或冲销；<br />
                                    5)	借款人应在款项发生后3个工作日内将余款退回财务部；<br />
                                    6)	借款报销必须在当月完成，不需等到报销日，即可随时到财务部核销借款。1个月以上未作核销的借款将从借款人当月工资中扣除。<br />
                                    <br />
                                    <strong>2．机动备用金借款</strong><br />
                                    1)	无明确用途的借款为机动备用金借款<br />
                                    2)	SAE(含)以上员工可以申请人民币10,000元(含)以下借款；总监(含)以上员工可以申请人民币10,000元以上至50,000元(含)以下借款。业务团队总经理可以申请50,000元以上借款<br />
                                    3)	机动备用金借款与报销除常规审批流程外，均须星言云汇总裁审核；<br />
                                    4)	该借款报销须与日常报销分开，并且须在项目结束后一周内冲销。<br />
                                    <br />
                                    <strong>3．临时支票／电汇付款申请管理</strong><br />
                                    临时支票／电汇付款申请流程同报销审批，需填写“支票/电汇付款申请单”。同时需遵守《采购申请流程管理办法》<br />
                                    申请单须比实际付款日提前3个工作日递交到财务部。<br />
                                    <br />
                                    <strong>四．其他费用管理</strong><br />
                                    <strong>1．第三方费用：</strong><br />
                                    涉及到主持人、摄像、摄影、模特等费用，由代理商提供发票，依照采购程序申请费用。<br />
                                    <br />
                                    <strong>2．关于现金收入：</strong><br />
                                    如客户直接支付给业务人员现金，须由业务人员将现金交与财务部的出纳，并在“收据”上签字，同时，收据上需要会计签字。行政部出售旧报刊杂志及废旧物品的现金收入，依此程序进行入账。<br />
                                    <br />
                                    <strong>3．如需向客户出示发票复印件：</strong><br />
                                    请一律先行准备好复印件，然后再提交给财务部发票原件。特殊情况下需要到财务部查找的，须由客户总监和团队总经理批准。<br />
                                    <br />
                                    <strong>五．特别规定</strong><br />
                                    1)	每月最后两个工作日，因公司结账进度要求，财务部不对外支付款项；<br />
                                    2)	费用申请须按《审批授权额度》审批。如发生任何经济问题，对存在问题的财务申请进行申请人，将被扣除1个月工资；批准的第一级审批人将被扣除1个月工资；批准的第二级审批人将被扣除半个月工资；<br />
                                    3)	付款申请须附有效发票。如有第三方收款后才能提供发票的情况，申请人须在款项付出后15日内将发票交给财务部，逾期未交回者，按借款未核销处理。<br />
                                    <br />
                                </p>
                            </td>
                            <td id="td621" valign="top" style="padding: 10px 30px; line-height: 20px; display: none;">
                                <div class="title-orag">
                                    商务信用卡管理条例
                                </div>
                                <br />
                                <p>
                                    <strong>公司卡是为了减轻员工执行业务时的垫款压力，提高星言云汇资金利用率而提供给符合条件员工使用的信用卡，请特别注意，此卡不能用于个人消费。</strong><br />
                                </p>
                                <p>
                                    1)	公司卡为公司统一申请并经银行核准的个人责权信用卡，公司仅支付已经审批并为业务发生的费用，不用于个人消费；如发生个人费用，则由持卡人本人承担；<br />
                                    2)	公司卡未开通预借现金功能，不能在自动提款机上提取现金；<br />
                                    3)	公司卡的使用申请等同于以前的支票电汇申请，使用前需要得到相应的费用审批方能使用，特别是和业务采购相关的费用；<br />
                                    4)	持卡人可以为各公司项目支付费用，但要注意发票抬头与项目号所属公司保持一致。报销时将不同公司的费用按公司汇总，分别报销，不能将不同公司的费用报销在同一张报销单上；<br />
                                    5)	持卡人报销刷卡消费报销要求等同于现金、支票电汇报销，但要与现金、支票电汇报销分开，同时要提供刷卡单；<br />
                                    6)	公司卡帐单日为每月10日，每月第二次报销提交截止日前持卡人务必将本月帐单上所有费用及时报销；<br />
                                    7)	每月28日前帐单上已报销金额将由公司统一偿付；如发生未报销、或者产生个人消费等费用，公司一律不予偿付，应由持卡人自行及时偿付；<br />
                                    8)	若因持卡人未及时偿付未报销、或者个人消费部分费用而产生的滞纳金、利息等一律由持卡人个人承担；<br />
                                    9)	若因业务需要临时调高信用额度，业务人员可自行向商务卡中心提出申请；若因业务需要长期调高信用额度，需向业务主管提出申请并提交财务部，由财务部向商务卡中心提出申请；<br />
                                    10)	持卡人有责任保管好公司卡。若有遗失，务必在第一时间通知银行挂失并通知财务部公司卡联系人；<br />
                                    11)	持卡人在离职前两周必须将公司卡按本手册离职程序的规定交回财务部，同时必须在办理离职手续前将离职之前及当月所有刷卡消费报销完毕。<br />
                                    <br />
                                </p>
                            </td>
                            <td id="td631" valign="top" style="padding: 10px 30px; line-height: 20px; display: none;">
                                <div class="title-orag">
                                    应收款项管理条例
                                </div>
                                <br />
                                <strong>为确保公司的正常商业运营，提高资金流动率，特制定本应收款管理条例。</strong><br />
                                1)	应收款的直接责任人为项目负责人或项目经理，二级责任人为项目总监，三级责任人为所属项目的团队总经理；<br />
                                2)	财务部参照合同中约定的付款条款，及时向客户开发票，以开发票或确认收入孰先原则，计算应收账款账龄；<br />
                                3)	项目的正常回款时间为开出发票或确认收入后的90天内（日历日）；<br />
                                4)	财务部每周一向星言总经理和副总发送星言各团队的应收款明细，对于帐龄分别超过180天、365天和730天（日历日）的应收款，财务部会在明细上加回款情况备注，并在邮件正文中对长账龄的应收账款或需要重点关注的问题做出特别提示；宣亚系统中非星言公司的应收账款，各团队小组长可随时从系统中导出查看最新应收账款情况；<br />
                                5)	未能在90天内（日历日，截止当月月底）回款的应收账款，所属项目的各级责任人将被扣除该月税前工资总额的5%，次月仍未回款的，扣除税前工资总额的10%，第三月为15%，即每月递增5%，逾期超过六个月的不做递增；<br />
                                6)	各级责任人税前工资总额的扣除及至款项收回当月停止扣除，所有扣除的工资将不再返还；<br />
                                7)	本条例第5条所述的内容，指单一项目，单一项目对应团队三级责任人，并依此扣除三级责任人工资；如出现多个逾期未回款项目时，将按项目数量，累加扣除金额；<br />
                                8) 特殊情况，团队总经理报请董事长审定。<br />
                                <br />
                            </td>
                            <td id="td641" valign="top" style="padding: 10px 30px; line-height: 20px; display: none;">
                                <div class="title-orag">
                                    发票管理条例
                                </div>
                                <br />
                                <p>
                                    <strong>为确保集团的正常商业运营，降低财务风险，特制定本应收款管理条例。</strong><br />
                                    <strong>1.责任人</strong>
                                    1)	发票直接责任人负责发票的信息核对、保管、递送和跟踪，确保发票相关事务的顺畅和安全。<br />
                                    2)	发票直接责任人由业务人员担任，根据发票金额的大小，对级别的要求分别对应如下：<br />
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
                                    3）直接责任人的直接汇报上级为二级责任人，承担发票管理的连带责任；<br /><br />
                                    <strong>2. 离职交接</strong><br />
                                    责任人离职时，发票的记录和管理责任必须交接给同级或更高级别的同事。接手的同事将成为发票新的责任人。交接发票新责任人的确认邮件需转给财务部留底。<br />
                                    <strong>3. 发票签收</strong><br />
                                    每张开出的发票都只能交给相应的直接责任人，直接责任人随后提供发票签收单。具体操作步骤如下：
                                            <br />
                                    1)	项目需要开发票时，项目负责人发邮件给应收财务（若项目负责人级别低于发票直接责任人，邮件需抄送相应直接责任人及责任人的直接汇报上级）。应收财务核实发票登记表信息无误后开出发票，发票直接责任人领取发票时需要在存根联背面签字。同时，应收财务打印发票签收单给直接责任人，并回复邮件告知所开发票相关信息；<br />
                                    2)	应收财务督促直接责任人在开出发票1周内提交客户签字的发票签收单（原件、传真件、扫描件均可）并邮件告知相关同事；如因客户的原因不能提供签字签收单的，请提供清晰的证据表明该发票已被客户接收（如照片、录音、邮件等）；<br />
                                    3)	应收财务按照公司、客户及项目号的依次顺序留存发票签收单，待收到回款邮件后在此单上做标记，回款半年后可销毁。<br />
                                    <br />
                                     <strong>4. 损失对应义务</strong><br />
                                    除因不可抗拒力外，如因发票信息错误、递送延误、丢失等问题照成公司损失的，发票直接责任人和二级责任人将承担相应的责任，并根据损失的大小受到包括但不限于警告、扣工资、解聘等相应的处罚。<br />
                                    <br />
                                    <strong>5. 其他</strong><br />
                                    特殊情况，团队总经理报请集团总裁审定。<br />
                                    <br />
                                    <strong>6. 附件-发票签收单</strong><br />
                                </p>
                                <br />
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
                                <br />
                            </td>
                            <td id="td711" valign="top" style="padding: 10px 30px; line-height: 20px; display: none;">
                                <div class="title-orag">
                                    采购管理办法
                                </div>
                                <br />
                                <p>
                                    <strong>第一．总则</strong><br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;星言云汇（“集团”）采购管理政策（“采购政策”）规定了集团公司进行产品与服务等第三方的采购行为规范与操作标准，同时明确了采购部的工作范围与基本职责。“采购政策”对采购活动进行监督,服务于企业运营管理，保障管理质量,
                                支持业务发展。<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;“采购政策”与集团公司各个职能部门的管理政策互为补充，协同管理，为企业实现有效内部控制与管理的健康发展提供了保障基础。<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;“采购政策” 适用于星言云汇各地分公司，由集团采购部负责统一制定、管理、颁布与实施。为了更好适应集团公司的发展需要，“采购政策”将定期审阅并及时调整。<br />
                                    <br />
                                    <strong>第二．采购申请流程管理办法</strong><br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;采购申请流程管理办法作为星言云汇“采购政策”的一重要组成部分与工作实施细则，详细规定了采购申请过程中的标准流程与操作规范，目的是提高政策监管效力，确保流程依顺度，有效控制违规采购行为，降低由此给公司带来潜在的商业风险。<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;采购部与各事业部门协作配合，严格按此管理办法的规定进行采购申请工作。<br />
                                    <br />
                                    <strong>第三．一般术语与定义</strong><br />
                                </p>
                                <table border="1" cellpadding="0" cellspacing="0">
                                    <tr style="border-width: thin; border: 0 1 0 0;" align="center">
                                        <td style="width: 20%;">用户
                                        </td>
                                        <td style="width: 80%;">又称使用者，是采购物资的需求者与最终使用者。（End-User）
                                        </td>
                                    </tr>
                                    <tr style="border-width: thin; border: 0 1 0 0;" align="center">
                                        <td style="width: 20%;">申请人
                                        </td>
                                        <td style="width: 80%;">采购申请的提交人。即是用户本身,有时代表用户提交申请。(Requestor)
                                        </td>
                                    </tr>
                                    <tr style="border-width: thin; border: 0 1 0 0;" align="center">
                                        <td style="width: 20%;">采购申请
                                        </td>
                                        <td style="width: 80%;">按业务需求，提出的第三方物资购买请求（注：媒介中心相关采购除外）
                                        </td>
                                    </tr>
                                    <tr style="border-width: thin; border: 0 1 0 0;" align="center">
                                        <td style="width: 20%;">采购申请单
                                        </td>
                                        <td style="width: 80%;">集团标准的第三方物资采购申请的模板文档，申请人通过手工填写或电子形式提交
                                        </td>
                                    </tr>
                                    <tr style="border-width: thin; border: 0 1 0 0;" align="center">
                                        <td style="width: 20%;">业务审批
                                        </td>
                                        <td style="width: 80%;">根据项目预算情况与实际业务需要，业务部门对采购申请进行的审核
                                        </td>
                                    </tr>
                                    <tr style="border-width: thin; border: 0 1 0 0;" align="center">
                                        <td style="width: 20%;">业务审批人
                                        </td>
                                        <td style="width: 80%;">按集团规定，具有审批权限的审批责任人。
                                        </td>
                                    </tr>
                                    <tr style="border-width: thin; border: 0 1 0 0;" align="center">
                                        <td style="width: 20%;">采购审批
                                        </td>
                                        <td style="width: 80%;">按采购审批流程管理办法中的规定，采购部对采购申请进行的审核。
                                        </td>
                                    </tr>
                                    <tr style="border-width: thin; border: 0 1 0 0;" align="center">
                                        <td style="width: 20%;">采购项目负责人
                                        </td>
                                        <td style="width: 80%;">按物料分工，采购部内部指定的采购审批负责人。采购总监为采购部最终审批人。
                                        </td>
                                    </tr>
                                    <tr style="border-width: thin; border: 0 1 0 0;" align="center">
                                        <td style="width: 20%;">协议供应商
                                        </td>
                                        <td style="width: 80%;">与星言云汇签订框架供应协议并建立长期合作关系的供应商。作为伙伴型供应资源，协议供应商承诺提供有竞争力的价格与优质的产品与服务，并严格按集团公司的标准流程与管理制度履行交付义务，同时支持集团公司的标准付款条件。
                                        </td>
                                    </tr>
                                    <tr style="border-width: thin; border: 0 1 0 0;" align="center">
                                        <td style="width: 20%;">物料
                                        </td>
                                        <td style="width: 80%;">具有相同性质具有相同行业专业的采购产品与服务项目
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <p>
                                    <br />
                                    <strong>第四．采购部“事前介入”原则</strong><br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;为了更好为业务部门提供采购支持，提高购买流程效率，降低采购风险，最终获得高性价比的物资产品与服务，采购部倡导“事前介入”工作原则与方法，实现有效的采购流程管理控制，为业务部门提供更多增值服务。<br />
                                    1、先期介入，协助业务团队明确业务需求，确定产品、服务规格与供应质量要求与交付标准；<br />
                                    2、推荐合适有竞争能力的供应资源，与业务部门协作完成供应商确定、开发选择工作；<br />
                                    3、价格制订，竞价策划，竞标流程管理与采购合同商业谈判，降低采购成本；<br />
                                    4、宣传操作规范，指导购买流程，实现有效管理控制，提高项目进程速度。<br />
                                    <br />
                                    <strong>第五．“协议供应商优先使用”原则</strong><br />
                                    须以“协议供应商优先使用”为原则，选择使用协议供应资源。<br />
                                    1、协议供应商提供较优惠宽松的商务条件与较高附加值的服务；<br />
                                    2、通过协议供应商购买，降低随机购买活动，并有效控制随机购买中潜在的风险<br />
                                    3、不断增加的双边商业合作可以巩固加强供应链伙伴关系，帮助双方持续改善成本结构，降低整体运营成本（包含购买与操作成本）<br />
                                    4、在使用协议供应商的情况下，内部审批流程较为简便，加快操作速度提高流转效率。<br />
                                    各个业务团队须遵循此原则，优先使用采购部推荐的协议供应商。<br />
                                    <br />
                                    <strong>第六．采购申请流程管理办法</strong><br />
                                    <strong>1、基本原则：</strong><br />
                                    （1）在任何情况下，采购申请人不可以同时为此采购申请的审批人。<br />
                                    （2）采购申请须事前提交，在审批合格后方可与供应商承诺，执行采购合同或订单。<br />
                                    （3）未获事前审批，但已经执行完毕或正在执行的采购项目，均视为事后申请即违反了集团管理规定。申请人须参考本管理条例中的《事后申请流程》中相关条例进行申请。<br />
                                    <br />
                                    <strong>2、一般规定：</strong><br />
                                    （1）第三方采购须事先提交采购申请，申请人须按照《集团审批授权规定》取得业务审批以及采购审批之后方可执行。（注：媒介中心采购除外，此类购买需求及费用支出申请，参照媒介中心与财务部管理规定执行。）<br />
                                    （2）项目执行过程中，原采购合同/订单出现的变更时，若遇以下情况，申请人必须事前提交采购申请，在获得业务与采购审批之后方可执行。<br />
                                    ☆ 原合同/订单中的物料单价提高；<br />
                                    ☆ 原合同/订单中的物料订购数量追加，且总金额增加在10％或2，000元以上，以先到达者为准；<br />
                                    （3）若遇项目现场临时发生的零星采购费用在RMB500元以下的（按每个项目计算），无须事前提交采购申请，项目负责人可以先行通过现金购买，事后按集团财务部《费用申请、报销与借款管理条例》提交费用报销申请。<br />
                                    （4）针对原采购合同、订单的取消，申请人需要事前通知采购部，进行订单取消手续。<br />
                                    （5）对于已有采购协议与标准价格的常规类采购申请，为了提高申请效率，只需业务审批而不需再进行采购审批。下面三类情况适合此办法，申请流程按集团行政部,财务部与人事部门相关管理规定执行。<br />
                                    ☆ 员工出差的差旅机票；<br />
                                    ☆ 员工出差的差旅酒店；<br />
                                    ☆ 市内，国内快递。<br />
                                    ☆ 直接支付给个人的有关临时服务人员、兼职人员、实习生人员的劳务费用，需要经由各地人事部门进行备案。注：具有一定专业技能的freelancer如兼职摄影，个人翻译，咨询人员等除外，仍需采购部审批。<br />
                                    ☆ 销售合同已经指明或客户已经确认的，由星言云汇进行代垫支付的有关活动中所涉及的津贴费用。<br />
                                    ☆ 试驾活动中所涉及的实报实销费用如：过路、过桥费用，停车，加油费用，申请人凭发票直接进行付款申请<br />
                                    ☆ 通过商场，超市购买的物品，包含书籍，报纸，杂志，代金卡，代金券等总费用不超过2,000元的<br />
                                    ☆ 活动现场发生的紧急临时费用，包含餐费，食品，住宿，交通等费用支出<br />
                                    ☆ 活动现场发生的为客户代垫费用 (特指项目现场临时追加性质的代垫费用)，须出具客户确认的追加单或相当于项目费用追加的确认说明<br />
                                    ☆ 公司需要支付政府部门相关费用，须出具政府部门的相关收费依据<br />
                                    ☆ 演出门票，社会公开培训或专家讲座费用，须出具主办方相关通知作为证明<br />
                                    ☆ 公司支付申请商业、行业协会等会籍费用, 须出具协会主办方相关通知作为证明<br />
                                    ☆ 捐赠，赞助等费用<br />
                                    ☆ 海关征收的物品进出口关税费用，须提供关税付款单据<br />
                                    <br />
                                    <strong>3、采购申请流程细则</strong><br />
                                    （1）一般情况下的第三方物料采购申请步骤，<br />
                                    ☆ 申请人填写集团统一格式的采购申请单。线上系统使用统一PR申请单; 特殊情况时需要使用手工模板的, 见附件(一)《采购申请单－手工模板v1.0》<br />
                                    ☆ 申请人通过手工填写或电子形式提交书面申请，并按《集团审批授权规定》获得业务审批。<br />
                                    ☆ 在业务审批后，申请人提交此采购申请及业务审批结果至采购部进行采购审批。采购部门依据《采购审批流程管理办法》中的规定进行采购审核。<br />
                                    ☆ 以上审批流程完成后才可与供应商承诺购买协议，开始执行采购合同或订单。<br />
                                    （2）采购申请申请步骤说明：<br />
                                    ☆ 采购部的审批时效为3个工作日内反馈，为了加快工作效率，请提前知会采购部并提交所需信息至采购项目负责人。<br />
                                    ☆ 所有申请与审批记录须为书面或电子邮件方式。若遇特殊情况即无法当时进<br />
                                    ☆ 行书面或电子邮件方式申请或审批的，请参考下面“特殊（紧急情况下）采购申请流程”中的规定执行。<br />
                                    （3）一般正常流程图例如下：<br />
                                </p>
                                <img src="images/p1.jpg" />
                                <br />
                                <p>
                                    <strong>（4）特殊（紧急情况下）采购申请流程</strong><br />
                                    特殊申请流程适用于在业务活动中出现紧急情况, 并在预计的时间内, 无法事先完成线上申请与审批, 但需要立即执行第三方采购的情况. 紧急(特殊)情况包含如下:
                                <br />
                                    ☆ 业务审批: 申请人发送邮件至团队总监以上级别进行业务审批.<br />
                                    注1: 若为上述无项目号的情况, 还必须通过集团总裁审批.
                                <br />
                                    注2: 按《集团审批授权规定》, 该申请的总采购额在1-5万的, 需总经理审批, 超过5万元的至集团总裁审批.
                                <br />
                                    注3: 申请邮件中须说明紧急采购的原因, 同时提供带有客户明确”同意提供服务/项目执行”的确认字样以及客户确认相关金额的邮件.<br />
                                    ☆ 财务审批: 所有特殊申请邮件必须同时抄送财务总监进行审批<br />
                                    ☆ 采购审批: 所有特殊申请邮件必须同时抄送采购物料负责人/采购总监进行采购审批<br />
                                    注1: 申请人须第一时间通知采购部进行供应商选择评估与价格确定.<br />
                                    注2: 采购部保留审批记录, 定期汇总并汇报至集团管理层审阅。《特殊采购申请报告》见附件（二）<br />
                                    ☆ 通知供应商执行: 采购部邮件通知供应商确认费用并执行。<br />
                                    ☆ 后补线上流程: 申请人须在申请后的3个工作日内补线上流程, 并在系统中附上原审批记录与执行邮件。若延期仍未提交申请的, 将按事后申请处理.<br />
                                    （5）若违反以上流程规定自行与供应商承诺采购的, 将按事后申请处理.
                                <br />
                                    （6）特殊申请流程图例如下：<br />
                                </p>
                                <img src="images/p2.jpg" />
                                <br />
                                <p>
                                    <strong>4、事后申请流程</strong><br />
                                    （1）任何未获事前审批，但已经执行完毕或正在执行的采购项目，无论是合同金额或购买数量多少，均视为事后申请。事后申请须执行严格报批手续。<br />
                                    （2）申请人须在项目结束后3个工作日提交事后申请，或委托其他项目相关人员代办。<br />
                                    （3）事后申请影响正常业务运作，给公司带来较大商业风险隐患，会直接或间接损害与供应商的长期合作关系，引起不必要的法律纠纷。由于此行为违反了集团采购管理规定，为了管理控制的需要，须按如下流程报批申请。<br />
                                    ☆ 申请人分别填写采购申请单与事后申请单, 说明事后申请理由与原因<br />
                                    ☆ 申请人须获得业务部门领导签字，财务执行官签字，CEO签署<br />
                                    ☆ 申请人提交采购部进行采购审批<br />
                                    （4）违纪处分<br />
                                    对于事后申请，视情节严重，公司会考虑给以责任人相应处罚。<br />
                                    注：为集团控制与管理需要，采购部对事后申请进行存档，记录，汇总，定期汇报集团管理层审阅。<br />
                                    （5）事后申请流程图例如下：<br />
                                    <br />
                                </p>
                                <img src="images/p3.jpg" />
                                <br />
                                <p>
                                    <br />
                                    <strong>第七、采购项目负责人职责与分工</strong><br />
                                    星言云汇采购部实行二级审批制度，即采购项目负责人与采购总监逐级审批制度。项目负责人按物料类别划分，进行审批工作，并对审批结果负责。采购总监为采购部内最后审批人对最终的审批结果负责。<br />
                                    <strong>1、采购项目负责人审批分工</strong><br />
                                    采购部内部实行物料管理分工。细节、专项的采购物料管理方法目的是提高采购部门的行业经验、专业技能与执行能力，实现与业务部门协作分工，高效互动。
                                <br />
                                    注：采购部总监负责按业务需要以及采购部人员配备情况，对项目分工做相应安排与调整。<br />
                                    <strong>2、采购项目负责人基本职责与工作范围（包含，但不限于以下项目）</strong><br />
                                    (1)采购项目支持方面：<br />
                                    ☆ 采购管理政策的执行, 流程解释，内外部宣传与沟通<br />
                                    ☆ 接收业务部门的采购申请，提供采购支持，确认采购需求与报价初步拟定<br />
                                    ☆ 审核购买预算(项目号与采购预算审批核查)<br />
                                    ☆ 供应商的推荐，寻求, 评估与确定，（注：评估工作须与业务团队协作完成）<br />
                                    ☆ 供应商询价、议价、竞标流程管理<br />
                                    ☆ 合同与商业谈判, 包括价格, 帐期与交付条件等条款<br />
                                    ☆ 供应交付质量<br />
                                    (2)日常采购管理方面：<br />
                                    ☆ 采购政策维护，流程持续改进<br />
                                    ☆ 采购合同与订单的日常统计与管理<br />
                                    ☆ 采购月报表，包含采购合同、订单报表，采购管理工作汇报<br />
                                    ☆ 协议供应商产品与服务供应价格更新<br />
                                    ☆ 新协议供应商引入，供应信息库维护<br />
                                    ☆ 供应商管理，包含供应商定期审计，绩效评估与供应资源开发<br />
                                    ☆ 供应商、业务团队的定期沟通与意见反馈<br />
                                    ☆ 用户满意度管理<br />
                                    ☆ 协议供应商付款流程支持<br />
                                    <br />
                                    <strong>第八．采购审批流程操作细则</strong><br />
                                    <strong>1、基本原则：</strong><br />
                                    在任何情况下，申请人不可以同时为审批人。<br />
                                    <strong>2、一般核查内容</strong><br />
                                    （1）预算：<br />
                                    ☆ 项目号是否正确<br />
                                    ☆ 费用明细是否已在项目号申请单内清楚列出<br />
                                    ☆ 费用金额是否涵盖在项目号申请单预算内<br />
                                    （2）采购审批（合同签署记录单）<br />
                                    ☆ 按《集团审批授权规定》的审批签署<br />
                                    ☆ 供应商名称（全称）<br />
                                    ☆ 申请人信息<br />
                                    ☆ 用户信息<br />
                                    ☆ 收货人信息<br />
                                    ☆ 星言云汇公司名称（全称）<br />
                                    （3）供应商资源：<br />
                                    ☆ 协议供应商<br />
                                    ☆ 客户指定供应商 （需要事前有CEO、张总签字）<br />
                                    ☆ 唯一供应商（single/sole source）<br />
                                    ☆ 业务团队推荐供应商<br />
                                    ☆ 其他<br />
                                    （4）报价单：<br />
                                    ☆ 标准报价单
                                <br />
                                    ☆ 产品、服务规格是否描述详细、准确<br />
                                    ☆ 产品单价（基础报价），数量，单位，折扣，税金，报价总额，有效期，货币种类，兑换税率等<br />
                                    ☆ 保修条款<br />
                                    ☆ 服务内容描述<br />
                                    ☆ 付款帐期<br />
                                    ☆ 承诺送货时间<br />
                                    ☆ 承诺交付标准<br />
                                    ☆ 供应商报价比较<br />
                                    ☆ 询、议价历史，或竞标文档<br />
                                    （5）采购订单或采购合同<br />
                                    ☆ 合同甲乙双方指代清楚：按甲方为买方, 乙方为卖方的方式, 即星言云汇为甲方, 供应商为乙方的原则进行签订<br />
                                    ☆ 星言云汇公司名称（全称）<br />
                                    ☆ 供应商名称（全称）<br />
                                    ☆ 公章是否是合同章<br />
                                    ☆ 签署日期是否准确<br />
                                    ☆ 合同中内重要信息与条款禁止手写：如金额，数量，产品、服务描述，合同期限等内容<br />
                                    ☆ 规格是否描述详细、准确<br />
                                    ☆ 数量是否已经明确<br />
                                    ☆ 单价是否准确<br />
                                    ☆ 总额是否准确<br />
                                    ☆ 明确的送货地点与接收人<br />
                                    ☆ 违约条款的规定<br />
                                    ☆ 支付条件满足<br />
                                    ☆ 合同解除条款<br />
                                    ☆ 供应商义务和责任承诺是否充分<br />
                                    ☆ 供应商服务范畴界定明确，并与业务需求一致<br />
                                    ☆ 承诺交付标准是否明确，并与业务需求一致<br />
                                    ☆ 其他不利于星言云汇的商务条款<br />
                                    <br />
                                    <strong>3、审批流程规范细则</strong><br />
                                    （1）通过协议供应商购买<br />
                                    1）购买标准产品服务。即供应商合作协议中已覆盖的，且有标准报价的产品与服务项目.<br />
                                    ①报价要求：<br />
                                    ☆ 采购合同总价为1,000元以下（包含），不需要供应商的书面报价，以协议中规定的报价为准<br />
                                    ☆ 采购合同总价为1,000元以上，需要供应商提供书面报价。（注：按采购部规定的标准，提供明细报价）<br />
                                    ②审批流程：<br />
                                    ☆ 10，000元以下（包含）无须报价比较，在确认为标准报价后，直接提交采购总监进行审批。<br />
                                    ☆ 10，000元以上,需要报价比较，操作流程如下：<br />
                                    A． 需要提供同类型的供应商的报价比较（按照协议供应商优先参与比价的原则）。提交采购总监审批时，以书面形式说明报价比较与选择依据。<br />
                                    B．若为唯一供应商（sole/single source supplier）即指向一家供应商购买如，(1)只能从特定供应商处采购，或供应商拥有专利权，其他商家无法替代的；(2)
                                在用的仪器设备，因后继维修或扩展功能所需的零配件或部件必须向原供应厂商购买的; (3)原采购合同的后继补充或追加订货的，在提交采购总监审批时，以书面形式提供说明及选择依据（包含报价合理性的评估判断）<br />
                                    C．若为客户指定供应商 （注：业务部门须事前得到客户书面确认及CEO的批准），项目负责人须按上述流程进行价格比较与审核，之后提交采购总监审批；另外，如果客户也同时确认了该报价金额
                                (注：业务部门须提供采购部门客户的书面报价确认)，此时不须报价比较，项目负责人可直接提交采购总监批审批。<br />
                                    D．若为业务部门建议的供应商，须按以上流程进行价格审核，若价格合理且具有竞争力，在提交采购总监审批时，项目负责人以书面形式提供比价说明；若此价格不具竞争力，还须提供业务部门的推荐说明并随附团队总经理的批示。<br />
                                    ③其他注意事项：<br />
                                    ☆ 项目负责人需要及时补充或更新供应商报价信息库。<br />
                                    ☆ 项目负责人员保留询价（包含口头询价）、议价、谈判的报价历史记录并存档备案。<br />
                                    2）购买非标准产品服务，即供应商合作协议之外的产品与服务项目，流程同“非协议供应商“审批流程（见如下细则）<br />
                                    （2）通过非协议供应商购买<br />
                                    1）报价要求：需要供应商书面报价（注：按采购部规定的标准，提供明细报价）<br />
                                    2）审批流程：<br />
                                    A． 无论金额大小，都需要提供同类型的供应商的报价比较（按照协议供应商优先参与比价的原则）。提交采购总监审批时，以书面形式说明报价比较与选择依据。<br />
                                    B．若为唯一供应商（sole/single source supplier）即指向一家供应商购买如，(1)只能从特定供应商处采购，或供应商拥有专利权，其他商家无法替代的；(2)
                                在用的仪器设备，因后继维修或扩展功能所需的零配件或部件必须向原供应厂商购买的; (3)原采购合同的后继补充或追加订货的，在提交采购总监审批时，以书面形式提供说明及选择依据（包含报价合理性的评估判断）<br />
                                    C．若为客户指定供应商 （注：业务部门须事前得到客户书面确认及CEO的批准），项目负责人须按上述流程进行价格比较与审核，之后提交采购总监审批；另外，如果客户也同时确认了该报价金额
                                (注：业务部门须提供采购部门客户的书面报价确认)，此时不须报价比较，项目负责人可直接提交采购总监批审批。<br />
                                    D．对于业务部门建议的供应商，须按以上流程进行价格审核，若价格合理且具有竞争力，在提交采购总监审批时，项目负责人以书面形式提供比价说明；若此价格不具竞争力，还须提供业务部门的推荐说明并随附团队总经理的批示。<br />
                                    3）其他注意事项：<br />
                                    A．对于优质供应商资源，项目负责人须及时总结，补充或更新现有供应商资源库，签订协议，建立长期合作关系。（参考《供应商准入制度》）<br />
                                    B．项目负责人员保留询价（包含口头询价）、议价、谈判的报价历史记录并存档备案。（询价记录在审批时附加在系统中）<br />
                                    C．参考〈集团审批授权规定〉，询、比价数量须按采购部门如下的规定进行：<br />
                                    ① 合同金额10，000以下（含），若购买协议供应商标准产品、服务无需报价比较；<br />
                                    ② 合同金额10，000以下 (含)，若购买非标准产品或服务，或使用非协议供应商的情况，需提供两家以上的报价比较。（特殊情况除外）<br />
                                    ③ 合同金额10，000 - 50，000（含），需提供两家以上的报价比较。（特殊情况除外）<br />
                                    ④ 合同金额50，000以上，需提供三家以上的报价比较，（特殊情况除外）<br />
                                    （3）其他规定：<br />
                                    以下情况为不需要报价比较的，可以直接进行采购审批。<br />
                                    1）若通过超级市场，百货商店，购物中心购买的商品<br />
                                    2）图书，报刊，杂志的购买申请<br />
                                    3）代客垫付费用（销售合同已经指明，或客户已经确认的代垫费用），但业务部门须提供客户的书面确认。<br />
                                    <br />
                                </p>
                            </td>
                            <td id="td811" valign="top" style="padding: 10px 30px; line-height: 20px; display: none;">
                                <div class="title-orag">
                                    网络制度（网络及设备安全管理办法）
                                </div>
                                <br />
                                <p>
                                    <strong>第一，电子邮件使用管理办法</strong><br />
                                    <strong>1、邮件性质：</strong><br />
                                    公司的电子邮箱只限工作使用，使用时必须遵守有关的国家法律和法规，严禁传播淫秽、反动等违犯国家法律和中国道德与风俗的内容。公司有权撤消违法犯纪者邮箱的使用，并交由有关部门处理。<br />
                                    <br />
                                    <strong>2、邮箱空间：</strong><br />
                                    公司个人邮箱采用无限邮箱容量策略，普通邮件附件大小限制在20MB以内。大附件限制在2GB以内需要通过网页界面上传发送。<br />
                                    <br />
                                    <strong>3、邮箱使用：</strong><br />
                                    可通过在outlook、Foxmail等客户端软件和WEB(<a href="https://exmail.qq.com" target="_blank">https://exmail.qq.com</a>）两种方式发送邮件。<br />
                                    <br />
                                    <strong>4、邮箱的申请、变更、撤销：</strong><br />
                                    符合条件的员工，经部门主管同意，建立电子邮箱，新邮箱1个工作日之后即可开通使用。实习、兼职、项目组员工建立电子邮箱，需求人员向HR申报，需逐级报批经主管部门同意可开通邮箱。<br />
                                    公司内部调动的员工，电子邮箱保持不变。邮箱的变更，个人不允许改变账号，只允许改变自己的邮箱密码。离职的员工，邮箱必须立即撤销。特殊情况以公司人力资源的手续为准。<br />
                                    <br />
                                    <strong>5、群发邮箱：</strong><br />
                                    群发邮箱必须逐级严格报批，经人事部门同意方可建立。临时或项目群发邮箱建立需明确使用期限。只有职能部门主管、总经理及总经理助理或总经理特别授权指定的人员有群发权限。对错发、误发及利用群发邮箱对公司造成损失和危害的，按本手册奖惩办法相关规定处罚。<br />
                                    <strong>6、网络禁止</strong><br />
                                    1)	禁止在办公区域安装个人无线设备（无线AP、无线路由、无线热点等）。<br />
                                    2)	禁止任意篡改网络IP<br />
                                    3)	禁止在网络上散布不当言论或执行不法情事<br />
                                    4)	禁止散布或发表威胁、猥亵、攻击、诽谤之资料及文章<br />
                                    5)	禁止散布或发表具有商业版权、商业广告营利、专利之资料及文章<br />
                                    6)	禁止传送未经授权的任何档案及文件<br />
                                    7)	禁止耗用大量带宽及储存空间的资料<br />
                                    8)	禁止使用破坏网路及节点的硬软件系统(如：P2P软件BT，电驴等)<br />
                                    9)	禁止利用系统漏洞做实验或破坏性活动。<br />
                                    10)	不得从事商业行为<br />
                                    11)	不得传送、放置、散布任何病毒、非法软件、广告信件<br />
                                    12)	未经许可，不得移动、修改、窥视任何不属于个人所有的档案及目录<br />
                                    <br />
                                    <strong>第二，星言云汇电话服务规范</strong><br />
                                    <strong>1、星言云汇电话的使用:</strong><br />
                                    1)	拨打市内电话<br />
                                    ☆ 上班时间原则上不使用公司电话拨打私人电话。如遇特殊情况，请尽量缩短通话时间(不应超过三分种)，以免影响工作。<br />
                                    ☆ 严禁工作时间打电话聊天。<br />
                                    ☆ 严禁拨打信息台。<br />
                                    2)	拨打长途电话<br />
                                    ☆ 星言云汇内部沟通（北、上、广）应使用内部IP电话，用四位分机号拨打。<br />
                                    ☆ 多方会议电话，准备工作应在10分钟内完成，以提高使用效率。<br />
                                    ☆ 公司会议室提供国际长途功能。<br />
                                    ☆ 所属私人长途，均按月统计费用，现金缴入财务。<br />
                                    <br />
                                    <strong>注：总机接线电话，只许接听，不准打出</strong><br />
                                    <br />
                                    <strong>2、电话变更与维护：</strong><br />
                                    1)	岗位变动等原因需调整电话，向本层HR提供工位点位编码和电话号码，由IT部实施调整。<br />
                                    3)	处罚对违反上述规定者，公司将视情节轻重及给公司造成的损失程度，处以不同金额的罚款及行政处理。<br />
                                    <br />
                                    <strong>第三，门禁、监控服务规范</strong><br />
                                    <strong>1、门禁使用</strong><br />
                                    1)	总部办公楼实行封闭式管理<br />
                                    2)	门禁系统自动存储集团员工进出信息（进出人员身份、时间、门的编号）、异常等信息，历史记录可以调用、查询及打印。<br />
                                    3)	门禁系统实行分级授权管理制度，各进出门管理可授权相应办公人员的进入级别，并可限制其进入的办公区域。<br />
                                    4)	员工上班刷进门读卡器，下班刷出门读卡器，伴随”嘀”声、读卡器显示绿灯表示打卡正常，进出门后务必随手关门。<br />
                                    <br />
                                    <strong>2、门卡变更、撤销</strong><br />
                                    1)	星言云汇行政具有对门禁卡的发放、回收及人员调动等相关信息的变更及数据查询的权利。持卡人若离职或调动，需将门卡收回或重新设定使用权限。<br />
                                    2)	持卡人有责任保管好自己的门卡。若有遗失，需立即通知行政部门禁止该丢失卡的使用权限，同时进行补卡。<br />
                                    <br />
                                    <strong>3、门禁维护</strong><br />
                                    1)	持卡人在使用过程中发现问题应立即通知行政部门联系维修。IT部每月应对门禁系统的使用、运行情况进行检查，如有问题，立即通知供应商上门维修。<br />
                                    2)	因该设备的供应商掌握门禁系统的高级密码。因此，门禁系统必须由固定的供应商进行维护保养。维护保养协议中必须要有追究供应商泄密责任的条款。<br />
                                    <br />
                                    <strong>4、禁止使用</strong><br />
                                    1)	非公司员工禁止使用公司门卡，特殊需要如保洁、保安、兼职等需严格登记，并定期核查打卡情况。<br />
                                    2)	禁止员工代打卡，外来人员来访需相关联系人打卡带入办公区域。<br />
                                    <br />
                                    5、监控系统<br />
                                    星言云汇对办公区域进行全面监控及录像，调阅时需行政协助。<br />
                                    <br />
                                    <br />
                                </p>
                            </td>
                            <td id="td1011" valign="top" style="padding: 10px 30px; line-height: 20px; display: none;">
                                <div class="title-orag">
                                    休假的管理原则
                                </div>
                                <br />
                                <p>
                                    1)	公司鼓励员工休假，并合理安排自己的假期，除不能预计的应急特殊情况外，员工请假均需要提前安排职责代理人，确保相关工作的顺利进行。所有假期，均需提前申请，除在系统进行提交外，员工应向其主管提出申请，获得批准后方可休假。<br />
                                    <br />
                                    2)	病假、事假可按小时计算（未满一小时按一小时计算）。<br />
                                    <br />
                                    3)	年假、调休可以半日为计算单位。<br />
                                    <br />
                                    4)	婚假、产假、丧假必须一次性休完。<br />
                                    <br />
                                    5)	员工请假如影响公司业务正常进行时，主管人员有权依本公司规定和业务情况不予准假、或缩短假期、或延后准假。<br />
                                    <br />
                                    6)	事先未请假或请假未获准而未出勤，或擅自离开工作地点、岗位者，视为旷工。<br />
                                    <br />
                                    7)	员工应在假期结束后返回公司正常上班，若需继续请假应事先向主管再次提出书面申请，续假未被批准者未出勤按旷工处理。<br />
                                    <br />
                                    8)	如本手册关于员工休假的相关规定与员工实际工作所在地法律法规的强制性规定存在冲突的，按照相关法律法规的规定执行。<br />
                                    <br />
                                    <br />
                                    <br />
                                    <strong>一、 年假
                                    <br />
                                        <br />
                                        年度休假类型：包括年假和福利年假两部分</strong>
                                    <br />
                                    1、 年假：<br />
                                </p>
                                <table border="1" cellpadding="0" cellspacing="0" width="50%">
                                    <tr style="background-color: Olive;">
                                        <td style="width: 50%;">工龄（年）
                                        </td>
                                        <td>年休假天数
                                        </td>
                                    </tr>
                                    <tr style="background-color: Silver;">
                                        <td style="width: 50%;">&nbsp;&nbsp;1‹工龄‹10
                                        </td>
                                        <td>5
                                        </td>
                                    </tr>
                                    <tr style="background-color: Silver;">
                                        <td style="width: 50%;">10‹工龄‹20
                                        </td>
                                        <td>10
                                        </td>
                                    </tr>
                                    <tr style="background-color: Silver;">
                                        <td>20‹工龄
                                        </td>
                                        <td>15
                                        </td>
                                    </tr>
                                </table>
                                <p>
                                    <br />
                                    2、 福利年假：<br />
                                    员工入职后享受5天公司福利年假。服务期每满一个自然年后的次年，福利年假可增加1.5天。<br />
                                    <br />
                                    ☆ 年度休假实施细则：<br />
                                    1)	年度休假以1月1日至12月31日历年制计算，逾期作废。年度休假不予提前预支。<br />
                                    2)	年度休假总天数最多不超过25天。<br />
                                    3)	新员工入职当年的年度休假天数以入职日起算至年底12月31日止，按比例折算，折算后不足0.5天的部分不享受年度休假。计算方法：（入职日至当年12月31日总天数）/ 365×年假基数<br />
                                    4)	 有下列情形之一的，不享受当年的年度休假：<br />
                                    ⅰ累计工作满1年不满10年的员工，当年病假累计2个月以上的；<br />
                                    ⅱ累计工作满10年不满20年的员工，当年病假累计3个月以上的；<br />
                                    ⅲ累计工作满20年以上的员工，当年病假累计4个月以上的。<br />

                                    5)	员工已享受当年年度休假，年度内又出现前款第4）项中的任何一项所规定情形之一的，不享受下一年度的年度休假或在当年扣回已休年假。<br />
                                    6)	只要员工或公司任何一方终止或解除劳动合同关系，不论是何理由，福利年假都将自动取消。若预支或超出离职日前所对应的福利假天数，则超出部分需补回公司，计算标准为：超出（预支）天数×日工资。<br />
                                    7)	年假和福利年假可以结合使用。<br />
                                    8)	以上休假的天数以工作日为单位。<br />
                                    9)	工龄小于1年，入职当年不享受年假及福利年假，工龄初满1年后至当年底，年假及福利假按比例折算。<br />
                                    10)	已申请笔记本报销的员工，年假期间享受笔记本补助。<br />

                                    <br />
                                    <br />
                                    <strong>二、婚假</strong><br />
                                    1)  正式加盟星言云汇后领取结婚证书，北京地区公司的员工享有10天婚假。再婚及其他地区按当地法律、法规规定执行。<br />
                                    2)	婚假须提前7天提出申请，提供结婚证书复印件并需在OA系统进行线上申请填写，报团队领导审批，人力资源与行政部备案。<br />
                                    3)	婚假有效期自结婚证书签发之日起一年内有效。<br />
                                    4)	婚假须一次性连续使用，不得随时间推移累积或转移。
                                    <br />
                                    5)	婚假期间不享受笔记本补助。<br />
                                    <br />
                                    <strong>三、产假</strong><br />
                                    1)	祝贺您！确定怀孕后，请通知您的直线领导及人力资源部并附上医生开具的证明，证明上需要注明预产期。依照国家法律，女员工生育享有产前检查假和产假，并且给予安排产后哺乳时间。<br />
                                    2)	有不满一周岁婴儿的女职工，每个工作日给予其两次哺乳时间，每次30分钟。两次哺乳时间可以合并使用。<br />
                                    3)	产假期间的公共假期不做额外计算，员工不享有额外薪水。<br />
                                    4)	顺产假共计128天，一般是预产期前连续日历日15天，分娩后连续日历日83天。晚育女员工多30天晚育假，在产假后连续使用。<br />
                                    5)	难产（剖腹产）或多一胞胎另各增15天难产假，在产假后连续使用。<br />
                                    6)	符合国家政策规定的生育二胎：享受128天产假，一般是预产期前连续日历日15天，分娩后连续日历日83天。难产（剖腹产）或多一胞胎另各增15天难产假，在产假后连续使用。<br />
                                    7)	产前检查假:自员工在医院建档后至怀孕28周，每月检查一次；28周至36周每2周检查一次；36周以后每周检查一次。每次检查时间均为半天。<br />
                                    8)	产假期间不享受笔记本补助。<br />
                                    9)	产假期间工资计算：<br />
                                    ☆  有生育保险的，按生育津贴发放工资；差额部分由公司补发。产假期间无餐补，13薪按全额工资计提。员工有义务配合公司在法规范围内申领生育津贴。<br />
                                    ☆  因符合当地法规要求而无生育保险的，按基本工资发放工资，无13薪。<br />
                                    ☆  因员工个人原因而未通过星言云汇缴纳生育保险的，工资按其本人全额工资及应缴生育保险基数的差额发放。产假期间无餐补，13薪按全额工资计提。<br />
                                    <strong>以上产假及相关生育政策均参照当地相关法律、法规执行，如当地法律、法规有最新规定，凡发生在最新规定自生效日期起的女员工按照新规定执行。</strong><br />
                                    <br />
                                    <strong>四、 丧假</strong><br />
                                    1)	原则上凭医院《死亡通知书》复印件，公司将按以下情况给予员工最长3天的丧假：<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp 员工的父母、配偶、子女或员工配偶的父母去世&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3天<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp 员工的祖父母，外祖父母，兄弟姐妹 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;1天<br />
                                    2)	上述休假须与相应的时间匹配，在事件当月休完，不得随着时间推移而累计、转移和冲抵。<br />
                                    <br />
                                    <strong>五、 病假</strong><br />
                                    1)	如果员工因病无法工作，可申请病假。员工休病假1天的，可不提供医院证明，但应于病假当天上午10点之前向直接主管请假；病假连续超过(含)2天的，需要在上班当天提供有效医院病假证明（社区医院以上级别的医院）及病例原件的复印件并办理病假手续，并报人力资源部备案。病假天数以日历日计算，公休日、法定节假日包括在内。<br />
                                    2)	如需长期治疗（1个月以上者），需提供三级甲等医院的诊断证明及病例原件的复印件，申请经部门总监及人事部门负责人审批，并按具体情况协商处理。<br />
                                    3)	病假未经核准者，其假期按旷工处理；情节严重或发现伪造病假者，公司可不经警示即时解除劳动合同，并不提供任何经济补偿。<br />
                                    4)	公司保留要求员工在指定医院复检的权利。<br />
                                    5)	病假期间工资的计算：<br />
                                    员工因患病或非因工负伤需停止工作进行治疗且在医疗期内的，可享受的工资待遇按如下标准执行：<br />
                                    1、 员工自入职日起，每年可享受累计15个工作日的50%带薪（基本工资的50%）病假。<br />
                                    2、 病假全年超过15天的，基本工资按照25%发放。病假仅限于当年度使用，不可累计至下一年度。<br />
                                    3、 当月或跨月连续病假超过10天且不足15天的，当月或次月无绩效，工资按请病假天数如上规定核算，病假期间无餐补，无笔记本补助，计提13薪。<br />
                                    4、 当月或跨月连续病假超过15天且不足1个月的，当月或次月无绩效，无笔记本补助，当月不计提13薪，病假当日无餐补。<br />
                                    5、	病假超过（含）1个月的，当月无绩效、无餐补，无笔记本补助，当月不计提13薪。<br />
                                    6、	核算后低于北京市最低工资的80%的，按最低工资的80%发放工资。<br />

                                    <br />
                                    <strong>六、事假：</strong><br />
                                    1)	员工应尽力控制事假天数，年度休假未休完者不能休事假。<br />
                                    2)	事假应事先申请，经主管经理、部门总监批准。<br />
                                    3)	请假未获批准而擅自不上班者按旷工处理。<br />
                                    4)	事假按实际请假天数在当月工资中扣除。<br />
                                    5)	如有特殊情况，需经部门总监批准，提前通知人力资源与行政部备档。<br />
                                    6)	事假期间工资的计算：<br />
                                    1、 当月事假超过10天且不足15天的，扣除100%当月绩效工资，事假按天数按基本工资全额扣款，同时，事假期间无餐补，无笔记本补助，计提13薪；<br />
                                    2、 当月事假超过15天且不足1个月的，当月无绩效，无笔记本补助，当月不计提13薪。事假当日无工资、无餐补。<br />
                                    3、 超过（含）1个月的，当月无绩效，事假当月无工资，无餐补，无笔记本补助，当月不计提13薪。<br />
                                    <br />
                                    <strong>七、工伤假：按照当地相关法律、法规规定执行。</strong><br />
                                    <br />
                                    <strong>八、 劳动合同暂时终止</strong><br />
                                    此期间无工资，无餐补，无13薪，不存在劳动关系。<br />
                                    <br />
                                    如需要公司协助继续缴纳社保，经本人申请，集团管理委员会同意办理，需要本人将公司和个人缴纳总额提前汇款至公司指定账户，并和公司签署免责协议。<br />
                                    <br />
                                    <strong>九、加班待遇及加班调休假</strong><br />
                                    <br />
                                    1)	公司提倡讲求工作效率，不鼓励员工加班。<br />
                                    2)	工作日加班申报流程：加班需提前在线下填写《加班申请单》，经上级主管及各级审批人批准方可生效。员工未填写《加班申请单》且未获得核准的，不予列入加班。<br />
                                    3)	员工自我职责或例行工作范围需要延长工作时间不予列入加班记录。<br />
                                    4)	员工内外教育培训学习及各类会议等不予列入加班记录。<br />
                                    5)	员工的加班在获得各方审批认可后，由总监根据业务情况对其进行安排核准可以适当做倒休，但不做累积计算。按本员工手册核准的倒休于加班发生当日起一个月内有效当月有效。<br />
                                    6)	加班待遇：按照国家法定相关规定执行。<br />
                                    <br />
                                    <strong>特别声明<br />
                                        1、本政策内容如与当地法律法规规定有冲突地方，以当地法律法规为准；<br />
                                        2、本公司对《星言云汇休假政策》拥有最终的解释权。
                                    </strong>

                                </p>
                            </td>
                            <td id="td1021" valign="top" style="padding: 10px 30px; line-height: 20px; display: none;">
                                <div class="title-orag">
                                    特别声明
                                </div>
                                <br />
                                <br />
                                <p>
                                    1)	本手册的人事规定和政策原则拥有与劳动合同相同的法律约束力，并与劳动合同同时使用。如与当地法律法规规定有冲突地方，以当地法律法规为准；<br />
                                    2)	本手册中所涉及到本公司的规章制度、以及公司所发布的在行的规章制度，均与本员工手册具有相同的法律效力。<br />
                                    3)	本手册中所涉及到本公司的规章制度若与本手册有冲突，则以现行的规章制度为准；<br />
                                    4)	本公司对本手册拥有最终的解释权，保留随时取消、改变或修改这些政策或其中部分的权利。<br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    集团人力资源部<br />
                                </p>
                            </td>
                        </tr>
                        <tr align="right">
                            <td width="201" valign="top">&nbsp;
                            </td>
                            <td background="images/staff-center-35.jpg" id="tdRoll" onclick="scrollbar();" align="center"
                                style="cursor: pointer; color: #787878;">返回顶部
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="padding-left: 177px;">
                    <img src="images/staff-center-36.jpg" width="808" height="56" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
