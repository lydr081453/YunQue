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
                msg += "��������������!" + "\n";
            }
            if (document.getElementById("<% = SupporterInfo.FindControl("txtBeginDate").ClientID%>").value == "") {
                msg += "������ҵ����ʼʱ��!" + "\n";
            }
            if (document.getElementById("<% = SupporterInfo.FindControl("txtEndDate").ClientID%>").value == "") {
                msg += "������ҵ�����ʱ��!" + "\n";
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
                    <asp:HyperLink ID="linkBack" runat="server" NavigateUrl="SupporterList.aspx">����֧�ַ��б�</asp:HyperLink></li>
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
                <asp:Button ID="btnSave" runat="server" Text=" ���� " OnClientClick="return ValidData();"
                    OnClick="btnSave_Click" ToolTip="�������뵥��Ϣ" CssClass="widebuttons" />
                <asp:Button ID="btnSaveAndSubmit" runat="server" Text="���������" OnClientClick="return ValidData();"
                    OnClick="btnSaveAndSubmit_Click" ToolTip="�������뵥��Ϣ�������������ҳ" CssClass="widebuttons" />
                <asp:Button ID="btnReturn" runat="server" Text=" ����  " CssClass="widebuttons" OnClick="btnReturn_Click" />
            </td>
        </tr>
    </table>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowSummary="false"
        ShowMessageBox="true" />
</asp:Content>
