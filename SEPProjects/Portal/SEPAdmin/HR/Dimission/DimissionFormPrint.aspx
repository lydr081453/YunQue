<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DimissionFormPrint.aspx.cs"
    Inherits="DimissionFormPrint" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>离职单打印</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">

    <script type="text/javascript" src="/public/js/syscomm.js"></script>

    <script type="text/javascript" src="/public/highslide/highslide-with-html.js"></script>

    <script language="javascript" type="text/javascript" src="../../public/js/DatePicker.js"></script>

    <script language="javascript" type="text/javascript" src="../../public/js/jquery.js"></script>

    <script language="javascript" type="text/javascript" src="../../public/js/dialog.js"></script>

    <script language="javascript" type="text/javascript" src="../../public/js/jquery-ui.js"></script>

    <link rel="stylesheet" href="/public/css/jquery.tabs.css" type="text/css" media="print, projection, screen" />

    <script type="text/javascript">
        hs.graphicsDir = 'public/highslide/graphics/';
        hs.outlineType = 'rounded-white';
        hs.outlineWhileAnimating = true;
    </script>

    <style type="text/css">
        .highslide-html
        {
            background-color: white;
        }
        .highslide-html-blur
        {
        }
        .highslide-html-content
        {
            position: absolute;
            display: none;
        }
        .highslide-loading
        {
            display: block;
            color: black;
            font-size: 8pt;
            font-family: sans-serif;
            font-weight: bold;
            text-decoration: none;
            padding: 2px;
            border: 1px solid black;
            background-color: white;
            padding-left: 22px;
            background-image: url(/public/highslide/graphics/loader.white.gif);
            background-repeat: no-repeat;
            background-position: 3px 1px;
        }
        a.highslide-credits, a.highslide-credits i
        {
            padding: 2px;
            color: silver;
            text-decoration: none;
            font-size: 10px;
        }
        a.highslide-credits:hover, a.highslide-credits:hover i
        {
            color: white;
            background-color: gray;
        }
        /* Styles for the popup */.highslide-wrapper
        {
            background-color: white;
        }
        .highslide-wrapper .highslide-html-content
        {
            width: 400px;
            padding: 5px;
        }
        .highslide-wrapper .highslide-header div
        {
        }
        .highslide-wrapper .highslide-header ul
        {
            margin: 0;
            padding: 0;
            text-align: right;
        }
        .highslide-wrapper .highslide-header ul li
        {
            display: inline;
            padding-left: 1em;
        }
        .highslide-wrapper .highslide-header ul li.highslide-previous, .highslide-wrapper .highslide-header ul li.highslide-next
        {
            display: none;
        }
        .highslide-wrapper .highslide-header a
        {
            font-weight: bold;
            color: gray;
            text-transform: uppercase;
            text-decoration: none;
        }
        .highslide-wrapper .highslide-header a:hover
        {
            color: black;
        }
        .highslide-wrapper .highslide-header .highslide-move a
        {
            cursor: move;
        }
        .highslide-wrapper .highslide-footer
        {
            height: 11px;
        }
        .highslide-wrapper .highslide-footer .highslide-resize
        {
            float: right;
            height: 11px;
            width: 11px;
            background: url(/public/highslide/graphics/resize.gif);
        }
        .highslide-wrapper .highslide-body
        {
        }
        .highslide-move
        {
            cursor: move;
        }
        .highslide-resize
        {
            cursor: nw-resize;
        }
        /* These must be the last of the Highslide rules */.highslide-display-block
        {
            display: block;
        }
        .highslide-display-none
        {
            display: none;
        }
        .tableBorder
        {
            border-collapse: collapse;
            border: solid 1px black;
        }
        .tdBorder
        {
            border-collapse: collapse;
            border: solid 1px black;
        }
        .white_font
        {
            font-size: 12px;
            color: #FFF;
            text-decoration: none;
            vertical-align: text-top;
            padding-top: 5px;
            background-repeat: no-repeat;
        }
        .white_font a:link
        {
            color: #FFF;
            text-decoration: none;
        }
        .white_font a:visited
        {
            color: #FFF;
            text-decoration: none;
        }
        .white_font a:hover
        {
            color: #FFF;
            text-decoration: underline;
        }
    </style>
</head>
<body onload="javascript:window.location.href='#top_A';">
    <form id="frmMain" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table style="width: 100%; text-align: center;">
        <tr>
            <td align="center">
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td class="heading" colspan="3">
                            员工离职清单<br />
                            EMPLOYEE CHECK-OUT LIST
                        </td>
                        <td align="right" valign="top">
                            <img src="../../Images/xingyan.png" alt="xingyan" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" valign="top">
                            <table width="100%" border="0" style="margin: 2px 5px 2px 5px;" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td width="20%" height="20">
                                        <font style="font-weight: bold;">员工编号：</font><asp:Label ID="labUserCode" runat="server" />
                                    </td>
                                    <td width="20%" height="20">
                                        <font style="font-weight: bold;">姓名：</font><asp:Label ID="labuserName" runat="server" />
                                    </td>
                                    <td width="20%">
                                        <font style="font-weight: bold;">职务：</font><asp:Label ID="labPosition" runat="server" />
                                    </td>
                                    <td width="20%">
                                        <font style="font-weight: bold;">所属团队：</font><asp:Label ID="labGroupName" runat="server" />
                                    </td>
                                    <td width="20%">
                                        <font style="font-weight: bold;">最后工作日期：</font><asp:Label ID="labLastDate" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td colspan="4">
                                        <%-- 业务交接信息 --%>
                                        <table width="100%" class="tableBorder" cellpadding="0" cellspacing="0">
                                            <tr height="50">
                                                <td rowspan="3" class="tdBorder" align="center" style="font-size: 14px;">
                                                    ①业务交接
                                                </td>
                                                <td style="font-weight: bold;" colspan="2" class="tdBorder">
                                                    重要：业务负责人在签字前应确保公司所需信息资料（含电子版）已经备份完毕
                                                </td>
                                            </tr>
                                            <tr height="50">
                                                <td class="tdBorder">
                                                    业务交接情况：<asp:Label ID="labDetailInfo" runat="server" />
                                                </td>
                                                <td width="20%" class="tdBorder">
                                                    交接人：<asp:Label ID="labReceiverName" runat="server" />
                                                </td>
                                            </tr>
                                            <tr height="50">
                                                <td colspan="2" class="tdBorder">
                                                    <table width="100%" border="0" style="margin: 2px 5px 2px 5px;" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td width="50%">
                                                                总监：<asp:Label ID="labDirector" runat="server" />
                                                            </td>
                                                            <td width="50%">
                                                                团队总经理：<asp:Label ID="labManager" runat="server" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <%-- 团队行政、财务、IT、集团行政、集团人力资源信息 --%>
                                        <table width="100%" class="tableBorder" cellpadding="0" cellspacing="0">
                                            <tr style="background-color: Black; color: White;">
                                                <td height="30" width="20%" align="center" class="tdBorder" style="font-size: 14px;">
                                                    ②团队行政
                                                </td>
                                                <td width="30%" align="center" class="tdBorder" style="font-size: 14px;">
                                                    ③财务
                                                </td>
                                                <td width="10%" align="center" class="tdBorder" style="font-size: 14px;">
                                                    ④IT
                                                </td>
                                                <td width="15%" align="center" class="tdBorder" style="font-size: 14px;">
                                                    ⑤集团行政
                                                </td>
                                                <td width="25%" align="center" class="tdBorder" style="font-size: 14px;">
                                                    ⑥集团人力资源
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="tdBorder" valign="top" width="20%">
                                                    <div style="margin: 5px 5px 5px 8px;">
                                                        <font style="font-weight: bolder">考勤（由各团队考勤员填写）</font><br />
                                                        1 / 上月考勤信息：<br />
                                                        <asp:Label ID="labPreMonthStatInfo" runat="server" /><br />
                                                        2 / 当月考勤信息：<br />
                                                        <asp:Label ID="labCurMonthStatInfo" runat="server" /><br />
                                                        3 / 年假余&nbsp;<font style="font-weight: bolder"><asp:Label ID="labAnnual"
                        runat="server" /></font>&nbsp;天； 预支<asp:Label ID="labOverDraft"
                        runat="server" />天年假&nbsp;<font style="font-weight: bolder; color: Red;">其中法定假<asp:Label
                            ID="labOverAnnual" runat="server" />天，福利假<asp:Label
                            ID="labOverReward" runat="server" />天。</font><br />
                                                        负责人：<asp:Label ID="labGroupHRPrincipal1" runat="server" /><br />
                                                        <font style="font-weight: bolder">行政（固定资产）</font><br />
                                                        交还非消耗性办公用品清单，工位<br />
                                                        牌人名标签，推柜钥匙：<br />
                                                        <asp:Label ID="labFixedAssets" runat="server" /><br />
                                                        负责人：<asp:Label ID="labGroupHRPrincipal2" runat="server" />
                                                    </div>
                                                </td>
                                                <td class="tdBorder" valign="top" width="30%">
                                                    <div style="margin: 5px 5px 5px 8px;">
                                                        1 / 借款：<br />
                                                        <asp:Label ID="labLoan" runat="server" /><br />
                                                        出纳：<asp:Label ID="labTellers" runat="server" /><br />
                                                        2 / 商务卡：<br />
                                                        <asp:Label ID="labBusinessCard" runat="server" /><br />
                                                        商务卡审批人：<asp:Label ID="labBusinessCardAudits" runat="server" /><br />
                                                        <br />
                                                        3 / 应收/应付款：<br />
                                                        <asp:Label ID="labAccountsPayable" runat="server" /><br />
                                                        会计：<asp:Label ID="labAccountants" runat="server" /><br />
                                                        4 / 工资信息：<br />
                                                        <asp:Label ID="labSalary" runat="server" /><br />
                                                        5 / 其他说明：<br />
                                                        <asp:Label ID="labOther" runat="server" /><br />
                                                        财务总监：<asp:Label ID="labFinanceDirector" runat="server" /><br />
                                                    </div>
                                                </td>
                                                <td class="tdBorder" valign="top"  width="10%">
                                                    <div style="margin: 5px 5px 5px 8px;">
                                                        1 / 电子邮箱帐户：<br />
                                                        <asp:Label ID="labCompanyEmail" runat="server" /><br />
                                                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButton GroupName="radEmailIsDelete" runat="server"
                                                            ID="radEmailIsDeleteTrue" Text="删除" Checked="true" /><br />
                                                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButton GroupName="radEmailIsDelete" runat="server"
                                                            ID="radEmailIsDeleteFalse" Text="保留期限" />
                                                        &nbsp;&nbsp;<asp:Label ID="labEmailSaveLastDay" runat="server" /><br />
                                                        <%--2 / OA系统帐户：
                                                        <br />
                                                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButton GroupName="radAccountIsDelete" runat="server"
                                                            ID="radAccountIsDeleteTrue" Text="删除" Checked="true" /><br />
                                                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButton GroupName="radAccountIsDelete" runat="server"
                                                            ID="radAccountIsDeleteFalse" Text="保留期限" />
                                                        &nbsp;&nbsp;<asp:Label ID="labAccountSaveLastDay" runat="server" /><br />--%>
                                                        2 / OA设备/自购、自带电脑编号：<asp:Label ID="labOwnPCCode" runat="server" /><br />
                                                        &nbsp;&nbsp;公司电脑编号：<asp:Label ID="labPCCode" runat="server" /><br />
                                                        &nbsp;&nbsp;公司电脑状况（含网线）：<br />
                                                        <asp:Label ID="labPCUsedDes" runat="server" /><br />
                                                        3 / 其他说明：<br />
                                                        <asp:Label ID="labITOther" runat="server" /><br />
                                                        负责人：<asp:Label ID="labITPrincipal" runat="server" /><br />
                                                    </div>
                                                </td>
                                                <td class="tdBorder" valign="top"  width="15%">
                                                    <div style="margin: 5px 5px 5px 8px;">
                                                        1 / 门卡卡号：<asp:Label ID="labDoorCard" runat="server" /><br />
                                                        <br />
                                                        <br />
                                                        <%--2 / 图书管理：<br />
                                                        <asp:Label ID="labLibraryManage" runat="server" />
                                                        <br />
                                                        <br />--%>
                                                        负责人：<asp:Label ID="labADPrincipal" runat="server" /><br />
                                                    </div>
                                                </td>
                                                <td class="tdBorder" valign="top"  width="25%">
                                                    <div style="margin: 5px 5px 5px 8px;">
                                                        1 / 各项保险及公积金：<br />
                                                        社保缴费至：
                                                        <asp:Label ID="labSocialInsY" runat="server" />
                                                        年
                                                        <asp:Label ID="labSocialInsM" runat="server" />
                                                        月
                                                        <br />
                                                        医疗缴费至：
                                                        <asp:Label ID="labMedicalInsY" runat="server" />
                                                        年
                                                        <asp:Label ID="labMedicalInsM" runat="server" />
                                                        月
                                                        <br />
                                                        住房公积金缴费至：
                                                        <asp:Label ID="labCapitalReserveY" runat="server" />
                                                        年
                                                        <asp:Label ID="labCapitalReserveM" runat="server" />
                                                        月
                                                        <br />
                                                        补充医疗缴费至：
                                                        <asp:Label ID="labAddedMedicalIns" runat="server" />
                                                        <%--年
                                                        <asp:Label ID="labAddedMedicalInsM" runat="server" />
                                                        月--%><br />
                                                        <br />
                                                        负责人：<asp:Label ID="labHRPrincipal1" runat="server" /><br />
                                                        1 / 人事档案：
                                                        <br />
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButton GroupName="radIsHaveArchives"
                                                            runat="server" ID="radIsHaveArchivesFalse" Text="无" Checked="true" /><br />
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButton GroupName="radIsHaveArchives"
                                                            runat="server" ID="radIsHaveArchivesTrue" Text="有" /><br />
                                                        &nbsp;&nbsp;须于<asp:Label ID="labTurnAroundDate" runat="server" />前办理完毕调转手续。<br />
                                                        <br />
                                                        负责人：<asp:Label ID="labHRPrincipal2" runat="server" />
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <%-- 个人其他信息 --%>
                                        <table width="100%" class="tableBorder" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td class="tdBorder">
                                                    <table width="100%" border="0" style="margin: 2px 5px 2px 5px;" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td colspan="4" height="20">
                                                                <font style="font-weight: bold;">请您对以上离职手续予以确认，并留下您的联络方式，便于双方互通信息，谢谢！</font><br />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="25%" height="20">
                                                                私人邮箱：<asp:Label ID="labEmail" runat="server" />
                                                            </td>
                                                            <td width="25%">
                                                                移动电话：<asp:Label ID="labMobilePhone" runat="server" />
                                                            </td>
                                                            <td width="25%">
                                                                签名：
                                                            </td>
                                                            <td width="25%">
                                                                办理日期：
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="font-weight: bold;">
                            <div style="margin: 8px 0px 0px 5px;">
                                注：此表完成并经人力资源总监签字后生效，由人力资源部通知财务结算离职人员工资或其他福利。
                            </div>
                        </td>
                        <td style="font-weight: bold;">
                            <div style="margin: 8px 0px 0px 50px;">
                                人力资源总监签字：
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="font-size: 10px; color: Gray" align="center">
                            <br />
                            <br />
                            北京市朝阳区双桥路12号 电子城数字新媒体创新产业园D1楼
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" height="20px">
                            &nbsp;
                        </td>
                        <td align="right">
                            <table border="1" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td width="50" id="btnPrint" runat="server" align="center" valign="bottom" background="../../Images/btnbgimg.gif"
                                        class="white_font">
                                        <a style="cursor: pointer" onclick="javascript:window.print();">打印</a>
                                    </td>
                                    <td width="50" id="btnClose" runat="server" align="center" valign="bottom" background="../../Images/btnbgimg.gif"
                                        class="white_font">
                                        <a style="cursor: pointer" onclick="javascript:window.close();">关闭</a>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
