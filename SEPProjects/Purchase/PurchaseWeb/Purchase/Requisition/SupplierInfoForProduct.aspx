<%@ Page Title="添加供应商" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    EnableEventValidation="false" Inherits="Purchase_Requisition_SupplierInfoForProduct" Codebehind="SupplierInfoForProduct.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%@ register src="../../UserControls/Edit/supplierInfo.ascx" tagname="supplierInfo"
        tagprefix="uc1" %>
<%--            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="updatePanel1" runat="server">
    <ContentTemplate>--%>
    <link href="/public/css/dialog.css" type="text/css" rel="stylesheet" />
    <script language="javascript" src="/public/js/jquery-1.2.6.js"></script>
    <script language="javascript" src="/public/js/dialog.js"></script>
    <script language="javascript">
        function show(sname, sid) {
            host = window.location.host;
            dialog(sname, "iframe:http://"+host+"/Purchase/Requisition/SupplierInfoView.aspx?isback=1&supplierId=" + sid, "900px", "400px", "text");
        }
        function show2() {

            dialog("物料类别结构图", "id:testID", "500px", "300px", "123123123");
        }
    </script>
    <uc1:supplierInfo ID="supplierInfo" runat="server" />
    <asp:ValidationSummary runat="server" ShowMessageBox="true" ShowSummary="false" />
    <table width="100%" border="0">
        <tr>
            <td><asp:Button ID="btnSave" runat="server" Text=" 提交 " CssClass="widebuttons" OnClick="btnSave_Click" />&nbsp;<input
        type="button" class="widebuttons" value=" 关闭 " onclick="window.close();" /></td>
        </tr>
    </table>

    <div id="testID" style="display: none">
        <div id="wl111" style="border-right: 1px inset; border-top: 1px inset; overflow: auto;
            border-left: 1px inset; width: 400px; border-bottom: 1px inset; height: 300px;
            background-color: white">
            <yyc:SmartTreeView ID="treeView" ShowLines="true" runat="server" Enabled="false"
                AllowCascadeCheckbox="True" ImageSet="Msdn" BackColor="#FFFFFF" Style="background-color: White"
                NodeIndent="10">
                <ParentNodeStyle Font-Bold="False" BackColor="#FFFFFF" />
                <HoverNodeStyle BackColor="#ffffff" BorderColor="#FFFFFF" BorderStyle="None" Font-Underline="False" />
                <SelectedNodeStyle BackColor="#FFFFFF" BorderColor="#FFFFFF" BorderStyle="None" Font-Underline="False" />
                <NodeStyle Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" HorizontalPadding="5px"
                    BackColor="#FFFFFF" NodeSpacing="1px" VerticalPadding="2px" />
            </yyc:SmartTreeView>
        </div>
    </div>
<%--        </ContentTemplate>
        <Triggers><asp:PostBackTrigger ControlID="btnSave" /></Triggers>
    </asp:UpdatePanel>--%>
</asp:Content>
