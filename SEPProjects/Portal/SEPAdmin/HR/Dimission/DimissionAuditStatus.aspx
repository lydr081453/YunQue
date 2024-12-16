<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DimissionAuditStatus.aspx.cs"
    Inherits="DimissionAuditStatus" MasterPageFile="~/MasterPage.Master" Title="离职单状态查看" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="/public/js/jquery.js"></script>

    <script type="text/javascript" src="/public/js/jquery.blockUI.js"></script>

    <script type="text/javascript">
        function ShowMsg(script) {
            jQuery.blockUI({
                message: script
            });
        }
    </script>

    <style type="text/css">
        .title
        {
            font-size: 24px;
            color: #15428b;
            font-weight: bold;
            border: 1px solid #b2cced;
        }
        .nav
        {
            border-right-width: 1px;
            border-bottom-width: 1px;
            border-left-width: 1px;
            border-right-style: solid;
            border-bottom-style: solid;
            border-left-style: solid;
            border-right-color: #b2cced;
            border-bottom-color: #b2cced;
            border-left-color: #b2cced;
            font-size: 18px;
            color: #595959;
            background-color: #eef6ff;
        }
        .centerline
        {
            border-left-width: 1px;
            border-left-style: solid;
            border-left-color: #B2CCED;
        }
    </style>
    <table width="100%" border="0" cellpadding="0" cellspacing="0" background="../../Images/v1_03.gif"
        class="title" style="background-repeat: repeat-x">
        <tr>
            <td width="15%" align="center">
                业务审批
            </td>
            <td width="2%">
                <img src="../../Images/v1_05.gif" width="26" height="59" />
            </td>
            <td width="15%" align="center">
                行政审批
            </td>
            <td width="2%">
                <img src="../../Images/v1_05.gif" width="26" height="59" />
            </td>
            <td width="15%" align="center">
                人力资源审批
            </td>
            <td width="2%">
                <img src="../../Images/v1_05.gif" width="26" height="59" />
            </td>
            <td width="15%" align="center">
                财务审批
            </td>
            <td width="2%">
                <img src="../../Images/v1_05.gif" width="26" height="59" />
            </td>
            <td width="15%" align="center">
                HR总监审批
            </td>
        </tr>
    </table>
    <table width="100%" border="0" cellpadding="0" cellspacing="10" class="nav">
        <tr>
            <td width="15%" align="center">
                <table width="90%" border="0" cellspacing="0" cellpadding="0" style="margin: 0 15px 0 15px;">
                    <asp:Literal runat="server" ID="litBiz"></asp:Literal>
                </table>
            </td>
            <td width="2%" class="centerline">
                &nbsp;
            </td>
            <td width="15%" align="center">
                <table width="90%" border="0" cellspacing="0" cellpadding="0" style="margin: 0 15px 0 15px;">
                    <asp:Literal runat="server" ID="litGroup"></asp:Literal>
                </table>
            </td>
            <td width="2%" class="centerline">
                &nbsp;
            </td>
            <td width="15%" align="center">
                <table width="90%" border="0" cellspacing="0" cellpadding="0" style="margin: 0 15px 0 15px;">
                    <asp:Literal runat="server" ID="litHRIT"></asp:Literal>
                </table>
            </td>
            <td width="2%" class="centerline">
                &nbsp;
            </td>
            <td width="15%" align="center">
                <table width="90%" border="0" cellspacing="0" cellpadding="0" style="margin: 0 15px 0 15px;">
                    <asp:Literal runat="server" ID="litFinance"></asp:Literal>
                </table>
            </td>
            <td width="2%" class="centerline">
                &nbsp;
            </td>
            <td width="15%" align="center">
                <table width="90%" border="0" cellspacing="0" cellpadding="0" style="margin: 0 15px 0 15px;">
                    <asp:Literal runat="server" ID="litAD"></asp:Literal>
                </table>
            </td>
        </tr>
    </table>
    <br />
    <br />
    <table width="100%" border="0" cellpadding="0" cellspacing="10">
        <tr>
            <td>
                <img src="../../Images/WF_Waiting.gif" width="10" height="10" />
            </td>
            <td>
                <font face="微软雅黑" size="2">未审批。</font>
            </td>
        </tr>
        <tr>
            <td>
                <img src="../../Images/WF_Reject.gif" width="10" height="10" />
            </td>
            <td>
                <font face="微软雅黑" size="2">审批驳回,请重新编辑提交。</font>
            </td>
        </tr>
        <tr>
            <td>
                <img src="../../Images/WF_Pass.gif" width="10" height="10" />
            </td>
            <td>
                <font face="微软雅黑" size="2">审批通过,请继续相关业务操作。</font>
            </td>
        </tr>
    </table>
</asp:Content>
