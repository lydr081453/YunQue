<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Message_NewPost" Title="发新帖" ValidateRequest="false" Codebehind="NewPost.aspx.cs" %>

<%@ Register TagPrefix="ucDG" Namespace="MyControls.DataGrid" Assembly="MyControls" %>
<%@ Register TagPrefix="uc" TagName="Post" Src="skins/Post.ascx" %>
<%@ Register TagPrefix="uc" TagName="Issue" Src="skins/Input.ascx" %>
<%@ Register TagPrefix="uc" TagName="UserView" Src="skins/UserView.ascx" %>
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
</asp:Content>
