<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Newcomer.aspx.cs" Inherits="SEPAdmin.ServiceCenter.Newcomer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>入职指引</title>
    <link href="style.css" rel="stylesheet" type="text/css" />
</head>
<body>

    <script type="text/javascript">
        function MenuClick(menuIndex) {
            var td1 = document.getElementById("td1");
            var td2 = document.getElementById("td2")
            var td3 = document.getElementById("td3");
            var td4 = document.getElementById("td4");

            var tr1 = document.getElementById("tr1");
            var tr2 = document.getElementById("tr2")
            var tr3 = document.getElementById("tr3");
            var tr4 = document.getElementById("tr4");

            var trDefault = document.getElementById("trDefault");

            var lbldir = document.getElementById("lbldir");

            if (menuIndex == 1) {
                td1.className = "menu-orag";
                td2.className = "menu-normal";
                td3.className = "menu-normal menu-line";
                td4.className = "menu-normal menu-line";
                tr1.style.display = "block";
                tr2.style.display = "none";
                tr3.style.display = "none";
                tr4.style.display = "none";

                trDefault.style.display = "none";
                lbldir.innerHTML = "考勤规范";
            }
            else if (menuIndex == 2) {
                td1.className = "menu-normal";
                td2.className = "menu-orag";
                td3.className = "menu-normal";
                td4.className = "menu-normal menu-line";
                tr1.style.display = "none";
                tr2.style.display = "block";
                tr3.style.display = "none";
                tr4.style.display = "none";
                trDefault.style.display = "none";
                lbldir.innerHTML = "工作规范";
            }

            else if (menuIndex == 3) {
                td1.className = "menu-normal menu-line";
                td2.className = "menu-normal menu-line";
                td3.className = "menu-orag";
                td4.className = "menu-normal";
                tr1.style.display = "none";
                tr2.style.display = "none";
                tr3.style.display = "block";
                tr4.style.display = "none";
                trDefault.style.display = "none";
                lbldir.innerHTML = "公司政策及员工福利";
            }
            else if (menuIndex == 4) {
                td1.className = "menu-normal";
                td2.className = "menu-normal menu-line";
                td3.className = "menu-normal menu-line";
                td4.className = "menu-orag";

                tr1.style.display = "none";
                tr2.style.display = "none";
                tr3.style.display = "none";
                tr4.style.display = "block";
                trDefault.style.display = "none";
                lbldir.innerHTML = "星言址引";

            }
        }

    </script>

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
            <td background="images/staff-center-12.jpg" style="background-position: right top;
                background-repeat: no-repeat; padding: 21px 30px;">
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="220" height="420" valign="top" background="images/staff-center-04.jpg"
                            style="background-repeat: no-repeat; background-position: 5px top;">
                            <table width="210" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="padding: 20px 0 15px 25px;">
                                        <img src="images/staff-center-13.jpg" width="157" height="40" />
                                    </td>
                                </tr>
                            </table>
                            <table width="210" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td height="280" valign="top">
                                        <table width="210" border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td id="td1" class="menu-normal" onclick="MenuClick(1);">
                                                    考勤规范
                                                </td>
                                            </tr>
                                            <tr>
                                                <td id="td2" class="menu-normal menu-line" onclick="MenuClick(2);">
                                                    工作规范
                                                </td>
                                            </tr>
                                            <tr>
                                                <td id="td3" class="menu-normal menu-line" onclick="MenuClick(3);">
                                                    公司政策及员工福利
                                                </td>
                                            </tr>
                                            <tr>
                                                <td id="td4" class="menu-normal menu-line" onclick="MenuClick(4);">
                                                    集团总部址引
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
                        <td valign="top" style="padding: 45px 30px;">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        <span style="color: #787878;">您目前所在的位置：</span><span style="color: #fe9000;">员工服务中心 -
                                            入职指引 -
                                            <label id="lbldir">
                                            </label>
                                        </span>
                                    </td>
                                </tr>
                                <tr id="trDefault">
                                    <td>
                                        <img src="images/serviceCenter1.jpg" style="vertical-align: top;" />
                                        <img src="images/serviceCenter.jpg" /><br />
                                         <strong>公司地址：</strong>北京市朝阳区双桥路12号院BEZ电子城数字新媒体创新产业园D1宣亚国际<br />
                                        <br />
                                        <strong>重庆地址：</strong>重庆市渝北区杨柳路6号三狼公园6号D4-102<br />
                                        <br />
                                    </td>
                                </tr>
                                <tr id="tr1" style="display: none;">
                                    <td style="padding: 25px 0 0 0; line-height: 24px;">
                                        <strong>日常打卡：</strong>工作时间：9：30am – 18:30pm（12：00pm – 1：30pm午餐），上下班需按时打卡。<br />
                                        <br />
                                        <strong>请假申请：</strong>病事年假等应及时申请并须得到主管级以上领导明确批准（病假两天以上需有效医院证明）。<br />
                                        <br />
                                        <strong>OT申请：</strong>OT必须线上事先申请，在申请时将OT时间标注清楚，总监批复后方可有效。（实际刷卡记录做参考）
                                    </td>
                                </tr>
                                <tr id="tr2" style="display: none;">
                                    <td style="padding: 25px 0 0 0; line-height: 24px;">
                                        <strong>邮件签名档：</strong>当您打开电脑开始工作，请首先确认已设置星言云汇员工的邮件签名。任何疑问，请务必及时咨询您的业务组负责人。<br />
                                        <br />
                                        <strong>办公用品申请：</strong>如需申请办公用品，请到前台填写办公用品申请单并领取。<br />
                                        <img src="images/itemapp.jpg" /><br />
                                        <br />
                                        <strong>培训通知：</strong>公司设置了完善的培训体系，包括入职培训、业务培训、专业培训等，培训前均有邮件通知 , 请随时关注集团邮件通知。<br />
                                        <br />
                                        <strong>人力地图：</strong>进入内网，在页面的右上方“人力地图”，可查询公司内部各部门同事的联系方式。
                                    </td>
                                </tr>
                                <tr id="tr3" style="display: none;">
                                    <td style="padding: 25px 0 0 0; line-height: 24px;">
                                        <strong>公司政策：</strong>请及时阅读员工手册，其他相关公司规章制度。<br />
                                        <br />
                                        <strong>员工福利：</strong>按照国家规定办理相关社保文件转入手续。员工需提供相关文件及资料（根据当地社保要求提交相关材料），并及时确认原单位停止缴纳社保的时间。每月15日前办理入职的员工并能及时提供完整社保手续的，当月社保由公司负责缴纳。因原单位原因未能操作完毕所有手续或其余情况，而使我公司无法于员工入职当月完成办理转入手续操作的，我们将从次月开始为员工正常缴纳。<br />
                                        点击<a href="http://xhr.shunyagroup.com/ServiceCenter/EntryGuid.aspx" style="text-decoration: underline; color: blue;">在职指引</a>将有更详细的内容介绍。<br />
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
            <td height="38" align="center" background="images/staff-center-08.jpg" style="border-bottom: 5px solid #eeaf70;
                color: #717172; font-size: 12px;">
                Copyright ©2018-2024 Xingyan. All rights reserved
            </td>
        </tr>
    </table>
</body>
</html>
