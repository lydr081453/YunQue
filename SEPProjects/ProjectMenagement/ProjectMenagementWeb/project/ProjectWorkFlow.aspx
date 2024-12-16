<%@ Page Language="C#" AutoEventWireup="true" Inherits="project_ProjectWorkFlow"
    EnableViewState="false" CodeBehind="ProjectWorkFlow.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

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
</head>
<body>
    <form id="form1" runat="server">
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
                <td width="20%" align="center">合同审批中心
                </td>
                <td width="5%">
                    <img src="/images/v1_05.gif" width="26" height="59" />
                </td>
                <td width="20%" align="center">财务审批中心
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
                        <asp:Literal runat="server" ID="litControl"></asp:Literal>
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
        <br />
        <br />
        <table width="100%" border="0" cellpadding="0" cellspacing="10">
            <tr>
                <td>
                    <img src="/images/WF_Waiting.gif" width="10" height="10" /></td>
                <td><font face="微软雅黑" size="2">未审批,点击图标可查看该审核人信息，方便联系审核人尽快审核。</font></td>
            </tr>
            <tr>
                <td>
                    <img src="/images/WF_Reject.gif" width="10" height="10" /></td>
                <td><font face="微软雅黑" size="2">审批驳回,请重新编辑提交。</font></td>
            </tr>
            <tr>
                <td>
                    <img src="/images/WF_Pass.gif" width="10" height="10" /></td>
                <td><font face="微软雅黑" size="2">审批通过,请继续相关业务操作。</font></td>
            </tr>
            <tr>
                <td>
                    <img src="/images/WF_Contract.gif" width="10" height="10" /></td>
                <td><font face="微软雅黑" size="2">等待合同,请在项目变更中重新提交合同附件；提交后，申请单将直接流转到合同审核人处。</font></td>
            </tr>
        </table>

    </form>
</body>
</html>
