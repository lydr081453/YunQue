<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Message_PostDetail" Title="留言" ValidateRequest="false" Codebehind="PostDetail.aspx.cs" %>

<%@ Register TagPrefix="cc4" Namespace="MyControls.GridView" Assembly="MyControls" %>
<%--<%@ Register TagPrefix="uc" TagName="Post" Src="skins/Post.ascx"%> 
<%@ Register TagPrefix="uc" TagName="UserView" Src="skins/UserView.ascx"%> --%>
<%@ Register TagPrefix="uc" TagName="Reply" Src="skins/Input.ascx" %>
<%@ Register TagPrefix="uc" TagName="PostItem" Src="skins/PostItem.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentHead" runat="Server">

    <script type="text/javascript">
function returnurl()
{
    var hidurl=document.getElementById("<% = hidUrl.ClientID %>");
    window.location = hidurl.value;
}
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMain" runat="Server">
    <input type="hidden" id="hidUrl" runat="server" />
    <div>
        <table width="100%" border="0" >
            <tr>
                <td colspan="2">
                    <uc:PostItem ID="IssuePost" runat="server"/>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <cc4:MyGridView ID="gvList" runat="server" OnRowDataBound="gvList_RowDataBound"
                        >
                    </cc4:MyGridView>
                </td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td style="width: 70%">
                    <uc:Reply runat="server" ID="newReply" />
                </td>
            </tr>
        </table>
    </div>
    <div style="float: right">
        <input type="button" value="返回" onclick="returnurl();" class="widebuttons" />
    </div>
</asp:Content>
