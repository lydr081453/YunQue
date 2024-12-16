<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Purchase_Requisition_ProductListForSearch" Codebehind="ProductListForSearch.aspx.cs" %>
<%@ Register Namespace="MyControls" TagPrefix="cc2" Assembly="MyControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Button ID="Button1" runat="server" Text=" 关闭 " CssClass="widebuttons" OnClientClick="window.close();" />
    &nbsp;<asp:Button runat="server" Text=" 上一步 " CssClass="widebuttons" OnClientClick="history.back();return false;" />
    <br /><br />
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading">
                目录物品列表
            </td>
        </tr>
        <tr>
            <td>
                <cc2:newgridview id="gvItem" runat="server" width="100%" autogeneratecolumns="false" AllowPaging="true" PageSize="20"
                     allowsorting="true">
                                                <Columns>
                                                    <asp:BoundField DataField="productName" HeaderText="物品名称" ItemStyle-HorizontalAlign="Center" SortExpression="productName"/>
                                                    <asp:BoundField DataField="productDes" HeaderText="描述" ItemStyle-HorizontalAlign="Center"/>
                                                    <asp:TemplateField HeaderText="参考价格" ItemStyle-HorizontalAlign="Right" SortExpression="ProductPrice">
                                                        <ItemTemplate>
                                                            <%# decimal.Parse(Eval("productprice").ToString()).ToString("#,##0.####")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="productUnit" HeaderText="单位" ItemStyle-HorizontalAlign="Center"/>
                                                    <asp:TemplateField HeaderText="供应商名称" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                                <%#Eval("suppliername")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    <asp:Label ID="labMsg" runat="server" Text=" 暂无目录物品" />
                                                </EmptyDataTemplate>
                                            </cc2:newgridview>
            </td>
        </tr>
    </table>
</asp:Content>
