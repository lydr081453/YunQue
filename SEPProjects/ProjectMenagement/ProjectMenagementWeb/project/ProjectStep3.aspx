<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" EnableEventValidation="false" Inherits="project_ProjectStep3" CodeBehind="ProjectStep3.aspx.cs" %>

<%@ Register Src="/UserControls/Project/CustomerDisplay.ascx" TagName="CustomerDisplay"
    TagPrefix="uc1" %>
<%@ Register Src="/UserControls/Project/PrepareDisplay.ascx" TagName="PrepareDisplay"
    TagPrefix="uc1" %>
<%@ Register Src="/UserControls/Project/ProjectMemberDisplay.ascx" TagName="ProjectMemberDisplay"
    TagPrefix="uc1" %>
<%@ Register Src="/UserControls/Project/ProjectInfo.ascx" TagName="ProjectInfo" TagPrefix="uc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            $(window).scrollTop($(document).height());
        });
        function testNum(a) {
            a += "";
            a = a.replace(/(^[\\s]*)|([\\s]*$)/g, "");
            if (a != "" && !isNaN(a) && Number(a) >= 0) {
                return true;
            }
            else {
                return false;
            }
        }
        function checkValid() {
            var msg = "";
            if (document.getElementById("<% = ProjectInfo.FindControl("txtBranchName").ClientID%>").value == "" || document.getElementById("<% = ProjectInfo.FindControl("hidBranchID").ClientID%>").value == "") {
                msg += "请选择公司." + "\n";
            }
            if (document.getElementById("<% = ProjectInfo.FindControl("txtBeginDate").ClientID%>").value == "") {
                msg += "请选择业务起始日期." + "\n";
            }
            if (document.getElementById("<% = ProjectInfo.FindControl("txtEndDate").ClientID%>").value == "") {
                msg += "请选择业务结束日期." + "\n";
            }
            var total = document.getElementById("<% = ProjectInfo.FindControl("txtTotalAmount").ClientID%>").value.replace(/,/g, '');
            if (!testNum(total)) {
                msg += "项目总金额输入错误." + "\n";
            }

            if (msg == "") {
                return true;
            }
            else {
                alert(msg);
                return false;
            }
        }
    </script>
    <table style="width: 100%">
        <tr>
            <td width="100%" align="center">
                <img id="imgtitle" src="/images/l_03.jpg" />
            </td>
        </tr>
        <tr>
            <td style="height: 15px"></td>
        </tr>
        <tr>
            <td colspan="2">
                <li>
                    <asp:HyperLink ID="linkBack" runat="server" NavigateUrl="ProjectList.aspx">返回项目号申请单列表</asp:HyperLink></li>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px; padding-top: 4px;">
                <uc1:PrepareDisplay ID="PrepareDisplay" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px; padding-top: 4px;">
                <uc1:ProjectMemberDisplay ID="ProjectMemberDisplay" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px; padding-top: 4px;">
                <uc1:CustomerDisplay ID="CustomerDisplay" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px; padding-top: 4px;">
                <uc1:ProjectInfo ID="ProjectInfo" runat="server" />
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td align="center">
                <asp:Button ID="btnSave" runat="server" Text="  保存 " CausesValidation="true"
                    OnClick="btnSave_Click" CssClass="widebuttons" />&nbsp;
                <asp:Button ID="btnPre" Text="上一步" CssClass="widebuttons" OnClick="btnPre_Click"
                    runat="server" CausesValidation="false" />&nbsp;
                <asp:Button ID="btnNext" Text="下一步" CssClass="widebuttons" CausesValidation="true"
                    OnClick="btnNext_Click" runat="server" />
                &nbsp;
                     <asp:Button ID="btnReturn" Text=" 返回 " CssClass="widebuttons" OnClick="btnReturn_Click" CausesValidation="false" runat="server" />
            </td>
        </tr>
    </table>
    <asp:ValidationSummary runat="server" ID="ValidationSummary" ShowSummary="false"
        ShowMessageBox="true" DisplayMode="bulletList" />
</asp:Content>
