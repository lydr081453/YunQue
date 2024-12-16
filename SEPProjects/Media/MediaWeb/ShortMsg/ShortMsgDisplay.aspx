<%@ Page Language="C#" AutoEventWireup="true"
    MasterPageFile="~/MasterPage.master" Inherits="ShortMsg_ShortMsgDisplay" Codebehind="ShortMsgDisplay.aspx.cs" %>

<asp:Content ID="cntHead" ContentPlaceHolderID="contentHead" runat="server">
<script type="text/javascript">
function PrintPage()
  {  
    window.print();
  }
</script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="contentMain" runat="Server">
    <table style="width: 100%;" border="1" class="tableForm">
        <tr>
            <td colspan="4" class="heading">
                <asp:Label ID="labHeading" runat="server"> 显示短消息</asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 20%;" class="oddrow">
                主题：
            </td>
            <td colspan="3" class="oddrow-l">
                <asp:Label ID="labSubject" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                内容：
            </td>
            <td colspan="3" class="oddrow-l">
                <asp:Label ID="labBody" runat="server" Width="80%" Height="98px"></asp:Label>
            </td>
        </tr>
    </table>
    <table style="width:100%" border="0">
        <tr>
            <td align="right" colspan="4">
                <asp:Button ID="Button1" runat="server" OnClientClick="PrintPage();" CssClass="widebuttons" Text="打印" />&nbsp;  
                <asp:Button ID="btnBack" Text="返回" OnClick="btnBack_Click" runat="server" CssClass="widebuttons"></asp:Button>
            </td>
        </tr>
    </table>
</asp:Content>
