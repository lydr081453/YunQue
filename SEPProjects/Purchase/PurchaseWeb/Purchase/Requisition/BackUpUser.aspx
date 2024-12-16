<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Purchase_BackUpUser" Codebehind="BackUpUser.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script language="javascript" src="../../public/js/DatePicker.js"></script>
    <script language="javascript">
        function EmplyeeClick(ctl) {
            var win = window.open('EmployeeList.aspx?type=backup&ctl='+ctl, null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
            win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
        }
        function removefocus() {
            document.getElementById("<%=btnSave.ClientID %>").focus();
        }
    </script>
    
<table width="100%" class="tableForm">
    <tr>
        <td class="heading">指定备用初审人</td>
    </tr>
    <tr>
        <td class="oddrow">原用户：</td>
        <td class="oddrow-l" colspan="3"><asp:TextBox ID="txtOldUser" Enabled="false" Width="100px" runat="server" /><asp:HiddenField ID="hidOldUser" runat="server" /><font color="red"> * </font><asp:Button ID="btnSel" runat="server" Text="选择..." CssClass="widebuttons" OnClientClick="EmplyeeClick('OldUser');return false;" /></td>
    </tr>
    <tr>
        <td class="oddrow" style="width:20%">代理用户：</td>
        <td class="oddrow-l" style="width:30%"><asp:TextBox ID="txtBackUpUser" Enabled="false" Width="100px" runat="server" /><asp:HiddenField ID="hidBackUpUser" runat="server" /><font color="red"> * </font><asp:Button ID="btnSelect" runat="server" Text="选择..." CssClass="widebuttons" OnClientClick="EmplyeeClick('BackUpUser');return false;" /></td>
        <td class="oddrow" style="width:20%">代审时间段：</td>
        <td class="oddrow-l" style="width:30%"><asp:TextBox ID="txtBegin" onfocus="removefocus();" Width="80px" runat="server" />&nbsp;<img src="../../images/dynCalendar.gif"
                                border="0" onclick="popUpCalendar(document.getElementById('<%= txtBegin.ClientID %>'), document.getElementById('<%= txtBegin.ClientID %>'), 'yyyy-mm-dd');" /> - <asp:TextBox ID="txtEnd" Width="80px" onfocus="removefocus();" runat="server" />&nbsp;<img src="../../images/dynCalendar.gif"
                                border="0" onclick="popUpCalendar(document.getElementById('<%= txtEnd.ClientID %>'), document.getElementById('<%= txtEnd.ClientID %>'), 'yyyy-mm-dd');" /><font color="red"> * </font></td>
    </tr>
    <tr>
        <td><asp:Button ID="btnSave" runat="server" Text="保存" CssClass="widebuttons" OnClick="btnSave_Click" /><asp:RequiredFieldValidator
                ID="RequiredFieldValidator1" runat="server" Display="None" ControlToValidate="txtBackUpUser" ErrorMessage="请选择备用初审人"></asp:RequiredFieldValidator><asp:RequiredFieldValidator
                    ID="RequiredFieldValidator2" runat="server" Display="None" ControlToValidate="txtBegin" ErrorMessage="请选择起始时间"></asp:RequiredFieldValidator><asp:RequiredFieldValidator
                    ID="RequiredFieldValidator3" runat="server" Display="None" ControlToValidate="txtEnd" ErrorMessage="请选择结束时间"></asp:RequiredFieldValidator><asp:ValidationSummary
                        ID="ValidationSummary1" DisplayMode="BulletList" ShowMessageBox="true" ShowSummary="false" runat="server" />
        </td>
    </tr>
</table>
</asp:Content>

