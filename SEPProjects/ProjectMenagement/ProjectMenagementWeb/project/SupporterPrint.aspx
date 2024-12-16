<%@ Page Language="C#" AutoEventWireup="true" Inherits="project_SupporterPrint" Codebehind="SupporterPrint.aspx.cs" %>

<html>
<head runat="server">
    <title>支持方打印预览</title>
    <style type="text/css" media="print">
        .noprint
        {
            display: none;
        }
    </style>
    <style type="text/css">
        body
        {
            margin: 0px;
        }
        img
        {
            border: none;
        }
        .nav
        {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 12px;
        }
        .nav table
        {
            border-top-width: 1px;
            border-left-width: 1px;
            border-top-style: solid;
            border-left-style: solid;
            border-top-color: #999999;
            border-left-color: #999999;
        }
        .nav td
        {
            border-right-width: 1px;
            border-bottom-width: 1px;
            border-right-style: solid;
            border-bottom-style: solid;
            border-right-color: #999999;
            border-bottom-color: #999999;
            padding: 0 0 0 5px;
            color: #333333;
            line-height: 150%;
        }
        .nav em
        {
            font-style: normal;
            font-size: 14px;
            color: #CC6633;
            font-weight: bold;
        }
        .topline
        {
            border-top-width: 2px;
            border-top-style: solid;
            border-top-color: #999999;
        }
    </style>
</head>
<body>
    <table width="750" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <table width="100%" border="0" cellspacing="0" cellpadding="0" style="margin: 40px 0 10px 0;">
                    <tr>
                        <td width="50%" style="font-weight:bolder;font-size:large;">
                            项目号申请
                        </td>
                        <td width="50%" align="right" valign="top">
                            <img src="/images/xingyan.png" width="63" height="35" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center" style="padding: 10px 0 0 0;">
                            <img src="/images/print_img/title_03.gif" width="446" height="42" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td valign="top" class="nav" style="padding: 10px 0 10px 0;">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="25" colspan="2">
                            <strong>Project supporter/项目支持方（办公室名称）/组别</strong>
                        </td>
                        <td colspan="2">
                            <asp:Label ID="lblGroupName" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" colspan="2">
                            <strong>Job number supported/所支持之项目号</strong>
                        </td>
                        <td colspan="2">
                            <asp:Label ID="lblProjectCode" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" colspan="2">
                            <p>
                                <strong>Service Type/服务类型（应与所支持之项目一致）</strong></p>
                        </td>
                        <td colspan="2">
                            <asp:Label ID="lblServiceType" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="100" colspan="2">
                            <strong>Service description/业务描述（包括活动主题）</strong>
                        </td>
                        <td colspan="2">
                            <asp:Label ID="lblBizDesc" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" colspan="2">
                            <strong>Group project leader/项目经理</strong>
                        </td>
                        <td colspan="2">
                            <asp:Label ID="lblLeader" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" colspan="2">
                            <strong>Budget allocated/业务总额</strong>
                        </td>
                        <td colspan="2">
                            <asp:Label ID="lblBudget" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" colspan="2">
                            <strong>不含增值税金额</strong>
                        </td>
                        <td colspan="2">
                            <asp:Label ID="lblBudgetNoVAT" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" colspan="2">
                            <strong>附加税</strong>
                        </td>
                        <td colspan="2">
                            <asp:Label ID="lblTaxVAT" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" colspan="2">
                            <strong>Project completed percentage in advance/<br />
                                预计完成百分比</strong>
                        </td>
                        <td colspan="2">
                            <asp:Label ID="lblPercent" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" colspan="2">
                            <strong>Supporter fee income/服务费收入</strong>
                        </td>
                        <td colspan="2">
                            <asp:Label ID="lblFee" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" colspan="2">
                            <strong>Billed tax/由客户支付之税金</strong>
                        </td>
                        <td colspan="2">
                            <asp:Label ID="lblTax" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td width="25%" height="25">
                            <strong>Requested by/申请人</strong>
                        </td>
                        <td width="25%">
                            &nbsp;
                        </td>
                        <td width="25%">
                            <strong>Date of request/申请日期</strong>
                        </td>
                        <td width="25%">
                            <asp:Label ID="lblAppDate" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <br />
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="30" colspan="5">
                            <em>项目组成员</em>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%" height="25" align="center">
                            <strong>真实姓名</strong>
                        </td>
                        <td width="20%" align="center">
                            <strong>成员编号</strong>
                        </td>
                        <td width="20%" align="center">
                            <strong>成员账号</strong>
                        </td>
                        <td width="20%" align="center">
                            <strong>邮箱</strong>
                        </td>
                        <td width="20%" align="center">
                            <strong>电话</strong>
                        </td>
                    </tr>
                    <asp:Literal runat="server" ID="ltPrjMem"></asp:Literal>
                </table>
                <br />
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="30" colspan="4">
                            <em>成本明细</em>
                        </td>
                    </tr>
                    <tr>
                        <td width="25%" height="25" align="center">
                            <strong>成本描述</strong>
                        </td>
                        <td width="25%" align="center">
                            <strong>成本金额</strong>
                        </td>
                        <td width="50%" align="center" colspan="2">
                            <strong>备注</strong>
                        </td>
                    </tr>
                    <asp:Literal runat="server" ID="ltContractDetail"></asp:Literal>
                    <tr>
                        <td colspan="4">
                            <em>审批记录</em>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" width="100%" align="Left">
                            <asp:Label ID="lblLog" runat="server" Font-Size="XX-Small"></asp:Label>
                        </td>
                    </tr>
                </table>
                <br />
                <p>
                    <br />
                    - The form shall be filled out by the project Supporter.<br />
                    &nbsp;&nbsp;本表由项目支持方填写。<br />
                    <br />
                    - The form is taken as effective when it has no conflict with the corresponding
                    Owner
                    <br />
                    &nbsp;&nbsp;Version submitted by the project owner.<br />
                    &nbsp;&nbsp;本表内容须与项目主申请方提供内容相符，方能生效。<br />
                    <br />
                    - The form is updated on January 10,2005 and is subject to any necessary change.<br />
                    &nbsp;&nbsp;本申请表于2006年1月10日更新，并将根据需要随时改动。</p>
            </td>
        </tr>
    </table>
    <table width="750" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr class="noprint">
            <td height="25" align="right">
                &nbsp;
            </td>
            <td width="1%" align="right">
                <a href="#" style="cursor: pointer">
                    <img src="/images/print_img/1_11.gif" width="50" height="20" hspace="1" vspace="5"
                        onclick="window.print();" /></a>
            </td>
            <td width="1%" align="right">
                <a href="#" style="cursor: pointer">
                    <img src="/images/print_img/1_13.gif" width="50" height="20" vspace="5" onclick="window.close();" /></a>
            </td>
        </tr>
    </table>
</body>
</html>
