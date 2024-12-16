<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    Title="选择推荐供应商" Inherits="Purchase_Requisition_ProposedSupplierList" CodeBehind="ProposedSupplierList.aspx.cs" %>

<%@ Register Namespace="MyControls" TagPrefix="cc2" Assembly="MyControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">

        function openwindow(id) {
            window.open("ProposedSupplierView.aspx?supplierId=" + id);
        }
        
    </script>

    <li><a href="ProposedSupplierEdit.aspx">添加新推荐供应商</a></li>
    <br />
    <br />
    <table width="100%" class="tableForm">
        <tr>
            <td colspan="4" class="heading">
                检索
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">
                推荐供应商名称:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox ID="txtsupplierName" runat="server" MaxLength="200" Width="200px" />
            </td>
            <td class="oddrow" style="width: 10%">
                物料类别
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox ID="txtType" runat="server" MaxLength="200" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                所属地区:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtArea" runat="server" MaxLength="200" Width="200px" />
            </td>
            <td class="oddrow">
                联系人:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtlinkName" runat="server" MaxLength="200" Width="200px" />
            </td>
        </tr>
        <tr>
            <td colspan="4" class="oddrow-l">
                <asp:Button ID="btnSearch" runat="server" Text=" 检索 " OnClick="btnSearch_Click" CssClass="widebuttons" />
            </td>
        </tr>
    </table>
    <br />
    <cc2:NewGridView ID="gvSupplier" runat="server" AutoGenerateColumns="False" OnRowDeleting="gvSupplier_RowDeleting"
        OnRowDataBound="gv_RowDataBound" DataKeyNames="id" PageSize="20" AllowPaging="True"  OnRowCommand="gvSupplier_RowCommand"
        AllowSorting="true" Width="100%" PagerSettings-Position="TopAndBottom">
        <Columns>
            <asp:TemplateField HeaderText="推荐供应商名称" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center"
                SortExpression="supplier_name">
                <ItemTemplate>
                    <label onclick="openwindow(<%#Eval("id") %>);" style="color: #7282a9; cursor: pointer">
                        <%#Eval("supplier_name")%></label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="supplier_area" HeaderText="所属地区" ItemStyle-HorizontalAlign="Center"
                SortExpression="supplier_area" />
            <asp:BoundField DataField="contact_name" HeaderText="联系人" ItemStyle-HorizontalAlign="Center"
                SortExpression="contact_name" />
            <asp:TemplateField HeaderText="联系电话">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate>
                    <asp:Label ID="labPhone" runat="server" Text='<%#Eval("contact_tel") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="contact_mobile" HeaderText="手机" ItemStyle-HorizontalAlign="Center" />
            <asp:TemplateField HeaderText="传真">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate>
                    <asp:Label ID="labFax" runat="server" Text='<%#Eval("contact_fax") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="contact_email" HeaderText="电子邮件" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="productTypes" HeaderText="物料类别" SortExpression="productTypes"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="supplier_source" HeaderText="供应商来源" ItemStyle-HorizontalAlign="Center" />
            <asp:TemplateField HeaderText="报价" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="4%">
                <ItemTemplate>
                    <%# Eval("business_price").ToString() == "" ? "" : "<a target='_blank' href='../../" + Eval("business_price").ToString() + "'><img src='/images/ico_04.gif' border='0' /></a>"%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="编辑" HeaderStyle-Width="4%">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate>
                    <a href='ProposedSupplierEdit.aspx?psupplierId=<%#DataBinder.Eval(Container.DataItem,"id")%>'>
                        <img src="../../images/edit.gif" border="0px;"></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="转换" HeaderStyle-Width="4%" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton2" CommandArgument='<%# Eval("id") %>' runat="server"
                        OnClientClick="return confirm('您确定转换为协议供应商吗？');" Text="<img src='/images/changed.gif' border='0' />"
                        ToolTip="转换为协议供应商" CommandName="Changed" CausesValidation="false" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="删除" HeaderStyle-Width="4%" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="return confirm('您确定删除吗？');"
                        Text="<img src='/images/disable.gif' border='0' />" CommandName="Delete" CausesValidation="false" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </cc2:NewGridView>
</asp:Content>
