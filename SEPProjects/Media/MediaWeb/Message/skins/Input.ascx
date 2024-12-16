<%@ Control Language="C#" AutoEventWireup="true" Inherits="Message_skins_Input" Codebehind="Input.ascx.cs" %>
<%@ Register TagPrefix="ucWE" Namespace="MyControls.MyEditor" Assembly="MyControls" %>
<%@ Register TagPrefix="cc2" Namespace="MyControls" Assembly="MyControls" %>
<script type="text/javascript">
function check()
{
return true;
}
</script>

<table border="1" width="100%" class="tableForm">
    <tr>
        <td colspan="4">
            主题：<asp:TextBox ID="txtSubject" runat="server"></asp:TextBox><asp:Label ID="labMsg"
                runat="server" ForeColor="red" Visible="false"></asp:Label>
        </td>
    </tr>
     <asp:Panel runat="server" ID="pTime" Visible="false">
    <tr>
        <td colspan="4">有效时间段：</td>
    </tr>
    <tr>
    <td style="width:20%">
    起始时间：
    </td>
        <td style="width:30%">
    <cc2:DatePicker ID="dpStart" runat="server"></cc2:DatePicker>
    </td>
        <td style="width:20%">
    结束时间：
    </td>
        <td style="width:30%">
    <cc2:DatePicker ID="dpEnd" runat="server"></cc2:DatePicker>
    </td>
    </tr>
    </asp:Panel>
    <tr>
        <td colspan="4">
            <ucWE:WebTextPane ID="wtpNew" runat="server" Width="100%" Height="240px" BackColor="white"></ucWE:WebTextPane>
        </td>
    </tr>
    <tr>
        <td colspan="4" align="right">
            <asp:LinkButton ID="lbtnEdit" CssClass="Abutton" ForeColor="blue" runat="server"
                OnClick="lbtnEdit_Click"><span>[确定]</span></asp:LinkButton>
            <asp:LinkButton ID="lbtnDel" CssClass="Abutton" ForeColor="blue" runat="server" OnClick="lbtnDel_Click"><span>[取消]</span></asp:LinkButton>
        </td>
    </tr>
</table>
