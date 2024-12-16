<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeBehind="PRPriOrderList.aspx.cs" Inherits="PRPriOrderList" %>

<%@ Register Namespace="MyControls" TagPrefix="cc2" Assembly="MyControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%@ register src="../UserControls/View/PRTopMessage.ascx" tagname="PRTopMessage"
        tagprefix="uc1" %>

    <script language="javascript" src="../../public/js/DatePicker.js"></script>

    <script language="javascript" src="/public/js/jquery-1.2.6.js"></script>

    <script type="text/javascript" src="/public/js/jquery.jcarousellite.min.js"></script>

    <script type="text/javascript" src="/public/js/script_jcarousellite.js"></script>

    <script src="/public/js/jquery.bgiframe.min.js" type="text/javascript"></script>

    

    <uc1:PRTopMessage ID="PRTopMessage" runat="server" IsEditPage="true" />
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
                付款申请
            </td>
        </tr>
    </table>
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
                3000以上
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <cc2:NewGridView ID="gvMediaOrder" runat="server" AllowSorting="true" AutoGenerateColumns="False"
                    OnRowDataBound="gvMediaOrder_RowDataBound" DataKeyNames="id" PageSize="20" AllowPaging="True"
                    Width="100%" PagerSettings-Position="Bottom">
                    <Columns>
                        <asp:TemplateField HeaderText="流水号" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
                            <ItemTemplate>
                                <%# int.Parse(Eval("id").ToString()).ToString("0000000") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="申请单号" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
                            <ItemTemplate>
                                <%# Eval("prno") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="采购物品" DataField="Item_No" />
                        <asp:BoundField HeaderText="描述" DataField="desctiprtion" />
                        <asp:TemplateField HeaderText="金额">
                            <ItemTemplate>
                                <%# decimal.Parse(Eval("ototalprice").ToString()).ToString("#,##0.00")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </cc2:NewGridView>
            </td>
        </tr>
    </table>
    <br />
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
                3000以下
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <cc2:NewGridView ID="NewGridView1" runat="server" AllowSorting="true" AutoGenerateColumns="False"
                    OnRowDataBound="gvMediaOrder_RowDataBound" DataKeyNames="id" PageSize="20" AllowPaging="True"
                    Width="100%" PagerSettings-Position="Bottom">
                    <Columns>
                        <asp:TemplateField HeaderText="流水号" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
                            <ItemTemplate>
                                <%# int.Parse(Eval("id").ToString()).ToString("0000000") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="申请单号" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
                            <ItemTemplate>
                                <%# Eval("prno") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="采购物品" DataField="Item_No" />
                        <asp:BoundField HeaderText="描述" DataField="desctiprtion" />
                        <asp:TemplateField HeaderText="金额">
                            <ItemTemplate>
                                <%# decimal.Parse(Eval("ototalprice").ToString()).ToString("#,##0.00")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </cc2:NewGridView>
            </td>
        </tr>
    </table>
    <br />
    <prc:CheckPRInputButton type="button" id="btnCreate" runat="server" value=" 创建 " class="widebuttons"
        onclick="if(confirm('您确定要创建吗？')){ this.disabled=true;}else{return false;}" onserverclick="btnCreate_Click" />
    &nbsp;<asp:Button ID="btnBack" runat="server" Text=" 返回 " CssClass="widebuttons"
        OnClick="btnBack_Click" />
</asp:Content>
