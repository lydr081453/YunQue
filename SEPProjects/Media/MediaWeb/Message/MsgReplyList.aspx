<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Message_MsgReplyList" Title="�����б�" Codebehind="MsgReplyList.aspx.cs" %>

<%@ Register TagPrefix="cc4" Namespace="MyControls.GridView" Assembly="MyControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMain" runat="Server">
        <table width="100%" border="0">
            <tr valign="top" style="display:none">
                <td style="width: 75%" align="left" class="headinglist">
                    ����
                </td>
            </tr>
            <tr style="display:none">
                <td align="center">
                    <cc4:MyGridView ID="dgAdmin" runat="server" Width="100%" OnRowDataBound="dgAdmin_ItemDataBound">
                    </cc4:MyGridView>
                </td>
            </tr>
            <tr style="display:none">
                <td style="height:30px"></td>
            </tr>
            <tr>
                <td align="left" class="headinglist">
                    ý������
                </td>
            </tr>
            <tr>
                <td align="center">
                    <cc4:MyGridView ID="dgPost" runat="server" Width="100%" OnRowDataBound="dgPost_ItemDataBound">
                    </cc4:MyGridView>
                </td>
            </tr>
        </table>
</asp:Content>
