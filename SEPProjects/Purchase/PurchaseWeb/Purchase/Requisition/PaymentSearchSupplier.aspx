<%@ Page Language="C#" MasterPageFile="~/MasterPage.master"
    Inherits="Purchase_Requisition_PaymentSearchSupplier" Title="选择供应商" AutoEventWireup="true"
    EnableEventValidation="false" Codebehind="PaymentSearchSupplier.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="/public/css/dialog.css" type="text/css" rel="stylesheet" />

    <script language="javascript" src="/public/js/jquery-1.2.6.js"></script>

    <script language="javascript" src="/public/js/dialog.js"></script>

    <script language="javascript">
        function show(sname, sid) {
            dialog(sname, "url:get?/Purchase/Requisition/SupplierInfoView.aspx?isback=1&supplierId=" + sid, "900px", "400px", "text");
        }
        function show2() {

            dialog("物料类别结构图", "id:testID", "500px", "300px", "123123123");
        }

    </script>
    
    <br />
    <br />
        供应商名称:&nbsp;<asp:TextBox ID="txtSupplierName" runat="server" />&nbsp;&nbsp;<asp:Button
        Text=" 检索 " CssClass="widebuttons" runat="server" ID="btnSearch" />&nbsp;<asp:Button ID="btnClean"
            runat="server" CssClass="widebuttons" OnClick="btnClean_Click" Text=" 重新搜索 " />
    <br />
    <br />
    <asp:GridView ID="gv" runat="server" AutoGenerateColumns="false" AllowPaging="false"
        DataKeyNames="id" OnRowCommand="gv_RowCommand"  Width="100%"
        CellPadding="4">
        
        <Columns>
            <asp:ButtonField Text="选择" CommandName="Add" ButtonType="button" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="id" HeaderText="id" Visible="False"  ItemStyle-HorizontalAlign="Center" />
            <asp:TemplateField HeaderText="供应商名称">
                <ItemTemplate>
                    <span id="sp1" style="cursor: pointer" onclick="show('<%#Eval("supplier_name")%>','<%#Eval("id")%>');" style="color: blue">
                        <%#Eval("supplier_name")%></span>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="contact_name" HeaderText="联系人" />
            <asp:BoundField DataField="contact_tel" HeaderText="联系电话" />
        </Columns>
    </asp:GridView>

</asp:Content>
