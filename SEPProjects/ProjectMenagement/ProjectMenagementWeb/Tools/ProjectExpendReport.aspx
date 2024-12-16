<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="ProjectExpendReport.aspx.cs" Inherits="FinanceWeb.Tools.ProjectExpendReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script language="javascript" src="../../public/js/DatePicker.js"></script>
    <script language="javascript" type="text/javascript">
            function setDate(obj) {
            popUpCalendar(obj, obj, 'yyyy-mm-dd');
            }
        </script>
        <table class="tableForm" width="100%">
        <tr >
            <td class="heading" colspan="4">项目支出</td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 20%;">项目关闭日期：</td>
            <td class="oddrow-l" style="width: 30%;">
                <asp:TextBox ID="txtBegin" runat="server" Text="2024-01-01" onclick="setDate(this);"/>-<asp:TextBox ID="txtEnd" runat="server"  Text="2024-12-01" onclick="setDate(this);"/><font style="color:red">*</font>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="" Display="None" ControlToValidate="txtBegin"></asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="" Display="None" ControlToValidate="txtEnd"></asp:RequiredFieldValidator>
            </td>
           </tr>
                    <tr>
            <td colspan="4" class="oddrow-l">
                <asp:Button ID="btnSearch" CssClass="widebuttons" runat="server" Text="导出" OnClientClick="if(Page_ClientValidate()){showLoading();}else {return false;} " OnClick="btnSearch_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
