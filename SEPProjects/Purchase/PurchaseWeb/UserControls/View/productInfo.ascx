<%@ Import Namespace="ESP.Purchase.Common" %>
<%@ Control Language="C#" AutoEventWireup="true" Inherits="UserControls_View_productInfo"
    CodeBehind="productInfo.ascx.cs" %>
<link href="../../public/css/dialog.css" type="text/css" rel="stylesheet" />

<script language="javascript" src="../../public/js/jquery-1.2.6.js"></script>

<script language="javascript" src="../../public/js/dialog.js"></script>

<script language="javascript">
    function showOrderMsgInfo(orderid) {
        dialog("查看新闻明细", "iframe:/Purchase/Requisition/OrderMsgDetail.aspx?orderid=" + orderid, "1000px", "500px", "text");
    }
</script>

<table border="0" cellspacing="1" runat="server" cellpadding="3" width="100%" id="tbNotify" style="background-color: Red;" visible="false">
    <tr>
        <td>
            <span style=" font-weight:bolder;color:White;">此项目利润率低于40%，请重点关注。</span>
        </td>
    </tr>
</table>
<table style="width: 100%;" class="tableForm">
    <tr>
        <td class="heading" colspan="4">
            ③ 采购物品信息
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            押金金额:
        </td>
        <td class="oddrow-l" colspan="3">
            <asp:Label ID="labforegift" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="oddrow" style="width: 15%">
            采购物料:
        </td>
        <td class="oddrow-l" style="width: 35%">
            <asp:Label ID="txtthirdParty_materielDesc" runat="server" />
        </td>
        <td class="oddrow" style="width: 15%">
            采购成本预算:
        </td>
        <td class="oddrow-l" style="width: 35%">
            <asp:Label ID="txtbuggeted" runat="server" />
        </td>
    </tr>
        <tr>
        <td class="oddrow" style="width: 15%">
            发票类型:
        </td>
        <td class="oddrow-l" style="width: 35%">
            <asp:Label ID="labInvoiceType" runat="server" />
        </td>
        <td class="oddrow" style="width: 15%">
            税率:
        </td>
        <td class="oddrow-l" style="width: 35%">
            <asp:Label ID="labTaxRate" runat="server" /> %
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:GridView ID="gdvItem" runat="server" AutoGenerateColumns="False" CellPadding="4"  OnRowDeleting="gdvItem_RowDeleting"
                Width="100%" DataKeyNames="ID,producttype" OnRowDataBound="gdvItem_RowDataBound"
                SkinID="gridviewSkin" EmptyDataText="请添加采购物品">
                <Columns>
                    <asp:BoundField DataField="id" HeaderText="序号" ItemStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="4%" />
                    <asp:TemplateField HeaderText="采购物品" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="lblItemNo" runat="server" Text='<%# Eval("productAttribute").ToString() == ((int)State.productAttribute.ml).ToString() ? ("* " + Eval("Item_No").ToString()) : Eval("Item_No")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="物料类别" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="lblType" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="描述" HeaderStyle-Width="18%">
                        <ItemTemplate>
                            <asp:Label ID="lblDesctiprtion" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="intend_receipt_date" HeaderText="预计收货时间" ItemStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="12%"></asp:BoundField>
                    <asp:TemplateField HeaderText="单价" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="8%">
                        <ItemTemplate>
                            <asp:Label ID="lblPrice" runat="server" Text='<%# decimal.Parse(Eval("price").ToString()).ToString("#,##0.##")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="uom" HeaderText="单位" ItemStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="8%" />
                    <asp:TemplateField HeaderText="数量" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="6%">
                        <ItemTemplate>
                            <%# decimal.Parse(Eval("quantity").ToString()).ToString("#,##0")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="小计" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="8%">
                        <ItemTemplate>
                            <asp:Label ID="lbltotal" runat="server" Text='<%# decimal.Parse(Eval("total").ToString()).ToString("#,##0.##")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="附件" HeaderStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label ID="labDown" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="删除" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnDelete" runat="server" OnClientClick="return confirm('您确定删除吗？');" Text="<img src='/images/disable.gif' border='0' />"
                                CommandName="Delete" Visible="false" CausesValidation="false" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td colspan="4" align="right" style="font-size: 12px">
            共<asp:Label ID="DgRecordCount" runat="server"></asp:Label>条数据,总金额
            <asp:Label ID="labTotalPrice" runat="server" />
            元
        </td>
    </tr>
    <tr>
        <td class="oddrow" colspan="4">
            <table width="100%" id="FCTable" runat="server" visible="false" >
                <tr>
                    <td class="heading">供应商框架合同</td>
                </tr>
                <tr>
                    <td class="oddrow-l">
                        <asp:GridView runat="server" ID="GvFCPr" Width="100%" AllowPaging="false" EmptyDataText="暂时没有相关记录" AutoGenerateColumns="False">
                            <Columns>
                                <asp:BoundField DataField="PrNo" HeaderText="Pr单号" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="supplier_name" HeaderText="供应商名称" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="desctiprtion" HeaderText="框架合同描述" />
                                <asp:TemplateField HeaderText="附件" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <a target='_blank' href="/Purchase/Requisition/UpfileDownload.aspx?OrderId=<%# Eval("orderId").ToString() %>&Index=0"><img src='/images/ico_04.gif' border='0' /></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td class="oddrow" style="width: 15%">
            备注:
        </td>
        <td colspan="3" class="oddrow-l">
            <asp:Label ID="labNote" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="oddrow" style="width: 15%">
            采购审批流向
        </td>
        <td class="oddrow-l">
            <font color="red">
                <asp:Label ID="lblValueLevel" runat="server" Width="400px"></asp:Label></font>
                </td>
        <td colspan="2"  class="oddrow-l"><asp:Label runat="server" ID="txtMsg" style=" color:Red;"></asp:Label></td>
    </tr>
</table>
