<%@ Page Title="协议供应商列表" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Purchase_Requisition_SupplierListForSearch" Codebehind="SupplierListForSearch.aspx.cs" %>
<%@ Register Namespace="MyControls" TagPrefix="cc2" Assembly="MyControls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" src="../../public/js/DatePicker.js"></script>
    <script language="javascript" src="/public/js/jquery-1.2.6.js"></script>
    <script src="/public/js/jquery.bgiframe.min.js" type="text/javascript"></script>
    
    <script type="text/javascript">
        function openwindow(id) {
            window.open("SupplierInfoViewD.aspx?supplierId=" + id);
        }
        function printSupplier() {
            var win = window.open('print/supplierPrint.aspx', null, 'height=10px,width=10px,scrollbars=yes,top=100px,left=100px');
            win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
        }
    </script>
    <script type="text/javascript">
        function opens(id) {
            window.open("SupplierInfoViewD.aspx?supplierId=" + id);
        }
    </script>
    
       <table width="100%" class="tableForm">
        <tr>
            <td colspan="4" class="heading">
                检索
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">
                供应商名称:
            </td>
            <td class="oddrow-l" style="width: 35%" >
                <asp:TextBox ID="txtsupplierName" runat="server" Width="200px" />
            </td>
                            <td class="oddrow" style="width:10%">物料类别</td>
                <td class="oddrow-l" style="width:35%"><asp:TextBox ID="txtType" runat="server" Width="200px"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="oddrow">所属地区:</td>
            <td class="oddrow-l"><asp:TextBox ID="txtArea" runat="server" Width="200px" /></td>
            <td class="oddrow">联系人:</td>
            <td class="oddrow-l"><asp:TextBox ID="txtlinkName" runat="server" Width="200px" /></td>
        </tr>
        <tr>
            <td colspan="4" class="oddrow-l">
                <asp:Button ID="btnSearch" runat="server" Text=" 检索 " OnClick="btnSearch_Click" CssClass="widebuttons" />
            </td>
        </tr>
    </table>
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading">
                <asp:Literal ID="litTitle" Text="" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                 <cc2:NewGridView ID="gvSupplier" runat="server" AutoGenerateColumns="False" AllowSorting="true"
        OnRowDataBound="gv_RowDataBound" DataKeyNames="id" PageSize="20" AllowPaging="True" PagerSettings-Position="TopAndBottom"
        Width="100%" >
        <Columns>
            <asp:TemplateField HeaderText="供应商名称" HeaderStyle-Width="15%" SortExpression="supplier_name">
                <ItemTemplate>
                   <label id="lab" onclick="openwindow(<%#Eval("id") %>);" style="color:#7282a9;cursor:pointer">
                        <%#Eval("supplier_name")%></label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="supplier_city" HeaderText="所属地区" ItemStyle-HorizontalAlign="Center" SortExpression="supplier_area" />
            <asp:BoundField DataField="contact_name" HeaderText="联系人" ItemStyle-HorizontalAlign="Center" SortExpression="contact_name" />
            <asp:BoundField DataField="contact_mobile" ItemStyle-HorizontalAlign="Center" HeaderText="手机" />
            <asp:TemplateField HeaderText="联系电话">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate>
                    <asp:Label ID="labPhone" runat="server" Text='<%#Eval("contact_tel") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="传真">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate>
                    <asp:Label ID="labFax" runat="server" Text='<%#Eval("contact_fax") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="contact_email" HeaderText="电子邮件" ItemStyle-Width="10%" />
            <asp:BoundField DataField="supplier_industry" HeaderText="行业类别" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%" />
            <asp:BoundField DataField="business_price" HeaderText="price" />
            <asp:TemplateField HeaderText="报价" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%# Eval("business_price").ToString() == "" ? "" : "<a target='_blank' href='../../" + Eval("business_price").ToString() + "'><img src='/images/ico_04.gif' border='0' /></a>"%>
                    </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </cc2:NewGridView>
            </td>
        </tr>
    </table>
</asp:Content>