<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BijiaDlg2.aspx.cs" MasterPageFile="~/MasterPage.master"
    Title="比价要求" Inherits="PurchaseWeb.Purchase.BijiaDlg2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="/public/css/dialog.css" type="text/css" rel="stylesheet" />

    <script language="javascript" src="/public/js/jquery-1.2.6.js"></script>

    <script language="javascript" src="/public/js/dialog.js"></script>

    <script type="text/javascript" src="/public/js/jquery.blockUI.js"></script>

    <table width="100%" class="tableForm">
        <tr>
            <td class="oddrow-l" style=" background-color:Yellow;">
                <span style=" color:Red; font-weight:bolder;">您没有填写比价信息！请您按比价、询价要求上传报价信息。详见下图：</span>
            </td>
        </tr>
    </table>
    <br />
    <img src="/images/bijia.gif" width="739" height="271" />
</asp:Content>
