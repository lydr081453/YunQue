<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Media_NewMedia" Title="添加新媒体" Codebehind="NewMedia.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="cntHead" ContentPlaceHolderID="contentHead" runat="server">
    
    <script type="text/javascript" src="/public/js/jquery-1.2.3.pack.js"></script>

    <script type="text/javascript" src="/publicjs/jquery.jcarousellite.min.js"></script>
    <script type="text/javascript" src="/publicjs/script_jcarousellite.js"></script>
    <script src="/public/js/jquery.bgiframe.min.js" type="text/javascript"></script>
    <script src="/public/js/dimensions.js" type="text/javascript"></script>
    <script src="/public/js/jquery.autocomplete.js" type="text/javascript"></script>
    <link rel="Stylesheet" type="text/css" href="/public/js/jquery.autocomplete.css" />


</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="contentMain" runat="Server">
    <input type="hidden" id="hidUrl" runat="server" />
    <table border="0" width="100%" >
        <tr>
            <td class="menusection-Packages" colspan="4">
                
            </td>
            <td colspan="4" style="text-align: right;">
                <asp:Button ID="btnNext" runat="server" Visible="false" Text="下一步" OnClick="btnNext_Click" CssClass="widebuttons" />
                <asp:Button ID="btnBack" Text="返回" runat="server" OnClick="btnBack_Click" CssClass="widebuttons" CausesValidation="false" />
            </td>
        </tr>
        <tr >
<%--            <td class="oddrow">
                请选择媒体类型：
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:DropDownList ID="ddlNewMediaList" runat="server" CssClass="fixddl">
                    <asp:ListItem Selected="True" Text="平面媒体" Value="plane"></asp:ListItem>
                    <asp:ListItem Text="电视媒体" Value="tv"></asp:ListItem>
                    <asp:ListItem Text="网络媒体" Value="web"></asp:ListItem>
                    <asp:ListItem Text="广播媒体" Value="dab"></asp:ListItem>
                </asp:DropDownList>
            </td>--%>
            <td align="center"><asp:ImageButton class="imagebutton" runat="server" ID="ibPlane" ImageUrl="/images/logo01.gif" AlternateText="平面媒体" OnClick="btnPlane_Click" /></td>
            <td align="center"><asp:ImageButton class="imagebutton"  runat="server" ID="ibTv" ImageUrl="/images/logo02.gif" AlternateText="电视媒体" OnClick="btnTv_Click" /></td>
            <td align="center"><asp:ImageButton class="imagebutton"  runat="server" ID="ibWen" ImageUrl="/images/logo03.gif" AlternateText="网络媒体" OnClick="btnWeb_Click" /></td>
            <td align="center"><asp:ImageButton class="imagebutton"  runat="server" ID="ibDab" ImageUrl="/images/logo04.gif" AlternateText="广播媒体" OnClick="btnDab_Click" /></td>
        </tr>
<%--        <tr>
            <td class="oddrow" style="width: 20%">
                媒体中文名称：
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:TextBox ID="txtPlaneName" runat="server"></asp:TextBox><font color="red"> *</font>
            </td>
            <td class="oddrow" style="width: 20%">
                媒体英文名称：
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:TextBox ID="txtPlaneEngName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                媒体中文简称：
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtPlaneHTCName" runat="server"></asp:TextBox>
            </td>
            <td class="oddrow">
                媒体英文简称：
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtPlaneEngHTCName" runat="server"></asp:TextBox>
            </td>
        </tr>--%>
    </table>

</asp:Content>
