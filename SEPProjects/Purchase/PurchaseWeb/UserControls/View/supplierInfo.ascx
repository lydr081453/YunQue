<%@ Import Namespace="ESP.Purchase.Common" %>
<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="UserControls_View_supplierInfo" Codebehind="supplierInfo.ascx.cs" %>
    <asp:LinkButton ID="btnUpdateInfo" runat="server" OnClick="btnUpdateInfo_Click" />
    <link href="../../public/css/dialog.css" type="text/css" rel="stylesheet" />

<script language="javascript" src="../../public/js/jquery-1.2.6.js"></script>

<script language="javascript" src="../../public/js/dialog.js"></script>
    <script language="javascript">
    function SupplierClick() {
        var generalid = '<%= Request[RequestName.GeneralID]%>';
        var win = window.open('SupplierList.aspx?<% = RequestName.GeneralID%>=' + generalid + '&name=' + document.getElementById("<%= txtsupplier_name.ClientID %>").innerHTML+'&Operate=Media', null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
        win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
    }
    function showUserList(gid) {
        dialog("查看联系人", "iframe:/Purchase/Requisition/PrLinkManEditList.aspx?gid=" + gid, "1000px", "500px", "text");
    }
    function onPageRefresh() {
        window.location.reload();
    }
    </script>
<table style="width: 100%;" class="tableForm">
    <tr>
        <td class="heading" colspan="4">
            ④ 供应商信息</td>
    </tr>
        <tr>
        <td class="oddrow">
            供应商类型:
        </td>
        <td class="oddrow-l" colspan="3">
            <asp:Label ID="labOperationType" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="oddrow" style="width:15%">
            供应商名称:</td>
        <td class="oddrow-l" style="width:35%">
            <asp:Label ID="txtsupplier_name" runat="server"></asp:Label><asp:Button ID="btn" runat="server" Visible="false"
                                        OnClientClick="SupplierClick();return false;" Text="请选择..." CssClass="widebuttons" />
        </td>
        <td class="oddrow" style="width:15%">
            供应商来源:</td>
        <td class="oddrow-l" style="width:35%">
            <asp:Label ID="txtsource" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            <asp:Label ID="labSAShow" runat="server">供应商地址</asp:Label>:</td>
        <td class="oddrow-l">
            <asp:Label ID="txtsupplier_address" runat="server"></asp:Label>
        </td>
        <td class="oddrow">
            客户指定邮件附件:</td>
        <td class="oddrow-l">   
            <asp:Label ID="labEmailFile" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            供应商联系电话:</td>
        <td class="oddrow-l">
            <asp:Label ID="txtsupplier_phone" runat="server"></asp:Label>
        </td>
        <td class="oddrow">
            供应商联系人:</td>
        <td class="oddrow-l">
            <asp:Label ID="txtsupplier_linkman" runat="server"></asp:Label>
            <input type="button"  class="widebuttons" value="更换" ID="btnEditUser" runat="server"  />
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            供应商传真:</td>
        <td class="oddrow-l">
            <asp:Label ID="txtsupplier_fax" runat="server"></asp:Label>
        </td>
        <td class="oddrow">
            供应商手机:</td>
        <td class="oddrow-l">
            <asp:Label ID="txtsupplier_cellphone" runat="server" />
        </td>
    </tr>
    <tr>        
        <td class="oddrow">
            框架协议号码:</td>
        <td class="oddrow-l">
            <asp:Label ID="txtfa_no" runat="server"></asp:Label>
        </td>
        <td class="oddrow">供应商邮件:</td>
        <td class="oddrow-l"><asp:Label ID="txtsupplier_email" runat="server"></asp:Label></td>
    </tr>
</table>
<table width="100%" class="tableForm">
    <tr>
        <td class="heading" colspan="6">
            帐户信息
        </td>
    </tr>
    <tr>
        <td class="oddrow" style="width:10%">
            开户公司名称:
        </td>
        <td class="oddrow-l" style="width:23%">
            <asp:Label ID="labaccountName" runat="server" />
        </td>
        <td class="oddrow" style="width:10%">
            开户银行:
        </td>
        <td class="oddrow-l" style="width:23%">
            <asp:Label ID="labaccountBank" runat="server" />
        </td>
        <td class="oddrow" style="width:10%">
            帐号:
        </td>
        <td class="oddrow-l" style="width:23%">
            <asp:Label ID="labaccountNum" runat="server" />
        </td>
    </tr>
</table>
