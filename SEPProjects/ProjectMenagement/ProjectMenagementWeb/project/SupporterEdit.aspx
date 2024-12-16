<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    Inherits="project_SupporterEdit" CodeBehind="SupporterEdit.aspx.cs" %>

<%@ Register Src="../UserControls/Project/SupporterInfo.ascx" TagName="SupporterInfo"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <%@ Register Src="../UserControls/Project/TopMessage.ascx" TagName="TopMessage" TagPrefix="uc1" %>

    <script language="javascript" type="text/javascript">

        function ValidData() {
            var msg = "";

            if (document.getElementById("<% = SupporterInfo.FindControl("txtIncomeFee").ClientID%>").value == "") {
                msg += "请输入服务费收入!" + "\n";
            }
            if (document.getElementById("<% = SupporterInfo.FindControl("txtBeginDate").ClientID%>").value == "") {
                msg += "请输入业务起始时间!" + "\n";
            }
            if (document.getElementById("<% = SupporterInfo.FindControl("txtEndDate").ClientID%>").value == "") {
                msg += "请输入业务结束时间!" + "\n";
            }
            if (msg == "")
                return true;
            else {
                alert(msg);
                return false;
            }
        }
    </script>

    <uc1:TopMessage ID="TopMessage" runat="server" IsEditPage="true" />
    <table style="width: 100%">
        <tr>
            <td colspan="2">
                <li>
                    <asp:HyperLink ID="linkBack" runat="server" NavigateUrl="SupporterList.aspx">返回支持方列表</asp:HyperLink></li>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px; padding-top: 4px;">
                <uc1:SupporterInfo ID="SupporterInfo" runat="server" />
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td align="center">
                <asp:Button ID="btnSave" runat="server" Text=" 保存 " OnClientClick="return ValidData();"
                    OnClick="btnSave_Click" ToolTip="保存申请单信息" CssClass="widebuttons" />
                <asp:Button ID="btnSaveAndSubmit" runat="server" Text="设置审核人" OnClientClick="return ValidData();"
                    OnClick="btnSaveAndSubmit_Click" ToolTip="保存申请单信息并进入审核设置页" CssClass="widebuttons" />
                <asp:Button ID="btnReturn" runat="server" Text=" 返回  " CssClass="widebuttons" OnClick="btnReturn_Click" />
            </td>
        </tr>
    </table>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowSummary="false"
        ShowMessageBox="true" />
</asp:Content>
