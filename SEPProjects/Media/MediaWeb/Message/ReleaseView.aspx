<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" MasterPageFile="~/MasterPage.master"
    Inherits="Message_ReleaseView" Codebehind="ReleaseView.aspx.cs" %>
<%@ Register TagPrefix="uc" TagName="Issue" Src="skins/Input.ascx" %>
<%@ Register TagPrefix="cc4" Namespace="MyControls.GridView" Assembly="MyControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMain" runat="Server">
    <div>
        <table style="width: 100%">
            <tr>
                <td style="width: 100%;">
                    <uc:issue id="newPost" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <table width="100%">
        <tr>
            <td class="headinglist">
                媒介视线
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <cc4:MyGridView ID="dgPost" runat="server" Width="100%" OnRowDataBound="dgPost_ItemDataBound">
                </cc4:MyGridView>
            </td>
        </tr>
    </table>
</asp:Content>
