<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" EnableEventValidation="false" Inherits="Message_MsgList" Title="留言列表" Codebehind="MsgList.aspx.cs" %>

<%@ Register TagPrefix="cc4" Namespace="MyControls.GridView" Assembly="MyControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMain" runat="Server">
    <div>
        <table width="100%" border="0">
            <tr valign="top">
                <td>
                    <table width="100%">
                        <tr>
                            <td  class="headinglist">
                                公 告
                            </td>
                            <td align="right">                            
                                <img src="/images/add.gif" border="0" style="vertical-align: bottom" />&nbsp;<asp:LinkButton
                                    ID="lbtnNewGong" runat="server" class="bigfont" Text="发布公告" OnClick="lbtnNewGong_Click" />
                                <asp:Label ID="lagMsgGong" runat="server" ForeColor="red" Visible="false"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <cc4:MyGridView ID="dgAdmin" runat="server" Width="100%" OnRowDataBound="dgAdmin_ItemDataBound">
                    </cc4:MyGridView>
                </td>
            </tr>
            <tr>
                <td style="height: 30px">
                </td>
            </tr>
            <tr>
                <td>
                    <table width="100%">
                        <tr>
                            <td class="headinglist">
                                媒介视线
                            </td>
                            <td align="right">
                                <img src="/images/add.gif" border="0" style="vertical-align: bottom" />&nbsp;<asp:LinkButton
                                    ID="lbtnNew" runat="server" class="bigfont" Text="发布博客" OnClick="lbtnNew_Click" />
                                <asp:Label ID="labMsg" runat="server" ForeColor="red" Visible="false"></asp:Label>
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
