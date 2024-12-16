<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" EnableEventValidation="false" Inherits="Message_ReleasePosts" Title="留言列表" Codebehind="ReleasePosts.aspx.cs" %>
<%@ Register TagPrefix="uc" TagName="Issue" Src="skins/Input.ascx" %>
<%@ Register TagPrefix="cc4" Namespace="MyControls.GridView" Assembly="MyControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMain" runat="Server">
    <div>
        <table style="width: 100%">
            <tr>
                <td style="width: 100%;">
                    <uc:Issue ID="newPost" runat="server" />
                </td>
            </tr>
      
        </table>
    </div>
        <table width="100%" border="0">
            <tr valign="top">
                <td>
                    <table width="100%">
                        <tr>
                            <td  class="headinglist">
                                公 告
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
        </table>

</asp:Content>
