<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Message_PostList" Title="留言列表" Codebehind="PostList.aspx.cs" %>

<%@ Register TagPrefix="cc4" Namespace="MyControls.GridView" Assembly="MyControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMain" runat="Server">
    <div>
        <table width="100%" border="0">
            <tr>
                <td>
                    <table class="blogheading">
                        <tr>
                            <td align="left">
                                帖  子 <asp:TextBox ID="txtKey" runat="server"></asp:TextBox><asp:Button ID="btnSearch" runat="server" Text="查询" OnClick="btnSearch_Click"/>
                            </td>
                            <td align="right">
                                <img src="/images/add.gif" border="0" style="vertical-align: bottom" />&nbsp;<asp:LinkButton
                                    ID="lbtnNewGong" runat="server" class="bigfont" Text="发布新帖" OnClick="lbtnNewGong_Click" />
                                <asp:Label ID="lagMsgGong" runat="server" ForeColor="red" Visible="false"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <cc4:MyGridView ID="dgPost" runat="server" Width="100%" OnRowDataBound="dgPost_ItemDataBound">
                    </cc4:MyGridView>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
