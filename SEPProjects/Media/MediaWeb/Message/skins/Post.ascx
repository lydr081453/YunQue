<%@ Control Language="C#" AutoEventWireup="true" Inherits="Message_skins_Post" Codebehind="Post.ascx.cs" %>
<%--<table border="0" style="width: 100%; height: 304px; background-color: White">
    <tr>
        <td style="width: 100%" class="heading">
            <asp:Literal ID="litSub" runat="server"></asp:Literal>
        </td>
    </tr>
    <tr>
        <td style="width: 100%; height: 209px;" align="left" valign="top">
            <asp:Literal ID="litBody" runat="server"></asp:Literal>
        </td>
    </tr>
    <tr>
        <td colspan="2" style="height: 19px" align="right">
            <asp:LinkButton ID="lbtnEdit" ForeColor="blue" runat="server" OnClick="lbtnEdit_Click">[编辑]</asp:LinkButton>
            <asp:LinkButton ID="lbtnDel" ForeColor="blue" runat="server" OnClick="lbtnDel_Click">[删除]</asp:LinkButton>
        </td>
    </tr>
</table>--%>
<div style="width: 100%; height: 304px; background-color: White; word-wrap: break-word;
    word-break: break-all">
    <asp:Literal ID="litSub" runat="server"></asp:Literal>
    <br />
    <asp:Literal ID="litBody" runat="server"></asp:Literal>
    <br />
</div>
<table width="100%">
<tr>
<td align="right">
<asp:LinkButton ID="lbtnEdit" ForeColor="blue" runat="server" OnClick="lbtnEdit_Click">[编辑]</asp:LinkButton>
<asp:LinkButton ID="lbtnDel" ForeColor="blue" runat="server" OnClick="lbtnDel_Click">[删除]</asp:LinkButton>
</td>
</tr>
</table>