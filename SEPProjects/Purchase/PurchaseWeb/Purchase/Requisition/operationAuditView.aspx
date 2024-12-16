<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Purchase_Requisition_operationAuditView" Title="查看业务审批流程" CodeBehind="operationAuditView.aspx.cs" %>

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
        .title {
            font-size: 24px;
            color: #15428b;
            font-weight: bold;
            border: 1px solid #b2cced;
        }

        .nav {
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

        .centerline {
            border-left-width: 1px;
            border-left-style: solid;
            border-left-color: #B2CCED;
        }
    </style>
    <table width="100%" border="0" cellpadding="0" cellspacing="0" background="/images/v1_03.gif"
        class="title" style="background-repeat: repeat-x">
        <tr>
            <td width="20%" align="center">业务审批中心
            </td>
            <td width="5%">
                <img src="/images/v1_05.gif" width="26" height="59" />
            </td>
            <td width="20%" align="center">风控审批中心
            </td>
            <td width="5%">
                <img src="/images/v1_05.gif" width="26" height="59" />
            </td>
            <td width="20%" align="center">采购审批审批中心
            </td>
            <td width="5%">
                <img src="/images/v1_05.gif" width="26" height="59" />
            </td>
            <td width="20%" align="center">业务附加审批中心
            </td>
        </tr>
    </table>
    <table width="100%" border="0" cellpadding="0" cellspacing="10" class="nav">
        <tr>
            <td width="20%" align="center">
                <table width="90%" border="0" cellspacing="0" cellpadding="0" style="margin: 0 15px 0 15px;">
                    <asp:Literal runat="server" ID="litBiz"></asp:Literal>
                </table>
            </td>
            <td width="5%" class="centerline">&nbsp;</td>
            <td width="20%" align="center">
                <table width="90%" border="0" cellspacing="0" cellpadding="0" style="margin: 0 15px 0 15px;">
                    <asp:Literal runat="server" ID="litRisk"></asp:Literal>
                </table>
            </td>
            <td width="5%" class="centerline">&nbsp;</td>
            <td width="20%" align="center">
                <table width="90%" border="0" cellspacing="0" cellpadding="0" style="margin: 0 15px 0 15px;">
                    <asp:Literal runat="server" ID="litContract"></asp:Literal>
                </table>
            </td>
            <td width="5%" class="centerline">&nbsp;</td>
            <td width="20%" align="center">
                <table width="90%" border="0" cellspacing="0" cellpadding="0" style="margin: 0 15px 0 15px;">
                    <asp:Literal runat="server" ID="litFinance"></asp:Literal>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

